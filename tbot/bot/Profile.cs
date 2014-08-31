namespace tbot.bot{
    public class Profile{
        public string Name { get; set; }

        public string ConsumerSecret { get; set; }

        public string ConsumerKey { get; set; }

        public string AccessTokenSecret { get; set; }

        public string AccessToken { get; set; }

        public override string ToString(){
            return Name;
        }
    }
}