using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animaitonBuff : MonoBehaviour
{

    Animator animator;
    private float buffTimeCheck;
    public float buffCooldown;





    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();


        buffTimeCheck = 0;
    }

    // Update is called once per frame
    void Update()
    {
        buffTimeCheck += Time.deltaTime;
      



        if (Input.GetKey(KeyCode.LeftShift)&&buffTimeCheck>=buffCooldown) {
            buffTimeCheck = 0;

            animator.SetBool("buffOn", true);
            Invoke("buffOff", 3f);

        }
 


    }






void buffOff() {

        animator.SetBool("buffOn", false);

    }


}
