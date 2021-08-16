namespace Game.UI
{
    public interface IUIElement : IDrawable
    {
        Theme Theme { get; }
    }
}