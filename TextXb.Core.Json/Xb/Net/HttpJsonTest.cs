using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xb.Net;

namespace TextXb.Core.Net
{
    public class HttpResultType
    {
        public string method { get; set; }
        public Dictionary<string, string> headers { get; set; }
        public string body { get; set; }
        public Dictionary<string, string> passing_values { get; set; }
        public string url { get; set; }
        public string input_encode { get; set; }
        public string output_encode { get; set; }
    }

    [TestClass]
    public class HttpJsonTest : TestsXb.Core.TestBase
    {
        [TestMethod()]
        public async Task GetResponseAsyncTestPartGet()
        {
            Xb.Net.HttpJson query;
            Xb.Net.Http.ResponseSet responseSet;
            Dictionary<string, object> param = new Dictionary<string, object>();
            string resText;
            HttpResultType result;
            var headers = new Dictionary<HttpRequestHeader, string>();
            headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            headers.Add(HttpRequestHeader.AcceptLanguage, "ja,en-US;q=0.7,en;q=0.3");
            headers.Add(HttpRequestHeader.Cookie, "_ga=GA1.2.2086932222.1470845468; _gat=1");


            //単純なGET
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", null, Xb.Net.Http.MethodType.Get);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("GET", result.method);
            Assert.AreEqual("", result.body);
            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, UTF-8指定
            param.Clear();
            param.Add("encode", "utf8");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Get, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("GET", result.method);
            Assert.AreEqual("", result.body);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("utf8", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, SJIS指定
            param.Clear();
            param.Add("encode", "sjis");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("shift_jis");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Get, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("GET", result.method);
            Assert.AreEqual("", result.body);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("sjis", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("SJIS", result.output_encode);


            //パラメータ付きGET, EUC-JP指定
            param.Clear();
            param.Add("encode", "eucjp");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("euc-jp");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Get, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("GET", result.method);
            Assert.AreEqual("", result.body);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("eucjp", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("EUC-JP", result.output_encode);
        }

        [TestMethod()]
        public async Task GetResponseAsyncTestPartPost()
        {
            Xb.Net.HttpJson query;
            Xb.Net.Http.ResponseSet responseSet;
            Dictionary<string, object> param = new Dictionary<string, object>();
            string resText;
            HttpResultType result;
            var headers = new Dictionary<HttpRequestHeader, string>();
            headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            headers.Add(HttpRequestHeader.AcceptLanguage, "ja,en-US;q=0.7,en;q=0.3");
            headers.Add(HttpRequestHeader.Cookie, "_ga=GA1.2.2086932222.1470845468; _gat=1");


            //単純なGET
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php");
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("POST", result.method);
            Assert.AreEqual("", result.body);
            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, UTF-8指定
            param.Clear();
            param.Add("encode", "utf8");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Post, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("POST", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("utf8", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("UTF-8", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, SJIS指定
            param.Clear();
            param.Add("encode", "sjis");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("shift_jis");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Post, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("POST", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("sjis", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("SJIS", result.input_encode);
            Assert.AreEqual("SJIS", result.output_encode);


            //パラメータ付きGET, EUC-JP指定
            param.Clear();
            param.Add("encode", "eucjp");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("euc-jp");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Post, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("POST", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("eucjp", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("EUC-JP", result.input_encode);
            Assert.AreEqual("EUC-JP", result.output_encode);
        }

        [TestMethod()]
        public async Task GetResponseAsyncTestPartPut()
        {
            Xb.Net.HttpJson query;
            Xb.Net.Http.ResponseSet responseSet;
            Dictionary<string, object> param = new Dictionary<string, object>();
            string resText;
            HttpResultType result;
            var headers = new Dictionary<HttpRequestHeader, string>();
            headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            headers.Add(HttpRequestHeader.AcceptLanguage, "ja,en-US;q=0.7,en;q=0.3");
            headers.Add(HttpRequestHeader.Cookie, "_ga=GA1.2.2086932222.1470845468; _gat=1");


            //単純なGET
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", null, Xb.Net.Http.MethodType.Put);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("PUT", result.method);
            Assert.AreEqual("", result.body);
            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, UTF-8指定
            param.Clear();
            param.Add("encode", "utf8");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Put, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("PUT", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("utf8", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("UTF-8", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, SJIS指定
            param.Clear();
            param.Add("encode", "sjis");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("shift_jis");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Put, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("PUT", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("sjis", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("SJIS", result.input_encode);
            Assert.AreEqual("SJIS", result.output_encode);


            //パラメータ付きGET, EUC-JP指定
            param.Clear();
            param.Add("encode", "eucjp");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("euc-jp");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Put, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("PUT", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("eucjp", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("EUC-JP", result.input_encode);
            Assert.AreEqual("EUC-JP", result.output_encode);
        }

        [TestMethod()]
        public async Task GetResponseAsyncTestPartDelete()
        {
            Xb.Net.HttpJson query;
            Xb.Net.Http.ResponseSet responseSet;
            Dictionary<string, object> param = new Dictionary<string, object>();
            string resText;
            HttpResultType result;
            var headers = new Dictionary<HttpRequestHeader, string>();
            headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            headers.Add(HttpRequestHeader.AcceptLanguage, "ja,en-US;q=0.7,en;q=0.3");
            headers.Add(HttpRequestHeader.Cookie, "_ga=GA1.2.2086932222.1470845468; _gat=1");


            //単純なGET
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", null, Xb.Net.Http.MethodType.Delete);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("DELETE", result.method);
            Assert.AreEqual("", result.body);
            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, UTF-8指定
            param.Clear();
            param.Add("encode", "utf8");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Delete, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("DELETE", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("utf8", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("UTF-8", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, SJIS指定
            param.Clear();
            param.Add("encode", "sjis");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("shift_jis");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Delete, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("DELETE", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("sjis", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("SJIS", result.input_encode);
            Assert.AreEqual("SJIS", result.output_encode);


            //パラメータ付きGET, EUC-JP指定
            param.Clear();
            param.Add("encode", "eucjp");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("euc-jp");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Delete, headers);
            responseSet = await query.GetResponseAsync();
            resText = Xb.Net.Http.Encode.GetString(Xb.Byte.GetBytes(responseSet.Stream));
            this.Out(resText);
            result = JsonConvert.DeserializeObject<HttpResultType>(resText);

            Assert.AreEqual("DELETE", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("eucjp", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("EUC-JP", result.input_encode);
            Assert.AreEqual("EUC-JP", result.output_encode);
        }


        [TestMethod()]
        public async Task GetAsyncTestPartGet()
        {
            Xb.Net.HttpJson query;
            Dictionary<string, object> param = new Dictionary<string, object>();
            HttpResultType result;
            var headers = new Dictionary<HttpRequestHeader, string>();
            headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            headers.Add(HttpRequestHeader.AcceptLanguage, "ja,en-US;q=0.7,en;q=0.3");
            headers.Add(HttpRequestHeader.Cookie, "_ga=GA1.2.2086932222.1470845468; _gat=1");


            //単純なGET
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", null, Xb.Net.Http.MethodType.Get);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("GET", result.method);
            Assert.AreEqual("", result.body);
            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, UTF-8指定
            param.Clear();
            param.Add("encode", "utf8");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Get, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("GET", result.method);
            Assert.AreEqual("", result.body);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("utf8", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, SJIS指定
            param.Clear();
            param.Add("encode", "sjis");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("shift_jis");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Get, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("GET", result.method);
            Assert.AreEqual("", result.body);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("sjis", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("SJIS", result.output_encode);


            //パラメータ付きGET, EUC-JP指定
            param.Clear();
            param.Add("encode", "eucjp");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("euc-jp");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Get, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("GET", result.method);
            Assert.AreEqual("", result.body);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("eucjp", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("EUC-JP", result.output_encode);
        }

        [TestMethod()]
        public async Task GetAsyncTestPartPost()
        {
            Xb.Net.HttpJson query;
            Dictionary<string, object> param = new Dictionary<string, object>();
            HttpResultType result;
            var headers = new Dictionary<HttpRequestHeader, string>();
            headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            headers.Add(HttpRequestHeader.AcceptLanguage, "ja,en-US;q=0.7,en;q=0.3");
            headers.Add(HttpRequestHeader.Cookie, "_ga=GA1.2.2086932222.1470845468; _gat=1");


            //単純なGET
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php");
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("POST", result.method);
            Assert.AreEqual("", result.body);
            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, UTF-8指定
            param.Clear();
            param.Add("encode", "utf8");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Post, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("POST", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("utf8", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("UTF-8", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, SJIS指定
            param.Clear();
            param.Add("encode", "sjis");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("shift_jis");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Post, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("POST", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("sjis", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("SJIS", result.input_encode);
            Assert.AreEqual("SJIS", result.output_encode);


            //パラメータ付きGET, EUC-JP指定
            param.Clear();
            param.Add("encode", "eucjp");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("euc-jp");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Post, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("POST", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("eucjp", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("EUC-JP", result.input_encode);
            Assert.AreEqual("EUC-JP", result.output_encode);
        }

        [TestMethod()]
        public async Task GetAsyncTestPartPut()
        {
            Xb.Net.HttpJson query;
            Dictionary<string, object> param = new Dictionary<string, object>();
            HttpResultType result;
            var headers = new Dictionary<HttpRequestHeader, string>();
            headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            headers.Add(HttpRequestHeader.AcceptLanguage, "ja,en-US;q=0.7,en;q=0.3");
            headers.Add(HttpRequestHeader.Cookie, "_ga=GA1.2.2086932222.1470845468; _gat=1");


            //単純なGET
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", null, Xb.Net.Http.MethodType.Put);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("PUT", result.method);
            Assert.AreEqual("", result.body);
            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, UTF-8指定
            param.Clear();
            param.Add("encode", "utf8");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Put, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("PUT", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("utf8", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("UTF-8", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, SJIS指定
            param.Clear();
            param.Add("encode", "sjis");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("shift_jis");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Put, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("PUT", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("sjis", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("SJIS", result.input_encode);
            Assert.AreEqual("SJIS", result.output_encode);


            //パラメータ付きGET, EUC-JP指定
            param.Clear();
            param.Add("encode", "eucjp");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("euc-jp");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Put, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("PUT", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("eucjp", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("EUC-JP", result.input_encode);
            Assert.AreEqual("EUC-JP", result.output_encode);
        }

        [TestMethod()]
        public async Task GetAsyncTestPartDelete()
        {
            Xb.Net.HttpJson query;
            Dictionary<string, object> param = new Dictionary<string, object>();
            HttpResultType result;
            var headers = new Dictionary<HttpRequestHeader, string>();
            headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            headers.Add(HttpRequestHeader.AcceptLanguage, "ja,en-US;q=0.7,en;q=0.3");
            headers.Add(HttpRequestHeader.Cookie, "_ga=GA1.2.2086932222.1470845468; _gat=1");


            //単純なGET
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", null, Xb.Net.Http.MethodType.Delete);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("DELETE", result.method);
            Assert.AreEqual("", result.body);
            Assert.AreEqual("ASCII", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, UTF-8指定
            param.Clear();
            param.Add("encode", "utf8");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.UTF8;
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Delete, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("DELETE", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("utf8", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("UTF-8", result.input_encode);
            Assert.AreEqual("UTF-8", result.output_encode);


            //パラメータ付きGET, SJIS指定
            param.Clear();
            param.Add("encode", "sjis");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("shift_jis");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Delete, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("DELETE", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("sjis", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("SJIS", result.input_encode);
            Assert.AreEqual("SJIS", result.output_encode);


            //パラメータ付きGET, EUC-JP指定
            param.Clear();
            param.Add("encode", "eucjp");
            param.Add("teststring", "日本語やで");
            Xb.Net.Http.Encode = System.Text.Encoding.GetEncoding("euc-jp");
            query = new Xb.Net.HttpJson("http://dobes.jp/tests/json.php", param, Xb.Net.Http.MethodType.Delete, headers);
            result = await query.GetAsync<HttpResultType>();

            Assert.AreEqual("DELETE", result.method);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Encoding"));
            Assert.AreEqual("gzip, deflate, br", result.headers["Accept-Encoding"]);
            Assert.IsTrue(result.headers.ContainsKey("Accept-Language"));
            Assert.AreEqual("ja,en-US;q=0.7,en;q=0.3", result.headers["Accept-Language"]);
            Assert.IsTrue(result.headers.ContainsKey("Cookie"));
            Assert.AreEqual("_ga=GA1.2.2086932222.1470845468; _gat=1", result.headers["Cookie"]);

            Assert.IsTrue(result.passing_values.ContainsKey("encode"));
            Assert.AreEqual("eucjp", result.passing_values["encode"]);
            Assert.IsTrue(result.passing_values.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", result.passing_values["teststring"]);

            Assert.AreEqual("EUC-JP", result.input_encode);
            Assert.AreEqual("EUC-JP", result.output_encode);
        }
    }
}
