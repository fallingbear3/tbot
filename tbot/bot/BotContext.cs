using System.Timers;

namespace tbot.bot
{
    public class BotContext : AbstractBotContext
    {
        public BotContext(Profile profile) : base(profile)
        {
            var retweetStrategy = new RetweetStrategy(TwitterConnection, "gamedev");

            setStrategy(retweetStrategy, 30000);
        }

        public void setStrategy(BotStragety botStrategy, int interval)
        {
            var timer = new Timer {Interval = interval, Enabled = true, AutoReset = true};
            timer.Elapsed += (sender, args) => botStrategy.run();
        }
    }
}