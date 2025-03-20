using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 5f; // Bev�gelseshastighed
    public float jumpForce = 5f; // Kraft til at hoppe
    private Rigidbody rb;
    private bool isGrounded = true; // Tjekker om spilleren er p� jorden

    private GameObject carriedObject = null; // Objekt spilleren b�rer
    public Transform carryPosition; // Position hvor objektet holdes

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

        // Pickup og placement
        if (Input.GetKeyDown(KeyCode.E)) // Saml op
        {
            TryPickupObject();
        }
        else if (Input.GetKeyDown(KeyCode.R)) // Plac�r
        {
            PlaceObject();
        }
    }

    private void TryPickupObject()
    {
        if (carriedObject == null) // Kun fors�g at samle, hvis spilleren ikke allerede b�rer noget
        {
            Ray ray = new Ray(transform.position, transform.forward); // Str�le foran spilleren
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2f)) // Tjek for objekt inden for en bestemt afstand
            {
                if (hit.collider.CompareTag("Pickup")) // Kun objekter med tagget "Pickup"
                {
                    carriedObject = hit.collider.gameObject;
                    carriedObject.GetComponent<Rigidbody>().isKinematic = true; // Deaktiver fysik
                    carriedObject.transform.position = carryPosition.position; // Flyt til b�reposition
                    carriedObject.transform.parent = carryPosition; // G�r objektet til barn af spilleren
                }
            }
        }
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

    // Tjek om spilleren r�rer jorden
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Spilleren er tilbage p� jorden
        }
    }
}
