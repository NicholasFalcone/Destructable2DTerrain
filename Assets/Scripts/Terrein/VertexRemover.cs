using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Destructable2D
{
    public class VertexRemover : MonoBehaviour
    {
        public MeshComponent mesh;

        public float radius;

        private void Awake()
        {
            mesh = GetComponent<MeshComponent>();
        }

        public void DeleateTri(int index)
        {
            Debug.Log("Removing Triangle");
            List<Vector3> curVer = mesh.vertices.ToList();

            curVer.RemoveAt(index);

            int[] oldTriangle = mesh.mesh.triangles;
            int[] newTriangle = new int[mesh.Triangle.Length - 3];

            int i = 0;
            int j = 0;

            while (j < mesh.mesh.triangles.Length)
            {
                if (j != index * 3)
                {
                    newTriangle[i++] = oldTriangle[j++];
                    newTriangle[i++] = oldTriangle[j++];
                    newTriangle[i++] = oldTriangle[j++];

                }
                else
                {
                    j += 3;
                }
            }
            mesh.Triangle = newTriangle;
            mesh.SetVertices(curVer);
        }
    }
    #region Shitty Stuff

    //private void Awake() {
    //	mesh = GetComponent<MeshComponent>();
    //}

    ///// <summary>
    ///// Update is called every frame, if the MonoBehaviour is enabled.
    ///// </summary>
    //   void OnMouseDown()
    //   { 
    //           Vector3 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);              //TODO
    //}

    //void CheckVertex(Vector3 point)
    //{

    //       var worldPos4 = this.transform.worldToLocalMatrix * point;
    //       var worldPos = new Vector3(worldPos4.x, worldPos4.y, worldPos4.z);

    //       List<Vector3> vertices = new List<Vector3>();
    //       vertices = mesh.vertices.ToList();

    //       Debug.Log("RemovingVertex");
    //	for (int i = 0; i < vertices.Count; i++)
    //	{

    //		Vector3 vertex = mesh.vertices[i];
    //           Vector3 worldPt = transform.TransformPoint(vertex);

    //           float distance = Vector3.Distance(worldPos, vertex);

    //           Debug.Log(distance);
    //           if (distance<radius)
    //		{
    //               Debug.Log(i);
    //               Debug.Log("Remove");
    //			vertices.Remove(vertices[i]);

    //			vertices.Remove(vertices[i++]);

    //			vertices.Remove(vertices[i--]);
    //		}
    //	}
    //       Debug.Log("Vertices Count = " + vertices.Count);
    //       if (IsDivisble(vertices.Count, 3))
    //       {
    //           Debug.LogWarning("Vertex Modified" + vertices.Count % 3);
    //           mesh.SetVertices(vertices);
    //       }
    //       else
    //       {
    //           Debug.LogError("Is Not Divisble by 3 ");
    //       }

    //   }

    //   public bool IsDivisble(int x, int n)
    //   {
    //       if (x % n == 0)
    //           return true;
    //       else
    //           return false;
    //   }
    #endregion
}