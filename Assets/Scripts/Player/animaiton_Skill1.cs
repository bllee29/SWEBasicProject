using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animaiton_Skill1 : MonoBehaviour
{
    Animator animator;


    private float DashTimeCheck;
    public float DashCooldown;



    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();

        DashTimeCheck = 0;

    }

    // Update is called once per frame
    void Update()
    {

        DashTimeCheck += Time.deltaTime;





        if (Input.GetKey(KeyCode.Space) && DashTimeCheck >= DashCooldown)
        {
            DashTimeCheck = 0;

            animator.SetBool("DashOn", true);
            Invoke("DashOff", 0.5f);

        }



    }







    void DashOff()
    {

        animator.SetBool("DashOn", false);
    }

}
