using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Weapon : MonoBehaviour
//{
//}

public class RayCastWeapon : MonoBehaviour
{

    public Transform firePoint;
    public int damage = 40;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;
    public AudioClip RayCastWeaponSound;
    private AudioSource audioSource;

    public void Shoot()
    {
        try
        {
            if (GetComponent<PlayerMovement>().IsJumping == false)
            {
                StartCoroutine(StartShoot());
            }
        }
        catch (System.NullReferenceException) { StartCoroutine(StartShoot()); }
    }

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    IEnumerator StartShoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        Debug.Log("play sound");

        audioSource.clip = RayCastWeaponSound;
        audioSource.Play();

        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            Crab crab = hitInfo.transform.GetComponent<Crab>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (crab != null)
            {
                crab.TakeDamage(damage);
            }

            if (hitInfo.collider.gameObject.tag == "Friendly")
            {
                hitInfo.collider.gameObject.GetComponent<YounGenTech.HealthScript.Health>()
                    .Damage(new YounGenTech.HealthScript.HealthEvent
                    {
                        Amount = 5,
                        EventObject = hitInfo.collider.gameObject
                    });
            }

            var bullet = Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
            Destroy(bullet, 2f);

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            Debug.Log(firePoint.position);
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }

        lineRenderer.enabled = true;

        yield return 0;

        lineRenderer.enabled = false;
    }
}
