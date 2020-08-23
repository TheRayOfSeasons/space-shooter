using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1;
    public List<string> includes;
    public List<string> excludes;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        Tags.Validate(tag);
        bool canDestroy = includes.Count > 0? includes.Contains(tag) : !excludes.Contains(tag);
        if(canDestroy)
            Destroy(gameObject);
    }
}
