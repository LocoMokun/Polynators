using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public FMODUnity.EventReference musicRef;

    FMOD.Studio.EventInstance musicInstance;

    public float localChangeParam = 0;

    bool shouldSwitch = true;


    void Start()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicRef);

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(musicInstance, transform);

        musicInstance.start();

        SetChangeParam(0.0f);

        EventManager.StartListening(EventNames.LEVEL_LOAD, ResetLevel);
    }

    void ResetLevel()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SetChangeParam(0.0f);
        musicInstance.start();
        shouldSwitch = true;
    }


    public void SetChangeParam(float value)
    {
        musicInstance.setParameterByName("musicChange", value);
        localChangeParam = value;
    }



    private void Update()
    {
        if (localChangeParam == 1.0f && shouldSwitch)
        {
            StartCoroutine(StopMusic());
            shouldSwitch = false;
        }
    }

    IEnumerator StopMusic()
    {
        Debug.Log("stopping");
        yield return new WaitForSeconds(8.0f);

        SetChangeParam(0.0f);
    }
}
