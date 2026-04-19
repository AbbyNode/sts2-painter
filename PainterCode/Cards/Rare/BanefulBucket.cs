using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Rare;

public class BanefulBucket() : PainterCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var canvas = CanvasManager.Current;

        if (canvas.IsChromatic)
        {
            var currentColors = canvas.Colors.ToList();
            var multiplier = IsUpgraded ? 2 : 1;
            for (var i = 0; i < multiplier; i++)
                foreach (var color in currentColors)
                    canvas.PaintColor(color);
        }

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        // Upgrade: Triple instead of Double (handled in OnPlay)
    }
}
