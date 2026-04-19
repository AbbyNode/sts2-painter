using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Cards.Common;

public class DoubleDraw() : PainterCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(4m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardAttack(this, play.Target).Execute(ctx);
        await CommonActions.CardAttack(this, play.Target).Execute(ctx);

        var player = Owner.Player;
        if (player != null)
        {
            var paintings = player.DiscardPile
                .Where(c => c.Keywords.Contains(PainterKeywords.Painting))
                .ToList();

            if (paintings.Count > 0)
            {
                var randomPainting = paintings[Random.Shared.Next(paintings.Count)];
                await CardPileCmd.MoveCard(ctx, randomPainting, player.Hand, player);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }
}
