using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeSpawner : MonoBehaviour
{
    float timer = 0f;
    public float minRangeAroundPlayer = 1f;
    public float maxRangeAroundPlayer = 2f;
    public GameObject ObjectToCreate;
    public GameObject ObjectToSpawnAround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectToSpawnAround == null) this.enabled = false;

        if (timer <= 5f){
            timer += Time.deltaTime;
        } 
        else {
            timer = 0f;
            Vector3 playerPosition = ObjectToSpawnAround.transform.position;
            int xDimension = Random.Range(0,2);
            if (xDimension == 0) xDimension--;
            int yDimension = Random.Range(0,2);
            if (yDimension == 0) yDimension--;
            playerPosition.x += Random.Range(minRangeAroundPlayer,maxRangeAroundPlayer) * xDimension;
            playerPosition.y += Random.Range(minRangeAroundPlayer,maxRangeAroundPlayer) * yDimension;
            Instantiate(ObjectToCreate,playerPosition,ObjectToCreate.transform.rotation);
        }
    }
}
