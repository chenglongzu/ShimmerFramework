namespace ShimmerSqlite
{
    public class Player : DataBase
    {
        public string account;
        public string passward;

        public Player()
        {
            this.id = 0;
            this.account = "Shimmer";
            this.passward = "123456";
        }

        public Player(int id,string account, string passward)
        {
            this.id = id;
            this.account = account;
            this.passward = passward;
        }

        //输出变量的值
        public override object[] DataToArray()
        {
            return new object[] { id,account, passward};
        }

        //输出变量的名称
        public override string[] NameToArray()
        {
            return new string[] { "id", "account", "passward" };
        }

        //输出变量的类型
        public override string[] TypeToArray()
        {
            return new string[] { "int","string","string" };
        }
    }
}