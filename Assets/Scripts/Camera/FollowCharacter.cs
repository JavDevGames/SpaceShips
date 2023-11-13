using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public float distance;
    public float offsetY;
    public Vector3 initialPosition;
    public Vector3 initialRotation;
    public InputManager inputManager;
    public float rotationSpeed;
    public float maxPitch;
    public float minPitch;
    public GameObject target;

    private static Vector3 DIR_HELPER;
    private static Vector3 POSITION;
    private static Vector3 TARGET_POSITION;

    private Vector3 mCurrentRotation;

    // Use this for initialization
    void Start()
    {
        POSITION = new Vector3();

        mCurrentRotation = new Vector3();

        InitCameraPosition();
    }

    private void InitCameraPosition()
    {
        DIR_HELPER = target.transform.position - this.transform.position;
        DIR_HELPER.Normalize();

        POSITION = target.transform.position;

        POSITION.x += Mathf.Sin(initialPosition.y) * distance;
        POSITION.z += Mathf.Cos(initialPosition.y) * distance;

        POSITION.y += Mathf.Sin(initialPosition.z) * distance;

        this.transform.position = POSITION;
        this.transform.LookAt(target.transform.position);

        mCurrentRotation = initialRotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        DIR_HELPER = this.transform.forward;

        POSITION = target.transform.position;

        TARGET_POSITION.x = POSITION.x;
        TARGET_POSITION.y = POSITION.y;
        TARGET_POSITION.z = POSITION.z;

        float targetSideRotation = inputManager.GetSideRotation() * rotationSpeed * Time.deltaTime;
        float targetUpRotation = inputManager.GetUpRotation() * rotationSpeed * Time.deltaTime;

        POSITION.y += offsetY;

        POSITION = POSITION + (target.transform.forward * distance);

        Vector3 diff = (POSITION - TARGET_POSITION) * 0.80f;

        this.transform.position = POSITION/* + diff*/;
        this.transform.LookAt(TARGET_POSITION);
    }
}