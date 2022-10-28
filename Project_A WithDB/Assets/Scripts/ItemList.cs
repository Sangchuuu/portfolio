using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField]
    Sprite m_sprEmpty;
    [SerializeField]
    public List<Item> items;

    public void Awake()
    {
        //Item ¼ø¼­ : string Name, int id, int Hp, int Mp, int Atk, int Def, int nGrade, int Cost, Sprite sprite, E_Item_Type eItem_Type

        items.Add(new Item("HpPotion", 1, 10, 0, 0, 0, 0, 10, Resources.Load<Sprite>("HpPotion"), Item.E_Item_Type.Potion));
        items.Add(new Item("SpPotion", 2, 0, 10, 0, 0, 0, 10, Resources.Load<Sprite>("SpPotion"), Item.E_Item_Type.Potion));
        items.Add(new Item("Sword", 101, 0, 0, 10, 0, 0, 100, Resources.Load<Sprite>("Sword"), Item.E_Item_Type.Equipment)); 

        GameManager.instance.ItemList = this;
    }

}