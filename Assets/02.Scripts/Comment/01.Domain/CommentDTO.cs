using Firebase.Firestore;
using System;

[FirestoreData]
public class CommentDTO
{
    [FirestoreProperty]
    public string PostID { get; set; }

    [FirestoreProperty]
    public string CommentID { get; set; }

    [FirestoreProperty]
    public string Content { get; set; }

    [FirestoreProperty]
    public string WriterName { get; set; }

    [FirestoreProperty]
    public string WriterEmail { get; set; }

    [FirestoreProperty]
    public DateTime PostTime { get; set; }

    public CommentDTO() { }

    public CommentDTO(Comment comment)
    {
        PostID = comment.PostID;
        CommentID = comment.CommentID;
        Content = comment.Content;
        WriterName = comment.WriterName;
        WriterEmail = comment.WriterEmail;
        PostTime = comment.PostTime;
    }
}
