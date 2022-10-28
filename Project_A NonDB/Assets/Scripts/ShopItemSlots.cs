using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemSlots : MonoBehaviour
{
    [SerializeField]
    private Item m_item;
    [SerializeField]
    private Image m_imgItem;
    [SerializeField]
    private Text m_txtItemName;
    [SerializeField]
    private Text m_txtItemCost;     


    public Item Item { get => m_item; set => m_item = value; }

    public void AddItem(Item item)
    {
        m_item = item;
        m_imgItem.sprite = item.m_nItemImg;
        m_txtItemName.text = item.m_sName.ToString();
        m_txtItemCost.text = item.m_nCost.ToString();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
