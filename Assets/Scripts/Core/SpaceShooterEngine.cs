using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterEngine
{
    public delegate void TimerAction();

    public class TimedAction
    {
        public float maxTime;
        public float currentTime { get; private set; }
        public TimerAction action;
        private bool initialAction = true;

        public TimedAction(float maxTime, TimerAction action)
        {
            this.maxTime = maxTime;
            this.currentTime = maxTime;
            this.action = action;
        }

        public void Run(float time)
        {
            currentTime -= time;
            if(currentTime > 0 && !initialAction)
                return;

            action();
            if(initialAction)
                initialAction = false;
            currentTime = maxTime;
        }
    }

    public class Shooter
    {
        public GameObject instance;
        public GameObject bulletPrefab;

        public Shooter(GameObject instance, GameObject bulletPrefab)
        {
            this.instance = instance;
            this.bulletPrefab = bulletPrefab;
        }

        public GameObject Shoot(Vector2 direction, float speed)
        {
            GameObject bullet = GameObject.Instantiate(this.bulletPrefab);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bullet.transform.position = instance.transform.position;
            bulletRigidbody.AddForce(direction * speed);
            return bullet;
        }
    }

    public class Entity : MonoBehaviour
    {
        public float maxHitPoints;
        public bool invulnerable;
        public bool cantHeal;

        [SerializeField] private float hitPoints;

        public float currentHitPoints
        {
            get { return hitPoints; }
        }

        void Awake()
        {
            ResetHealth();
        }

        public void ResetHealth()
        {
            hitPoints = maxHitPoints;
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

            bool isKillingHit = damage > this.hitPoints;
            this.hitPoints -= isKillingHit? this.hitPoints : damage;
            OnDamage();

            if(isKillingHit)
                OnZeroHitPoints();

            return true;
        }

        public virtual void OnMaxHeal() {}

        public virtual void OnZeroHitPoints() {}

        public virtual void OnDamage() {}

        public virtual void OnObstacleHit() {}
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
        public virtual void Navigate(float time, float speed)
        {
            float movement = time * speed;
            if(Input.GetKey(Configs.FORWARD))
            {
                transform.Translate(0, movement, 0);
            }
            else if (Input.GetKey(Configs.BACK))
            {
                transform.Translate(0, movement * -1, 0);
            }

            if(Input.GetKey(Configs.LEFT))
            {
                transform.Translate(movement * -1, 0, 0);
            }
            else if (Input.GetKey(Configs.RIGHT))
            {
                transform.Translate(movement, 0, 0);
            }
        }

        public virtual void ClampMovement(float xClamp, float yClamp, float xOffset, float yOffset)
        {
            Vector3 position = this.transform.position;
            position.x = Mathf.Clamp(position.x, xClamp * -1 - xOffset, xClamp + xOffset);
            position.y = Mathf.Clamp(position.y, yClamp * -1 - yOffset, yClamp + yOffset);
            this.transform.position = position;
        }
    }
}
