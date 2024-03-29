using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBow : MonoBehaviour
{
    public GameObject arrow;
    public Transform sPoint;
    public float timeBetweenShots;
    
    private float shotTime;




  

    // Update is called once per frame
    void Update()
    {

       




        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotaion = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotaion;

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= shotTime) 
            {
                Instantiate(arrow, sPoint.position, Quaternion.AngleAxis(angle, Vector3.forward));

                shotTime = Time.time + timeBetweenShots;



            }
        
        
        }
    }
}
