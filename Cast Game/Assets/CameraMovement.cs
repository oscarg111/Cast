using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject fireMage;
    public GameObject waterMage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (player == null) this.enabled = false;
        transform.position = getAveragePlayerPosition();
        
    }

    Vector3 getAveragePlayerPosition() {
        float x = 0f;
        float y = 0f;
        int n = 0; // num of alive mages
        if (fireMage != null) {
            x += fireMage.transform.position.x;
            y += fireMage.transform.position.y;
            n++;
        }

        if (waterMage != null) {
            x += waterMage.transform.position.x;
            y += waterMage.transform.position.y;
            n++;
        }

        if (n == 0) return transform.position;
        else return new Vector3(x/n, y/n, transform.position.z);
    }
}
