using UnityEngine;
using ShimmerSqlite;

public class SqliteInitController : MonoBehaviour
{
    [SerializeField]
    private string dataBaseName;

    void Start()
    {
        SqlManager.GetInstance().InitDataBase(dataBaseName);
    }
}
