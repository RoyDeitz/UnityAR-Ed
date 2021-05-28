using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject cannonHead;
    public GameObject btnControlGroup;
    public GameObject shootButton;

    public Dropdown dropdownMaterial;

    public Slider forceSlider;
    public Slider angleSlider;

    public Transform cannon;

    public Text txtForce;
    public Text txtAngle;

    void Start()
    {
        transform.position = initialPosition.position;
        rb = GetComponent<Rigidbody>();
        materialType = MaterialType.Rubber;
        ballState = BallState.BallControl;

        colliderS = GetComponent<SphereCollider>();
        mesh = GetComponent<MeshRenderer>();

        colliderS.material = ironPhy;
        mesh.material = ironMat;
        rb.mass = massIron;

        dropdownMaterial.onValueChanged.AddListener(delegate { OnValueChangedHandler(dropdownMaterial);  });
        forceSlider.onValueChanged.AddListener(delegate { OnForceChanged(forceSlider); });
        angleSlider.onValueChanged.AddListener(delegate { OnAngleChanged(angleSlider); });
      
    }

    // Update is called once per frame
    void Update()
    {
        if (ballState == BallState.CannonSet)
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = cannonHead.transform.position;
            btnControlGroup.SetActive(false);
            angleSlider.enabled = true;
            forceSlider.enabled = true;
            shootButton.SetActive(true);
        }
        else if (ballState == BallState.BallControl)
        {
            rb.useGravity = true;

            btnControlGroup.SetActive(true);
            
            forceSlider.enabled = true;
            shootButton.SetActive(false);
        }
        else if(ballState == BallState.FreeFall)
        {
            rb.useGravity = true;
            btnControlGroup.SetActive(false);
            forceSlider.enabled = false;
            angleSlider.enabled = false;
            shootButton.SetActive(false);


        }

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
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Respawner") 
        {
            ResetPlayer();
        }

        if (other.tag == "Cannon") 
        {
            ballState = BallState.CannonSet;
        }
    }

    public void OnValueChangedHandler(Dropdown sender) 
    {
        if (sender.value == 0) materialType = MaterialType.Iron;
        else if (sender.value == 1) materialType = MaterialType.Plastic;
        else if (sender.value == 2) materialType = MaterialType.Rubber;


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

    public void OnForceChanged(Slider slider) 
    {
        force = slider.value;
        //txtForce.text = "Force: "+(force/100).ToString()+" N";
    }

    public void OnAngleChanged(Slider slider) 
    {
        cannon.localRotation = Quaternion.Euler(-slider.value,cannon.localRotation.y,cannon.localRotation.z);
        txtAngle.text= "Angle: " + slider.value.ToString() + "°";
    }

    public void Shoot() 
    {
        ballState = BallState.FreeFall;
        Vector3 direction = cannonHead.transform.position - cannon.position;
        //isShootable = true;
        rb.AddForce(direction*force*.01f,ForceMode.Impulse);
       
    }

    public void ResetPlayer() 
    {
        rb.velocity = Vector3.zero;
        this.transform.rotation = initialPosition.rotation;
        this.transform.position = initialPosition.position;
        ballState = BallState.BallControl;
    }
}
