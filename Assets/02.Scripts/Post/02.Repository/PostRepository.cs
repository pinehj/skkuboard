using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class PostRepository
{
    public async Task AddPost(PostDTO post)
    {
        DocumentReference docRef = FirebaseManager.Instance.DB.Collection("Posts").Document(post.ID);
        await docRef.SetAsync(post);
    }

    public async Task<List<PostDTO>> GetPosts()
    {
        Query query = FirebaseManager.Instance.DB.Collection("Posts").OrderByDescending("PostTime");
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

        List<PostDTO> posts = new List< PostDTO > ();
        foreach(DocumentSnapshot documentSnapshot in querySnapshot.Documents)
        {
            posts.Add(documentSnapshot.ConvertTo<PostDTO>());
        }

        return posts;
    }

    public async Task<PostDTO> GetPost(string postID)
    {
        DocumentReference docRef = FirebaseManager.Instance.DB.Collection("Posts").Document(postID);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            PostDTO post = snapshot.ConvertTo<PostDTO>();
            return post;
        }
        else
        {
            return null;
        }
    }

    public async Task UpdatePost(PostDTO post)
    {
        DocumentReference docRef = FirebaseManager.Instance.DB.Collection("Posts").Document(post.ID.ToString());
        await docRef.SetAsync(post);
    }
    public async Task DeletePost(string postID)
    {
        DocumentReference cityRef = FirebaseManager.Instance.DB.Collection("Posts").Document(postID.ToString());
        await cityRef.DeleteAsync();
    }
}
