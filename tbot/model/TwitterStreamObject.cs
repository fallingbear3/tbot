using Newtonsoft.Json;

namespace tbot.model{
    public class User{
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("url")]
        public object Url { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("protected")]
        public bool Protected { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }

        [JsonProperty("listed_count")]
        public int ListedCount { get; set; }

        [JsonProperty("favourites_count")]
        public int FavouritesCount { get; set; }

        [JsonProperty("statuses_count")]
        public int StatusesCount { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("utc_offset")]
        public object UtcOffset { get; set; }

        [JsonProperty("time_zone")]
        public object TimeZone { get; set; }

        [JsonProperty("geo_enabled")]
        public bool GeoEnabled { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("contributors_enabled")]
        public bool ContributorsEnabled { get; set; }

        [JsonProperty("is_translator")]
        public bool IsTranslator { get; set; }

        [JsonProperty("profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        [JsonProperty("profile_background_image_url")]
        public string ProfileBackgroundImageUrl { get; set; }

        [JsonProperty("profile_background_image_url_https")]
        public string ProfileBackgroundImageUrlHttps { get; set; }

        [JsonProperty("profile_background_tile")]
        public bool ProfileBackgroundTile { get; set; }

        [JsonProperty("profile_link_color")]
        public string ProfileLinkColor { get; set; }

        [JsonProperty("profile_sidebar_border_color")]
        public string ProfileSidebarBorderColor { get; set; }

        [JsonProperty("profile_sidebar_fill_color")]
        public string ProfileSidebarFillColor { get; set; }

        [JsonProperty("profile_text_color")]
        public string ProfileTextColor { get; set; }

        [JsonProperty("profile_use_background_image")]
        public bool ProfileUseBackgroundImage { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("profile_image_url_https")]
        public string ProfileImageUrlHttps { get; set; }

        [JsonProperty("profile_banner_url")]
        public string ProfileBannerUrl { get; set; }

        [JsonProperty("default_profile")]
        public bool DefaultProfile { get; set; }

        [JsonProperty("default_profile_image")]
        public bool DefaultProfileImage { get; set; }

        [JsonProperty("following")]
        public object Following { get; set; }

        [JsonProperty("follow_request_sent")]
        public object FollowRequestSent { get; set; }

        [JsonProperty("notifications")]
        public object Notifications { get; set; }
    }

    public class Hashtag{
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("indices")]
        public int[] Indices { get; set; }
    }

    public class Url{
        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("expanded_url")]
        public string ExpandedUrl { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("indices")]
        public int[] Indices { get; set; }
    }

    public class UserMention{
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("indices")]
        public int[] Indices { get; set; }
    }

    public class Entities{
        [JsonProperty("hashtags")]
        public Hashtag[] Hashtags { get; set; }

        [JsonProperty("trends")]
        public object[] Trends { get; set; }

        [JsonProperty("urls")]
        public Url[] Urls { get; set; }

        [JsonProperty("user_mentions")]
        public UserMention[] UserMentions { get; set; }

        [JsonProperty("symbols")]
        public object[] Symbols { get; set; }
    }

    public class TwitterStreamObject{
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public ulong Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public object InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public object InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public object InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public object InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public object InReplyToScreenName { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("geo")]
        public object Geo { get; set; }

        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty("place")]
        public object Place { get; set; }

        [JsonProperty("contributors")]
        public object Contributors { get; set; }

        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public int FavoriteCount { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty("possibly_sensitive")]
        public bool PossiblySensitive { get; set; }

        [JsonProperty("filter_level")]
        public string FilterLevel { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("timestamp_ms")]
        public string TimestampMs { get; set; }
    }
}