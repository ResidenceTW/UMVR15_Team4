using System;
using UnityEngine;

public class MoveState : PlayerState
{
    public Action<bool> IsMoving;
    public Action<bool> IsRun;

    public MoveState(PlayerStateMachine stateMachine, PlayerController player) : base(stateMachine, player) { }

    public override void Enter()
    {
        IsMoving?.Invoke(true);
        IsRun?.Invoke(player.IsRun);
    }

    public override void Update()
    {
        if (player.IsHit || player.IsCriticalHit || player.IsRivive)
        {
            return;
        }
        // 如果玩家沒有輸入移動，則切換到 Idle 狀態
        if (player.GetMoveInput().sqrMagnitude < 0.01f)
        {
            StateMachine.ChangeState(player.idleState);
            IsMoving?.Invoke(false);
            IsRun?.Invoke(false);
            return;
        }
        if (player.IsAiming)
        {
            StateMachine.ChangeState(player.aimState);
            IsMoving?.Invoke(false);
            IsRun?.Invoke(false);
            return;
        }
        if (player.IsDie)
        {
            StateMachine.ChangeState(player.deadState);
            IsMoving?.Invoke(false);
            IsRun?.Invoke(false);
            return;
        }
        if (player.IsAttack)
        {
            StateMachine.ChangeState(player.fightState);
            IsMoving?.Invoke(false);
            IsRun?.Invoke(false);
            return;
        }
        if (player.IsDash)
        {
            StateMachine.ChangeState(player.dashState);
            IsMoving?.Invoke(false);
            IsRun?.Invoke(false);
            return;
        }
        if (player.IsHit)
        {
            IsMoving?.Invoke(false);
            IsRun?.Invoke(false);
            return;
        }
        if (player.IsCriticalHit)
        {
            IsMoving?.Invoke(false);
            IsRun?.Invoke(false);
            return;
        }

        Move();
    }

    public override void Move()
    {
        Vector3 moveDirection = player.GetMoveInput().normalized;

        // 計算角色相對於攝影機的移動方向
        Vector3 cameraForward = player.GetCurrentCameraForward();
        Vector3 cameraRight = player.GetCurrentCameraRight();

        // 根據相機方向調整角色的移動方向
        Vector3 targetDirection = cameraForward * moveDirection.z + cameraRight * moveDirection.x;

        bool isSprinting = player.IsRun;
        // 根據玩家是否正在跑步來調整速度
        float currentSpeed = player.IsRun ? player.playerData.MoveSpeed * player.playerData.SprintSpeedModifier : player.playerData.MoveSpeed;

        IsRun?.Invoke(isSprinting);
        IsMoving?.Invoke(!isSprinting);

        player.MoveCharacter(targetDirection, currentSpeed);
    }
}
