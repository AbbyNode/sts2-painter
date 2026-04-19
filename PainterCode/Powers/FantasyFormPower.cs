using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Powers;

public class FantasyFormPower : PainterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!cardPlay.Card.Keywords.Contains(PainterKeywords.Painting))
            return Task.CompletedTask;

        Flash();
        var canvas = CanvasManager.Current;
        var distinctColors = canvas.GetDistinctColors();
        foreach (var color in distinctColors)
        {
            canvas.PaintColor(color, Amount);
        }

        return Task.CompletedTask;
    }
}
