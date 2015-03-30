using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

/// <summary>
/// Summary description for AzureFileSystemStorage
/// </summary>
public class AzureFileSystemStorage : IStorageProvider
{
	public AzureFileSystemStorage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public List<Post> GetAllPosts()
    {
        throw new NotImplementedException();
    }

    public StorageSaveStatus Save(Post post)
    {
        var blob = AzureStorageService.Instance();
        var postContainer = blob.BlobPostStorage;

        post.LastModified = DateTime.UtcNow;

        XDocument doc = new XDocument(
                        new XElement("post",
                            new XElement("title", post.Title),
                            new XElement("slug", post.Slug),
                            new XElement("author", post.Author),
                            new XElement("pubDate", post.PubDate.ToString("yyyy-MM-dd HH:mm:ss")),
                            new XElement("lastModified", post.LastModified.ToString("yyyy-MM-dd HH:mm:ss")),
                            new XElement("excerpt", post.Excerpt),
                            new XElement("content", post.Content),
                            new XElement("ispublished", post.IsPublished),
                            new XElement("categories", string.Empty),
                            new XElement("comments", string.Empty)
                        ));

        XElement categories = doc.XPathSelectElement("post/categories");
        foreach (string category in post.Categories)
        {
            categories.Add(new XElement("category", category));
        }

        XElement comments = doc.XPathSelectElement("post/comments");
        foreach (Comment comment in post.Comments)
        {
            comments.Add(
                new XElement("comment",
                    new XElement("author", comment.Author),
                    new XElement("email", comment.Email),
                    new XElement("website", comment.Website),
                    new XElement("ip", comment.Ip),
                    new XElement("userAgent", comment.UserAgent),
                    new XElement("date", comment.PubDate.ToString("yyyy-MM-dd HH:m:ss")),
                    new XElement("content", comment.Content),
                    new XAttribute("isAdmin", comment.IsAdmin),
                    new XAttribute("isApproved", comment.IsApproved),
                    new XAttribute("id", comment.ID)
                ));
        }

        MemoryStream ms = new MemoryStream();
        XmlWriterSettings xws = new XmlWriterSettings();
        xws.OmitXmlDeclaration = true;
        xws.Indent = true;

        using (XmlWriter xw = XmlWriter.Create(ms, xws))
        {
            doc.WriteTo(xw);
        }


        var saveStatus = new StorageSaveStatus();

        try
        {
            if (postContainer.Get(post.ID) == null)
            {
                saveStatus.IsNew = true;
            }
            else
            {
                saveStatus.IsNew = false;
            }
        }
        catch (Exception)
        {
            saveStatus.IsNew = true;
        }

        try
        {       
            postContainer.Save(ms,post.ID);
            saveStatus.WasSuccessful = true;
        }
        catch (Exception)
        {
            saveStatus.WasSuccessful = false;
            saveStatus.IsNew = false;
        }
    
        return saveStatus;
    
    }

    public void Delete(Post post)
    {
        throw new NotImplementedException();
    }
}