using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Painter.PainterCode.Cards.Rare;

public class MysticMasterpiece() : PainterCard(3, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        // TODO: Permanently add a Painting based on the Canvas to your deck.
        // Fleeting: the card is removed at end of combat.
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // TODO: Reduce cost to 1 once cost upgrade API is available
        // Upgrade also adds Grave keyword (Ethereal + Grave)
    }
}
