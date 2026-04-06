using GymBroApi.Models;
using System.Xml.Linq;

namespace GymBroApi.Services;

public class GymBroService
{
	private readonly List<GymBro> _bros = new();
	private int _nextId = 1;

	// CRUD
	public List<GymBro> GetAll() => _bros;

	public GymBro? GetById(int id) =>
		_bros.FirstOrDefault(b => b.Id == id);

	public GymBro Create(GymBro bro)
	{
		bro.Id = _nextId++;
		bro.JoinedAt = DateTime.UtcNow;
		_bros.Add(bro);
		return bro;
	}

	public GymBro? Update(int id, GymBro updated)
	{
		var bro = GetById(id);
		if (bro is null) return null;

		bro.Name = updated.Name;
		bro.WeightClass = updated.WeightClass;
		return bro;
	}

	public bool Delete(int id)
	{
		var bro = GetById(id);
		if (bro is null) return false;
		_bros.Remove(bro);
		return true;
	}

	// Log a PR — only updates if it's an actual new record
	public GymBro? LogPr(int id, PrRequest pr)
	{
		var bro = GetById(id);
		if (bro is null) return null;

		if (pr.BenchPressKg.HasValue && pr.BenchPressKg > bro.BenchPressKg)
			bro.BenchPressKg = pr.BenchPressKg.Value;

		if (pr.SquatKg.HasValue && pr.SquatKg > bro.SquatKg)
			bro.SquatKg = pr.SquatKg.Value;

		if (pr.DeadliftKg.HasValue && pr.DeadliftKg > bro.DeadliftKg)
			bro.DeadliftKg = pr.DeadliftKg.Value;

		return bro;
	}

	// Leaderboard — sorted by total, includes rank
	public List<object> GetLeaderboard()
	{
		return _bros
			.OrderByDescending(b => b.TotalLifted)
			.Select((b, index) => (object)new
			{
				Rank = index + 1,
				b.Name,
				b.WeightClass,
				b.Tier,
				b.BenchPressKg,
				b.SquatKg,
				b.DeadliftKg,
				b.TotalLifted
			})
			.ToList();
	}
}