using System.Collections;
using SteamKit.Internal;

namespace SteamKit
{
    /// <summary>
    /// CookieCollection
    /// </summary>
    public class CookieCollection : ICollection<Cookie>
    {
        private readonly ArrayList list = new ArrayList();

        /// <summary>
        /// 
        /// </summary>
        public CookieCollection()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseCookies"></param>
        public CookieCollection(CookieCollection baseCookies) : base()
        {
            Add(baseCookies);
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Cookie数量
        /// </summary>
        public int Count => list.Count;

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="cookie"></param>
        public void Add(Cookie cookie)
        {
            ArgumentNullException.ThrowIfNull(cookie);

            int idx = IndexOf(cookie);
            if (idx == -1)
            {
                list.Add(cookie);
            }
            else
            {
                list[idx] = cookie;
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="cookies"></param>
        public void Add(CookieCollection cookies)
        {
            ArgumentNullException.ThrowIfNull(cookies);

            foreach (Cookie? cookie in cookies.list)
            {
                Add(cookie!);
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public bool Remove(Cookie cookie)
        {
            int idx = IndexOf(cookie);
            if (idx == -1)
            {
                return false;
            }
            list.RemoveAt(idx);
            return true;
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public bool Contains(Cookie cookie)
        {
            return IndexOf(cookie) >= 0;
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            ((ICollection)list).CopyTo(array, index);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Cookie[] array, int index)
        {
            list.CopyTo(array, index);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Cookie this[int index]
        {
            get
            {
                if (index < 0 || index >= list.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return (list[index] as Cookie)!;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Cookie? this[string name]
        {
            get
            {
                foreach (Cookie? c in list)
                {
                    if (string.Equals(c!.Name, name, StringComparison.OrdinalIgnoreCase))
                    {
                        return c;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 获取指定域Cookie
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<Cookie> GetCookies(string domain, string path)
        {
            List<Cookie> cookies = new List<Cookie>();
            var source = list.OfType<Cookie>()
                .OrderBy(c => c.Domain?.Length ?? 0)
                .ThenBy(c => c.Domain, StringComparer.CurrentCultureIgnoreCase)
                .ThenBy(c => c.Path?.Length ?? 0)
                .ThenBy(c => c.Path, StringComparer.CurrentCultureIgnoreCase);
            foreach (Cookie cookie in source)
            {
                if (!string.IsNullOrWhiteSpace(cookie.Domain) && !CookieComparer.ContainsDomains(cookie.Domain, domain))
                {
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(cookie.Path) && !CookieComparer.ContainsPath(cookie.Path, path))
                {
                    continue;
                }

                if (cookies.Any(c => c.Name.Equals(cookie.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    cookies.RemoveAll(c => c.Name.Equals(cookie.Name, StringComparison.OrdinalIgnoreCase));
                }

                cookies.Add(cookie);
            }
            return cookies;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Cookie> GetEnumerator()
        {
            foreach (Cookie? cookie in list)
            {
                yield return cookie!;
            }
        }

        /// <summary>
        /// 设置作用域
        /// </summary>
        /// <param name="domain">作用域</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public CookieCollection SetDoamin(string? domain, string? path)
        {
            foreach (Cookie? cookie in list)
            {
                cookie!.Domain = domain;
                cookie!.Path = path;
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        internal int IndexOf(Cookie cookie)
        {
            int idx = 0;
            foreach (Cookie? c in list)
            {
                if (CookieComparer.Equals(cookie, c!))
                {
                    return idx;
                }
                ++idx;
            }
            return -1;
        }
    }
}
