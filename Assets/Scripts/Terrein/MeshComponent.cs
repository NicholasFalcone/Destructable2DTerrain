using UnityEngine;
using System.Collections.Generic;

namespace Destructable2D
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(PolygonCollider2D))]
    public class MeshComponent : MonoBehaviour
    {
        public int xSize, ySize;

        public Mesh mesh;
        public Vector3[] vertices;

        public List<EdgeHelpers.Edge> edges = new List<EdgeHelpers.Edge>();
        public int edgeLength;

        [SerializeField] private int[] m_triangles;

        public int[] Triangle
        {
            get
            {
                return m_triangles ?? (mesh.triangles);
            }
            set
            {
                m_triangles = value;
                mesh.triangles = m_triangles;
            }
        }

        public PolygonCollider2D m_collider;

        private void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            Generate();
        }

        private void Start()
        {
            // CalculateColliderPoint();

        }

        private void OnValidate()
        {
            Generate();
        }

        public void Generate()
        {
            if (!Application.isPlaying)
                mesh = GetComponent<MeshFilter>().sharedMesh;
            else
                mesh = GetComponent<MeshFilter>().mesh;
            mesh.name = "Test Mesh";

            vertices = new Vector3[(xSize + 1) * (ySize + 1)];
            Vector2[] uv = new Vector2[vertices.Length];
            Vector4[] tangents = new Vector4[vertices.Length];
            Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
            for (int i = 0, y = 0; y <= ySize; y++)
            {
                for (int x = 0; x <= xSize; x++, i++)
                {
                    vertices[i] = new Vector3(x, y);
                    uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                    tangents[i] = tangent;
                }
            }
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.tangents = tangents;

            GenerateTriangle();
            mesh.RecalculateNormals();
            edges = EdgeHelpers.GetEdges(m_triangles).FindBoundary().SortEdges();
            CalculateColliderPoint();
        }


        void GenerateTriangle()
        {
            m_triangles = new int[xSize * ySize * 6];
            for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
            {
                for (int x = 0; x < xSize; x++, ti += 6, vi++)
                {
                    m_triangles[ti] = vi;
                    m_triangles[ti + 3] = m_triangles[ti + 2] = vi + 1;
                    m_triangles[ti + 4] = m_triangles[ti + 1] = vi + xSize + 1;
                    m_triangles[ti + 5] = vi + xSize + 2;
                }
            }
            mesh.triangles = m_triangles;
        }

        public void UpdateVertice()
        {
            mesh.vertices = vertices;
            CalculateColliderPoint();
            // triangles = mesh.triangles;
        }

        public void CalculateColliderPoint()
        {
            List<Vector2> points = new List<Vector2>();

            for (int i = 0; i < edges.Count; i++)
            {
                points.Add(vertices[edges[i].v1]);

                points.Add(vertices[edges[i].v2]);

            }
            m_collider.points = points.ToArray();
        }
        public void SetVertices(List<Vector3> newVrtices)
        {
            vertices = newVrtices.ToArray();
            UpdateVertice();
        }
    }
}