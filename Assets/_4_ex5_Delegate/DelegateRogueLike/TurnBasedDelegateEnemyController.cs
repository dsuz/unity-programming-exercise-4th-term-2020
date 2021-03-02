using UnityEngine;
using System.Linq;

/// <summary>
/// 敵を制御するコンポーネント
/// </summary>
[RequireComponent(typeof(GridMoveController))]
public class TurnBasedDelegateEnemyController : TurnEventSubscriberDelegate
{
    /// <summary>この範囲にプレイヤーがいる場合、プレイヤーを見つけることができる</summary>
    [SerializeField] float m_playerSearchRangeRadius = 5f;
    /// <summary>１ターンで動くのにかける時間（単位: 秒）</summary>
    [SerializeField] float m_moveTime = 1f;
    GridMoveController m_gridMove = null;

    void Start()
    {
        m_gridMove = GetComponent<GridMoveController>();
    }

    public override void OnBeginTurn()
    {
        if (m_gridMove.IsMoving)
        {
            m_gridMove.Skip();
        }
    }

    public override void OnEndTurn()
    {
        // 移動方向を決定する
        GameObject player = SearchPlayer();
        int x = 0;
        int y = 0;

        if (player) // プレイヤーが見つかった場合はその方向に移動する
        {
            if (Mathf.Abs(player.transform.position.x - this.transform.position.x) > float.Epsilon)
            {
                x = player.transform.position.x > this.transform.position.x ? 1 : -1;
            }

            if (Mathf.Abs(player.transform.position.y - this.transform.position.y) > float.Epsilon)
            {
                y = player.transform.position.y > this.transform.position.y ? 1 : -1;
            }
        }
        else // プレイヤーが見つからない場合はランダムに移動する
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);
        }

        m_gridMove.Move(x, y, m_moveTime);
    }

    /// <summary>
    /// 索敵範囲内からプレイヤーを見つける
    /// </summary>
    /// <returns>プレイヤーが見つかったらそのオブジェクトを返す。見つからない場合は null を返す。</returns>
    GameObject SearchPlayer()
    {
        return Physics2D.OverlapCircleAll(this.transform.position, m_playerSearchRangeRadius).ToList()
            .Where(col => col.gameObject.CompareTag("Player")).FirstOrDefault()?.gameObject;
    }
}
