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
    public ScoreController scoreController;

    private bool isShooting = false;

    public Animation deathEffect;
    public GameObject Player;
    #endregion

    private void Start()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    void Update()
    {
        if (isDying == false)
        {
            if (Seek())
            {
                FlipToTarget(Player);
                if (isShooting == false)
                {
                    Shoot();
                    isShooting = true;
                }
            }
            else
            {
                isShooting = false;
                try { StopCoroutine(shootingCoroutine); }
                catch (System.NullReferenceException ex) { }
            }
        }
    }


    private bool Seek()
    {
        if ((transform.position - Player.transform.position).magnitude < 6.7f)
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
            FlipToRight();
        }
        else if (Player.transform.position.x < transform.position.x)
        {
            FlipToLeft();
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


    private int scorePrice = 25;
    Animator anim;
    bool isDying = false;
    void Die()
    {
        if (isDying == false)
        {
            isDying = true;
            anim = GetComponent<Animator>();
            anim.SetBool("Dying", true);
            //Instantiate(deathEffect, transform.position, Quaternion.identity);
            RayCastWeapon.enabled = false;
            GetAnimPlayTime();
            scoreController.Score += scorePrice;
            scoreController.UpdateScoreUI();
            Destroy(gameObject, _playTime);
        }

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

    private IEnumerator shootingCoroutine;

    void Shoot()
    {
        shootingCoroutine = StartShooting();
        StartCoroutine(shootingCoroutine);
    }

    IEnumerator StartShooting()
    {
        RayCastWeapon.Shoot();
        yield return new WaitForSeconds(2f);
        if (isShooting)
        {
            Debug.Log("Shoot!");
            Shoot();
        }
    }
}
