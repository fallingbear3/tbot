using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using LinqToTwitter;

namespace tbot.bot{
    public class Bot : BotCommands{
        private TwitterContext twitterCtx;

        public  Bot(Profile profile)
        {
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

        public void Invoke(string cmd){
            string[] cmdParams = cmd.Split(' ');

            MethodInfo theMethod = typeof (BotCommands).GetMethod(cmdParams[0]);
            if (theMethod == null){
                throw new ArgumentException("Method [" + cmdParams[0] + "] does not exist.");
            }

            if (cmdParams.Length > 1){
                ParameterInfo[] parameterInfos = theMethod.GetParameters();
                string[] parameterValues = cmdParams.Skip(1).ToArray();
                object[] parameters = GetParametersForType(parameterValues, parameterInfos);
                theMethod.Invoke(this, parameters);
            }
            else{
                theMethod.Invoke(this, new object[0]);
            }
        }

        private object[] GetParametersForType(IEnumerable<string> values, IEnumerable<ParameterInfo> infos){
            return values.Zip(infos, (v, i) => ConvertType(v, i.ParameterType)).ToArray();
        }

        private object ConvertType(object value, Type conversionType){
            //Check if type is Nullable
            if (!conversionType.IsGenericType || conversionType.GetGenericTypeDefinition() != typeof (Nullable<>))
                return Convert.ChangeType(value, conversionType);

            if (value == null) return null;

            //Type is Nullable and we have a value, override conversion type to underlying
            //type for the Nullable to avoid exception in Convert.ChangeType
            var nullableConverter = new NullableConverter(conversionType);
            conversionType = nullableConverter.UnderlyingType;

            return Convert.ChangeType(value, conversionType);
        }
    }

    public interface BotCommands
    {
        Task mytweets(int count);
        Task stream();
    }
}