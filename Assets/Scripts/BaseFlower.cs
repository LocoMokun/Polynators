using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;


public static class COLORS
{
    public enum Hue
    {
        WHITE = 0,
        BLUE = 1,
        PINK = 2,
        PURPLE = 3,
        LIGHTBLUE = 4,
        LIGHTPINK = 5,
        GREEN = 6,
        CAROLINABLUE = 7,
        YELLOW = 8,
        BLACK = 9,
        MAHOGANY = 10,
        TANGERINE = 11,
        GARNET = 12,
        GREY = 13,
        RED = 14,
    }


    public const string white = "#FFFFFF";
    public const string blue = "#3636EE";
    public const string pink = "#FF0086";
    public const string purple = "#824F96";
    public const string lightBlue = "#6BD8FF";
    public const string lightPink = "#F4C5DE";
    public const string green = "#32C80B";
    public const string carolinaBlue = "#4E9ADE";
    public const string yellow = "#E3DB2C";
    public const string black = "#000000";
    public const string mahogany = "#C83F00";
    public const string tangerine = "#FF9767";
    public const string garnet = "#8B0058";
    public const string grey = "#A6A09E";
    public const string red = "#FF0000";


    public static Color GetColorFromEnum(COLORS.Hue hue)
    {
        switch(hue)
        {
            case Hue.WHITE : return GetColorFromName(white);
            case Hue.BLUE: return GetColorFromName(blue);
            case Hue.PINK: return GetColorFromName(pink);
            case Hue.PURPLE: return GetColorFromName(purple);
            case Hue.LIGHTBLUE: return GetColorFromName(lightBlue);
            case Hue.LIGHTPINK: return GetColorFromName(lightPink);
            case Hue.GREEN: return GetColorFromName(green);
            case Hue.CAROLINABLUE: return GetColorFromName(carolinaBlue);
            case Hue.YELLOW: return GetColorFromName(yellow);
            case Hue.BLACK: return GetColorFromName(black);
            case Hue.MAHOGANY: return GetColorFromName(mahogany);
            case Hue.TANGERINE: return GetColorFromName(tangerine);
            case Hue.GARNET: return GetColorFromName(garnet);
            case Hue.GREY: return GetColorFromName(grey);
            case Hue.RED: return GetColorFromName(red);
            default:
                Debug.Log("Couldn't parse hue for color");
                return GetColorFromName(COLORS.white);
        }
    }

    // 3 Colors
    public static Hue[] BiFlag = { Hue.BLUE, Hue.PINK, Hue.PURPLE };
    public static Hue[] PolyFlag = { Hue.CAROLINABLUE, Hue.PINK, Hue.GREEN };
    public static Hue[] PanFlag = { Hue.PINK, Hue.YELLOW, Hue.LIGHTBLUE };


    // 4 Colors
    public static Hue[] NonBinaryFlag = { Hue.YELLOW, Hue.WHITE, Hue.PURPLE, Hue.BLACK };
    public static Hue[] AceFlag = { Hue.BLACK, Hue.GREY, Hue.WHITE, Hue.GARNET };

    // 5 Colors
    public static Hue[] TransFlag = { Hue.LIGHTBLUE, Hue.LIGHTPINK, Hue.WHITE, Hue.LIGHTPINK, Hue.LIGHTBLUE };
    public static Hue[] LesbianFlag = { Hue.MAHOGANY, Hue.TANGERINE, Hue.WHITE, Hue.PINK, Hue.GARNET };


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

    private Light2D light2D;

    public List<COLORS.Hue> colors;

    public UnityEvent callback = new();

    
    
    private int currentIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        light2D = GetComponent<Light2D>();

        ForceColor(COLORS.GetColorFromEnum(colors[0]));

        

    }

    /*
    public void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        light2D = GetComponent<Light2D>();

        if (colors.Count > 0)
        {
            ForceColor(COLORS.GetColorFromEnum(colors[0]));
        }
    }
    */

    public void EnableLight(bool shouldEnable)
    {
        light2D.enabled = shouldEnable;
    }


    public void ChangeColor()
    {
        if (spriteRenderer != null)
        {
            Color color = GetNextColor();

            spriteRenderer.color = color;
            light2D.color = color;
 

            callback.Invoke();
        }
    }

    public void ForceColor(Color color)
    {
        if (spriteRenderer != null)
        {

            spriteRenderer.color = color;
            light2D.color = color;


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

       // Debug.Log("Getting next color at index: " + currentIdx + "On objected" + name);

        return COLORS.GetColorFromEnum(colors[currentIdx]);


    }

    public Color GetCurrentColor()
    {
        return COLORS.GetColorFromEnum(colors[currentIdx]);
    }

    public COLORS.Hue GetCurrentHue()
    {
        // Debug.Log("Index from GETCURRENTHUE " + currentIdx);
        return colors[currentIdx];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
