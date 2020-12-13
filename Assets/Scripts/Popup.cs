using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public static Popup instance;
    public Vector3 outscreenPosition;
    Vector3 screenPosition = Vector3.zero;
    
    public List<Achievement> listToDisplay = new List<Achievement>();
    public AchievementDisplay display;
    bool ready = false;
    private void Awake()
    {
        instance = this;
    }

    public void DisplayPopup(Achievement data)
    {
        listToDisplay.Add(data);
        StartCoroutine(Display());
    }

    private void Update()
    {
        if(listToDisplay.Count>0 && ready)
        {
            listToDisplay.RemoveAt(0);
            if (listToDisplay.Count > 0)
            {
                StartCoroutine(Display());
            }
        }
    }

    IEnumerator Display()
    {
        ready = false;
        display.Setup(listToDisplay[0]);
        float tmptime = 0;
        while (tmptime<1.0f)
        {
            transform.Translate(-transform.localPosition * Time.deltaTime * 5.0f, Space.World);
            tmptime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(5.0f);
        tmptime = 0.0f;
        while (tmptime<2.0f)
        {
            transform.Translate((outscreenPosition - transform.localPosition) * Time.deltaTime * 5.0f, Space.World);
            tmptime += Time.deltaTime;
            yield return null;
        }
        ready = true;
    }
}
