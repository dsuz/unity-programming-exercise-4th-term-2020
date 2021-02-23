using UnityEngine;

/// <summary>
/// TurnManager から通知を受け取りたい場合はこれを継承してコンポーネントを作ること。
/// </summary>
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
        Debug.Log($"OnEndTurn #{TurnManager.TurnCount.ToString()}");
    }

    public virtual void OnBeginTurn()
    {
        Debug.Log($"OnBeginTurn #{TurnManager.TurnCount.ToString()}");
    }
}
