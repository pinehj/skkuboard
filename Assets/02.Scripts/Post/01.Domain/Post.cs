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

    public Post(Guid iD, string writer, DateTime postTime, string content)
    {
        var contentSpecification = new ContentSpecification();
        if (!contentSpecification.IsSatisfiedBy(content))
        {
            throw new Exception(contentSpecification.ErrorMassage);
        }
        ID = iD;
        Writer = writer;    
        PostTime = postTime;
        Content = content;
    }

    public PostDTO ToDTO()
    {
        return new PostDTO(this);
    }
}
