using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The core namespace that holds all the seamless logic of SpaceShooter.
/// </summary>
namespace SpaceShooterEngine
{
    /// <summary>
    /// A delegate method for timed actions.
    /// </summary>
    public delegate void TimerAction();

    /// <summary>
    /// A class that manages timed events.
    /// </summary>
    public class TimedAction
    {
        /// <summary>
        /// The maximum amount of time before the configured event triggers.
        /// </summary>
        public float maxTime;

        /// <summary>
        /// The elapsed time left before the event triggers.
        /// </summary>
        public float currentTime { get; private set; }

        /// <summary>
        /// The event that will trigger once time runs out.
        /// </summary>
        public TimerAction action;

        /// <summary>
        /// A private boolean field for handling the initial trigger.
        /// </summary>
        private bool triggerOnInitial;

        /// <summary>
        /// The constructor for the timed action.
        /// </summary>
        public TimedAction(float maxTime, TimerAction action, bool triggerOnInitial = true)
        {
            this.maxTime = maxTime;
            this.currentTime = maxTime;
            this.action = action;
            this.triggerOnInitial = triggerOnInitial;
        }

        /// <summary>
        /// Runs the timed action.
        /// </summary>
        /// <param name="time">
        /// The timer's basis for the countdown.
        /// </param>
        public void Run(float time)
        {
            currentTime -= time;
            if(currentTime > 0 && !triggerOnInitial)
                return;

            action();
            if(triggerOnInitial)
                triggerOnInitial = false;
            currentTime = maxTime;
        }
    }

    public class Entity : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Color realColor;
        public float maxHitPoints = 1f;
        public float movementSpeed = 0f;
        public float attackSpeed = 0f;
        public float scoreReward = 0f;
        public bool invulnerable = false;
        public bool cantHeal = false;
        [SerializeField] public string hexcode;

        [SerializeField] private float hitPoints;

        public float currentHitPoints
        {
            get { return hitPoints; }
        }

        void Awake()
        {
            ResetHealth();
            if(GetComponent<SpriteRenderer>())
                this.spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ResetHealth()
        {
            hitPoints = maxHitPoints;
        }

        public bool HasEqualColorWith(Entity entity)
        {
            string localColor = ColorUtility.ToHtmlStringRGBA(this.realColor);
            string otherColor = ColorUtility.ToHtmlStringRGBA(entity.realColor);
            return localColor == otherColor;
        }

        public bool Heal(float heal)
        {
            if(cantHeal)
                return false;

            float variance = maxHitPoints - hitPoints;
            bool healsFull = variance < heal;
            hitPoints += healsFull? variance : heal;

            if(healsFull)
                OnMaxHeal();

            return true;
        }

        public bool Damage(float damage)
        {
            if(invulnerable)
                return false;

            bool isKillingHit = damage >= this.hitPoints;
            this.hitPoints -= isKillingHit? this.hitPoints : damage;
            OnDamage();

            if(isKillingHit)
                OnZeroHitPoints();

            return true;
        }

        public void Move(Vector2 direction, float time, bool slerp = false)
        {
            float movement = this.movementSpeed * time;
            GameObject instance = this.gameObject;
            Quaternion lookDirection = Quaternion.LookRotation(transform.forward, -direction);
            Quaternion newRotation = slerp
                ? Quaternion.Slerp(transform.rotation, lookDirection, movement)
                : lookDirection;
            transform.Translate(Vector2.up * movement);
            transform.rotation = newRotation;
        }

        public Color GetEntityColor()
        {
            return Constants.EntityColor.TranslateHexToColor(this.hexcode);
        }

        public void AssignColor(string hexcode)
        {
            this.hexcode = hexcode;
            Color color = this.GetEntityColor();
            InjectColor(color);
        }

        public void InjectColor(Color color)
        {
            this.spriteRenderer.color = color;
            this.realColor = color;
        }

        public virtual void OnMaxHeal() {}

        public virtual void OnZeroHitPoints() {}

        public virtual void OnDamage() {}

        public virtual void OnObstacleHit() {}
    }

    public class Shooter
    {
        public GameObject instance;
        public GameObject bulletPrefab;
        public bool limitToColor = true;

        public Shooter(GameObject instance, GameObject bulletPrefab)
        {
            this.instance = instance;
            this.bulletPrefab = bulletPrefab;
        }

        public virtual GameObject InstantiateBullet()
        {
            return GameObject.Instantiate(this.bulletPrefab);
        }

        public GameObject SetupBulletValues(Vector2 direction, float speed, GameObject bullet)
        {
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bullet.transform.position = this.instance.transform.position;
            bulletRigidbody.AddForce(direction * speed);
            return bullet;
        }

        public GameObject Shoot(Vector2 direction, float speed)
        {
            GameObject bullet = this.InstantiateBullet();
            bullet = SetupBulletValues(direction, speed, bullet);
            return bullet;
        }
    }

    public class Projectile : Entity
    {
        public float damage = 1;
        public List<string> includes;
        public List<string> excludes;
        public float timeout = float.NaN;
        private float defaultTimeout = 10f;
        public bool destroyAlwaysOnHit = false;

        void Start()
        {
            Setup();
            float timeoutValue = float.IsNaN(this.timeout) ? this.timeout : this.defaultTimeout;
            Destroy(gameObject, timeoutValue);
        }

        public virtual void Setup() {}

        private void SendPayload(Entity entity)
        {
            BeforeAttachPayload(entity);
            AttachPayload(entity);
            AfterAttachPayload(entity);
        }

        public virtual void BeforeAttachPayload(Entity entity) {}

        public virtual void AttachPayload(Entity entity) {}

        public virtual void AfterAttachPayload(Entity entity) {}

        void OnTriggerEnter2D(Collider2D collider)
        {
            GameObject entityObject = collider.gameObject;
            Entity entity = entityObject.GetComponent<Entity>();
            string tag = entityObject.tag;
            Tags.Validate(tag);
            bool canDestroy = includes.Count > 0? includes.Contains(tag) : !excludes.Contains(tag);
            if(canDestroy)
            {
                if(entity)
                    SendPayload(entity);
                if(this.destroyAlwaysOnHit)
                    Destroy(gameObject);
            }
        }
    }

    public class ControlSystem
    {
        public GameObject gameObject;
        public Transform transform;

        public ControlSystem(GameObject gameObject)
        {
            this.gameObject = gameObject;
            this.transform = gameObject.transform;
        }

        /// <summary>
        /// Allows the gameObject's position to be controlled by the player
        /// through its transform translate method.
        /// </summary>
        public virtual void Navigate(Transform localTransform, float time, float speed)
        {
            float movement = time * speed;
            if(Input.GetKey(Configs.FORWARD))
            {
                localTransform.Translate(0, movement, 0);
            }
            else if (Input.GetKey(Configs.BACK))
            {
                localTransform.Translate(0, movement * -1, 0);
            }

            if(Input.GetKey(Configs.LEFT))
            {
                localTransform.Rotate(new Vector3(0, 0, 1) * movement * 100f);
            }
            else if (Input.GetKey(Configs.RIGHT))
            {
                localTransform.Rotate(new Vector3(0, 0, -1) * movement * 100f);
            }
        }

        public virtual void ClampMovement(
            Transform localTransform, float xClamp, float yClamp, float xOffset, float yOffset)
        {
            Vector3 position = localTransform.position;
            position.x = Mathf.Clamp(position.x, xClamp * -1 - xOffset, xClamp + xOffset);
            position.y = Mathf.Clamp(position.y, yClamp * -1 - yOffset, yClamp + yOffset);
            localTransform.position = position;
        }
    }

    public class Waypoint2D
    {
        public Entity instance;
        public Queue<Vector2> destinations;
        public List<Vector2> destinationSource;
        public Vector2 currentDestination { get; private set; }
        public float distanceOffset;
        public bool loop;
        public delegate void DestinationDelegate(Vector2 previous, Vector2 next);
        public bool disabled = false;
        public bool slerp = false;


        public Waypoint2D(Entity instance, List<Vector2> destinations, float distanceOffset)
        {
            this.instance = instance;
            this.destinationSource = destinations;
            this.distanceOffset = distanceOffset;
            this.destinations = new Queue<Vector2>();
            ResetDestinations();
        }

        public void ResetDestinations()
        {
            foreach(Vector2 destination in destinationSource)
                this.destinations.Enqueue(destination);
            this.currentDestination = this.destinations.Dequeue();
            disabled = false;
        }

        public void TravelToNextDestination(float time, DestinationDelegate onDestinationReach = null)
        {
            if(disabled)
                return;

            Vector2 entity = this.instance.transform.position;
            Vector2 destination = this.currentDestination;
            Vector2 direction = (entity - destination).normalized;
            this.instance.Move(direction, time, this.slerp);

            bool canGoToNext = Vector2.Distance(entity, destination) < this.distanceOffset;
            if(!canGoToNext)
                return;
            if(this.destinations.Count > 0)
                this.currentDestination = this.destinations.Dequeue();
            else
                if(loop)
                    ResetDestinations();
                else
                    disabled = true;

            if(onDestinationReach != null)
                onDestinationReach(destination, this.currentDestination);
        }
    }
}
