using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataDefination))]
[CanEditMultipleObjects]
public class DataDefinationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Recreate Guid"))
        {
            foreach (Object obj in targets)
            {
                DataDefination myScript = (DataDefination)obj;
                myScript.RecreateGUID();    //被点击时调用并重新生成GUID
                EditorUtility.SetDirty(myScript);   //标记对象已被修改
            }
            AssetDatabase.SaveAssets(); //保存
        }
    }
}
