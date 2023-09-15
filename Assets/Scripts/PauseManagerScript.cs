using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManagerScript : MonoBehaviour
{
    private CustomInputAction actions;

    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        actions = new();


        actions.Player.Enable();

        actions.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    { 
        if (isPaused)
        {
            UnPause();
            return;
        }

        else if (!isPaused)
        {
            Pause();
        }
    }

    public void Pause()
    {
        Debug.Log("Paused game");
        Time.timeScale = 0;

        isPaused = true;
    }

    public void UnPause()
    {
        Debug.Log("Unpaused game");
        Time.timeScale = 1;

        isPaused = false;
    }
}
