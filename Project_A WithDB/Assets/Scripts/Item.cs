using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Item
{
    public string m_sName;
    public int m_nId;
    public int m_nHp;
    public int m_nMp;
    public int m_nAtk;
    public int m_nDef;
    public int m_nGrade;
    public int m_nCost;


    public Sprite m_nItemImg;

    public enum E_Item_Type {Potion, Equipment};
    public E_Item_Type m_eItemType;

    public Item(string Name, int id, int Hp, int Mp, int Atk, int Def, int nGrade, int Cost, Sprite sprite, E_Item_Type eItem_Type)
    {
        m_sName = Name;
        m_nId = id;
        m_nHp = Hp;
        m_nMp = Mp;
        m_nAtk = Atk;
        m_nDef = Def;
        m_nGrade = nGrade;
        m_nCost = Cost;
        m_nItemImg = sprite;
        m_eItemType = eItem_Type;
    }
}
