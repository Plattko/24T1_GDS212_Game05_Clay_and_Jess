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
        [Header("Insect Images")]
        [SerializeField] private List<SubjectImage> insects = new List<SubjectImage>();
        [Header("Place Images")]
        [SerializeField] private List<SubjectImage> places = new List<SubjectImage>();
        [Header("Moment Images")]
        [SerializeField] private List<SubjectImage> moments = new List<SubjectImage>();
        [Header("Face Images")]
        [SerializeField] private List<SubjectImage> faces = new List<SubjectImage>();

        [Header("Player Images")]
        [SerializeField] private SubjectImage playerImage1;
        [SerializeField] private SubjectImage playerImage2;
        [SerializeField] private SubjectImage playerImage3;

        public SubjectImage PickImage(int progressionIndex)
        {
            if (progressionIndex < 35)
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
            else if (progressionIndex == 35)
            {
                return playerImage1;
            }
            else if (progressionIndex == 36)
            {
                return playerImage2;
            }
            else if (progressionIndex == 37)
            {
                return playerImage3;
            }
            Debug.LogWarning("Progress index out of range, returning null.");
            return null;
        }

        private List<SubjectImage> PickCategory(int progressionIndex)
        {
            List<SubjectImage> list = new List<SubjectImage>();

            // Images 1-5 are flowers
            if (progressionIndex >= 1 && progressionIndex <6)
            {
                list = flowers;
            }
            // Images 6-10 are trees
            else if (progressionIndex >= 6 && progressionIndex < 11)
            {
                list = trees;
            }
            // Images 11-15 are animals
            else if (progressionIndex >= 11 && progressionIndex < 16)
            {
                list = animals;
            }
            // Images 16-20 are insects
            else if (progressionIndex >= 16 && progressionIndex < 21)
            {
                list = insects;
            }
            // Images 20-25 are places
            else if (progressionIndex >= 21 && progressionIndex < 26)
            {
                list = places;
            }
            // Images 26-30 are places
            else if (progressionIndex >= 26 && progressionIndex < 31)
            {
                list = moments;
            }
            // Images 31-34 are places
            else if (progressionIndex >= 31 && progressionIndex < 35)
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
