using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MonsterStatus
{
    [SerializeField]
    private string m_sName;
    [SerializeField]
    private int m_nAtk;
    [SerializeField]
    private int m_nDef;
    [SerializeField]
    private int m_nMaxHp;
    [SerializeField]
    private int m_nBaseExp;
    [SerializeField]
    private int m_nJobExp;
    [SerializeField]
    private int m_nLevel;
    [SerializeField]
    private int m_nGold;
    [SerializeField]
    private float m_fAtkDelay;
    [SerializeField]
    private float m_fMovSpd;
    [SerializeField]
    private float m_fAtkDist;
    [SerializeField]
    private float m_fDetDist;


    public enum E_Mon_Attribute { Fire, Water, Ground, Posion, None, Wind }
    public enum E_Mon_AttackType { First, After } //선공몬스터, 후공몬스터.

  
    [SerializeField]
    private E_Mon_Attribute m_eMonAtb;
    [SerializeField]
    private E_Mon_AttackType m_eMonAttackType;

    public string Name { get => m_sName; set => m_sName = value; }
    public int Atk { get => m_nAtk; set => m_nAtk = value; }
    public int Def { get => m_nDef; set => m_nDef = value; }
    public int MaxHp { get => m_nMaxHp; set => m_nMaxHp = value; }
    public int BaseExp { get => m_nBaseExp; set => m_nBaseExp = value; }
    public int JobExp { get => m_nJobExp; set => m_nJobExp = value; }
    public int Level { get => m_nLevel; set => m_nLevel = value; }
    public int Gold { get => m_nGold; set => m_nGold = value; }
    public float AtkDelay { get => m_fAtkDelay; set => m_fAtkDelay = value; }
    public float MovSpd { get => m_fMovSpd; set => m_fMovSpd = value; }
    public float AtkDist { get => m_fAtkDist; set => m_fAtkDist = value; }
    public float DetDist { get => m_fDetDist; set => m_fDetDist = value; }
    public E_Mon_Attribute EMonAtb { get => m_eMonAtb; set => m_eMonAtb = value; }
    public E_Mon_AttackType EMonAttackType { get => m_eMonAttackType; set => m_eMonAttackType = value; }
}
