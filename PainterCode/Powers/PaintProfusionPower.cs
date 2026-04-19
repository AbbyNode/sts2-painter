using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Powers;

public class PaintProfusionPower : PainterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (!card.Keywords.Contains(PainterKeywords.Painting))
            return;

        if (Owner.Player == null)
            return;

        Flash();
        await CardPileCmd.Draw(choiceContext, Amount, Owner.Player);
    }
}
