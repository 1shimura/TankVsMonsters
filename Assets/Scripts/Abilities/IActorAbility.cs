namespace Scripts
{
    public interface IActorAbility
    {
        IActor Actor { get; set; }
        void Initialize(IActor actor);
        void Execute();
    }
}