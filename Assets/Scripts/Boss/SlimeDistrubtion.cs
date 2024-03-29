using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDistrubtion : MonoBehaviour
{
    // Start is called before the first frame update
    public SlimeAi selfSlime;
    Transform slime11;
    Transform slime22;
    int count = 1;
    public GameObject nextSlime;
    float curHp;
    public GameObject rangeObject;
    BoxCollider2D spawnCollider;    
    int num = 1;
    private void Awake()
    {
        spawnCollider = rangeObject.GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        slime11 = GameObject.Find(returnName(num)).transform;
        Debug.Log(returnName(num));
        // slime22 = GameObject.Find("Boss(2)").transform;      
    }

    // Update is called once per frame
    void Update()
    {
        // selfSlime = GameObject.Find("Slime1").GetComponent<SlimeAi>();
        curHp = selfSlime.curHp;
        disrupt();
    }
    string returnName(int i){
        return "Slime" + i;
    }
    void disrupt()
    {

        if (curHp == 1f && count == 1)
        {
            GameObject slime1 = Instantiate(nextSlime, Return_Position(), transform.rotation);
            GameObject slime2 = Instantiate(nextSlime, Return_Position(), transform.rotation);            
            selfSlime.gameObject.SetActive(false);
            // Destroy(selfSlime);
            count++;
        }
        
    }
    Vector3 Return_Position()
    {
        Vector3 originPosition = rangeObject.transform.position;

        float range_X = spawnCollider.bounds.size.x;
        float range_Y = spawnCollider.bounds.size.y;

        range_X /= 2f;
        range_Y /= 2f;
        Vector3 RandomPostion = new Vector3(range_X, range_Y, 0f);
        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }    
}
