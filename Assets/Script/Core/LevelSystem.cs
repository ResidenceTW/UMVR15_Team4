using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public PlayerData playerData;

    //�ʒm�S��Ǘ���߉ƛߏ����i�@�ʗL���v�I�b�j
    public event Action<bool> PlayerLevelup;

    private void Update()
    {
        LevelUp();
    }

    public void LevelUp()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            playerData.attackDamage += 5;
            playerData.MaxHealth += 10;
            Debug.Log("�߉ƛߏ���!");
        }
    }
}
