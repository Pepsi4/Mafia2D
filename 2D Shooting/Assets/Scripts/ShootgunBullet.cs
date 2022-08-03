using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootgunBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    [SerializeField] private int _layerInvisibleForRayCast;

    // Use this for initialization
    void Start()
    {
        
    }

    public void SetVelocity(Vector2 vec)
    {
        rb.velocity = vec * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        if (hitInfo.gameObject.layer != _layerInvisibleForRayCast && hitInfo.gameObject.tag != "Bullet")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
