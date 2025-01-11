using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;

    [Header("��������")]
    public bool manual;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("״̬")]
    public bool isGrounded;
    public bool touchLeftWall;
    public bool touchRightWall;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();

        if (!manual)
        {
            rightOffset = new Vector2(coll.offset.x + coll.bounds.size.x / 2, coll.offset.y);
            leftOffset = new Vector2(coll.offset.x - coll.bounds.size.x / 2, coll.offset.y);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    void Check()
    {
        //�жϵ���
        //����Ҫ��localScale����Ϊ�ڽ�ɫ��תʱ����ײ���λ��Ҳ�ᷢ���仯�������޷������жϵ���
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset * transform.localScale, checkRadius, groundLayer);

        //�ж�ǽ��
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }
    //���ƽŵ���ײ����
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
