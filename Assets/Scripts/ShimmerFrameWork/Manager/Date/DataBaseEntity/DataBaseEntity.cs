using System;

namespace ShimmerFramework
{
    /// <summary>
    /// 数据库实体
    /// </summary>
    [Serializable]
    public class DataBaseEntity
    {
        public string tableName;

        //数据库存储的数据类型
        public DataEntityBase dataEntity;
    }
}
