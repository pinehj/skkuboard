using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PostManager : MonoBehaviour
{
    private PostRepository _repository;
    private List<Post> _posts;
    private List<PostDTO> Posts => _posts.ConvertAll(post => post.ToDTO());

    public event Action OnDataChanged;


    private void Init()
    {
        _repository = new PostRepository();
        _posts = new List<Post>();
    }
    
}
