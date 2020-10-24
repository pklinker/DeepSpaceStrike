using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * Y then X for local rotation is easiest
 * 
 */
public class PlayerController : MonoBehaviour
{

    [Header("General")]
    [Tooltip("in m")] [SerializeField] float maxXRange = 17f;
    [Tooltip("in m")] [SerializeField] float maxYRange = 9.5f;
    [Tooltip("in m")] [SerializeField] float minYRange = -9.5f;
    // X-axis speed in meters per second.
    [Tooltip("in ms^-1")] [SerializeField] float xSpeed = 17f;
    // Y-axis speed in meters per second.
    [Tooltip("in ms^-1")] [SerializeField] float ySpeed = 17f;
    [Header("Screen-position Based")]
    [SerializeField] float positionYawFactor = 2.44f;
    [SerializeField] float positionPitchFactor = -2.66f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -18f;
    [SerializeField] float controlRollFactor = -30f;

    [Header("Controls")]
    [SerializeField] bool invertY = true;

    [Header("Effects")]
    [Tooltip("In seconds")] [SerializeField] float loadLevelDelay = 1f;
    [Tooltip("animation and sound")][SerializeField] GameObject deathFX;


    private float horizontalThrow;
    private float verticalThrow;
    private bool isControllEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isControllEnabled)
        {
            transform.localPosition = new Vector3(getXPosition(), getYPosition(), transform.localPosition.z);
            processRotation();
        }
    }
    public void CollisionOccurred()  // string referenced
    {
        isControllEnabled = false;
        deathFX.SetActive(true);
        Invoke("LoadDyingScene", loadLevelDelay);
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

    private void LoadDyingScene() // string referenced
    {
        isControllEnabled = true;
        SceneManager.LoadScene(1);
        
    }
}
