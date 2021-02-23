using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurnBasedEnemyController : TurnEventSubscriber
{
    [SerializeField] float m_playerSearchRangeRadius = 5f;
    [SerializeField] LayerMask m_walkableLyerMask;
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

        GameObject player = SearchPlayer();
        int x = 0;
        int y = 0;

        if (player)
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
        else
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);
        }

        // 移動可能か判定する
        Vector2 destination = (Vector2)this.transform.position + new Vector2(x, y);
        var col = Physics2D.OverlapCircle(destination, .1f, m_walkableLyerMask);

        if (col == null)    // 移動可能
        {
            m_gridMove.Move(x, y, m_moveTime);
        }
    }

    GameObject SearchPlayer()
    {
        return Physics2D.OverlapCircleAll(this.transform.position, m_playerSearchRangeRadius).ToList()
            .Where(col => col.gameObject.CompareTag("Player")).FirstOrDefault()?.gameObject;
    }
}
