using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysqlConnetionWrapper
{
    public static class Sample
    {
        //기본 msyqlConect사용 예제 문젝 생길경우 확인할것!
        static void PureMysqlConnet()
        {
            string strConnect = "Server=localhost;Database=test;Uid=root;Pwd=autoset;";

            MySqlConnection cConnect = new MySqlConnection(strConnect);

            if (cConnect != null)
            {
                string strSql = "CREATE TABLE Members (" +
                                    "ID int(6) PRIMARY KEY AUTO_INCREMENT," +
                                    "NAME VARCHAR(30) NOT NULL," +
                                    "PSWD VARCHAR(30) NOT NULL," +
                                    "GOLD int(255)," +
                                    "BESTSCORE int(255)," +
                                    "LOGIN int(1)" +
                                ")";
                cConnect.Open();
                MySqlCommand cmd = new MySqlCommand(strSql, cConnect);
                //MySqlCommand cmd = new MySqlCommand()
                //Console.WriteLine("CREATE TABLE");
                //cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO `members`(`NAME`, `PSWD`) VALUES ('SBSGAME','SBSPSWD')";
                int status = cmd.ExecuteNonQuery();
                Console.WriteLine("INSERT INTO:" + status);
                cmd.CommandText = "UPDATE `members` SET `LOGIN`=0 WHERE `NAME`= 'SBSGAME' ";
                status = cmd.ExecuteNonQuery();
                Console.WriteLine("UPDATE:" + status);
                cmd.CommandText = "UPDATE `members` SET `LOGIN`=0 WHERE `NAME`= 'Test' ";
                status = cmd.ExecuteNonQuery();
                Console.WriteLine("UPDATE:" + status);

                cmd.CommandText = "SELECT * FROM `members`";
                MySqlDataReader dataReader = cmd.ExecuteReader();

                string strResult = string.Empty;
                if (dataReader == null) strResult = "No Return";
                else
                {
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            if (i != dataReader.FieldCount - 1)
                                strResult += dataReader[i] + ";";
                            else //if (i == dataReader.FieldCount - 1)
                                strResult += dataReader[i] + "\n";
                        }
                    }
                }
                Console.WriteLine(strResult);
                cConnect.Close();
            }
            else
                Console.WriteLine("Mysql Connect Failed!");
        }
        //기본사용방법 예. 참조해서 사용할것!
        static public void MysqlConnterWrapper()
        {
            MysqlLinker mysqlLinker = new MysqlLinker();

            mysqlLinker.Connect("localhost", "test", "root", "autoset");
            //mysqlLinker.Connect(MakeQuery.Connection("localhost", "test", "root", "autoset"));
            //mysqlLinker.Connect("Server=localhost;Database=test;Uid=root;Pwd=autoset;");

            string strCreateTable = "CREATE TABLE Members (" +
                                        "ID int(6) PRIMARY KEY AUTO_INCREMENT," +
                                        "NAME VARCHAR(30) NOT NULL," +
                                        "PSWD VARCHAR(30) NOT NULL," +
                                        "GOLD int(255)," +
                                        "BESTSCORE int(255)," +
                                        "LOGIN int(1)" +
                                    ")";

            int nResult = mysqlLinker.CheckMode(strCreateTable);
            Console.WriteLine("Create Table:" + nResult);
            //nResult = mysqlLinker.CheckMode("INSERT INTO `members`(`NAME`, `PSWD`) VALUES ('SBSGAME','SBSPSWD')");
            nResult = mysqlLinker.CheckMode(MakeQuery.Insert("`members`", "`NAME`, `PSWD`", "'SBSGAME','SBSPSWD'"));
            Console.WriteLine("INSERT INTO:" + nResult);
            nResult = mysqlLinker.CheckMode(MakeQuery.Update("`members`", "`LOGIN`=0", "`NAME`= 'Test'"));
            Console.WriteLine("UPDATE:" + nResult);
            //################## 해당코드는 작동에 문제가 있음. 이러한 형식으로는 사용하지 말 것! ##################
            //MySqlDataReader mySqlDataReader = mysqlLinker.SelectMode(MakeQuery.Select("*", "`members`"));
            //string strResult = mysqlLinker.DataReaderToString(mySqlDataReader);\
            //#################################################################################################
            string strResult = mysqlLinker.SelectToString(MakeQuery.Select("*", "`members`"));
            Console.WriteLine("##### SelectResult ####\n", strResult);

            mysqlLinker.Close();
        }
    }
    /// <summary>
    /// DB를 사용할때는 접속을 연속으로 유지하는 것보다 웹처럼 단일 엑세스하고 끊는것이 서버성능을 위해 좋음.
    /// 1.아래 함수에서는 Connect로 접속하고, 쿼리함수를 사용하고 사용이 끝나면 닫는것을 권장한다.
    /// ex) Conect->Insert->Close
    /// 2.다만, 사용상 쿼리를 다중처리해야하는 구간을 고려하여 Conect와 Close는 각 쿼리함수에 포함시키지않았음.
    /// ex) Connect->Insert->Updata->Select->Close 
    /// 3.GetLastException()이용하여 마지막에 동작 실패한 Exception을 반환한다. 다른 작업이 성공하면 값은 null이 된다.
    /// </summary>
    public class MysqlLinker
    { 
        MySqlConnection m_mySqlConnection;
        Exception m_eLastException; //익셉션이 일어나면 마지막 익셉션을 저장한다.
        bool m_bConnect = false;

        public bool CheckConnet
        {
            get { return m_bConnect;  }
        }

        public Exception GetLastException()
        {
            return m_eLastException;
        }

        public void Connect(string ip, string port, string db, string id, string pswd)
        {
            try
            {
                string strConnect = MakeQuery.Connection(ip, port, db, id, pswd);
                m_mySqlConnection = new MySqlConnection(strConnect);
                m_mySqlConnection.Open();
                m_eLastException = null;
                m_bConnect = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Connect Failed!\n{0}", e);
                m_eLastException = e;
            }
        }

        public void Connect(string ip, string db, string id, string pswd)
        {
            try
            {
                string strConnect = MakeQuery.Connection(ip, db, id, pswd);
                m_mySqlConnection = new MySqlConnection(strConnect);
                m_mySqlConnection.Open();
                m_eLastException = null;
                m_bConnect = true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Connect Failed!\n{0}", e);
                m_eLastException = e;
            }
        }

        public void Connect(string strConnect)
        {
            try
            {
                m_mySqlConnection = new MySqlConnection(strConnect);
                m_mySqlConnection.Open();
                m_bConnect = true;
                m_eLastException = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(" Connect Failed!\n{0}", e);
                m_eLastException = e;
            }
        }
        //업데이트횟수를 반환한다.
        public int CheckMode(string query)
        {
            try
            {
                MySqlCommand dbcmd = new MySqlCommand(query, m_mySqlConnection); //명령어를 커맨드에 입력
                m_eLastException = null;
                return dbcmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(" CheckMode!\n{0}", e);
                m_eLastException = e;
                return -1;
            }
        }
        //사용은 가능하나 리턴받은 데이터리더가 지역에서만 스트링변환이 가능함.
        //SelectModeToString()함수를 이용하여 문자열로 받아서 사용하면 동일한 기능을함.
        public MySqlDataReader SelectMode(string query)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(query, m_mySqlConnection);
                m_eLastException = null;
                return command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(" SelectMode!\n{0}", e);
                m_eLastException = e;
                return null;
            }
        }
        //SelectMode가 일정상황에 불안정하게 작동하여, 통합하여 제공됨.
        public string SelectToString(string query)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(query, m_mySqlConnection);
                m_eLastException = null;
                MySqlDataReader dataReader = command.ExecuteReader();
                return DataReaderToString(dataReader);
            }
            catch (Exception e)
            {
                Console.WriteLine(" SelectMode!\n{0}", e);
                m_eLastException = e;
                return "none";
            }
        }
        public int SelectToInt(string query)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(query, m_mySqlConnection);
                m_eLastException = null;
                MySqlDataReader dataReader = command.ExecuteReader();
                return DataReaderToInt(dataReader);
            }
            catch (Exception e)
            {
                Console.WriteLine(" SelectMode!\n{0}", e);
                m_eLastException = e;
                return 0;
            }
        }

        public string DataReaderToString(MySqlDataReader dataReader)
        {
            try
            {
                //DataReader 값을 파싱
                string strResult = string.Empty;
                if (dataReader == null) strResult = "No Return";
                else
                {
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            if (i != dataReader.FieldCount - 1)
                                strResult += dataReader[i] + ";";
                            else if (i == dataReader.FieldCount - 1)
                                strResult += dataReader[i] + "\n";
                        }
                    }
                }
                m_eLastException = null;
                dataReader.Close();//리더를 사용 후 클로즈하지않으면 제대로 작동하지않음.
                return strResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(" ToString()\n{0}", e);
                m_eLastException = e;
                return "none";
            }
        }
        public int DataReaderToInt(MySqlDataReader dataReader)
        {
            try
            {
                //DataReader 값을 파싱
                int data = 0;
                if (dataReader == null) data = 0;
                else
                {
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            if (i != dataReader.FieldCount - 1)
                                data += (int)dataReader[i];
                            else if (i == dataReader.FieldCount - 1)
                                data += (int)dataReader[i];
                        }
                    }
                }
                m_eLastException = null;
                dataReader.Close();//리더를 사용 후 클로즈하지않으면 제대로 작동하지않음.
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(" ToString()\n{0}", e);
                m_eLastException = e;
                return 0;
            }
        }

        public void Close()
        {
            m_mySqlConnection.Close();
            m_mySqlConnection = null;
            m_eLastException = null;
            m_bConnect = false;
        }
    }

    //쿼리를 만드는데 도움을 주는 함수. 사용은 꼭하지않아도됨.
    public static class MakeQuery
    {
        static public string Connection(string ip, string db, string id, string pswd)
        {
            return string.Format("Server={0};Database={1};UserId={2};Password={3};", ip, db, id, pswd);
        }

        static public string Connection(string ip, string port, string db, string id, string pswd)
        {
            return string.Format("Server={0};Port={1};Database={2};UserId={3};Password={4};", ip, port, db, id, pswd);
        }


        static public string Insert(string table, string filed, string values)
        {
            return string.Format("INSERT INTO {0}({1}) VALUES ({2})", table, filed, values);
        }

        static public string Update(string table, string set, string where)
        {
            return string.Format("UPDATE {0} SET {1} WHERE {2} ", table, set, where);
        }

        static public string Select(string select, string from, string where = "1")
        {
            return string.Format("SELECT {0} FROM {1} WHERE {2}", select, from, where);
        }
    }
}
