using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Powers;

public class BloodBrushPower : PainterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result,
        ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target != Owner)
            return Task.CompletedTask;

        if (result.UnblockedDamage <= 0)
            return Task.CompletedTask;

        Flash();
        CanvasManager.Current.PaintColor(PaintColor.Rainbow, Amount);
        return Task.CompletedTask;
    }
}
