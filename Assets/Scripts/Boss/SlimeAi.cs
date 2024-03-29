using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeAi : BossAi
{
    string[] eCharacterState = { "leftAttack", "rightAttack","readyAttackLeft", "readyAttackRight", "Slime_special_left", "Slime_special_right" };
    public int maxHp;
    public float curHp;
    public float curTime;
    public float coolTime;
    public float scurTime;
    public float scoolTime;    
    public SlimeAi selfSlime;    
    public GameObject nextSlime1;
    public GameObject nextSlime2;    
    // private BossHpBar bossHpBar;
    public GameObject rangeObject;    
    public GameObject[] fire = new GameObject[3];
    BoxCollider2D firespawn;
    BoxCollider2D spawnCollider;
    BoxCollider2D tempc;
    public int count = 1;
    GameObject boss2;
    Rigidbody2D rb1;
    Vector2 tempv;    
    float attackSpeed = 10f;
              
    private void Awake()
    {    
        spawnCollider = rangeObject.GetComponent<BoxCollider2D>();
        firespawn = GameObject.Find("Firespawn").GetComponent<BoxCollider2D>();
        // Instantiate(firespawn).transform.parent = GameObject.Find("Boss(2)").transform;;
        Instantiate(firespawn, new Vector3(firespawn.transform.position.x, firespawn.transform.position.y, transform.position.z), transform.rotation).transform.parent = GameObject.Find("Boss(2)").transform;
        // tempc        
    }    
    // Start is called before the first frame update
    void start()
    {
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        normalAttack();
        Dir_whileAttack();
        attackSpecial();
        // Debug.Log(speed);
        // bossHpBar = GameObject.Find("Canvas2").GetComponent<BossHpBar>(); // 
        // curHp = bossHpBar.curHp;
        curHp = GetComponentInChildren<BossHpBar>().returnHp(); // 자식 object들 중에서 BossHpBar를 가지고 있는 요소의 returnHP를 실행 위의 bossHPBAr클래스를 이용해서 정보를 가져오면
                                                                // 서로 다른 slime에 들어가있는 여러 canvas2들 중에 제일 위에있는 canvas2를 기준으로 모든 슬라임이 동작.
                                                                // 이렇게 하면 서로다른 slime객체들이 한가지의 canvas를 이용하지않고 각자의 자식canvas를 이용할것이라고 예상
        disrupt();        
    }

    void FixedUpdate()
    {   
        move_whileAttack();  
        if(isInchaseRange)
        {
            MoveCharacter(movement);
        }
        if(isInAttackRange && speed == attackSpeed)
        {
            MoveCharacter(tempv);
        }

    }
    void OnTriggerEnter2D(Collider2D isAttacked)
    {
        if (isAttacked.gameObject.tag == "Arrow")
        {
            // Debug.Log("Arrow");
            OnDamaged();
        }
    }    

    void normalAttack()
    {
        if (curTime <= 0)
        {
            if (isInAttackRange && (dir.x < 0))
            {
                anim.SetTrigger("readyAttackLeft");
                anim.SetTrigger("attackLeft");
                curTime = coolTime;
            }
            else if (isInAttackRange && (dir.x >= 0))
            {
                anim.SetTrigger("readyAttackRight");
                anim.SetTrigger("attackRight");                
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    void attackSpecial()
    {
        if (scurTime <= 0)
        {
            if ((dir.x < 0))
            {
                anim.SetTrigger("AttackSLeft");
                Fire();
                scurTime = scoolTime;
            }
            else if ((dir.x >= 0))
            {
                anim.SetTrigger("AttackSRight");
                Fire();               
                scurTime = scoolTime;
            }
        }
        else
        {
            scurTime -= Time.deltaTime;
        } 

    }

    void disrupt()
    {

        if (curHp == 1f & count == 1)
        {
            count++;
            GameObject slime1 = Instantiate(nextSlime1, Distrubt_Position(1f), transform.rotation);
            GameObject slime2 = Instantiate(nextSlime2, Distrubt_Position(-1f), transform.rotation);
            slime1.transform.parent = GameObject.Find("Boss(2)").transform;
            slime2.transform.parent = GameObject.Find("Boss(2)").transform;
            Destroy(gameObject);
            Destroy(GameObject.Find("Firespawn(Clone)")); // Object의 이름으로 찾음. 가장 처음에 나오는 Object를 반환.)
            Debug.Log(count);
            Stage.Instance.setcount();
        }

    }
    void Fire()
    {
        if (scurTime < scoolTime)
        {
            GameObject fire1 = Instantiate(fire[0], fire_Position(), transform.rotation);
            GameObject fire2 = Instantiate(fire[1], fire_Position(), transform.rotation);
            GameObject fire3 = Instantiate(fire[2], fire_Position(), transform.rotation);
            fire1.transform.parent = GameObject.Find("Firespawn(Clone)").transform;
            fire2.transform.parent = GameObject.Find("Firespawn(Clone)").transform;
            fire3.transform.parent = GameObject.Find("Firespawn(Clone)").transform;                        
        }
    }    
    // void disruption(){

    // }

    void move_whileAttack()
    {
        AnimatorStateInfo a = anim.GetCurrentAnimatorStateInfo(0);
        if (a.IsName(eCharacterState[0]) || a.IsName(eCharacterState[1]))
        {
            speed = attackSpeed;

        }
        else if (a.IsName(eCharacterState[4]) || a.IsName(eCharacterState[5]))
        {
            speed = 0;
        }        
        else
        {
            speed = temp;
        }

    }
    // void do_not_move_whileAttack()
    // {
    //     AnimatorStateInfo a = anim.GetCurrentAnimatorStateInfo(0);

    //     else
    //     {
    //         speed = temp;
    //     }
    // }
    
    void Dir_whileAttack()
    {
        AnimatorStateInfo a = anim.GetCurrentAnimatorStateInfo(0);
        if (a.IsName(eCharacterState[2]) || a.IsName(eCharacterState[3]))
        // tempv = target.position - transform.position;
        tempv = movement;
    }


    Vector3 Distrubt_Position(float sign)
    {
        Vector3 originPosition = rangeObject.transform.position;

        float range_X = spawnCollider.bounds.size.x;
        float range_Y = spawnCollider.bounds.size.y;

        range_X /= 5*sign;
        range_Y /= 2f;
        Vector3 RandomPostion = new Vector3(range_X, range_Y, 0f);
        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }    
    Vector3 fire_Position()
    {
        Vector3 originPosition = firespawn.transform.position;

        float range_X = firespawn.bounds.size.x;
        float range_Y = firespawn.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector3 RandomPostion = new Vector3(range_X, range_Y, 0f);
        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

}


