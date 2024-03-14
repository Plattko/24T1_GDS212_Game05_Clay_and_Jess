using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindingBeauty
{
    public class SFXManager : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxObjectPrefab;

        public void PlaySoundEffect(AudioClip audioClip, Transform spawnTransform, float volume)
        {
            // Instantiate sound effect object
            AudioSource audioSource = Instantiate(sfxObjectPrefab, spawnTransform.position, Quaternion.identity);

            // Set the sound effect
            audioSource.clip = audioClip;

            // Get the clip's length
            float clipLength = audioSource.clip.length;

            // Set the volume
            audioSource.volume = volume;

            // Play the sound effect
            audioSource.Play();

            // Destroy the sound effect object after the clip finishes playing
            Destroy(audioSource.gameObject, clipLength);
        }
    }
}
