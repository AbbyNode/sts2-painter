using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Cards.Common;

public class CrushingCanvas() : PainterCard(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(3m, ValueProp.Move),
        new IntVar("bonus", 3m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        // Deal damage once per Painting card across all piles
        var paintingCount = CountPaintingCards();
        for (var i = 0; i < Math.Max(1, paintingCount); i++)
            await CommonActions.CardAttack(this, play.Target).Execute(ctx);
    }

    private int CountPaintingCards()
    {
        var count = 0;
        if (Owner.Creature.CombatState is { } combatState)
        {
            foreach (var card in combatState.IterateHookListeners().OfType<CardModel>())
            {
                if (card.Keywords.Contains(PainterKeywords.Painting))
                    count++;
            }
        }
        return count;
    }

    protected override void OnUpgrade()
    {
        DynamicVars["bonus"].UpgradeValueBy(2m);
    }
}
