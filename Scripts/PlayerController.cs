using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //initial variables 
    private Camera cam;
    private Rigidbody2D rb;
    private Vector2 movement;

    //To be able to set movement speed 
    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private GameObject bomb;

    public bool isPowerup = false;
    public float powerUpStrength = 5f;
    


// Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();  //using physics based engine, so get Rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();  //update is called every frame, we need to see if any input is provided. 
        if(Input.GetKeyDown(KeyCode.Space))
        {
        StartCoroutine(ShootTheBomb());
        }
       
    }

    private void FixedUpdate()
    {
         rb.velocity = movement * speed;  //physics based movement. 
    }

    private void GetInput()  //GetAxis for the inputs
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        
        
    }

    //For the bombs 
    IEnumerator ShootTheBomb()
    {
        Vector3 temp = transform.position;
        temp.y +=2.5f;
        Instantiate(bomb, temp, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
    }


    private void MouseDirection()
    {
        if(cam is null) return; //if there is no camera it wont move or do anything

        // if the camera is active, the player will look in the direction of the MouseDirection
        // Movement will be through the Horizontal and Vertical axis as defined in GetInput()

        var dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            isPowerup = true;
            Destroy(other.gameObject);
            Debug.Log("Power Up!");
            StartCoroutine(PowerUpCountDown());
        }
   }

   IEnumerator PowerUpCountDown()
   {
       yield return new WaitForSeconds(7);
       isPowerup = false;
   }
    


   private void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.CompareTag("Enemy") && isPowerup)
        {
           Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
           Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
           enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse); 
           Destroy(collision.gameObject);
        }
    }
}