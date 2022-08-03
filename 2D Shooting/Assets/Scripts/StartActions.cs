using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartActions : MonoBehaviour
{
    public Animator ExplosionAnimator;
    public string ExplosionAnimation;
    float explosionLifeTime = 2f;

    public Animator WhiteScreenAnimator;
    public string WhiteScreenAnimation;

    

    void Start()
    {
        ExplosionAnimator.Play(ExplosionAnimation);
        WhiteScreenAnimator.Play(WhiteScreenAnimation);

        Destroy(ExplosionAnimator.gameObject, explosionLifeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
