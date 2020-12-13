using UnityEngine;

public class GameStateMenager : MonoBehaviour
{
    public static GameStateMenager instance;
    public enum State { Play=0, Pause=1, Achievements=2, Description=3, Stats=4}
    public State currentState = State.Play;
    private void Awake()
    {
        instance = this;
    }

    public void SetNewState(int value)
    {
        currentState = (State)value;
    }
}
