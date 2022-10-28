using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Itemlist/Potion", order = 2)]
public class Potion : ScriptableObject
{
    public string m_sName;
    public int m_nHp;
    public int m_nMp;
    
    public Sprite m_nItemIcon;
}
