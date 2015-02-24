using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using tbot.model;
using tbot.utils;

namespace tbot.bot
{
    public abstract class AbstractBotStrategy : BotStragety
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected readonly TwitterConnection connection;
        private readonly int expirationMillis;
        private readonly Queue<TwitterStreamObject> stream = new Queue<TwitterStreamObject>();

        protected AbstractBotStrategy(TwitterConnection connection, IEnumerable<string> keywords, int expirationMillis)
        {
            this.connection = connection;
            this.expirationMillis = expirationMillis;
            connection.OnStreamUpdate += stream.Enqueue;

            try
            {
                connection.startStream(keywords);
            }
            catch (Exception e)
            {
                log.Error("Unable to start stream: " + e.Message);
            }
        }

        public abstract void run();

        public TwitterStreamObject get()
        {
            if (stream.Count == 0) return null;

            log.Info("Tweets in queue: " + stream.Count);
            TwitterStreamObject tweet = stream.Dequeue();
            return !isExpired(tweet) ? tweet : get();
        }

        private bool isExpired(TwitterStreamObject tweet)
        {
            return long.Parse(tweet.TimestampMs) < DateTimeUtils.CurrentTimeMillis() - expirationMillis;
        }
    }
}