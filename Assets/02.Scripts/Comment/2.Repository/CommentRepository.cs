using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Firestore;

public class CommentRepository 
{
    private const string COLLECTION_NAME = "Comments";
    private const string SUBCOLLECTION_NAME = "CommentList";
    private FirebaseFirestore _db;

    public void Init()
    {
        _db = FirebaseManager.Instance.DB;
    }

    public async Task AddComment(CommentDTO dto)
    {
        if (string.IsNullOrEmpty(dto.PostID) || string.IsNullOrEmpty(dto.CommentID))
        {
            Debug.Log("PostID나 CommentID가 비어 있습니다. 저장 불가");
            return;
        }

        DocumentReference postDocRef = _db.Collection(COLLECTION_NAME).Document(dto.PostID);
        await postDocRef.SetAsync(new Dictionary<string, object> {{ "PostID", dto.PostID }}, SetOptions.MergeAll); // 이미 있으면 병합 (덮어쓰기 방지)

        DocumentReference docRef = _db.Collection(COLLECTION_NAME).Document(dto.PostID).Collection(SUBCOLLECTION_NAME).Document(dto.CommentID);
        await docRef.SetAsync(dto);
        Debug.Log($"댓글 추가 완료: {dto.CommentID} / {dto.Content}");
    }

    // 카톡에는 댓글 수정 기능 X
    /*public async Task UpdateComment(CommentDTO dto)
    {
        if (string.IsNullOrEmpty(dto.PostID) || string.IsNullOrEmpty(dto.CommentID))
        {
            Debug.Log("PostID나 CommentID가 비어 있습니다. 수정 불가");
            return;
        }

        DocumentReference docRef = _db.Collection(COLLECTION_NAME).Document(dto.PostID).Collection(SUBCOLLECTION_NAME).Document(dto.CommentID);

        await docRef.SetAsync(dto, SetOptions.Overwrite);
        Debug.Log($"댓글 수정 완료: {dto.CommentID}");
    }*/

    public async Task DeleteComment(string postID, string commentID)
    {
        if (string.IsNullOrEmpty(commentID))
        {
            Debug.Log("CommentID가 비어 있습니다. 삭제할 수 없습니다.");
            return;
        }

        DocumentReference docRef = _db.Collection(COLLECTION_NAME).Document(postID).Collection(SUBCOLLECTION_NAME).Document(commentID);
        await docRef.DeleteAsync();
        Debug.Log($"댓글 삭제 완료: {commentID}");
    }

    public async Task<List<CommentDTO>> GetComments(string postID)
    {
        if (string.IsNullOrEmpty(postID))
        {
            Debug.Log("PostID가 비어 있습니다. 댓글 불러올 수 없음");
            return new List<CommentDTO>();
        }

        CollectionReference commentCollection = _db.Collection(COLLECTION_NAME).Document(postID).Collection(SUBCOLLECTION_NAME);

        QuerySnapshot snapshot = await commentCollection.OrderBy("PostTime").GetSnapshotAsync();

        List<CommentDTO> result = new List<CommentDTO>();
        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            CommentDTO dto = document.ConvertTo<CommentDTO>();
            result.Add(dto);
        }

        return result;
    }

    public async Task<List<string>> GetAllPostIDs()
    {
        List<string> postIDs = new List<string>();

        QuerySnapshot snapshot = await FirebaseFirestore.DefaultInstance.Collection("Comments").GetSnapshotAsync();

        foreach (DocumentSnapshot doc in snapshot.Documents)
        {
            postIDs.Add(doc.Id); // 각 게시글의 문서 ID가 곧 PostID
        }

        return postIDs;
    }
}
