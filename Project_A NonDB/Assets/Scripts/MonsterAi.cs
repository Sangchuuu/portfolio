using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAi : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vMovePoint;
    [SerializeField]
    private float m_fDelayTime;
    [SerializeField]
    private bool m_bIsMoving;
    [SerializeField]
    private bool m_bIsAttack;
    [SerializeField]
    private bool m_bIsAttacked;
    [SerializeField]
    private bool m_bisDie;
    [SerializeField]
    private GameObject m_objTarget;
    [SerializeField]
    private int m_nCurHp;
    [SerializeField]
    private MonsterStatus m_MStatus = new MonsterStatus();

    MonsterManager m_MonsterManager;

    [SerializeField]
    Image m_imgHpBar;
    

    Animator m_MonAnim;

    public GameObject Target { get => m_objTarget; set => m_objTarget = value; }
    public int CurHp { get => m_nCurHp; set => m_nCurHp = value; }
    public bool IsAttacked { get => m_bIsAttacked; set => m_bIsAttacked = value; }
    public bool IsDie { get => m_bisDie; set => m_bisDie = value; }
    public MonsterStatus MonStat { get => m_MStatus; set => m_MStatus = value; }

    enum E_Ai_State { Idle, Move, Chase, Attack };
    [SerializeField]
    E_Ai_State m_eAi_State = E_Ai_State.Idle;

    public void Timmer()
    {
        m_fDelayTime -= Time.deltaTime;
        if(m_fDelayTime <= 0)
        {
            m_fDelayTime = Random.Range(1f, 5f);
            if (m_bIsMoving == false)
            {
                m_bIsMoving = true;
                float fXaxis = Random.Range(this.transform.position.x - 5f, this.transform.position.x + 5f);
                float fZaxis = Random.Range(this.transform.position.z - 5f, this.transform.position.z + 5f);
                m_vMovePoint = new Vector3(fXaxis, this.transform.position.y, fZaxis);
            }
            else
                m_bIsMoving = false;
        }
    }
    private void FindTarget()
    {
        if (m_objTarget == null)
        {
            if (m_MStatus.EMonAttackType == MonsterStatus.E_Mon_AttackType.First)
            {
                Collider[] colliders = Physics.OverlapSphere(this.transform.position, m_MStatus.DetDist);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].CompareTag("Player"))
                    {
                        Debug.Log(colliders[i].name);
                        m_objTarget = colliders[i].gameObject;
                    }
                }
            }
        }
        else if(m_objTarget.GetComponent<Movement>().PlayerDie == true)
        {
            m_objTarget = null;
        }
    }
    private void Attack()
    {
        if (m_objTarget != null)
        {
            float fDist = (this.transform.position - m_objTarget.transform.position).magnitude;
            if (fDist >= m_MStatus.AtkDist)
            {
                this.transform.LookAt(m_objTarget.transform);
                this.transform.forward += transform.forward * m_MStatus.MovSpd * Time.deltaTime;
            }
            else if (m_bIsAttack == false)
            {
                StartCoroutine(AttackDelay(m_MStatus.AtkDelay));
                m_MonAnim.SetTrigger("Attack");
            }
        }
    }
    IEnumerator AttackDelay(float fTime)
    {
        m_bIsAttack = true;
        PlayerStatus playerStatus = m_objTarget.GetComponent<PlayerStatus>();
        ApplyStatus applyStatus = playerStatus.AplyStat;
        playerStatus.CurHp = playerStatus.CurHp - (m_MStatus.Atk - applyStatus.Def);
        yield return new WaitForSeconds(fTime);
        m_bIsAttack = false;
    }
    private void AiState()
    {
        if (m_objTarget != null)
        {
            float fDist = (m_objTarget.transform.position - this.transform.position).magnitude;
            if (m_MStatus.EMonAttackType == MonsterStatus.E_Mon_AttackType.First)
            {
                if (m_bIsAttacked == true)
                {
                    if (fDist > m_MStatus.AtkDist)
                        m_eAi_State = E_Ai_State.Chase;
                    else if (fDist <= m_MStatus.AtkDist)
                        m_eAi_State = E_Ai_State.Attack;
                    if (fDist >= m_MStatus.DetDist)
                        m_objTarget = null;
                }
                else
                {
                    if (fDist > m_MStatus.AtkDist)
                        m_eAi_State = E_Ai_State.Chase;
                    else if (fDist <= m_MStatus.AtkDist)
                        m_eAi_State = E_Ai_State.Attack;
                    if (fDist >= m_MStatus.DetDist)
                    {
                        m_eAi_State = E_Ai_State.Idle;
                        m_objTarget = null;
                    }
                }
            }
            if (m_bIsAttacked == true)
            {
                if (m_MStatus.EMonAttackType == MonsterStatus.E_Mon_AttackType.After)
                {
                    if (fDist > m_MStatus.AtkDist)
                        m_eAi_State = E_Ai_State.Chase;
                    else if (fDist <= m_MStatus.AtkDist)
                        m_eAi_State = E_Ai_State.Attack;
                    if (fDist >= m_MStatus.DetDist)
                        m_objTarget = null;
                }
            }
        }
        else
        {
            m_bIsAttacked = false;
            Timmer();
            if (m_bIsMoving == true)
            {
                m_eAi_State = E_Ai_State.Move;
            }
            else
                m_eAi_State = E_Ai_State.Idle;
        }
        ApplyAiState(m_eAi_State);
        if (m_nCurHp <= 0)
            Die();
    }

    private void ApplyAiState(E_Ai_State state)
    {
        switch (state)
        {
            case E_Ai_State.Idle:
                break;

            case E_Ai_State.Move:
                float fDist = (this.transform.position - m_vMovePoint).magnitude;
                this.transform.LookAt(m_vMovePoint);
                if (fDist >= 0.3f)
                {
                    this.transform.position += transform.forward * m_MStatus.MovSpd * Time.deltaTime;
                }
                break;

            case E_Ai_State.Chase:
                if (m_objTarget != null)
                {
                    transform.LookAt(m_objTarget.transform);
                    this.transform.position += transform.forward * m_MStatus.MovSpd * Time.deltaTime;
                }
                break;

            case E_Ai_State.Attack:
                Attack();
                break;
        }
    }

    private void Die()
    {
        if (m_objTarget != null)
        {
            PlayerStatus playerStatus = m_objTarget.GetComponent<PlayerStatus>();
            playerStatus.CurBaseExp += MonStat.BaseExp;
            playerStatus.CurJobExp += MonStat.JobExp;
            playerStatus.Money += MonStat.Gold;
            m_objTarget.GetComponent<Movement>().ObjEnemy = null;
        }
        Debug.Log(m_objTarget);
        m_MonsterManager.Enque(this.gameObject, m_MStatus.MaxHp);
        m_MonsterManager.MonsterDieCount++;
        m_bIsAttacked = false;
        m_bIsAttack = false;
        m_bisDie = true;
        m_objTarget = null;
        this.gameObject.SetActive(false);
    }

    void Start()
    {
        m_MonsterManager = MonsterManager.instance;
        m_fDelayTime = Random.Range(1f, 5f);
        m_MonAnim = this.gameObject.GetComponent<Animator>();
        m_bIsAttacked = false;
        m_nCurHp = m_MStatus.MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        AiState();
    }
}
