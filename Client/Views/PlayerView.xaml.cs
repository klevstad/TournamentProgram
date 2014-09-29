using System.Windows.Controls;
using Client.ViewModels;
using Core;
using Database;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for AddPlayerView.xaml
    /// </summary>
    public partial class AddPlayerView
    {
        public AddPlayerView()
        {
            InitializeComponent();
            DataContext = new PlayerViewModel(new PlayerManagement(new DatabaseManager()));
        }
    }
}
