using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time;
    public bool pause;
    private TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pause)
        {
            time -= Time.deltaTime;
        }
        if(time < 0)
        {
            pause = true;
            time = 0;
            RanOut();
        }
        timerText.text = (((int)time / 60).ToString("D2") + ":" + ((int)time % 60).ToString("D2") + ":" + ((int)(time % 1 * 100)).ToString("D2"));
    }

    public void RanOut()
    {

    }
}
