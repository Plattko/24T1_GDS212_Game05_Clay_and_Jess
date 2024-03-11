using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindingBeauty
{
    [CreateAssetMenu(fileName = "New Image", menuName = "Image")]
    public class SubjectImage : ScriptableObject
    {
        public new string name;
        public string imageInfo;

        public Sprite image;
    }
}
