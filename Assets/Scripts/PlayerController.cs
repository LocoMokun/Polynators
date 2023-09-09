using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 5;

    private CustomInputAction actions;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        actions = new();
        actions.Player.Enable();


        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnDestroy()
    {
        actions.Player.Disable();
    }

    void Update()
    {
        Vector2 movement = actions.Player.Move.ReadValue<Vector2>();

        Debug.Log(movement.ToString());

        transform.Translate(speed * Time.deltaTime * movement);


        FlipSprite(movement);

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
