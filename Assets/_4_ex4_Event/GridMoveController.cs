using System.Collections;
using UnityEngine;

public class GridMoveController : MonoBehaviour
{
    bool m_isMoving = false;
    Vector2Int m_destination;

    public bool IsMoving
    {
        get { return m_isMoving; }
    }

    public void Move(int x, int y, float moveTime)
    {
        m_isMoving = true;
        StartCoroutine(MoveAsync(x, y, moveTime));
    }

    IEnumerator MoveAsync(int x, int y, float moveTime)
    {
        // 移動先を計算する
        Vector2Int origin = Vector2Int.RoundToInt(this.transform.position);
        m_destination = origin + new Vector2Int(x, y);

        while (Vector2.Distance(this.transform.position, m_destination) > float.Epsilon)
        {
            Vector2 velocity = Vector2.zero;
            this.transform.position = Vector2.MoveTowards(this.transform.position, m_destination, Time.deltaTime / moveTime);
            yield return new WaitForEndOfFrame();
        }
        m_isMoving = false;
    }

    public void Skip()
    {
        if (m_isMoving)
        {
            StopAllCoroutines();
            this.transform.position = (Vector2)m_destination;
            m_isMoving = false;
        }
    }
}

