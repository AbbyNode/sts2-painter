using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Potions;

/// <summary>
/// Uncommon potion. Choose and Paint 3 Colors.
/// </summary>
public class PaintPotion : PainterPotion
{
    public override PotionRarity Rarity => PotionRarity.Uncommon;

    private const int ColorsToPaint = 3;

    public override Task OnUse(PlayerChoiceContext ctx)
    {
        // TODO: Present a color-selection UI and let the player choose 3 colors to paint.
        // For now, paint 3 random (Rainbow) colors as a placeholder.
        CanvasManager.Current.PaintColor(PaintColor.Rainbow, ColorsToPaint);
        return Task.CompletedTask;
    }
}
