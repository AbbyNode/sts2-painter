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

    /// <summary>
    /// If true, the next PaintColor call will paint one additional color (used by Bent Brush).
    /// </summary>
    public bool BonusPaintEnabled { get; set; }

    /// <summary>
    /// If true, Rainbow resolution paints an extra random color (used by Brilliant Brush).
    /// </summary>
    public bool ExtraRainbowEnabled { get; set; }

    /// <summary>
    /// Fired after each individual color is resolved and added to the canvas.
    /// Subscribers receive the resolved color.
    /// </summary>
    public event Action<PaintColor>? OnColorPainted;

    public int TotalColors => Colors.Count;

    public bool IsChromatic => GetDistinctColors().Count == 2;

    public bool IsClear => Colors.Count == 0;

    public void PaintColor(PaintColor color, int count = 1)
    {
        var isFirstPaint = FirstPaintSinceReshuffle;

        for (var i = 0; i < count; i++)
        {
            var resolved = ResolveAndAdd(color);
            OnColorPainted?.Invoke(resolved);
        }

        // Bent Brush: paint one bonus of the same color on the first paint since reshuffle
        if (isFirstPaint && BonusPaintEnabled)
        {
            var resolved = ResolveAndAdd(color);
            OnColorPainted?.Invoke(resolved);
        }

        FirstPaintThisCombat = false;
        FirstPaintSinceReshuffle = false;
    }

    private PaintColor ResolveAndAdd(PaintColor color)
    {
        if (color == Canvas.PaintColor.Rainbow)
        {
            var randomColor = NonSpecialColors[Random.Shared.Next(NonSpecialColors.Length)];
            Colors.Add(randomColor);

            // Brilliant Brush: paint an extra random color on Rainbow resolution
            if (ExtraRainbowEnabled)
            {
                var extraColor = NonSpecialColors[Random.Shared.Next(NonSpecialColors.Length)];
                Colors.Add(extraColor);
                OnColorPainted?.Invoke(extraColor);
            }

            return randomColor;
        }

        Colors.Add(color);
        return color;
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
