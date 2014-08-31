using System.Collections.Generic;
using tbot.model;

namespace tbot.bot{
    public abstract class AbstractBotStrategy : BotStragety{
        protected readonly TwitterConnection connection;
        protected readonly Queue<TwitterStreamObject> stream = new Queue<TwitterStreamObject>();

        protected AbstractBotStrategy(TwitterConnection connection, IEnumerable<string> keywords){
            this.connection = connection;
            connection.OnStreamUpdate += stream.Enqueue;
            connection.startStream(keywords);
        }

        public abstract void run();
    }
}