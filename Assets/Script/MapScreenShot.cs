using System.IO;
using UnityEngine;

public class MapScreenshot : MonoBehaviour
{
    public Camera mapCamera; // ���w�A������v��v��
    public int imageWidth = 2048; // �]�w�Ϥ��e��
    public int imageHeight = 2048; // �]�w�Ϥ�����
    public string fileName = "MapScreenshot.png"; // ��X���Ϥ��W��

    public void CaptureMap()
    {
        if (mapCamera == null)
        {
            Debug.LogError("�Ы��w�@����v���I");
            return;
        }

        // **�إ� RenderTexture**�A����v����V��o�ӶK�ϤW
        RenderTexture rt = new RenderTexture(imageWidth, imageHeight, 24);
        mapCamera.targetTexture = rt;
        mapCamera.Render();

        // **�N RenderTexture �ഫ�� Texture2D**
        Texture2D screenShot = new Texture2D(imageWidth, imageHeight, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        screenShot.Apply();

        // **�x�s�� PNG**
        byte[] bytes = screenShot.EncodeToPNG();
        string filePath = Path.Combine(Application.dataPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        // **�M�� RenderTexture**
        mapCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        Debug.Log($"�a�ϺI�Ϥw�x�s�G{filePath}");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CaptureMap(); // ���U P ��ɺI��
        }
    }
}
