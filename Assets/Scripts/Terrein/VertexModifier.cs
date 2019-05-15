using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Destructable2D
{
    public class VertexModifier : MonoBehaviour
    {

        public List<Vector3> originalVecrtices;

        public List<Vector3> modifierVecrtices;

        public float maximumDepression = 0.8f;

        public MeshComponent mesh;

        private void Start()
        {
            MeshRegenereted();
        }


        public void MeshRegenereted()
        {
            mesh = GetComponent<MeshComponent>();
            originalVecrtices = mesh.vertices.ToList();
            modifierVecrtices = mesh.vertices.ToList();
        }

        public void AddDepression(Vector3 depressurePoint, float radius)
        {

            int index = 0;
            var worldPos4 = this.transform.worldToLocalMatrix * depressurePoint;
            var worldPos = new Vector3(worldPos4.x, worldPos4.y, worldPos4.z);

            for (int i = 0; i < modifierVecrtices.Count; i++)
            {
                Vector3 worldPt = transform.TransformPoint(modifierVecrtices[i]);

                var distance = Vector3.Distance(worldPos, worldPt);
                if (distance < radius)
                {
                    Vector3 direction = (depressurePoint - modifierVecrtices[i]).normalized;
                    Debug.Log(direction);
                    index = i;
                    var newVertex = modifierVecrtices[i] + direction * maximumDepression;
                    modifierVecrtices.RemoveAt(i);
                    modifierVecrtices.Insert(i, newVertex);
                }

            }
            mesh.SetVertices(modifierVecrtices);
        }

    }
}