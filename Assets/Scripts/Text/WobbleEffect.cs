using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FindingBeauty
{
    public class WobbleEffect : MonoBehaviour
    {
        private TMP_Text textMesh;
        private Mesh currentMesh;

        private Vector3[] currentVertices;

        private float characterDelay = 0.2f;

        private void Start()
        {
            textMesh = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            textMesh.ForceMeshUpdate();
            currentMesh = textMesh.mesh;
            currentVertices = currentMesh.vertices;

            for (int i = 0; i < textMesh.textInfo.characterCount; i++)
            {
                TMP_CharacterInfo charInfo = textMesh.textInfo.characterInfo[i];

                // Skip invisible characters
                if (!charInfo.isVisible)
                {
                    continue;
                }

                int index = charInfo.vertexIndex;
                float time = Time.time + i * characterDelay;

                Vector3 offset = Wobble(time);

                currentVertices[index] += offset;
                currentVertices[index + 1] += offset;
                currentVertices[index + 2] += offset;
                currentVertices[index + 3] += offset;
            }

            currentMesh.vertices = currentVertices;
            textMesh.canvasRenderer.SetMesh(currentMesh);
        }

        private Vector2 Wobble(float time)
        {
            return new Vector2(Mathf.Sin(time * 3f), Mathf.Cos(time * 3f));
        }
    }
}
