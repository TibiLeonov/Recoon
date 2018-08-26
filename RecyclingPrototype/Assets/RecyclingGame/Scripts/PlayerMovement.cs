using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Camera mainCamera;
    Animator anim;

    Rigidbody rb;
    public Vector3 movementInput;
    public Vector3 velocity;



    public float maxSpeed;
    public float timeToMaxSpeed;
    public float timeToZeroSpeed;
    float accelerationRate;
    float decelerationRate;

    public float timeToRotate;
    float rotationRate;

    //checks if its in rolling motion;
    public bool rolling;
    public bool rollingCD;
    public float rollingSpeed;
    public float rollingDuration;
    public float rollingCDDuration = 0.5f;
    Vector3 rollingDirection;

    //checks if you are holding something
    public bool holdingItem;
    public Transform holding;
    public Vector3 armHoldingPosition = new Vector3(1, 1, 0);
    public Vector3 pickUpOffset = new Vector3(0, 1, 1);
    public float pickUpRadius = 2;
    public LayerMask pickUpLayer = 9;
    private Transform modelObject;

    public Vector3 throwDirection = new Vector3(0, 1, 1);
    public float throwForce = 250;

    Vector3 lastInputedDirection = Vector3.zero;
    Vector3 rollingInput = Vector3.zero;
    //Vector3 smoothedRotation = Vector3.zero;

    Material groundMaterial;

    public ParticleSystem ps;
    public ParticleSystem trailCloud;

    public ParticleSystem footsteps;





    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
        //make sure the model is the first childed object
        modelObject = transform.GetChild(0).Find("mixamorig:Hips");
    }

    void Start() {


        accelerationRate = maxSpeed / timeToMaxSpeed;
        decelerationRate = maxSpeed / timeToZeroSpeed;
        rotationRate = 180f / timeToRotate;
    }


    void Update() {
        //so you don't move faster moving diagonally
        movementInput = Vector3.ClampMagnitude(movementInput, 1);
        movementInput = mainCamera.transform.TransformDirection(movementInput);
        anim.SetFloat("Speed", movementInput.magnitude);

        //setting the last inputing direction, used for rolling if no input
        if (movementInput != Vector3.zero)
        {
            lastInputedDirection = movementInput;
        }

        //lerp smoothed input to the current input
        rollingInput = Vector3.Lerp(rollingInput, movementInput, 0.04f);

        if (!rolling)
        {
            velocity = rb.velocity;
            if (movementInput != Vector3.zero)
            {


                velocity += new Vector3(movementInput.x, 0, movementInput.z).normalized * accelerationRate * Time.deltaTime;


            }
            else
            {


                velocity = new Vector3(velocity.x - velocity.x * decelerationRate * Time.deltaTime,
                                    // velocity.y - velocity.y * decelerationRate * Time.deltaTime,
                                    velocity.y,
                                    velocity.z - velocity.z * decelerationRate * Time.deltaTime);

            }

            Vector2 velNoGravity = Vector2.ClampMagnitude(new Vector2(velocity.x, velocity.z), maxSpeed);
            velocity = new Vector3(velNoGravity.x, velocity.y, velNoGravity.y);

            //velocity only updates while not rolling, to work with addforce
            rb.velocity = velocity;

            //normal rotation only while not rolling
            if (movementInput != Vector3.zero)
            {
                rb.MoveRotation(Quaternion.Euler(0, Mathf.Rad2Deg * Mathf.Atan2(velocity.x, velocity.z), 0));
            }
        }
        else
        {


            //velocity = rollingDirection * rollingSpeed;
            //velocity += new Vector3(rollingInput.x, 0, rollingInput.z).normalized * accelerationRate*10 * Time.deltaTime;
            // velocity += new Vector3(rollingInput.x, 0, rollingInput.z).normalized * Time.deltaTime;
            //  Vector2 velNoGravity = Vector2.ClampMagnitude(new Vector2(velocity.x, velocity.z), rollingSpeed);
            // velocity = new Vector3(velNoGravity.x, velocity.y, velNoGravity.y);


            rb.MoveRotation(Quaternion.Euler(0, Mathf.Rad2Deg * Mathf.Atan2(rollingInput.x, rollingInput.z), 0));

            //transform velocity into forward vector
            Vector3 forwardsVelocity = transform.forward * new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
            rb.velocity = new Vector3(forwardsVelocity.x, rb.velocity.y, forwardsVelocity.z);
        }



        //Vector3 normalizedInput = new Vector3(movementInput.x, 0, movementInput.z).normalized;

        //float currentAngle = ((transform.rotation.y) - Mathf.Rad2Deg * Mathf.Atan2(velocity.x, velocity.z))  ;
        // Debug.Log(currentAngle);




        //Debug.Log(transform.rotation.y);
        // float rotation = Mathf.Sign(Vector3.Cross(transform.forward, velocity).y);

        // float currentAngle = (transform.rotation.y ) * Mathf.Rad2Deg;
        //  Debug.Log();
        //  rb.MoveRotation(Quaternion.Euler(0,currentAngle, 0));
        //rb.MoveRotation(Quaternion.Euler(0,5,0));
        //  transform.rotation = Quaternion.Euler(0, transform.rotation.y + ( 5 * Time.deltaTime * Mathf.Rad2Deg), 0);
        //  Debug.Log(transform.rotation.y + (5 * Time.deltaTime * Mathf.Rad2Deg));

        //footstep particles
        GetGroundMaterial();
        if (groundMaterial != null)
        {
            if (footsteps.isStopped)
            {
                ParticleSystemRenderer psr = footsteps.GetComponent<ParticleSystemRenderer>();
                psr.material = groundMaterial;
                footsteps.Play();
            }
        }
        else
        {
            footsteps.Stop();
        }
    }


    

    public void GetGroundMaterial()
    {
        /*
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.position + Vector3.down * 1.5f, out hit)){
            if (hit.transform.parent.GetComponentInChildren<Renderer>())
            {
                groundMaterial = hit.transform.parent.GetComponentInChildren<Renderer>().material;
            }
        }
        else
        {
            groundMaterial = null;
        }
        */

       

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.parent != null)
        {
            if (collision.transform.parent.GetComponentInChildren<Renderer>())
            {
                groundMaterial = collision.transform.parent.GetComponentInChildren<Renderer>().material;
            }
            else
            {
                groundMaterial = null;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        groundMaterial = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1.5f);
    }


    public void DoRoll()
    {
        if (!rollingCD)
        {
            rollingCD = true;
            rolling = true;
            anim.SetBool("Rolling", true);
            rollingDirection = transform.forward;
            rollingInput = transform.forward;

            ps.Play();
            trailCloud.Play();


            rb.AddForce((transform.forward) * rollingSpeed * 30, ForceMode.Impulse);

            StartCoroutine(Rolling());
        }
    }

    IEnumerator Rolling()
    {
        

        yield return new WaitForSeconds(rollingDuration);
        rolling = false;
        anim.SetBool("Rolling", false);
        trailCloud.Stop();

        yield return new WaitForSeconds(rollingCDDuration);
        rollingCD = false;
        yield return null;



    }

    public void DoAction()
    {
        if (holdingItem)
        {
            ThrowItem();
        }
        else
        {
            PickUpItem();
        }

    }

    public void PickUpItem()
    {

        Collider[] hitColliders = Physics.OverlapSphere(pickUpOffset.x * transform.right + pickUpOffset.y * transform.up + pickUpOffset.z * transform.forward + transform.position, pickUpRadius, pickUpLayer);
        if (hitColliders.Length > 0)
        {
           
            holdingItem = true;
            holding = hitColliders[0].transform;
            holding.parent.GetComponent<Rigidbody>().isKinematic = true;
            holding.parent.GetComponent<Rigidbody>().MovePosition(this.transform.position + armHoldingPosition);
            //reposition to be local to the game object
            holding.parent.SetParent(transform);
            holding.parent.localPosition = armHoldingPosition;
            holding.parent.SetParent(modelObject);
            holding.parent.localEulerAngles = Vector3.zero;

        }
       
    }

    public void ThrowItem()
    {
        holding.parent.SetParent(null);
        holding.parent.GetComponent<Rigidbody>().isKinematic = false;
        
        holding.parent.GetComponent<Rigidbody>().AddForce((throwDirection.x * transform.right + throwDirection.y * transform.up + throwDirection.z * transform.forward)*throwForce, ForceMode.Impulse);
        
        //conserve momentum
        holding.parent.GetComponent<Rigidbody>().velocity += new Vector3(rb.velocity.x, 0, rb.velocity.z);


        holdingItem = false;
    }


    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(pickUpOffset.x * transform.right+ pickUpOffset.y * transform.up+pickUpOffset.z * transform.forward   + transform.position, pickUpRadius);
    }
}
