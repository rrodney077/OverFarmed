using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSwitcher : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera cam1;
    [SerializeField] CinemachineVirtualCamera camFollow;

    public bool isMoveable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveable)
        {
            cam1.Priority = 1;
            cam1.GetComponent<CameraMovement>().enabled = true;
            camFollow.Priority = 0;
        }
        else
        {
            cam1.Priority = 0;            
            cam1.GetComponent<CameraMovement>().enabled = false;
            camFollow.Priority = 1;
        }
    }
}
