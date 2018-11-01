using System;
using System.Collections.Generic;
using UnityEngine;

public class MissileGuidance
{
    private Vector3 mSpawnOffset;
    private GameObject mMissile;
    private float mInternalTime;
    private GameObject mTarget;
    private float mSpeed;
    private Vector3 deltaVelocity;
    private Vector3 mRandomMovement;
    private List<Vector3> mMidPoints;
    private Vector3 mMovementDelta;
    private Vector3 mLastTargetPos;
    private int mCurIdx;
    private GameObject mMissileParent;
    private Vector3 mTargetOffset;

    public void Init(GameObject missileParent, GameObject missile, GameObject target, Vector3 spawnPosition)
    {
        mSpawnOffset = spawnPosition;
        mMissile = missile;
        mInternalTime = Time.time * UnityEngine.Random.Range(-10.0f, 10.0f);
        mTarget = target;
        mSpeed = 8.0f;
        mMissileParent = missileParent;
        mTargetOffset = new Vector3()
        {
            x = UnityEngine.Random.Range(-0.4f, 0.4f),
            y = UnityEngine.Random.Range(-0.4f, 0.4f),
            z = UnityEngine.Random.Range(-0.4f, 0.4f)
        };
    }


    // Update is called once per frame
    public void Update()
    {
        UpdateParent();
        UpdateMesh();
    }

    private void UpdateParent()
    { 
        Vector3 curPos = mMissileParent.transform.position;
        Vector3 targetPos = mTarget.transform.position + mTargetOffset;

        mMissileParent.transform.position = Vector3.MoveTowards(curPos, targetPos, Time.deltaTime * mSpeed);

        var dir = (targetPos - curPos);
        float dist = dir.magnitude;
        dir.Normalize();

        curPos = mMissileParent.transform.position;
        Vector3 lookAt = new Vector3()
        {
            x = curPos.x + (dir.x * 2.0f),
            y = curPos.y + (dir.y * 2.0f),
            z = curPos.z + (dir.z * 2.0f)
        };

        mMissileParent.transform.LookAt(lookAt);
    }

    private void UpdateMesh()
    {
        Vector3 dir = mTarget.transform.position - mMissileParent.transform.position;
        float dist = dir.magnitude;

        if (dist < 4.0f)
        {
            Vector3 newPos = mMissile.transform.localPosition;
            newPos *= 0.9f;

            mMissile.transform.localPosition = newPos;
        }
        else
        {
            Vector3 parentPos = mMissileParent.transform.position;
            Vector3 newPos = new Vector3();
            mInternalTime += (Time.deltaTime * dist);

            float sinDist = Mathf.Sin(((float)(Math.Cos(mInternalTime) + Math.Sin(mInternalTime * 0.25f))));
            newPos.x = sinDist * 1.2f;
            newPos.y = sinDist * 0.8f;
            //newPos.z = sinDist * 1.2f;
            mMissile.transform.localPosition = newPos;
        }
    }
}
