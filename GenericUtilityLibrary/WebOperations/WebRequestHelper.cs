using System;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
using System.Web;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Threading;

namespace com.cagdaskorkut.utility {
	public class WebRequestHelper {
        public static string CallPage(string address)
        {
            const int totalTryCount = 5;
            string returnText = "";
            int tryCount = 0;
            while (string.IsNullOrEmpty(returnText) && tryCount < totalTryCount)
            {
                // if this is not the first try, let the thread rest for half a second
                if (tryCount > 0)
                    Thread.Sleep(500);
                try
                {
                    Ping ping = new Ping();
                    PingReply pngrpl = ping.Send("www.google.com");
                    if (pngrpl.Status == IPStatus.Success)
                    {
                        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(address);
                        req.UserAgent = "CrawlWeb";

                        WebResponse response = req.GetResponse();

                        Stream strm = response.GetResponseStream();
                        StreamReader strmrdr = new StreamReader(strm);

                        returnText = HttpUtility.HtmlDecode(strmrdr.ReadToEnd());
                    }
                    else {
                        if (++tryCount == totalTryCount)
                            throw new Exception("Internet connection error.\n\n\n");
                    }
                }
                catch (PingException ex)
                {
                    if (++tryCount == totalTryCount)
                        throw new Exception("Internet connection error.\n\n\n", ex);
                }
            }
            return returnText;
        }

        public static string getQuerystringForChange(NameValueCollection fullQuerystring, string qsParamToIgnore)
        {
            string tempStr = "?";
            for (int i = 0; i < fullQuerystring.Count; i++)
            {
                if (!string.Equals(fullQuerystring.GetKey(i), qsParamToIgnore))
                {
                    if (tempStr != "?")
                        tempStr += "&";
                    if (!string.IsNullOrEmpty(fullQuerystring.GetKey(i)) && !string.IsNullOrEmpty(fullQuerystring[i]))
                        tempStr += fullQuerystring.GetKey(i) + "=" + fullQuerystring[i];
                }
            }
            if (tempStr != "?") tempStr += "&";
            return tempStr;
        }
        public static string getQuerystringForChange(NameValueCollection fullQuerystring, List<string> qsParamToIgnore)
        {
            string tempStr = "?";
            for (int i = 0; i < fullQuerystring.Count; i++)
            {
                if (!qsParamToIgnore.Contains(fullQuerystring.GetKey(i)))
                {
                    if (tempStr != "?")
                        tempStr += "&";
                    if (!string.IsNullOrEmpty(fullQuerystring.GetKey(i)) && !string.IsNullOrEmpty(fullQuerystring[i]))
                        tempStr += fullQuerystring.GetKey(i) + "=" + fullQuerystring[i];
                }
            }
            if (tempStr != "?") tempStr += "&";
            return tempStr;
        }

        public static void Download ( string downloadUrl, string savePath ) {
			try {
				WebClient client = new WebClient ( );
				client.DownloadFile ( downloadUrl, savePath );
			} catch ( Exception ex ) {
				throw new KnownException ( "FileDownloadError", ex );
			}
		}
	}
}