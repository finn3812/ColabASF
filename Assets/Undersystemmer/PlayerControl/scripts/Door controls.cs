using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class Doorcontrols : MonoBehaviour
{
    public enum state { idle, DoorOpen, DoorClose }
    public state states = state.idle;

    public CharacterController controller;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (states)
        {
            case state.idle:
                animator.SetInteger("state", 0);
                if (Input.GetKeyDown(KeyCode.O))
                {
                    states = state.DoorOpen;
                }
                break;
            case state.DoorOpen:
                animator.SetInteger("state", 1);
                if (Input.GetKeyDown(KeyCode.C))
                {
                    states = state.DoorClose;
                }

                break;
            case state.DoorClose:
                animator.SetInteger("state", 2);
                if (Input.GetKeyDown(KeyCode.O))
                {
                    states = state.DoorOpen;
                }
                break;
        }
    }
}
