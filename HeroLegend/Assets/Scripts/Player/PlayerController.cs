using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("�¼�����")]
    public SceneLoadEventSO sceneLoadEvent;
    public VoidEventSO afterSceneLoadEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO backToMenuEvent;

    private PlayerInputController inputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private CapsuleCollider2D coll;
    private PlayerAnimation playerAnimation;
    [Header("��������")]
    public Vector2 InputDirection;
    public float speed;
    public float jumpForce;
    public float hurtForce;

    //�¶ײ���(��δʵ��)
    private Vector2 originalOffset;
    private Vector2 originalSize;

    [Header("�������")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("״̬")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool isCrouch;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputController();
        physicsCheck = GetComponent<PhysicsCheck>();
        coll = GetComponent<CapsuleCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();

        originalOffset = coll.offset;
        originalSize = coll.size;

        inputControl.Gameplay.Jump.started += Jump;

        //����
        inputControl.Gameplay.Attack.started += PlayerAttack;
        inputControl.Enable();
    }


    private void OnEnable()
    {
        sceneLoadEvent.LoadRequestEvent += OnLoadEvent;
        afterSceneLoadEvent.onEventRaised += OnAfterSceneLoadEvent;
        loadDataEvent.onEventRaised += OnLoadDataEvent;
        backToMenuEvent.onEventRaised += OnLoadDataEvent;
    }


    private void OnDisable()
    {
        inputControl.Disable();
        sceneLoadEvent.LoadRequestEvent -= OnLoadEvent;
        afterSceneLoadEvent.onEventRaised -= OnAfterSceneLoadEvent;
        loadDataEvent.onEventRaised -= OnLoadDataEvent;
        backToMenuEvent.onEventRaised -= OnLoadDataEvent;
    }

    private void Update()
    {
        InputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
        if (inputControl.Gameplay.ChangeSpeed.triggered)
        {
            speed = speed > 400 ? 300 : 500;
        }
        StateCheck();
    }

    //��������ص�Ҫ�ŵ�FixedUpdate��
    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Move();
        }

    }

    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputControl.Gameplay.Disable();
    }

    private void OnLoadDataEvent()
    {
        isDead = false;
    }


    private void OnAfterSceneLoadEvent()
    {
        inputControl.Gameplay.Enable();
    }



    private void Move()
    {
        if (!isCrouch)
        {
            //���ĸ�����ƶ����Բ���deltaTime����TransForm����Ҫ
            rb.velocity = new Vector2(speed * Time.deltaTime * InputDirection.x, rb.velocity.y);
        }

        int faceDir = (int)transform.localScale.x;

        if (InputDirection.x > 0)
        {
            faceDir = 1;
        }
        else if (InputDirection.x < 0)
        {
            faceDir = -1;
        }
        //����else����ΪInputDirection.x����Ϊ0�����������泯ͬһ����

        transform.localScale = new Vector3(faceDir, 1, 1);

        //�¶�
        isCrouch = InputDirection.y < -0.5f && physicsCheck.isGrounded;
        if (isCrouch)
        {
            coll.offset = new Vector2(-0.05f, 0.85f);
            coll.size = new Vector2(0.7f, 1.7f);
        }
        else
        {
            coll.size = originalSize;
            coll.offset = originalOffset;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGrounded)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); //ʩ��һ������˲ʱ����
        GetComponent<AudioDefination>()?.PlayAudioClip();
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
    }


    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero; //������ͣ����
        //�������Ļ����õ�ǰ���������x����ȥ�����������x
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;  //��ֵ��һ������Ϊ�����Զ���ᵼ�����Ĵ�С��һ��

        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }

    public void StateCheck()
    {
        coll.sharedMaterial = physicsCheck.isGrounded ? normal : wall;
    }
}
