using System.Data.SQLite;

namespace Database
{
    public interface IDatabaseManager
    {
        long InsertPlayerToDatabase(string playerName, string teamName);
        void EditPlayerInDatabase(string playerName, string playerTeam);
        void DeletePlayerFromDatabase(int playerId);
        SQLiteConnection EstablishConnectionToDatabase(string connection);
    }
}
