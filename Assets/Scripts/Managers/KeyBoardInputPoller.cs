using UnityEngine;

public class KeyBoardInputPoller : BaseInputPoller
{
    public KeyBoardInputPoller() : base()
    {

    }

    override public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            mForwardMovement = 1.0f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            mForwardMovement = -1.0f;
        }

        //Usually used for camera controls
        if (Input.GetKey(KeyCode.RightArrow))
        {
            mSideRotation = 1.0f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            mSideRotation = -1.0f;
        }

        //Usually used for camera controls
        if (Input.GetKey(KeyCode.UpArrow))
        {
            mUpRotation = 1.0f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            mUpRotation = -1.0f;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            mAttack = 1.0f;
        }
    }
}