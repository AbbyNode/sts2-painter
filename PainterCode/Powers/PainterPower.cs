using BaseLib.Abstracts;
using BaseLib.Extensions;
using Painter.PainterCode.Extensions;
using Godot;

namespace Painter.PainterCode.Powers;

public abstract class PainterPower : CustomPowerModel
{
    //Loads from Painter/images/powers/your_power.png
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}
