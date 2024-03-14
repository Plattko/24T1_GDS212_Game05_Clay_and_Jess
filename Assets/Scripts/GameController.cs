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
            string inputText = writingInputField.text.Trim(); // Trim leading and trailing spaces
            int wordCount = inputText.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

            if (wordCount < 3 || inputText.Length < 10) // Ensuring at least 15 characters for 5 words, adjust as necessary
            {
                // Not enough words
                popupMessage.text = "Please write at least 3 words.";
                popupMessage.gameObject.SetActive(true);
                return;
            }
            else if (!IsInputValid(inputText))
            {
                // Input is not considered valid (e.g., repeated characters or doesn't look like actual words)
                popupMessage.text = "Please don't bash your keyboard.";
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
        
        private bool IsInputValid(string input)
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