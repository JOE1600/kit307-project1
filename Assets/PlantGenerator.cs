using UnityEngine;

public class PlantGenerator : MonoBehaviour
{
    [SerializeField] private Material stemMaterial;    // Assign this in the Inspector
    [SerializeField] private Material flowerMaterial;  // Assign this in the Inspector

    private void Start()
    {
        // Define the custom plant profile for the stem
        Vector3[] stemProfile = new Vector3[]
        {
            new Vector3(0.05f, 0, 0),
            new Vector3(0.05f, 1, 0),
            new Vector3(-0.05f, 1, 0),
            new Vector3(-0.05f, 0, 0),
        };

        // Define the complex sweep path for the stem
        Matrix4x4[] stemPath = new Matrix4x4[20]; // Adjust density as needed

        for (int i = 0; i < stemPath.Length; i++)
        {
            float angle = Random.Range(0f, 360f);
            float scale = Random.Range(0.8f, 1.2f);
            float yOffset = Random.Range(-0.1f, 0.1f);

            Vector3 position = Quaternion.Euler(0, -angle, 0) * Vector3.forward * 0.1f;
            position.y += yOffset;

            Matrix4x4 transformation = Matrix4x4.Rotate(Quaternion.Euler(0, angle, 0)) *
                                        Matrix4x4.Scale(new Vector3(scale, scale, scale)) *
                                        Matrix4x4.Translate(position);

            stemPath[i] = transformation;
        }

        // Create the stem object using the Sweep function from MeshUtilities
        Mesh stemMesh = MeshUtilities.Sweep(stemProfile, stemPath, false);

        // Create a new GameObject for the stem
        GameObject stemObject = new GameObject("Stem");

        // Attach a MeshFilter and MeshRenderer to the stem object
        MeshFilter stemMeshFilter = stemObject.AddComponent<MeshFilter>();
        MeshRenderer stemMeshRenderer = stemObject.AddComponent<MeshRenderer>();
        stemMeshFilter.mesh = stemMesh;

        // Assign the stem material (assigned in the Inspector)
        stemMeshRenderer.material = stemMaterial;

        // Position the stem object as needed
        stemObject.transform.position = transform.position; // Position it at the parent's position

        // Create the flower object
        GameObject flowerObject = new GameObject("Flower");

        // Attach a MeshFilter and MeshRenderer to the flower object
        MeshFilter flowerMeshFilter = flowerObject.AddComponent<MeshFilter>();
        MeshRenderer flowerMeshRenderer = flowerObject.AddComponent<MeshRenderer>();

        // Create the flower mesh (modified to create a clear center with colored petals)
        Mesh flowerMesh = CreateCustomFlowerMesh();

        // Assign the flower material (assigned in the Inspector)
        flowerMeshRenderer.material = flowerMaterial;

        // Position the flower object above the stem
        flowerObject.transform.position = stemObject.transform.position + new Vector3(0, 1, 0);

        // Make the stem object the parent of both stem and flower objects
        flowerObject.transform.parent = stemObject.transform;
        stemObject.transform.parent = transform;
    }

    // Create a custom flower mesh with a clear center and four colored petals
    // Create a custom flower mesh with a clear center and four colored petals
private Mesh CreateCustomFlowerMesh()
{
    Mesh customFlowerMesh = new Mesh();

    // Define the flower geometry (vertices, triangles, UVs, etc.) for a flower with a clear center and quad-shaped petals
    int numPetals = 4; // Number of petals
    int numVerticesPerPetal = 4; // Number of vertices per petal (a quad has 4 vertices)

    // Calculate the total number of vertices, triangles, and UVs needed
    int numVertices = numVerticesPerPetal * numPetals + 1; // +1 for the flower center
    int numTriangles = numVerticesPerPetal * numPetals; // One quad (two triangles) per petal

    Vector3[] vertices = new Vector3[numVertices];
    int[] triangles = new int[numTriangles * 3]; // Each quad has two triangles, each with 3 indices
    Vector2[] uv = new Vector2[numVertices];
    Color[] colors = new Color[numVertices]; // Array to hold colors for each vertex

    // Define the flower center
    vertices[0] = Vector3.zero;
    uv[0] = new Vector2(0.5f, 0.5f); // UV for the flower center
    colors[0] = Color.clear; // Clear color for the flower center

    float angleIncrement = 360f / numPetals;

    for (int petalIndex = 0; petalIndex < numPetals; petalIndex++)
    {
        float startAngle = angleIncrement * petalIndex;
        float endAngle = startAngle + angleIncrement;

        int vertexOffset = petalIndex * numVerticesPerPetal + 1; // Skip the center vertex

        // Define a color for each petal (you can adjust these colors as needed)
        Color petalColor = Color.Lerp(Color.red, Color.blue, (float)petalIndex / numPetals);

        // Create quad-shaped petals with different colors
        for (int i = 0; i < numVerticesPerPetal; i++)
        {
            float angle = Mathf.Lerp(startAngle, endAngle, (float)i / (numVerticesPerPetal - 1));
            float radius = 0.1f; // Adjust the petal radius as needed

            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            vertices[vertexOffset + i] = new Vector3(x, y, 0);

            // UV coordinates for the petals (simple UV mapping for a quad)
            uv[vertexOffset + i] = new Vector2((i % 2 == 0) ? 0f : 1f, (i < 2) ? 0f : 1f);

            // Assign the color to the petal vertex
            colors[vertexOffset + i] = petalColor;
        }

        // Define triangles for the quad (two triangles per quad)
        int triangleOffset = petalIndex * numVerticesPerPetal * 3;

        triangles[triangleOffset + 0] = vertexOffset + 0;
        triangles[triangleOffset + 1] = vertexOffset + 1;
        triangles[triangleOffset + 2] = vertexOffset + 2;
        triangles[triangleOffset + 3] = vertexOffset + 2;
        triangles[triangleOffset + 4] = vertexOffset + 3;
        triangles[triangleOffset + 5] = vertexOffset + 0;
    }

    customFlowerMesh.vertices = vertices;
    customFlowerMesh.triangles = triangles;
    customFlowerMesh.uv = uv;
    customFlowerMesh.colors = colors; // Set the colors for the mesh

    // Additional mesh settings, such as normals, can be added as needed

    return customFlowerMesh;
}




}
