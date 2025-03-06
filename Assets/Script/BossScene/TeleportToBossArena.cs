using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToBossArena : MonoBehaviour
{
    public Collider portal;
    public bool isUseable = false;
    public Transform targetPos;
    public Transform player;
    public CameraController mainCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isUseable == false)
        {
            isUseable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isUseable == true)
        {
            isUseable = false;
        }
    }

    private IEnumerator TeleportSequence()
    {
        Debug.Log("�}�l�ǰe�S�ġI");

        //1.=====�Ұʶǰe�S��=====
        /*portalEffect.Play();*/  // ���] portalEffect �O�A���ǰe���ɤl
        yield return new WaitForSeconds(1f);  // ���� 1 ��

        //2.=====�S�ĥ[�j + �«� (���ӷ|�[)=====

        //3.=====�ǰe=====
        Debug.Log("�i��ǰe...");
        if (player != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                player.transform.position = targetPos.transform.position;
                player.transform.rotation = Quaternion.Euler(0.8f, 48f, 0f);

                controller.enabled = true;
            }
        }
        if (mainCamera != null)
        {
            Vector3 playerEuler = player.transform.eulerAngles;
            mainCamera.SetCameraRotation(playerEuler.y, playerEuler.x);
        }
        yield return new WaitForSeconds(0.5f);  // �� 0.5 ������v���A��

        //4.=====�ǰe��t�X=====
        Debug.Log("�ǰe�����I");
    }

    void Update()
    {
        if (isUseable == true && Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(TeleportSequence());
        }
        //if (isUseable == true && Input.GetKeyDown(KeyCode.P)) 
        //{
        //    if (player != null)
        //    {
        //        CharacterController controller = player.GetComponent<CharacterController>();
        //        if (controller != null)
        //        {
        //            controller.enabled = false;
        //            player.transform.position = targetPos.transform.position;
        //            player.transform.rotation = Quaternion.Euler(0.8f, 48f,0f);
                    
        //            controller.enabled = true;
        //        }
        //    }
        //    if (mainCamera != null)
        //    {
        //        Vector3 playerEuler = player.transform.eulerAngles;
        //        mainCamera.SetCameraRotation(playerEuler.y, playerEuler.x);
        //    }
        //}
    }
}
