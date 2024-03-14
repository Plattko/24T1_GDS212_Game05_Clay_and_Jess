using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace FindingBeauty
{
    public class AudioMixerManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;

        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20f);
        }

        public void SetSFXVolume(float volume)
        {
            audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20f);
        }

        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
        }
    }
}
