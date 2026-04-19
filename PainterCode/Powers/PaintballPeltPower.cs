using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Combat;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Powers;

public class PaintballPeltPower : PainterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (!card.Keywords.Contains(PainterKeywords.Painting))
            return;

        Flash();
        var enemies = CombatState.GetCreaturesOnSide(CombatSide.Enemy);
        await CreatureCmd.Damage(choiceContext, enemies, Amount, default, Owner);
    }
}
