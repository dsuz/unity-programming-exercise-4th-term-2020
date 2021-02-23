using UnityEngine;

/// <summary>
/// ターンベースでのプレイヤー移動を制御するコンポーネント
/// </summary>
[RequireComponent(typeof(GridMoveController))]
public class TurnBasedPlayerController : MonoBehaviour
{
    /// <summary>１ターンで動くのにかける時間（単位: 秒）</summary>
    [SerializeField] float m_moveTime = 1f;
    GridMoveController m_gridMove = null;

    void Start()
    {
        m_gridMove = GetComponent<GridMoveController>();
    }

    void Update()
    {
        if (m_gridMove.IsMoving)    // 動いている最中は何もしない
        {
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            // 移動可能ならば移動して、ターンを進める
            if (m_gridMove.Move((int)h, (int)v, m_moveTime))
            {
                TurnManager.EndTurn();
            }
        }
    }
}
