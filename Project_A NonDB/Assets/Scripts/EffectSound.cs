using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : MonoBehaviour
{
    public AudioClip m_audioMove;
    public AudioClip m_audioAttack;
    public AudioClip m_audioHit;

    private AudioSource m_AudioSource;

    public float m_fTime = 0.3f;
    public static EffectSound instance = null;


    private void Awake()
    {
        m_AudioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void PlaySound(string state)
    {
        switch (state)
        {
            case "Walk":
                m_fTime = m_fTime - Time.deltaTime;
                if(m_fTime <= 0)
                {
                    m_AudioSource.clip = m_audioMove;
                    m_AudioSource.Play();
                    m_fTime = 0.55f;
                }

                break;
            case "Attack":
                m_AudioSource.clip = m_audioAttack;
                m_AudioSource.Play();
                break;

        }

    }

    public void MonsterSound(string state)
    {
        switch (state)
        {
            case "Hit":
                m_AudioSource.clip = m_audioHit;
                m_AudioSource.Play();
                break;
        }

    }
}
