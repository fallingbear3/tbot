using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media;
using tbot.Annotations;
using tbot.bot;

namespace tbot{
    public partial class MainWindow : Window, INotifyPropertyChanged{
        public MainWindow(){
            ProfileManager = new ProfileManager();
            DataContext = this;
            InitializeComponent();
            SetupConsole();
        }

        private Bot Bot { get; set; }

        public ProfileManager ProfileManager { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetupConsole(){
            Console.SetOut(createBotConsoleStream(Colors.Black));
            Console.SetError(createBotConsoleStream(Colors.Red));
        }

        private ConsoleStream createBotConsoleStream(Color streamColor){
            var botConsoleStream = new ConsoleStream(streamColor);
            botConsoleStream.OnConsoleFeed += (feed, color) =>{
                BotConsole.AppendText(feed, color);
                OnPropertyChanged("BotConsoleStream");
            };
            return botConsoleStream;
        }

        private void AddProfile(object sender, RoutedEventArgs e){
            var newWindow = new ProfileWindow();
            newWindow.OnProfile += ProfileManager.add;
            newWindow.ShowDialog();
        }

        private void StartBot(object sender, RoutedEventArgs e){
            var profile = (Profile) Profiles.SelectedItem;
            if (checkValid(profile, "You have to choose profile first.") == 1) return;
            Bot = new Bot(profile);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null){
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Clear(object sender, RoutedEventArgs e){
            BotConsole.Document.Blocks.Clear();
        }

        private void Cmd(object sender, RoutedEventArgs e){
            if (checkValid(Bot, "You have to start bot first.") == 1) return;
            try{
                Bot.Invoke(Commands.Text);
            }
            catch (Exception ex){
                Console.Error.Write(ex.Message+"\n");
            }
        }

        private int checkValid(Object o, string message){
            if (o == null){
                Console.Error.Write(message + "\n");
                return 1;
            }
            return 0;
        }
    }

    public class ConsoleStream : TextWriter{
        public delegate void ConsoleFeedHandler(string feed, Color color);

        private readonly Color color;

        public ConsoleStream(Color color){
            this.color = color;
        }

        public override Encoding Encoding{
            get { return Encoding.UTF8; }
        }

        public event ConsoleFeedHandler OnConsoleFeed;

        public override void Write(char value)
        {
            base.Write(value);
            if (OnConsoleFeed != null) OnConsoleFeed(value.ToString(), color);
        }
    }
}