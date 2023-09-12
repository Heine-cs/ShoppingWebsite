//上傳圖片function使用指南，你需要準備好圖片表單與上傳的input元素，主要透過function sign event listener來執行照片預覽上傳的操作，此函數需要一個SellerID傳入，務必在啟用function的頁面加入參數
//例子如下:
//<div class="col-4">
//    <label asp-for="Picture" class="control-label"></label>
//    <form class="d-flex justify-content-between" asp-controller="FileUpload" asp-action="photoUpload" enctype="multipart/form-data" id="pictureForm">
//        <input type="file" name="photo" id="photoInput" />
//        <input class="btn btn-outline-dark" type="submit" value="上傳圖片" />
//    </form>
//    <span class="text-danger d-none" id="pictureValidation">請上傳圖片一張</span>
//    <img id="afterUploadShow" class="w-100 my-2" />
//</div>
//暫時擱置，需要考慮的點太多，先以開發為主
//function photoUpload() {
//    //預覽上傳圖片
//    $('#photoInput').change(function () {
//        let file = this.files[0];
//        let reader = new FileReader();

//        reader.onload = event => {
//            $("#afterUploadShow").attr("src", event.target.result);
//        };
//        reader.readAsDataURL(file);
//    });

//    // 使用jQuery的方式綁定Form的submit事件
//    $("#pictureForm").submit(function (e) {
//        e.preventDefault(); // 阻止Form的默認提交行為
//        if (photoInput.value != "") {
//            // 使用FormData來構建請求數據，包括文字數據和檔案
//            var formData = new FormData(this);
//            // 使用AJAX向Controller的Action發送POST請求
//            $.ajax({
//                url: "@Url.Action("photoUpload", "FileUpload",new {SRID=@sellerFullInfo.SRID})",
//                type: "POST",
//                data: formData, // 把Form中的數據序列化作為請求參數
//                processData: false, // 不要處理data
//                contentType: false, // 不要設置contentType，因為FormData已經處理了
//                success: function (response) {
//                    // 成功接收到返回的字串後，將結果傳到specform需要的欄位與顯示上傳後圖片畫面
//                    if (response != "上傳圖片檔案類型僅限jpg或png兩種格式之一") {
//                        $("#pictureAddressInput").val(response);
//                        //$("#afterUploadShow").attr("src", response);
//                        $('#pictureValidation').addClass("d-none");
//                    }
//                    else {
//                        alert(response);
//                    }
//                },
//                error: function (error) {
//                    console.log(error);
//                }
//            });
//        }
//        else {
//            alert("請先選擇圖片後再點上傳");
//        }
//    });
//}

function RedirectToProductPage(PTID) {
    //ProductPageAction get
    window.open("/Home/ProductPage?PTID=" + PTID);
}