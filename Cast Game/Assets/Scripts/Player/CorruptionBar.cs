using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxCorruption(int corruption)
    {
        slider.maxValue = corruption;
        slider.value = corruption;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetMinCorruption(int corruption)
    {
        slider.minValue = corruption;
        slider.value = corruption;
        fill.color= gradient.Evaluate(0f);
    }
    public void SetCorruption(int corruption)
    {
        slider.value = corruption;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
