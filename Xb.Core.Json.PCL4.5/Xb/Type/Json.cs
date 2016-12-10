using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Xb.Type
{
    /// <summary>
    /// JSON string functions
    /// JSON文字列関数群
    /// </summary>
    public class Json
    {
        /// <summary>
        /// Convert object to JSON-String
        /// 渡し値オブジェクトをJSON文字列に変換する。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string Stringify(object value, bool isIndented = false)
        {
            var setting = new JsonSerializerSettings();
            var formatting = Formatting.None;

            if (isIndented)
                formatting = Formatting.Indented;

            setting.MaxDepth = Int32.MaxValue;

            return JsonConvert.SerializeObject(value, formatting, setting);
        }


        /// <summary>
        /// Parse JSON-String to Dictionary
        /// 渡し値文字列をJSONと見做し、Dictionary型にパースする。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Dictionary<string, object> Parse(string text)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(text);
        }


        /// <summary>
        /// Parse JSON-String to any
        /// 渡し値文字列をJSONと見做し、指定の型にパースする。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static T Parse<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
