using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Animator anim;

    public ParticleSystem explosion;
    

    public float delay=.6f;
    public float countdown;
    bool isExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        isExploded = false;
        anim=GetComponent<Animator>();
        explosion.Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isExploded) 
        {
            if (!explosion.isPlaying)
            {
                explosion.Play();
            }
            countdown -= Time.deltaTime;
            if (countdown <= 0) 
            {
                explosion.Stop();
                isExploded = false;
            }
        }
    }
    public void ShootAnim() 
    {
        countdown = delay;
        isExploded = true;
        anim.SetTrigger("Shoot");
    }
}
