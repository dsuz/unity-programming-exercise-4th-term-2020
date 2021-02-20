using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedPlayerController : MonoBehaviour
{
    [SerializeField] float m_moveTime = 1f;
    GridMoveController m_gridMove = null;

    void Start()
    {
        m_gridMove = GetComponent<GridMoveController>();
    }

    void Update()
    {
        if (m_gridMove.IsMoving)
        {
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            // 入力された方向に移動可能か判定する
            Vector2 destination = (Vector2)this.transform.position + new Vector2(h, v);
            var col = Physics2D.OverlapCircle(destination, .1f);
            
            if (col == null)    // 移動可能
            {
                m_gridMove.Move((int)h, (int)v, m_moveTime);
                TurnManager.EndTurn();
            }
        }
    }
}
