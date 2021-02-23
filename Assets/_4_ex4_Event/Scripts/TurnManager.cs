using System;

/// <summary>
/// ターン管理をするクラス。
/// GameObject にはアタッチしない。
/// </summary>
public class TurnManager
{
    public static event Action OnBeginTurn;
    public static event Action OnEndTurn;
    static bool m_isTurnStarted = false;
    /// <summary>現在のターン数</summary>
    static int m_turnCount = 1;

    /// <summary>
    /// 現在のターン数
    /// </summary>
    public static int TurnCount
    {
        get { return m_turnCount; }
    }

    /// <summary>
    /// ターン開始時に呼ぶ
    /// </summary>
    public static void BeginTurn()
    {
        OnBeginTurn?.Invoke();
        m_isTurnStarted = true;
    }

    /// <summary>
    /// ターン終了時に呼ぶ
    /// </summary>
    public static void EndTurn()
    {
        // ターンを開始せずに終了した場合はまず強制的にターンを開始する
        if (!m_isTurnStarted)
        {
            BeginTurn();
        }

        OnEndTurn?.Invoke();
        m_isTurnStarted = false;
    }
}
