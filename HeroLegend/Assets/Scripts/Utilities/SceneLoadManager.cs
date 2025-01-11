using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour, ISaveable
{
    public Transform playerTransform;
    [Header("场景")]
    public Vector3 firstPosition;
    public Vector3 menuPosition;
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuScene;
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public VoidEventSO newGameEvent;
    public VoidEventSO backToMenuEvent;
    [Header("广播")]
    public VoidEventSO afterSceneLoad;
    public FadeEventSO fadeEvent;
    public SceneLoadEventSO sceneUnloadedEvent;

    private GameSceneSO currentLoadScene;
    private GameSceneSO SceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;
    public float fadeDuration;
    private bool isLoading;

    private void Awake()
    {
        //现在加载的是哪一个场景
        //currentLoadScene = firstLoadScene;
        //异步叠加加载场景
        //currentLoadScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void Start()
    {
        //NewGame();
        loadEventSO.RaiseLoadRequestEvent(menuScene, menuPosition, true);
    }

    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += LoadScene;
        newGameEvent.onEventRaised += NewGame;
        backToMenuEvent.onEventRaised += OnBackToMenuEvent;

        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= LoadScene;
        newGameEvent.onEventRaised -= NewGame;
        backToMenuEvent.onEventRaised -= OnBackToMenuEvent;


        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
    }

    private void OnBackToMenuEvent()
    {
        SceneToLoad = menuScene;
        loadEventSO.RaiseLoadRequestEvent(SceneToLoad, menuPosition, true);
    }

    void NewGame()
    {
        SceneToLoad = firstLoadScene;
        //LoadScene(SceneToLoad, firstPosition, true);
        loadEventSO.RaiseLoadRequestEvent(SceneToLoad, firstPosition, true);
    }

    private void LoadScene(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        if (isLoading)
            return;

        isLoading = true;
        //临时存储变量
        SceneToLoad = locationToLoad;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;

        //不为空时卸载当前场景
        if (currentLoadScene != null)
        {
            StartCoroutine(UnloadPreviousScene());
        }
        else
        {
            LoadNewScene();
        }
    }

    private IEnumerator UnloadPreviousScene()
    {
        //播放过渡动画
        if (fadeScreen)
        {
            fadeEvent.FadeIn(fadeDuration);
        }

        //异步等待并加载
        yield return new WaitForSeconds(fadeDuration);

        //卸载场景，调整血条显示
        sceneUnloadedEvent.RaiseLoadRequestEvent(SceneToLoad, positionToGo, true);

        yield return currentLoadScene.sceneReference.UnLoadScene();

        //隐藏玩家
        playerTransform.gameObject.SetActive(false);

        LoadNewScene();
    }

    private void LoadNewScene()
    {
        var loadingOption = SceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        //执行加载完成后的事件
        loadingOption.Completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperationHandle<SceneInstance> handle)
    {
        //重置当前已加载场景的值，方便后续操作
        currentLoadScene = SceneToLoad;

        //将玩家传送到指定位置
        playerTransform.position = positionToGo;

        //显示玩家
        playerTransform.gameObject.SetActive(true);

        if (fadeScreen)
        {
            fadeEvent.FadeOut(fadeDuration);
        }

        isLoading = false;
        if (currentLoadScene.SceneType == SceneType.Location)
        {
            //执行获取相机边界的事件
            afterSceneLoad.RaiseEvent();
        }
    }

    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        data.SaveGameScene(currentLoadScene);
    }

    public void LoadData(Data data)
    {
        var PlayerID = playerTransform.GetComponent<DataDefination>().ID;
        if (data.characterPosDict.ContainsKey(PlayerID))
        {
            positionToGo = data.characterPosDict[PlayerID];
            SceneToLoad = data.GetSavedScene();

            LoadScene(SceneToLoad, positionToGo, true);
        }
    }
}
