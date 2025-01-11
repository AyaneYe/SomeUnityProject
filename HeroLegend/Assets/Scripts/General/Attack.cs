using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float attackRate;

    public void OnTriggerStay2D(Collider2D other)
    {
        //使用?.操作符，如果对面没有Character组件，则不会执行TakeDamage
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
