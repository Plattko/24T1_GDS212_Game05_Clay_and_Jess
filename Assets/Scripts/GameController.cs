using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

namespace FindingBeauty
{
    public class GameController : MonoBehaviour
    {
        private ImageDisplay imageDisplay;
        private Onboarding onboarding;

        [SerializeField] private TMP_InputField writingInputField;
        [SerializeField] private TMP_Text popupMessage;

        private int progressionIndex = 1;

        private void Start()
        {
            imageDisplay = GetComponent<ImageDisplay>();
            onboarding = GetComponent<Onboarding>();
            popupMessage.gameObject.SetActive(false);
        }

        public void SubmitButton()
        {
            string inputText = writingInputField.text.Trim();
            int wordCount = writingInputField.text.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

            if (wordCount < 3 || inputText.Length < 10)
            {
                popupMessage.text = "Please write at least 3 words!";
                popupMessage.gameObject.SetActive(true);
                return;
            }
            else if (!IsInputValid(inputText))
            {
                popupMessage.text = "Please don't keyboard smash :(";
                popupMessage.gameObject.SetActive(true);
                return;
            }
            else
            {
                popupMessage.gameObject.SetActive(false);
            }

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

        bool IsInputValid(string input)
        {
            var distinctCharCount = input.Distinct().Count();
            return distinctCharCount > 5; // Ensure there's a variety of characters
        }

        public void PlayButton()
        {
            onboarding.StartCoroutine(onboarding.StartGame());
        }
    }
}
