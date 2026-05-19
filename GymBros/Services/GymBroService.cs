using GymBroApi.Data;
using GymBroApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GymBroApi.Services;

public class GymBroService
{
    private readonly AppDbContext _db;

    public GymBroService(AppDbContext db) => _db = db;

    // CRUD
    public async Task<List<GymBro>> GetAll() =>
        await _db.Bros.ToListAsync();

    public async Task<GymBro?> GetById(int id) =>
        await _db.Bros.FindAsync(id);

    public async Task<GymBro> Create(GymBro bro)
    {
        bro.JoinedAt = DateTime.UtcNow;
        _db.Bros.Add(bro);
        await _db.SaveChangesAsync();
        return bro;
    }

    public async Task<GymBro?> Update(int id, GymBro updated)
    {
        var bro = await GetById(id);
        if (bro is null) return null;

        bro.Name = updated.Name;
        bro.WeightClass = updated.WeightClass;
        await _db.SaveChangesAsync();
        return bro;
    }

    public async Task<bool> Delete(int id)
    {
        var bro = await GetById(id);
        if (bro is null) return false;

        _db.Bros.Remove(bro);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<GymBro?> LogPr(int id, PrRequest pr)
    {
        var bro = await GetById(id);
        if (bro is null) return null;

        if (pr.BenchPressKg.HasValue && pr.BenchPressKg > bro.BenchPressKg)
            bro.BenchPressKg = pr.BenchPressKg.Value;

        if (pr.SquatKg.HasValue && pr.SquatKg > bro.SquatKg)
            bro.SquatKg = pr.SquatKg.Value;

        if (pr.DeadliftKg.HasValue && pr.DeadliftKg > bro.DeadliftKg)
            bro.DeadliftKg = pr.DeadliftKg.Value;

        await _db.SaveChangesAsync();
        return bro;
    }

    public async Task<List<object>> GetLeaderboard()
    {
        var bros = await _db.Bros.OrderByDescending(b =>
            b.BenchPressKg + b.SquatKg + b.DeadliftKg).ToListAsync();

        return bros.Select((b, index) => (object)new
        {
            Rank = index + 1,
            b.Name,
            b.WeightClass,
            b.Tier,
            b.BenchPressKg,
            b.SquatKg,
            b.DeadliftKg,
            TotalLifted = b.TotalLifted
        }).ToList();
    }
}