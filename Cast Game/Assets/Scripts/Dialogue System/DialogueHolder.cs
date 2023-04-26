using System.Collections;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {

        private IEnumerator dialogueSeq;
        public bool dialogueFinished;
        public GameObject hordeSpawn;

        private void OnEnable()
        {
            GameObject.Find("FireMage").GetComponent<movement>().DialogueUI = gameObject;
            GameObject.Find("FireMage").GetComponent<weapon>().DialogueUI = gameObject;
            GameObject.Find("WaterMage").GetComponent<movementWaterMage>().DialogueUI = gameObject;
            GameObject.Find("WaterMage").GetComponent<weaponWaterMage>().DialogueUI = gameObject;
            dialogueSeq = dialogueSequence();
            StartCoroutine(dialogueSeq);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Deactivate();
                gameObject.SetActive(false);
                StopCoroutine(dialogueSeq);
            }
        }

        private IEnumerator dialogueSequence()
        {
            if (!dialogueFinished)
            {
                for (int i = 0; i < transform.childCount - 1; i++)
                {
                    Deactivate();
                    transform.GetChild(i).gameObject.SetActive(true);
                    yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
                }
            }
            else
            {
                int index = transform.childCount - 1;
                Deactivate();
                transform.GetChild(index).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(index).GetComponent<DialogueLine>().finished);
            }

            dialogueFinished = true;
            if (hordeSpawn != null)
            {
                hordeSpawn.SetActive(true);
                GameObject.Find("Timer").GetComponent<Timer>().pause = false;
            }
            gameObject.SetActive(false);
            
        }

        private void Deactivate()
        {
            for(int i=0; i<transform.childCount;i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}