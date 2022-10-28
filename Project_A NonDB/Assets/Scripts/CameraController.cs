using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    CameraController instance = null;

    [SerializeField]
    Transform m_tranPlayer;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
       // m_tranPlayer = GameManager.instance.Player.transform;
    }

    private void LateUpdate()
    {
        if(m_tranPlayer != null)
        transform.position = m_tranPlayer.position + new Vector3(0,6,-3);
    }
    // Update is called once per frame
    void Update()
    {
        m_tranPlayer = GameManager.instance.Player.transform;
    }
}
