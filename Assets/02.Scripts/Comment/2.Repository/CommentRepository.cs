using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Firestore;

public class CommentRepository : MonoBehaviour
{
    public async Task AddComment()
    {

    }

    public async Task UpdateComment()
    {

    }

    public async Task DeleteComment()
    {

    }

    public async Task<List<CommentDTO>> GetComments()
    {
        var _db = FirebaseFirestore.DefaultInstance;

        QuerySnapshot snapshot = await _db.Collection("comments").OrderByDescending("PostTime").GetSnapshotAsync();

        List<CommentDTO> result = new List<CommentDTO>();

        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            CommentDTO dto = document.ConvertTo<CommentDTO>();
            result.Add(dto);
        }

        return result;
    }

}
