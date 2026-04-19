using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Rare;

public class IndiaInk() : PainterCard(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        // TODO: Gain 1 Energy (2 if upgraded) once energy command API is available
        CanvasManager.Current.DarkenCanvas();
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Gain 2 Energy instead of 1 (handled in OnPlay TODO)
    }
}
