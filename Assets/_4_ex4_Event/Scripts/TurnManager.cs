using System;

public class TurnManager
{
    public static event Action OnBeginTurn;
    public static event Action OnEndTurn;
    static bool m_isTurnStarted = false;

    public static void BeginTurn()
    {
        OnBeginTurn?.Invoke();
        m_isTurnStarted = true;
    }

    public static void EndTurn()
    {
        if (!m_isTurnStarted)
        {
            BeginTurn();
        }

        OnEndTurn?.Invoke();
        m_isTurnStarted = false;
    }
}
