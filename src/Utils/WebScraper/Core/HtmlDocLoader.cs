using System.Net;
using System.Text;
using ApplManga.Utils.AppLogger.Core.Logger;
using HtmlAgilityPack;

namespace ApplManga.Utils.WebScraper.Core {
    public class HtmlDocLoader : IHtmlDocLoader {
        private WebRequest CreateRequest(string url) {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = 5000;
            req.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";
            return req;
        }

        public HtmlDocument LoadDocument(string url) {
            var htmlDoc = new HtmlDocument();

            try {
                using (var responseStream = CreateRequest(url).GetResponse().GetResponseStream()) {
                    htmlDoc.Load(responseStream, Encoding.UTF8);
                }
            } catch (WebException webEx) {
                AppLogHelper.Log(AppLoggerBase.LogTarget.File, "Error connecting to specified URL: " + webEx.Message + ", retrying...");
            }

            return htmlDoc;
        }
    }
}
