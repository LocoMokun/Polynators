using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isLeft = true;

    [SerializeField] private float timer = 2.0f;
    private float timeRemaining;

    private bool visible = true;

    void Start()
    {
        isLeft = !GetComponent<SpriteRenderer>().flipX;

        timeRemaining = timer;
    }

    

    // Update is called once per frame
    void Update()
    {

        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            visible = !visible;

            GetComponent<SpriteRenderer>().enabled = visible;
            GetComponent<BoxCollider2D>().enabled = visible;

            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.enabled = visible;
            }




            timeRemaining = timer;
        }
    }
}
