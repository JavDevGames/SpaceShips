using UnityEngine;

public class BaseInputPoller
{
    protected float mForwardMovement;
    protected float mSideRotation;
    protected float mUpRotation;
    protected float mAttack;

    public BaseInputPoller()
    {

    }

    virtual public void Update()
    {
        //override this
    }

    public float GetForwardMovement()
    {
        return mForwardMovement;
    }

    public float GetAttack()
    {
        return mAttack;
    }

    public float GetSideRotation()
    {
        return mSideRotation;
    }

    public float GetUpRotation()
    {
        return mUpRotation;
    }

    public void Reset()
    {
        mForwardMovement = 0;
        mAttack = 0;
        mSideRotation = 0;
        mUpRotation = 0;
    }
}