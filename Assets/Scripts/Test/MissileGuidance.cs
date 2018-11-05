using System;
using System.Collections.Generic;
using UnityEngine;

public class MissileGuidance : MonoBehaviour
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
    private AnimationCurve[] mAnimCurves;
    private Vector3 mCurveIndeces;
    private float mDuration;

    public void Init(GameObject missileParent, GameObject missile, GameObject target, Vector3 spawnPosition, AnimationCurve[] animCurves)
    {
        mSpawnOffset = spawnPosition;
        mMissile = missile;
        mInternalTime = 0;
        mTarget = target;
        mSpeed = 8.0f + UnityEngine.Random.Range(-2.0f, 2.0f);
        mMissileParent = missileParent;
        mTargetOffset = new Vector3()
        {
            x = UnityEngine.Random.Range(-0.4f, 0.4f),
            y = UnityEngine.Random.Range(-0.4f, 0.4f),
            z = UnityEngine.Random.Range(-0.4f, 0.4f)
        };

        mAnimCurves = animCurves;
        mCurveIndeces = new Vector3()
        {
            x = (int)UnityEngine.Random.Range(0, mAnimCurves.Length - 1),
            y = (int)UnityEngine.Random.Range(0, mAnimCurves.Length - 1),
            z = (int)UnityEngine.Random.Range(0, mAnimCurves.Length - 1)
        };
         
        mDuration = 3.0f;
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

            if (dist < 1.0f)
            {
                Destroy(gameObject, 0.1f);
            }
        }
        else
        {
            /*
            dist = Mathf.Clamp(dist, 4, 7);

            Vector3 parentPos = mMissileParent.transform.position;
            Vector3 newPos = new Vector3();
            mInternalTime += (Time.deltaTime * dist);

            float sinDist = Mathf.Sin(((float)(Math.Cos(mInternalTime) + dist * Math.Sin(mInternalTime * 0.25f))));
            float yWave = Mathf.Sin(((float)(dist * Math.Sin(mInternalTime * 0.25f))));
            newPos.x = sinDist * 1.2f;
            newPos.y = yWave * 0.8f;
            newPos.z = sinDist * 0.6f;
            */

            mInternalTime += (Time.deltaTime);

            AnimationCurve curCurveX = mAnimCurves[(int) mCurveIndeces.x];
            AnimationCurve curCurveY = mAnimCurves[(int) mCurveIndeces.y];
            AnimationCurve curCurveZ = mAnimCurves[(int) mCurveIndeces.z];

            float progress = mInternalTime / mDuration;
            progress = Mathf.Clamp(progress, 0.0f, 1.0f);

            Vector3 newPos = new Vector3();

            newPos.x = curCurveX.Evaluate(progress);
            newPos.y = curCurveY.Evaluate(progress);
            newPos.z = curCurveZ.Evaluate(progress);

            mMissile.transform.localPosition = newPos;

            if(progress >= 1.0f)
            {
                mInternalTime = 0;

                mCurveIndeces.x = UnityEngine.Random.Range(0, mAnimCurves.Length - 1);
                mCurveIndeces.y = UnityEngine.Random.Range(0, mAnimCurves.Length - 1);
                mCurveIndeces.z = UnityEngine.Random.Range(0, mAnimCurves.Length - 1);
            }
        }
    }
}
