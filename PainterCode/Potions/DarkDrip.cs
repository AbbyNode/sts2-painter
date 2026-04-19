using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Potions;

/// <summary>
/// Common potion. Darken the Canvas.
/// </summary>
public class DarkDrip : PainterPotion
{
    public override PotionRarity Rarity => PotionRarity.Common;

    public override Task OnUse(PlayerChoiceContext ctx)
    {
        CanvasManager.Current.DarkenCanvas();
        return Task.CompletedTask;
    }
}
