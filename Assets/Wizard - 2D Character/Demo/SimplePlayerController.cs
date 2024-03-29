using UnityEngine;

namespace ClearSky
{
    public class SimplePlayerController : Hp
    {

        public Camera sceneCamera;

        public float dodgeDistance;
        public float dodgeTime;
        private float dodgeTimeCheck;

        public float Skill3CoolDown;
        private float Skil3TimeCheck;

        public Vector3 directionVec;


        public float Skill1CoolDown;
        private float Skil1TimeCheck;

        public LayerMask layer;



        private Vector2 vector;


        public float movePower = 10f;
        public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5

        private Rigidbody2D rb;
        private Animator anim;
        Vector3 movement;
        private int direction = 1;

        private bool alive = true;
        private bool BOOM;

        private float activeMoveSpeed;
        public float dashSpeed;

        public float dashLength = 0.5f, dashCooldown = 1f;

        private float dashCounter;
        private float dashCoolCounter;

        public float Skill0CoolDown;
        private float Skil0TimeCheck;
        public static int slimecount = 0;


        private bool delaymotion;
        private float QTimeCheck;
        private float QCoolMotion;

        private float ETimecheck;
        private float ECoolTime;


        AudioSource audioSource;

        public AudioClip space;
        public AudioClip q;
        public AudioClip e;
        public AudioClip shift;
        public AudioClip baasse;



        private void Awake()
        {
            this.audioSource = GetComponent<AudioSource>();
        }
        // Start is called before the first frame update
        void Start()
        {
            BOOM = true;
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            activeMoveSpeed = movePower;

            delaymotion = false;
            QTimeCheck = 5;
            QCoolMotion = 0;
            
            ETimecheck = 15;
            ECoolTime = 0;

        }

        private void Update()
        {
            sceneCamera.transform.position = new Vector3(transform.position.x, transform.position.y, sceneCamera.transform.position.z);
            QCoolMotion += Time.deltaTime;
            ECoolTime += Time.deltaTime;

            if (health > numOfHearts)
            {
                health = numOfHearts;

            }
            for (int i = 0; i < hearts.Length; i++)
            {

                if (i < health)
                {
                    hearts[i].sprite = fullHeart;

                }
                else
                {
                    hearts[i].sprite = emptyHeart;

                }
                if (i < numOfHearts)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;

                }
            }

            vector.x = Input.GetAxisRaw("Horizontal");
            vector.y = Input.GetAxisRaw("Vertical");
            rb.velocity = vector.normalized * activeMoveSpeed;

            Skil0TimeCheck += Time.deltaTime;


            if (Stage.Instance.getcount() == 15)
                Stage.Instance.boss2endgate();
            // Debug.Log(Stage.Instance.getcount());




            Restart();
            if (alive)
            {


                Attack();
                Run();
                Direction();
            }
        }

     


        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);


            if (Input.GetAxisRaw("Horizontal") < 0 && delaymotion == false)
            {
                direction = -1;


                transform.localScale = new Vector3(direction, 1, 1);
                anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Horizontal") > 0 && delaymotion == false)
            {
                direction = 1;


                transform.localScale = new Vector3(direction, 1, 1);
                anim.SetBool("isRun", true);

            }

            if (Input.GetAxisRaw("Vertical") > 0 && delaymotion == false)
            {



                transform.localScale = new Vector3(direction, 1, 1);
                anim.SetBool("isRun", true);


            }

            if (Input.GetAxisRaw("Vertical") < 0 && delaymotion == false)
            {



                transform.localScale = new Vector3(direction, 1, 1);
                anim.SetBool("isRun", true);


            }

            if (Input.GetKey(KeyCode.Q) && QCoolMotion >= QTimeCheck)
            {
                delaymotion = true;
                QCoolMotion = 0;
                Invoke("Scale", 1f);
                PlaySound("QQ");

            }


            if (Input.GetKey(KeyCode.E) && ECoolTime >= ETimecheck)
            {

             
                PlaySound("EE");
                ECoolTime = 0;

           }




            if (Input.GetKey(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                    PlaySound("SPSP");

                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMoveSpeed = movePower;
                    dashCoolCounter = dashCooldown;

                }

            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;

            }






            transform.position += moveVelocity * movePower * Time.deltaTime;
        }

        void Scale()
        {
            delaymotion = false;

        }


        void Attack()
        {
            if (Input.GetMouseButtonDown(0) && Skil0TimeCheck >= Skill0CoolDown)
            {
                PlaySound("attack");
                delaymotion = true;
                Skil0TimeCheck = 0;
                anim.SetTrigger("attack");
                Invoke("Scale", 1f);
                
            }
        }
        void Hurt()
        {

            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);

        }
        void Die()
        {
            activeMoveSpeed = 0;
            anim.SetTrigger("die");
            alive = false;

        }
        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                anim.SetTrigger("idle");
                alive = true;
            }
        }

        void FixedUpdate()
        {
            dodgeTimeCheck += Time.deltaTime;
            Skil1TimeCheck += Time.deltaTime;




            if (Input.GetKey(KeyCode.LeftShift) &&
          Skil1TimeCheck >= Skill1CoolDown)
            {
                Skil1TimeCheck = 0;

                Skill1();
                PlaySound("Shift");

            }

        }

        void PlaySound(string action) {
            switch (action) {
                case "QQ":
                    audioSource.clip = q;
                    break;
                case "EE":
                    audioSource.clip = e;
                    break;
                case "SPSP":
                    audioSource.clip = space;
                    break;
                case "Shift":
                    audioSource.clip = shift;
                    break;
                case "attack":
                    audioSource.clip = baasse;
                    break;

            }
            audioSource.Play();
        
        }



        public void Skill1()
        {

            gameObject.layer = 9;

            Invoke("S1", 3f);




        }

        public void S1()
        {
            gameObject.layer = 8;


        }





        public void Dodge()
        {
            //RaycastHit hit;
            // Debug.DrawRay(transform.position, directionVec * 0.9f, new Color(0, 1, 0));

            if (Physics2D.Raycast(transform.position, directionVec.normalized, dodgeDistance, layer))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position - directionVec * 0.5f, directionVec * (dodgeDistance * 1.5f));

                transform.position = new Vector3(hit.point.x - (directionVec.x * 0.3f), hit.point.y - (directionVec.y * 0.3f), 0);
            }
            else
            {
                transform.position += directionVec.normalized * dodgeDistance;
            }
        }


        public void Direction()
        {
            if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 1)//UpLeft
            {
                directionVec = new Vector3(-1.14f, 1.14f, 0);

            }
            if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 0)//Left
            {
                directionVec = new Vector3(-2f, 0, 0);

            }
            if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == -1)//DownLeft
            {
                directionVec = new Vector3(-1.14f, -1.14f, 0);

            }

            if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 1)//UpRight
            {
                directionVec = new Vector3(1.14f, 1.14f, 0);

            }
            if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 0)//Right
            {
                directionVec = new Vector3(2f, 0, 0);

            }
            if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == -1)//DownRight
            {
                directionVec = new Vector3(1.14f, -1.14f, 0);

            }
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 1)//Up
            {
                directionVec = new Vector3(0, 2.0f, 0);

            }
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == -1)//Down
            {
                directionVec = new Vector3(0, -2.0f, 0);

            }



        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("NextRoom1"))
            {
                Debug.Log("Get Next Room1");
                Stage.Instance.NextStage(1);
            }
            if (other.CompareTag("NextRoom2"))
            {
                Debug.Log("Get Next Room2");
                Stage.Instance.NextStage(2);
            }
            if (other.CompareTag("NextRoom4"))
            {
                Debug.Log("Get Next Room3");
                Stage.Instance.NextStage(4);
            }            
            if (other.CompareTag("End"))
            {
                Debug.Log("Get Start Room");
                Stage.Instance.NextStage(3);
            }
            if (gameObject.layer == 8 && (other.gameObject.tag == "BossAttack" || other.gameObject.tag == "Bullet"))
            {
                health--;
                Hurt();
                OnDamaged(other.transform.position);
                if (health == 0)
                {
                    Die();
                }
            }
            if (other.gameObject.tag == "BOOM!")
            {
                if (!BOOM)
                {
                    Debug.Log("살았습니다!");
                }
                else
                {
                    Debug.Log("죽었습니다!");
                    Die();
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "SafeZone")
            {
                BOOM = false;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "SafeZone")
            {
                BOOM = true;
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (gameObject.layer == 8 && collision.gameObject.tag == "Boss")
            {
                health--;
                Hurt();

                OnDamaged(collision.transform.position);
                if (health == 0)
                {
                    Die();
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


        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, directionVec + transform.position);
        }



    }



}