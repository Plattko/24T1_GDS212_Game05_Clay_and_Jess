using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindingBeauty
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        [Header("Sound Effect Variables")]
        [SerializeField] private SFXManager sfxManager;
        [SerializeField] private AudioClip buttonSFX;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeInHierarchy == false)
            {
                pauseMenu.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeInHierarchy == true)
            {
                pauseMenu.SetActive(false);
            }
        }

        public void SettingsButton()
        {
            pauseMenu.SetActive(true);
            sfxManager.PlaySoundEffect(buttonSFX, transform, 0.4f);
        }

        public void BackButton()
        {
            pauseMenu.SetActive(false);
            sfxManager.PlaySoundEffect(buttonSFX, transform, 0.4f);
        }
    }
}
