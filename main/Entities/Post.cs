namespace EF6Test.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }

        public Role Role { get; set; }
        public virtual Blog Blog { get; set; }
    }
}