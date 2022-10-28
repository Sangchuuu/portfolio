using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Movement : MonoBehaviour
{
    private PlayerStatus m_PlayerStatus;
    private ApplyStatus m_ApplyStatus;
    [SerializeField]
    private bool m_bIsMoving;
    [SerializeField]
    private bool m_bIsAttack;
    [SerializeField]
    private bool m_bIsAttacking;
    [SerializeField]
    private bool m_bIsDie;

    [SerializeField]
    private Camera m_camPlayer;

    [SerializeField]
    private Vector3 m_traMovePoint;

    [SerializeField]
    private Animator m_PAnimator;

    [SerializeField]
    private GameObject m_objEnemy;

    static GameObject m_objPlayer;

    public GameObject ObjEnemy { get => m_objEnemy; set => m_objEnemy = value; }
    public bool IsAttacking { get => m_bIsAttack; set => m_bIsAttack = value; }
    public bool PlayerDie { get => m_bIsDie; set => m_bIsDie = value; }
    public bool IsMoving { get => m_bIsMoving; set => m_bIsMoving = value; }
    public void Move()
    {
        if (JoyStick.instance.IsInput == false)
        {
        float fMovSpd = m_ApplyStatus.MovSpd;
            if (m_bIsMoving == true)
            {
                float fDist = (m_traMovePoint - this.transform.position).magnitude;
                Vector3 vDir = (m_traMovePoint - this.transform.position).normalized;
                if (m_objEnemy == null)
                {
                    if (fDist > 0.3f)
                    {
                        m_bIsMoving = true;
                        this.transform.position += vDir * fMovSpd * Time.deltaTime;
                    }
                    else if (fDist <= 0.3f)
                    {
                        m_bIsMoving = false;
                    }
                }
                else if (fDist >= 0.8f)
                {
                    m_bIsMoving = true;
                    this.transform.position += vDir * fMovSpd * Time.deltaTime;

                }
                else if (fDist <= 0.8f)
                    m_bIsMoving = false;
            }
        }

    }
    public void Attack()
    {
        if (m_objEnemy != null)
        {
            float fDist = (m_objEnemy.transform.position - this.transform.position).magnitude;
            if (fDist <= 1f)
            {
                if (m_bIsAttacking == true || PlayerUi.instance.m_bIsAttackButtonClick == true)
                {
                    if (m_bIsAttack == false && m_objEnemy.activeSelf == true)
                    {
                        this.transform.LookAt(m_objEnemy.transform.position);
                        StartCoroutine(AttackDelay(m_ApplyStatus.AtkDelay));
                    }
                }
            }

            if (m_objEnemy.GetComponent<MonsterAi>().IsDie == true)
            {
                m_objEnemy = null;
            }

            if (fDist > 8)
            {
                m_objEnemy = null;
                PlayerUi.instance.m_objMonster = null;
                PlayerUi.instance.MonsterInfo.SetActive(false);
            }
        }
    }
    public void EmptyAttack() // 쉬프트 누르고 공격시 허공에 공격
    {   
        FindMovingSopt();
        if (Input.GetKey(KeyCode.LeftShift)) // 허공공격
        {
           
            if (Input.GetMouseButton(0))
            {
                if (m_bIsAttack == false)
                {
                    StartCoroutine(AttackDelay(m_ApplyStatus.AtkDelay));  
                }
                transform.LookAt(m_traMovePoint);
                m_PAnimator.SetTrigger("Attack");
            }
        }
    }
    IEnumerator AttackDelay(float time)
    {
        m_bIsAttack = true;
        m_PAnimator.SetTrigger("Attack");
        if (m_objEnemy != null)
        {
            MonsterAi Monster = m_objEnemy.GetComponent<MonsterAi>();
            Monster.CurHp = Monster.CurHp - (m_ApplyStatus.Atk - Monster.MonStat.Def);
            m_objEnemy.GetComponent<MonsterAi>().IsAttacked = true;
            Debug.Log(m_ApplyStatus.Atk - Monster.MonStat.Def);
        }
        yield return new WaitForSeconds(time);
        m_bIsAttack = false;
    }

    public void Die()
    {
        if(m_ApplyStatus.CurHp <= 0)
        {
            m_bIsDie = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
            m_bIsDie = false;
        }
    }
    
    public void FindMovingSopt()
    {
        if (JoyStick.instance.IsInput == false)
        {
            if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
            {
                Ray ray = m_camPlayer.ScreenPointToRay(Input.mousePosition);
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                if (Physics.Raycast(ray, out RaycastHit raycastHit, 100f))
                {
                    if (raycastHit.collider.gameObject.CompareTag("Monster"))
                    {
                        m_objEnemy = raycastHit.collider.gameObject;
                        m_objEnemy.GetComponent<MonsterAi>().Target = this.gameObject;
                        PlayerUi.instance.m_objMonster = m_objEnemy;
                    }
                    m_traMovePoint = raycastHit.point;
                    m_traMovePoint.y = this.transform.position.y;
                    this.transform.LookAt(m_traMovePoint);

                }
                if (Input.GetMouseButtonDown(1))
                {
                    if (raycastHit.collider.gameObject.CompareTag("Monster"))
                    {
                        m_bIsAttacking = true;
                    }
                    else
                    {
                        m_bIsAttacking = false;
                        PlayerUi.instance.m_bIsAttackButtonClick = false;
                    }
                    m_bIsMoving = true;
                    m_traMovePoint = raycastHit.point;
                    m_traMovePoint.y = this.transform.position.y;
                    this.transform.LookAt(m_traMovePoint);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    PlayerUi.instance.m_bIsAttackButtonClick = false;
                    if (raycastHit.collider.gameObject.CompareTag("Shop"))
                    {
                        PlayerUi.instance.Shop.SetActive(true);
                        this.transform.LookAt(m_traMovePoint);
                    }
                    if (raycastHit.collider.gameObject.CompareTag("Job"))
                    {
                        PlayerUi.instance.Job.SetActive(true);
                        this.transform.LookAt(m_traMovePoint);
                    }
                    m_bIsMoving = false;
                    m_bIsAttacking = false;
                }
            }
        }
    }
    
    private void Awake()
    {
        if(m_objPlayer != null)
        {
            Destroy(m_objPlayer);
        }
        else
        {
            m_objPlayer = this.gameObject;
        }
    }
    
    void Start()
    {
        m_bIsDie = false;
        m_PlayerStatus = this.gameObject.GetComponent<PlayerStatus>();
        m_PAnimator = this.gameObject.GetComponent<Animator>();
        m_camPlayer = GameManager.instance.PlayerCamera.GetComponent<Camera>();
    }
    void Update()
    {
        m_ApplyStatus = m_PlayerStatus.AplyStat;
        Die();
        Move();
        EmptyAttack();
        DontDestroyOnLoad(this);
        m_PAnimator.SetBool("IsWalk", m_bIsMoving); 
        Attack();



    }
}
