namespace SABSync
{
    public class FeedInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (FeedInfo)) return false;
            return Equals((FeedInfo) obj);
        }

        public bool Equals(FeedInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name) && Equals(other.Url, Url);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ (Url != null ? Url.GetHashCode() : 0);
            }
        }
    }

}