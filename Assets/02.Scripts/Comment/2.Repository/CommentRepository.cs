using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Firestore;

public class CommentRepository : MonoBehaviour
{
    private FirebaseFirestore _db = FirebaseFirestore.DefaultInstance;
    private const string COLLECTION_NAME = "Comments";

    private void Awake()
    {
        _db = FirebaseFirestore.DefaultInstance;
    }

    public async Task AddComment(CommentDTO dto)
    {
        if (string.IsNullOrEmpty(dto.CommentID))
        {
            Debug.LogError("CommentID가 비어 있습니다. Firestore에 저장할 수 없습니다.");
            return;
        }

        DocumentReference docRef = _db.Collection(COLLECTION_NAME).Document(dto.CommentID);
        await docRef.SetAsync(dto);
        Debug.Log($"댓글 추가 완료: {dto.CommentID}");
    }

    public async Task UpdateComment(CommentDTO dto)
    {
        if (string.IsNullOrEmpty(dto.CommentID))
        {
            Debug.LogError("CommentID가 비어 있습니다. 수정할 수 없습니다.");
            return;
        }

        DocumentReference docRef = _db.Collection(COLLECTION_NAME).Document(dto.CommentID);
        await docRef.SetAsync(dto, SetOptions.Overwrite);
        Debug.Log($"댓글 수정 완료: {dto.CommentID}");
    }

    public async Task DeleteComment(string commentID)
    {
        if (string.IsNullOrEmpty(commentID))
        {
            Debug.LogError("CommentID가 비어 있습니다. 삭제할 수 없습니다.");
            return;
        }

        DocumentReference docRef = _db.Collection(COLLECTION_NAME).Document(commentID);
        await docRef.DeleteAsync();
        Debug.Log($"댓글 삭제 완료: {commentID}");
    }

    public async Task<List<CommentDTO>> GetComments()
    {
        QuerySnapshot snapshot = await _db.Collection("Comments").OrderByDescending("PostTime").GetSnapshotAsync();

        List<CommentDTO> result = new List<CommentDTO>();

        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            CommentDTO dto = document.ConvertTo<CommentDTO>();
            result.Add(dto);
        }

        return result;
    }

}
