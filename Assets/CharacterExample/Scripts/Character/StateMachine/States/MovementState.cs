using UnityEngine;

public abstract class MovementState : IState
{
    protected readonly IStateSwitcher StateSwitcher;
    protected readonly StateMachineData Data;
    private readonly Character _character;

    public MovementState(IStateSwitcher stateSwitcher, StateMachineData data, Character character)
    {
        StateSwitcher = stateSwitcher;
        _character = character;
        Data = data;
    }

    protected PlayerInput Input => _character.Input;
    protected CharacterController CharacterController => _character.CharacterController;
    protected CharacterView View => _character.View;

    private Quaternion TurnRight => new Quaternion(0, 0, 0, 0);
    private Quaternion TurnLeft => Quaternion.Euler(0, 180, 0);

    public virtual void Enter()
    {
        Debug.Log(GetType());
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public void HandleInput()
    {
        Data.XInput = ReadHorizontalInput();
        Data.XVelocity = Data.XInput * Data.Speed;
        Data.IsFastMoving = ReadFastMoveInput() > 0;
        Data.IsWalking = ReadWalkingInput() > 0;
    }

    public virtual void Update()
    {
        Vector3 velocity = GetConvertedVelocity();

        CharacterController.Move(velocity * Time.deltaTime);
        _character.transform.rotation = GetRotationFrom(velocity);
    }

    protected virtual void AddInputActionsCallbacks() { }
    protected virtual void RemoveInputActionsCallbacks() { }

    protected bool IsHorizontalInputZero() => Data.XInput == 0;
    
    protected bool IsFastMoving() => Data.IsFastMoving;
    
    protected bool IsWalking() => Data.IsWalking;

    private Quaternion GetRotationFrom(Vector3 velocity)
    {
        if (velocity.x > 0)
            return TurnRight;

        if (velocity.x < 0)
            return TurnLeft;

        return _character.transform.rotation;
    }

    private Vector3 GetConvertedVelocity() => new Vector3(Data.XVelocity, Data.YVelocity, 0);

    private float ReadHorizontalInput() => Input.Movement.Move.ReadValue<float>();
    
    private float ReadFastMoveInput() => Input.Movement.FastMove.ReadValue<float>();
    
    private float ReadWalkingInput() => Input.Movement.Walk.ReadValue<float>();
}
