using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float delay = 2f;
 
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
