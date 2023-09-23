using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleResetManager : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform;



    private CameraControllerScript cameraController;
    // Start is called before the first frame update
    void Start()
    {

        EventManager.TriggerEvent(EventNames.LEVEL_LOAD);

        cameraController = FindObjectOfType<CameraControllerScript>();

        cameraController.virtualCamera.enabled = false;


        FindObjectOfType<PlayerController>().transform.position = spawnTransform.position;

        cameraController.virtualCamera.PreviousStateIsValid = false;

        cameraController.virtualCamera.enabled = true;

        var bees = FindObjectsOfType<BeeFriendScript>();

        foreach (BeeFriendScript bee in bees)
        {
            Destroy(bee.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
