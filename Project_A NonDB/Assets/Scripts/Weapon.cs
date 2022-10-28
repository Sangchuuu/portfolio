using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Itemlist/Weapon",order = 1)]
public class Weapon : ScriptableObject
{
    public string m_sName;
    public int m_nDmg;
    public int m_nDef;
    public int[] m_nWeaponStat = new int[6]; 

    public int m_nGrade;

    public GameObject m_nObjWeapon;
    public Sprite m_nItemIcon;

}
