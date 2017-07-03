using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Xml.XPath;

namespace ApplManga.ViewModels {
    public class WebScraper {
        private long _loadAndParseTime;
        public long LoadAndParseTime {
            get { return _loadAndParseTime; }
            set { _loadAndParseTime = value; }
        }

        /// <summary>
        /// Scrapes table cell contents from specified URL and adds them to a List
        /// </summary>
        /// <param name="webUrl"></param>
        /// <param name="tableXPath"></param>
        /// <param name="rowXPath"></param>
        /// <param name="cellXPath"></param>
        /// <returns></returns>
        public List<string> GetCellsFromHTML(
            string webUrl,
            string tableXPath,
            string rowXPath,
            string cellXPath) {
            List<string> htmlCellList = new List<string>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = new HtmlDocument();

            // Check if URL is valid and currently existing
            if(CheckIfPageExists(webUrl) == false) {
                return null;
            } else {
                htmlDoc = web.Load(webUrl);
            }

            // Check XPaths for errors
            if (IsXPathValid(new string[] { tableXPath, rowXPath, cellXPath }) == false) {
                return null;
            }

            if (htmlDoc == null) {
                Console.WriteLine();
                return null;
            } else {
                foreach (HtmlNode table in htmlDoc.DocumentNode.SelectNodes(tableXPath)) {
                    if (table == null) {
                        Console.WriteLine("Specified node not found");
                        break;
                    }

                    foreach (HtmlNode row in table.SelectNodes(rowXPath)) {
                        HtmlNodeCollection cells = row.SelectNodes(cellXPath);

                        if (cells == null) {
                            continue;
                        }

                        foreach (HtmlNode cell in cells) {
                            string formattedName = HtmlEntity.DeEntitize(cell.InnerText);
                            htmlCellList.Add(formattedName);
                            // Console.WriteLine(cell.InnerText);
                        }
                    }
                }

                return htmlCellList;
            }
        }

        /// <summary>
        /// Checks XPath expression/s for errors
        /// </summary>
        /// <param name="xPathString"></param>
        /// <returns></returns>
        private bool IsXPathValid(string[] xPathStrings) {
            string[] xPaths = xPathStrings;

            try {
                foreach(string xPath in xPaths) {
                    XPathExpression.Compile(xPath);
                }
            } catch(XPathException ex) {
                MessageBox.Show("XPath syntax error: " + ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns specified URL's status
        /// </summary>
        /// <param name="webUrl"></param>
        /// <returns></returns>
        /// TODO: Speed up 
        private bool CheckIfPageExists(string webUrl) {
            try {
                var pageRequest = (HttpWebRequest)WebRequest.Create(webUrl);
                pageRequest.Timeout = 5000;
                pageRequest.Method = "HEAD";

                var pageResponse = (HttpWebResponse)pageRequest.GetResponse();
                pageResponse.Close();

                return (pageResponse.StatusCode == HttpStatusCode.OK);
            } catch(WebException ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
