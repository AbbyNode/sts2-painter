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

                // Paint the colors from the exhausted Painting onto the Canvas
                var canvas = CanvasManager.Current;
                var canvasState = canvas;
                // TODO: Extract colors from the Painting card and paint them on the canvas

                if (IsUpgraded)
                    canvas.DarkenCanvas();
            }
        }
    }

    protected override void OnUpgrade()
    {
        // Upgrade effect handled in OnPlay (also Darkens the Canvas)
    }
}
