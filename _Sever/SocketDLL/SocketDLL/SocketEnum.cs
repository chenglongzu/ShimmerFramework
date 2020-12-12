
namespace SocketDLL
{
    /// <summary>
    /// Socket相关消息头.
    /// </summary>
    public enum MessageHead
    {
        /// <summary>
        /// 客户端登录请求.
        /// </summary>
        CS_Login,
        /// <summary>
        /// 服务器端登录响应.
        /// </summary>
        SC_Login,
        /// <summary>
        /// 新用户进入主城.
        /// </summary>
        CS_PlayerEnterCity,
        /// <summary>
        /// 客户端创建新角色.
        /// </summary>
        SC_CreateNewPlayer,
        /// <summary>
        /// 客户端创建所有角色.
        /// </summary>
        SC_CreateAllPlayer,
        /// <summary>
        /// 客户端移动请求.
        /// </summary>
        CS_PlayerMove,
        /// <summary>
        /// 服务器端移动广播.
        /// </summary>
        SC_PlayerMove,
        /// <summary>
        /// 客户端退出请求.
        /// </summary>
        CS_Exit,
        /// <summary>
        /// 服务器端群发x用户退出消息.
        /// </summary>
        SC_Exit,
        /// <summary>
        /// 客户端请求进入竞技场.
        /// </summary>
        CS_EnterArena,
        /// <summary>
        /// 服务器端返回竞技场消息.
        /// </summary>
        SC_EnterArena,
        /// <summary>
        /// 服务器端角色退出主城消息.
        /// </summary>
        SC_ExitCity,
        /// <summary>
        /// 客户端新角色进入竞技场.
        /// </summary>
        CS_NewPlayerEnterArena,
        /// <summary>
        /// 服务器端返回竞技场角色数据.
        /// </summary>
        SC_NewPlayerEnterArena,
        /// <summary>
        /// 客户端竞技场角色移动.
        /// </summary>
        CS_ArenaPlayerMove,
        /// <summary>
        /// 服务器端竞技场角色移动同步.
        /// </summary>
        SC_ArenaPlayerMove,
        /// <summary>
        /// 客户端竞技场角色攻击.
        /// </summary>
        CS_ArenaPlayerAttack,
        /// <summary>
        /// 服务器端竞技场角色攻击同步.
        /// </summary>
        SC_ArenaPlayerAttack,
        /// <summary>
        /// 客户端伤害请求.
        /// </summary>
        CS_Hit,
        /// <summary>
        /// 服务器端伤害判定.
        /// </summary>
        SC_Hit,
        /// <summary>
        /// 客户端输入请求
        /// </summary>
        CS_Input,
        /// <summary>
        /// 服务器输入请求
        /// </summary>
        SC_Input
    }

    /// <summary>
    /// 常用输入按键
    /// </summary>
    public enum KeyCode
    {
        /// <summary>
        /// W
        /// </summary>
        W,
        /// <summary>
        /// A
        /// </summary>
        A,
        /// <summary>
        /// S
        /// </summary>
        S,
        /// <summary>
        /// D
        /// </summary>
        D,

        /// <summary>
        /// 鼠标左键
        /// </summary>
        Zero,
        /// <summary>
        /// 鼠标右键
        /// </summary>
        One,
        /// <summary>
        /// 鼠标滚轮
        /// </summary>
        Two
    }

    /// <summary>
    /// 按键状态
    /// </summary>
    public enum KeyCodeState
    {
        /// <summary>
        /// 点击
        /// </summary>
        Click,
        /// <summary>
        /// 按下
        /// </summary>
        Pressed,
        /// <summary>
        /// 抬起
        /// </summary>
        Up
    }


}
