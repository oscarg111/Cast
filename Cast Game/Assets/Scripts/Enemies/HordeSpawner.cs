using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeSpawner : MonoBehaviour
{
    //float timer = 0f;
    //public float minRangeAroundPlayer = 1f;
    //public float maxRangeAroundPlayer = 2f;
    public GameObject ObjectToCreate;
    public GameObject ObjectToSpawnAround;
    public float normalCooldown;
    public LayerMask spawnArea;
    private float currentWait;
    private float missedCooldown = .01f;
    // Start is called before the first frame update
    void Start()
    {
        currentWait = normalCooldown;
        StartCoroutine(HordeSpawn());
    }

    IEnumerator HordeSpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(currentWait);
            float aspect = (float)Screen.width / Screen.height;
            float height = ObjectToSpawnAround.GetComponent<Camera>().orthographicSize * 2;
            float width = height * aspect;
            int randDir = Random.Range(0, 4);
            Vector2 offset;
            switch(randDir)
            {
                case 0:
                    //Right
                    offset = (Vector2)ObjectToSpawnAround.transform.position + new Vector2(width / 2 + 2, Random.Range(-height / 2, height / 2));
                    if (Physics2D.OverlapCircle(offset, .1f, spawnArea) != null) 
                    {
                        Instantiate(ObjectToCreate, offset, transform.rotation);
                        currentWait = normalCooldown;
                        //Debug.Log("Right hit " + offset);
                    }
                    else
                    {
                        //Debug.Log("Right miss " + offset);
                        currentWait = missedCooldown;
                    }
                    break;
                case 1:
                    //Left
                    offset = (Vector2)ObjectToSpawnAround.transform.position - new Vector2(width / 2 + 2, Random.Range(-height / 2, height / 2));
                    if (Physics2D.OverlapCircle(offset, .1f, spawnArea) != null)
                    {
                        Instantiate(ObjectToCreate, offset, transform.rotation);
                        currentWait = normalCooldown;
                        //Debug.Log("Left hit " + offset);
                    }
                    else
                    {
                        //Debug.Log("Left miss " + offset);
                        currentWait = missedCooldown;
                    }
                    break;
                case 2:
                    //Up
                    offset = (Vector2)ObjectToSpawnAround.transform.position + new Vector2(Random.Range(-width / 2, width / 2), height / 2 + 2);
                    if (Physics2D.OverlapCircle(offset, .1f, spawnArea) != null)
                    {
                        Instantiate(ObjectToCreate, offset, transform.rotation);
                        currentWait = normalCooldown;
                        //Debug.Log("Up hit " + offset);
                    }
                    else
                    {
                        //Debug.Log("Up miss " + offset);
                        currentWait = missedCooldown;
                    }
                    break;
                case 3:
                    //Down
                    offset = (Vector2)ObjectToSpawnAround.transform.position - new Vector2(Random.Range(-width / 2, width / 2), height / 2 + 2);
                    if (Physics2D.OverlapCircle(offset, .1f, spawnArea) != null)
                    {
                        Instantiate(ObjectToCreate, offset, transform.rotation);
                        currentWait = normalCooldown;
                        //Debug.Log("Down hit " + offset);
                    }
                    else
                    {
                        //Debug.Log("Down miss " + offset);
                        currentWait = missedCooldown;
                    }
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (ObjectToSpawnAround == null) this.enabled = false;

        if (timer <= 5f){
            timer += Time.deltaTime;
        } 
        else {
            timer = 0f;
            Vector3 playerPosition = ObjectToSpawnAround.transform.position + new Vector3(0, 0, 10);
            int xDimension = Random.Range(0,2);
            if (xDimension == 0) xDimension--;
            int yDimension = Random.Range(0,2);
            if (yDimension == 0) yDimension--;
            playerPosition.x += Random.Range(minRangeAroundPlayer,maxRangeAroundPlayer) * xDimension;
            playerPosition.y += Random.Range(minRangeAroundPlayer,maxRangeAroundPlayer) * yDimension;
            Instantiate(ObjectToCreate,playerPosition,ObjectToCreate.transform.rotation);
        }*/
    }
}
