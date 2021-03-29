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
    [MenuItem("Framework/Export/ExportBasic")]
#endif
    private static void ExportBasic()
    {
        var assetPathName_1 = "Assets/Plugins/Demigiant";
        var assetPathName_2 = "Assets/Resources/Ui";
        var assetPathName_3 = "Assets/Scripts/ShimmerFrameWork";

        var fileName = "ShimmerFrameWork(Basic)" + DateTime.Now.ToString("yyyyMMdd_hh")+ ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2 , assetPathName_3 }, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }

#if UNITY_EDITOR
    [MenuItem("Framework/Export/ExportSqlite")]
#endif
    private static void ExportSqlite()
    {
        var assetPathName_1 = "Assets/Plugins/Demigiant";
        var assetPathName_2 = "Assets/Resources/Ui";
        var assetPathName_3 = "Assets/Scripts/ShimmerFrameWork";
        var assetPathName_4 = "Assets/Scripts/ShimmerSqlite";
        var assetPathName_5 = "Assets/Plugins/Sqlite";

        var fileName = "ShimmerFrameWork(Sqlite)" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2, assetPathName_3,assetPathName_4,assetPathName_5 }, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }

#if UNITY_EDITOR
    [MenuItem("Framework/Export/ExportXLua")]
#endif
    private static void ExportXLua()
    {
        var assetPathName_1 = "Assets/Resources/Ui";
        var assetPathName_2 = "Assets/Scripts/ShimmerFrameWork";
        var assetPathName_3 = "Assets/XLua";
        var assetPathName_4 = "Assets/Source";
        var assetPathName_5 = "Assets/Editor/Custom";
        var assetPathName_6 = "Assets/Plugins";
        var assetPathName_7 = "Assets/Lua";
        var assetPathName_8 = "StreamingAssets";
        var assetPathName_9 = "Assets/Scripts/ShimmerHotUpdate";

        var fileName = "ShimmerFrameWork(XLua)" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2, assetPathName_3, assetPathName_4, 
            assetPathName_5, assetPathName_6, assetPathName_7, assetPathName_8 ,assetPathName_9}, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }

#if UNITY_EDITOR
    [MenuItem("Framework/Export/ExportToLua")]
#endif
    private static void ExportToLua()
    {
        var assetPathName_1 = "Assets/Resources/Ui";
        var assetPathName_2 = "Assets/Scripts/ShimmerFrameWork";
        var assetPathName_3 = "Assets/ToLua";
        var assetPathName_4 = "Assets/Source";
        var assetPathName_5 = "Assets/Editor/Custom";
        var assetPathName_6 = "Assets/Plugins";
        var assetPathName_7 = "Assets/Lua";
        var assetPathName_8 = "StreamingAssets";
        var assetPathName_9 = "Assets/Scripts/ShimmerHotUpdate";

        var fileName = "ShimmerFrameWork(XLua)" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
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
