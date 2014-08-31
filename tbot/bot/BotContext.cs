using System.ComponentModel;

namespace tbot.bot{
    public class BotContext{
        private readonly TwitterConnection TwitterConnection;
        private BackgroundWorker bw;

        public BotContext(Profile profile){
            TwitterConnection = new TwitterConnection(profile);
            setStrategy(new RetweetStrategy(TwitterConnection, 60, "gamedev"));
        }

        public void setStrategy(BotStragety botStrategy){
            if (bw != null){
                bw.CancelAsync();
            }
            bw = new BackgroundWorker();
            bw.DoWork += (sender, args) =>{
                while (true){
                    botStrategy.run();
                }
            };
            bw.RunWorkerAsync();
        }
    }
}