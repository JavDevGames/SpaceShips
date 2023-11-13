using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum InputPollerType
    {
        keyboard,
        gamepad
    };

    public InputPollerType inputType;

    private BaseInputPoller mActivePoller;

    // Use this for initialization
    void Start()
    {
        if (inputType == InputPollerType.keyboard)
        {
            mActivePoller = new KeyBoardInputPoller();
        }
        else if (inputType == InputPollerType.gamepad)
        {
            //Removed gamepad implementation for showcase
        }
    }

    void Update()
    {
        Reset();

        mActivePoller.Update();
    }

    public float GetForwardMovement()
    {
        return mActivePoller.GetForwardMovement();
    }

    public float GetAttack()
    {
        return mActivePoller.GetAttack();
    }

    public float GetSideRotation()
    {
        return mActivePoller.GetSideRotation();
    }

    public float GetUpRotation()
    {
        return mActivePoller.GetUpRotation();
    }

    public void Reset()
    {
        mActivePoller.Reset();
    }
}