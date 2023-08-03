using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace OllieShop.Controllers
{
    public class TestFunctionController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormFile photo)
        {

            if (photo != null)
            {

                FileInfo fileInfo = new FileInfo(photo.FileName);

                if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".png")
                {
                    //上傳檔案目前在photo物件中，要存入wwwroot > img > SpecificationsPicture > 對應Seller的資料夾，目前寫死
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/SpecificationsPicture/Seller_4");
                    //path不存在時創造一個新的資料夾
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);

                        string newFileName = "1.jpg";

                        string filePath = Path.Combine(path, newFileName);

                        //fileNameWithPath參數需要路徑與名稱才能用FileStream()
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            photo.CopyTo(stream);
                        }
                    }
                    else
                    {
                        //從資料夾撈出所有檔名

                        string[] fileNames = Directory.GetFiles(path)
                            .Select(Path.GetFileName)
                            .ToArray();
                        string stringLeftPart;
                        int i = 0;
                        int[] filesNumber = new int[fileNames.Length];
                        foreach (string fileName in fileNames)
                        {
                            stringLeftPart = fileName.Substring(0, fileName.Length - 4 );
                            filesNumber[i] = int.Parse(stringLeftPart);
                            i++;
                        }
                        int newFileName1 = filesNumber[0];
                        int newFileName2 = filesNumber[1];
                        ////path組合檔案名稱後使用copy to的方法就會改檔案名稱並傳入資料夾了
                        //string filePath = Path.Combine(path, "");

                        ////fileNameWithPath參數需要路徑與名稱才能用FileStream()
                        //using (var stream = new FileStream(filePath, FileMode.Create))
                        //{
                        //    photo.CopyTo(stream);
                        //}
                    }
             

                }

            }
            return View(photo);
        }
    }
}
