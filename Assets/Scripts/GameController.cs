using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FindingBeauty
{
    public class GameController : MonoBehaviour
    {
        private ImageDisplay imageDisplay;

        [SerializeField] private TMP_InputField writingInputField;

        private void Start()
        {
            imageDisplay = GetComponent<ImageDisplay>();
        }

        public void SubmitButton()
        {
            // Add collage functionality

            // Empty input field
            writingInputField.text = "";

            imageDisplay.DisplayNewImage();
        }
    }
}
