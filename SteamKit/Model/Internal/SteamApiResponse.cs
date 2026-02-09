using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ProtoBuf;
using SteamKit.Builder;

namespace SteamKit.Model.Internal
{
    internal class SteamApiResponse<T> : WebResponse, ISteamApiResponse<T>
    {
        public static SteamApiResponse<T> JsonResponse(HttpResponseMessage response, params JsonConverter[] converters) => new SteamApiResponse<T>(response, converters);

        public static SteamApiResponse<T> ProtobufResponse(HttpResponseMessage response) => new SteamApiResponse<T>(response, Serializer.Deserialize<T>);

        public SteamApiResponse(HttpResponseMessage response, params JsonConverter[] converters) : this(response, CreateJsonBody(converters))
        {

        }

        public SteamApiResponse(HttpResponseMessage response, Func<Stream, T?> streamDeserialize) : this(response, CreateStreamBody(streamDeserialize))
        {

        }

        private SteamApiResponse(HttpResponseMessage response, Func<Stream, string, T?> createBody) : base(response)
        {
            ResultCode = ErrorCodes.Invalid;
            if (response.Headers.TryGetValues("x-eresult", out var result) && int.TryParse(result.First(), out var resultInt))
            {
                ResultCode = (ErrorCodes)resultInt;
            }

            using (Stream stream = new MemoryStream())
            {
                Content.CopyTo(stream);
                Content.Seek(0, SeekOrigin.Begin);

                byte[] buffer = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.ReadExactly(buffer, 0, buffer.Length);
                MediaTypeHeaderValue streamContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                Response = response.Content.Headers.ContentType switch
                {
                    MediaTypeHeaderValue contentType when streamContentType.MediaType!.Equals(contentType.MediaType, StringComparison.CurrentCultureIgnoreCase) => System.Convert.ToBase64String(buffer),
                    _ => Encoding.UTF8.GetString(buffer)
                };

                if (MediaTypeHeaderValue.Parse("text/html").MediaType!.Equals(response.Content.Headers.ContentType?.MediaType, StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }

                stream.Seek(0, SeekOrigin.Begin);
                try
                {
                    Body = createBody.Invoke(stream, Response);
                }
                catch (Exception ex)
                {
                    var logger = LoggerBuilder.GetLogger();
                    logger?.LogException(ex, "SteamApiResponse Deserialize Body Failed");
                    logger?.LogDebug("SteamApiResponse Deserialize Body({0}) Failed, Response Body:{\n}{1}", typeof(T), Response);

                    throw;
                }
            }
        }

        private SteamApiResponse(ErrorCodes errorCode, string response, T? data)
        {
            ResultCode = errorCode;
            Response = response;
            Body = data;
        }

        /// <summary>
        /// 创建一个新对象
        /// </summary>
        /// <typeparam name="Out"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        public SteamApiResponse<Out> Convert<Out>(Func<T?, Out?> factory)
        {
            return new SteamApiResponse<Out>(ResultCode, Response, factory(Body))
            {
                RequestUri = RequestUri,
                HttpStatusCode = HttpStatusCode,
                Headers = Headers,
                Cookies = Cookies,
                Content = Content,
                Message = Message,
            };
        }

        private static Func<Stream, string, T?> CreateJsonBody(params JsonConverter[] converters)
        {
            return (stream, json) =>
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    return default;
                }

                if (typeof(string).Equals(typeof(T)))
                {
                    return (T)(object)json;
                }

                return JsonConvert.DeserializeObject<T>(json, converters);
            };
        }

        private static Func<Stream, string, T?> CreateStreamBody(Func<Stream, T?> streamDeserialize)
        {
            return (stream, base64) => streamDeserialize.Invoke(stream);
        }

        public ErrorCodes ResultCode { get; private set; }

        public T? Body { get; private set; }

        public string Response { get; private set; }

        public string? Message { get; private set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"HttpStatusCode:{HttpStatusCode}");
            stringBuilder.AppendLine($"EResult:{ResultCode}");
            return stringBuilder.ToString();
        }
    }
}
