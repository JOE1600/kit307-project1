using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 60f;

    private GameObject carBody;
    private GameObject frontLeftWheel;
    private GameObject frontRightWheel;
    private GameObject rearLeftWheel;
    private GameObject rearRightWheel;

    private void Start()
    {
        // Create the main body of the car (cube)
        carBody = CreateCube("CarBody", Vector3.zero, new Vector3(2f, 0.5f, 1f));
        carBody.GetComponent<Renderer>().material.color = Color.blue;

        // Create the wheels of the car (custom tires)
        frontLeftWheel = CreateCustomTire("FrontLeftWheel", new Vector3(-0.75f, -0.25f, 0.6f), new Vector3(0.2f, 0.1f, 0.2f));
        frontRightWheel = CreateCustomTire("FrontRightWheel", new Vector3(0.75f, -0.25f, 0.6f), new Vector3(0.2f, 0.1f, 0.2f));
        rearLeftWheel = CreateCustomTire("RearLeftWheel", new Vector3(-0.75f, -0.25f, -0.6f), new Vector3(0.2f, 0.1f, 0.2f));
        rearRightWheel = CreateCustomTire("RearRightWheel", new Vector3(0.75f, -0.25f, -0.6f), new Vector3(0.2f, 0.1f, 0.2f));

        // Add more specifications here
        // For example, create a roof, windows, or any other desired parts
    }

    private void Update()
    {
        // Move the car forward
        float moveAmount = moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveAmount);

        // Rotate the car body
        float rotationAmount = rotationSpeed * Time.deltaTime;
        carBody.transform.Rotate(Vector3.up, rotationAmount);

        // Rotate the wheels
        AnimateWheel(frontLeftWheel);
        AnimateWheel(frontRightWheel);
        AnimateWheel(rearLeftWheel);
        AnimateWheel(rearRightWheel);
    }

    private GameObject CreateCube(string name, Vector3 position, Vector3 scale)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = name;
        cube.transform.parent = transform;
        cube.transform.localPosition = position;
        cube.transform.localScale = scale;
        return cube;
    }

    private GameObject CreateCustomTire(string name, Vector3 position, Vector3 scale)
    {
        // Create the main body of the tire (cylinder)
        GameObject tire = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        tire.name = name;
        tire.transform.parent = transform;
        tire.transform.localPosition = position;
        tire.transform.localScale = scale;

        // Create the ends of the tire (scaled spheres)
        GameObject tireEnd1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        tireEnd1.name = name + "End1";
        tireEnd1.transform.parent = tire.transform;
        tireEnd1.transform.localPosition = new Vector3(0f, 0f, -scale.z * 0.5f);
        tireEnd1.transform.localScale = new Vector3(1f, 1f, 0.5f); // Adjust the scale to match the desired circular appearance

        GameObject tireEnd2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        tireEnd2.name = name + "End2";
        tireEnd2.transform.parent = tire.transform;
        tireEnd2.transform.localPosition = new Vector3(0f, 0f, scale.z * 0.5f);
        tireEnd2.transform.localScale = new Vector3(1f, 1f, 0.5f); // Adjust the scale to match the desired circular appearance

        return tire;
    }

    private void AnimateWheel(GameObject wheel)
    {
        // Rotate the wheel around its local axis
        wheel.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
