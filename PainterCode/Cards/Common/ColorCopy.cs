using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Common;

public class ColorCopy() : PainterCard(0, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var canvas = CanvasManager.Current;
        var distinctColors = canvas.GetDistinctColors();

        if (distinctColors.Count > 0)
        {
            // Pick a random distinct color from the canvas (color choice UI pending Godot scene)
            var color = distinctColors[Random.Shared.Next(distinctColors.Count)];
            canvas.PaintColor(color);
        }

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
