using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    ParticleSystem[] m_particleSystems = null;

    void Start()
    {
        m_particleSystems = this.transform.GetComponentsInChildren<ParticleSystem>();
        Explode(Vector3.up * -100f); // 見えない所で一回爆発させて初期化する
    }

    public void Explode(Vector3 position)
    {
        this.transform.position = position;

        foreach(var p in m_particleSystems)
        {
            p.Play();
        }
    }

    public void ExplodeTest(float positionY)
    {
        Explode(Vector3.up * positionY);
    }
}
