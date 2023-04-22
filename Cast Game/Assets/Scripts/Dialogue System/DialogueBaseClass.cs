using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{

    public class DialogueBaseClass : MonoBehaviour
    {
    
        public bool finished {get; private set;}

        public IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delay, float delayBetweenLines, AudioClip sound)
        {
            textHolder.color = textColor;
            textHolder.font = textFont;
            
            for(int i = 0; i < input.Length; i++)
                {
                    textHolder.text += input[i];
                DialogueSoundManager.instance.PlaySound(sound);
                    yield return new WaitForSeconds(delay);
                }

                //yield return new WaitForSeconds(delayBetweenLines);
                yield return new WaitUntil(()=> Input.GetMouseButton(0)); //currently on mb input only, figure out how to specify this between players
                finished = true;
        }
    
    }
}
