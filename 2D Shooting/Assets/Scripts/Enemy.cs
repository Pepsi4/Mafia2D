using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Searching = 0,
        Attacking = 1
    }

    #region variables
    public RayCastWeapon RayCastWeapon;
    public int health = 100;
    [SerializeField] private string _enemyDiesAnimationName = "EnemyDies";

    public EnemyState EnemyStateCurrent;

    public Animation deathEffect;
    public GameObject Player;
    #endregion

    private void Start()
    {
        Shoot();

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    void Update()
    {
        if (Seek())
        {
            FlipToTarget(Player);
        }
    }


    private bool Seek()
    {
        if ((transform.position - Player.transform.position).magnitude < 5.0f)
        {
            Debug.Log("near");
            return true;
        }
        return false;
    }

    private void FlipToTarget(GameObject gameObject)
    {
        if (Player.transform.position.x > transform.position.x)
        {
            //face right

            FlipToRight();
            //transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Player.transform.position.x < transform.position.x)
        {

            FlipToLeft();
            //face left
            //transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FlipToLeft()
    {
        transform.rotation = new Quaternion(0, 180, 0, 0);
    }

    void FlipToRight()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
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
