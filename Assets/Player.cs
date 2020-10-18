using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxXRange = 6f;

    // Speed in meters per second.
    [Tooltip("in ms^-1")][SerializeField] float xSpeed = 12f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalThrow = Input.GetAxis("Horizontal");
        float xOffset = Time.deltaTime * horizontalThrow * xSpeed;
        float rawNewXPosition = xOffset + transform.localPosition.x;
        rawNewXPosition = Mathf.Clamp(rawNewXPosition, -maxXRange, maxXRange);
//        print("X offset = " + xOffset);
        transform.localPosition = new Vector3(rawNewXPosition, transform.localPosition.y, transform.localPosition.z);

    }
}
