using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControler : MonoBehaviour
{

    public enum Material 
    {
    Iron,
    Plastic,
    Rubber
    }
    public Material material;

    public PhysicMaterial iron;
    public PhysicMaterial plastic;
    public PhysicMaterial rubber;

    public float massIron;
    public float massPlastic;
    public float massRubber;

    private Rigidbody rb;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        material = Material.Rubber;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   public void MoveLeft()
    {
        rb.velocity = new Vector3(-2f,rb.velocity.y,rb.velocity.z);
    }
    public void MoveRight()
    {
        rb.velocity = new Vector3(2f, rb.velocity.y, rb.velocity.z);
    }

}
