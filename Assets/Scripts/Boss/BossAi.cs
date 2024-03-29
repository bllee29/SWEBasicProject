using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAi : MonoBehaviour
{   
    
    public float speed;
    public float checkRadius;
    public float attackRadius;

    protected float temp;

    public bool shouldRotate;

    public LayerMask whatIsPlayer;
    public Collision2D collision;
    public Transform target;
    public Rigidbody2D rb;
    public Animator anim;
    public Vector2 movement;
    public Vector3 dir;
    SpriteRenderer sr;

    public bool isInchaseRange;
    public bool isInAttackRange;



    // Start is called before the first frame update
    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        temp = speed;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        anim.SetBool("isMoving", isInchaseRange);

        isInchaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        dir = target.position - transform.position;
        // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if(shouldRotate)
        {
            anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);
        }
        if(dir.x < 0)
        {
            anim.SetBool("isLeft", true);
            anim.SetBool("isRight", false);
        }
        else
        {
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", true);
        }


    }
    public virtual void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }


    public void OnDamaged()
    {
        sr.color = new Color(1, 0.4f, 0.4f);
        // //animator.SetTrigger();
        Invoke("offDamaged", 0.2f);
    }

    public void offDamaged()
    {
        sr.color = new Color(1, 1, 1);
    }
}
