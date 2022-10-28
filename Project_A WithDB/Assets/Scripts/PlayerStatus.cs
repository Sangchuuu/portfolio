using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    ApplyStatus m_ApplyStatus;
    [SerializeField]
    private int[] m_nStatus = new int[6];

    [SerializeField]
    private int m_nMoney;
    [SerializeField]
    private int m_nCurBaseExp;
    [SerializeField]
    private int m_nMaxBaseExp;
    [SerializeField]
    private int m_nCurJobExp;
    [SerializeField]
    private int m_nMaxJobExp;
    [SerializeField]
    private int m_nCurBaseLevel;
    [SerializeField]
    private int m_nMaxBaseLevel;
    [SerializeField]
    private int m_nCurJobLevel;
    [SerializeField]
    private int m_nMaxJobLevel;
    [SerializeField]
    private int m_nStatPoint;
    [SerializeField]
    private int m_nSkillPoint;
    [SerializeField]
    private bool m_bIsGetJob;
    private bool m_bIsBaseLevelUp;
    private bool m_bIsJobLevelUp;
    
    public enum E_Player_Job { Nobis, SwordMan, Archer, Magician, Acolyte }
    public E_Player_Job m_ePlayerJob;

    public E_Player_Job PlayerJob { get => m_ePlayerJob; set => m_ePlayerJob = value; }
    public bool IsBaseLevelUp { get => m_bIsBaseLevelUp; set => m_bIsBaseLevelUp = value; }
    public bool IsJobLevelUp { get => m_bIsJobLevelUp; set => m_bIsJobLevelUp = value; }



    public void Setstat(int stat, int count)
    {
        for (int i = 0; i < m_nStatus.Length; i++)
        {
            if (i == count)
            {
                m_nStatus[i] = stat;
            }
        }
    }
    public int GetStat(int count)
    {
        return m_nStatus[count];
    }

    #region 게터세터
    public int[] Stats { get => m_nStatus; set => m_nStatus = value; }
    public ApplyStatus AplyStat { get { return m_ApplyStatus; } set { m_ApplyStatus = value; } }
    public int Str { get => m_nStatus[0]; set => m_nStatus[0] = value; }
    public int Agi { get => m_nStatus[1]; set => m_nStatus[1] = value; }
    public int Vit { get => m_nStatus[2]; set => m_nStatus[2] = value; }
    public int Int { get => m_nStatus[3]; set => m_nStatus[3] = value; }
    public int Dex { get => m_nStatus[4]; set => m_nStatus[4] = value; }
    public int Luk { get => m_nStatus[5]; set => m_nStatus[5] = value; }

    public int Money { get => m_nMoney; set => m_nMoney = value; }
    public int CurBaseExp { get => m_nCurBaseExp; set => m_nCurBaseExp = value; }
    public int MaxBaseExp { get => m_nMaxBaseExp; set => m_nMaxBaseExp = value; }
    public int CurJobExp { get => m_nCurJobExp; set => m_nCurJobExp = value; }
    public int MaxJobExp { get => m_nMaxJobExp; set => m_nMaxJobExp = value; }
    public int CurBaseLevel { get => m_nCurBaseLevel; set => m_nCurBaseLevel = value; }
    public int MaxBaseLevel { get => m_nMaxBaseLevel; set => m_nMaxBaseLevel = value; }
    public int CurJobLevel { get => m_nCurJobLevel; set => m_nCurJobLevel = value; }
    public int MaxJobLevel { get => m_nMaxJobLevel; set => m_nMaxJobLevel = value; }
    public int StatPoint { get => m_nStatPoint; set => m_nStatPoint = value; }
    public int SkillPoint { get => m_nSkillPoint; set => m_nSkillPoint = value; }
    public bool IsGetJob { get => m_bIsGetJob; set => m_bIsGetJob = value; }
    #endregion


    public void ApplyStats()
    {
        m_ApplyStatus.MaxHp = 100 + (m_ApplyStatus.MaxHp / 100 * m_nStatus[2]);
        m_ApplyStatus.Atk = 10 + m_nStatus[0] + (m_nStatus[4] / 5) + (m_nStatus[5] / 3)  ;
        m_ApplyStatus.Matk = 10 + (int)(m_nStatus[3] * 1.5) + (m_nStatus[4] / 5) + (m_nStatus[5] / 3);
        m_ApplyStatus.SAtk = 0;
        m_ApplyStatus.SMAtk = 0;
        m_ApplyStatus.HpRecovery = (m_nStatus[2] / 5);
        m_ApplyStatus.Hit = m_nStatus[4] + (m_nStatus[5] / 3);
        m_ApplyStatus.MaxSp = 100 + (m_ApplyStatus.MaxSp / 100 * m_nStatus[3]);
        m_ApplyStatus.Def = (m_nStatus[1] / 5) + (m_nStatus[2] / 5);
        m_ApplyStatus.Mdef = (m_nStatus[2] / 5) + (m_nStatus[3] / 2) + (m_nStatus[4] / 5);
        m_ApplyStatus.SDef = 0;
        m_ApplyStatus.SMDef = 0;
        m_ApplyStatus.SpRecovery = m_nStatus[3] / 6 + SpRecoveryAlpha();
        m_ApplyStatus.Flee = m_nStatus[1] + (m_nStatus[5] / 5);
        m_ApplyStatus.MaxWeight = 1000 + (m_nStatus[0] * 30);
        m_ApplyStatus.Critical = (float)(m_nStatus[5] * 0.3);
        m_ApplyStatus.MovSpd = 3f;
        m_ApplyStatus.AtkSpd = 156 + ((float)(m_nStatus[1] / 10));
        m_ApplyStatus.AtkDelay = 1 / (50 / (200 - m_ApplyStatus.AtkSpd));
    }
    public int SpRecoveryAlpha()
    {
        int i = 0;
        if(m_nStatus[3] >= 120)
        {
            int n = 0;
            if (m_nStatus[3] == 120)
            {
                n = 4;
            }
            i = n + (m_nStatus[3] - 118) / 2;
        }
        return i;
    }    


    public void BaseLevelUp()
    {
        if (m_nCurBaseExp >= m_nMaxBaseExp)
        {
            m_nCurBaseLevel++;
            m_nCurBaseExp -= m_nMaxBaseExp;
            int nCount = m_nCurBaseLevel / 5;
            int nPoint = 3;
            m_nStatPoint = m_nStatPoint + nPoint + nCount;
            m_bIsBaseLevelUp = true;
        }
    }



    public void JobLevelUp()
    {
        if (m_nCurJobExp >= m_nMaxJobExp)
        {
            m_nCurJobExp -= m_nMaxJobExp;
            m_nCurJobLevel++;
            m_nSkillPoint++;
            m_bIsJobLevelUp = true;
        }
    }
    public void CurState()
    {
        m_ApplyStatus.CurHp = m_ApplyStatus.MaxHp;
        m_ApplyStatus.CurSp = m_ApplyStatus.MaxSp;
    }
    public void GetJob()
    {
        if (m_ePlayerJob == E_Player_Job.Nobis)
        {
            if (m_nCurJobLevel < 10)
                m_bIsGetJob = false;
            else if(m_nCurJobLevel >= 10)
                m_bIsGetJob = true;
        }
    }
    private void Awake()
    {
        ApplyStats();
        CurState();
    }
    private void Start()
    {

    }
    private void Update()
    {
        BaseLevelUp();
        JobLevelUp();
        GetJob(); 
    }
}
