using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject fireMage;
    public GameObject waterMage;
    public float minHeight;
    public float maxHeight;
    public float paddingPercent;
    public float cameraAccelRate;
    private float aspect;
    private float height;
    private float width;
    private float goalWidthSize;
    private float goalHeightSize;
    private float goalHeight;
    

    // Start is called before the first frame update
    void Start()
    {
        aspect = (float)Screen.width / Screen.height;

        height = GetComponent<Camera>().orthographicSize * 2;

        width = height * aspect;

    }

    // Update is called once per frame
    void Update()
    {
        height = GetComponent<Camera>().orthographicSize * 2;
        width = height * aspect;
        // if (player == null) this.enabled = false;
        transform.position = getAveragePlayerPosition();
        if(waterMage != null && fireMage != null)
        {
            goalWidthSize = Mathf.Abs(waterMage.transform.position.x - fireMage.transform.position.x) * (1 + paddingPercent);
            goalWidthSize /= aspect;
            goalHeightSize = Mathf.Abs(waterMage.transform.position.y - fireMage.transform.position.y) * (1 + paddingPercent);
            if (goalWidthSize > goalHeightSize)
            {
                if(goalWidthSize/2 < minHeight)
                {
                    goalHeight = minHeight;
                }
                else if(goalWidthSize / 2 > maxHeight)
                {
                    goalHeight = maxHeight;
                }
                else
                {
                    goalHeight = goalWidthSize / 2;
                }
            }
            else
            {
                if (goalHeightSize / 2 < minHeight)
                {
                    goalHeight = minHeight;
                }
                else if (goalHeightSize / 2 > maxHeight)
                {
                    goalHeight = maxHeight;
                }
                else
                {
                    goalHeight = goalHeightSize / 2;
                }
            }
            GetComponent<Camera>().orthographicSize = height / 2 + (goalHeight - height / 2) * Time.deltaTime * cameraAccelRate;
        }
        else
        {
            goalHeight = minHeight;
            GetComponent<Camera>().orthographicSize = height / 2 + (goalHeight - height / 2) * Time.deltaTime * cameraAccelRate;
        }
        
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
