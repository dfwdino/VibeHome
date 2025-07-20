using KidsChore.Domain.Entities;

namespace KidsChore.UI.Shared
{
    public class AchievementSystem
    {
        public static List<Achievement> GetAvailableAchievements()
        {
            return new List<Achievement>
            {
                new Achievement
                {
                    Id = "first_chore",
                    Name = "First Steps",
                    Description = "Complete your first chore",
                    Icon = "🎯",
                    Points = 10,
                    Type = AchievementType.FirstCompletion
                },
                new Achievement
                {
                    Id = "week_warrior",
                    Name = "Week Warrior",
                    Description = "Complete 5 chores in a week",
                    Icon = "⚡",
                    Points = 25,
                    Type = AchievementType.WeeklyGoal
                },
                new Achievement
                {
                    Id = "money_master",
                    Name = "Money Master",
                    Description = "Earn $50 in a week",
                    Icon = "💰",
                    Points = 50,
                    Type = AchievementType.EarningsGoal
                },
                new Achievement
                {
                    Id = "consistency_king",
                    Name = "Consistency King",
                    Description = "Complete chores 7 days in a row",
                    Icon = "👑",
                    Points = 100,
                    Type = AchievementType.Streak
                },
                new Achievement
                {
                    Id = "variety_victor",
                    Name = "Variety Victor",
                    Description = "Complete 10 different types of chores",
                    Icon = "🎨",
                    Points = 75,
                    Type = AchievementType.Variety
                },
                new Achievement
                {
                    Id = "speed_demon",
                    Name = "Speed Demon",
                    Description = "Complete 3 chores in one day",
                    Icon = "🏃",
                    Points = 30,
                    Type = AchievementType.DailyGoal
                }
            };
        }

        public static List<Achievement> CheckAchievements(List<ChoreCompletion> completions, int kidId)
        {
            var achievements = new List<Achievement>();
            var availableAchievements = GetAvailableAchievements();
            var kidCompletions = completions.Where(c => c.KidId == kidId).ToList();

            foreach (var achievement in availableAchievements)
            {
                if (HasEarnedAchievement(achievement, kidCompletions))
                {
                    achievements.Add(achievement);
                }
            }

            return achievements;
        }

        private static bool HasEarnedAchievement(Achievement achievement, List<ChoreCompletion> completions)
        {
            if (!completions.Any()) return false;

            return achievement.Type switch
            {
                AchievementType.FirstCompletion => completions.Count >= 1,
                AchievementType.WeeklyGoal => CheckWeeklyGoal(completions, 5),
                AchievementType.EarningsGoal => CheckEarningsGoal(completions, 50m),
                AchievementType.Streak => CheckStreak(completions, 7),
                AchievementType.Variety => CheckVariety(completions, 10),
                AchievementType.DailyGoal => CheckDailyGoal(completions, 3),
                _ => false
            };
        }

        private static bool CheckWeeklyGoal(List<ChoreCompletion> completions, int goal)
        {
            var weekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var weeklyCompletions = completions.Where(c => c.CompletionDateTime >= weekStart).ToList();
            return weeklyCompletions.Count >= goal;
        }

        private static bool CheckEarningsGoal(List<ChoreCompletion> completions, decimal goal)
        {
            var weekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var weeklyEarnings = completions
                .Where(c => c.CompletionDateTime >= weekStart)
                .Sum(c => c.Price);
            return weeklyEarnings >= goal;
        }

        private static bool CheckStreak(List<ChoreCompletion> completions, int goal)
        {
            var dates = completions
                .Select(c => c.CompletionDateTime.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            if (dates.Count < goal) return false;

            var currentStreak = 1;
            var maxStreak = 1;

            for (int i = 1; i < dates.Count; i++)
            {
                if ((dates[i] - dates[i - 1]).Days == 1)
                {
                    currentStreak++;
                    maxStreak = Math.Max(maxStreak, currentStreak);
                }
                else
                {
                    currentStreak = 1;
                }
            }

            return maxStreak >= goal;
        }

        private static bool CheckVariety(List<ChoreCompletion> completions, int goal)
        {
            var uniqueChores = completions
                .Select(c => c.ChoreItemId)
                .Distinct()
                .Count();
            return uniqueChores >= goal;
        }

        private static bool CheckDailyGoal(List<ChoreCompletion> completions, int goal)
        {
            var today = DateTime.Today;
            var todayCompletions = completions
                .Where(c => c.CompletionDateTime.Date == today)
                .Count();
            return todayCompletions >= goal;
        }
    }

    public class Achievement
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int Points { get; set; }
        public AchievementType Type { get; set; }
        public bool IsEarned { get; set; }
        public DateTime? EarnedDate { get; set; }
    }

    public enum AchievementType
    {
        FirstCompletion,
        WeeklyGoal,
        EarningsGoal,
        Streak,
        Variety,
        DailyGoal
    }
} 