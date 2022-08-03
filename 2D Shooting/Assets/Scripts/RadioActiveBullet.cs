using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioActiveBullet : MonoBehaviour
{
    public AudioSource AudioSource;
    bool isDead = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && isDead == false)
        {
            isDead = true;
            GetComponent<RadioActiveBullet>().enabled = false;
            collision.gameObject.GetComponent<YounGenTech.HealthScript.Health>().Damage(
                new YounGenTech.HealthScript.HealthEvent(collision.gameObject, 25));

            AudioSource.Play();
            Destroy(this.gameObject.transform.parent.gameObject, 0.1f);

        }
    }
}
