using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Y then X for local rotation is easiest
 * 
 */
public class Player : MonoBehaviour
{
    public float maxXRange = 17f;
    public float maxYRange = 9.5f;
    public float minYRange = -9.5f;
   

    // X-axis speed in meters per second.
    [Tooltip("in ms^-1")][SerializeField] float xSpeed = 17f;

    // Y-axis speed in meters per second.
    [Tooltip("in ms^-1")] [SerializeField] float ySpeed = 17f;

    [SerializeField] float positionPitchFactor = -2.66f;
    [SerializeField] float controlPitchFactor = -18f;
    [SerializeField] float positionYawFactor = 2.44f;
    [SerializeField] float controlRollFactor = -30f;
    [SerializeField] bool invertY = true;

    float horizontalThrow;
    float verticalThrow;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(getXPosition(), getYPosition(), transform.localPosition.z);
        processRotation();
    }
 
    //When the Primitive exits the collision, it will change Color
    private void OnTriggerExit(Collider other)
    {
    }
        private void processRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = verticalThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;

        float yaw = transform.localPosition.x * positionYawFactor; 
        float roll = controlRollFactor * horizontalThrow;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }
    private float getXPosition()
    {
        horizontalThrow = Input.GetAxis("Horizontal");
        float xOffset = Time.deltaTime * horizontalThrow * xSpeed;
        //        print("X offset = " + xOffset);
        float rawNewXPosition = xOffset + transform.localPosition.x;
        rawNewXPosition = Mathf.Clamp(rawNewXPosition, -maxXRange, maxXRange);
        return rawNewXPosition;
    }
    private float getYPosition()
    {
        verticalThrow = Input.GetAxis("Vertical");
        if (invertY)
        {
            verticalThrow = -verticalThrow;
        }
        float yOffset = Time.deltaTime * verticalThrow * ySpeed;
        float rawNewYPosition = yOffset + transform.localPosition.y;
        rawNewYPosition = Mathf.Clamp(rawNewYPosition, minYRange, maxYRange);
        return rawNewYPosition;
    }

}
