using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : Hp
{

  



    private void Start()
    {
       
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.layer == 8 && collision.gameObject.tag == "Boss")
        {
            health--;
            OnDamaged(collision.transform.position);
            if (health == 0) {
                die();
            }

        }
    }

    void OnDamaged(Vector3 vec)
    {

        gameObject.layer = 9;

       // transform.position = Vector3.MoveTowards(transform.position, -directionVec, 1.3f);

       // //animator.SetTrigger();
         Invoke("offDamaged", 5);
    }

    void offDamaged()
    {
        gameObject.layer = 8;

    }
    private void die() { 
    //gameover
        
    
    }


   


}
