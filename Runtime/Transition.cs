namespace UnityStateMachine
{
    public delegate bool Condition();

    public interface Transition
    {
        public Condition DoesApply { get;}
        public State Next { get; }
    }
}