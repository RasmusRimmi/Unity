using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public float jumpHeight;
    public float gravityStrenght;
    public bool isGrounded;
    public Rigidbody playerRB;
    private float horizontalInput;
    private float forwardInput;
    public float coins;
    private GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        block = GameObject.Find("Block");

        coins = 0;   

        isGrounded = true;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 gravityS = new Vector3(0, gravityStrenght, 0);
        Physics.gravity = gravityS;

        // Get player input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        forwardInput = Input.GetAxisRaw("Vertical");

        // Moves the car base on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * forwardInput);
        // Turns the car based on horizontal input
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    //void FixedUpdate()
    //{
    //    Vector3 gravityS = new Vector3(0, gravityStrenght, 0);
    //    Physics.gravity = gravityS;

    //    // Get player input
    //    horizontalInput = Input.GetAxisRaw("Horizontal");
    //    forwardInput = Input.GetAxisRaw("Vertical");

    //    // Moves the car base on vertical input
    //    transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * forwardInput);
    //    // Turns the car based on horizontal input
    //    transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

    //    if (Input.GetKey(KeyCode.Space) && isGrounded)
    //    {
    //        Jump();
    //    }
    //}

    void Jump()
    {
        playerRB.AddForce(new Vector3(0, jumpHeight, 0));

        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Cube"))
        {
            isGrounded = true;
            Debug.Log("Maassa");
        }

        else if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("toimiiko");
            isGrounded = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins = coins + 1;
            Destroy(other.gameObject);

            if (coins == 5)
            {
                block.SetActive(false);
            }
        }
    }


}
