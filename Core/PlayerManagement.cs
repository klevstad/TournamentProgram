using Database;

namespace Core
{
    public class PlayerManagement :IPlayerManagement
    {
        private IDatabaseManager m_database;

        public PlayerManagement(IDatabaseManager database)
        {
            m_database = database;
        }


        public long AddPlayer(string playerName, string teamName)
        {
            return m_database.InsertPlayerToDatabase(playerName, teamName);
            
        }

        public void EditPlayer(int playerId)
        {
            
        }

        public void DeletePlayer(int playerId)
        {
            
        }

        public void UpdatePlayerView()
        {
            
        }


    }
}
