using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    AudioSource audioSource;
    public AudioClip shotArrowSound;

  



    public Rigidbody2D ArrowRigid;

    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
        audioSource.clip = shotArrowSound;
    }
    // Start is called before the first frame update
    void Start()
    {
        ArrowRigid = GetComponent<Rigidbody2D>();

        audioSource.Play();
        Destroy(gameObject, lifeTime);
      

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Boss") {
         
           DestroyArrow();
            Debug.Log("damage");
        }

    }


    void DestroyArrow() {
        Destroy(gameObject);

    }


}
