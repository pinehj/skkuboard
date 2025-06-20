using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public struct PostAndComments
{
    public string PostID;
    public List<Comment> Comments;

    public PostAndComments(string postID, List<Comment> comments)
    {
        PostID = postID;
        Comments = comments;
    }
}

/// <summary>
/// 처음 글 - 글에 대한 댓글들로 struct 만들어서 가지고 있기
/// 글을 클릭했을 경우, 그 글의 댓글들 불러오기-ui
/// 댓글 쓰기, 댓글 수정, 자신이 단 댓글 삭제 가능
/// </summary>
public class CommentManager : Singleton<CommentManager>
{
    private string _currentPostID;
    private List<Comment> _currentPostComments;
    public List<CommentDTO> CurrentPostComments => _currentPostComments.ConvertAll(c => new CommentDTO(c));

    private List<PostAndComments> _postAndComments;

    private CommentRepository _repository;

    public Action OnLoadAllComments;
    public Action OnLoadPostComments;

    protected override void Start()
    {
        base.Start();
        FirebaseManager.Instance.OnFirebaseLinked += Init;
    }

    private void Init()
    {
        _repository = new CommentRepository();
        _repository.Init();
        _postAndComments = new List<PostAndComments>();
        _currentPostComments = new List<Comment>();

        _ = LoadAllComments();
    }

    public async Task LoadAllComments()
    {
        List<string> postIDs = await _repository.GetAllPostIDs();

        foreach (string postID in postIDs)
        {
            List<CommentDTO> commentDTOs = await _repository.GetComments(postID);
            List<Comment> comments = commentDTOs.ConvertAll(dto => new Comment(dto));
            _postAndComments.Add(new PostAndComments(postID, comments));
            Debug.Log($"로드된 PostID: {postID} / 댓글 수: {comments.Count}");
        }

        Debug.Log($"총 {postIDs.Count}개의 게시글 댓글 불러오기 완료");
        OnLoadAllComments?.Invoke();
    }

    public void GetComments(string postID)
    {
        _currentPostID = postID;
        var entry = _postAndComments.Find(p => p.PostID == postID);
        if (entry.Comments == null)
        {
            Debug.LogWarning($"PostID '{postID}'에 대한 댓글 데이터가 없습니다.");
            _currentPostComments = new List<Comment>();
        }
        else
        {
            _currentPostComments = entry.Comments;
        }
        OnLoadPostComments?.Invoke();
    }

    public async Task AddComment(string content)
    {
        string commentID = Guid.NewGuid().ToString();
        string writerName = AccountManager.Instance.User.Nickname;
        string writerEmail = AccountManager.Instance.User.Email;
        // 테스트용
        //string writerName = "테스트용";
        //string writerEmail = "test@emai.com";

        var dto = new CommentDTO
        {
            PostID = _currentPostID,
            CommentID = commentID,
            Content = content,
            WriterName = writerName,
            WriterEmail = writerEmail,
            PostTime = DateTime.Now
        };

        await _repository.AddComment(dto);

        // 로컬에도 반영
        Comment newComment = new Comment(dto);
        _currentPostComments.Add(newComment);
    }

    public async Task DeleteComment(string commentID)
    {
        await _repository.DeleteComment(_currentPostID, commentID);

        Comment deleteComment = _currentPostComments.Find(c => c.CommentID == commentID);
        if (deleteComment != null)
        {
            _currentPostComments.Remove(deleteComment);
            Debug.Log($"댓글 삭제 완료: {commentID}");
        }
        else
        {
            Debug.LogWarning($"삭제하려는 댓글을 로컬 목록에서 찾을 수 없습니다: {commentID}");
        }
    }
}