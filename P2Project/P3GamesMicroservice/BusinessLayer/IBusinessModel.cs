namespace BusinessLayer
{
    public interface IBusinessModel
    {
        bool CapLose(int userId);
        double CapRecord(int userId);
        bool CapWin(int userId);
        string RandomPokemon();
        bool RpsLose(int userId);
        double RpsRecord(int userId);
        bool RpsWin(int userId);
        string WhosThatPokemonGame();
        bool WtpLose(int userId);
        double WtpRecord(int userId);
        bool WtpWin(int userId);
    }
}