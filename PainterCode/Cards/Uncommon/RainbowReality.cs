using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class RainbowReality() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("paint", 2m)];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var canvas = CanvasManager.Current;
        var wasClear = canvas.IsClear;
        var paintCount = DynamicVars["paint"].IntValue;

        canvas.PaintColor(PaintColor.Rainbow, paintCount);

        if (wasClear)
            canvas.PaintColor(PaintColor.Rainbow, 2);

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        DynamicVars["paint"].UpgradeValueBy(1m);
    }
}
