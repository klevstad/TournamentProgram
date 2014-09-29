using System;
using System.Linq;
using Core;

namespace Client.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        private readonly IPlayerManagement m_playerManagement;

        private string m_playerName;
        private string m_playerTeam;

        public PlayerViewModel(IPlayerManagement playerManagement)
        {
            m_playerManagement = playerManagement;
            m_playerName = "";
            m_playerTeam = "";

            AddPlayerToDatabase = new DelegateCommand(AddPlayer, SaveButtonIsEnabled);
        }

        private void AddPlayer(object o = null)
        {
            m_playerManagement.AddPlayer(m_playerName, m_playerTeam);
        }

        private void DeletePlayer(object o = null)
        {
            // helpme
            m_playerManagement.DeletePlayer(12345678);
        }

        public string PlayerName
        {
            get { return m_playerName; }
            set
            {
                SetProperty(ref m_playerName, value, () => PlayerName);
                AddPlayerToDatabase.RaiseCanExecuteChanged();
            }
            
        }

        public string PlayerTeam
        {
            get { return m_playerTeam; }
            set
            {
                SetProperty(ref m_playerTeam, value, () => PlayerTeam);
                AddPlayerToDatabase.RaiseCanExecuteChanged();
            }
        }

        public Boolean SaveButtonIsEnabled()
        {
            if (PlayerName.Any(char.IsDigit)) return false;
            if (string.IsNullOrWhiteSpace(PlayerName)) return false;
            return !string.IsNullOrWhiteSpace(PlayerTeam);
        }

        public DelegateCommand AddPlayerToDatabase { get; set; }

    }
}
