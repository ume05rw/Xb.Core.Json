using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextXb.Core.Type
{
    /// <summary>
    /// Xb.Type.JsonTest
    /// </summary>
    [TestClass]
    public class JsonTest
    {
        public class Simple
        {
            public string encode { get; set; }
            public string teststring { get; set; }
        }

        public class Nest
        {
            public string encode { get; set; }
            public string teststring { get; set; }
            public Dictionary<string, object> add { get; set; }
        }

        [TestMethod]
        public void StringifyTest()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("encode", "utf8");
            dic.Add("teststring", "日本語やで");
            var json = "{\"encode\":\"utf8\",\"teststring\":\"\u65e5\u672c\u8a9e\u3084\u3067\"}";

            Assert.AreEqual(json, Xb.Type.Json.Stringify(dic));

            var subDic = new Dictionary<string, object>();
            subDic.Add("key1", 1);
            subDic.Add("key2", true);
            subDic.Add("key3", "string");
            dic.Add("add", subDic);

            json = "{\"encode\":\"utf8\",\"teststring\":\"\u65e5\u672c\u8a9e\u3084\u3067\",\"add\":{\"key1\":1,\"key2\":true,\"key3\":\"string\"}}";
            Assert.AreEqual(json, Xb.Type.Json.Stringify(dic));
        }

        [TestMethod]
        public void ParseTest()
        {
            var json = "{\"encode\":\"utf8\",\"teststring\":\"日本語やで\"}";
            var dic = Xb.Type.Json.Parse(json);

            Assert.IsTrue(dic.ContainsKey("encode"));
            Assert.AreEqual("utf8", dic["encode"]);
            Assert.IsTrue(dic.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", dic["teststring"]);

            json = "{\"encode\":\"utf8\",\"teststring\":\"\u65e5\u672c\u8a9e\u3084\u3067\"}";
            dic = Xb.Type.Json.Parse(json);
            Assert.IsTrue(dic.ContainsKey("encode"));
            Assert.AreEqual("utf8", dic["encode"]);
            Assert.IsTrue(dic.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", dic["teststring"]);

            json = "{\"encode\":\"utf8\",\"teststring\":\"\u65e5\u672c\u8a9e\u3084\u3067\",\"add\":{\"key1\":1,\"key2\":true,\"key3\":\"string\"}}";
            dic = Xb.Type.Json.Parse(json);
            Assert.IsTrue(dic.ContainsKey("encode"));
            Assert.AreEqual("utf8", dic["encode"]);
            Assert.IsTrue(dic.ContainsKey("teststring"));
            Assert.AreEqual("日本語やで", dic["teststring"]);
            Assert.IsTrue(dic.ContainsKey("add"));
        }

        [TestMethod]
        public void ParseTTest()
        {
            var json = "{\"encode\":\"utf8\",\"teststring\":\"日本語やで\"}";
            var simple = Xb.Type.Json.Parse<Simple>(json);

            Assert.AreEqual("utf8", simple.encode);
            Assert.AreEqual("日本語やで", simple.teststring);

            json = "{\"encode\":\"utf8\",\"teststring\":\"\u65e5\u672c\u8a9e\u3084\u3067\"}";
            simple = Xb.Type.Json.Parse<Simple>(json);

            Assert.AreEqual("utf8", simple.encode);
            Assert.AreEqual("日本語やで", simple.teststring);

            json = "{\"encode\":\"utf8\",\"teststring\":\"\u65e5\u672c\u8a9e\u3084\u3067\",\"add\":{\"key1\":1,\"key2\":true,\"key3\":\"string\"}}";
            var nest = Xb.Type.Json.Parse<Nest>(json);
            Assert.AreEqual("utf8", nest.encode);
            Assert.AreEqual("日本語やで", nest.teststring);
            Assert.AreEqual((Int64)1, nest.add["key1"]);
            Assert.AreEqual(true, nest.add["key2"]);
            Assert.AreEqual("string", nest.add["key3"]);

        }
    }
}
