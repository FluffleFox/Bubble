using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMovement : MonoBehaviour
{
    public Vector3 windSpeed;
    public float limes = 20;

    void Update()
    {
        transform.Translate(windSpeed * Time.deltaTime, Space.World);
        if (transform.localPosition.x > limes)
        {
            transform.localPosition = new Vector3(-limes, transform.position.y, transform.position.z);
        }
    }
}
