using ExerciseWeather.Models.Exercise;
using SQLite;
using System.Threading.Tasks;

namespace ExerciseWeather.Persistence
{
    public class StatsDatabase
    {
        private static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<StatsDatabase> Instance = new AsyncLazy<StatsDatabase>(async () =>
        {
            StatsDatabase instance = new StatsDatabase();
            CreateTableResult result = await Database.CreateTableAsync<UserStats>();
            return instance;
        });

        public StatsDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public async Task<UserStats> GetStatsAsync(string id)
        {
            return await Database.Table<UserStats>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task AddStatsAsync(UserStats stats)
        {
            _ = await Database.InsertAsync(stats);
        }

        public async Task UpdateStatsAsync(UserStats stats)
        {
            _ = await Database.UpdateAsync(stats);
        }

        public async Task DeleteStatsAsync(UserStats stats)
        {
            _ = await Database.DeleteAsync(stats);
        }

        // TESTING ONLY
        protected async Task DropTableAsync()
        {
            await Database.DropTableAsync<UserStats>();
        }
    }
}