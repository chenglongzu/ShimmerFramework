using UnityEngine;
using MySql.Data.MySqlClient;
using ShimmerFramework;

namespace ShimmerNote
{
    /// <summary>
    /// Mysql的增删改查操作
    /// </summary>
    public class MySqlManager : BaseManager<MySqlManager>
    {
        string sqlInfo = "sever=127.0.0.1;port=3306;datebase=game;user=root;passward=123456";

        //链接数据库
        public MySqlConnection ConnectMysql()
        {
            MySqlConnection mysqlConnnection = new MySqlConnection(sqlInfo);//连接数据库
            return mysqlConnnection;
        }

        //增加数据
        public void InsertDate()
        {
            MySqlConnection mysqlConnection = ConnectMysql();//首先实例化生成MysqlConnection的对象

            string addCommmad = "insert into user(Id,account,password) value(1005,\"a777\",555555)";
            MySqlCommand mySqlCommand = new MySqlCommand(addCommmad, mysqlConnection);//实例化生成MySqlCommand的对象，通过sql语句和数据库链接对象进行初始化。

            mysqlConnection.Open();//通过数据库的链接对象打开数据库，然后才可获得数据库的相关信息
            int resoult = mySqlCommand.ExecuteNonQuery();//获得数据库命令的返回结果的次数

            //如果次数是大于0的则添加成功
            if (resoult > 0)
            {
                Debug.Log("添加成功");
            }
            else
            {
                Debug.Log("添加失败");
            }
            //在方法的结尾关闭数据库的链接对象
            mysqlConnection.Close();
        }

        //删除数据
        public void DeleteDate()
        {
            MySqlConnection mySqlConnection = ConnectMysql();

            string deleteCommand = "delete from user where account=\"hongqigong\" and password=555 and Id=1004";
            MySqlCommand mySqlCommand = new MySqlCommand(deleteCommand, mySqlConnection);

            mySqlConnection.Open();
            int resoult = mySqlCommand.ExecuteNonQuery();

            if (resoult > 0)
            {
                Debug.Log("删除成功");
            }
            else
            {
                Debug.Log("删除失败");
            }
            mySqlConnection.Close();
        }

        //修改数据
        public void UpdateDate()
        {
            MySqlConnection mySqlConnection = ConnectMysql();

            string updateCommand = "update user set password=555 where account=\"hongqigong\" and password=444 and Id=1004";
            MySqlCommand mySqlCommand = new MySqlCommand(updateCommand, mySqlConnection);

            mySqlConnection.Open();
            int resoult = mySqlCommand.ExecuteNonQuery();

            if (resoult > 0)
            {
                Debug.Log("删除成功");
            }
            else
            {
                Debug.Log("删除失败");
            }
            mySqlConnection.Close();

        }

        //查询数据
        public void SelectDate()
        {
            MySqlConnection mySqlConnection = ConnectMysql();//首先实例化mysql数据库的链接对象

            string inqureCommand = "select *from user";//查询数据的sql语句
            MySqlCommand mySqlCommand = new MySqlCommand(inqureCommand, mySqlConnection);//实例化生成mysqlcommand对象

            mySqlConnection.Open();//打开mysql数据库的链接对象

            MySqlDataReader resoultDate = mySqlCommand.ExecuteReader();

            while (resoultDate.Read())
            {
                string account = resoultDate.GetString("account");
                int id = resoultDate.GetInt32("Id");
                int passward = resoultDate.GetInt32("password");
            }

            resoultDate.Close();
            mySqlConnection.Close();

        }

    }
}