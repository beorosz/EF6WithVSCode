namespace EF6Test.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }

        /// <summary>
        /// Property added just for demonstration purposes
        /// </summary>
        public Role Role { get; set; }
        public virtual Blog Blog { get; set; }
    }
}