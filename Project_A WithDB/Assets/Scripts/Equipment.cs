using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField]
    GameObject m_objSword;
    [SerializeField]
    GameObject m_objHand;
    Vector3 m_vWeaponPos;

    void Start()
    {
        
        m_vWeaponPos = new Vector3(0.1f, 0.13f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_objSword = Instantiate(Resources.Load("Prefabs/Sword") as GameObject, m_objHand.transform);
           
        }
    }
}
