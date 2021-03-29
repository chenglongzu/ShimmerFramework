using ShimmerFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ShimmerHotUpdate
{
    public class AssetBundleManager : BaseManager<AssetBundleManager>
    {
        private AssetBundle mainAb = null; //主包的引用，避免二次加载

        private AssetBundleManifest manifest = null; //主包的信息文件


        private string PathUrl
        {
            get
            {
                return Application.streamingAssetsPath + "/";
            }
        }     //AB包的路径

        //获得当前的平台
        private string MainAbName
        {
            get
            {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else
                return "PC";
#endif
            }
        }  //主包的名称，通过判断不同平台来返回不同的值

        private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>(); //AB包的字典容器


        #region 同步加载AB包资源
        public Object LoadResources(string abName, string resName)
        {
            GetDependsAsset(abName);

            Object obj = abDic[abName].LoadAsset(resName);

            if (obj is GameObject)
            {
                return MonoManager.GetInstance().InstantiationGameobject((GameObject)obj);
            }
            else
            {
                return obj;
            }
        }

        public Object LoadResources(string abName, string resName, System.Type type)
        {
            GetDependsAsset(abName);

            Object obj = abDic[abName].LoadAsset(resName, type);

            if (obj is GameObject)
            {
                return MonoManager.GetInstance().InstantiationGameobject((GameObject)obj);
            }
            else
            {
                return obj;
            }
        }

        public T LoadResources<T>(string abName, string resName) where T : Object
        {

            GetDependsAsset(abName);

            T obj = abDic[abName].LoadAsset<T>(resName);

            if (obj is GameObject)
            {
                return MonoManager.GetInstance().InstantiationGameobject(obj as GameObject) as T;
            }
            else
            {
                return obj;
            }
        }
        #endregion

        #region 异步加载资源
        public void LoadResourcesAsync(string abName, string resName, UnityAction<Object> callBack)
        {
            MonoManager.GetInstance().StartCoroutine(ReallyLoadResourcesAsync(abName, resName, callBack));
        }
        private IEnumerator ReallyLoadResourcesAsync(string abName, string resName, UnityAction<Object> callBack)
        {
            GetDependsAsset(abName);

            AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);

            yield return abr;

            if (abr.asset is GameObject)
            {
                callBack(MonoManager.GetInstance().InstantiationGameobject((GameObject)abr.asset));
            }
            else
            {
                callBack(abr.asset);
            }
        }

        public void LoadResourcesAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
        {
            MonoManager.GetInstance().StartCoroutine(ReallyLoadResourcesAsync(abName, resName, type, callBack));
        }
        private IEnumerator ReallyLoadResourcesAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
        {
            GetDependsAsset(abName);

            AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName, type);

            yield return abr;

            if (abr.asset is GameObject)
            {
                callBack(MonoManager.GetInstance().InstantiationGameobject((GameObject)abr.asset));
            }
            else
            {
                callBack(abr.asset);
            }
        }

        public void LoadResourcesAsync<T>(string abName, string resName, UnityAction<Object> callBack)
        {
            MonoManager.GetInstance().StartCoroutine(ReallyLoadResourcesAsync<T>(abName, resName, callBack));
        }
        private IEnumerator ReallyLoadResourcesAsync<T>(string abName, string resName, UnityAction<Object> callBack)
        {
            GetDependsAsset(abName);

            AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);

            yield return abr;

            if (abr.asset is GameObject)
            {
                callBack(MonoManager.GetInstance().InstantiationGameobject((GameObject)abr.asset));
            }
            else
            {
                callBack(abr.asset);
            }
        }
        #endregion

        #region 包管理
        //获取依赖包
        private void GetDependsAsset(string abName)
        {
            if (mainAb == null)
            {
                mainAb = AssetBundle.LoadFromFile(PathUrl + MainAbName);
                manifest = mainAb.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }

            AssetBundle ab = null;
            string[] strs = manifest.GetAllDependencies(abName);

            for (int i = 0; i < strs.Length; i++)
            {
                if (!abDic.ContainsKey(strs[i]))
                {
                    ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                    abDic.Add(abName, ab);
                }
            }

            if (!abDic.ContainsKey(abName))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + abName);
                abDic.Add(abName, ab);
            }

        }

        //单个包卸载
        public void UnLoad(string abName)
        {
            if (abDic.ContainsKey(abName))
            {
                abDic[abName].Unload(false);
                abDic.Remove(abName);
            }
        }

        //卸载所有包
        public void UnLoadAllAb()
        {
            AssetBundle.UnloadAllAssetBundles(false);
            abDic.Clear();
            mainAb = null;
            manifest = null;
        }
        #endregion
    }
}