using UnityEngine;

public class TeleportPoint : MonoBehaviour,IInteractable
{
    public SceneLoadEventSO SceneLoadEventSO;
    //Ҫ���͵��ĳ�����λ��
    public GameSceneSO SceneToGo;
    public Vector3 PositionToGo;

    public void TriggerAction()
    {
        Debug.Log("Teleporting...");
        //�����¼�
        SceneLoadEventSO.RaiseLoadRequestEvent(SceneToGo, PositionToGo, true);
    }
}
