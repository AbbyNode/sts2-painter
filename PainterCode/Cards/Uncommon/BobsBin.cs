using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;
using Painter.PainterCode.Keywords;

namespace Painter.PainterCode.Cards.Uncommon;

public class BobsBin() : PainterCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var hand = PileType.Hand.GetPile(Owner);
        var paintings = hand.Cards
            .Where(c => c.Keywords.Contains(PainterKeywords.Painting))
            .ToList();

        if (paintings.Count > 0)
        {
            var selected = await CommonActions.SelectSingleCard(this, new("", "PAINTER-BOBSBIN.select"), ctx, PileType.Hand);
            if (selected != null && selected.Keywords.Contains(PainterKeywords.Painting))
            {
                await CardPileCmd.Add(selected, PileType.Exhaust, CardPilePosition.None, this);

                // Re-paint a copy of the current canvas colors to amplify them
                var currentColors = CanvasManager.Current.Colors.ToList();
                foreach (var color in currentColors)
                    CanvasManager.Current.PaintColor(color);

                if (IsUpgraded)
                    CanvasManager.Current.DarkenCanvas();
            }
        }
    }

    protected override void OnUpgrade()
    {
        // Upgrade effect handled in OnPlay (also Darkens the Canvas)
    }
}
