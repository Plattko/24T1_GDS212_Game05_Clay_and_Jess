using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FindingBeauty
{
    public class DisplayContinueSprite : MonoBehaviour
    {
        public GameObject continueSprite { get; private set; }
        private Image continueImage;

        private void Awake()
        {
            continueSprite = transform.GetChild(0).gameObject;
            continueImage = continueSprite.GetComponent<Image>();
        }

        public void ShowContinueSprite()
        {
            continueImage.color = new Color(continueImage.color.r, continueImage.color.g, continueImage.color.b, 0);
            continueSprite.SetActive(true);
            StartCoroutine(FadeInSprite(1f));
        }

        public void DisableContinueSprite()
        {
            continueSprite.SetActive(false);
        }

        private IEnumerator FadeInSprite(float fadeDuration)
        {
            float timeElapsed = 0f;

            while (timeElapsed < fadeDuration)
            {
                float newAlpha = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
                continueImage.color = new Color(continueImage.color.r, continueImage.color.g, continueImage.color.b, newAlpha);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
