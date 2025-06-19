using System;
using System.Collections.Generic;

public class Comment
{
    public string PostID { get; private set; }
    public readonly string CommentID;
    public string Content { get; private set; }
    public readonly string WriterName;
    public readonly string WriterEmail;
    public DateTime PostTime { get; private set; }

    public Comment(string postID, string commentID, string content, string writerName, string writerEmail)
    {
        PostID = postID;
        CommentID = commentID;
        Content = content;
        WriterName = writerName;
        WriterEmail = writerEmail;
        PostTime = DateTime.Now;
    }

    public Comment(CommentDTO dto)
    {
        PostID = dto.PostID;
        CommentID = dto.CommentID;
        Content = dto.Content;  
        WriterName = dto.WriterName;
        WriterEmail = dto.WriterEmail;
        PostTime = dto.PostTime;
    }

    // 카톡게시판에 댓글 수정기능 없음
    /*public void EditComment(string content)
    {
        Content = content;
    }*/
}
