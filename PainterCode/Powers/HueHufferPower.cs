using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Powers;

public class HueHufferPower : PainterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    private int _paintingsPlayedThisTurn;

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Keywords.Contains(PainterKeywords.Painting))
            _paintingsPlayedThisTurn++;

        return Task.CompletedTask;
    }

    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Player)
            return Task.CompletedTask;

        if (_paintingsPlayedThisTurn > 0)
        {
            Flash();
            var totalColors = Amount * _paintingsPlayedThisTurn;
            CanvasManager.Current.PaintColor(PaintColor.Rainbow, totalColors);
        }

        _paintingsPlayedThisTurn = 0;
        return Task.CompletedTask;
    }

    public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side,
        CombatState combatState)
    {
        if (side == CombatSide.Player)
            _paintingsPlayedThisTurn = 0;

        return Task.CompletedTask;
    }
}
