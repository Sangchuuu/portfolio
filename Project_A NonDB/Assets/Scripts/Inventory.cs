using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int x;
    public int y;
    int nClickBtnNum = 0;

    [SerializeField]
    private Button[] m_listBtnAplys; // 0 = 사용 , 1 = 설정

    [SerializeField]
    private ItemSlot[] m_listUseSlots;
    [SerializeField]
    private ItemSlot[] m_listItemSlots;
    [SerializeField]
    private ItemSlot m_slotNext;

    [SerializeField]
    private Transform m_traSlot;
    [SerializeField]
    GameObject m_objSetbutton;
    [SerializeField]
    GameObject m_objInventory;
    [SerializeField]
    List<GameObject> m_objItems = new List<GameObject>();


    public List<GameObject> objITems { get => m_objItems; set => m_objItems = value; }
    public ItemSlot[] ItemSlots { get => m_listItemSlots; set => m_listItemSlots = value; }
    public ItemSlot NextSlot { get => m_slotNext; set => m_slotNext = value; }

    public static Inventory instance = null;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
            instance = this;
    }

    public void UseITem(Item item)
    {
        if (item.m_nItemImg != null)
        {
            PlayerStatus playerStatus = GameManager.instance.Player.GetComponent<PlayerStatus>();
            playerStatus.CurHp += item.m_nHp;
            playerStatus.CurSp += item.m_nMp;
        }
    }

    public void ButtonClick()
    {
        for (int i = 0; i < m_objItems.Count; i++)
        {
            int n = i;
            Button button = m_objItems[n].GetComponent<Button>();
            button.onClick.AddListener(delegate
            {
                if (m_objItems.Count > 0)
                {
                    Debug.Log(m_objItems.Count);
                    Debug.Log("Click");
                    nClickBtnNum = n;
                    ClickItemSlotEvent(m_objItems[n].GetComponent<ItemSlot>());
                }
            });
        }
    }
   
    public void ClickItemSlotEvent(ItemSlot itemSlot)
    {
        m_objSetbutton.transform.position = new Vector3(itemSlot.gameObject.transform.position.x + x, itemSlot.gameObject.transform.position.y +y);
        m_objSetbutton.SetActive(true);
    }

    public void FindNextSlot() // 다음 슬롯 찾음.
    {
        for (int i =0; i < m_listItemSlots.Length; i++)
        {
            if (m_listItemSlots[i].IsEmpty == true) 
            {
                m_slotNext = m_listItemSlots[i];
                break;  
            }
        }
    }
    void Start()
    {
        FindNextSlot();
        m_objInventory.SetActive(false);
        m_listBtnAplys[0].onClick.AddListener(delegate 
        {
            UseITem(m_objItems[nClickBtnNum].GetComponent<ItemSlot>().Item);
            m_objItems[nClickBtnNum].GetComponent<ItemSlot>().ItemCount--;
            m_objSetbutton.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        ButtonClick();
        FindNextSlot();
        
    }
}
