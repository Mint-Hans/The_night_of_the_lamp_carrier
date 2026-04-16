using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("攻击")]
    public Vector2[] attackMovement;
    public bool isBusy { get; private set; }

    [Header("移动")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("冲刺")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public int energycost = 10;

    public PlayerDeadState deadState {  get; private set; }




    #region States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerPrimaryAttack primaryAttack { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, StateMachine, "Idle");
        moveState = new PlayerMoveState(this, StateMachine, "Move");
        jumpState = new PlayerJumpState(this, StateMachine, "Jump");
        airState = new PlayerAirState(this, StateMachine, "Jump");
        dashState = new PlayerDashState(this, StateMachine, "Dash");


        primaryAttack = new PlayerPrimaryAttack(this, StateMachine, "Attack");

        deadState = new PlayerDeadState(this, StateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();
        
        StateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        StateMachine.currentState.Update();

        CheckForDashInput();
        StartCoroutine("BusyFor", .1f);
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }
    public void AnimationTrigger() => StateMachine.currentState.AnimationFinishTrigger();
    private void CheckForDashInput()
    {
        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1) && dashUsageTimer < 0 && stats.currentEnergy >= energycost)
        {
            dashUsageTimer = dashCooldown;
            StateMachine.ChangeState(dashState);
            stats.TakeConsumption(energycost);
        }
    }
    
    public override void Die()
    {
        base.Die();

        StateMachine.ChangeState(deadState);
    }

}


