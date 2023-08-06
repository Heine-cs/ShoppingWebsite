using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using OllieShop.Models;


namespace OllieShop.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string photoUpload(IFormFile photo, long SRID)
        {//replacePhotoRoadSwitch收到true值就將圖庫內圖檔使用新圖檔替換，false為直接新增到圖庫

            if (photo != null)
            {
                FileInfo fileInfo = new FileInfo(photo.FileName);

                if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".png")
                {
                    //上傳檔案目前在photo物件中，要存入wwwroot > img > SpecificationsPicture > 對應Seller的資料夾
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/img/SpecificationsPicture/Seller_{SRID}");

                    //path目錄不存在時創造一個新的資料夾，並把上傳圖片命名為1.jpg放入
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);

                        string newFileName = $"1.{fileInfo.Extension}";

                        //path組合新檔案名稱
                        string filePath = Path.Combine(path, newFileName);


                        //FileStream參數需要路徑與名稱才能用FileStream()
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            //使用copy to的方法改檔名並複製到資料夾
                            photo.CopyTo(stream);
                        }
                        //處理要傳到規格資料表的路徑字串，處理成像是:/img/SpecificationsPicture/Seller_SRID/1.jpg
                        path = $"/img/SpecificationsPicture/Seller_{SRID}/" + newFileName;
                        return path;
                    }
                    else
                    {
                        //從資料夾撈出所有檔名轉為檔名陣列
                        string?[] fileNames = Directory.GetFiles(path)
                            .Select(Path.GetFileName)
                            .ToArray();
                        //提取檔名陣列長度+1作為新檔名
                        string newestPhotoName = (fileNames.Length + 1).ToString();
                        newestPhotoName += fileInfo.Extension;

                        //path組合檔案名稱後使用copy to的方法改檔名並複製到資料夾
                        string filePath = Path.Combine(path, newestPhotoName);

                        //FileStream參數需要路徑與名稱才能用FileStream()
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            //使用copy to的方法改檔名並複製到資料夾
                            photo.CopyTo(stream);
                        }
                        //處理要傳到規格資料表的路徑字串，處理成像是:/img/SpecificationsPicture/Seller_SRID/5.png
                        path = $"/img/SpecificationsPicture/Seller_{SRID}/" + newestPhotoName;
                        return path;
                    }
                }
                return "上傳圖片檔案類型僅限jpg或png兩種格式之一";

            }
            return "請上傳圖片檔案喔!";
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public string photoReplace(IFormFile photo, long SRID,string oldPhotoPath)
        {//replacePhotoRoadSwitch收到true值就將圖庫內圖檔使用新圖檔替換，false為直接新增到圖庫

            if (photo != null)
            {
                FileInfo fileInfo = new FileInfo(photo.FileName);

                if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".png")
                {
                    //刪除舊檔案
                    string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    FileInfo oldPhoto = new FileInfo(rootPath + oldPhotoPath);

                    if (oldPhoto.Exists)
                    {
                        oldPhoto.Delete();
                    }


                    //上傳的新檔案目前在photo物件中
                    //FileStream參數需要路徑與名稱才能用FileStream()，新檔案路徑要組合wwwroot
                    string oldPhotoPathWithoutExtension = oldPhotoPath.Substring(0, oldPhotoPath.Length-4);
                    oldPhotoPathWithoutExtension += ("_" + DateTime.Now.Ticks.ToString()) + oldPhoto.Extension;


                    string newPhotoPath = rootPath+oldPhotoPathWithoutExtension;
                    using (var stream = new FileStream(newPhotoPath, FileMode.Create))
                    {
                        //使用copy to的方法將檔案複製到資料夾
                        photo.CopyTo(stream);
                    }

                    // 回傳新的檔案路徑讓呼叫者頁面的 img src 重寫後重載圖片
                    return oldPhotoPathWithoutExtension;

                }
                return "上傳圖片檔案類型僅限jpg或png兩種格式之一";
            }
            return "請上傳圖片檔案喔!";

        }



    }
}
