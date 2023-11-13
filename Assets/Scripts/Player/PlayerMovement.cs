using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float verticalSpeed;
    public float horizontalSpeed;
    public float rotationDampening = 0.85f;
    public float forwardSpeedDampening = 0.92f;

    private Vector3 mForwardSpeed;
    private Vector3 mVerticalSpeed;
    private Vector3 mRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            Vector3 forward = transform.forward;
            mForwardSpeed = forward * speed;
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector3 forward = transform.forward;
            mForwardSpeed = forward * -speed;
        }

        Vector3 newPos = transform.position + mForwardSpeed;
        transform.position = newPos;

        mForwardSpeed *= 0.98f;

        if (Input.GetKey(KeyCode.Q))
        {
            mVerticalSpeed.y -= verticalSpeed;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            mVerticalSpeed.y += verticalSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            mRotation.y -= rotationSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            mRotation.y += rotationSpeed;
        }

        transform.position += mVerticalSpeed;
        mVerticalSpeed *= 0.8f;

        Vector3 rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotation + mRotation);

        mRotation.y *= rotationDampening;
    }
}
