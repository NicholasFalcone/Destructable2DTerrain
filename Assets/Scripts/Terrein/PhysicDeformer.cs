using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Destructable2D
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicDeformer : MonoBehaviour
    {
        public float collisionRadius = 1.5f;

        public VertexModifier deformerMesh;

        private void Awake()
        {
            deformerMesh = FindObjectOfType<VertexModifier>();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                deformerMesh = collision.gameObject.GetComponent<VertexModifier>();
                if(deformerMesh == null)
                    return;
                foreach (var contact in collision.contacts)
                {
                    deformerMesh.AddDepression(contact.point, collisionRadius);
                }
            }
        }
    }
}