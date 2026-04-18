using BaseLib.Abstracts;
using BaseLib.Utils;
using Painter.PainterCode.Character;

namespace Painter.PainterCode.Potions;

[Pool(typeof(PainterPotionPool))]
public abstract class PainterPotion : CustomPotionModel;
