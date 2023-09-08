using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilGenerator : MonoBehaviour
{
void Start()
    {
        // Parameters for the pencil
        float length = 1f;
        float radius = 0.1f;
        float taper = 0.2f; // Adjust this value to control tapering
        int sides = 12;
        int tipDensity = 20;

        // Generate the pencil body mesh
        Mesh pencilBodyMesh = MeshUtilities.Pencil(length, radius, taper, sides, tipDensity);

        // Create a GameObject for the pencil body and assign the mesh
        GameObject pencilBody = new GameObject("PencilBody");
        MeshFilter bodyMeshFilter = pencilBody.AddComponent<MeshFilter>();
        bodyMeshFilter.mesh = pencilBodyMesh;

        // Create a MeshRenderer component for the pencil body
        MeshRenderer bodyMeshRenderer = pencilBody.AddComponent<MeshRenderer>();
        // Set materials and textures for the pencil body if needed

        // Set the position of the pencil body
        pencilBody.transform.position = Vector3.zero;
    }

    void Update()
    {
        // You can add update logic here if needed
    }
}

