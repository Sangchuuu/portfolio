using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCartSlot : MonoBehaviour
{
    [SerializeField]
    private Sprite m_EmtpySprite;
    [SerializeField]
    private Image m_imgItem;
    [SerializeField]
    private Text m_txtItemName;
    [SerializeField]
    private Text m_txtItemCount;
    [SerializeField]
    private Button[] m_btnPlusMinus = new Button[2];

    public bool m_bIsEmpty;

    [SerializeField]
    private Item m_item;

    public Item Item { get => m_item; set => m_item = value; }

    private int m_nItemCount;
    public int ItemCount { get => m_nItemCount; set => m_nItemCount = value; }

    private void PlusMinusclick()
    {
            m_btnPlusMinus[0].onClick.AddListener(
                delegate 
                {
                    if (m_bIsEmpty == false)
                    {
                        m_nItemCount++;
                        Shop.instace.TotalCost += m_item.m_nCost;
                    }
                });
            m_btnPlusMinus[1].onClick.AddListener(
                delegate 
                {
                    if (m_nItemCount > 0 && m_bIsEmpty == false)
                    {
                        m_nItemCount--;
                        Shop.instace.TotalCost -= m_item.m_nCost;
                    }
                });
    }
    
    void Start()
    {
        
        m_nItemCount = 0;
        m_bIsEmpty = true;
        PlusMinusclick();
    }
   public void AddCartItem(Item item)
   { 
       m_item = item;
       m_txtItemName.text = m_item.m_sName.ToString();
       m_imgItem.sprite = m_item.m_nItemImg;
       m_bIsEmpty = false;
   }
    public void DeleteItem()
    {
        m_bIsEmpty = true;
        m_txtItemName.text = "Empty";
        m_imgItem.sprite = m_EmtpySprite;
        m_item.m_nItemImg = null;
        m_nItemCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_txtItemCount.text = m_nItemCount.ToString();

    }
}
