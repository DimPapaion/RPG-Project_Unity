namespace Scripts.Control
{
    public interface IRaycastable 
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerControl callingController);
    }
}
