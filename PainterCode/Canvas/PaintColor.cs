namespace Painter.PainterCode.Canvas;

public enum PaintColor
{
    Red,
    Blue,
    Yellow,
    Green,
    Aqua,
    Magenta,
    Pink,
    Purple,
    Gray,
    Rainbow
}

public static class PaintColorExtensions
{
    public static string GetHexColor(this PaintColor color) => color switch
    {
        PaintColor.Red => "#b0120a",
        PaintColor.Blue => "#303f9f",
        PaintColor.Yellow => "#ffeb3b",
        PaintColor.Green => "#42bd41",
        PaintColor.Aqua => "#26a69a",
        PaintColor.Magenta => "#880e4f",
        PaintColor.Pink => "#f0749e",
        PaintColor.Purple => "#673ab7",
        PaintColor.Gray => "#b4b4b4",
        PaintColor.Rainbow => "#ffffff",
        _ => "#ffffff"
    };

    public static bool IsSpecial(this PaintColor color) =>
        color is PaintColor.Gray or PaintColor.Rainbow;
}
