using Microsoft.EntityFrameworkCore;
using GymBroApi.Services;
using GymBroApi.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace GymBroApi.Models;

public class GymBro
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string WeightClass { get; set; } = string.Empty;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public double BenchPressKg { get; set; } = 0;
    public double SquatKg { get; set; } = 0;
    public double DeadliftKg { get; set; } = 0;

    [NotMapped] // 👈 EF Core will not create a column for these
    public double TotalLifted => BenchPressKg + SquatKg + DeadliftKg;

    [NotMapped]
    public string Tier => TierCalculator.GetTier(TotalLifted);
}