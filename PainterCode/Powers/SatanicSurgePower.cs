using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Painter.PainterCode.Powers;

public class SatanicSurgePower : PainterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    // This is a marker power. CursedPower checks for its presence
    // to skip the halving of Cursed stacks. CursedPower also removes
    // this power when it skips the reduction.
}
