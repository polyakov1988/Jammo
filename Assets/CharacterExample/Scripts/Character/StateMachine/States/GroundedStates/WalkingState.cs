public class WalkingState : GroundedState
{
    private readonly RunningStateConfig _config;

    public WalkingState(IStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
        => _config = character.Config.RunningStateConfig;

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _config.Speed * _config.WalkRate;

        View.StartRunning();
    }

    public override void Exit()
    {
        base.Exit(); 

        View.StopRunning();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();
        
        if (IsWalking() == false)
            StateSwitcher.SwitchState<RunningState>();
    }
}
