using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Rare;

public class PollocksPotluck() : PainterCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    private static readonly PaintColor[] NonSpecialColors =
        Enum.GetValues<PaintColor>().Where(c => !c.IsSpecial()).ToArray();

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        foreach (var color in NonSpecialColors)
            CanvasManager.Current.PaintColor(color);

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}
