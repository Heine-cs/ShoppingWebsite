using Microsoft.AspNetCore.Mvc;

namespace OllieShop.Models
{
    public class FileUpload
    {
        public string uploadAddressGenerator(IFormFile photo) {

            //if (photo != null)
            //{
            //    FileInfo fileInfo = new FileInfo(photo.FileName);

            //    //如果副檔名--JPG為TRUE OR PNG才放行
            //    if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".png")
            //    {
            //        //上傳檔案目前在photo物件中，要存入wwwroot > img > SpecificationsPicture > 對應Seller的資料夾，
            //        //而且要把檔名修改為SN號碼.JPG或PNG，要先告訴檔案路徑在哪
            //        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos");
            //        if (!Directory.Exists(path))
            //        {
            //            Directory.CreateDirectory(path);
            //        }


            //        string fileNameWithPath = Path.Combine(path, photo.FileName);
            //        //fileNameWithPath參數需要路徑與名稱才能用FileStream()
            //        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            //        {
            //            photo.CopyTo(stream);
            //        }
            //        ViewData["Message"] = "上傳成功";
            //        return View();
            //    }
            //}
            //ViewData["Message"] = "上傳失敗";
            //return View();



            return "";
        }

    }
}
