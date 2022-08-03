using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabWeapon : MonoBehaviour
{
    float destroyTime = 0.2f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioClip ShotgunSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        var bullet_1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet_1.GetComponent<ShootgunBullet>().SetVelocity(new Vector2(transform.right.x, 0.33f));

        var bullet_2 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet_2.GetComponent<ShootgunBullet>().SetVelocity(new Vector2(transform.right.x, 0f));

        var bullet_3 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet_3.GetComponent<ShootgunBullet>().SetVelocity(new Vector2(transform.right.x, -0.33f));

        Destroy(bullet_1, destroyTime);
        Destroy(bullet_2, destroyTime);
        Destroy(bullet_3, destroyTime);

        audioSource.clip = ShotgunSound;
        audioSource.Play();
    }
}
