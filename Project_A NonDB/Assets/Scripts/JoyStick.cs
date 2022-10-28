using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    public RectTransform m_rectController;
    private RectTransform m_rectTransform;

    private float m_fControllerRange;
    private float m_fSpeed;

    private GameObject m_objPlayer;

    private Vector3 m_vMove;

    [SerializeField]
    private bool m_bisInput;

    public bool IsInput { get => m_bisInput; set => m_bisInput = value; }

    public static JoyStick instance = null;

    private void Awake()
    {
        m_rectTransform = this.GetComponent<RectTransform>();
        m_fControllerRange = m_rectTransform.rect.width * 0.5f;
        if (instance == null)
            instance = this;

    }
     
    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoyStick(eventData.position);
        m_bisInput = true;
        m_objPlayer.GetComponent<Movement>().IsMoving = m_bisInput;
    }
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoyStick(eventData.position);
        m_bisInput = true;
        m_objPlayer.GetComponent<Movement>().IsMoving = m_bisInput;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        m_rectController.anchoredPosition = Vector2.zero;
        m_bisInput = false;
        m_objPlayer.GetComponent<Movement>().IsMoving = m_bisInput;
    }
    public void ControlJoyStick(Vector2 vTouch)
    {
        Vector2 vec = new Vector2(vTouch.x - m_rectTransform.position.x, vTouch.y - m_rectTransform.position.y); // 터치되는 부분과 원점과의 차의 벡터를 구한다.

        vec = Vector2.ClampMagnitude(vec, m_fControllerRange); // vec값을 m_fRadious이상이 되지 않도록 한다.
        m_rectController.localPosition = vec;

        float fsqr = (m_rectTransform.position - m_rectController.position).sqrMagnitude / (m_fControllerRange * m_fControllerRange);
        Vector2 vNormal = vec.normalized;

        m_vMove = new Vector3(vNormal.x , 0f , vNormal.y );
        m_objPlayer.transform.eulerAngles = new Vector3(0f, Mathf.Atan2(vNormal.x, vNormal.y) * Mathf.Rad2Deg, 0f);
    }

    private void Start()
    {
        Debug.Log("pp");
    }
    // Update is called once per frame
    void Update()
    {
        if (m_objPlayer == null)
        {
            m_objPlayer = GameManager.instance.Player;
            m_fSpeed = m_objPlayer.GetComponent<PlayerStatus>().AplyStat.MovSpd;
        }
        else if (m_bisInput == true)
        {
            m_objPlayer = GameManager.instance.Player;
            m_objPlayer.transform.position += m_vMove * m_fSpeed *Time.deltaTime;
            //m_objPlayer.transform.position += transform.forward * m_fSpeed * Time.deltaTime;
        }    
           
    }
}
