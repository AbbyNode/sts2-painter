using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
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

        if (effects.Block > 0)
            await CreatureCmd.GainBlock(Owner.Creature, effects.Block, default, play);

        if (effects.CardDraw > 0)
            await CardPileCmd.Draw(ctx, effects.CardDraw, Owner);

        if (effects.Weak > 0 && enemies != null)
            foreach (var enemy in enemies)
                await PowerCmd.Apply<WeakPower>(enemy, effects.Weak, Owner.Creature, this);

        if (effects.Vulnerable > 0 && enemies != null)
            foreach (var enemy in enemies)
                await PowerCmd.Apply<VulnerablePower>(enemy, effects.Vulnerable, Owner.Creature, this);

        if (effects.TempHp > 0)
            // No TempHP API exists in STS2 reference assemblies; using Block as fallback
            await CreatureCmd.GainBlock(Owner.Creature, effects.TempHp, default, play);

        if (effects.Energy > 0)
            await PlayerCmd.GainEnergy(effects.Energy, Owner);

        if (effects.Cursed > 0 && enemies != null)
            foreach (var enemy in enemies)
                await PowerCmd.Apply<CursedPower>(enemy, effects.Cursed, Owner.Creature, this);

        if (effects.Wounds > 0)
            await CardPileCmd.AddToCombatAndPreview<Wound>(Owner.Creature, PileType.Discard, effects.Wounds, false);

        if (effects.AutoExhaust)
            await CardPileCmd.Add(this, PileType.Exhaust, CardPilePosition.None, this);
    }
}
