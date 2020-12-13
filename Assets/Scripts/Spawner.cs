using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public GameObject bubble;
    public float interval = 0.1f;
    float reload = 0;

    [HideInInspector]
    public float screenRatio;

    List<GameObject> readyToUse = new List<GameObject>();
    private void Awake()
    {
        instance = this;
        screenRatio = (float)Screen.height / (float)Screen.width;
    }
    void Update()
    {
        if (reload <= 0)
        {
            float z = Random.Range(5.0f, 15.0f);
            float x = Random.Range(-0.57735f * z, 0.57735f * z);
            float y = -0.57735f * z * screenRatio-0.5f;

            GameObject GO;
            if (readyToUse.Count == 0)
            {
                GO = (GameObject)Instantiate(bubble, new Vector3(x, y, z), Quaternion.identity);
            }
            else
            {
                GO = readyToUse[0];
                GO.transform.position = new Vector3(x, y, z);
                GO.GetComponent<BubbleMovement>().enabled = true;
                readyToUse.RemoveAt(0);
            }
            float r = Random.Range(0.0f, 1.0f);
            float g = Random.Range(0.0f, 1.0f);
            float b = Random.Range(0.0f, 1.0f);
            Vector3 color = new Vector3(r, g, b);
            color.Normalize();
            color /= Mathf.Max(color.x, color.y, color.z);
            GO.GetComponent<Renderer>().material.color = new Color(color.x,color.y,color.z);
            reload = interval;
        }
        else
        {
            reload -= Time.deltaTime;
        }
    }

    public void AddToList(GameObject item)
    {
        if (!readyToUse.Contains(item))
        { readyToUse.Add(item); }
    }

    public void UpdateInterval(float newValue)
    {
        interval = newValue;
    }
}
