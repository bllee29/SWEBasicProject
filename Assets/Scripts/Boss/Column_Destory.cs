using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column_Destory : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BOOM!")
        {
            Destroy(gameObject);
        }
    }
}
