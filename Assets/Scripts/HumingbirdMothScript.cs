using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumingbirdMothScript : MonoBehaviour
{

    [SerializeField] private List<BaseFlower> flowers;

    [SerializeField] private float speed = 2.0f;

    [SerializeField] private float waitTime = 1.0f;

    private Vector2 target; 

    [SerializeField] private int currentIdx = 0;

    private SpriteRenderer sprite;

    private bool isPaused = false;


    void Start()
    {
        ChangeTargetByCurrentIndex();

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


        if (!isPaused)
        {
            float step = speed * Time.deltaTime;

            float x0 = transform.position.x;
            float x1 = target.x;
            sprite.flipX = x0 < x1;


            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<BaseFlower>(out BaseFlower flower))
        {
            StartCoroutine(ChangeFlower(flower));
        }
    }

    IEnumerator ChangeFlower(BaseFlower flower)
    {
        flower.ChangeColor();

        isPaused = true;

        yield return new WaitForSeconds(waitTime);

        if (currentIdx + 1 >= flowers.Count)
        {
            currentIdx = 0;
            ChangeTargetByCurrentIndex();
            isPaused = false;
        }
        else
        {
            currentIdx++;
            ChangeTargetByCurrentIndex();
            isPaused = false;
        }
    }

    public void ChangeTargetByCurrentIndex()
    {
        target = flowers[currentIdx].transform.position;
    }
}
