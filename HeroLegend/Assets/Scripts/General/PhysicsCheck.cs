using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;

    [Header("基本参数")]
    public bool manual;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("状态")]
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
        //判断地面
        //这里要乘localScale是因为在角色翻转时，碰撞体的位置也会发生变化，导致无法正常判断地面
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset * transform.localScale, checkRadius, groundLayer);

        //判断墙体
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }
    //绘制脚底碰撞区域
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
