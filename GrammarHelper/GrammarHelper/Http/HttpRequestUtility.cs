using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
namespace GrammarHelper.Http
{

    public class HttpRequestUtility
    {
        /// <summary>
        ///通用HttpWebRequest
        /// </summary>
        /// <param name="method">请求方式（POST/GET）</param>
        /// <param name="url">请求地址</param>
        /// <param name="param">请求参数</param>
        /// <param name="ContentType">设置 Content-type HTTP 标头的值 例如 application/json</param>
        /// <returns>请求返回的结果</returns>
        public static string Request(string method, string url, string param, string contentType)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("URL");

            switch (method.ToUpper())
            {
                case "POST":
                    return Post(url, param, contentType);
                case "GET":
                    return Get(url, param, contentType);
                default:
                    //Log.Error(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "method:" + method);
                    throw new EntryPointNotFoundException("method:" + method);
            }
        }

        public static string PostData(string url, string paramList, string contentType)
        {
            byte[] requestBytes;
            StreamReader sr;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            requestBytes = Encoding.UTF8.GetBytes(paramList);
            req.Method = "POST";
            req.ContentType = contentType;
            req.ContentLength = requestBytes.Length;
            req.Timeout = 600000;

            Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
            string backstr = sr.ReadToEnd();
            sr.Close();
            res.Close();
            return backstr;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">参数</param>
        /// <param name="contentType">设置 Content-type HTTP 标头的值</param>
        /// <returns>请求返回的结果</returns>
        public static string Post(string url, string param, string contentType, string token = "")
        {
            UrlCheck(ref url);

            byte[] bufferBytes = Encoding.UTF8.GetBytes(param);

            var request = WebRequest.Create(url) as HttpWebRequest;//HttpWebRequest方法继承自WebRequest, Create方法在WebRequest中定义，因此此处要显示的转换
            request.Method = "POST";
            request.ContentLength = bufferBytes.Length;
            request.ContentType = contentType;
            request.Headers.Add(HttpRequestHeader.Authorization, token);
            try
            {
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bufferBytes, 0, bufferBytes.Length);
                }
            }
            catch (WebException ex)
            {
                //Log.Error(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, ex.Message);
                return ex.Message;
            }

            return HttpRequest(request);
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">参数</param>
        /// <param name="contentType">设置 Content-type HTTP 标头的值</param>
        /// <returns>请求返回结果</returns>
        public static string Get(string url, string param, string contentType, string token = "")
        {
            UrlCheck(ref url);

            if (!string.IsNullOrEmpty(param))
                if (!param.StartsWith("?"))
                    param += "?" + param;
                else
                    param += param;

            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = contentType;
            request.Headers.Add(HttpRequestHeader.Authorization, token);

            return HttpRequest(request);
        }

        /// <summary>
        /// 请求的主体部分（由此完成请求并返回请求结果）
        /// </summary>
        /// <param name="request">请求的对象</param>
        /// <param name="contentType">设置 Content-type HTTP 标头的值</param>
        /// <returns>请求返回结果</returns>
        private static string HttpRequest(HttpWebRequest request)
        {
            HttpWebResponse response = null;

            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            if (response == null)
            {
                return null;
            }

            string result = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }
            return result;

        }

        /// <summary>
        /// URL拼写完整性检查
        /// </summary>
        /// <param name="url">待检查的URL</param>
        private static void UrlCheck(ref string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "http://" + url;
        }

    }
}
