using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // Adjust the speed of the space ship
    public float speed = 60.0f;
    // Adjust the rotation speed of the space ship
    public float rotationSpeed = 5.0f;

    // The target (waypoint) position.
    private Transform target;
    GameObject circuit;
    private int childIndex;
    private int numOfChildren = 10;

    // Start is called before the first frame update
    void Start()
    {
        // get initial target
        circuit = GameObject.Find("Circuit");
        Transform circuitTransform = circuit.transform;
        target = circuit.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
        RotateTowardsTarget();
        UpdateTarget();
    }

    void MoveTowardsTarget()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

    }
    void RotateTowardsTarget()
    {
        float step = rotationSpeed * Time.deltaTime; // calculate distance to move
        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    private void UpdateTarget()
    {
        // Check if the position of the camera and the target waypoint are approximately equal.
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            // get netxt waypoint
            childIndex = ((childIndex + 1) % (numOfChildren));
            target = circuit.transform.GetChild(childIndex);
        }
    }

}
