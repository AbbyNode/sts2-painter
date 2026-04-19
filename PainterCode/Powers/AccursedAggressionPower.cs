using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Painter.PainterCode.Powers;

public class AccursedAggressionPower : PainterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterAttack(AttackCommand command)
    {
        var attacker = command.Attacker;
        if (attacker == null || attacker == Owner)
            return;

        var hitOwner = command.Results.Any(r => r.Receiver == Owner);
        if (!hitOwner)
            return;

        Flash();
        await PowerCmd.Apply<CursedPower>(attacker, Amount, Owner, null);
    }
}
