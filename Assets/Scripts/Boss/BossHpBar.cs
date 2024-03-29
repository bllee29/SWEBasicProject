using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public Slider hpBar;
    public float maxHp;
    public float curHp;
    public Transform Boss;
    // Start is called before the first frame update
    void Start()
    {

    }
    public float returnHp()
    {
        return curHp;
    }
    // Update is called once per frame
    void Update()
    {
        hpBar.value = curHp / maxHp;
    }
    void OnTriggerEnter2D(Collider2D isAttacked)
    {
        if (isAttacked.gameObject.tag == "Arrow")
        {
            Debug.Log("Arrow");
            curHp -= 50;
        }
    }
}
