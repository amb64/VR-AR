using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public AudioSource music1;
    public AudioSource music2;


    // Start is called before the first frame update
    void Start()
    {
        music1 = GetComponent<AudioSource>();
        music2 = GetComponent<AudioSource>();

        music1.Play(0);
        music2.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
