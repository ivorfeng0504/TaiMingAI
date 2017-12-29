using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace TaiMingAI.Tools
{
    public class WebApiHelper
    {
        /// <summary>
        /// 调用WebAPI的get方法
        /// </summary>
        /// <param name="uri">地址</param>
        /// <returns>Json数据</returns>
        public static string GetResponseJson(string url)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseJson = response.Content.ReadAsStringAsync().Result;
                return responseJson;
            }
            else
            {
                string sError = response.Content.ReadAsStringAsync().Result;
                throw new Exception(sError);
            }
        }

        /// <summary>
        /// 调用WebAPI的get方法
        /// </summary>
        /// <param name="uri">地址</param>
        /// <returns>实体对象T</returns>
        public static T GetResponseJson<T>(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseStr = response.Content.ReadAsStringAsync().Result;
                    T t = JsonHelper.FromJson<T>(responseStr);
                    return t;
                }
                else
                {
                    string sError = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(sError);
                }
            }
        }

        /// <summary>
        /// 调用WebAPI的get方法
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>返回文件流</returns>
        public static byte[] GetResponseStream(string uri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;
                    return bytes;
                }
                else
                {
                    string sError = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(sError);
                }
            }
        }

        /// <summary>
        /// 将json数据Post到指定Uri
        /// </summary>
        /// <param name="uri">链接</param>
        /// <param name="json">数据</param>
        /// <returns>json字符串</returns>
        public static string PostResponseJson(string uri, string postJson)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(postJson);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = httpClient.PostAsync(uri, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseJson = response.Content.ReadAsStringAsync().Result;
                    return responseJson;
                }
                else
                {
                    string sError = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(sError);
                }
            }
        }

        #region  TrackID
        /// <summary>
        /// 将json数据Post到指定Uri 带TrackId
        /// </summary>
        /// <param name="uri">链接</param>
        /// <param name="json">数据</param>
        /// <returns></returns>
        public static string PostResponseJsonTrackId(string uri, string postJson)
        {
            var Base = new HttpContextWrapper(HttpContext.Current) as HttpContextBase;
            var _context = Base.Request.RequestContext.HttpContext;
            var hash = _context.GetHashCode();
            var apiId = HttpContext.Current.Session[(hash + 1).ToString()];
            uri = UpdateUrl(uri, apiId == null ? "11111111-1111-1111-1111-111111111111" : apiId.ToString());
            return PostResponseJson(uri, postJson);
        }

        public static string UpdateUrl(string url, string apiId)
        {
            var Base = new HttpContextWrapper(HttpContext.Current) as HttpContextBase;
            var _context = Base.Request.RequestContext.HttpContext;
            var hash = _context.GetHashCode();
            var trackID = HttpContext.Current.Session[hash.ToString()];
            var trackid = trackID == null ? "" : trackID.ToString();
            if (url.Contains("?"))
            {
                url = url + "&trackID=" + trackid + "&AppId=" + apiId;
            }
            else
            {
                url = url + "?trackID=" + trackid + "&AppId=" + apiId;
            }
            return url;
        }

        /// <summary>
        /// 调用WebAPI的get方法 带TrackId
        /// </summary>
        /// <param name="uri">地址</param>
        /// <returns>Json数据</returns>
        /// <Remark>YYX,2017-01-05,2017-01-05</ Remark >
        public static string GetResponseJsonTrackId(string url)
        {
            var Base = new HttpContextWrapper(HttpContext.Current) as HttpContextBase;
            var _context = Base.Request.RequestContext.HttpContext;
            var hash = _context.GetHashCode();
            var apiId = HttpContext.Current.Session[(hash + 1).ToString()];
            url = UpdateUrl(url, apiId == null ? "11111111-1111-1111-1111-111111111111" : apiId.ToString());
            return GetResponseJson(url);
        }

        public static T PostResponseJsonTrackId<T>(string uri, object content)
        {
            var Base = new HttpContextWrapper(HttpContext.Current) as HttpContextBase;
            var _context = Base.Request.RequestContext.HttpContext;
            var hash = _context.GetHashCode();
            var apiId = HttpContext.Current.Session[(hash + 1).ToString()];
            uri = UpdateUrl(uri, apiId == null ? "11111111-1111-1111-1111-111111111111" : apiId.ToString());
            return PostResponseJson<T>(uri, content);
        }

        public static T GetResponseJsonTrackId<T>(string url)
        {
            var Base = new HttpContextWrapper(HttpContext.Current) as HttpContextBase;
            var _context = Base.Request.RequestContext.HttpContext;
            var hash = _context.GetHashCode();
            var apiId = HttpContext.Current.Session[(hash + 1).ToString()];
            url = UpdateUrl(url, apiId == null ? "11111111-1111-1111-1111-111111111111" : apiId.ToString());
            return GetResponseJson<T>(url);
        }
        #endregion


        /// <summary>
        /// 将object数据Post到指定Uri
        /// </summary>
        /// <param name="uri">链接</param>
        /// <param name="content">数据</param>
        /// <returns>实体对象T</returns>
        public static T PostResponseJson<T>(string uri, object content)
        {
            using (var httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(JsonHelper.ToJson(content));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = httpClient.PostAsync(uri, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = response.Content.ReadAsStringAsync().Result;
                    T t = JsonHelper.FromJson<T>(responseStr);
                    return t;
                }
                else
                {
                    string sError = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(sError);
                }
            }
        }

        /// <summary>
        /// 将byte[]数据Post到指定Uri
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="fileByte"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public static string PostResponseFormData(string uri, Dictionary<string, byte[]> fileByte, Dictionary<string, string> formData = null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var httpContent = new MultipartFormDataContent();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
                if (formData != null)
                {
                    foreach (var key in formData.Keys)
                    {
                        var dataContent = new ByteArrayContent(Encoding.UTF8.GetBytes(formData[key]));
                        dataContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            Name = key
                        };
                        httpContent.Add(dataContent);
                    }
                }
                foreach (var key in fileByte.Keys)
                {
                    var fileContent = new ByteArrayContent(fileByte[key]);
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = key
                    };
                    httpContent.Add(fileContent);
                }
                var response = httpClient.PostAsync(uri, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseJson = response.Content.ReadAsStringAsync().Result;
                    return responseJson;
                }
                else
                {
                    string sError = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(sError);
                }
            }
        }

        /// <summary>
        /// 将json数据Post到指定Uri
        /// </summary>
        /// <param name="uri">链接</param>
        /// <param name="json">数据</param>
        /// <returns></returns>
        public static byte[] PostResponseJson2Bytes(string uri, string postJson)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent httpContent = new StringContent(postJson);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = httpClient.PostAsync(uri, httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    byte[] bytes = response.Content.ReadAsByteArrayAsync().Result;
                    return bytes;
                }
                else
                {
                    string sError = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(sError);
                }
            }
        }

        /// <summary>
        /// 将文件流数据Post到指定Uri
        /// </summary>
        /// <param name="uri">链接</param>
        /// <param name="json">数据</param>
        /// <returns></returns>
        public static string PostResponseStream(string uri, string filename, Stream stream)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.MaxResponseContentBufferSize = 256000;
                MultipartFormDataContent mulContent = new MultipartFormDataContent();
                HttpContent fileContent = new StreamContent(stream);

                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(" text/html; charset=utf-8");
                mulContent.Add(fileContent, "myFile", filename);

                var response = httpClient.PostAsync(uri, mulContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseJson = response.Content.ReadAsStringAsync().Result;
                    return responseJson;
                }
                else
                {
                    string sError = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(sError);
                }
            }
        }

        /// <summary>
        /// 带有Cookie的Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postJson"></param>
        /// <param name="cookieStr"></param>
        /// <returns></returns>
        public static string PostForm(string url, string postJson, string cookieStr)
        {
            CookieContainer cookies = new CookieContainer();
            string[] cookieStrs = cookieStr.Split('=');
            Cookie cookie = new Cookie(cookieStrs[0], cookieStrs[1]);
            string domain = Regex.Replace(url, "[a-zA-z]+://", "");
            domain = domain.Split('/')[0];
            domain = domain.Split(':')[0];
            cookie.Domain = domain.Equals("localhost") ? "127.0.0.1" : domain;
            cookies.Add(cookie);
            byte[] postData = Encoding.ASCII.GetBytes(postJson.Substring(0, postJson.Length - 1));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.AllowAutoRedirect = false;
            request.ContentType = "application/x-www-form-urlencoded;charset=gbk";
            request.CookieContainer = cookies;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11";
            request.ContentLength = postData.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postData, 0, postData.Length);
            requestStream.Close();
            return "ok";
        }

        /// <summary>
        /// 带令牌post指定Url
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="ticket">令牌</param>
        /// <returns></returns>
        /// <Remark>yuxuxuan,2016-12-14,2016-12-14</ Remark >
        public static string PostResponsewithToken(string url, string ticket)
        {
            WebRequest request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + ticket);
            request.Method = "POST";
            request.ContentLength = 0;
            WebResponse response = request.GetResponse();
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                var s = stream.ReadToEnd();
                return s;
            }
        }
    }
}
