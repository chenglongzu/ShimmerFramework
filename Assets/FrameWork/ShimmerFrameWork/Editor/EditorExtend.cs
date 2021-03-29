using UnityEngine;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class EditorExtend
{
    #region Print 打印数据
#if UNITY_EDITOR
    [MenuItem("Framework/Print/CodeNumber")]
    private static void PrintTotalLine()
    {
        string[] fileName = Directory.GetFiles("Assets/Scripts", "*.cs", SearchOption.AllDirectories);

        int totalLine = 0;
        foreach (var temp in fileName)
        {
            int nowLine = 0;
            StreamReader sr = new StreamReader(temp);
            while (sr.ReadLine() != null)
            {
                nowLine++;
            }

            //文件名 + 文件行数
            //Debug.Log(String.Format("{0}——{1}", temp, nowLine));

            totalLine += nowLine;
        }

        Debug.Log(String.Format("总代码行数：{0}", totalLine));
    }
#endif
#if UNITY_EDITOR
    [MenuItem("Framework/Print/PrintPersistentDataPath")]
    private static void PrintPersistentDataPath()
    {
        Debug.Log(String.Format("Application.persistentDataPath:{0}", Application.persistentDataPath));
    }
#endif
    #endregion

    #region Export打包
#if UNITY_EDITOR
    [MenuItem("Framework/Export/ExportShimmerFrameWork")]
#endif
    private static void ExportBasic()
    {
        var assetPathName_1 = "Assets/Plugins/Common";
        var assetPathName_2 = "Assets/ThirdParty/Demigiant";
        var assetPathName_3 = "Assets/FrameWork/ShimmerFrameWork";
        var assetPathName_4 = "Assets/Download";
        var assetPathName_5 = "Assets/Plugins/Sirenix";
        var assetPathName_6 = "Assets/Resources";

        var fileName = "ShimmerFrameWork(Basic)" + DateTime.Now.ToString("yyyyMMdd_hh")+ ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2 , assetPathName_3, assetPathName_4, assetPathName_5, assetPathName_6 }, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }

#if UNITY_EDITOR
    [MenuItem("Framework/Export/ExportSqlite")]
#endif
    private static void ExportSqlite()
    {
        var assetPathName_1 = "Assets/Plugins/Common";
        var assetPathName_2 = "Assets/Plugins/Sqlite";
        var assetPathName_3 = "Assets/ThirdParty/Demigiant";
        var assetPathName_4 = "Assets/FrameWork/ShimmerFrameWork";
        var assetPathName_5 = "Assets/FrameWork/ShimmerSqlite";
        var assetPathName_6 = "Assets/Download";
        var assetPathName_7 = "Assets/Plugins/Sirenix";
        var assetPathName_8 = "Assets/Resources";

        var fileName = "ShimmerFrameWork(Sqlite)" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2, assetPathName_3,assetPathName_4,assetPathName_5, assetPathName_6, assetPathName_7, assetPathName_8 }, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }

#if UNITY_EDITOR
    [MenuItem("Framework/Export/ExportXLua")]
#endif
    private static void ExportXLua()
    {
        var assetPathName_1 = "Assets/Plugins/Common";
        var assetPathName_2 = "Assets/Plugins/XLua";
        var assetPathName_3 = "Assets/ThirdParty/Demigiant";
        var assetPathName_4 = "Assets/FrameWork/ShimmerFrameWork";
        var assetPathName_5 = "Assets/FrameWork/ShimmerHotUpdate";
        var assetPathName_6 = "Assets/Download";
        var assetPathName_7 = "Assets/Plugins/Sirenix";
        var assetPathName_8 = "Assets/Resources";
        var assetPathName_9 = "Assets/XLua";

        var fileName = "ShimmerFrameWork(XLua)" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2, assetPathName_3, assetPathName_4, 
            assetPathName_5, assetPathName_6, assetPathName_7, assetPathName_8 ,assetPathName_9}, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }

#if UNITY_EDITOR
    [MenuItem("Framework/Export/ExportNetwork")]
#endif
    private static void ExportToLua()
    {
        var assetPathName_1 = "Assets/Plugins/Common";
        var assetPathName_2 = "Assets/Plugins/Network";
        var assetPathName_3 = "Assets/ThirdParty/Demigiant";
        var assetPathName_4 = "Assets/FrameWork/ShimmerFrameWork";
        var assetPathName_5 = "Assets/FrameWork/ShimmerNetwork";
        var assetPathName_6 = "Assets/Download";
        var assetPathName_7 = "Assets/Plugins/Sirenix";
        var assetPathName_8 = "Assets/Resources";

        var fileName = "ShimmerFrameWork(Network)" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2, assetPathName_3, assetPathName_4,
            assetPathName_5, assetPathName_6, assetPathName_7, assetPathName_8 }, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }


#if UNITY_EDITOR
    [MenuItem("Framework/Export/ExportAll")]
#endif
    private static void ExportAll()
    {
        var assetPathName = "Assets";
        var fileName = "ShimmerFrameWork(All)" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
        AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }
    #endregion
}
