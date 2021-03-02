using UnityEngine;

/// <summary>
/// TurnManagerDelegate から通知を受け取りたい場合はこれを継承してコンポーネントを作ること。
/// TurnEventSubscriber から TurnManager -> TurnManagerDelegate に変更しただけ。
/// </summary>
public class TurnEventSubscriberDelegate : MonoBehaviour
{
    void OnEnable()
    {
        TurnManagerDelegate.OnEndTurn += OnEndTurn;
        TurnManagerDelegate.OnBeginTurn += OnBeginTurn;
    }

    void OnDisable()
    {
        TurnManagerDelegate.OnEndTurn -= OnEndTurn;
        TurnManagerDelegate.OnBeginTurn += OnBeginTurn;
    }

    public virtual void OnEndTurn()
    {
        Debug.Log($"OnEndTurn #{TurnManagerDelegate.TurnCount.ToString()}");
    }

    public virtual void OnBeginTurn()
    {
        Debug.Log($"OnBeginTurn #{TurnManagerDelegate.TurnCount.ToString()}");
    }
}
