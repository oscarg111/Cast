using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) this.enabled = false;
        else transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        
    }
}
