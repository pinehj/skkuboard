using System;
using System.Collections.Generic;

public class Post
{
    public readonly string ID;
    public readonly User Writer;
    public DateTime PostTime { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    // 접근제한자 ..
    public List<User> Likes;

    public Post(string id, User writer, string content, List<User> likes)
    {
        // 글 내용 검증
        var contentSpecification = new ContentSpecification();
        if (!contentSpecification.IsSatisfiedBy(content))
        {
            throw new Exception(contentSpecification.ErrorMassage);
        }

        // 닉네임 검증
        var nicknameSpecification = new NicknameSpecification();
        //닉네임명세가 명세 인터페이스 구현을 안하고 있음. 함수명도 다름
        if (!nicknameSpecification.IsSatifiedBy(writer.Nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMassage);
        }

        //email 검증
        var emailSpecification = new EmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(writer.Email))
        {
            throw new Exception(emailSpecification.ErrorMassage);
        }

        ID = id;
        Writer = writer;
        PostTime = DateTime.Now;
        Content = content;

        Likes = likes;
    }

    public Post(PostDTO postDTO)
    {
        // 글 내용 검증
        var contentSpecification = new ContentSpecification();
        if (!contentSpecification.IsSatisfiedBy(postDTO.Content))
        {
            throw new Exception(contentSpecification.ErrorMassage);
        }

        // 닉네임 검증
        var nicknameSpecification = new NicknameSpecification();
        //닉네임명세가 명세 인터페이스 구현을 안하고 있음. 함수명도 다름
        if (!nicknameSpecification.IsSatifiedBy(postDTO.Writer.Nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMassage);
        }

        //email 검증
        var emailSpecification = new EmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(postDTO.Writer.Email))
        {
            throw new Exception(emailSpecification.ErrorMassage);
        }

        if(postDTO.PostTime == new DateTime())
        {
            throw new Exception("작성일은 비어있을 수 없습니다.");
        }

        ID = postDTO.ID;
        Writer = postDTO.Writer;
        PostTime = postDTO.PostTime;
        Content = postDTO.Content;
        Likes = postDTO.Likes;
    }

    public PostDTO ToDTO()
    {
        return new PostDTO(this);
    }

    public void ModifyContent(string content)
    {
        var contentSpecification = new ContentSpecification();
        if (!contentSpecification.IsSatisfiedBy(content))
        {
            throw new Exception(contentSpecification.ErrorMassage);
        }

        Content = content;
    }

    public void AddLike(User user)
    {
        // 같은 유저 있을 경우 추가 x
        if (!Likes.Exists(u => u.Email == user.Email))
        {
            Likes.Add(user);
        }
    }

    public void CancelLike(User user)
    {
        Likes.RemoveAll(u => u.Email == user.Email);
    }
}
