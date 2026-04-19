using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Powers;

public class CursedPower : PainterPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    // Track paints to deal accumulated damage at end of turn.
    private int _paintsSinceLastCheck;
    private Action<PaintColor>? _paintHandler;

    public override Task BeforeCombatStart()
    {
        _paintsSinceLastCheck = 0;
        _paintHandler = _ => _paintsSinceLastCheck++;
        CanvasManager.Current.OnColorPainted += _paintHandler;
        return Task.CompletedTask;
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Player || _paintsSinceLastCheck <= 0 || Amount <= 0)
            return;

        Flash();
        var totalDamage = Amount * _paintsSinceLastCheck;
        _paintsSinceLastCheck = 0;
        await CreatureCmd.Damage(choiceContext, [Owner], totalDamage, default, Owner);
    }

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
            if (_paintHandler != null)
                CanvasManager.Current.OnColorPainted -= _paintHandler;
            await PowerCmd.Remove(this);
        }
        else
        {
            Flash();
            await PowerCmd.SetAmount<CursedPower>(Owner, newAmount, null, null);
        }
    }
}
