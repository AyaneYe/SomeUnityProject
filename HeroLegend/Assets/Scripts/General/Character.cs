using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour, ISaveable
{
    [Header("事件监听")]
    public VoidEventSO newGameEvent;
    [Header("基础信息")]
    public float MaxHealth;
    public float CurrentHealth;
    [Header("受伤无敌")]
    public float invincibleTime;
    private float invincibleCounter;
    public bool invincible;

    public UnityEvent<Character> onHealthChange;
    public UnityEvent<Transform> onTakeDamage;
    public UnityEvent OnDie;

    private void NewGame()
    {
        CurrentHealth = MaxHealth;
        onHealthChange?.Invoke(this);
    }

    private void OnEnable()
    {
        newGameEvent.onEventRaised += NewGame;
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnDisable()
    {
        newGameEvent.onEventRaised -= NewGame;
        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (invincible)
        {
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter <= 0)
            {
                invincible = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth = 0;
                onHealthChange?.Invoke(this);
                OnDie?.Invoke();
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if (invincible)
        {
            return;
        }
        if (CurrentHealth - attacker.damage > 0)
        {
            CurrentHealth -= attacker.damage;
            triggerInvincible();
            // 触发受伤事件
            onTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            CurrentHealth = 0;
            OnDie?.Invoke();
        }

        onHealthChange?.Invoke(this);
    }

    //无敌状态
    public void triggerInvincible()
    {
        if (!invincible)
        {
            invincible = true;
            invincibleCounter = invincibleTime;
        }
    }

    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            data.characterPosDict[GetDataID().ID] = transform.position;
            data.floatSavedData[GetDataID().ID + "Health"] = this.CurrentHealth;
        }
        else
        {
            data.characterPosDict.Add(GetDataID().ID, transform.position);
            data.floatSavedData.Add(GetDataID().ID + "Health", this.CurrentHealth);
        }
    }

    public void LoadData(Data data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            transform.position = data.characterPosDict[GetDataID().ID];
            CurrentHealth = data.floatSavedData[GetDataID().ID + "Health"];

            //通知UI更新
            onHealthChange?.Invoke(this);
        }
    }
}
