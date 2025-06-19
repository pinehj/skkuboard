using System;
using System.Collections.Generic;

public class Post
{
    public readonly Guid ID;
    public readonly string Writer;
    public DateTime PostTime { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }

    //private List<User> _likes;

    public Post(Guid iD, string writer, DateTime postTime, string title, string content)
    {
        ID = iD;
        Writer = writer;
        PostTime = postTime;
        Title = title;
        Content = content;
    }

    public PostDTO ToDTO()
    {
        return new PostDTO(this);
    }
}
