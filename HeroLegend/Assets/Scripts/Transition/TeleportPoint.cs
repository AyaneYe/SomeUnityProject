using UnityEngine;

public class TeleportPoint : MonoBehaviour,IInteractable
{
    public SceneLoadEventSO SceneLoadEventSO;
    //要传送到的场景和位置
    public GameSceneSO SceneToGo;
    public Vector3 PositionToGo;

    public void TriggerAction()
    {
        Debug.Log("Teleporting...");
        //呼叫事件
        SceneLoadEventSO.RaiseLoadRequestEvent(SceneToGo, PositionToGo, true);
    }
}
