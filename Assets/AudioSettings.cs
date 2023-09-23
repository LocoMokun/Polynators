using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;


    public void MusicVolume()
    {
        float x = musicSlider.value;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("musicVolume", x);
    }

    public void SfxVolume()
    {
        float x = sfxSlider.value;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("sfxVolume", x);
    }
}
