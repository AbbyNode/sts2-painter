using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;

namespace Painter.PainterCode.Cards.Uncommon;

public class PalettePick() : PainterCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("paint", 2m)];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        // TODO: Present color choice UI to the player.
        // For now, paint a placeholder color (Red).
        var chosenColor = PaintColor.Red;

        CanvasManager.Current.PaintColor(chosenColor, DynamicVars["paint"].IntValue);

        // Exhaust this card if Yellow was chosen
        if (chosenColor == PaintColor.Yellow)
            await CardPileCmd.Add(this, PileType.Exhaust, CardPilePosition.None, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["paint"].UpgradeValueBy(1m);
    }
}
