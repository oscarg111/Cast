using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{

    public AudioSource sourceOfAudio;
    public AudioClip loop;
    public float timer = 57.5f;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sourceOfAudio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    { 
        sceneName = SceneManager.GetActiveScene().name;
        if (timer >= 58f) {
            sourceOfAudio.PlayOneShot(loop);
            timer = 0f;
        } else if (sceneName == "Main Menu" || sceneName == "Character Select") {
            timer += Time.deltaTime; 
        } else
        {
            timer = 0f;
            sourceOfAudio.Stop();
        }
    }
}
