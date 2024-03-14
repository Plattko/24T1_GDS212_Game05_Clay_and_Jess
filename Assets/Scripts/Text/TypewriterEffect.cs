using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace FindingBeauty
{
    public class TypewriterEffect : MonoBehaviour
    {
        private TextMeshProUGUI textBox;

        // Typewriter Functionality
        private int currentVisibleCharacterIndex;
        private Coroutine typewriterCoroutine;

        [Header("Typewriter Settings")]
        [SerializeField] private float charactersPerSecond = 30f;
        
        private WaitForSeconds characterDelay;
        private bool isImageInfoText = false;

        // Text Skipping Functionality
        public bool isSkipping { get; private set; }

        // Event Functionality
        private WaitForSeconds textCompleteEventDelay;
        [SerializeField] [Range(0f, 1f)] private float sendCompleteDelay = 0.25f;

        private Action TypewriterCompleteCallback;

        private void Awake()
        {
            textBox = GetComponent<TextMeshProUGUI>();

            characterDelay = new WaitForSeconds(1 / charactersPerSecond);
            textCompleteEventDelay = new WaitForSeconds(sendCompleteDelay);

            isImageInfoText = (gameObject.tag == "ImageInfoText");
        }

        private void Update()
        {
            if (!isImageInfoText)
            {
                if (Input.GetMouseButtonDown(0) && textBox.maxVisibleCharacters != textBox.textInfo.characterCount)
                {
                    Debug.Log("Max visible characters: " + textBox.maxVisibleCharacters + ". Character count: " + (textBox.textInfo.characterCount));
                    Skip();
                }
            }
        }

        public void SetText(Action onCompleteCallback = null, string text = null)
        {
            TypewriterCompleteCallback = onCompleteCallback;
            
            // Stop the current typewriter coroutine if it exists
            StopTypewriter();

            // Set the new text and reset the max and current visible characters to 0
            if (text != null)
            {
                textBox.text = text;
            }
            textBox.maxVisibleCharacters = 0;
            currentVisibleCharacterIndex = 0;

            // Start the typewriter coroutine
            typewriterCoroutine = StartCoroutine(Typewriter());
        }

        private IEnumerator Typewriter()
        {
            TMP_TextInfo textInfo = textBox.textInfo;

            while (currentVisibleCharacterIndex < textInfo.characterCount + 1)
            {
                // Invoke the TextComplete event after the delay if the current visible character is the last character
                var lastCharacterIndex = textInfo.characterCount - 1;

                if (currentVisibleCharacterIndex == lastCharacterIndex)
                {
                    textBox.maxVisibleCharacters++;
                    yield return textCompleteEventDelay;
                    TypewriterCompleteCallback?.Invoke();
                    yield break;
                }
                
                // Reveal the next character by incrementing the maximum visible characters
                textBox.maxVisibleCharacters++;
                // Wait for the character delay
                yield return characterDelay;
                // Increment the current visible character index
                currentVisibleCharacterIndex++;
            }
        }

        private void Skip()
        {
            Debug.Log("Called skip.");
            
            if (isSkipping)
            {
                return;
            }

            isSkipping = true;

            // Stop the typewriter coroutine if it exists
            StopTypewriter();
            // Complete the text box
            textBox.maxVisibleCharacters = textBox.textInfo.characterCount;
            // Invoke the TextComplete event immediately
            TypewriterCompleteCallback?.Invoke();
        }

        private void StopTypewriter()
        {
            if (typewriterCoroutine != null)
            {
                StopCoroutine(typewriterCoroutine);
            }
        }
    }
}
