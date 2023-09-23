using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{

    public Animator cameraController { get; private set; }

    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    public Cinemachine.CinemachineVirtualCamera honeycombCamera;


    void Start()
    {
        if (cameraController == null)
        {
            cameraController = GetComponent<Animator>();
        }


        EventManager.StartListening(EventNames.LEVEL_LOAD, SetHoneycombCamera);
        
    }



    public void SetHoneycombCamera()
    {
        honeycombCamera.Follow = FindObjectOfType<BreakableScript>().transform;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
