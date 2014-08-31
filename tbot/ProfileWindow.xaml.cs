using System.Windows;
using tbot.bot;

namespace tbot{
    public partial class ProfileWindow : Window{
        public delegate void OnProfileHandler(Profile profile);

        public ProfileWindow(){
            InitializeComponent();
            Loaded += OnPageLoaded;
        }

        public event OnProfileHandler OnProfile;

        private void OnPageLoaded(object sender, RoutedEventArgs e){
            DataContext = new Profile();
        }

        private void Apply(object sender, RoutedEventArgs e){
            var profile = (Profile) DataContext;
            if (profile.AccessToken == null || profile.AccessTokenSecret == null || profile.ConsumerSecret == null ||
                profile.ConsumerKey == null){
                MessageBox.Show("All fields have to be filled out.", "Validation", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            if (OnProfile != null) OnProfile(profile);
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e){
            Close();
        }
    }
}