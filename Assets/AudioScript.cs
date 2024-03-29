using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

    public AudioClip space;
    public AudioClip q;
    public AudioClip e;
    public AudioClip shift;
    public AudioClip baasse;
    AudioSource audioSource;


    // Start is called before the first frame update
    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

}
