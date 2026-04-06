using System.Reflection;

namespace GymBroApi.Helpers;

public static class RoastEngine
{
    public static string Roast(Models.GymBro a, Models.GymBro b)
    {
        var winner = a.TotalLifted >= b.TotalLifted ? a : b;
        var loser = winner == a ? b : a;
        double diff = Math.Abs(a.TotalLifted - b.TotalLifted);

        // Tied
        if (diff < 1)
            return $"🤝 {a.Name} and {b.Name} are equally mid. Both need to go back to the gym.";

        // Loser roasts based on how big the gap is
        string gapInsult = diff switch
        {
            < 20 => $"{loser.Name} is only {diff}kg behind. Still loses though. Tragic.",
            < 60 => $"{loser.Name} is {diff}kg behind. That's like a whole person of weakness.",
            < 120 => $"{loser.Name} is {diff}kg behind. Bro skipped more than just leg day.",
            _ => $"{loser.Name} is {diff}kg behind. Are they even trying? 💀"
        };

        string tierJoke = (loser.Tier, winner.Tier) switch
        {
            ("🛋️ Couch Potato", _) =>
                $"{loser.Name} is literally a {loser.Tier}. The couch has more muscle.",
            ("🐣 Newbie Gains", "🐐 Sigma Grindset") =>
                $"{loser.Name} still has newbie gains while {winner.Name} achieved full sigma. Different species.",
            _ =>
                $"{winner.Name} is {winner.Tier} and {loser.Name} is stuck at {loser.Tier}. Embarrassing."
        };

        return $"🏆 {winner.Name} WINS! {gapInsult} {tierJoke}";
    }
}