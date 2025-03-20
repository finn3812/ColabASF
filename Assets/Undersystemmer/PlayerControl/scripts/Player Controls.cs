using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 5f; // Bev�gelseshastighed
    public float jumpForce = 5f; // Kraft til at hoppe
    private Rigidbody rb;
    private bool isGrounded = true; // Tjekker om spilleren er p� jorden

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Henter Rigidbody-komponenten
    }

    void FixedUpdate()
    {
        // Input fra WASD-tasterne
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D eller venstre/h�jre pile
        float moveVertical = Input.GetAxis("Vertical"); // W/S eller op/ned pile

        // Beregn bev�gelsesretningen
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Flyt objektet
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        // Tjek for hop-input og om spilleren er p� jorden
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Spilleren er i luften
        }
    }

    // Tjek om spilleren r�rer jorden
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Spilleren er tilbage p� jorden
        }
    }
}
