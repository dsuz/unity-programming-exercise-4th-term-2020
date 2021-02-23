using System.Collections;
using UnityEngine;

/// <summary>
/// グリッド移動を制御するコンポーネント。移動させる時は Move 関数を使う。
/// </summary>
public class GridMoveController : MonoBehaviour
{
    /// <summary>移動を妨害するコライダーが所属するレイヤーを指定する</summary>
    [SerializeField] LayerMask m_walkableLayerMask;
    /// <summary>移動中フラグ</summary>
    bool m_isMoving = false;
    /// <summary>Move で指定された目的地を保存する</summary>
    Vector2Int m_destination;

    /// <summary>
    /// オブジェクトが移動中ならば true, そうでない場合は false を返す
    /// </summary>
    public bool IsMoving
    {
        get { return m_isMoving; }
    }

    /// <summary>
    /// 指定された相対座標に滑らかに移動する。指定された座標に移動できない場合は何もしない。
    /// </summary>
    /// <param name="x">移動する X 座標</param>
    /// <param name="y">移動する Y 座標</param>
    /// <param name="moveTime">何秒かけて移動するか</param>
    /// <returns>移動可能な場合は true, 移動できない場合は false</returns>
    public bool Move(int x, int y, float moveTime)
    {
        m_isMoving = false;

        // 指定された方向に移動できるかどうか判定する
        Vector2 destination = (Vector2)this.transform.position + new Vector2(x, y);
        var col = Physics2D.OverlapCircle(destination, .1f, m_walkableLayerMask);

        if (col == null)    // 移動可能
        {
            m_isMoving = true;
            StartCoroutine(MoveAsync(x, y, moveTime));
        }

        return m_isMoving;
    }

    /// <summary>
    /// 指定された相対座標に滑らかに移動する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="moveTime"></param>
    /// <returns></returns>
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

    /// <summary>
    /// コルーチンによる滑らかな移動処理をキャンセルして目的地に瞬間移動させる。
    /// </summary>
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

