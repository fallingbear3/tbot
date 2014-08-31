using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;
using Newtonsoft.Json;
using tbot.model;

namespace tbot.bot{
    public class TwitterConnection{
        public delegate bool FilterHandler(Streaming streaming);

        public delegate void StreamHandler(TwitterStreamObject queue);

        private readonly JsonSerializerSettings settings = new JsonSerializerSettings{
            NullValueHandling = NullValueHandling.Ignore
        };

        private readonly TwitterContext twitterCtx;

        public TwitterConnection(Profile profile){
            twitterCtx = new TwitterContext(new SingleUserAuthorizer{
                CredentialStore = new SingleUserInMemoryCredentialStore{
                    ConsumerKey = profile.ConsumerKey,
                    ConsumerSecret = profile.ConsumerSecret,
                    AccessToken = profile.AccessToken,
                    AccessTokenSecret = profile.AccessTokenSecret
                }
            });
        }

        public async Task mytweets(int count){
            List<Status> friendTweets = await (from tweet in twitterCtx.Status
                where tweet.Type == StatusType.User && tweet.Count == count
                select tweet).ToListAsync();

            if (friendTweets != null){
                Console.Write("Tweets: \n");
                friendTweets.ForEach(tweet =>{
                    if (tweet != null && tweet.User != null)
                        Console.Write(
                            "User: " + tweet.User.Name +
                            "\nTweet: " + tweet.Text +
                            "\nTweet ID: " + tweet.ID + "\n");
                });
            }
        }

        public async Task retweet(ulong tweetID){
            await twitterCtx.RetweetAsync(tweetID);
        }

        public async Task startStream(IEnumerable<string> keywords){
            try{
                await
                    (from strm in twitterCtx.Streaming
                        where
                            strm.Type == StreamingType.Filter &&
                            strm.Track == string.Join(",", keywords)
                        select strm)
                        .StartAsync(async strm => updateStream(strm));
            }
            catch (Exception e){
                Console.WriteLine(e);
            }
        }

        private void updateStream(StreamContent strm){
            if (OnStreamUpdate != null){
                var tso = JsonConvert.DeserializeObject<TwitterStreamObject>(strm.Content, settings);
                if (tso != null) OnStreamUpdate(tso);
            }
        }

        public event StreamHandler OnStreamUpdate;
    }
}