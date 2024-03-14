using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FindingBeauty
{
    public class ImageDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private TypewriterEffect infoTypewriterEffect;

        [SerializeField] private Image displayImage;
        [SerializeField] private Image frameImage;

        private ImagePicker imagePicker;

        [Header("Image Scaling")]
        [SerializeField] private SubjectImage playerImage;
        [SerializeField] private RectTransform imageSpace;
        private float maxHeight;

        [Header("Image Transition")]
        [SerializeField] CanvasGroup imageLayout;

        [Header("Sound Effect Variables")]
        [SerializeField] private SFXManager sfxManager;
        [SerializeField] private AudioClip newImageSFX;

        void Start()
        {
            imagePicker = GetComponent<ImagePicker>();

            maxHeight = imageSpace.rect.height;
        }

        public IEnumerator DisplayNewImage(int progressionIndex)
        {
            SubjectImage image = imagePicker.PickImage(progressionIndex);

            if (image != null)
            {
                // Fade out image
                yield return FadeLayout(imageLayout, 1f, 0f, 0.5f);
                
                // Set new name and image
                nameText.text = image.name;
                SetImage(image);
                // Reset info text
                infoText.text = "";

                // Play new image sound effect
                sfxManager.PlaySoundEffect(newImageSFX, transform, 1f);
                // Fade in new image
                yield return FadeLayout(imageLayout, 0f, 1f, 1f);

                // Display info text with typewriter effect
                SetInfoText(image);
            }
            else
            {
                Debug.LogWarning("No image available.");
            }
        }

        private void SetImage(SubjectImage image)
        {
            displayImage.sprite = image.image;

            if (image.name != playerImage.name)
            {
                displayImage.SetNativeSize();

                float currentWidth = image.image.rect.width;
                float currentHeight = image.image.rect.height;

                if (currentHeight > maxHeight)
                {
                    float aspectRatio = currentWidth / currentHeight;
                    float clampedHeight = Mathf.Clamp(currentHeight, 0f, maxHeight);
                    float clampedWidth = clampedHeight * aspectRatio;
                    displayImage.rectTransform.sizeDelta = new Vector2(clampedWidth, clampedHeight);
                }

                frameImage.rectTransform.sizeDelta = displayImage.rectTransform.sizeDelta;
            }
            else
            {
                Vector2 imageSpaceScale = new Vector2(imageSpace.rect.width, imageSpace.rect.height);
                displayImage.rectTransform.sizeDelta = imageSpaceScale;
                frameImage.rectTransform.sizeDelta = imageSpaceScale;
            }
        }

        private void SetInfoText(SubjectImage image)
        {
            infoTypewriterEffect.SetText(null, image.imageInfo);
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
    }
}
