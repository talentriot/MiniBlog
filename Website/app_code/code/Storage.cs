using System.Collections.Generic;
using System.Web;
using System.Web.Hosting;

public static class Storage
{
    private static readonly IStorageProvider _storageProvider =
        new AzureFileSystemStorage();

    public static List<Post> GetAllPosts()
    {
        if (HttpRuntime.Cache["posts"] == null)
        {
            var list = _storageProvider.GetAllPosts();
            HttpRuntime.Cache.Insert("posts", list);
        }

        if (HttpRuntime.Cache["posts"] != null)
        {
            return (List<Post>)HttpRuntime.Cache["posts"];
        }
        return new List<Post>();
    }

    // Can this be done async?
    public static void Save(Post post)
    {
        var saveStatus = _storageProvider.Save(post);
        if (saveStatus.IsNew)
        {
            var posts = GetAllPosts();
            posts.Insert(0, post);
            posts.Sort((p1, p2) => p2.PubDate.CompareTo(p1.PubDate));
            HttpRuntime.Cache.Insert("posts", posts);
        }
        else
        {
            Blog.ClearStartPageCache();
        }
    }

    public static void Delete(Post post)
    {
        _storageProvider.Delete(post);
    }

    
}