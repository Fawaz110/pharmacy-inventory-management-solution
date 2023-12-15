namespace pharmacy_inventory_management.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName) 
        {
            // 1. Get located Folder path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Files" , folderName);

            // 2. Get FileName and make it unique
            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";

            // 3. Get File Path
            var filePath = Path.Combine(folderPath, fileName);

            // 4. 
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }
    }
}
