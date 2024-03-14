using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FindingBeauty
{
    public class Onboarding : MonoBehaviour
    {
        [Header("Layout Groups")]
        [SerializeField] private CanvasGroup mainMenuLayout;
        [SerializeField] private CanvasGroup gameLayout;

        [Header("Instruction Texts")]
        [SerializeField] private TextMeshProUGUI instructionText1;
        [SerializeField] private TextMeshProUGUI instructionText2;
        [SerializeField] private TextMeshProUGUI instructionText3;
        [SerializeField] private TextMeshProUGUI instructionText4;
        [SerializeField] private TextMeshProUGUI instructionText5;
        
        private TextMeshProUGUI currentText;

        private List<TextMeshProUGUI> instructionMessages = new List<TextMeshProUGUI>();

        [Header("Interactable Elements")]
        [SerializeField] private TMP_InputField writingInputField;
        [SerializeField] private Button submitButton;

        public bool isInTransition = true;

        [Header("Finish Onboarding Variables")]
        [SerializeField] private TypewriterEffect infoTypewriterEffect;

        private void Awake()
        {
            mainMenuLayout.gameObject.SetActive(true);
            gameLayout.gameObject.SetActive(false);

            instructionMessages.AddRange(new List<TextMeshProUGUI> { instructionText1, instructionText2, instructionText3, instructionText4, instructionText5 });

            foreach (TextMeshProUGUI text in instructionMessages)
            {
                text.gameObject.SetActive(false);
            }

            writingInputField.enabled = false;
            submitButton.enabled = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !isInTransition)
            {
                if (instructionMessages.Count > 0)
                {
                    StartCoroutine(UpdateText());
                }
                else
                {
                    // Start gameplay
                    StartCoroutine(FinishOnboarding());
                }
            }
        }

        public IEnumerator StartGame()
        {
            yield return StartCoroutine(FadeLayout(mainMenuLayout, 1f, 0f, 0.5f));
            mainMenuLayout.gameObject.SetActive(false);

            yield return StartCoroutine(UpdateText());
        }

        private IEnumerator UpdateText()
        {
            Debug.Log("Called UpdateText.");
            isInTransition = true;

            // Fade out the current text if it exists
            if (currentText != null)
            {
                DisplayContinueSprite currentContinueSprite = currentText.transform.GetComponent<DisplayContinueSprite>();
                yield return currentContinueSprite.StartCoroutine(currentContinueSprite.DisableContinueSprite(0.5f));
                yield return StartCoroutine(FadeLayout(currentText.GetComponent<CanvasGroup>(), 1f, 0f, 0.5f));
                currentText.gameObject.SetActive(false);
            }

            if (currentText == instructionText1)
            {
                gameLayout.alpha = 0f;
                gameLayout.gameObject.SetActive(true);
                yield return StartCoroutine(FadeLayout(gameLayout, 0f, 1f, 1f));
            }

            // Get the next text
            TextMeshProUGUI text = GetNextText();

            text.maxVisibleCharacters = 0;
            // Enable the next text
            text.gameObject.SetActive(true);

            // Play the typewriter effect and end the transition when it finishes
            text.transform.GetComponent<TypewriterEffect>().SetText(() =>
            {
                // Update the current text
                currentText = text;
                // Display the text's continue sprite
                text.transform.GetComponent<DisplayContinueSprite>().ShowContinueSprite();
                // End the transition
                isInTransition = false;
            });
        }

        private TextMeshProUGUI GetNextText()
        {
            TextMeshProUGUI text = instructionMessages[0];
            instructionMessages.Remove(text);
            return text;
        }

        private IEnumerator FadeText(TextMeshProUGUI text, float startAlpha, float targetAlpha, float fadeDuration)
        {
            float timeElapsed = 0f;

            while (timeElapsed < fadeDuration)
            {
                text.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator FadeLayout(CanvasGroup layout, float startAlpha, float targetAlpha, float fadeDuration)
        {
            float timeElapsed = 0f;

            while (timeElapsed < fadeDuration)
            {
                layout.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator FinishOnboarding()
        {
            isInTransition = true;

            if (currentText != null)
            {
                DisplayContinueSprite currentContinueSprite = currentText.transform.GetComponent<DisplayContinueSprite>();
                yield return currentContinueSprite.StartCoroutine(currentContinueSprite.DisableContinueSprite(0.5f));
                yield return StartCoroutine(FadeLayout(currentText.GetComponent<CanvasGroup>(), 1f, 0f, 0.5f));
                currentText.gameObject.SetActive(false);
            }

            writingInputField.enabled = true;
            submitButton.enabled = true;

            Debug.Log("Onboarding complete.");
            infoTypewriterEffect.enabled = true;
            enabled = false;
        }
    }
}
