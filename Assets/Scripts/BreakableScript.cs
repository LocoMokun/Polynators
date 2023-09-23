using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{

    private Animator animator;

    [SerializeField] private GameObject prefabToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Break()
    {

        FMODUnity.RuntimeManager.PlayOneShot("event:/LevelFinish");

        animator.SetTrigger("break");

        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        
    }


    public void DestroyAfterAnim()
    {

        Destroy(gameObject);

    }


}
