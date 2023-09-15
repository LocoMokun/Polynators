using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 5;

    private CustomInputAction actions;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    private Vector2 movement;



    public BaseFlower currentFlower;
    public bool isOverFlower = false;

    void Start()
    {
        actions = new();
        actions.Player.Enable();

        actions.Player.Interact.performed += Interact_performed;

        spriteRenderer = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

    }


    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        if (isOverFlower && currentFlower != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Flower");
            currentFlower.ChangeColor();
        }
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
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out BaseFlower flower))
        {

            currentFlower = null;
            isOverFlower = false;
        }
    }

    void Update()
    {
        movement = actions.Player.Move.ReadValue<Vector2>();

        
        FlipSprite(movement);
    }


    private void FixedUpdate()
    {
        rb.AddForce(movement * speed, ForceMode2D.Force);


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
