using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject A;
    Transform AT;


    // Start is called before the first frame update
    void Start()
    {
        AT = A.transform;    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(AT.position.x, AT.position.y, transform.position.z);
    }
}
