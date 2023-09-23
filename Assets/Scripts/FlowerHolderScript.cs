using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class FlowerHolderScript : MonoBehaviour
{

    public List<COLORS.Hue> correctColors;
    public List<COLORS.Hue> currentColors;


    [SerializeField] private BreakableScript breakable;


    private Animator cameraAnimator;

    [Header("Editor Variables")]
    [SerializeField] private FlagScriptableObject flagSO;
    [SerializeField] private Animator flagAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;


    private bool updateSprites = false;


    void Start()
    {

        LoadSO();



        cameraAnimator = FindObjectOfType<CameraControllerScript>().cameraController;

        foreach (Transform t in transform)
        {
            t.GetComponent<BaseFlower>().callback.AddListener(CheckColors);
        }
    }

    public void LoadSO()
    {
        correctColors = flagSO.flagHues;
        spriteRenderer.sprite = flagSO.flagSprite;
        flagAnimator.runtimeAnimatorController = flagSO.flagAnimator;
    }

    /*
    public void OnValidate()
    {
        updateSprites = true; 

    }


    private void LateUpdate()
    {
        if (updateSprites)
        {
            LoadSO();
            updateSprites = false;
        }
    }
    */

    public void CheckColors()
    {
        // Debug.Log("Checking colors");

        currentColors.Clear();


        foreach(Transform T in transform)
        {
            currentColors.Add(T.GetComponent<BaseFlower>().GetCurrentHue());
        }

        // Make sure colors are sorted
        correctColors.Sort();
        currentColors.Sort();


        if (currentColors.Count == correctColors.Count)
        {

            for (int i = 0; i < correctColors.Count; i++)
            {
                if (correctColors[i] != currentColors[i])
                {
                    // Debug.Log("Colors don't match");

                    // return if any of the colors don't match
                    return;
                }
            }

            // Should only reach this point if all colors are correct
            // Debug.Log("Colors match");
            if (breakable != null && cameraAnimator != null)
            {
                StartCoroutine(Break());
            }
        }
    }


    IEnumerator Break()
    {
        FindObjectOfType<MusicPlayerScript>().SetChangeParam(1.0f);

        EventManager.TriggerEvent(EventNames.PAUSE_EVENT, true);

        cameraAnimator.SetBool("honeycomb", true);

        yield return new WaitForSeconds(0.5f);

        breakable.Break();

        yield return new WaitForSeconds(1.5f);

        cameraAnimator.SetBool("honeycomb", false);


        EventManager.TriggerEvent(EventNames.PAUSE_EVENT, false);

    }


}
