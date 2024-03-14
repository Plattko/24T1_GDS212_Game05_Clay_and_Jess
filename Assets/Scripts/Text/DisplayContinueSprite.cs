using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FindingBeauty
{
    public class DisplayContinueSprite : MonoBehaviour
    {
        public GameObject continueSpriteSpace { get; private set; }
        private Transform continueSpriteTransform;
        private CanvasGroup continueGroup;

        private Animator animator;

        private void Awake()
        {
            continueSpriteSpace = transform.GetChild(0).gameObject;
            continueGroup = continueSpriteSpace.GetComponent<CanvasGroup>();
            continueSpriteTransform = continueSpriteSpace.transform.GetChild(1);
            animator = continueSpriteTransform.GetComponent<Animator>();
        }

        public void ShowContinueSprite()
        {
            continueGroup.alpha = 0f;
            continueSpriteSpace.SetActive(true);
            StartCoroutine(FadeInSprite(0.25f));
            Debug.Log(name);
            Debug.Log("Called ShowContinueSprite.");
        }

        public IEnumerator DisableContinueSprite(float enlargeDuration)
        {
            // Stop the continue sprite where it is
            animator.speed = 0f;

            RectTransform rectTransform = continueSpriteTransform.GetComponent<RectTransform>();
            float timeElapsed = 0f;

            while (timeElapsed < enlargeDuration)
            {
                float newScale = Mathf.Lerp(1f, 1.4f, timeElapsed / enlargeDuration);
                rectTransform.localScale = new Vector3(newScale, newScale, newScale);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator FadeInSprite(float fadeDuration)
        {
            float timeElapsed = 0f;

            while (timeElapsed < fadeDuration)
            {
                float newAlpha = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
                continueGroup.alpha = newAlpha;
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
