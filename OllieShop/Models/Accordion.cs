using System.Text;

namespace OllieShop.Models
{
    public class Accordion
    {
        //function用途:輸入標題與內容,能生成一組可折疊內容的區塊
        //如何在View上顯示運作function後的結果?
        //Step.1 在Controller上new 一個Accordion物件，在把物件的記憶體位置存入變數
        //Step.2 透過建立好的Accordion物件呼叫方法成員accordionGenerator,記得要輸入參數 ex:accordion.accordionGenerator("標題","內容",數字),數字參數表示第幾組,id不能重複
        //Step.3 把回傳值輸出給View(View只接收結果，可以用ViewData或陣列儲存結果在View使用foreach讀陣列也是可以的)
        //Step.4 在View上render內容到網頁上時，請使用@Html.Raw()包覆ViewData才能正常顯示
        public string accordionGenerator(string title, string content, int id)
        {
            return this.accordionGenerator(title, content, id, ""); ;
        }

        //多載,適合一個頁面呼叫多個物件呼叫此函數時,id會發生重複的問題
        public string accordionGenerator(string title, string content, int id,string idTag)
        {
            string result = "";
            result =
            "<div class='accordion'>" +
                "<div class='accordion-item'>" +
                    "<h2 class='accordion-header'>" +
                        $"<button class='accordion-button' type='button' data-bs-toggle='collapse' data-bs-target='#collapse{idTag + id}' aria-expanded='true' aria-controls='collapse{idTag + id}'>" +
                        $"{title}" +
                        "</button>" +
                    "</h2>" +
                    $"<div class='accordion-collapse collapse' id='collapse{idTag + id}' aria-labelledby='heading{idTag + id}' data-bs-parent='#accordionExample' style=''>" +
                        "<div class='accordion-body'>" +
                            $"<strong>{content}</strong>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
            "</div>"
            ;

            return result;

        }


    }
}
