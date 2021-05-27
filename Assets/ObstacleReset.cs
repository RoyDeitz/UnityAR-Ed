using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleReset : MonoBehaviour
{
    public AudioSource winSound;
    public AudioClip winClip;
    public Transform initialPos;
    bool TargetHit;
    void Start()
    {
        transform.position = initialPos.position;
        transform.rotation = initialPos.rotation;
        TargetHit = false;
    }

    public void ResetPos() 
    {
        transform.position = initialPos.position;
        transform.rotation = initialPos.rotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Target")
        {
            TargetHit = true;
            StartCoroutine(TargetVerificationCoroutine());
        }
        else if (other.tag == "Respawner") 
        {
            ResetPos();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Target") 
        {
            TargetHit = false;
        }
    }
    IEnumerator TargetVerificationCoroutine()
    {
        
        yield return new WaitForSeconds(4);
        if (TargetHit) 
        {
            //winFeedBack/TheoryFeedback
            winSound.PlayOneShot(winClip);
            //Debug.Log("YouW!!!!!!in");
            ResetPos();
            StopAllCoroutines();
        }
       
    }
}
