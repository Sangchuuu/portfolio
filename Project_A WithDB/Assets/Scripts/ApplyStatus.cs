using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ApplyStatus
{
    #region 스테이터스
    [SerializeField]
    private int m_nMaxHp;
    [SerializeField]
    private int m_nAtk;
    [SerializeField]
    private int m_nMatk;
    [SerializeField]
    private int m_nSAtk;
    [SerializeField]
    private int m_nSMAtk;
    [SerializeField]
    private int m_nHpRecovery;
    [SerializeField]
    private int m_nHit;
    [SerializeField]
    private int m_nMaxSp;
    [SerializeField]
    private int m_nDef;
    [SerializeField]
    private int m_nMdef;
    [SerializeField]
    private int m_nSDef;
    [SerializeField]
    private int m_nSMDef;
    [SerializeField]
    private int m_nSpRecovery;
    [SerializeField]
    private int m_nFlee;

    [SerializeField]
    private int m_nCurHp;
    [SerializeField]
    private int m_nCurSp;

    [SerializeField]
    private int m_nMaxWeight;

    [SerializeField]
    private float m_fCritical;
    [SerializeField]
    private float m_fMovSpd;
    [SerializeField]
    private float m_fAtkSpd;
    [SerializeField]
    private float m_fAtkDelay;
    #endregion

    #region 게터세터
    public int MaxHp        { get => m_nMaxHp; set => m_nMaxHp = value; }
    public int Atk          { get => m_nAtk; set => m_nAtk = value; }
    public int Matk         { get => m_nMatk; set => m_nMatk = value; }
    public int SAtk         { get => m_nSAtk; set => m_nSAtk = value; }
    public int SMAtk        { get => m_nSMAtk; set => m_nSMAtk = value; }
    public int HpRecovery   { get => m_nHpRecovery; set => m_nHpRecovery = value; }
    public int Hit          { get => m_nHit; set => m_nHit = value; }
    public int MaxSp        { get => m_nMaxSp; set => m_nMaxSp = value; }
    public int Def          { get => m_nDef; set => m_nDef = value; }
    public int Mdef         { get => m_nMdef; set => m_nMdef = value; }
    public int SDef         { get => m_nSDef; set => m_nSDef = value; }
    public int SMDef        { get => m_nSMDef; set => m_nSMDef = value; }
    public int SpRecovery   { get => m_nSpRecovery; set => m_nSpRecovery = value; }
    public int Flee         { get => m_nFlee; set => m_nFlee = value; }
    public int CurHp        { get => m_nCurHp; set => m_nCurHp = value; }
    public int CurSp        { get => m_nCurSp; set => m_nCurSp = value; }
    public int MaxWeight    { get => m_nMaxWeight; set => m_nMaxWeight = value; }
    public float Critical   { get => m_fCritical; set => m_fCritical = value; }
    public float MovSpd     { get => m_fMovSpd; set => m_fMovSpd = value; }
    public float AtkSpd     { get => m_fAtkSpd; set => m_fAtkSpd = value; }
    public float AtkDelay   { get => m_fAtkDelay; set => m_fAtkDelay = value; }


    #endregion

}
