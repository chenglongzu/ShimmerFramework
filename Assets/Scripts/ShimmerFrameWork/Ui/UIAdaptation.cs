using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ShimmerFramework
{
    /// <summary>
    /// ������canvas����� ��ȡcanvasSclaer���
    /// </summary>
    public class UIAdaptation : MonoBehaviour
    {
        void Start()
        {
            //ͨ����ȡ������жϵ�ǰ��Ļ�ķֱ���
            CanvasScaler canvasScaler = transform.GetComponent<CanvasScaler>();
            if ((float)Screen.width / (float)Screen.height > 1920f / 1080f)
            {
                //������Ļ�ĸ߶������Ż���
                canvasScaler.matchWidthOrHeight = 1;
            }
            else if ((float)Screen.width / (float)Screen.height < 1920f / 1080f)
            {
                //������Ļ�Ŀ�������Ż���
                canvasScaler.matchWidthOrHeight = 0;
            }

            StartCoroutine(PosAdaptation());
        }

        /// <summary>
        ///Эͬ���� ��UI���崫���޸�ÿһ��UI�����λ��
        /// </summary>
        /// <param name="UIWindow"></param>
        /// <returns></returns>
        IEnumerator PosAdaptation()
        {
            yield return null;

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform Child = gameObject.transform.GetChild(i);
                Child.localPosition = new Vector2(Child.localPosition.x * (transform.GetComponent<RectTransform>().rect.width / 1920f), Child.localPosition.y * (transform.GetComponent<RectTransform>().rect.height / 1080f));
            }

        }

    }
}