using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class Sign : MonoBehaviour
{
    public PlayerInputController playerInput;
    public Transform playerTrans;
    private Animator anim;
    public GameObject signSprite;
    private bool canPress;
    private IInteractable targetItem;

    private void Awake()
    {
        anim = signSprite.GetComponent<Animator>();

        playerInput = new PlayerInputController();
        playerInput.Enable();
    }

    private void OnEnable()
    {
        //�����¼�
        InputSystem.onActionChange += OnActionChange;
        playerInput.Gameplay.Confirm.started += OnConfirm;
    }
    private void OnDisable()
    {
        canPress = false;
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if (canPress)
        {
            //�������޸�״̬
            targetItem.TriggerAction();
            canPress = false;
        }
    }

    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        //���豸�л�ʱ��ͨ���豸���жϲ����Ŷ�Ӧ����ʾ����
        if (actionChange == InputActionChange.ActionStarted)
        {
            var d = ((InputAction)obj).activeControl.device;
            switch (d.device)
            {
                case Keyboard:
                    anim.Play("Keyboard");
                    break;
                case DualShockGamepad:
                    anim.Play("Playstation");
                    break;
            }
        }
    }

    private void Update()
    {
        //ֱ�ӿ������忪���ᵼ���޷�������������������ֱ�ӿ���SpriteRenderer
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        //���ڽ����ʾ�ᷭת������
        signSprite.transform.localScale = playerTrans.localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //ͨ��ƥ��Tag���ж�
        if (other.CompareTag("Interactable"))
        {
            canPress = true;
            //��ȡ�ӿ�
            targetItem = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�뿪�ɴ�������ʱ�ر���ʾ
        canPress = false;
    }
}
