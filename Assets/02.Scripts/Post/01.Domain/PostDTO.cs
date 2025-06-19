using System;
using UnityEngine;

public class PostDTO
{
    public readonly Guid ID;
    public readonly string Writer;
    public readonly DateTime PostTime;
    public readonly string Title;
    public readonly string Content;

    //private List<User> _likes;

    public PostDTO(Post post)
    {
        ID = post.ID;
        Writer = post.Writer;
        PostTime = post.PostTime;
        Title = post.Title;
        Content = post.Content;
    }
}
