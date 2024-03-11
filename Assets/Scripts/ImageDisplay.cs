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

        private ImagePicker imagePicker;
        
        void Start()
        {
            imagePicker = GetComponent<ImagePicker>();
        }

        private void DisplayNewImage()
        {
            SubjectImage image = imagePicker.PickImage();

            if (image != null)
            {
                nameText.text = image.name;
                displayImage.sprite = image.image;
                infoText.text = image.imageInfo;
            }
            else
            {
                Debug.LogWarning("No image available.");
            }
        }

        public void SubmitButton()
        {
            // Add collage functionality

            DisplayNewImage();
        }
    }
}
