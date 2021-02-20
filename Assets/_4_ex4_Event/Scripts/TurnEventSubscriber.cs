using UnityEngine;

public class TurnEventSubscriber : MonoBehaviour
{
    void OnEnable()
    {
        TurnManager.OnEndTurn += OnEndTurn;
        TurnManager.OnBeginTurn += OnBeginTurn;
    }

    void OnDisable()
    {
        TurnManager.OnEndTurn -= OnEndTurn;
        TurnManager.OnBeginTurn += OnBeginTurn;
    }

    public virtual void OnEndTurn()
    {
        Debug.Log("OnEndTurn");
    }

    public virtual void OnBeginTurn()
    {
        Debug.Log("OnBeginTurn");
    }
}
