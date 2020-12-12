using UnityEngine;
using ShimmerSqlite;

public class SqliteInitManager : MonoBehaviour
{
    private string gameDataBase;
    private string tableName;

    void Start()
    {
        SqlManager.GetInstance().InitDataBase(gameDataBase);
        SqlManager.GetInstance().CreateTable(tableName,new Player());
        SqlManager.GetInstance().PrintValueInDataBase(tableName);
    }
}
