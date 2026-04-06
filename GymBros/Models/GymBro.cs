using GymBroApi.Helpers;

namespace GymBroApi.Models;

public class GymBro
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string WeightClass { get; set; } = string.Empty; // e.g. "75kg", "90kg"
	public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

	// PRs in kg
	public double BenchPressKg { get; set; } = 0;
	public double SquatKg { get; set; } = 0;
	public double DeadliftKg { get; set; } = 0;

	// Computed
	public double TotalLifted => BenchPressKg + SquatKg + DeadliftKg;
	public string Tier => TierCalculator.GetTier(TotalLifted);
}