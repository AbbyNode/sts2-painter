using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class DeepDesperation() : PainterCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("paint", 1m)];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var basePaint = DynamicVars["paint"].IntValue;
        var canvas = CanvasManager.Current;

        canvas.PaintColor(PaintColor.Purple, basePaint);

        // Paint 1 additional Purple for each Status and Curse card in all piles
        var statusCurseCount = 0;
        if (Owner.Creature.CombatState is { } combatState)
        {
            foreach (var card in combatState.IterateHookListeners().OfType<CardModel>())
            {
                if (card.Type is CardType.Status or CardType.Curse)
                    statusCurseCount++;
            }
        }

        if (statusCurseCount > 0)
            canvas.PaintColor(PaintColor.Purple, statusCurseCount);

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        DynamicVars["paint"].UpgradeValueBy(1m);
    }
}
