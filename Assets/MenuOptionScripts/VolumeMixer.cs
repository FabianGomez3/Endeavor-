using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class VolumeMixer : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    //background music
    public void SetMusicVolume(float level)
    {
        mixer.SetFloat("BackgroundMusic", level);
    }

}
