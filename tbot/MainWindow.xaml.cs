using System.Windows;
using tbot.bot;

namespace tbot{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        private readonly ProfileManager profileManager;

        public MainWindow(){
            profileManager = new ProfileManager();
            DataContext = profileManager;
            InitializeComponent();
        }

        private void AddProfile(object sender, RoutedEventArgs e){
            var newWindow = new ProfileWindow();
            newWindow.OnProfile += profileManager.add;
            newWindow.ShowDialog();
        }

        private void StartBot(object sender, RoutedEventArgs e){
            var profile = (Profile) Profiles.SelectedItem;
            // bot.Init(profile);
        }
    }
}