using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class EditorExtend
{
#if UNITY_EDITOR
    [MenuItem("Framework/ExportBasic")]
#endif
    private static void ExportBasic()
    {
        var assetPathName_1 = "Assets/Demigiant";
        var assetPathName_2 = "Assets/Resources/Ui";
        var assetPathName_3 = "Assets/Scripts/ShimmerFrameWork";

        var fileName = "ShimmerFrameWork(Basic)" + DateTime.Now.ToString("yyyyMMdd_hh")+ ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2 , assetPathName_3 }, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }

#if UNITY_EDITOR
    [MenuItem("Framework/ExportSqlite")]
#endif
    private static void ExportSqlite()
    {
        var assetPathName_1 = "Assets/Demigiant";
        var assetPathName_2 = "Assets/Resources/Ui";
        var assetPathName_3 = "Assets/Scripts/ShimmerFrameWork";
        var assetPathName_4 = "Assets/Scripts/ShimmerSqlite";
        var assetPathName_5 = "Assets/Plugins/Sqlite";

        var fileName = "ShimmerFrameWork(Sqlite)" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2, assetPathName_3,assetPathName_4,assetPathName_5 }, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }

#if UNITY_EDITOR
    [MenuItem("Framework/ExportHotUpdate")]
#endif
    private static void ExportHotUpdate()
    {
        var assetPathName_1 = "Assets/Resources/Ui";
        var assetPathName_2 = "Assets/Scripts/ShimmerFrameWork";
        var assetPathName_3 = "Assets/ToLua";
        var assetPathName_4 = "Assets/XLua";
        var assetPathName_5 = "Assets/Source";
        var assetPathName_6 = "Assets/Editor/Custom";
        var assetPathName_7 = "Assets/Plugins";
        var assetPathName_8 = "Assets/Lua";
        var assetPathName_9 = "StreamingAssets";

        var fileName = "ShimmerFrameWork(HotUpdate)" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2, assetPathName_3, assetPathName_4, 
            assetPathName_5, assetPathName_6, assetPathName_7, assetPathName_8, assetPathName_9 }, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }

#if UNITY_EDITOR
    [MenuItem("Framework/ExportAll")]
#endif
    private static void ExportAll()
    {
        var assetPathName = "Assets";
        var fileName = "ShimmerFrameWork(All)" + DateTime.Now.ToString("yyyyMMdd_hh") + ".unitypackage";
        AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }
}
