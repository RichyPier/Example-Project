using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    private float speed = 10.0f;
    private float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;

    public KeyCode switchKey;
    public string inputID;

    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;

    private float zBoundS = 25;
    private float zBoundN = 25;

    public int maxHealth = 6;
    public int currentHealth;

    public SpriteRenderer damageOverRenderer;
    public Sprite[] damageOverlays;
    private int currentDamageOverlay = -1;

    // public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        currentHealth = maxHealth;

        //gameManager.SetHealth(currentHealth);
    }

    void DisplayWelcomeMassage()
    {
        Debug.Log("Welcome, " + "firstName" + "Smith");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();

    }
//   private void OnMouseDrag()
//   {
//       int counter = 0;
//
//       if (counter < 100)
//       {
//           counter++;
//           Debug.Log("Counter " + counter);
//       }
//   }

    // Moves the Player based on arrow key input
    void MovePlayer()
    {
      float horizontalInput = Input.GetAxis("Horizontal");
      float verticalInput = Input.GetAxis("Vertical");

        // Move the Player-Body forward based on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        // Rotates the Player-Bodu on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

       playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if(collision.gameObject.CompareTag("Soul"))
        {
            Debug.Log("Player has collided with Soul");
        }
        if(collision.gameObject.CompareTag("Proteges"))
        {
            Debug.Log("Player has collided with Living Thing");
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collided with Death");
        }
        if (collision.gameObject.CompareTag("Powerup"))
        {
            Debug.Log("Player has collided with Power up");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }

    // Prevent the player from leaving the top or bottom of the screen
    void ConstrainPlayerPosition()
    {
        // Unsichtbare Wand unten
        if (transform.position.z < -zBoundS)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundS);
        }
        // Invisible Wall top
        if (transform.position.z > zBoundN)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundN);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //gameManager.SetHealth(currentHealth);

        currentDamageOverlay = Mathf.Min(++currentDamageOverlay, damageOverlays.Length - 1);
        damageOverRenderer.sprite = damageOverlays[currentDamageOverlay];

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);

           // gameManager.GameOver();

          //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
   

        

}
