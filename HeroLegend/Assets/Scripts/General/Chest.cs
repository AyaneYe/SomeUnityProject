using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? openSprite : closeSprite;
    }

    public void TriggerAction()
    {
        if (!isDone)
        {
            Debug.Log("Chest opened");
            OpenChest();
        }
    }

    void OpenChest()
    {
        
        spriteRenderer.sprite = openSprite;
        GetComponent<AudioDefination>()?.PlayAudioClip();
        //ͨ���޸�tag��canPress��״̬����ֹ�ظ�����
        isDone = true;
        this.gameObject.tag = "Untagged";
    }
}
