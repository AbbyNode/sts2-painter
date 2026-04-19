using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Powers;

namespace Painter.PainterCode.Cards.Uncommon;

public class GrowingGreens() : PainterCard(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await PowerCmd.Apply<GrowingGreensPower>(Owner.Creature, 1, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
