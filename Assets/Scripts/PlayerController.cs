using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1.0f;
    public int pickupCount; //Number of pickups in our scene
    int totalPickups;
    private bool wonGame = false;
    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text winText;
    public GameObject inGamePanel;
    public GameObject winPanel;
    public Image pickupFill;
    float pickupChunk;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;
      

    void Start()
    {
        //Turn off our win panel object
        winPanel.SetActive(false);
        //Turn on our in game panel
        inGamePanel.SetActive(true);
        //Gets the rigidbody component attached to this game object
        rb = GetComponent<Rigidbody>();
        

        //Work out how many pickups are in the scene and store in pickupCount
        pickupCount = GameObject.FindGameObjectsWithTag("Pickups").Length;
        //Assign the amount of pickups to the total pickups
        totalPickups = pickupCount;
        //Work out the amount of fill for our pickup fill
        pickupChunk = 0.1f / totalPickups;
        //Display the pickups to the user
        CheckPickups();
        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;

        Time.timeScale = 1;
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickups")) ;
        {
            //Decrement the pickupCount when we collide with a pickup
            pickupCount -= 1;
            //Increase the fill amount of our pickup fill image
            pickupFill.fillAmount = pickupFill.fillAmount + pickupChunk;
            //Display the pickups to the user
            CheckPickups();
            Destroy(other.gameObject);
        }
        
    }

    void CheckPickups()
    {
        //Display the new pickup count to the player
        Debug.Log("Pickup Count: " + pickupCount);
        scoreText.text = "Pickups Left: " + pickupCount.ToString() + "/" + totalPickups.ToString();

        //Check if the pickupcount == 0 
        if (pickupCount == 0)
        {
            //if pickup count == 0, display win message
            Debug.Log("You Win");
            winPanel.SetActive(true);
            //Turn off our in game panel
            inGamePanel.SetActive(false);
            //remove controls from player
            wonGame = true;
            //Set the velocity of the rigidbody to zero
            rb.velocity = Vector3.zero;
        }
    }

        //temporary reset functionality
     public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    

    void FixedUpdate()
    {
        if (resetting)
            return;
        //Store the horizontal axis value in a float
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the vertical axis value in a float
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //Add force to our rigid body from our vector times our speed
        rb.AddForce(movement * speed);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i +=Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;
    }
}
