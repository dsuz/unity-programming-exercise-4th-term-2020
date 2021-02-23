using UnityEngine;

/// <summary>
/// Pause された時、Rigidbody の動きを止める
/// Resume された時、Rigidbody を動かす
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Pauser : MonoBehaviour
{
    Rigidbody m_rb = null;
    Vector3 m_angular;
    Vector3 m_velo;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        PauseManager.OnPause += OnPause;
        PauseManager.OnResume += OnResume;
    }

    void OnDisable()
    {
        PauseManager.OnPause += OnPause;
        PauseManager.OnResume += OnResume;
    }

    public void OnPause()
    {
        m_angular = m_rb.angularVelocity;
        m_velo = m_rb.velocity;
        m_rb.Sleep();
    }

    public void OnResume()
    {
        m_rb.WakeUp();
        m_rb.AddForce(m_velo, ForceMode.VelocityChange);
        m_rb.AddTorque(m_angular, ForceMode.VelocityChange);
    }
}
