namespace Painter.PainterCode.Canvas;

public static class CanvasManager
{
    private static CanvasState? _current;

    public static CanvasState Current => _current ??= new CanvasState();

    public static void Reset()
    {
        _current = new CanvasState();
    }
}
