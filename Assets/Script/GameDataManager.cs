using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance{get; private set;}

    public GameData gameData;

    private string savePath;

    private void Awake()
    {
        Instance = this;

        savePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        LoadGame();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(gameData, false);
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        if(File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            gameData = new GameData();
        }
    }

    //---------

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // �� ESC �����Ҧ�
        {
            ToggleCursorMode();
        }
    }

    void ToggleCursorMode()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None; // �Ѱ���w
            Cursor.visible = true; // ��ܴ��
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // ��w�ƹ�
            Cursor.visible = false; // ���ô��
        }
    }
}
