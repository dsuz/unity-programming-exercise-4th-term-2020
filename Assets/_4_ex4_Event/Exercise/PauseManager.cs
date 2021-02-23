/// <summary>
/// ポーズを管理するクラス
/// </summary>
public class PauseManager
{
    static bool m_isPaused = false;

    public static bool IsPaused { get { return m_isPaused; } }

    public static void Pause()
    {
        m_isPaused = true;
    }

    public static void Resume()
    {
        m_isPaused = false;
    }
}
