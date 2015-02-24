using System;
using System.Reflection;
using log4net;
using tbot.model;

namespace tbot.bot
{
    public class RetweetStrategy : AbstractBotStrategy
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public RetweetStrategy(TwitterConnection connection, params string[] hashtags)
            : base(connection, hashtags, 300000)
        {
        }

        public override void run()
        {
            retweet();
        }

        private async void retweet()
        {
            TwitterStreamObject tweet = get();
            if (tweet != null)
            {
                log.Info("Retweeting tweet [tweedId=" + tweet.Id + "]");
                try
                {
                    await connection.retweet(tweet.Id);
                }
                catch (Exception e)
                {
                    log.Error("Retweeting failed " + e.Message);
                    retweet();
                }
            }
        }
    }
}