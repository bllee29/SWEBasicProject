using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class box : MonoBehaviour
{
    public GameObject r;
    // Start is called before the first frame update

    public float Skill3CoolDown;
    private float Skil3TimeCheck;


    // Update is called once per frame

    private void Start()
    {
        Skil3TimeCheck = 0;
    }
    void Update()
    {

        Skil3TimeCheck += Time.deltaTime;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(mousePosition.x, mousePosition.y + 3);

        if (Input.GetKeyDown(KeyCode.E) && Skil3TimeCheck >= Skill3CoolDown)
        {

            Skil3TimeCheck = 0;
            Instantiate(r, transform.position, Quaternion.identity);
          

        }


    }
}
