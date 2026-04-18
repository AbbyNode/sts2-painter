using BaseLib.Abstracts;
using Painter.PainterCode.Extensions;
using Godot;

namespace Painter.PainterCode.Character;

public class PainterRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => Painter.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}
