using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Painter.PainterCode.Cards.Common;

public class CanvasCatapult() : PainterCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(9m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(ctx);

        if (IsUpgraded)
        {
            var selected = await CommonActions.SelectSingleCard(this, new("", "PAINTER-CANVASCATAPULT.select"), ctx, PileType.Hand);
            if (selected != null)
                await CardPileCmd.Add(selected, PileType.Exhaust, CardPilePosition.None, this);
        }
        else
        {
            var hand = PileType.Hand.GetPile(Owner);
            var candidates = hand.Cards.Where(c => c != this).ToList();
            if (candidates.Count > 0)
            {
                var randomCard = candidates[Random.Shared.Next(candidates.Count)];
                await CardPileCmd.Add(randomCard, PileType.Exhaust, CardPilePosition.None, this);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }
}
