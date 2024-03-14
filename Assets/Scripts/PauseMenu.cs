using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindingBeauty
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

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
        }

        public void BackButton()
        {
            pauseMenu.SetActive(false);
        }
    }
}
