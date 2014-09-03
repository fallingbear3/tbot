using System;
using System.Timers;

namespace tbot.bot{
    public class RetweetStrategy : AbstractBotStrategy{
        private bool canRetweet;

        public RetweetStrategy(TwitterConnection connection, int retweetTimer, params string[] hashtags)
            : base(connection, hashtags){
            var myTimer = new Timer();
            myTimer.Elapsed += (sender, args) => canRetweet = true;
            myTimer.Interval = retweetTimer;
            myTimer.Enabled = true;
            myTimer.AutoReset = true;
        }

        public override void run(){
            if (canRetweet && stream.Count > 0){
                rt();
                stream.Clear();
                canRetweet = false;
            }
        }

        private async void rt(){
            try{
                await connection.retweet(stream.Dequeue().Id);
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}