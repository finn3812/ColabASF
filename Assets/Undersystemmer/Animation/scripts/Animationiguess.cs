using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationiguess : MonoBehaviour
{
    Animator animator;
    public enum States {Drunk_run, HAPPY_IDLE, praying, Rollkick, Walkin, backwalk, biting_necks}
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Animationskift(States Animationstype)
    {
        switch(Animationstype)
        {
            case States.Drunk_run:
                animator.SetInteger("State", 2);
                break;


            case States.HAPPY_IDLE:
                animator.SetInteger("State", 0);
                break;

            case States.praying:
                animator.SetInteger("State", 6);
                break;

            case States.Rollkick:
                animator.SetInteger("State", 4);
                break;

            case States.Walkin:
                animator.SetInteger("State", 1);
                break;

            case States.backwalk:
                animator.SetInteger("State", 3);
                break;

            case States.biting_necks:
                animator.SetInteger("State", 5);
                break;
        }
    }





    // Update is called once per frame
    void Update()
    {

        //walk
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetInteger("State", 0);
        }

        //walk back
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetInteger("State", 3);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetInteger("State", 0);
        }


        //run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetInteger("State", 0);
        }

        

        //pray
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetInteger("State", 6);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            animator.SetInteger("State", 0);
        }


        //biting
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetInteger("State", 5);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            animator.SetInteger("State", 0);

        }

        //Kick
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetInteger("State", 4);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            animator.SetInteger("State", 0);

        }


    }
}

