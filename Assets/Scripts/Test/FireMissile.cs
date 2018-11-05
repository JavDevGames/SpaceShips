using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissile : MonoBehaviour
{
    public int barrageCount;
    public GameObject missilePrefab;
    public GameObject[] targets;

    private int mCurTarget;
    private List<MissileGuidance> mMissiles;

    public AnimationCurve[] animCurves;
    
    // Start is called before the first frame update
    void Start()
    {
        mMissiles = new List<MissileGuidance>();
        mCurTarget = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < barrageCount; ++i)
            {
                SpawnMissile();
            }
        }
    }

    private void SpawnMissile()
    {
        GameObject missileParent = new GameObject();
        var missile = Instantiate(missilePrefab);

        missile.transform.parent = missileParent.transform;
        missileParent.transform.position = new Vector3();

        var missileGuidance = missileParent.AddComponent<MissileGuidance>();

        GameObject target = targets[mCurTarget];
        Vector3 spawnPosition = RandomPointInSphere(missile.transform.position, 0.5f);
        missileParent.transform.position = this.transform.position + spawnPosition;
        missileGuidance.Init(missileParent, missile, target, spawnPosition, animCurves);

        mCurTarget = (mCurTarget + 1) % targets.Length;

        mMissiles.Add(missileGuidance);
    }
    private Vector3 RandomPointInSphere(Vector3 center, float radius)
    {
        Vector3 pos = new Vector3
        {
            x = center.x + (UnityEngine.Random.Range(-radius, radius)),
            y = center.y + (UnityEngine.Random.Range(-radius, radius)),
            z = center.z + (UnityEngine.Random.Range(-radius, radius))
        };

        return pos;
    }
}
