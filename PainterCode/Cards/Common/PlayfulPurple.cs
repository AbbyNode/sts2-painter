using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Canvas;
using Painter.PainterCode.Powers;

namespace Painter.PainterCode.Cards.Common;

public class PlayfulPurple() : PainterCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("paint", 1m)];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var basePaint = DynamicVars["paint"].IntValue;
        var canvas = CanvasManager.Current;

        canvas.PaintColor(PaintColor.Purple, basePaint);

        var enemies = CombatState.GetCreaturesOnSide(CombatSide.Enemy);
        foreach (var enemy in enemies)
        {
            if (enemy.HasPower<CursedPower>())
                canvas.PaintColor(PaintColor.Purple);
        }

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        DynamicVars["paint"].UpgradeValueBy(1m);
    }
}
