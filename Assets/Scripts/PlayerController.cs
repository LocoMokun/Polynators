using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 5;

    private CustomInputAction actions;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    private bool isPaused;

    private Vector2 movement;

    public FMODUnity.EventReference beeEvent;

    public FMOD.Studio.PARAMETER_ID speedParamID;

    [SerializeField] private GameObject wheel;


    private int currentLevel = 1;



    FMOD.Studio.EventInstance beeEventInstance;

    public BaseFlower currentFlower;
    public bool isOverFlower = false;

    void Start()
    {
        beeEventInstance = FMODUnity.RuntimeManager.CreateInstance(beeEvent);

        FMODUnity.RuntimeManager.AttachInstanceToGameObject(beeEventInstance, transform, rb);

        beeEventInstance.start();

   

        actions = new();
        actions.Player.Enable();

        actions.Player.Interact.performed += Interact_performed;

        actions.Player.Interact.started += Interact_started;

        actions.Player.Interact.canceled += Interact_canceled;

        spriteRenderer = GetComponent<SpriteRenderer>();

        EventManager.StartListening(EventNames.PAUSE_EVENT, Pause);
        rb = GetComponent<Rigidbody2D>();

    }

    private void Interact_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        wheel.SetActive(false);
    }

    private void Interact_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isOverFlower && currentFlower != null)
        {
            wheel.SetActive(true);
        }
    }

    public void ChangeFlower()
    {

        if (isOverFlower && currentFlower != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Flower");
            currentFlower.ChangeColor();

            wheel.SetActive(false);
        }
    }

    private void Pause(object data)
    {
        isPaused = (bool)data;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

    }

    private void OnDestroy()
    {
        actions.Player.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Check if colliding with a flower
        if (collision.transform.TryGetComponent(out BaseFlower flower))
        {

            currentFlower = flower;
            isOverFlower = true;

            flower.EnableLight(true);
        }


        if (collision.CompareTag("SceneTransition"))
        {
            string nextLevel = "Level" + (currentLevel + 1).ToString();
            SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);

            string level = "Level" + currentLevel.ToString();
            SceneManager.UnloadSceneAsync(level);

            


            currentLevel++;
        }

        if (collision.CompareTag("Wind"))
        {
            if (collision.GetComponent<WindScript>().isLeft)
                rb.AddForce(Vector2.left * 25, ForceMode2D.Impulse);

            else
                rb.AddForce(Vector2.right * 25, ForceMode2D.Impulse);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out BaseFlower flower))
        {

            currentFlower = null;
            isOverFlower = false;

            wheel.SetActive(false);
            flower.EnableLight(false);
        }
    }

    void Update()
    {
        if (!isPaused)
        {

            movement = actions.Player.Move.ReadValue<Vector2>();


            FlipSprite(movement);




            actions.Player.Interact.IsPressed();
        }

  
    }


    private void FixedUpdate()
    {
        rb.AddForce(movement * speed, ForceMode2D.Force);
        float s = Mathf.Max(Mathf.Abs(rb.velocity.x * 15.0f));
        // Debug.Log(s);
        beeEventInstance.setParameterByName("speed", s);
    }

    void FlipSprite(Vector2 movement)
    {
        if (movement.x < 0 && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
            return;
        }

        if (movement.x > 0 && spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
            return;
        }
    }

}
