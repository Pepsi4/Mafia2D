using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public RayCastWeapon RayCastWeapon;
    public int health = 100;
    [SerializeField] private string _enemyDiesAnimationName = "EnemyDies";

    public Animation deathEffect;

    private void Start()
    {
        Shoot();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    Animator anim;
    void Die()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Dying", true);
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        RayCastWeapon.enabled = false;
        GetAnimPlayTime();
        Destroy(gameObject, _playTime);
    }

    float _playTime;
    void GetAnimPlayTime()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            _playTime = clip.length;
        }
    }

    void Shoot()
    {
        StartCoroutine(StartShooting());
    }

    IEnumerator StartShooting()
    {
        RayCastWeapon.Shoot();
        yield return new WaitForSeconds(1f);
        Shoot();
    }
}
