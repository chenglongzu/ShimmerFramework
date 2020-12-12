namespace ShimmerSqlite
{
    public class Player : DataBase
    {
        public string passward;

        public Player()
        {
            this.account = "";
            this.passward = "123456";
        }

        public Player(string account, string passward)
        {
            this.account = account;
            this.passward = passward;
        }

        //输出变量的值
        public override object[] DataToArray()
        {
            return new object[] { account, passward};
        }

        //输出变量的名称
        public override string[] NameToArray()
        {
            return new string[] { "account", "passward"};
        }

        //输出变量的类型
        public override string[] TypeToArray()
        {
            return new string[] { "string", "string" };
        }
    }
}