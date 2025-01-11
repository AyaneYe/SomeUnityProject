using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("事件监听")]
    public VoidEventSO saveDataEvent;
    public VoidEventSO loadDataEvent;

    private List<ISaveable> saveableList = new List<ISaveable>();

    private Data saveData;

    //通用写法，只能有一个单例模式
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        saveData = new Data();
    }

    private void OnEnable()
    {
        saveDataEvent.onEventRaised += Save;
        loadDataEvent.onEventRaised += Load;
    }

    private void OnDisable()
    {
        saveDataEvent.onEventRaised -= Save;
        loadDataEvent.onEventRaised -= Load;
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Load();
        }
    }

    public void RegisterSaveData(ISaveable saveable)
    {
        if (!saveableList.Contains(saveable))
        {
            saveableList.Add(saveable);
        }
    }

    public void UnRegisterSaveData(ISaveable saveable)
    {
        saveableList.Remove(saveable);
    }

    public void Save()
    {
        foreach (var saveable in saveableList)
        {
            saveable.GetSaveData(saveData);
        }
        foreach (var item in saveData.characterPosDict)
        {
            Debug.Log(item.Key + " " + item.Value);
        }
    }

    public void Load()
    {
        foreach(var saveable in saveableList)
        {
            saveable.LoadData(saveData);
        }
    }
}
