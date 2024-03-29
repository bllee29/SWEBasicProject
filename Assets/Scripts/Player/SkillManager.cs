using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject skilll1;
    public GameObject skilll2;


    public float Skill1CoolDown;
    private float Skil1TimeCheck;

    public float Skill0CoolDown;
    private float Skil0TimeCheck;

    // Start is called before the first frame update
    void Start()
    {
        skilll1.SetActive(false);
        skilll2.SetActive(false);

        Skil1TimeCheck = 0;
        Skil0TimeCheck = 0;


    }

    // Update is called once per frame
    void Update()
    {
        Skil0TimeCheck += Time.deltaTime;
        Skil1TimeCheck += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && Skil0TimeCheck >= Skill0CoolDown)
        {
            Skil0TimeCheck = 0;


            skilll1.SetActive(true);
            Invoke("Offskill1",1f);
        
        
        }

        if (Input.GetKeyDown(KeyCode.Q) && Skil1TimeCheck >= Skill1CoolDown)
        {

            Skil1TimeCheck = 0;
            skilll2.SetActive(true);
            Invoke("Offskill2", 1f);


        }
   





    }

    void Offskill1() {

        skilll1.SetActive(false);

    }

    void Offskill2()
    {

        skilll2.SetActive(false);

    }

 

}
