namespace GymBroApi.Helpers;

public static class TierCalculator
{
    public static string GetTier(double totalLifted) => totalLifted switch
    {
        < 50 => "🛋️ Couch Potato",
        < 150 => "🐣 Newbie Gains",
        < 300 => "🐀 Gym Rat",
        < 500 => "💪 Beast Mode",
        _ => "🐐 Sigma Grindset"
    };
}