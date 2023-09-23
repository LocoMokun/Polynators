using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManagerScript : MonoBehaviour
{
    private CustomInputAction actions;

    [SerializeField] private GameObject pauseMenu;

    private bool isPaused = false;
    // Start is called before the first frame update


    static public PauseManagerScript instance;

    void Start()
    {


        instance = this;

        actions = new();


        actions.Player.Enable();

        actions.Player.Pause.performed += Pause_performed;
    }


    public bool GetPauseState()
    {
        return isPaused;
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
        EventManager.TriggerEvent(EventNames.PAUSE_EVENT, isPaused);
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        Debug.Log("Unpaused game");
        Time.timeScale = 1;

        isPaused = false;
        EventManager.TriggerEvent(EventNames.PAUSE_EVENT, isPaused);
        pauseMenu.SetActive(false);
    }
}
