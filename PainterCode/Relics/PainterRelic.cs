using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Painter.PainterCode.Character;
using Painter.PainterCode.Extensions;
using Godot;

namespace Painter.PainterCode.Relics;

[Pool(typeof(PainterRelicPool))]
public abstract class PainterRelic : CustomRelicModel
{
    public override string PackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImagePath();
    protected override string PackedIconOutlinePath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_outline.png".RelicImagePath();
    protected override string BigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigRelicImagePath();
}
