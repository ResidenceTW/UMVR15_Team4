using UnityEngine.SceneManagement;
using UnityEngine;

public class MySingleTon : MonoBehaviour
{
    private static MySingleTon instance;
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //�q�\��������ƥ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TitleScene")
        {
            Destroy(gameObject);
            OnDestroy();
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
