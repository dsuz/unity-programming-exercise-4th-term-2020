using UnityEngine;

/// <summary>
/// （テスト的に）ターンを操作するコンポーネント
/// </summary>
public class TurnManagerDriver : MonoBehaviour
{
    public void EndTurn()
    {
        TurnManager.EndTurn();
    }
}
