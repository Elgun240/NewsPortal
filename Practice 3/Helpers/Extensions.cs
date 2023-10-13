using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Practice_3.Helpers
{
    public static  class Extensions
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }
        public static bool IsMore4mb(this IFormFile file) 
        {
            return file.Length / 1024 > 4096;        
        }
        public static async Task<string> SaveImageAsync(this IFormFile file,string path)
        {
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string pullpath = Path.Combine(path, filename);
            using (FileStream fileStream = new FileStream(pullpath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return filename;
        }

    }
    public enum Roles
    {
        Admin,
        Member

    }
}
