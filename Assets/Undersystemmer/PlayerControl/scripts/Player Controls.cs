using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 5f; // Bevægelseshastighed
    public float rotationSpeed = 100f; // Rotationshastighed
    public float jumpForce = 5f; // Kraft til at hoppe
    private Rigidbody rb;
    private bool isGrounded = true; // Tjekker om spilleren er på jorden

    private GameObject carriedObject = null; // Objekt spilleren bærer
    public Transform carryPosition; // Position hvor objektet holdes

    private GameObject nearbyObject = null; // Objekt, som spilleren kan samle op

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Henter Rigidbody-komponenten
    }

    void FixedUpdate()
    {
        // Input til bevægelse og rotation
        float moveVertical = Input.GetAxis("Vertical"); // W/S for fremad/bagud
        float rotationHorizontal = Input.GetAxis("Horizontal"); // A/D for rotation

        // Bevægelse fremad eller bagud
        Vector3 movement = transform.forward * moveVertical;
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);

        // Rotation til venstre eller højre
        float rotation = rotationHorizontal * rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0);
    }

    void Update()
    {
        // Tjek for hop-input og om spilleren er på jorden
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Spilleren er i luften
        }

        // Pickup og placement
        if (Input.GetKeyDown(KeyCode.E)) // Saml op
        {
            if (carriedObject == null && nearbyObject != null)
            {
                PickupObject();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R)) // Placér
        {
            PlaceObject();
        }
    }

    private void PickupObject()
    {
        carriedObject = nearbyObject; // Sæt det nærmeste objekt som det bårne objekt
        carriedObject.GetComponent<Rigidbody>().isKinematic = true; // Deaktiver fysik
        carriedObject.transform.position = carryPosition.position; // Flyt til bæreposition
        carriedObject.transform.parent = carryPosition; // Gør objektet til barn af spilleren
    }

    private void PlaceObject()
    {
        if (carriedObject != null) // Kun forsøg at placere, hvis spilleren bærer noget
        {
            carriedObject.GetComponent<Rigidbody>().isKinematic = false; // Aktivér fysik igen
            carriedObject.transform.parent = null; // Fjern objektet fra spilleren
            carriedObject = null; // Nulstil carriedObject
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup")) // Hvis objektet er markeret som et Pickup-objekt
        {
            nearbyObject = other.gameObject; // Registrer det nærmeste objekt
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup")) // Hvis spilleren forlader området omkring Pickup-objektet
        {
            nearbyObject = null; // Fjern referencen til objektet
        }
    }

    // Tjek om spilleren rører jorden
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Spilleren er tilbage på jorden
        }
    }
}
