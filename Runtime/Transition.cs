namespace StM
{
    public delegate bool Condition();

    public interface Transition
    {
        public Condition DoesApply { get; set;}
        public State Next { get; set; }
    }
}