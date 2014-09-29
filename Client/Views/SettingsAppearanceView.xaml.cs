using Client.ViewModels;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for SettingsAppearance.xaml
    /// </summary>
    public partial class SettingsAppearance
    {
        public SettingsAppearance()
        {
            InitializeComponent();
            DataContext = new SettingsAppearanceViewModel();
        }
    }
}
