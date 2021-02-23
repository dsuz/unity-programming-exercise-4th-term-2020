using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ターンを画面に表示する
/// </summary>
public class TurnDisplay : TurnEventSubscriber
{
    [SerializeField] Text m_turnText = null;

    public override void OnBeginTurn()
    {
        m_turnText.text = TurnManager.TurnCount.ToString("D10");
    }

    public override void OnEndTurn()
    {
    }
}
