namespace tbot.bot
{
    public class AbstractBotContext
    {
        protected AbstractBotContext(Profile profile)
        {
            TwitterConnection = new TwitterConnection(profile);
        }

        protected TwitterConnection TwitterConnection { get; private set; }
    }
}