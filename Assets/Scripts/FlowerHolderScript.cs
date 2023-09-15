using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlowerHolderScript : MonoBehaviour
{

    public List<COLORS.Hue> correctColors;
    public List<COLORS.Hue> currentColors;

    [SerializeField] private BreakableScript breakable;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in transform)
        {
            t.GetComponent<BaseFlower>().callback.AddListener(CheckColors);
        }
    }

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
            if (breakable != null)
                breakable.Break();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
