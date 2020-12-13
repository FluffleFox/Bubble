using UnityEngine;

public class CanvasMovement : MonoBehaviour
{
    public Vector3 outscreenPosition;
    public GameStateMenager.State[] displayState;

    void Update()
    {    
        if (Show())
        {
            transform.Translate(-transform.localPosition * Time.deltaTime * 5.0f, Space.World);
        }
        else
        {
            transform.Translate((outscreenPosition-transform.localPosition) * Time.deltaTime*5.0f, Space.World);
        }
    }

    bool Show()
    {
        for(int i=0; i<displayState.Length; i++)
        {
            if (displayState[i] == GameStateMenager.instance.currentState) return true;
        }
        return false;
    }
}
