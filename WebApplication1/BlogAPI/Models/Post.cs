public class Post
{
    public int id { get; set; }
    public string title { get; set; }
    public  string content { get; set; }
    public string author { get; set; }
    public Comment[] comments { get; set; }
    public int Likes { get; set; }
}