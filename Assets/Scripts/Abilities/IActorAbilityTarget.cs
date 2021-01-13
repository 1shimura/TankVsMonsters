namespace Scripts
{
    public interface IActorAbilityTarget : IActorAbility
    {
        IActor TargetActor { get; set; }
    }
}