using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Break()
    {
        animator.SetTrigger("break");
    }


    public void DestroyAfterAnim()
    {
        Destroy(gameObject);

    }


}
