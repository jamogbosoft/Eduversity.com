using Eduversity.com.Shared.Models.Data;

namespace Eduversity.com.Client.Services.LevelService
{
    public class LevelService : ILevelService
    {
        public List<Level> Levels
        {
            get

            {
                List<Level> levels = new();

                for (int i = 100; i <= 700; i += 100)
                {
                    levels.Add(new Level { Name = $"{i}L", Value = i.ToString() });
                }
                return levels;
            }
        }
    }
}
