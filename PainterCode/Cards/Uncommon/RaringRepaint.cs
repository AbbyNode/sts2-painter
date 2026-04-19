using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Cards.Uncommon;

public class RaringRepaint() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(5, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);

        var discardPile = PileType.Discard.GetPile(Owner);
        var paintings = discardPile.Cards
            .Where(c => c.Keywords.Contains(PainterKeywords.Painting))
            .ToList();

        if (paintings.Count > 0)
        {
            var randomPainting = paintings[Random.Shared.Next(paintings.Count)];
            await CardPileCmd.Add(randomPainting, PileType.Hand, CardPilePosition.None, this);
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
