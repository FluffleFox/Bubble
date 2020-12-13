using UnityEngine;
using UnityEngine.UI;

public class InformationContainer : MonoBehaviour
{
    // Start is called before the first frame update
    public Text information;
    public static InformationContainer instance;
    void Start()
    {
        instance = this;
    }

    public void SetText(string text)
    {
        information.text = text;
    }
}
