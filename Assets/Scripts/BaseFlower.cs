using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class COLORS
{
    public enum Hue
    {
        WHITE = 0,
        BLUE = 1,
        PINK = 2,
        PURPLE = 3,
    }


    public const string white = "#FFFFFF";
    public const string blue = "#009CFF";
    public const string pink = "#FF4EBF";
    public const string purple = "#6E30E2";


    public static Color GetColorFromEnum(COLORS.Hue hue)
    {
        switch(hue)
        {
            case Hue.WHITE : return GetColorFromName(white);
            case Hue.BLUE: return GetColorFromName(blue);
            case Hue.PINK: return GetColorFromName(pink);
            case Hue.PURPLE: return GetColorFromName(purple);
            default:
                Debug.Log("Couldn't parse hue for color");
                return GetColorFromName(COLORS.white);
        }
    }


    public static Color GetColorFromName(string color)
    {
        if (ColorUtility.TryParseHtmlString(color, out Color color1))
            return color1;

        Debug.Log("Couldn't parse hex");
        return Color.white;
    }
}

public class BaseFlower : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;

    public List<COLORS.Hue> colors;

    public UnityEvent callback = new();

    
    
    private int currentIdx = -1;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        ChangeColor();

        

    }


    public void ChangeColor()
    {
        if (spriteRenderer != null)
        {

            spriteRenderer.color = GetNextColor();

 

            callback.Invoke();
        }
    }

    public Color GetNextColor()
    {

        if (currentIdx + 1 >= colors.Count)
        {
            currentIdx = 0;
        }

        else
        {
            currentIdx++;
        }

        // Debug.Log("Getting next color at index: " + currentIdx);

        return COLORS.GetColorFromEnum(colors[currentIdx]);


    }

    public Color GetCurrentColor()
    {
        return COLORS.GetColorFromEnum(colors[currentIdx]);
    }

    public COLORS.Hue GetCurrentHue()
    {
        return colors[currentIdx];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
