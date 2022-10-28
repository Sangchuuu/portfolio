using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_objPlayer;
    private GameObject m_prePlayer;
    [SerializeField]
    private GameObject m_objPCamera;
    private GameObject m_preCamera;
    private ItemList m_itemList;


    public ItemList ItemList { get => m_itemList; set => m_itemList = value; }
    public GameObject Player { get => m_objPlayer; set => m_objPlayer = value; }
    public GameObject PlayerCamera { get => m_objPCamera; set => m_objPCamera = value; }


    public static GameManager instance = null;
    private void Awake()
    {

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        m_prePlayer = Resources.Load("Prefabs/Nobis") as GameObject;
        m_preCamera = Resources.Load("Prefabs/PlayerCamera") as GameObject;
    }

    public void RespawnPlayer()
    {
        if (m_objPlayer == null)
        {
            m_objPlayer = Instantiate(m_prePlayer);
            PlayerStatus playerStatus = m_objPlayer.GetComponent<PlayerStatus>();
        }
        if (m_objPCamera == null)
        {
            m_objPCamera = Instantiate(m_preCamera);
        }
            m_objPCamera = GameObject.FindGameObjectWithTag("PCamera");
        
     
    }

    void Start()
    {
        RespawnPlayer();
    }

    void Update()
    {
        RespawnPlayer();
    }
}
