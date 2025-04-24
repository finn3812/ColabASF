using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationiguess : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //walk
        if(Input.GetKeyDown(KeyCode.W))
        {
            animator.SetInteger("State", 1);
        }
        if(Input.GetKeyUp(KeyCode.W))
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


    }
}
