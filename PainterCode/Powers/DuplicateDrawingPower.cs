using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Powers;

public class DuplicateDrawingPower : PainterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    // When a Painting card is about to be played, set its replay count to double-play it.
    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (!cardPlay.Card.Keywords.Contains(PainterKeywords.Painting))
            return Task.CompletedTask;

        if (Amount <= 0)
            return Task.CompletedTask;

        Flash();
        cardPlay.Card.BaseReplayCount += 1;
        return Task.CompletedTask;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!cardPlay.Card.Keywords.Contains(PainterKeywords.Painting))
            return;

        await PowerCmd.Decrement(this);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Player)
            await PowerCmd.Remove(this);
    }
}
