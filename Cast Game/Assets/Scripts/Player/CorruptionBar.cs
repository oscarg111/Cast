using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionBar : MonoBehaviour
{
    //public Slider slider;
    //public Gradient gradient;
    //public Image fill;
    public GameObject child;
    private RectTransform trans;
    private float maxCorr;
    private float currentHealth;
    private float goalHealth;

    public void SetMaxCorruption(int corruption)
    {
        trans = GetComponent<RectTransform>();
        maxCorr = (float)corruption;
        //slider.maxValue = corruption;
        //slider.value = corruption;

        //fill.color = gradient.Evaluate(1f);
    }
    public void SetMinCorruption(int corruption)
    {
        //slider.minValue = corruption;
        //slider.value = corruption;
        //fill.color= gradient.Evaluate(0f);
    }
    public void SetCorruption(int corruption)
    {
        if (corruption > 100)
        {
            corruption = 100;
        }
        goalHealth = corruption;
        //slider.value = corruption;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private void FixedUpdate()
    {
        if(currentHealth != goalHealth)
        {
            child.GetComponent<RectTransform>().SetParent(trans.parent, true);
            currentHealth = Mathf.Lerp(currentHealth, goalHealth, Time.fixedDeltaTime * 5);
            trans.localScale = new Vector2((1 - ((float)currentHealth / maxCorr)) * 1.26f, (1 - ((float)currentHealth / maxCorr)) * 1.26f);
            child.GetComponent<RectTransform>().SetParent(trans, true);
        }
    }
}
