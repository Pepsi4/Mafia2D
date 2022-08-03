using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public GameObject Player;

    private int health = 500;
    bool isAttacking = false;

    void Start()
    {
        health = 500;
        Player = GameObject.Find("Player");
        scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDying == false)
        {
            if (Seek())
            {
                FlipToTarget(Player);
                if (isAttacking == false)
                {
                    Attack();
                    isAttacking = true;
                }
            }
            else
            {
                isAttacking = false;
                try { StopCoroutine(attackingCoroutine); }
                catch (System.NullReferenceException ex) { }
            }
        }
    }

    private IEnumerator attackingCoroutine;

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(2f);
        if (isAttacking)
        {
            Debug.Log("Attack!");
            Attack();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(StartAttack());
        GetComponent<Animator>().Play("attack");



        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<YounGenTech.HealthScript.Health>()
                   .Damage(new YounGenTech.HealthScript.HealthEvent
                   {
                       Amount = 25,
                       EventObject = collision.gameObject
                   });
        }

    }



    void Attack()
    {
        attackingCoroutine = StartAttack();
        StartCoroutine(attackingCoroutine);
    }

    private bool Seek()
    {
        if ((transform.position - Player.transform.position).magnitude < 12f)
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
            FlipToLeft();
            
            MoveToPlayer();
        }
        else if (Player.transform.position.x < transform.position.x)
        {
            FlipToRight();
            MoveToPlayer();
        }
    }
    public float speed = 0.2f;
    void MoveToPlayer()
    {
        //if (isLerping)
        //{

        //}
        transform.Translate((Player.transform.position - transform.position) * Time.deltaTime * speed);
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
        Debug.Log(damage);
        Debug.Log(health);
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    Animator anim;

    public ScoreController scoreController;
    int scorePrice = 150;
    bool isDying = false;
    void Die()
    {
        if(isDying == false)
        {
            Debug.Log("die");
            isDying = true;
            anim = GetComponent<Animator>();
            anim.SetBool("Dying", true);
            GetComponent<AudioSource>().Play();
            //Instantiate(deathEffect, transform.position, Quaternion.identity);
            //  RayCastWeapon.enabled = false;
            // GetAnimPlayTime();
            scoreController.Score += scorePrice;
            scoreController.UpdateScoreUI();
            Destroy(gameObject, 0.9f);
        }
    }
}
