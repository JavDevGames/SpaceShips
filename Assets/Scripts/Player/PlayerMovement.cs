using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float verticalSpeed;
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
            forward *= speed;
            Vector3 newPos = transform.position + forward;
            transform.position = newPos;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Vector3 forward = transform.forward;
            forward *= -speed;
            Vector3 newPos = transform.position + forward;
            transform.position = newPos;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 right = transform.right;
            right *= -speed;
            Vector3 newPos = transform.position + right;
            transform.position = newPos;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 left = transform.right;
            left *= speed;
            Vector3 newPos = transform.position + left;
            transform.position = newPos;
        }

        if(Input.GetKey(KeyCode.Q))
        {
            Vector3 offset = transform.position;
            offset.y -= verticalSpeed;
            transform.position = offset;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Vector3 offset = transform.position;
            offset.y += verticalSpeed;
            transform.position = offset;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y -= rotationSpeed;            
            transform.rotation = Quaternion.Euler(rotation);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y += rotationSpeed;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
