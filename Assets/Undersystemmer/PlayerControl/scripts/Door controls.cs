using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class Doorcontrols : MonoBehaviour
{
    public enum State { Idle, DoorOpen, DoorClose }
    public State states = State.Idle;

    public CharacterController controller; // Not strictly needed for proximity checks
    private Animator animator;
    private bool playerInRange = false; // To track if the player is within range

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!playerInRange)
        {
            return; // Ignore input if the player is not in range
        }

        switch (states)
        {
            case State.Idle:
                animator.SetInteger("state", 0);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    audiomanager.Instance.PlaySound("door creaking", 0.5f);
                    states = State.DoorOpen;
                }
                break;

            case State.DoorOpen:
                animator.SetInteger("state", 1);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    audiomanager.Instance.PlaySound("door creaking", 0.5f);
                    states = State.DoorClose;
                }
                break;

            case State.DoorClose:
                animator.SetInteger("state", 2);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    audiomanager.Instance.PlaySound("door creaking", 0.5f);
                    states = State.DoorOpen;
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the tag "Player"
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
