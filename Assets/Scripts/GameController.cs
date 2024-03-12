using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace FindingBeauty
{
    public class GameController : MonoBehaviour
    {
        private ImagePicker imagePicker;
        private ImageDisplay imageDisplay;

        [SerializeField] private TMP_InputField writingInputField;

        private int progressionIndex = 1;

        private void Start()
        {
            imagePicker = GetComponent<ImagePicker>();
            imageDisplay = GetComponent<ImageDisplay>();
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
    }
}
