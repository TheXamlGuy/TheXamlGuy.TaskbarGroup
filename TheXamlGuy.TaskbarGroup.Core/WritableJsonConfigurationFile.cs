using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace TheXamlGuy.TaskbarGroup.Core
{
    internal class WritableJsonConfigurationFile
    {
        private readonly Dictionary<string, (JsonValueKind, string)> _data = new(StringComparer.OrdinalIgnoreCase);
        private readonly Stack<string> _paths = new();
        private JObject _tokenCache = new();

        public IDictionary<string, string> Parse(Stream input)
        {
            return ParseStream(input);
        }

        private object ConvertValue(JsonValueKind kind, string value)
        {
            if (kind is JsonValueKind.True or JsonValueKind.False)
            {
                return bool.Parse(value);
            }

            return value;
        }

        public void Write(string key, string value, Stream output)
        {
            var tokenPath = $"$.{key.Replace(":", ".")}";

            var token = _tokenCache.SelectToken(tokenPath);
            if (token is not null)
            {
                var (kind, _) = _data[key];

                var newValue = ConvertValue(kind, value);
                _data[key] = new(kind, value);

                token.Replace(JToken.FromObject(newValue));

                using StreamWriter streamWriter = new(output);
                using JsonTextWriter writer = new(streamWriter) { Formatting = Formatting.Indented };
                _tokenCache.WriteTo(writer);
                output.SetLength(output.Position);
            }
        }

        private void EnterContext(string context)
        {
            _paths.Push(_paths.Count > 0 ? _paths.Peek() + ConfigurationPath.KeyDelimiter + context : context);
        }

        private void ExitContext()
        {
            _paths.Pop();
        }

        private IDictionary<string, string> ParseStream(Stream input)
        {
            _data.Clear();

            var jsonDocumentOptions = new JsonDocumentOptions
            {
                CommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            };

            using (var reader = new StreamReader(input))
            {
                var content = reader.ReadToEnd();
                _tokenCache = JObject.Parse(content);

                using var doc = JsonDocument.Parse(content, jsonDocumentOptions);
                VisitElement(doc.RootElement);
            }

            return _data.ToDictionary(k => k.Key, v => v.Value.Item2.ToString());
        }

        private void VisitElement(JsonElement element)
        {
            var isEmpty = true;

            foreach (JsonProperty property in element.EnumerateObject())
            {
                isEmpty = false;

                EnterContext(property.Name);
                VisitValue(property.Value);
                ExitContext();
            }

            if (isEmpty && _paths.Count > 0)
            {
                _data[_paths.Peek()] = (JsonValueKind.Null, null);
            }
        }

        private void VisitValue(JsonElement value)
        {
            switch (value.ValueKind)
            {
                case JsonValueKind.Object:
                    VisitElement(value);
                    break;

                case JsonValueKind.Array:
                    int index = 0;
                    foreach (JsonElement arrayElement in value.EnumerateArray())
                    {
                        EnterContext(index.ToString());
                        VisitValue(arrayElement);
                        ExitContext();
                        index++;
                    }
                    break;

                case JsonValueKind.Number:
                case JsonValueKind.String:
                case JsonValueKind.True:
                case JsonValueKind.False:
                case JsonValueKind.Null:
                    string key = _paths.Peek();
                    if (_data.ContainsKey(key))
                    {
                        throw new FormatException();
                    }
                    _data[key] = new(value.ValueKind, value.ToString());
                    break;

                default:
                    throw new FormatException();
            }
        }
    }
}
