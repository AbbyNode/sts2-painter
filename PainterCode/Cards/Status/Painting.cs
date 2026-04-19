using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Painter.PainterCode.Canvas;
using Painter.PainterCode.Keywords;
using Painter.PainterCode.Powers;

namespace Painter.PainterCode.Cards.Status;

// Painting is a dynamically generated card whose effects are determined by the
// canvas state at the time of creation. It is NOT collectible.
public class Painting() : PainterCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [PainterKeywords.Painting];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        var canvas = CanvasManager.Current;
        var effects = PaintingCardHelper.ComputePaintingEffects(canvas);

        var enemies = CombatState?.GetCreaturesOnSide(CombatSide.Enemy);

        if (effects.Damage > 0 && enemies != null)
            foreach (var enemy in enemies)
                await CreatureCmd.Damage(ctx, [enemy], effects.Damage, default, Owner.Creature);

        // TODO: Implement Block gain once the correct API is identified
        // if (effects.Block > 0) ...

        if (effects.CardDraw > 0)
            await CardPileCmd.Draw(ctx, effects.CardDraw, Owner);

        if (effects.Weak > 0 && enemies != null)
            foreach (var enemy in enemies)
                await PowerCmd.Apply<WeakPower>(enemy, effects.Weak, Owner.Creature, this);

        if (effects.Vulnerable > 0 && enemies != null)
            foreach (var enemy in enemies)
                await PowerCmd.Apply<VulnerablePower>(enemy, effects.Vulnerable, Owner.Creature, this);

        // TODO: Implement TempHp gain once the API is identified
        // if (effects.TempHp > 0) ...

        // TODO: Implement Energy gain once the API is identified
        // if (effects.Energy > 0) ...

        if (effects.Cursed > 0 && enemies != null)
            foreach (var enemy in enemies)
                await PowerCmd.Apply<CursedPower>(enemy, effects.Cursed, Owner.Creature, this);

        // TODO: Add Wounds to discard pile
        // if (effects.Wounds > 0) ...

        if (effects.AutoExhaust)
            await CardPileCmd.Add(this, PileType.Exhaust, CardPilePosition.None, this);
    }
}
