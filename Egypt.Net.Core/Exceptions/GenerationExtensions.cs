using System.ComponentModel;
using System.Reflection;
using Egypt.Net.Core.Enums;

namespace Egypt.Net.Core;

/// <summary>
/// Extension methods for the Generation enum.
/// Provides helper methods for working with generational cohorts.
/// </summary>
public static class GenerationExtensions
{
    /// <summary>
    /// Gets the Arabic name of the generation.
    /// </summary>
    /// <param name="generation">The generation enum value.</param>
    /// <returns>The Arabic name of the generation.</returns>
    public static string GetArabicName(this Generation generation)
    {
        var field = generation.GetType().GetField(generation.ToString());
        if (field == null)
            return generation.ToString();

        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? generation.ToString();
    }

    /// <summary>
    /// Gets the English name of the generation.
    /// </summary>
    /// <param name="generation">The generation enum value.</param>
    /// <returns>The English name of the generation.</returns>
    public static string GetEnglishName(this Generation generation)
    {
        return generation.ToString();
    }

    /// <summary>
    /// Gets both Arabic and English names of the generation.
    /// </summary>
    /// <param name="generation">The generation enum value.</param>
    /// <returns>A tuple containing (ArabicName, EnglishName).</returns>
    public static (string Arabic, string English) GetBothNames(this Generation generation)
    {
        return (generation.GetArabicName(), generation.GetEnglishName());
    }

    /// <summary>
    /// Gets the birth year range for a generation.
    /// </summary>
    /// <param name="generation">The generation enum value.</param>
    /// <returns>A tuple containing (StartYear, EndYear).</returns>
    public static (int StartYear, int EndYear) GetYearRange(this Generation generation)
    {
        return generation switch
        {
            Generation.SilentGeneration => (1928, 1945),
            Generation.BabyBoomers => (1946, 1964),
            Generation.GenerationX => (1965, 1980),
            Generation.Millennials => (1981, 1996),
            Generation.GenerationZ => (1997, 2012),
            Generation.GenerationAlpha => (2013, DateTime.Today.Year),
            _ => throw new ArgumentException($"Unknown generation: {generation}")
        };
    }

    /// <summary>
    /// Determines the generation based on birth year.
    /// </summary>
    /// <param name="birthYear">The birth year.</param>
    /// <returns>The generation for the given birth year.</returns>
    public static Generation GetGenerationFromYear(int birthYear)
    {
        // Note: For years before 1928, we return SilentGeneration
        // as it's the earliest defined generation
        if (birthYear < 1928)
            return Generation.SilentGeneration;

        if (birthYear <= 1945)
            return Generation.SilentGeneration;

        if (birthYear <= 1964)
            return Generation.BabyBoomers;

        if (birthYear <= 1980)
            return Generation.GenerationX;

        if (birthYear <= 1996)
            return Generation.Millennials;

        if (birthYear <= 2012)
            return Generation.GenerationZ;

        // 2013 and later
        return Generation.GenerationAlpha;
    }

    /// <summary>
    /// Checks if the generation is a digital native generation.
    /// Digital natives are Millennials, Gen Z, and Gen Alpha.
    /// </summary>
    /// <param name="generation">The generation to check.</param>
    /// <returns>True if the generation is digital native; otherwise, false.</returns>
    public static bool IsDigitalNative(this Generation generation)
    {
        return generation == Generation.Millennials ||
               generation == Generation.GenerationZ ||
               generation == Generation.GenerationAlpha;
    }
}