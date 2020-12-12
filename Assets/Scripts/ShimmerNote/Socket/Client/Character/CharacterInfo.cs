using UnityEngine;
using UnityEngine.UI;
using SocketDLL.Message;

namespace ShimmerNote
{
    public class CharacterInfo : MonoBehaviour
    {
        private Transform m_Transform;
        private Image image_Face;
        private Text text_Name;
        private Text text_Level;
        private Text text_HP;
        private Image image_Blood;

        void Awake()
        {
            FindInit();
        }

        /// <summary>
        /// UI查找初始化.
        /// </summary>
        private void FindInit()
        {
            m_Transform = gameObject.GetComponent<Transform>();
            image_Face = m_Transform.Find("UserFace").GetComponent<Image>();
            text_Name = m_Transform.Find("UserName").GetComponent<Text>();
            text_Level = m_Transform.Find("UserLevel").GetComponent<Text>();
            text_HP = m_Transform.Find("UserBlood/BloodValue").GetComponent<Text>();
            image_Blood = m_Transform.Find("UserBlood/BloodBar").GetComponent<Image>();
        }

        /// <summary>
        /// UI数据初始化.
        /// </summary>
        /// <param name="userData"></param>
        public void DataInit(UserData userData)
        {
            image_Face.sprite = Resources.Load<Sprite>("Face/" + userData.FaceName);
            text_Name.text = userData.UserName;
            text_Level.text = userData.Level.ToString();
            text_HP.text = userData.HP.ToString();
        }

        /// <summary>
        /// 设置血条相关信息.
        /// </summary>
        public void SetBloodInfo(int hp, float blood)
        {
            text_HP.text = hp.ToString();
            image_Blood.fillAmount = blood;
        }

    }
}