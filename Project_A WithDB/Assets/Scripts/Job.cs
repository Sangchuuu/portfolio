using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Job : MonoBehaviour
{
    [SerializeField]
    Button[] m_btnsJob = new Button[4];
    // 0 = SwordMan, 1 = Archer, 2 = Magician, 3 = Acolyte
    PlayerStatus playerStatus;
    
    public void ButtonClickEvent()
    {
        m_btnsJob[0].onClick.AddListener(delegate
        {
            if(playerStatus.IsGetJob == true)
                playerStatus.m_ePlayerJob = PlayerStatus.E_Player_Job.SwordMan;
        });
        m_btnsJob[1].onClick.AddListener(delegate
        {
            if (playerStatus.IsGetJob == true)
                playerStatus.m_ePlayerJob = PlayerStatus.E_Player_Job.Archer;
        });
        m_btnsJob[2].onClick.AddListener(delegate
        {
            if (playerStatus.IsGetJob == true)
                playerStatus.m_ePlayerJob = PlayerStatus.E_Player_Job.Magician;
        });
        m_btnsJob[3].onClick.AddListener(delegate
        {
            if (playerStatus.IsGetJob == true)
                playerStatus.m_ePlayerJob = PlayerStatus.E_Player_Job.Acolyte;
        });
    }
    
    void Start()
    {
        playerStatus = GameManager.instance.Player.GetComponent<PlayerStatus>();
        ButtonClickEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
