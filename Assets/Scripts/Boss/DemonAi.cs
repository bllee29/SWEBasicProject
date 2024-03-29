using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAi : BossAi
{
    public float maxHp;
    public float curHp;
    public float bulletcurDelay;
    public float bulletcoolDelay;
    public float BeamCoolTime;
    public float BeamCurTime;
    public float curTime;
    public float coolTime;
    public float scurTime;
    public float scoolTime;
    public int setBullet;
    public GameObject Bullet;
    public GameObject rangeObject;
    public GameObject headColumn;
    public GameObject leftColumn;
    public GameObject rightColumn;
    public GameObject bottomColumn;
    public new SpriteRenderer renderer;
    public GameObject endgate;
    CircleCollider2D rangeCollider;
    int AttackOnce;

    bool enableBullet;
    bool enableBoom;

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<CircleCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        enableBullet = false;
        enableBoom = false;
        base.Start();
        bulletcurDelay = 0f;
        curTime = 0f;
        scurTime = 0f;
        BeamCurTime = 5f;
        AttackOnce = 0;
    }
    // Update is called once per frame
    public override void Update()
    {   
        StartCoroutine(BOOM());
        base.Update();
        if(!enableBoom)
        {
            Reload();
            attackBreath();
            attackSpecial();
            attackBeam();
            StartCoroutine(FireCool());
        }
        do_not_move_whileAttack();
        curHp = GetComponentInChildren<BossHpBar>().returnHp();
        StartCoroutine(Die());
    }
    IEnumerator FireCool()
    {
        if (enableBullet)
        {
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.2f);
                Fire();
            }
            enableBullet = false;
        }
    }
    void Fire()
    {
        if (bulletcurDelay < bulletcoolDelay)
            return;
            GameObject bullet = Instantiate(Bullet, Return_RandomPosition(), transform.rotation);
            GameObject bullet2 = Instantiate(Bullet, Return_RandomPosition(), transform.rotation);
            GameObject bullet3 = Instantiate(Bullet, Return_RandomPosition(), transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
            Rigidbody2D rigid3 = bullet3.GetComponent<Rigidbody2D>();
            Vector3 dirVec = target.position - transform.position;
            rigid.AddForce(dirVec.normalized * 11, ForceMode2D.Impulse);
            rigid2.AddForce(dirVec.normalized * 9, ForceMode2D.Impulse);
            rigid3.AddForce(dirVec.normalized * 7, ForceMode2D.Impulse);
        bulletcurDelay = 0;
    }
    void Reload()
    {
        bulletcurDelay += Time.deltaTime;
    }
    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;

        float range_X = rangeCollider.bounds.size.x;
        float range_Y = rangeCollider.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector3 RandomPostion = new Vector3(range_X, range_Y, 0f);
        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }


    void OnTriggerEnter2D(Collider2D isAttacked)
    {
        if (isAttacked.gameObject.tag == "Player")
        {
            Debug.Log("플레이어가 공격에 맞았습니다!");
        }
        if (isAttacked.gameObject.tag == "Arrow")
        {
            if (curHp < 501 && AttackOnce == 0)
            {
                enableBoom = true;
            }
            else
            {
                enableBoom = false;
            }
            // Debug.Log("Arrow");
            curHp -= 50;
            OnDamaged();
        }
    }

    IEnumerator Die()
    {
        if(curHp < 0)
        {
            anim.SetTrigger("die");
            speed = 0;
            enableBullet = false;
            curTime = 10f;
            scurTime = 10f;
            BeamCurTime = 10f;
            yield return new WaitForSeconds(3f);
            endgate.SetActive(true);        
            gameObject.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        if (isInchaseRange && !isInAttackRange)
        {
            MoveCharacter(movement);
        }
        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void attackBreath()
    {
        if (curTime <= 0)
        {
            if (isInAttackRange && (dir.x < 0) && ((dir.y >= -0.8) && (dir.y <= 0.5)))
            {
                anim.SetTrigger("attackLeftBreath");
                curTime = coolTime;
            }
            else if (isInAttackRange && (dir.x >= 0) && ((dir.y >= -0.8) && (dir.y <= 0.5)))
            {
                anim.SetTrigger("attackRightBreath");
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }
    void attackBeam()
    {
        if (BeamCurTime <= 0)
        {
            if (!isInAttackRange && (isInchaseRange))
            {
                if (dir.x <= 0)
                {
                    anim.SetTrigger("attackLeftBeam");
                }
                else if(dir.x > 0)
                {
                    anim.SetTrigger("attackRightBeam");
                }
                BeamCurTime = BeamCoolTime;
            }
        }
        else
        {
            BeamCurTime -= Time.deltaTime;
        }
    }
    void attackSpecial()
    {
        if (scurTime <= 0)
        {
            if (isInAttackRange && (dir.y > 0.5))
            {
                anim.SetTrigger("attackHead");
                scurTime = scoolTime;
            }
            else if (isInAttackRange && (dir.y < -0.8))
            {
                anim.SetTrigger("attackBottom");
                scurTime = scoolTime;
            }
        }
        else
        {
            scurTime -= Time.deltaTime;
        }
    }
    void do_not_move_whileAttack()
    {
        string[] eCharacterState = { "Boss_left_breath", "Boss_right_breath", "Boss_Head_Attack", "Boss_Bottom_Attack", "Boss_Left_Beam", "Boss_Right_Beam", "BOOM!"};
        AnimatorStateInfo a = anim.GetCurrentAnimatorStateInfo(0);
        if (a.IsName(eCharacterState[0]) || a.IsName(eCharacterState[1]) || a.IsName(eCharacterState[6])|| a.IsName(eCharacterState[2]) || a.IsName(eCharacterState[3]))
        {
            speed = 0;
            enableBullet = false;
        }
        else if (a.IsName(eCharacterState[4]) || a.IsName(eCharacterState[5]))
        {
            speed = 0;
            enableBullet = true;
        }
        else
        {
            speed = temp;
        }
    }
    IEnumerator BOOM()
    {
        string boom = "BOOM!";
        if (enableBoom)
        {
            AttackOnce += 1;
            anim.SetBool("BOOM!" , true);
            yield return new WaitForSeconds(3f);
            anim.SetBool("BOOM!", false);
            GameObject Head = Instantiate(headColumn, new Vector3(-53, 52, 0), transform.rotation);
            GameObject Bottom = Instantiate(bottomColumn, new Vector3(-53, 38, 0), transform.rotation);
            GameObject Left = Instantiate(leftColumn, new Vector3(-63, 45, 0), transform.rotation);
            GameObject Right = Instantiate(rightColumn, new Vector3(-43, 45, 0), transform.rotation);
            enableBoom = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(boom))
        {
            transform.position = new Vector3(-53, 45, 0);
            speed = 0;
        }

        enableBoom = false;
    }
  
 }

