using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private static Stage instance;
    public GameObject Player;
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;    
    public GameObject Startroom;
    public GameObject Boss2;
    public GameObject Boss2end;
    private static int boss2count = 0;
    public static Stage Instance{
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    // void update()
    // {
    //     if(boss2count == 15){
    //         Boss2end.SetActive(true);
    //     }
    //     Debug.Log("boss2count");

    // }
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag ("Player");
        Stage1 = GameObject.Find ("Stage1");
        Stage2 = GameObject.Find ("Stage2");
        Stage3 = GameObject.Find ("Stage3");
        Startroom = GameObject.Find ("StartRoom");
        // Boss1 = GameObject.Find("Boss(1)");

    }
    // void endboss(){
    //     if(Boss1.activeSelf == true){
    //         Debug.Log("bosssssss");
    //     }
    // }
    public void setcount(){
        boss2count++;
    }
    public int getcount(){
        return boss2count;
    }
    public void boss2endgate(){
        Boss2end.SetActive(true);
    }
    public void NextStage(int number){
        if(number == 1)
            Player.transform.position = Stage1.transform.position;
        if(number == 2){
            Player.transform.position = Stage2.transform.position;
            Boss2.SetActive(true);
        }
        if(number == 3)
            Player.transform.position = Startroom.transform.position;
        if(number == 4)
            Player.transform.position = Stage3.transform.position;            
    }


}
