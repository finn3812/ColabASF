using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    Quaternion targetRotation;

    CameraController cameraController;

    // public float rotationSpeed = 100f; // Rotationshastighed
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

    private void Awake()
    {
         cameraController = Camera.main.GetComponent<CameraController>();
    }
    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);

        var moveInput = (new Vector3(h, 0, v)).normalized;

        var moveDir = cameraController.transform.rotation * moveInput;
        
       if (moveAmount > 0)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


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
