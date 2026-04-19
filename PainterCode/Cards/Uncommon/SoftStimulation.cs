using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class SoftStimulation() : PainterCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("paint", 4m)];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var canvas = CanvasManager.Current;
        var basePaint = DynamicVars["paint"].IntValue;

        if (IsUpgraded && canvas.IsChromatic)
        {
            // Chromatic: not reduced
            canvas.PaintColor(PaintColor.Green, basePaint);
        }
        else
        {
            var distinctColorCount = canvas.GetDistinctColors().Count;
            var paintCount = Math.Max(0, basePaint - distinctColorCount);
            canvas.PaintColor(PaintColor.Green, paintCount);
        }

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Chromatic: not reduced (handled in OnPlay)
    }
}
