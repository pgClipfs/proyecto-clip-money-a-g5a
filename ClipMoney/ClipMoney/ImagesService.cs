using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipMoney.Services
{
    public static class ImagesService
    {
        public static List<string> StoreImage(HttpRequest httpRequest)
        {
            List<string> images = new List<string>();
            if (httpRequest.Files.Count > 0)
            {

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    images.Add(filePath);
                }

            }


            return images;
        }
    }
}