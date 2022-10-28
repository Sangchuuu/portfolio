using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private Text m_txtTotalCost;
    [SerializeField]
    private Text m_txtComment;
    [SerializeField]
    private List<Item> m_listItems = new List<Item>();
    [SerializeField]
    private ShopItemSlots[] m_ShopItemSlots;
    [SerializeField]
    private int m_nTotalCost;
    [SerializeField]
    private List<ShopCartSlot> m_listShopCartSlots;
    [SerializeField]
    private Button m_btnExit;
    [SerializeField]
    private Button m_btnCancel;
    [SerializeField]
    private Button m_btnApply;

    [SerializeField]
    private GameObject m_objPlayer;

    public bool m_bIsApply ;

    private int m_nEmptySlot;
    
    public List<Item> ListItems { get => m_listItems; set => m_listItems = value; }
    
    public static Shop instace = null;
    private void Awake()
    {
        
        if (instace != null)
        {
            Destroy(instace);
        }
        else
            instace = this;
    }

    public int TotalCost { get => m_nTotalCost; set => m_nTotalCost = value; }

    private void EmptyCartSlot(int i)
    {
        bool isHaveItem = false;

        for(int count = 0; count < m_listShopCartSlots.Count; count++)//1번에서 2번으로 바로 넘어가버림 2번 넘어가면 바로 칸이 꽉참.
        {
            
            if (m_ShopItemSlots[i].Item.m_nItemImg == m_listShopCartSlots[count].Item.m_nItemImg)
            {
                isHaveItem = true;
                break;
            }
            else if(m_listShopCartSlots[count].m_bIsEmpty == true)
            {
                isHaveItem = false;
                m_nEmptySlot = count;
                break;
            }
        }
        if(isHaveItem == false)
        {
            m_listShopCartSlots[m_nEmptySlot].AddCartItem(m_ShopItemSlots[i].Item);
        }
    }

    private void ItemButtonClick()
    {
        for (int i = 0; i < m_ShopItemSlots.Length; i++)
        {
            int n = i;
            m_ShopItemSlots[n].GetComponent<Button>().onClick.AddListener(delegate
            {
                EmptyCartSlot(n);
            });
        }
    }

    private void CheckSameItem()
    {
        bool bIsHaveSameItem = false;
        int nEmptySlot = 0;
        int nSameSlot = 0;
        for (int  count = 0; count < m_listShopCartSlots.Count; count++)
        {
            if (m_listShopCartSlots[count].ItemCount != 0)
            {
                for (int i = 0; i < Inventory.instance.ItemSlots.Length; i++ )
                {
                    if (Inventory.instance.ItemSlots[i].Item.m_sName == m_listShopCartSlots[count].Item.m_sName)
                    {
                        Inventory.instance.ItemSlots[i].ItemCount += m_listShopCartSlots[count].ItemCount;
                        bIsHaveSameItem = true;
                        nSameSlot = i;
                        break;
                    }
                    else if (Inventory.instance.ItemSlots[i].IsEmpty == true)
                    {
                        bIsHaveSameItem = false;
                        nEmptySlot = i;
                        break;
                    }
                }
                if(bIsHaveSameItem == false)
                {
                    Inventory.instance.ItemSlots[nEmptySlot].ItemCount += m_listShopCartSlots[count].ItemCount;
                    Inventory.instance.ItemSlots[nEmptySlot].IsEmpty = false;
                    Inventory.instance.ItemSlots[nEmptySlot].GetItem(m_listShopCartSlots[count].Item);
                }
                else
                {
                    Inventory.instance.ItemSlots[nSameSlot].GetItem(m_listShopCartSlots[count].Item);
                }
            }
        }
    }
    private void ApplyButtonClick()
    {
        
        m_btnApply.onClick.AddListener(delegate
        {
            CheckSameItem();
            for (int i = 0; i < m_listShopCartSlots.Count; i++)
            {
                m_listShopCartSlots[i].DeleteItem();
            }
            m_nEmptySlot = 0;
            m_objPlayer.GetComponent<PlayerStatus>().Money -= m_nTotalCost;
        });
    }
    private void CancelButtonClick()
    {
        for (int i = 0; m_listShopCartSlots.Count > i; i++)
        {
            m_listShopCartSlots[i].DeleteItem();
            m_nTotalCost = 0;
        }
        m_nEmptySlot = 0;
    }
    private void AddItem()
    {
        ItemList itemList = GameManager.instance.GetComponent<ItemList>();
        for (int i =0 ; itemList.items.Count > i; i++)
        {
            m_ShopItemSlots[i].AddItem(itemList.items[i]);
        }
    }
    private void Calculate()
    {
        if (m_objPlayer.GetComponent<PlayerStatus>().Money < m_nTotalCost)
        {
            m_txtComment.text = "보유 재화가 부족합니다.";
            m_txtTotalCost.color = Color.red;
            m_txtComment.color = Color.red;
            m_btnApply.interactable = false;

        }
        else
        {
            m_btnApply.interactable = true;
            m_txtComment.text = "구매 하시겠습니까?";
            m_txtTotalCost.color = Color.black;
            m_txtComment.color = Color.black;
        }
        if (m_nTotalCost == 0)
            m_txtComment.text = "선택하신 아이템이 없습니다.";
    }
    void Start()
    {
        m_nEmptySlot = 0;
        m_objPlayer = GameManager.instance.Player;
        m_btnCancel.onClick.AddListener(delegate { CancelButtonClick(); });
        m_btnExit.onClick.AddListener(delegate { CancelButtonClick(); });
        ApplyButtonClick();
        ItemButtonClick();
        CancelButtonClick();
        AddItem();
    }

    void Update()
    {
        m_txtTotalCost.text = m_nTotalCost.ToString();
        Calculate();
    }
}
