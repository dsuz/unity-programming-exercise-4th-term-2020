 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    ParticleSystem[] m_particles = null;
    [SerializeField] AudioClip m_sfx = null;
    AudioSource m_audio = null;
    Animator m_anim = null;

    void Start()
    {
        m_particles = GetComponentsInChildren<ParticleSystem>();
        m_audio = GetComponent<AudioSource>();
        m_anim = GetComponent<Animator>();
    }

    public void PlayParticle()
    {
        m_audio.PlayOneShot(m_sfx);

        foreach (var p in m_particles)
        {
            p.Play();
        }
    }
    
    public void PlayAnimation(string stateName)
    {
        m_anim.Play(stateName);
    }
}
