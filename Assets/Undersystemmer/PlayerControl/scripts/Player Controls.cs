using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 5f; // Bev�gelseshastighed
    public float rotationSpeed = 100f; // Rotationshastighed
    public float jumpForce = 5f; // Kraft til at hoppe
    private Rigidbody rb;
    private bool isGrounded = true; // Tjekker om spilleren er p� jorden

    private GameObject carriedObject = null; // Objekt spilleren b�rer
    public Transform carryPosition; // Position hvor objektet holdes

    private GameObject nearbyObject = null; // Objekt, som spilleren kan samle op

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Henter Rigidbody-komponenten
    }

    void FixedUpdate()
    {
        // Input til bev�gelse og rotation
        float moveVertical = Input.GetAxis("Vertical"); // W/S for fremad/bagud
        float rotationHorizontal = Input.GetAxis("Horizontal"); // A/D for rotation

        // Bev�gelse fremad eller bagud
        Vector3 movement = transform.forward * moveVertical;
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);

        // Rotation til venstre eller h�jre
        float rotation = rotationHorizontal * rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0);
    }

    void Update()
    {
        // Tjek for hop-input og om spilleren er p� jorden
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
        else if (Input.GetKeyDown(KeyCode.R)) // Plac�r
        {
            PlaceObject();
        }
    }

    private void PickupObject()
    {
        carriedObject = nearbyObject; // S�t det n�rmeste objekt som det b�rne objekt
        carriedObject.GetComponent<Rigidbody>().isKinematic = true; // Deaktiver fysik
        carriedObject.transform.position = carryPosition.position; // Flyt til b�reposition
        carriedObject.transform.parent = carryPosition; // G�r objektet til barn af spilleren
    }

    private void PlaceObject()
    {
        if (carriedObject != null) // Kun fors�g at placere, hvis spilleren b�rer noget
        {
            carriedObject.GetComponent<Rigidbody>().isKinematic = false; // Aktiv�r fysik igen
            carriedObject.transform.parent = null; // Fjern objektet fra spilleren
            carriedObject = null; // Nulstil carriedObject
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup")) // Hvis objektet er markeret som et Pickup-objekt
        {
            nearbyObject = other.gameObject; // Registrer det n�rmeste objekt
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup")) // Hvis spilleren forlader omr�det omkring Pickup-objektet
        {
            nearbyObject = null; // Fjern referencen til objektet
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
