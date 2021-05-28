using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleReset : MonoBehaviour
{
    public AudioSource winSound;
    public AudioClip winClip;
    public Transform initialPos;
    bool TargetHit;


    public PhysicMaterial ironPhy;
    public PhysicMaterial plasticPhy;
    public PhysicMaterial woodPhy;
    private BoxCollider colliderB;

    public Material ironMat;
    public Material plasticMat;
    public Material woodMat;
    private MeshRenderer mesh;


    public float massIron;
    public float massPlastic;
    public float massWood;

    public Dropdown dropdownMaterial;
    public enum MaterialType
    {
        Iron,
        Plastic,
        Wood
    }
    public MaterialType materialType;

    private Rigidbody rb;
    void Start()
    {
        transform.position = initialPos.position;
        transform.rotation = initialPos.rotation;
        TargetHit = false;

        rb = GetComponent<Rigidbody>();
        colliderB = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();

        colliderB.material = ironPhy;
        mesh.material = ironMat;
        rb.mass = massIron;
        materialType = MaterialType.Iron;

        dropdownMaterial.onValueChanged.AddListener(delegate { OnValueChangedHandler(dropdownMaterial); });
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

    public void OnValueChangedHandler(Dropdown sender)
    {
        if (sender.value == 0) materialType = MaterialType.Iron;
        else if (sender.value == 1) materialType = MaterialType.Plastic;
        else if (sender.value == 2) materialType = MaterialType.Wood;


        if (materialType == MaterialType.Iron)
        {
            colliderB.material = ironPhy;
            mesh.material = ironMat;
            rb.mass = massIron;
        }
        else if (materialType == MaterialType.Plastic)
        {
            colliderB.material = plasticPhy;
            mesh.material = plasticMat;
            rb.mass = massPlastic;
        }
        else if (materialType == MaterialType.Wood)
        {
            colliderB.material = woodPhy;
            mesh.material = woodMat;
            rb.mass = massWood;
        }
    }

}
