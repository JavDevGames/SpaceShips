using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoveScript : MonoBehaviour
{
    private Vector3 mStart;
    private Vector3 mEnd;
    private float mStartTime;
    private float mDuration;

    // Start is called before the first frame update
    void Start()
    {
        mStart = gameObject.transform.position;
        mEnd = mStart;
        mEnd.x -= 10f;
        mStartTime = Time.time;
        mDuration = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float progress = (Time.time - mStartTime) / mDuration;
        Vector3 newPos;
        newPos.x = Mathf.Lerp(mStart.x, mEnd.x, progress);
        newPos.y = Mathf.Lerp(mStart.y, mEnd.y, progress);
        newPos.z = Mathf.Lerp(mStart.z, mEnd.z, progress);

        transform.position = newPos;

        if(progress >= 1)
        {
            SwapDestinations();
            mStartTime = Time.time;
        }
    }

    private void SwapDestinations()
    {
        Vector3 temp = mStart;
        mStart = mEnd;
        mEnd = temp;
    }
}
