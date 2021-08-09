using Models;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IBusinessModel
    {
        bool CapLose(int userId);
        string CapRecord(int userId);
        bool CapWin(int userId);
        string RandomPokemon();
        bool RpsLose(int userId);
        string RpsRecord(int userId);
        bool RpsWin(int userId);
        string WhosThatPokemonGame();
        bool WtpLose(int userId);
        string WtpRecord(int userId);
        bool WtpWin(int userId);
        GameInfo CreateGame(GameDetail gameDetail);
        List<GameDetail> GetGameInfoList();
        GameInfo DeleteGame(int id);
        GameInfo ModifyGame(GameDetail gameDetail);
        GameDetail SingleGame(int id);
    }
}