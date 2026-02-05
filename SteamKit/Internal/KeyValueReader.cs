
using System.Text;

namespace SteamKit.Internal
{
    internal class KeyValueReader : StreamReader
    {
        private StringBuilder sb = new(128);

        internal static IDictionary<char, char> escapedMapping = new Dictionary<char, char>
        {
            { '\\', '\\' },
            { 'n', '\n' },
            { 'r', '\r' },
            { 't', '\t' }
        };

        public KeyValueReader(Stream stream) : base(stream)
        {

        }

        public KeyValue ReadKeyValue()
        {
            bool wasQuoted;
            bool wasConditional;

            KeyValue keyValue = new KeyValue();

            KeyValue? currentKey = keyValue;

            do
            {
                var s = ReadToken(out wasQuoted, out wasConditional);

                if (string.IsNullOrEmpty(s))
                {
                    break;
                }

                if (currentKey == null)
                {
                    currentKey = new KeyValue(s);
                }
                else
                {
                    currentKey.Name = s;
                }

                s = ReadToken(out wasQuoted, out wasConditional);

                if (wasConditional)
                {
                    s = ReadToken(out wasQuoted, out wasConditional);
                }

                if (s != null && s.StartsWith('{') && !wasQuoted)
                {
                    currentKey.RecursiveLoadFromBuffer(this);
                }
                else
                {
                    throw new Exception("LoadFromBuffer: missing {");
                }

                currentKey = null;
            }
            while (!EndOfStream);

            return keyValue;
        }

        private void EatWhiteSpace()
        {
            while (!EndOfStream)
            {
                if (!char.IsWhiteSpace((char)Peek()))
                {
                    break;
                }

                Read();
            }
        }

        private bool EatCPPComment()
        {
            if (!EndOfStream)
            {
                char next = (char)Peek();
                if (next == '/')
                {
                    ReadLine();
                    return true;
                    /*
                     *  As came up in parsing the Dota 2 units.txt file, the reference (Valve) implementation
                     *  of the KV format considers a single forward slash to be sufficient to comment out the
                     *  entirety of a line. While they still _tend_ to use two, it's not required, and likely
                     *  is just done out of habit.
                     */
                }

                return false;
            }

            return false;
        }

        public string? ReadToken(out bool wasQuoted, out bool wasConditional)
        {
            wasQuoted = false;
            wasConditional = false;

            while (true)
            {
                EatWhiteSpace();

                if (EndOfStream)
                {
                    return null;
                }

                if (!EatCPPComment())
                {
                    break;
                }
            }

            if (EndOfStream)
                return null;

            char next = (char)Peek();
            if (next == '"')
            {
                wasQuoted = true;

                // "
                Read();

                sb.Clear();
                while (!EndOfStream)
                {
                    if (Peek() == '\\')
                    {
                        Read();

                        char escapedChar = (char)Read();
                        char replacedChar;

                        if (escapedMapping.TryGetValue(escapedChar, out replacedChar))
                            sb.Append(replacedChar);
                        else
                            sb.Append(escapedChar);

                        continue;
                    }

                    if (Peek() == '"')
                        break;

                    sb.Append((char)Read());
                }

                // "
                Read();

                return sb.ToString();
            }

            if (next == '{' || next == '}')
            {
                Read();
                return next.ToString();
            }

            bool bConditionalStart = false;
            int count = 0;
            sb.Clear();
            while (!EndOfStream)
            {
                next = (char)Peek();

                if (next == '"' || next == '{' || next == '}')
                    break;

                if (next == '[')
                    bConditionalStart = true;

                if (next == ']' && bConditionalStart)
                    wasConditional = true;

                if (char.IsWhiteSpace(next))
                    break;

                if (count < 1023)
                {
                    sb.Append(next);
                }
                else
                {
                    throw new Exception("ReadToken overflow");
                }

                Read();
            }

            return sb.ToString();
        }
    }
}
