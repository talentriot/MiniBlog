﻿using System.Collections.Generic;

/// <summary>
/// Helps Storage.cs class save to different locations
/// </summary>
public interface IStorageProvider
{
    List<Post> GetAllPosts();
    void Save(Post post);
    void Delete(Post post);
}