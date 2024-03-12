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

        private bool isInTextTransition = true;

        [Header("Interactable Elements")]
        [SerializeField] private TMP_InputField writingInputField;
        [SerializeField] private Button submitButton;

        void Start()
        {
            // Get all components of main menu with an alpha

            instructionMessages.AddRange(new List<TextMeshProUGUI> { instructionText1, instructionText2, instructionText3, instructionText4, instructionText5 });

            mainMenuLayout.gameObject.SetActive(true);
            gameLayout.gameObject.SetActive(false);
            writingInputField.enabled = false;
            submitButton.enabled = false;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !isInTextTransition)
            {
                if (instructionMessages.Count > 0)
                {
                    StartCoroutine(UpdateText());
                }
                else
                {
                    // Start gameplay
                    StartCoroutine(StartGameplay());
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
            isInTextTransition = true;

            if (currentText != null)
            {
                yield return StartCoroutine(FadeText(currentText, 1f, 0f, 0.5f));
            }

            if (currentText == instructionText1)
            {
                gameLayout.alpha = 0f;
                gameLayout.gameObject.SetActive(true);
                yield return StartCoroutine(FadeLayout(gameLayout, 0f, 1f, 1f));
            }

            TextMeshProUGUI text = GetNextText();

            yield return StartCoroutine(FadeText(text, 0f, 1f, 1f));

            currentText = text;
            isInTextTransition = false;
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

        private IEnumerator StartGameplay()
        {
            isInTextTransition = true;

            yield return StartCoroutine(FadeText(currentText, 1f, 0f, 0.5f));

            writingInputField.enabled = true;
            submitButton.enabled = true;

            Debug.Log("Onboarding complete.");
        }
    }
}
