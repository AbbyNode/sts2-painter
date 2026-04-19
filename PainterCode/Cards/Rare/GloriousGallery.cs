using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Cards.Rare;

public class GloriousGallery() : PainterCard(3, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var paintings = new List<CardModel>();

        foreach (var pileType in new[] { PileType.Draw, PileType.Discard })
        {
            var pile = pileType.GetPile(Owner);
            paintings.AddRange(pile.Cards.Where(c => c.Keywords.Contains(PainterKeywords.Painting)).ToList());
        }

        foreach (var painting in paintings)
            await CardPileCmd.Add(painting, PileType.Hand, CardPilePosition.None, this);
    }

    protected override void OnUpgrade()
    {
        // TODO: Reduce cost to 2 once cost upgrade API is available
    }
}
