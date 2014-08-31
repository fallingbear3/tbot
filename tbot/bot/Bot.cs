using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace tbot.bot{
    internal class Bot{
        private TwitterContext twitterCtx;

        public async Task Init(Profile profile){
            twitterCtx = new TwitterContext(new SingleUserAuthorizer{
                CredentialStore = new SingleUserInMemoryCredentialStore{
                    ConsumerKey = profile.ConsumerKey,
                    ConsumerSecret = profile.ConsumerSecret,
                    AccessToken = profile.AccessToken,
                    AccessTokenSecret = profile.AccessTokenSecret
                }
            });
        }

        public async Task getMyTweets(int count)
        {
            List<Status> friendTweets = await (from tweet in twitterCtx.Status
                                               where tweet.Type == StatusType.User && tweet.Count == count
                                               select tweet).ToListAsync();

            if (friendTweets != null)
            {
                Console.WriteLine("Tweets: \n");
                friendTweets.ForEach(tweet =>
                {
                    if (tweet != null && tweet.User != null)
                        Console.WriteLine(
                            "User: " + tweet.User.Name +
                            "\nTweet: " + tweet.Text +
                            "\nTweet ID: " + tweet.ID + "\n");
                });
            }
        }


        public async Task getTweetsByHashTag(int count)
        {
            List<Status> friendTweets = await (from tweet in twitterCtx.Status
                                               where tweet.Type == StatusType.User && tweet.Count == count
                                               select tweet).ToListAsync();

            if (friendTweets != null)
            {
                Console.WriteLine("Tweets: \n");
                friendTweets.ForEach(tweet =>
                {
                    if (tweet != null && tweet.User != null)
                        Console.WriteLine(
                            "User: " + tweet.User.Name +
                            "\nTweet: " + tweet.Text +
                            "\nTweet ID: " + tweet.ID + "\n");
                });
            }
        }
    }
}