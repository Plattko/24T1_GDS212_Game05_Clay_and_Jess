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

        [SerializeField] private Image displayImage;
        [SerializeField] private Image frameImage;

        private ImagePicker imagePicker;

        [Header("Image Scaling")]
        [SerializeField] private SubjectImage playerImage;
        [SerializeField] private RectTransform imageSpace;
        private float maxHeight;
        
        void Start()
        {
            imagePicker = GetComponent<ImagePicker>();

            maxHeight = imageSpace.rect.height;
        }

        public void DisplayNewImage()
        {
            SubjectImage image = imagePicker.PickImage();

            if (image != null)
            {
                nameText.text = image.name;
                SetImage(image);
                infoText.text = image.imageInfo;
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
    }
}
