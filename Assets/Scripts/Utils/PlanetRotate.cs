using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    public GameObject planet;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        planet.transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
