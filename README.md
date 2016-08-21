Xb.Core.Json
====

Xamarin Ready, Json Library(PCL) for .Net Framework 4.5 or later.

## Description
It's Simple Lybrary for Json. Http-Query on JSON, Stringify, and Parse.

## Requirement
[Xb.Core](https://github.com/ume05rw/Xb.Core), 
[Json.NET](http://www.newtonsoft.com/json)

## Usage
1. Add reference [Xb.Core.dll](https://github.com/ume05rw/Xb.Core.Json/blob/master/binary/Xb.Core.dll), [Xb.Json.dll](https://github.com/ume05rw/Xb.Core.Json/blob/master/binary/Xb.Json.dll), and [Newtonsoft.Json.dll](https://github.com/ume05rw/Xb.Core.Json/blob/master/binary/Newtonsoft.Json.dll) to your project.
2. Create Instance Xb.Net.HttpJson, or Call Static Methods Xb.Any()

Namespace and Methods are...

    ・Xb.Net
          |
          +- HttpJson(Instance)
          |   |
          |   +- .Constructor(string url,
          |   |               Dictionary<string, object> passingValues = null,
          |   |               Xb.Net.Http.MethodType method = Xb.Net.Http.MethodType.Post,
          |   |               Dictionary<HttpRequestHeader, string> headers = null)
          |   |   Create Xb.Net.HttpJson Instance
          |   |
          |   +- .GetResponseAsync()
          |   |   Get WebResponse and Stream by url
          |   |
          |   +- .GetAsync<T>()
          |       Get Response from url, and Cast response to <T>
          |
          +- HttpJson(Static)
              |
              +- .GetParamString(Dictionary<string, object> values)
                  Convert Associative-Array to Http-Parameter-String
    
    ・Xb.Type
          |
          +- Json
              |
              +- .Stringify(object value, bool isIndented = false)
              |   Convert object to JSON-String
              |
              +- .Parse(string text)
              |   Parse JSON-String to Dictionary
              |
              +- .Parse<T>(string text)
                  Parse JSON-String to any


## Contribution
1. Fork it ( https://github.com/ume05rw/Xb.Core.Json/fork )
2. Create your feature branch (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Add some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create new Pull Request


## Licence

[MIT Licence](https://github.com/ume05rw/Xb.Core.Json/blob/master/LICENSE)

## Author

[Do-Be's](http://dobes.jp)
