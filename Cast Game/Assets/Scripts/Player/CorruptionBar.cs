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
        child.GetComponent<RectTransform>().SetParent(trans.parent, true);
        trans.localScale = new Vector2((1 - ((float)corruption / maxCorr)) * 1.26f, (1 - ((float)corruption / maxCorr)) * 1.26f);
        child.GetComponent<RectTransform>().SetParent(trans, true);
        //slider.value = corruption;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
