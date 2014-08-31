using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using tbot.Annotations;
using tbot.bot;

namespace tbot{
    public partial class MainWindow : Window, INotifyPropertyChanged{
        private readonly ProfileManager profileManager;
        private Bot bot;

        public MainWindow(){
            profileManager = new ProfileManager();
            DataContext = profileManager;
            InitializeComponent();
            SetupConsole();
        }

        public ConsoleStream BotConsoleStream { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void SetupConsole(){
            BotConsoleStream = new ConsoleStream();
            BotConsoleStream.OnConsoleFeed += feed =>{
                BotConsole.AppendText(feed);
                OnPropertyChanged("BotConsoleStream");
            };
            Console.SetOut(BotConsoleStream);
        }

        private void AddProfile(object sender, RoutedEventArgs e){
            var newWindow = new ProfileWindow();
            newWindow.OnProfile += profileManager.add;
            newWindow.ShowDialog();
        }

        private async void StartBot(object sender, RoutedEventArgs e){
            var profile = (Profile) Profiles.SelectedItem;

            bot = new Bot();
            await bot.Init(profile);
            await bot.getMyTweets(3);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null){
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Clear(object sender, RoutedEventArgs e){
            BotConsole.Text = "";
        }
    }

    public class ConsoleStream : TextWriter{
        public delegate void ConsoleFeedHandler(string feed);

        public override Encoding Encoding{
            get { return Encoding.UTF8; }
        }

        public event ConsoleFeedHandler OnConsoleFeed;

        public override void Write(char value){
            base.Write(value);
            if (OnConsoleFeed != null) OnConsoleFeed(value.ToString());
        }
    }
}