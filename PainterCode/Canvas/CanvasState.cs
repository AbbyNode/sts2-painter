namespace Painter.PainterCode.Canvas;

public class CanvasState
{
    private static readonly PaintColor[] NonSpecialColors =
        Enum.GetValues<PaintColor>().Where(c => !c.IsSpecial()).ToArray();

    public List<PaintColor> Colors { get; private set; } = [];
    public int DarkenLevel { get; private set; }
    public bool PaintingCreatedThisTurn { get; set; }
    public bool FirstPaintThisCombat { get; set; } = true;
    public bool FirstPaintSinceReshuffle { get; set; } = true;

    public int TotalColors => Colors.Count;

    public bool IsChromatic => GetDistinctColors().Count == 2;

    public bool IsClear => Colors.Count == 0;

    public void PaintColor(PaintColor color, int count = 1)
    {
        for (var i = 0; i < count; i++)
        {
            if (color == Canvas.PaintColor.Rainbow)
            {
                var randomColor = NonSpecialColors[Random.Shared.Next(NonSpecialColors.Length)];
                Colors.Add(randomColor);
            }
            else
            {
                Colors.Add(color);
            }
        }

        FirstPaintThisCombat = false;
        FirstPaintSinceReshuffle = false;
    }

    public void DarkenCanvas()
    {
        DarkenLevel++;
    }

    public void Clear()
    {
        Colors.Clear();
        DarkenLevel = 0;
    }

    public void ClearColors()
    {
        Colors.Clear();
    }

    public void ResetForNewTurn()
    {
        PaintingCreatedThisTurn = false;
    }

    public void ResetForNewCombat()
    {
        Colors.Clear();
        DarkenLevel = 0;
        PaintingCreatedThisTurn = false;
        FirstPaintThisCombat = true;
        FirstPaintSinceReshuffle = true;
    }

    public int GetColorCount(PaintColor color) =>
        Colors.Count(c => c == color);

    public List<PaintColor> GetDistinctColors() =>
        Colors.Distinct().ToList();
}
