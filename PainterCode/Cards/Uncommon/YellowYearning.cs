using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class YellowYearning() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("paint", 1m)];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        // TODO: Gain 1 Energy (2 if upgraded) once energy command API is available
        CanvasManager.Current.PaintColor(PaintColor.Yellow, DynamicVars["paint"].IntValue);
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Gain 2 Energy instead of 1 (handled in OnPlay TODO)
    }
}
