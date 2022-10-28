using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerUi : MonoBehaviour
{

    [SerializeField]
    int[] m_nStatus = new int[6];
    [SerializeField]
    Image m_imgPlayerHpBar;
    [SerializeField]
    Image m_imgPlayerMpBar;
    [SerializeField]
    Text m_txtPlayerGold;
    [SerializeField]
    Text m_txtBaseLevel;
    [SerializeField]
    Text m_txtJobLevel;
    [SerializeField]
    Text m_txtPlayerJob;
    [SerializeField]
    Text m_txtRemainPoint;
    [SerializeField]
    Text[] m_txtApplyStatus = new Text[14];
    [SerializeField]
    Text[] m_txtPlayerStatus = new Text[6];
    [SerializeField]
    Button[] m_btnStatusPlus = new Button[6];
    [SerializeField]
    Button[] m_btnStatusMinus = new Button[6];
    [SerializeField]
    Button m_btnApply;
    [SerializeField]
    Button m_btnStart;
    [SerializeField]
    Button m_btnAtk;

    [SerializeField]
    Text m_txtMonsterName;
    [SerializeField]
    Text m_txtMonsterLevel;
    [SerializeField]
    Image m_igmMonsterHpBar;
    
    
    [SerializeField]
    GameObject m_objShop;
    [SerializeField]
    GameObject m_objJob;
    [SerializeField]
    GameObject m_objMonsterInfo;
    [SerializeField]
    GameObject m_objInventory;
    [SerializeField]
    GameObject m_objCharInfo;
    [SerializeField]
    GameObject m_objController;


    public GameObject m_objMonster;
    public GameObject m_objPlayer;
    public GameObject m_objPcam;

    public bool m_bIsStart = false;
    public bool m_bIsAttackButtonClick;
    public bool m_bIsSetButtonClick = false;
    public bool m_bIsStartButtonClick = false;

    PlayerStatus m_PlayerStatus;
    ApplyStatus m_ApplyStatus;

    public GameObject Shop { get => m_objShop; set => m_objShop = value; }
    public GameObject Job { get => m_objJob; set => m_objJob = value; }
    public GameObject Controller { get => m_objController; set => m_objController = value; }
    public GameObject MonsterInfo { get => m_objMonsterInfo; set => m_objMonsterInfo = value; }
    public bool AttackButtonclick { get => m_bIsAttackButtonClick; set => m_bIsAttackButtonClick = value; }

    public static PlayerUi instance = null;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
            DontDestroyOnLoad(this);
    }
    public void PlayerAttack() 
    {
        m_btnAtk.onClick.AddListener(delegate
        {
            m_bIsAttackButtonClick = true;
            m_objPlayer.GetComponent<Movement>().IsMoving = true;
        });
    }
    public void SetMonsterInfo()
    {
        if (m_objMonster != null)
        {
            m_objMonsterInfo.SetActive(true);
            MonsterStatus monsterstat = m_objMonster.GetComponent<MonsterAi>().MonStat;
            m_txtMonsterName.text = monsterstat.Name;
            m_txtMonsterLevel.text = monsterstat.Level.ToString();
            m_igmMonsterHpBar.fillAmount = ((float)m_objMonster.GetComponent<MonsterAi>().CurHp / (float)monsterstat.MaxHp);
            if(m_objMonster.activeSelf == false)
            {
                m_objMonster = null;
                m_objMonsterInfo.SetActive(false);
            }
        }
 
        
    }    
    public void SetStatus()
    {
        for (int i = 0; i < m_nStatus.Length; i++)
        {
            m_nStatus[i] = m_PlayerStatus.Stats[i];
        }
    }
    public void ApplyApplyStatus()
    {
        m_txtApplyStatus[0].text = m_ApplyStatus.MaxHp.ToString();
        m_txtApplyStatus[1].text = m_ApplyStatus.Atk.ToString();
        m_txtApplyStatus[2].text = m_ApplyStatus.Matk.ToString();
        m_txtApplyStatus[3].text = m_ApplyStatus.SAtk.ToString();
        m_txtApplyStatus[4].text = m_ApplyStatus.SMAtk.ToString();
        m_txtApplyStatus[5].text = m_ApplyStatus.HpRecovery.ToString();
        m_txtApplyStatus[6].text = m_ApplyStatus.Hit.ToString();
        m_txtApplyStatus[7].text = m_ApplyStatus.MaxSp.ToString();
        m_txtApplyStatus[8].text = m_ApplyStatus.Def.ToString();
        m_txtApplyStatus[9].text = m_ApplyStatus.Mdef.ToString();
        m_txtApplyStatus[10].text = m_ApplyStatus.SDef.ToString();
        m_txtApplyStatus[11].text = m_ApplyStatus.SMDef.ToString();
        m_txtApplyStatus[12].text = m_ApplyStatus.SpRecovery.ToString();
        m_txtApplyStatus[13].text = m_ApplyStatus.Flee.ToString();
    }
    public void SwitchCharInfo()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (m_objCharInfo.activeSelf == true)
                m_objCharInfo.SetActive(false);
            else
                m_objCharInfo.SetActive(true);
        }
    }
    public void SwitchInven()
    {
       if(Input.GetKeyDown(KeyCode.I))
        {
            if (m_objInventory.activeSelf == true)
                m_objInventory.SetActive(false);
            else
                m_objInventory.SetActive(true);
        }
    }
    public void PlayerStatBar()
    {
        m_imgPlayerHpBar.fillAmount = (float)m_ApplyStatus.CurHp / (float)m_ApplyStatus.MaxHp;
        m_imgPlayerMpBar.fillAmount = (float)m_ApplyStatus.CurSp / (float)m_ApplyStatus.MaxSp;
    }
    public void PlayerBaseLevel()
    {
        m_txtBaseLevel.text = m_PlayerStatus.CurBaseLevel.ToString();
    }
    public void PlayerJobLevel()
    {
        m_txtJobLevel.text = m_PlayerStatus.CurJobLevel.ToString();
    }
    public void ApplyPlayerStatus()
    {
        for (int i = 0; i < m_txtPlayerStatus.Length; i++)
        {
            //m_txtPlayerStatus[i].text = m_nStatus[i].ToString();
            m_txtPlayerStatus[i].text = m_PlayerStatus.Stats[i].ToString();
        }
        m_txtRemainPoint.text = m_PlayerStatus.StatPoint.ToString();
    }
    public void PlayerJobText()
    {
        m_txtPlayerJob.text = m_PlayerStatus.m_ePlayerJob.ToString();
    }
    public void PlayerGold()
    {
        m_txtPlayerGold.text = "Gold : " + m_PlayerStatus.Money.ToString();
    }
    public void StatPlus()
    {
        for(int i = 0; i < m_btnStatusPlus.Length; i++)
        {
            int n = i;
            m_btnStatusPlus[n].onClick.AddListener 
                (delegate 
                    {
                        if (m_PlayerStatus.StatPoint > 0)
                        {
                            m_nStatus[n]++;
                            m_PlayerStatus.Stats[n]++;
                            m_PlayerStatus.StatPoint--;
                            m_txtPlayerStatus[n].text = m_PlayerStatus.Stats[n].ToString();
                        }
                    }
                );
        }
    }
    public void StatMinus()
    {
        for (int i = 0; i < m_btnStatusMinus.Length; i++)
        {
            int n = i;
            m_btnStatusMinus[n].onClick.AddListener
                (delegate 
                    {
                        if (m_nStatus[n] > 1)
                        {
                            m_nStatus[n]--;
                            m_PlayerStatus.Stats[n]--;
                            m_PlayerStatus.StatPoint++;
                            m_txtPlayerStatus[n].text = m_PlayerStatus.Stats[n].ToString();
                        }
                    }
                );
        }
    }
    public void StatApply()
    {
        m_btnApply.onClick.AddListener
            (delegate
                {
                    for (int i = 0; i < m_nStatus.Length; i++)
                    {
                        m_PlayerStatus.Stats[i] = m_nStatus[i];
                    };
                    m_PlayerStatus.ApplyStats();
                    m_PlayerStatus.CurState();
                    m_bIsSetButtonClick = true;
                }
            );
    }
    public void StartGame()
    {
        m_btnStart.onClick.AddListener
            (delegate
                {
                    m_bIsStart = true;
                }
            );
    }
  

    void Start()
    {
        StartGame();
        PlayerAttack();

    }

    private void OnValidate()
    {
        if (m_objPlayer != null)
        {
            SetStatus();
            ApplyPlayerStatus();
        }
    }
    // Update is called once per frame
    void Update()
    {


        if(m_objPcam == null)
        {
            m_objPcam = GameManager.instance.PlayerCamera;
        }
        else
        {
            this.GetComponent<Canvas>().worldCamera = m_objPcam.GetComponent<Camera>();
            this.GetComponent<Canvas>().planeDistance = 1;
        }
        if (m_objPlayer != null)
        {
            m_ApplyStatus = m_PlayerStatus.AplyStat;
            PlayerGold();
            PlayerJobText();
            PlayerBaseLevel();
            PlayerJobLevel();
            PlayerStatBar();
            ApplyPlayerStatus();
            ApplyApplyStatus();
        }
        else if (m_bIsStart == true)
        {
            m_objPlayer = GameManager.instance.Player;
            m_PlayerStatus = m_objPlayer.GetComponent<PlayerStatus>();  
            SetStatus();
            StatPlus();
            StatMinus();
            StatApply();
        }

        SetMonsterInfo();
        SwitchCharInfo();
        SwitchInven();
    }
}
