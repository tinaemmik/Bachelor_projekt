using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public Rigidbody2D rb;
    //public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        BossHealth boss = hitInfo.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(gameObject);
    }
}
