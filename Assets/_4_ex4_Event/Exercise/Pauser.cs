using UnityEngine;

/// <summary>
/// Pause された時、Rigidbody の動きを止める
/// Resume された時、Rigidbody を動かす
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Pauser : MonoBehaviour
{
    Rigidbody m_rb = null;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }
}
