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
        if (string.IsNullOrWhiteSpace(postID))
            throw new Exception("postID는 비어 있을 수 없습니다.");
        if (string.IsNullOrWhiteSpace(commentID))
            throw new Exception("commentID는 비어 있을 수 없습니다.");
        if (string.IsNullOrWhiteSpace(content))
            throw new Exception("content는 비어 있을 수 없습니다.");
        if (content.Length > 200)
            throw new Exception("댓글은 200자를 초과할 수 없습니다.");
        if (string.IsNullOrWhiteSpace(writerName))
            throw new Exception("writerName은 비어 있을 수 없습니다.");
        if (string.IsNullOrWhiteSpace(writerEmail))
            throw new Exception("writerEmail은 비어 있을 수 없습니다.");

        PostID = postID;
        CommentID = commentID;
        Content = content;
        WriterName = writerName;
        WriterEmail = writerEmail;
        PostTime = DateTime.Now;
    }

    public Comment(CommentDTO dto)
    : this(dto.PostID, dto.CommentID, dto.Content, dto.WriterName, dto.WriterEmail)
    {
        PostTime = dto.PostTime;
    }

    // 카톡게시판에 댓글 수정기능 없음
    /*public void EditComment(string content)
    {
        Content = content;
    }*/
}
