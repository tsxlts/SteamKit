
using System.Diagnostics.CodeAnalysis;
using System.Text;
using SteamKit.Internal;

namespace SteamKit
{
    /// <summary>
    /// Cookie
    /// </summary>
    public class Cookie
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Cookie()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Cookie(string name, string? value) : this(name, value, null, null)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="domain"></param>
        /// <param name="path"></param>
        public Cookie(string name, string? value, string? domain, string? path)
        {
            Name = name;
            Value = value;
            Domain = domain;
            Path = path;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 值
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// 作用域
        /// </summary>
        public string? Domain { get; set; }

        /// <summary>
        /// 作用路径
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime Expires { get; set; } = DateTime.MinValue;

        /// <summary>
        /// 是否已过期
        /// </summary>
        public bool Expired
        {
            get
            {
                return (Expires != DateTime.MinValue) && (Expires.ToLocalTime() <= DateTime.Now);
            }
        }

        /// <summary>
        /// 拷贝Cookie
        /// </summary>
        /// <returns></returns>
        public Cookie Copy()
        {
            return new Cookie()
            {
                Name = Name,
                Value = Value,
                Domain = Domain,
                Expires = Expires
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Name).Append('=').Append(Value).Append(HttpUtils.CookieSplit);
            if (!string.IsNullOrWhiteSpace(Domain))
            {
                builder.Append("$Doamin").Append('=').Append(Domain).Append(HttpUtils.CookieSplit);
            }
            if (!string.IsNullOrWhiteSpace(Path))
            {
                builder.Append("$Path").Append('=').Append(Path).Append(HttpUtils.CookieSplit);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparand"></param>
        /// <returns></returns>
        public override bool Equals([NotNullWhen(true)] object? comparand)
        {
            Cookie? other = comparand as Cookie;

            return other != null
                    && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase)
                    && CookieComparer.EqualDomains(Domain ?? "", other.Domain ?? "")
                    && CookieComparer.EqualPath(Path ?? "", other.Path ?? "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return $"{Name}={Value};{Domain}".GetHashCode();
        }
    }
}
