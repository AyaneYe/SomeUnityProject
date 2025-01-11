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
                myScript.RecreateGUID();    //�����ʱ���ò���������GUID
                EditorUtility.SetDirty(myScript);   //��Ƕ����ѱ��޸�
            }
            AssetDatabase.SaveAssets(); //����
        }
    }
}
