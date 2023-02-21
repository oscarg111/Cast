using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeSpawner : MonoBehaviour
{
    float timer = 0f;
    float minRangeAroundPlayer = 1f;
    float maxRangeAroundPlayer = 2f;
    public GameObject ObjectToCreate;
    public GameObject ObjectToSpawnAround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 5f){
            timer += Time.deltaTime;
        } 
        else {
            timer = 0;
            Vector3 playerPosition = ObjectToSpawnAround.transform.position;
            playerPosition.x += Random.Range(minRangeAroundPlayer,maxRangeAroundPlayer);
            playerPosition.y += Random.Range(minRangeAroundPlayer,maxRangeAroundPlayer);
            Instantiate(ObjectToCreate,playerPosition,ObjectToCreate.transform.rotation);
        }
    }
}
