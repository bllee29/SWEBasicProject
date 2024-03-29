using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Camera sceneCamera;

    public  SpriteRenderer src;


    public float speed;
    Animator animator;
    public Vector3 directionVec;
    


  


    public LayerMask layer;



    public new  Rigidbody2D rigidbody;
    private Vector2 vector;

    public float dodgeDistance;
    public float dodgeTime;
    private float dodgeTimeCheck;

    public float Skill3CoolDown;
    private float Skil3TimeCheck;
   

  

    public float Skill1CoolDown;
    private float Skil1TimeCheck;
    





    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        src = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Dir.X", vector.x);
        animator.SetFloat("Dir.Y", vector.y);


        
        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");

        rigidbody.velocity = vector.normalized * speed;

        if(vector.x>0)
            animator.SetBool("direction", true);
        if (vector.x < 0)
            animator.SetBool("direction", false);


        if (vector.x == 0 && vector.y == 0)
          {

            animator.SetBool("Walking", false);
            
          }
        if (vector.x != 0 || vector.y != 0)
        {

            animator.SetBool("Walking", true);
            
        }


        

    }


    void FixedUpdate()
    {
        dodgeTimeCheck += Time.deltaTime;
        Skil3TimeCheck += Time.deltaTime;
        Skil1TimeCheck += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) &&
            dodgeTimeCheck >= dodgeTime)
        {
            dodgeTimeCheck = 0;

            Dodge();
           

        }
   
        if (Input.GetKey(KeyCode.E) &&
          Skil3TimeCheck >= Skill3CoolDown)
        {
            Skil3TimeCheck = 0;
            
            Skill3();


        }

        if (Input.GetKey(KeyCode.LeftShift) &&
      Skil1TimeCheck >= Skill1CoolDown)
        {
            Skil1TimeCheck = 0;

            Skill1();


        }


        Direction();






    }

    public void Skill3()
    {
        speed *= 2;
        dodgeDistance *= 2;
        dodgeTime /= 2;
        src.color = new Color(0, 1, 0);


        Invoke("S3", 3f);
       

    }
    public void S3()
    {
  
        speed /= 2;
        dodgeDistance /= 2;
        dodgeTime *= 2;
        src.color = new Color(1, 1, 1);
    }


    public void Skill1()
    {
        speed = 0;
        src.color = new Color(1, 1, 0);
        gameObject.layer = 9;

        Invoke("S1", 2f);




    }

    public void S1()
    {
        gameObject.layer = 8;
        speed = 5;
        src.color = new Color(1, 1, 1);

    }





    public void Dodge()
    {
        //RaycastHit hit;
       // Debug.DrawRay(transform.position, directionVec * 0.9f, new Color(0, 1, 0));

        if (Physics2D.Raycast(transform.position, directionVec.normalized,  dodgeDistance,layer))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position-directionVec*0.5f, directionVec * (dodgeDistance*1.5f));

            transform.position =new Vector3(hit.point.x-(directionVec.x*0.3f),hit.point.y-(directionVec.y*0.3f),0);
        }
        else
        {
            transform.position += directionVec.normalized * dodgeDistance;
        }
    }






    public void Direction()
    {
        if (vector.x == -1 && vector.y == 1)//UpLeft
        {
            directionVec = new Vector3(-1.14f, 1.14f,0);

        }
        if (vector.x == -1 && vector.y == 0)//Left
        {
            directionVec = new Vector3(-2f, 0,0);

        }
        if (vector.x == -1 && vector.y == -1)//DownLeft
        {
            directionVec = new Vector3(-1.14f, -1.14f,0);

        }

        if (vector.x == 1 && vector.y == 1)//UpRight
        {
            directionVec = new Vector3(1.14f, 1.14f,0);

        }
        if (vector.x == 1 && vector.y == 0)//Right
        {
            directionVec = new Vector3(2f, 0,0);

        }
        if (vector.x == 1 && vector.y == -1)//DownRight
        {
            directionVec = new Vector3(1.14f, -1.14f,0);

        }
        if (vector.x == 0 && vector.y == 1)//Up
        {
            directionVec = new Vector3(0, 2.0f,0);

        }
        if (vector.x == 0 && vector.y == -1)//Down
        {
            directionVec = new Vector3(0, -2.0f,0);

        }



    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position,directionVec+transform.position);
    }


    

   


    private void OnTriggerEnter2D(Collider2D other){
         if ( other.CompareTag ("NextRoom1") )
        {
            Debug.Log ("Get Next Room1");
            Stage.Instance.NextStage(1);
        }
        if( other.CompareTag ("NextRoom2"))
        {
            Debug.Log ("Get Next Room2");
            Stage.Instance.NextStage(2);
        }
    }
}


