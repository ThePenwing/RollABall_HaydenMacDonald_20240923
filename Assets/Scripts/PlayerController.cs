using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;
    private int count;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;
    public float jumpPower = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private bool grounded = true;

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        count = 0;
        SetCountText();
        rb = GetComponent<Rigidbody>();
        winTextObject.SetActive(false);
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;

        //print("MovementX " + movementX);
        //print("MovementY " + movementY);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winTextObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        Quaternion camRotationFlattened = Quaternion.LookRotation(camForward);
        movement = camRotationFlattened * movement;

        //print(movement);

        // Apply force to the Rigidbody to move the player.

        if (grounded)
        {
            rb.AddForce(movement * speed);
        }
    }

    private bool IsGrounded()
    {
        var Height = GetComponent<SphereCollider>().radius;
        bool grounded;

        if (Physics.Raycast(transform.position, Vector3.down, Height * 1.1f))
        {
            print("Grounded");
            grounded = true;
        }
        else
        {
            print("Not grounded");
            grounded = false;
        }
        return grounded;
    }

    private void Update()
    {
        grounded = IsGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(new Vector3(0, 1f, 0) * jumpPower);
            print("Jumped");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }
}