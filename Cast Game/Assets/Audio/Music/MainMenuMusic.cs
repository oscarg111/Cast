using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{

    public AudioSource sourceOfAudio;
    public AudioClip loop;
    public float timer = 57.5f;

    // Start is called before the first frame update
    void Start()
    {
        sourceOfAudio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 58f) {
            sourceOfAudio.PlayOneShot(loop);
            timer = 0f;
        } else timer += Time.deltaTime;
    }
}
