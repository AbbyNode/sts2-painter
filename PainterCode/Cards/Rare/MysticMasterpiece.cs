using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Cards.Status;

namespace Painter.PainterCode.Cards.Rare;

public class MysticMasterpiece() : PainterCard(3, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var painting = CombatState!.CreateCard<Painting>(Owner);
        await CardPileCmd.AddGeneratedCardToCombat(painting, PileType.Hand, true);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-2);
    }
}
