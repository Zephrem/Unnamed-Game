using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region __SINGLETON__

    public static SceneLoader Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public enum Scene
    {
        BattleScene,
        HubScene
    }

    public void Start()
    {
        SceneManager.LoadScene(Scene.HubScene.ToString(), LoadSceneMode.Additive);

        StartCoroutine(WaitForLoadCo(Scene.HubScene));
    }

    public void Load(Scene scene)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);

        StartCoroutine(WaitForLoadCo(scene));
    }

    private IEnumerator WaitForLoadCo(Scene scene)
    {
        var newScene = SceneManager.GetSceneByName(scene.ToString());

        yield return new WaitUntil(() => newScene.isLoaded);

        SceneManager.SetActiveScene(newScene);
    }

}
