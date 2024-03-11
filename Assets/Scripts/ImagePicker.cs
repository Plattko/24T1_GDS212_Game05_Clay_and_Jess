using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindingBeauty
{
    public class ImagePicker : MonoBehaviour
    {
        [Header("Test Images")]
        [SerializeField] private List<SubjectImage> tests = new List<SubjectImage>();

        [Header("Flower Images")]
        [SerializeField] private List<SubjectImage> flowers = new List<SubjectImage>();
        [Header("Tree Images")]
        [SerializeField] private List<SubjectImage> trees = new List<SubjectImage>();
        [Header("Animal Images")]
        [SerializeField] private List<SubjectImage> animals = new List<SubjectImage>();
        [Header("Place Images")]
        [SerializeField] private List<SubjectImage> places = new List<SubjectImage>();
        [Header("Face Images")]
        [SerializeField] private List<SubjectImage> faces = new List<SubjectImage>();

        [Header("Player Image")]
        [SerializeField] private SubjectImage playerImage;

        private List<List<SubjectImage>> categories = new List<List<SubjectImage>>();

        private void Start()
        {
            categories.AddRange(new List<List<SubjectImage>> { flowers, trees, animals, places, faces });
        }

        private List<SubjectImage> PickCategory()
        {
            List<SubjectImage> list = tests;
            
            if (list.Count > 0)
            {
                return list;
            }
            Debug.LogWarning("List not available, returning null.");
            return null;
        }

        public SubjectImage PickImage()
        {
            List<SubjectImage> imageList = PickCategory();
            
            if (imageList != null)
            {
                SubjectImage image = imageList[Random.Range(0, imageList.Count)];

                imageList.Remove(image);
                Debug.Log("Image list count: " + imageList.Count);

                return image;
            }
            return playerImage;
        }
    }
}
