using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Painter.PainterCode.Powers;

public class CursedPower : PainterPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    // Cursed damage is triggered from the canvas PaintColor method via a separate hook.
    // TODO: Hook into CanvasState.PaintColor to deal Amount damage to this power's Owner.

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side,
        CombatState combatState)
    {
        if (side != CombatSide.Player)
            return;

        if (Owner.HasPower<SatanicSurgePower>())
        {
            await PowerCmd.Remove<SatanicSurgePower>(Owner);
            return;
        }

        var newAmount = Amount / 2;
        if (newAmount <= 0)
        {
            await PowerCmd.Remove(this);
        }
        else
        {
            Flash();
            await PowerCmd.SetAmount<CursedPower>(Owner, newAmount, null, null);
        }
    }
}
