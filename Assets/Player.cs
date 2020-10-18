using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Y then X for local rotation is easiest
 * 
 */
public class Player : MonoBehaviour
{
    public float maxXRange = 6f;
    public float maxYRange = 4.6f;
    public float minYRange = -4.3f;


    // X-axis speed in meters per second.
    [Tooltip("in ms^-1")][SerializeField] float xSpeed = 17f;

    // Y-axis speed in meters per second.
    [Tooltip("in ms^-1")] [SerializeField] float ySpeed = 17f;

    [Tooltip("in ms^-1")] [SerializeField] float positionPitchFactor = -5f;
    [Tooltip("in ms^-1")] [SerializeField] float controlPitchFactor = -18f;
    [Tooltip("in ms^-1")] [SerializeField] float positionYawFactor = 4f;
    [Tooltip("in ms^-1")] [SerializeField] float controlRollFactor = -20f;


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
        float yOffset = Time.deltaTime * verticalThrow * ySpeed;
        float rawNewYPosition = yOffset + transform.localPosition.y;
        rawNewYPosition = Mathf.Clamp(rawNewYPosition, minYRange, maxYRange);
        return rawNewYPosition;
    }

}
