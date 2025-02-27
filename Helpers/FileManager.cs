namespace MVCShop.Helpers;

public static class FileManager
{
    public static string UploadFile(IFormFile file) 
    {
        var imageName = @$"{Guid.NewGuid().ToString()}{file.FileName}";

        var path = Path.Combine("wwwroot", "images", imageName); 

        using (var fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            file.CopyTo(fs);
        }
        return imageName;
    }

    public static void DeleteFile(string fileName) 
    {
        var path = Path.Combine("wwwroot", "images", fileName);

        if(File.Exists(path)) 
        {
            File.Delete(path);
        }

    }
}
