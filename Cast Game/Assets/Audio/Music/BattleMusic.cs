using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMusic : MonoBehaviour
{

    public AudioSource sourceOfAudio;
    public AudioClip loop;
    public float timer = 56.307f;

    // Start is called before the first frame update
    void Start()
    {
        sourceOfAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (timer >= 56.507f) {
            sourceOfAudio.PlayOneShot(loop,0.2f);
            timer = 0f;
        } else timer += Time.deltaTime;
    }
}