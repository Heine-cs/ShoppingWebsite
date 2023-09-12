using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using OllieShop.Models;
using System.IO;


namespace OllieShop.Controllers
{
    public class FileUploadController : Controller
    {
        //與商品相關的上傳action
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
                        string[] fileNames = Directory.GetFiles(path)
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
        public string photoReplace(IFormFile photo,string oldPhotoPath)
        {
            if (photo != null)
            {
                FileInfo fileInfo = new FileInfo(photo.FileName);

                if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".png")
                {
                    string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string oldPhotoFullPath = rootPath + oldPhotoPath;

                    using (var stream = new FileStream(oldPhotoFullPath, FileMode.Create))
                    {
                        //使用copy to的方法將新檔案覆蓋舊檔
                        photo.CopyTo(stream);
                    }

                    return "上傳圖片成功";
                }
                return "上傳圖片檔案類型僅限jpg或png兩種格式之一";
            }
            return "請上傳圖片檔案喔!";

        }

        //業者概述圖上傳，用於初次註冊業者用戶
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string sellerOverviewPhotoUpload(IFormFile photo, long URID)
        {//replacePhotoRoadSwitch收到true值就將圖庫內圖檔使用新圖檔替換，false為直接新增到圖庫

            if (photo != null)
            {
                FileInfo fileInfo = new FileInfo(photo.FileName);

                if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".png")
                {
                    //上傳檔案目前在photo物件中，要存入wwwroot > img > ShopOverviewPhoto資料夾中
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/img/ShopOverviewPhoto");

                    //path目錄不存在時創造一個新的資料夾，並把上傳圖片命名為Seller_SRID.jpg放入
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string newFileName = $"User_{URID}{fileInfo.Extension}";

                        //path組合新檔案名稱
                        string filePath = Path.Combine(path, newFileName);


                        //FileStream()的參數需要路徑與名稱才能用生效
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            //使用copy to的方法改檔名並複製到資料夾
                            photo.CopyTo(stream);
                        }
                        //處理要傳到業者概述圖資料表的路徑字串，處理成像是:/img/ShopOverviewPhoto/User_URID.jpg
                        path = $"/img/ShopOverviewPhoto/" + newFileName;
                        return path;
                }
                return "上傳圖片檔案類型僅限jpg或png兩種格式之一";
            }
            return "請上傳圖片檔案喔!";
        }

        //業者概述圖修改
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string sellerOverviewPhotoReplace(IFormFile photo, string oldPhotoPath)
        {
            if (photo != null)
            {
                FileInfo fileInfo = new FileInfo(photo.FileName);

                if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".png")
                {
                    string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string oldPhotoFullPath = rootPath + oldPhotoPath;

                    //FileStream()的參數需要路徑與檔案名稱才能用生效
                    using (var stream = new FileStream(oldPhotoFullPath, FileMode.Create))
                    {
                        //使用copy to的方法改檔名並複製到資料夾
                        photo.CopyTo(stream);
                    }
                    return "上傳圖片成功";
                }
                return "上傳圖片檔案類型僅限jpg或png兩種格式之一";
            }
            return "請上傳圖片檔案喔!";
        }

    }
}
