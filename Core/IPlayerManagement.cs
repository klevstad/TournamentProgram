namespace Core
{
    public interface IPlayerManagement
    {
        long AddPlayer(string playerName, string teamName);
        void EditPlayer(int playerId);
        void DeletePlayer(int playerId);
        void UpdatePlayerView();
    }
}
