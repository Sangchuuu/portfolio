using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    int m_nMosterDieCount;
    [SerializeField]
    bool m_bisRespawn;
    [SerializeField]
    GameObject m_objAlliveMonsters;
    [SerializeField]
    GameObject m_objDieMonsters;
    [SerializeField]
    GameObject m_prefabMonster;
    [SerializeField]
    GameObject m_objMonsterKimg;
    [SerializeField]
    GameObject m_prefabMonsterKimg;
    [SerializeField]
    GameObject[] m_objMonsters = new GameObject[20];
    [SerializeField]
    Queue<GameObject> m_queMonster = new Queue<GameObject>();


    public int MonsterDieCount { get => m_nMosterDieCount; set => m_nMosterDieCount = value; }
    public static MonsterManager instance = null;
    
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }

    public void ResPawnMonster()
    {
        for (int i = 0; i < m_objMonsters.Length; i++)
        {
            if (m_objMonsters[i] == null)
            {
                float fX = Random.Range(-20f, +20f);
                float fZ = Random.Range(-20f, +20f);
                Vector3 vResPoint = new Vector3(fX, 0.1f, fZ);
                m_objMonsters[i] = Instantiate(m_prefabMonster, m_objAlliveMonsters.transform);
                m_objMonsters[i].transform.position = vResPoint;
            }
        }
    }
    public void Enque(GameObject objMonster, int MaxHp)
    {
        m_queMonster.Enqueue(objMonster);
        objMonster.transform.SetParent(m_objDieMonsters.transform);  
    }
    
    public void Deque()
    {
        if (m_queMonster.Count > 0)
        {
            if(m_bisRespawn == false)
            {
                StartCoroutine(ResPone(3));
            }
        }
    }    

    public void ResPawnMonsterKing()
    {
        if (m_objMonsterKimg == null)
        {
            float fX = Random.Range(-20f, +20f);
            float fZ = Random.Range(-20f, +20f);
            Vector3 vResPoint = new Vector3(fX, 0.1f, fZ);
            m_objMonsterKimg = Instantiate(m_prefabMonsterKimg, m_objAlliveMonsters.transform);
            m_objMonsterKimg.transform.position = vResPoint;
        }
    }

    IEnumerator ResPone(float fTime)
    {
        m_bisRespawn = true;
        yield return new WaitForSeconds(fTime);
        GameObject objMonster = m_queMonster.Dequeue();
        objMonster.transform.SetParent(m_objAlliveMonsters.transform);
        objMonster.GetComponent<MonsterAi>().CurHp = objMonster.GetComponent<MonsterAi>().MonStat.MaxHp;
        objMonster.GetComponent<MonsterAi>().IsDie = false;
        objMonster.SetActive(true);
        m_bisRespawn = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_bisRespawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        ResPawnMonster();
        Deque();
        if (m_nMosterDieCount > 30)
        {
            ResPawnMonsterKing();
        }
    }
}
