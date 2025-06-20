using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor.Build.Content;
using UnityEngine;

public class PostManager : Singleton<PostManager>
{
    private PostRepository _repository;
    private List<Post> _posts;
    public List<PostDTO> Posts => _posts.ConvertAll(post => post.ToDTO());

    public event Action OnDataChanged;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    private void Init()
    {
        _repository = new PostRepository();
        _posts = new List<Post>();
    }

    public async Task Test()
    {
        User user = new User()
        {
            Nickname = "ss",
            Email = "ss@sss.com"
        };
        if(await TryAddPost(user, "내용내용\n내용"))
        {
            Debug.Log("성공");
        }
        else
        {
            Debug.Log("실패");
        }
    }
    public async Task Test2()
    {
        if(await TryLoadPosts())
        {
            Debug.Log("성공");
        }
        else
        {
            Debug.Log("실패");
        }

        Debug.Log((await TryLoadPost(_posts[0].ID)).Content);
    }

    public async Task<bool> TryLoadPosts()
    {
        try
        {
            _posts = (await _repository.GetPosts()).ConvertAll(post => new Post(post));
            return true;
        }
        catch(Exception e)
        {
            return false;
        }

    }

    public async Task<PostDTO> TryLoadPost(string postID)
    {
        try
        {
            _posts = (await _repository.GetPosts()).ConvertAll(post => new Post(post));

            return await _repository.GetPost(postID);
        }
        catch (Exception e)
        {
            return null;
        }

    }

    public async Task<bool> TryAddPost(User writer, string content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            Debug.Log($"내용 비어있나:{content}dd");
        }
        
        Post newPost = new Post(System.Guid.NewGuid().ToString(), writer, content, new List<User>());

        try
        {
            await _repository.AddPost(newPost.ToDTO());
            return true;
        }
        catch(Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public async Task<bool> TryModifyPost(string postID, string content)
    {
        Post post = FindPostByID(postID);
        if(post == null)
        {
            return false;
        }
        post.ModifyContent(content);
        try
        {
            await _repository.UpdatePost(post.ToDTO());
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private Post FindPostByID(string id)
    {
        foreach(Post post in _posts)
        {
            if(post.ID == id)
            {
                return post;
            }
        }
        return null;
    }
    public async Task<PostDTO> TryLike(string postID, User liker)
    {
        Post post = FindPostByID(postID);
        if (post == null)
        {
            return null;
        }
        post.AddLike(liker);
        try
        {
            await _repository.UpdatePost(post.ToDTO());
            return post.ToDTO();
        }
        catch (Exception e)
        {
            return null;
        }
    }
    public async Task<bool> TryDeletePost(string id)
    {
        try
        {
            await _repository.DeletePost(id.ToString());
            return true;
        }
        catch(Exception e)
        {
            return false;
        }
    }

}
