using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    Scene m_sceneNow;
    static public SceneChange instance = null;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
            instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject m_objPlayer = other.gameObject;
            if (m_sceneNow.name == "Field")
            {
                SceneManager.LoadScene("Town");
                m_objPlayer.GetComponent<Movement>().IsMoving = false;
                m_objPlayer.transform.position = new Vector3(0, m_objPlayer.transform.position.y, -20);
                this.transform.position = new Vector3(0, this.transform.position.y, -23);
                JoyStick.instance.m_rectController.anchoredPosition = Vector2.zero;
                JoyStick.instance.IsInput = false;
            }
            else if(m_sceneNow.name == "Town")
            {
                SceneManager.LoadScene("Field"); 
                m_objPlayer.GetComponent<Movement>().IsMoving = false;
                m_objPlayer.transform.position = new Vector3(0, m_objPlayer.transform.position.y, 20);
                this.transform.position = new Vector3(0, this.transform.position.y, 23);
                JoyStick.instance.m_rectController.anchoredPosition = Vector2.zero;
                JoyStick.instance.IsInput = false;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_sceneNow = SceneManager.GetActiveScene();

    }
}
