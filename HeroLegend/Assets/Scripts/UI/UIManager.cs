using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public PlayerStatus playerStatus;

    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO unloadedSceneEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO gameOverEvent;
    public VoidEventSO backToMenuEvent;

    [Header("组件")]
    public GameObject gameOverPanel;
    public GameObject restartBtn;

    private void OnEnable()
    {
        //添加事件订阅
        healthEvent.OnEventRaised += UpdateHealth;
        unloadedSceneEvent.LoadRequestEvent += OnUnloadedSceneEvent;
        loadDataEvent.onEventRaised += OnLoadDataEvent;
        gameOverEvent.onEventRaised += OnGameOverEvent;
        backToMenuEvent.onEventRaised += OnLoadDataEvent;
    }

    private void OnDisable()
    {
        //取消事件订阅
        healthEvent.OnEventRaised -= UpdateHealth;
        unloadedSceneEvent.LoadRequestEvent -= OnUnloadedSceneEvent;
        loadDataEvent.onEventRaised -= OnLoadDataEvent;
        gameOverEvent.onEventRaised -= OnGameOverEvent;
        backToMenuEvent.onEventRaised -= OnLoadDataEvent;
    }

    private void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(restartBtn);
    }

    private void OnLoadDataEvent()
    {
        gameOverPanel.SetActive(false);
    }


    private void OnUnloadedSceneEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
    {
        var isMenu = sceneToLoad.SceneType == SceneType.Menu;
        playerStatus.gameObject.SetActive(!isMenu);
    }


    private void UpdateHealth(Character character)
    {
        var percentage = character.CurrentHealth / character.MaxHealth;
        playerStatus.OnHealthChange(percentage);
    }
}
