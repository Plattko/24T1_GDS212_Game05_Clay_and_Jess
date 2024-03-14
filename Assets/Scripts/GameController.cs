using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace FindingBeauty
{
    public class GameController : MonoBehaviour
    {
        private ImageDisplay imageDisplay;
        private Onboarding onboarding;

        [SerializeField] private TMP_InputField writingInputField;

        private int progressionIndex = 1;

        private void Start()
        {
            imageDisplay = GetComponent<ImageDisplay>();
            onboarding = GetComponent<Onboarding>();
        }

        public void SubmitButton()
        {
            Debug.Log("Progression index: " + progressionIndex);

            if (progressionIndex < 23)
            {
                // Add collage functionality

                // Empty input field
                writingInputField.text = "";

                imageDisplay.DisplayNewImage(progressionIndex);
                progressionIndex++;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        public void PlayButton()
        {
            onboarding.StartCoroutine(onboarding.StartGame());
        }
    }
}
