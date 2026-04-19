using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Common;

public class ClearChroma() : PainterCard(0, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    private static readonly PaintColor[] NonSpecialColors =
        Enum.GetValues<PaintColor>().Where(c => !c.IsSpecial()).ToArray();

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var canvas = CanvasManager.Current;

        if (canvas.IsClear)
        {
            var color1 = NonSpecialColors[Random.Shared.Next(NonSpecialColors.Length)];
            var color2 = NonSpecialColors[Random.Shared.Next(NonSpecialColors.Length)];
            canvas.PaintColor(color1);
            canvas.PaintColor(color2);

            if (canvas.IsChromatic)
            {
                canvas.PaintColor(color1);
                canvas.PaintColor(color2);
            }
        }

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}
