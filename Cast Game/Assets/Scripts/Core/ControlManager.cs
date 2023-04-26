using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlManager : MonoBehaviour
{
    public int connectedDevices = 0;
    private PlayerInput fireMageControl;
    private PlayerInput waterMageControl;
    private string fireCType;
    private string waterCType;
    private InputDevice fireCDevice;
    private InputDevice waterCDevice;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(connectedDevices == 2)
        {
            PlayerInput fireSelect = GameObject.FindGameObjectsWithTag("FireMage")[0].GetComponentInChildren<PlayerInput>();
            PlayerInput waterSelect = GameObject.FindGameObjectsWithTag("WaterMage")[0].GetComponentInChildren<PlayerInput>();
            fireCType = fireSelect.currentControlScheme;
            waterCType = waterSelect.currentControlScheme;
            fireCDevice = fireSelect.devices[0];
            waterCDevice = waterSelect.devices[0];
            SceneManager.LoadScene("Main Room 1", LoadSceneMode.Single);
            connectedDevices = 0;
        }
        if(SceneManager.GetActiveScene().name == "Main Room 1")
        {
            GameObject.Find("FireMage").GetComponent<PlayerInput>().SwitchCurrentControlScheme(fireCType, fireCDevice);
            GameObject.Find("WaterMage").GetComponent<PlayerInput>().SwitchCurrentControlScheme(waterCType, waterCDevice);
            Destroy(gameObject);
        }
    }
}
