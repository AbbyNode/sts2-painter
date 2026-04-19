using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Painter.PainterCode.Cards.Basic;
using Painter.PainterCode.Extensions;
using Painter.PainterCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;

namespace Painter.PainterCode.Character;

public class Painter : PlaceholderCharacterModel
{
    public const string CharacterId = "Painter";
    
    public static readonly Color Color = new("ffffff");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 70;
    
    public override IEnumerable<CardModel> StartingDeck => [
        ModelDb.Card<StrikingStroke>(),
        ModelDb.Card<StrikingStroke>(),
        ModelDb.Card<StrikingStroke>(),
        ModelDb.Card<StrikingStroke>(),
        ModelDb.Card<PaletteParry>(),
        ModelDb.Card<PaletteParry>(),
        ModelDb.Card<PaletteParry>(),
        ModelDb.Card<PaletteParry>(),
        ModelDb.Card<BrushBash>(),
        ModelDb.Card<NeoNeutralize>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<BentBrush>()
    ];
    
    public override CardPoolModel CardPool => ModelDb.CardPool<PainterCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<PainterRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<PainterPotionPool>();
    
    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets. 
        These are just some of the simplest assets, given some placeholders to differentiate your character with. 
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }
    public override string CustomIconTexturePath => "character_icon_painter.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_painter.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_painter_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_painter.png".CharacterUiPath();
}
