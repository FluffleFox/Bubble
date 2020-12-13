using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    public Image image;
    public Text tittle;
    public Text description;
    string fullDescription;
    public void Setup(Achievement data)
    {
        tittle.text = data.tittle;
        transform.localScale = Vector3.one;
        if(data.CheeckUnlock())
        { 
            image.sprite = data.unlockedIcon;
            description.text = data.smallDescriptionWhenUnlocked;
            fullDescription = data.fullDescriptionWhenUnlocked;
        }
        else
        {
            image.sprite = data.lockedIcon;
            description.text = data.smallDescriptionWhenLocked;
            fullDescription = data.fullDescriptionWhenLocked;
        }
    }

    //Wywołanie przycisku albo rezygnacja z tego...
    public void SetInformation()
    {
        GameStateMenager.instance.SetNewState(3);
        InformationContainer.instance.SetText(fullDescription);
    }

}
