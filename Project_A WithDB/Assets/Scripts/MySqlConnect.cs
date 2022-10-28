using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MysqlConnetionWrapper;
using MySql.Data.MySqlClient;

public class MySqlConnect: MonoBehaviour
{
    MysqlLinker mysqlLinker = new MysqlLinker();
    public string m_strServerIP = "127.0.0.1";
    public string m_strPort = "3306";
    public string m_strDB = "item";
    public string m_strUser = "root";
    public string m_strPSWD = "fkrmsk2472!)";
    public string m_strTable = "`playerstatustablea`";
    string m_strSelectString = "none";

    Vector2 vScrollPos;



    //테스트용 샘플.
    private void OnGUI()
    {
        if (PlayerUi.instance.m_bIsStart == true)
        {
            mysqlLinker.Connect(m_strServerIP, m_strPort, m_strDB, m_strUser, m_strPSWD);
            m_strTable = "`playerstatustable`";
            PlayerStatus playerstatus = GameManager.instance.Player.GetComponent<PlayerStatus>();
            string strQuery = MakeQuery.Select("Str", m_strTable);
            playerstatus.Str = mysqlLinker.SelectToInt(strQuery);
            strQuery = MakeQuery.Select("Agi", m_strTable);
            playerstatus.Agi = mysqlLinker.SelectToInt(strQuery);
            strQuery = MakeQuery.Select("Vit", m_strTable);
            playerstatus.Vit = mysqlLinker.SelectToInt(strQuery);
            strQuery = MakeQuery.Select("Dex", m_strTable);
            playerstatus.Dex = mysqlLinker.SelectToInt(strQuery);
            strQuery = MakeQuery.Select("Luk", m_strTable);
            playerstatus.Luk = mysqlLinker.SelectToInt(strQuery);
            strQuery = MakeQuery.Select("StatPoint", m_strTable);
            playerstatus.StatPoint = mysqlLinker.SelectToInt(strQuery);
            strQuery = MakeQuery.Select("BaseLevel", m_strTable);
            playerstatus.CurBaseLevel = mysqlLinker.SelectToInt(strQuery);
            strQuery = MakeQuery.Select("JobLevel", m_strTable);
            playerstatus.CurJobLevel = mysqlLinker.SelectToInt(strQuery);
            PlayerUi.instance.m_bIsStart = false;
        }
        GUI.color = Color.white;
        if (mysqlLinker.CheckConnet) GUI.color = Color.red;
        if (GUI.Button(new Rect(100, 50, 100, 20), string.Format("Close")))
        {
            mysqlLinker.Close();
        }
        GUI.color = Color.white;
        if (mysqlLinker.CheckConnet)
        {
            PlayerStatus playerstatus = GameManager.instance.Player.GetComponent<PlayerStatus>();
            if (GUI.Button(new Rect(0, 90, 200, 20), string.Format("Insert")))
            {
                m_strTable = "`playerstatustable`";
                string strQuery = MakeQuery.Insert
                    (m_strTable,
                    "`Agi`","'10'");
                int nResult = mysqlLinker.CheckMode(strQuery);
                Debug.Log("INSERT INTO[" + nResult + "]:" + strQuery);

            }
            if (PlayerUi.instance.m_bIsSetButtonClick == true)
            {
                m_strTable = "`playerstatustable`";
                string strQuery = MakeQuery.Update(m_strTable,
                "`Str`= " + " ' " + playerstatus.Str.ToString() + " ' " + "," +
                "`Agi`= " + " ' " + playerstatus.Agi.ToString() + " ' " + "," +
                "`Vit`= " + " ' " + playerstatus.Vit.ToString() + " ' " + "," +
                "`Dex`= " + " ' " + playerstatus.Dex.ToString() + " ' " + "," +
                "`Luk`= " + " ' " + playerstatus.Luk.ToString() + " ' " + "," +
                "`StatPoint`= " + " ' " + playerstatus.StatPoint.ToString() + " ' " + "," +
                "`BaseLevel`= " + " ' " + playerstatus.CurBaseLevel.ToString() + " ' " + "," +
                "`JobLevel`= " + " ' " + playerstatus.CurJobLevel.ToString() + " ' "
                , "`id` <= " +  " ' " + playerstatus.Agi.ToString() + " ' ");
                int nResult = mysqlLinker.CheckMode(strQuery);
                Debug.Log("Update[" + nResult + "]:" + strQuery);
                PlayerUi.instance.m_bIsSetButtonClick = false;
            }

            if (playerstatus.IsBaseLevelUp == true)
            {
                m_strTable = "`playerstatustable`";
                string strQuery = MakeQuery.Update(m_strTable,
                    "`BaseLevel`= " + " ' " + playerstatus.CurBaseLevel.ToString() + " ' "
                    , "`id` <= " + " ' " + playerstatus.Agi.ToString() + " ' ");
                    int nResult = mysqlLinker.CheckMode(strQuery);
                Debug.Log("Update[" + nResult + "]:" + strQuery);
                playerstatus.IsBaseLevelUp = false;
            }
            if (playerstatus.IsJobLevelUp == true)
            {
                m_strTable = "`playerstatustable`";
                string strQuery = MakeQuery.Update(m_strTable,
                    "`JobLevel`= " + " ' " + playerstatus.CurJobLevel.ToString() + " ' "
                    , "`id` <= " + " ' " + playerstatus.Agi.ToString() + " ' ");
                    int nResult = mysqlLinker.CheckMode(strQuery);
                Debug.Log("Update[" + nResult + "]:" + strQuery);
                playerstatus.IsBaseLevelUp = false;
            }

            if (GUI.Button(new Rect(0, 130, 200, 20), string.Format("Select")))
            {
                //작동은 가능하나 지역변수일때만 데이터리더를 스트링으로 변환가능함.//
                //1.멤버변수에 객체를 저장해서 다른 지역에서 사용할때 Read에서 문제발생
                //mySqlDataReader = mysqlLinker.SelectMode(MakeQuery.Select("*", "`members`"));
                //m_strSelectString = mysqlLinker.DataReaderToString(mySqlDataReader);
                //////////////////////////////////////////////////////////////////////////////
                string strQuery = MakeQuery.Select("*", m_strTable);
                //위와 같은 문제가 있어 통합용 함수를 따로 작성함. 요것을 사용하길 권장함.
                m_strSelectString = mysqlLinker.SelectToString(strQuery);
                Debug.Log("Select:" + strQuery);
            }
        }

        //if (mysqlLinker.CheckConnet)
        //{
        //    if (GUI.Button(new Rect(0, 70, 200, 20), string.Format("CreateTable")))
        //    {
        //        string strCreateTable = "CREATE TABLE Members (" +
        //                                "ID int(6) PRIMARY KEY AUTO_INCREMENT," +
        //                                "NAME VARCHAR(30) NOT NULL," +
        //                                "PSWD VARCHAR(30) NOT NULL," +
        //                                "GOLD int(255)," +
        //                                "BESTSCORE int(255)," +
        //                                "LOGIN int(1)" + ")";

        //        int nResult = mysqlLinker.CheckMode(strCreateTable);
        //        Debug.Log("Create Table:" + nResult);
        //    }
        //    if (GUI.Button(new Rect(0, 90, 200, 20), string.Format("Insert")))
        //    {
        //        string strQuery = MakeQuery.Insert("`members`", "`NAME`, `PSWD`, `GOLD`", "'SBSGAME','SBSPSWD','1'");
        //        int nResult = mysqlLinker.CheckMode(strQuery);
        //        Debug.Log("INSERT INTO[" + nResult + "]:" + strQuery);
        //    }
        //    if (GUI.Button(new Rect(0, 110, 200, 20), string.Format("Update")))
        //    {
        //        string strQuery = MakeQuery.Update("`members`", "`LOGIN`=0", "`NAME`= 'Test'");
        //        int nResult = mysqlLinker.CheckMode(strQuery);
        //        Debug.Log("Update[" + nResult + "]:" + strQuery);
        //    }
        //    if (GUI.Button(new Rect(0, 130, 200, 20), string.Format("Select")))
        //    {
        //        //작동은 가능하나 지역변수일때만 데이터리더를 스트링으로 변환가능함.//
        //        //1.멤버변수에 객체를 저장해서 다른 지역에서 사용할때 Read에서 문제발생
        //        //mySqlDataReader = mysqlLinker.SelectMode(MakeQuery.Select("*", "`members`"));
        //        //m_strSelectString = mysqlLinker.DataReaderToString(mySqlDataReader);
        //        //////////////////////////////////////////////////////////////////////////////
        //        string strQuery = MakeQuery.Select("*", "`members`");
        //        //위와 같은 문제가 있어 통합용 함수를 따로 작성함. 요것을 사용하길 권장함.
        //        m_strSelectString = mysqlLinker.SelectToString(strQuery);
        //        Debug.Log("Select:" + strQuery);
        //    }
        //}

        if (m_strSelectString != null)
        {
           
            int size = m_strSelectString.Split(new char[] { '\n' }).Length;
            vScrollPos = GUI.BeginScrollView(new Rect(200, 0, 200, 150), vScrollPos, new Rect(200, 0, 200, 20 * size));;
            GUI.Box(new Rect(200, 0, 300, 20 * size), m_strSelectString);
            GUI.EndScrollView();
        }

        if (mysqlLinker.GetLastException() != null)
        {
            GUI.color = Color.red;
            GUI.Box(new Rect(0, 150, 700, 400), mysqlLinker.GetLastException().ToString());
            GUI.color = Color.gray;
        }
    }
}
