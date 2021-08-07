using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IBusinessModel
    {
        Task<string> RandomPokemonAsync();
        Task<bool> CapLoseAsync(int userId);
        Task<string> CapRecordAsync(int userId);
        Task<bool> CapWinAsync(int userId);
        Task<bool> RpsLoseAsync(int userId);
        Task<string> RpsRecordAsync(int userId);
        Task<bool> RpsWinAsync(int userId);
        Task<string> WhosThatPokemonGameAsync();
        Task<bool> WtpLoseAsync(int userId);
        Task<string> WtpRecordAsync(int userId);
        Task<bool> WtpWinAsync(int userId);
        Task<List<GameInfo>> GameInfoListAsync();
        Task<bool> AddGameInfoAsync(GameInfo gameInfo);
    }
}