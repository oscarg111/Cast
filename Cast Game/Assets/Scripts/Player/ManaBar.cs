using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    //public Slider slider;
    public Gradient gradient;
    public Image fill;
    private float maxMana = 0;
    private RectTransform trans;

    public void SetMaxMana(int mana)
    {
        //slider.maxValue = mana;
        //slider.value = mana;
        trans = GetComponent<RectTransform>();
        maxMana = mana;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetMana(int mana)
    {
        //slider.value = mana;
        trans.localScale = new Vector2(mana / maxMana * 1.4f, trans.localScale.y);
        fill.color = gradient.Evaluate(mana / maxMana);
    }
}
