using System;
using System.Collections.Generic;

public class Comment
{
    public string Content { get; private set; }
    public readonly string WriterName;
    public readonly string WriterEmail;
    // Account�� ���� ���ɼ� ����
    public List<string> LikedUsers;
    public DateTime PostTime { get; private set; }

    public Comment(string content, string writerName, string writerEmail, List<string> likedUsers, DateTime postTime)
    {
        Content = content;
        WriterName = writerName;
        WriterEmail = writerEmail;
        LikedUsers = likedUsers;
        PostTime = postTime;
    }
}
