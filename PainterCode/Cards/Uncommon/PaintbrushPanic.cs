using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class PaintbrushPanic() : PainterCard(-1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var x = play.Resources.EnergySpent;
        var paintCount = IsUpgraded ? x + 1 : x;

        // Paint X (or X+1) random Colors (Rainbow picks a random non-special color)
        CanvasManager.Current.PaintColor(PaintColor.Rainbow, paintCount);
        // Paint X (or X+1) more Rainbow Colors
        CanvasManager.Current.PaintColor(PaintColor.Rainbow, paintCount);

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Paint X+1 of each instead of X (handled in OnPlay)
    }
}
