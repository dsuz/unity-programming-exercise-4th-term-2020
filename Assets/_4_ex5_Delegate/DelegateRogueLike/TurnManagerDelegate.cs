/// <summary>
/// ターン管理をするクラス。
/// GameObject にはアタッチしない。
/// ex4 イベントで作った TurnManager を delegate で書き変えたもの
/// </summary>
public class TurnManagerDelegate
{
    // 引数なしのデリゲート型を宣言する
    public delegate void DelegateMethod();

    public static DelegateMethod OnBeginTurn;
    public static DelegateMethod OnEndTurn;
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
