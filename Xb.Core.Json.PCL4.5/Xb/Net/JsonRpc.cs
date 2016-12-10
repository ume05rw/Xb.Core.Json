using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Xb.Net
{
    /// <summary>
    /// JSON-RPC2.0通信用各種変換処理関数群
    /// 
    /// TODO: .NetFW2.0ベースのため、Newtonsoft.Json.dllを使用する。
    /// TODO: .NetFW4.0にはJSONを扱うライブラリがネイティブに存在するため、移行すること。
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class JsonRpc
    {
        /// <summary>
        /// エラーコード定義
        /// </summary>
        /// <remarks></remarks>
        public enum ErrorCode
        {
            /// <summary>
            /// 解析エラー
            /// </summary>
            /// <remarks></remarks>
            ParseError = -32700,

            /// <summary>
            /// 不正リクエスト
            /// </summary>
            /// <remarks></remarks>
            InvalidRequest = -32600,

            /// <summary>
            /// 指定メソッド不明
            /// </summary>
            /// <remarks></remarks>
            MethodNotFound = -32601,

            /// <summary>
            /// パラメータ不正
            /// </summary>
            /// <remarks></remarks>
            InvalidParams = -32602,

            /// <summary>
            /// 内部エラー
            /// </summary>
            /// <remarks></remarks>
            InternalError = -32603,

            /// <summary>
            /// サーバ定義エラー範囲開始値
            /// </summary>
            /// <remarks></remarks>
            ServerErrorStart = -32099,

            /// <summary>
            /// サーバ定義エラー範囲終了値
            /// </summary>
            /// <remarks></remarks>
            ServerErrorEnd = -32000
        }


        /// <summary>
        /// JSON-RPC解析結果保持クラス
        /// </summary>
        /// <remarks></remarks>
        public class Response
        {
            public readonly bool IsError;
            public readonly Dictionary<string, object> Error;
            public readonly Dictionary<string, object> Result;

            public Response(Dictionary<string, object> result, Dictionary<string, object> err = null)
            {
                if (err != null)
                {
                    this.IsError = true;
                }
                else
                {
                    this.IsError = false;
                }

                this.Result = result;
                this.Error = err;
            }
        }

        /// <summary>
        /// リクエスト渡し値を元にJSON文字列を生成する。
        /// </summary>
        /// <param name="method"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string BuildRequest(string method, Dictionary<string, object> @params)
        {
            Dictionary<string, object> objResult = null;

            if (method == null | string.IsNullOrEmpty(method))
                return null;

            objResult = new Dictionary<string, object>();
            objResult.Add("id", (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
            objResult.Add("jsonrpc", 2.0);
            objResult.Add("method", method);

            if (@params != null)
            {
                objResult.Add("params", @params);
            }

            return JsonConvert.SerializeObject(objResult, Formatting.Indented);
        }


        /// <summary>
        /// リクエスト渡し値を元にJSON文字列を生成する。
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string BuildRequest(string method)
        {
            Dictionary<string, object> @params = new Dictionary<string, object>();
            return BuildRequest(method, @params);
        }


        /// <summary>
        /// 応答データオブジェクトの土台を作る。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Dictionary<string, object> BuildResult(ref Dictionary<string, object> request)
        {
            var result = new Dictionary<string, object>();
            string id = "null";

            if ((request != null) && request.ContainsKey("id"))
            {
                id = request["id"].ToString();
            }
            result.Add("id", id);
            result.Add("jsonrpc", "2.0");

            return result;
        }


        /// <summary>
        /// 文字列からJSON-RPCリクエスト値を取得する。
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Response GetRequest(string jsonString)
        {
            Dictionary<string, object> objAll = null;

            try
            {
                objAll = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
            }
            catch (Exception)
            {
                return new Response(null, BuildError((int)ErrorCode.ParseError, jsonString));
            }

            //id, jsonrpc項目が存在しないとき、フォーマットエラー

            if ((!objAll.ContainsKey("id") | !objAll.ContainsKey("jsonrpc") | !objAll.ContainsKey("method")))
            {
                return new Response(null, BuildError((int)ErrorCode.ParseError, jsonString));
            }

            return new Response(objAll);
        }


        /// <summary>
        /// 文字列からJSON-RPC応答値を取得する。
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Response GetResult(string jsonString)
        {
            Dictionary<string, object> objAll = null;

            try
            {
                objAll = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
            }
            catch (Exception)
            {
                return new Response(null, BuildError((int)ErrorCode.ParseError, jsonString));
            }

            //id, jsonrpc項目が存在しないとき、フォーマットエラー

            if ((!objAll.ContainsKey("id") | !objAll.ContainsKey("jsonrpc")))
            {
                return new Response(null, BuildError((int)ErrorCode.ParseError, jsonString));
            }

            //resultが存在しない、もしくは error が存在するとき、エラー応答と見做す。
            if ((!objAll.ContainsKey("result") | objAll.ContainsKey("error")))
            {
                int code = (int)ErrorCode.ParseError;
                int tmp = 0;
                object data = null;
                string message = "";

                if ((objAll.ContainsKey("code") & int.TryParse(objAll["code"].ToString(), out tmp)))
                {
                    code = tmp;
                }

                if ((objAll.ContainsKey("data")))
                {
                    data = objAll["data"];
                }

                if ((objAll.ContainsKey("message")))
                {
                    message = objAll["message"].ToString();
                }
                return new Response(null, BuildError(code, data, message));
            }

            return new Response((Dictionary<string, object>)objAll["result"]);
        }


        /// <summary>
        /// エラー応答オブジェクトを生成する。
        /// </summary>
        /// <param name="code"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Dictionary<string, object> BuildError(int code, object data, string message = "")
        {
            Dictionary<string, object> objResult = null;
            objResult = new Dictionary<string, object>();

            switch (code)
            {
                case -32700:
                    message = "Parse error";
                    break;
                case -32600:
                    message = "Invalid Request";
                    break;
                case -32601:
                    message = "Method not found";
                    break;
                case 32602:
                    message = "Invalid params";
                    break;
                case -32603:
                    message = "Internal error";
                    break;
                default:
                    if ((-32099 <= code & code <= -32000))
                    {
                        message = "Server error";
                    }
                    else if ((string.IsNullOrEmpty(message)))
                    {
                        message = "Other error";
                    }
                    break;
            }

            objResult.Add("code", code);
            objResult.Add("message", message);

            if (((data != null)))
            {
                objResult.Add("data", data);
            }

            return objResult;
        }
    }
}
