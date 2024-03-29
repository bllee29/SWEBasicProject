using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphinxAi : BossAi
{
    GameObject leftHand;
    GameObject rightHand;
    public GameObject slamSound;
    public GameObject rangeObject;
    public GameObject rockFall;
    BoxCollider2D rangeCollider;
    Rigidbody2D leftRb;
    Rigidbody2D rightRb;
    Collider2D leftCollider;
    Collider2D rightCollider;
    bool isLeft;
    bool isRight;
    bool isDone;
    float slamCoolTime;
    public float slamCurTime;
    public float maxHp;
    public float curHp;
    float rockCurTime;
    float rockCoolTime;
    float attackCoolTime;
    float attackCurTime;
    Vector3 leftStart;
    Vector3 rightStart;
    Vector3 fixedPosition;
    int leftcounter;
    int rightcounter;


    // Start is called before the first frame update
    public override void Start()
    {
        leftHand = transform.Find("leftHand").gameObject;
        rightHand = transform.Find("rightHand").gameObject;
        base.Start();
        leftRb = leftHand.GetComponent<Rigidbody2D>();
        rightRb = rightHand.GetComponent<Rigidbody2D>();
        leftCollider = leftHand.GetComponent<CapsuleCollider2D>();
        rightCollider = rightHand.GetComponent<CapsuleCollider2D>();
        rangeCollider = rangeObject.GetComponent<BoxCollider2D>();
        speed = 0;
        slamCoolTime = 5;
        slamCurTime = 0;
        rockCurTime = 5;
        rockCoolTime = 10;
        leftStart = leftHand.transform.position;
        rightStart = rightHand.transform.position;
        attackCoolTime = 2;
        attackCurTime = 0;
        fixedPosition = target.position;
        isDone = false;
    }

    void OnTriggerEnter2D(Collider2D isAttacked)
    {
        if (isAttacked.gameObject.tag == "Arrow")
        {
            curHp -= 50;
            OnDamaged();
        }
        if (isAttacked.gameObject.tag == "Ready")
        {
            fixedPosition = target.position;
        }
    }

    IEnumerator Die()
    {
        if (curHp < 0)
        {
            anim.enabled = true;
            anim.SetTrigger("die");
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
        }
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

    void Fall()
    {
        if (rockCurTime > 0)
        {
            anim.enabled = false;
            rockCurTime -= Time.deltaTime;
        }
        else if(anim.enabled)
        {
            anim.SetTrigger("slam");
            GameObject rock = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock2 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock3 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock4 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock5 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock6 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock7 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock8 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock9 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock10 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock11 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock12 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock13 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock14 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock15 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock16 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock17 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            GameObject rock18 = Instantiate(rockFall, Return_RandomPosition(), transform.rotation);
            Destroy(rock, 3f);
            Destroy(rock2, 3f);
            Destroy(rock3, 3f);
            Destroy(rock4, 3f);
            Destroy(rock5, 3f);
            Destroy(rock6, 3f);
            Destroy(rock7, 3f);
            Destroy(rock8, 3f);
            Destroy(rock9, 3f);
            Destroy(rock10, 3f);
            Destroy(rock11, 3f);
            Destroy(rock12, 3f);
            Destroy(rock13, 3f);
            Destroy(rock14, 3f);
            Destroy(rock15, 3f);
            Destroy(rock16, 3f);
            Destroy(rock17, 3f);
            Destroy(rock18, 3f);
            rockCurTime = rockCoolTime;
            attackCurTime = attackCoolTime;
        }

    }
    void Attack()
    {
        if (isInchaseRange && (attackCurTime < 0))
        {
            Fall();
            StartCoroutine(Slam());
        }
        else
        {
            attackCurTime -= Time.deltaTime;
        }
    }
    // Update is called once per frame
    public override void Update()
    {
        isInchaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        dir = target.position - transform.position;
        if (dir.x < 0)
        {
            isLeft = true;
            isRight = false;
            Debug.Log("isLeft");
        }
        else
        {
            isRight = true;
            isLeft = false;
            Debug.Log("isRight");
        }
        if(!isDone)
        {
            fixedPosition = target.position;
            isDone = true;
        }
        Attack();
        curHp = GetComponentInChildren<BossHpBar>().returnHp();
        StartCoroutine(Die());
    }

    public override void MoveCharacter(Vector2 dir)
    {
        
    }

    IEnumerator Slam()
    {
        if (slamCurTime <= 0)
        {
            anim.enabled = false;
            if(isLeft&&!isRight)
            {
                yield return StartCoroutine(leftSlam());
                slamCurTime = slamCoolTime;
                attackCurTime = attackCoolTime;
            }
            else if(!isLeft&&isRight)
            {
                yield return StartCoroutine(rightSlam());
                slamCurTime = slamCoolTime;
                attackCurTime = attackCoolTime;
            }
        }
        else
        {
            leftcounter = 0;
            rightcounter = 0;
            anim.enabled = true;
            slamCurTime -= Time.deltaTime;
        }
    }

    IEnumerator leftSlam()
    {
        leftRb.position = Vector3.MoveTowards(leftHand.transform.position, leftStart + new Vector3(0, 10, 0), 0.08f);
        yield return new WaitForSeconds(2f);
        if (leftcounter == 0 && isDone == true)
        {
            isDone = false;
        }
        else if(leftcounter > 0)
        {
            isDone = true;
        }
        leftcounter++;
        leftRb.position = Vector3.MoveTowards(leftHand.transform.position, fixedPosition, 0.8f);
        slamSound.SetActive(true);
        Invoke("offSound", 0.6f);
        yield return new WaitForSeconds(1.5f);
        leftRb.position = Vector3.MoveTowards(leftHand.transform.position, leftStart, 0.5f);
        yield return new WaitForSeconds(1.5f);
    }

    void offSound()
    {
        slamSound.SetActive(false);
    }
    IEnumerator rightSlam()
    {
        rightRb.position = Vector3.MoveTowards(rightHand.transform.position, rightStart + new Vector3(0, 10, 0), 0.08f);
        yield return new WaitForSeconds(2f);
        if (rightcounter == 0 && isDone == true)
        {
            isDone = false;
        }
        else if (rightcounter > 0)
        {
            isDone = true;
        }
        rightcounter++;
        rightRb.position = Vector3.MoveTowards(rightHand.transform.position, fixedPosition, 0.8f);
        yield return new WaitForSeconds(1.5f);
        slamSound.SetActive(true);
        Invoke("offSound", 0.6f);
        rightRb.position = Vector3.MoveTowards(rightHand.transform.position, rightStart, 0.5f);
        yield return new WaitForSeconds(1.5f);
    }

}
