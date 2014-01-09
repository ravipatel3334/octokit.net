namespace Octokit
{
    public class Author
    {
        public string Login { get; set; }

        public int Id { get; set; }

        public string AvatarUrl { get; set; }

        /// <summary>
        /// Hex Gravatar identifier
        /// </summary>
        public string GravatarId { get; set; }

        public string Url { get; set; }

        public string HtmlUrl { get; set; }

        public string FollowersUrl { get; set; }

        public string FollowingUrl { get; set; }

        public string GistsUrl { get; set; }

        public string StarredUrl { get; set; }

        public string SubscriptionsUrl { get; set; }

        public string OrganizationsUrl { get; set; }

        public string ReposUrl { get; set; }

        public string EventsUrl { get; set; }

        public string ReceivedEventsUrl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "This is what is returned from the api")]
        public string Type { get; set; }

        public bool SiteAdmin { get; set; }
    }
}