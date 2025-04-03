using Microsoft.AspNetCore.Routing;

namespace Company.PL.Helper
{
    public static class DocumentSettings
    {
        //1-Upload
        //ImageName
        public static string UploadFile(IFormFile file, string FolderName)
        {
            //1-GetfolderLocation
            //filePath

            //var FolderPath = Directory.GetCurrentDirectory()+ "\\wwwroot\\Files\\ " + FolderName
           var FolderPath = Path.Combine(Directory.GetCurrentDirectory() ,@"wwwroot\Files" ,FolderName);
            //2-Get file name and make it unique by guid
            var fileName = $"{Guid.NewGuid()}{file.FileName}";
            var FilePath= Path.Combine(FolderPath, fileName);
           using var FileStream = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(FileStream);
            return fileName;
        }
        //2-Delete
        public static void DeleteFile(string fileName, string FolderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files" , FolderName ,fileName);
            if (File.Exists(filePath)) 
                File.Delete(filePath);
        }

    }
}
