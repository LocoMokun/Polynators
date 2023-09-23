using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class BeeFriendScript : MonoBehaviour
{
    private HiveScript hive;

    private bool isPaused = false;

    private SpriteRenderer sprite;

    private float speed = 2.0f;


    [SerializeField] private TextMeshPro textMesh;


    private List<string> buzzwords = new List<string>
    {
        "Bee yourself!",
        "Bee-utiful!",
        "Comb together!",
        "To bee or not to bee",
        "It was meant to bee",
        "Keep calm and buzz on",
        

    };

    void Start()
    {

        int whichWord = (int)Random.Range(0, buzzwords.Count);
        textMesh.text = buzzwords[whichWord];


        hive = FindObjectOfType<HiveScript>();

        sprite = GetComponent<SpriteRenderer>();



        EventManager.StartListening(EventNames.PAUSE_EVENT, Pause);
    }


    void Pause(object data)
    {
        isPaused = (bool)data;
    }

    // Update is called once per frame
    void Update()
    {

        if (this == null)
        {
            Destroy(gameObject);
        }

        if (!isPaused)
        {
            float step = speed * Time.deltaTime;

            float x0 = transform.position.x;
            float x1 = hive.transform.position.x;
            sprite.flipX = x0 < x1;


            transform.position = Vector2.MoveTowards(transform.position, hive.transform.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HiveScript>())
        {
            Destroy(gameObject);
        }
    }
}
