using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ESC が入力された時、Pause/Resume を制御する
/// </summary>
public class PauseController : MonoBehaviour
{
    [SerializeField] UnityEvent m_onPause;
    [SerializeField] UnityEvent m_onResume;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (PauseManager.IsPaused)
            {
                Debug.Log("Resume");
                PauseManager.Resume();
                m_onResume?.Invoke();
            }
            else
            {
                Debug.Log("Pause");
                PauseManager.Pause();
                m_onPause?.Invoke();
            }
        }
    }
}
