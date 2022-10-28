using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private int m_nItemCount = 0;
    [SerializeField]
    private int m_nMaxItemCount;
    [SerializeField]
    private Image m_imgSlot;
    [SerializeField]
    private Sprite m_imgOriginal;
    [SerializeField]
    private GameObject m_objItemCount;
    [SerializeField]
    private bool m_bisEmpty = true;
    [SerializeField]
    public Item m_item;

    public bool IsEmpty { get => m_bisEmpty; set => m_bisEmpty = value; }
    public int MaxItemCount { get => m_nMaxItemCount; set => m_nMaxItemCount = value; }
    public int ItemCount { get => m_nItemCount; set => m_nItemCount = value; }
    public Item Item { get => m_item; set => m_item = value; }

    public void GetItem(Item item)
    {
        if (m_nItemCount != 0)
        {
            IsEmpty = false;
            Inventory.instance.objITems.Add(this.gameObject);
        }
        m_imgSlot = this.gameObject.GetComponent<Image>();
        m_imgSlot.sprite = item.m_nItemImg;
        m_item = item;
    }
    public void SetItemCount(int count)
    {
        m_nItemCount = count;
    }

    public void SetMaxItemCount() // 아이템별 최대수치 할당
    {
        if (Item.m_eItemType == Item.E_Item_Type.Potion)
            m_nMaxItemCount =  10;

        if (Item.m_eItemType == Item.E_Item_Type.Equipment)
            m_nMaxItemCount = 1;
    }   
    

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        SetMaxItemCount();
        m_objItemCount.GetComponent<Text>().text = m_nItemCount.ToString();
        if (m_nItemCount <= 0)
        {
            IsEmpty = true;
            Inventory.instance.objITems.Remove(this.gameObject);
            m_objItemCount.SetActive(false);
            m_imgSlot.sprite = m_imgOriginal;
        }
        else
            m_objItemCount.SetActive(true);
    }
}

public class CopyOfItemSlot : MonoBehaviour
{
    [SerializeField]
    private int m_nItemCount = 0;
    [SerializeField]
    private int m_nMaxItemCount;
    [SerializeField]
    private Image m_imgSlot;
    [SerializeField]
    private Sprite m_imgOriginal;
    [SerializeField]
    private GameObject m_objItemCount;
    [SerializeField]
    private bool m_bisEmpty = true;
    [SerializeField]
    public Item m_item;

    public bool IsEmpty { get => m_bisEmpty; set => m_bisEmpty = value; }
    public int MaxItemCount { get => m_nMaxItemCount; set => m_nMaxItemCount = value; }
    public int ItemCount { get => m_nItemCount; set => m_nItemCount = value; }
    public Item Item { get => m_item; set => m_item = value; }

    public void GetItem(Item item)
    {
        if (m_nItemCount != 0)
        {
            IsEmpty = false;
            Inventory.instance.objITems.Add(this.gameObject);
        }
        m_imgSlot = this.gameObject.GetComponent<Image>();
        m_imgSlot.sprite = item.m_nItemImg;
        m_item = item;
    }
    public void SetItemCount(int count)
    {
        m_nItemCount = count;
    }

    public void SetMaxItemCount() // 아이템별 최대수치 할당
    {
        if (Item.m_eItemType == Item.E_Item_Type.Potion)
            m_nMaxItemCount = 10;

        if (Item.m_eItemType == Item.E_Item_Type.Equipment)
            m_nMaxItemCount = 1;
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetMaxItemCount();
        m_objItemCount.GetComponent<Text>().text = m_nItemCount.ToString();
        if (m_nItemCount <= 0)
        {
            IsEmpty = true;
            Inventory.instance.objITems.Remove(this.gameObject);
            m_objItemCount.SetActive(false);
            m_imgSlot.sprite = m_imgOriginal;
        }
        else
            m_objItemCount.SetActive(true);
    }
}
