using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Cards.Uncommon;

public class WatercolorWave() : PainterCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(8m, ValueProp.Move),
        new IntVar("draw", 5m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(ctx);

        var handBefore = PileType.Hand.GetPile(Owner).Cards.ToHashSet();

        await CardPileCmd.Draw(ctx, DynamicVars["draw"].IntValue, Owner);

        var handAfter = PileType.Hand.GetPile(Owner).Cards;
        var drawnCards = handAfter.Where(c => !handBefore.Contains(c)).ToList();

        foreach (var card in drawnCards)
        {
            if (!card.Keywords.Contains(PainterKeywords.Painting))
                await CardPileCmd.Add(card, PileType.Discard, CardPilePosition.None, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
        DynamicVars["draw"].UpgradeValueBy(2m);
    }
}
