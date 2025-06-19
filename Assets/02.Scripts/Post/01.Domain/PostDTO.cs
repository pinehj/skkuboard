using Firebase.Firestore;
using System;
using System.Collections.Generic;

[FirestoreData]
public class PostDTO
{
    [FirestoreProperty]
    public string ID { get; private set; }
    [FirestoreProperty]
    public User Writer { get; private set; }
    [FirestoreProperty]
    public DateTime PostTime { get; private set; }
    [FirestoreProperty]
    public string Content { get; private set; }
    [FirestoreProperty]
    public List<User> Likes { get; private set; }


    public PostDTO()
    {

    }

    public PostDTO(Post post)
    {
        ID = post.ID;
        Writer = post.Writer;
        PostTime = post.PostTime;
        Content = post.Content;
        Likes = post.Likes;
    }
}
