using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    // public AudioSource steamAudio;
    // Start is called before the first frame update
    void Start()
    {
        // steamAudio = GetComponent<AudioSource>();
        // steamAudio.Play();
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
