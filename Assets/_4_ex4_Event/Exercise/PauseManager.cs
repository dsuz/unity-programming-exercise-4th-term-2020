using System;

public class PauseManager
{
    public static event Action OnPause;
    public static event Action OnResume;
    static bool m_isPaused = false;

    public static bool IsPaused { get { return m_isPaused; } }

    public static void Pause()
    {
        m_isPaused = true;
        OnPause?.Invoke();
    }

    public static void Resume()
    {
        m_isPaused = false;
        OnResume?.Invoke();
    }
}
