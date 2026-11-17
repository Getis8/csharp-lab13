namespace Lab_13;

// Command: знає, як виконатися і як скасуватися
public interface ICarCommand
{
    RemoteCar Target { get; }
    string Sender { get; }
    string Name { get; }
    void Execute();
    void Undo();
}

public abstract class CarCommand : ICarCommand
{
    public RemoteCar Target { get; }
    public string Sender { get; }
    public abstract string Name { get; }

    protected CarCommand(RemoteCar target, string sender)
    {
        Target = target ?? throw new ArgumentNullException(nameof(target));
        Sender = sender;
    }

    public abstract void Execute();
    public abstract void Undo();
}

public class OpenDoorsCommand : CarCommand
{
    public OpenDoorsCommand(RemoteCar target, string sender) : base(target, sender) { }
    public override string Name => "відкрити двері";
    public override void Execute() => Target.UnlockDoors();
    public override void Undo() => Target.LockDoors();
}

public class CloseDoorsCommand : CarCommand
{
    public CloseDoorsCommand(RemoteCar target, string sender) : base(target, sender) { }
    public override string Name => "закрити двері";
    public override void Execute() => Target.LockDoors();
    public override void Undo() => Target.UnlockDoors();
}

public class AlarmCommand : CarCommand
{
    public AlarmCommand(RemoteCar target, string sender) : base(target, sender) { }
    public override string Name => "увімкнути аварійну сигналізацію";
    public override void Execute() => Target.SetAlarm(true);
    public override void Undo() => Target.SetAlarm(false);
}

public class BlockEngineCommand : CarCommand
{
    public BlockEngineCommand(RemoteCar target, string sender) : base(target, sender) { }
    public override string Name => "заблокувати двигун";
    public override void Execute() => Target.BlockEngine();
    public override void Undo() => Target.UnblockEngine();
}