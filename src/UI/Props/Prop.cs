namespace PiBa.UI.Props
{
    public abstract class Prop<T> : IProp
    {
        public abstract T Value { get; set; }
    }
}