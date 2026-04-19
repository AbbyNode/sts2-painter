using System.Text;

namespace Painter.PainterCode.Canvas;

public record PaintingEffects
{
    public int Damage { get; init; }
    public int Block { get; init; }
    public int Energy { get; init; }
    public int CardDraw { get; init; }
    public int Weak { get; init; }
    public int Vulnerable { get; init; }
    public int TempHp { get; init; }
    public int Cursed { get; init; }
    public int Wounds { get; init; }
    public bool AutoExhaust { get; init; }
    public int EnergyCost { get; init; }
    public string Description { get; init; } = "";
}

public static class PaintingCardHelper
{
    private const int BaseDamagePerRed = 6;
    private const int BaseBlockPerBlue = 5;
    private const int BaseEnergyPerYellow = 1;
    private const int BaseDrawPerGreen = 1;
    private const int BaseWeakPerAqua = 1;
    private const int BaseVulnPerMagenta = 1;
    private const int BaseTempHpPerPink = 3;
    private const int BaseCursedPerPurple = 2;
    private const int BaseWoundsPerGray = 1;

    public static PaintingEffects ComputePaintingEffects(CanvasState canvas)
    {
        var darkenMultiplier = 1.0 + canvas.DarkenLevel * 0.5;

        var redCount = canvas.GetColorCount(PaintColor.Red);
        var blueCount = canvas.GetColorCount(PaintColor.Blue);
        var yellowCount = canvas.GetColorCount(PaintColor.Yellow);
        var greenCount = canvas.GetColorCount(PaintColor.Green);
        var aquaCount = canvas.GetColorCount(PaintColor.Aqua);
        var magentaCount = canvas.GetColorCount(PaintColor.Magenta);
        var pinkCount = canvas.GetColorCount(PaintColor.Pink);
        var purpleCount = canvas.GetColorCount(PaintColor.Purple);
        var grayCount = canvas.GetColorCount(PaintColor.Gray);

        var damage = ApplyDarken(BaseDamagePerRed * redCount, darkenMultiplier);
        var block = ApplyDarken(BaseBlockPerBlue * blueCount, darkenMultiplier);
        var energy = ApplyDarken(BaseEnergyPerYellow * yellowCount, darkenMultiplier);
        var cardDraw = ApplyDarken(BaseDrawPerGreen * greenCount, darkenMultiplier);
        var weak = ApplyDarken(BaseWeakPerAqua * aquaCount, darkenMultiplier);
        var vulnerable = ApplyDarken(BaseVulnPerMagenta * magentaCount, darkenMultiplier);
        var tempHp = ApplyDarken(BaseTempHpPerPink * pinkCount, darkenMultiplier);
        var cursed = ApplyDarken(BaseCursedPerPurple * purpleCount, darkenMultiplier);
        var wounds = ApplyDarken(BaseWoundsPerGray * grayCount, darkenMultiplier);

        var autoExhaust = yellowCount > 0;
        var energyCost = 1 + canvas.DarkenLevel;

        var description = BuildDescription(
            damage, block, energy, cardDraw,
            weak, vulnerable, tempHp, cursed, wounds,
            autoExhaust);

        return new PaintingEffects
        {
            Damage = damage,
            Block = block,
            Energy = energy,
            CardDraw = cardDraw,
            Weak = weak,
            Vulnerable = vulnerable,
            TempHp = tempHp,
            Cursed = cursed,
            Wounds = wounds,
            AutoExhaust = autoExhaust,
            EnergyCost = energyCost,
            Description = description
        };
    }

    private static int ApplyDarken(int baseValue, double multiplier) =>
        (int)(baseValue * multiplier);

    private static string BuildDescription(
        int damage, int block, int energy, int cardDraw,
        int weak, int vulnerable, int tempHp, int cursed, int wounds,
        bool autoExhaust)
    {
        var sb = new StringBuilder();

        if (damage > 0) sb.AppendLine($"Deal {damage} damage.");
        if (block > 0) sb.AppendLine($"Gain {block} Block.");
        if (energy > 0) sb.AppendLine($"Gain {energy} Energy.");
        if (cardDraw > 0) sb.AppendLine($"Draw {cardDraw} card(s).");
        if (weak > 0) sb.AppendLine($"Apply {weak} Weak.");
        if (vulnerable > 0) sb.AppendLine($"Apply {vulnerable} Vulnerable.");
        if (tempHp > 0) sb.AppendLine($"Gain {tempHp} Temp HP.");
        if (cursed > 0) sb.AppendLine($"Apply {cursed} Cursed.");
        if (wounds > 0) sb.AppendLine($"Add {wounds} Wound(s) to discard pile.");
        if (autoExhaust) sb.AppendLine("Exhaust.");

        return sb.ToString().TrimEnd();
    }
}
