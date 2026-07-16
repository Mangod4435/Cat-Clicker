public static class UIState
{
    public enum OpenedInterface
    {
        None,
        Setting,
        Upgrade,
    }

    public static OpenedInterface state = OpenedInterface.Upgrade;
}
