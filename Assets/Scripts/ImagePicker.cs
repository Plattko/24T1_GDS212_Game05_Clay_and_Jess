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

        [Header("Player Images")]
        [SerializeField] private SubjectImage playerImage1;
        [SerializeField] private SubjectImage playerImage2;
        [SerializeField] private SubjectImage playerImage3;

        private List<List<SubjectImage>> categories = new List<List<SubjectImage>>();

        private void Start()
        {
            categories.AddRange(new List<List<SubjectImage>> { flowers, trees, animals, places, faces });
        }

        public SubjectImage PickImage(int progressionIndex)
        {
            if (progressionIndex < 15)
            {
                List<SubjectImage> imageList = PickCategory(progressionIndex);

                if (imageList != null)
                {
                    SubjectImage image = imageList[Random.Range(0, imageList.Count)];

                    imageList.Remove(image);
                    Debug.Log("Image list count: " + imageList.Count);

                    return image;
                }
            }
            else if (progressionIndex == 15)
            {
                return playerImage1;
            }
            else if (progressionIndex == 16)
            {
                return playerImage2;
            }
            else if (progressionIndex == 17)
            {
                return playerImage3;
            }
            Debug.LogWarning("Progress index out of range, returning null.");
            return null;
        }

        private List<SubjectImage> PickCategory(int progressionIndex)
        {
            List<SubjectImage> list = new List<SubjectImage>();

            // Image 1 is a flower
            if (progressionIndex == 1)
            {
                list = flowers;
            }
            // Image 2 is a tree
            else if (progressionIndex == 2)
            {
                list = trees;
            }
            // Images 3-5 are flowers or trees
            else if (progressionIndex >= 3 && progressionIndex < 6)
            {
                List<List<SubjectImage>> availableCategories = new List<List<SubjectImage>> { flowers, trees };
                list = availableCategories[Random.Range(0, availableCategories.Count)];
            }
            // Image 6 is a place
            else if (progressionIndex == 6)
            {
                list = places;
            }
            // Images 7-9 are flowers, trees or places
            else if (progressionIndex >= 7 && progressionIndex < 10)
            {
                List<List<SubjectImage>> availableCategories = new List<List<SubjectImage>> { flowers, trees, places };
                list = availableCategories[Random.Range(0, availableCategories.Count)];
            }
            // Images 10-14 are faces
            else if (progressionIndex >= 10 && progressionIndex < 15)
            {
                list = faces;
            }

            if (list.Count > 0)
            {
                return list;
            }
            Debug.LogWarning("No items in list, returning null.");
            return null;
        }
    }
}
