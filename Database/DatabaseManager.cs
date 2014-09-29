using System;
using System.Data.SQLite;
using System.IO;
using System.Transactions;

namespace Database
{
    public class DatabaseManager :IDatabaseManager
    {
        private const string DatabaseName = "Databasefile.sqlite";
        private const string DatabaseConnection = "Data Source=" + DatabaseName + ";Version=3;";
        private const string SupportForeignKeysSql = "PRAGMA foreign_keys = ON;";

        public DatabaseManager()
        {
            CreateDatabase(DatabaseName);
            
        }

        public long InsertPlayerToDatabase(string playerName, string playerTeam)
        {
            var dbConnection = EstablishConnectionToDatabase(DatabaseConnection);

            object playerId;
            using (var trans = new TransactionScope())
            {
                var command = new SQLiteCommand(string.Format(DatabaseQueries.InsertPlayerToDatabase, playerName, playerTeam), dbConnection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand(string.Format(DatabaseQueries.FuckThis, playerName, playerTeam), dbConnection);
                playerId = command.ExecuteScalar();
                trans.Complete();
            }

            return (long)playerId;
        }

        public void EditPlayerInDatabase(string playerName, string playerTeam)
        {
            var dbConnection = EstablishConnectionToDatabase(DatabaseConnection);
            using (var trans = new TransactionScope())
            {
                var command = new SQLiteCommand(string.Format(DatabaseQueries.DeletePlayerFromDatabase, playerName, playerTeam), dbConnection);
                command.ExecuteNonQuery();
                trans.Complete();
                trans.Dispose();
            }
        }

        public void DeletePlayerFromDatabase(int playerId)
        {
            var dbConnection = EstablishConnectionToDatabase(DatabaseConnection);
            using (var trans = new TransactionScope())
            {
                var command = new SQLiteCommand(string.Format(DatabaseQueries.DeletePlayerFromDatabase, playerId), dbConnection);
                command.ExecuteNonQuery();
                trans.Complete();
                trans.Dispose();
            }
        }


        public SQLiteConnection EstablishConnectionToDatabase(string connection)
        {
            var dbConnection = new SQLiteConnection(connection);
            dbConnection.Open();
            EnableUsageOfForeignKeysInDatabase(dbConnection);
            return dbConnection;
        }

        private void CreateDatabase(string dbName)
        {
            if (File.Exists(String.Format(@"{0}\{1}", Environment.CurrentDirectory, dbName))) return;
            using (var transactionScope = new TransactionScope())
            {
                SQLiteConnection.CreateFile(dbName);
                var dbConnection = EstablishConnectionToDatabase(DatabaseConnection);

                var command = new SQLiteCommand(DatabaseQueries.CreatePlayerTable, dbConnection);
                command.ExecuteNonQuery();

                command = new SQLiteCommand(DatabaseQueries.CreateFixtureTable, dbConnection);
                command.ExecuteNonQuery();

                dbConnection.Close();
                dbConnection.Dispose();

                transactionScope.Complete();
                transactionScope.Dispose();
            }
        }

        private static void EnableUsageOfForeignKeysInDatabase(SQLiteConnection dbConnection)
        {
            var command = new SQLiteCommand(SupportForeignKeysSql, dbConnection);
            command.ExecuteNonQuery();
        }
    }
}
