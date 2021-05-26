using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControler : MonoBehaviour
{

    public enum MaterialType
    {
    Iron,
    Plastic,
    Rubber
    }
    public MaterialType material;

    public PhysicMaterial ironPhy;
    public PhysicMaterial plasticPhy;
    public PhysicMaterial rubberPhy;

    public Material ironMat;
    public Material plasticMat;
    public Material rubberMat;

    public float massIron;
    public float massPlastic;
    public float massRubber;

    private Rigidbody rb;


    public float force;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        material = MaterialType.Rubber;
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

}
