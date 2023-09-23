using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FlagScriptableObject : ScriptableObject
{
    public List<COLORS.Hue> flagHues;

    public Sprite flagSprite;
    public RuntimeAnimatorController flagAnimator;
}
