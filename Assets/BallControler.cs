using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;

public class BallControler : MonoBehaviour
{

    public enum MaterialType
    {
    Iron,
    Plastic,
    Rubber
    }
    public MaterialType materialType;

    public enum BallState 
    {
    BallControl,
    CannonSet,
    FreeFall
    
    }
    public BallState ballState;
    public PhysicMaterial ironPhy;
    public PhysicMaterial plasticPhy;
    public PhysicMaterial rubberPhy;
    private SphereCollider colliderS;

    public Material ironMat;
    public Material plasticMat;
    public Material rubberMat;
    private MeshRenderer mesh;
  

    public float massIron;
    public float massPlastic;
    public float massRubber;

    private Rigidbody rb;


    public float force;

    public Transform initialPosition;
    public Transform cannonHead;
    public GameObject btnControlGroup;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        materialType = MaterialType.Rubber;
        ballState = BallState.BallControl;

        colliderS = GetComponent<SphereCollider>();
        mesh = GetComponent<MeshRenderer>();

        colliderS.material = ironPhy;
        mesh.material = ironMat;
        rb.mass = massIron;
    }

    // Update is called once per frame
    void Update()
    {
       

    }

   public void MoveLeft()
    {
        rb.AddForce(-force , 0f,0f,ForceMode.Impulse);
    }
    public void MoveRight()
    {
        rb.AddForce(force , 0f, 0f, ForceMode.Impulse);
    }
    public void MoveForward()
    {
        rb.AddForce(0f, 0f, force, ForceMode.Impulse);
    }
    public void MoveBackward()
    {
        rb.AddForce(0f, 0f,-force, ForceMode.Impulse);
    }
    public void Jump() 
    {
        rb.AddForce(0f, force , 0f,ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Respawner") 
        {
            rb.velocity = Vector3.zero;
            this.transform.rotation = initialPosition.rotation;
            this.transform.position = initialPosition.position;
        }

        if (other.tag == "Cannon") 
        {
        
        }
    }

    public void OnValueChanged(int val) 
    {
        if (val == 0) materialType = MaterialType.Iron;
        else if (val == 1) materialType = MaterialType.Plastic;
        else if (val == 2) materialType = MaterialType.Rubber;


        if (materialType == MaterialType.Iron)
        {
            colliderS.material = ironPhy;
            mesh.material = ironMat;
            rb.mass = massIron;
        }
        else if (materialType == MaterialType.Plastic)
        {
            colliderS.material = plasticPhy;
            mesh.material = plasticMat;
            rb.mass = massPlastic;
        }
        else if (materialType == MaterialType.Rubber)
        {
            colliderS.material = rubberPhy;
            mesh.material = rubberMat;
            rb.mass = massRubber;
        }
    }
}
