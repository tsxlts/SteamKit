using System.Globalization;
using System.Text;
using Newtonsoft.Json.Linq;

namespace SteamKit.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyValue
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly static KeyValue Invalid = new KeyValue();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public KeyValue(string? name = null, string? value = null)
        {
            Name = name;
            Value = value;

            Children = new List<KeyValue>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<KeyValue> Children { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public KeyValue this[string key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                var child = Children.Where(c => string.Equals(c.Name, key, StringComparison.OrdinalIgnoreCase));

                if (!child.Any())
                {
                    return Invalid;
                }

                if (child.Count() == 1)
                {
                    return child.First();
                }

                var result = new KeyValue(key);
                foreach (var kv in child)
                {
                    result.Children.AddRange(kv.Children);
                }

                return result;
            }
            set
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                var existingChild = Children.FirstOrDefault(c => string.Equals(c.Name, key, StringComparison.OrdinalIgnoreCase));

                if (existingChild != null)
                {
                    Children.Remove(existingChild);
                }
                value.Name = key;

                Children.Add(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JObject ToObject()
        {
            JObject json = new JObject();
            json.Add(Name ?? "", Value);
            if (Children.Any())
            {
                JObject childJson = new JObject();
                foreach (var child in Children)
                {

                    if (!childJson.ContainsKey(child.Name!))
                    {
                        childJson.Add(child.Name!, child.ToObject().GetValue(child.Name!));
                        continue;
                    }

                    JArray jArray = new JArray();
                    var temp = childJson[child.Name!];
                    if (temp is JArray array)
                    {
                        foreach (var item in array)
                        {
                            jArray.Add(item);
                        }
                    }
                    else
                    {
                        jArray.Add(temp!);
                    }
                    jArray.Add(child.ToObject().GetValue(child.Name!)!);

                    childJson[child.Name!] = jArray;
                }

                json[Name!] = childJson;
            }

            return json;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string? AsString()
        {
            return this.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte AsUnsignedByte(byte defaultValue = default)
        {
            byte value;

            if (byte.TryParse(this.Value, out value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ushort AsUnsignedShort(ushort defaultValue = default)
        {
            ushort value;

            if (ushort.TryParse(this.Value, out value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int AsInteger(int defaultValue = default)
        {
            int value;

            if (int.TryParse(this.Value, out value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public uint AsUnsignedInteger(uint defaultValue = default)
        {
            uint value;

            if (uint.TryParse(this.Value, out value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long AsLong(long defaultValue = default)
        {
            long value;

            if (long.TryParse(this.Value, out value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ulong AsUnsignedLong(ulong defaultValue = default)
        {
            ulong value;

            if (ulong.TryParse(this.Value, out value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public float AsFloat(float defaultValue = default)
        {
            float value;

            if (float.TryParse(this.Value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool AsBoolean(bool defaultValue = default)
        {
            int value;

            if (int.TryParse(this.Value, out value) == false)
            {
                return defaultValue;
            }

            return value != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T AsEnum<T>(T defaultValue = default)
            where T : struct
        {
            T value;

            if (Enum.TryParse<T>(this.Value, out value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} = {1}", this.Name, this.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static KeyValue FromStream(Stream stream)
        {
            KeyValue keyValue = new KeyValueReader(stream).ReadKeyValue();

            return keyValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="asBinary"></param>
        /// <returns></returns>
        public void Save(Stream stream, bool asBinary)
        {
            if (asBinary)
            {
                RecursiveSaveBinaryToStream(stream);
            }
            else
            {
                RecursiveSaveTextToFile(stream);
            }
            stream.Seek(0, SeekOrigin.Begin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool TryReadAsBinary(Stream input)
        {
            ArgumentNullException.ThrowIfNull(input);
            return TryReadAsBinaryCore(input, this);
        }

        internal void RecursiveLoadFromBuffer(KeyValueReader kvr)
        {
            bool wasQuoted;
            bool wasConditional;

            while (true)
            {
                var name = kvr.ReadToken(out wasQuoted, out wasConditional);
                if (name is null || name.Length == 0)
                {
                    throw new InvalidDataException("RecursiveLoadFromBuffer: got EOF or empty keyname");
                }

                if (name.StartsWith("}") && !wasQuoted)	// top level closed, stop reading
                {
                    break;
                }

                KeyValue dat = new KeyValue(name);
                dat.Children = new List<KeyValue>();
                Children.Add(dat);

                string? value = kvr.ReadToken(out wasQuoted, out wasConditional);

                if (wasConditional && value != null)
                {
                    value = kvr.ReadToken(out wasQuoted, out wasConditional);
                }

                if (value == null)
                {
                    throw new Exception("RecursiveLoadFromBuffer:  got NULL key");
                }

                if (value.StartsWith("}") && !wasQuoted)
                {
                    throw new Exception("RecursiveLoadFromBuffer:  got } in key");
                }

                if (value.StartsWith("{") && !wasQuoted)
                {
                    dat.RecursiveLoadFromBuffer(kvr);
                }
                else
                {
                    if (wasConditional)
                    {
                        throw new Exception("RecursiveLoadFromBuffer:  got conditional between key and value");
                    }

                    dat.Value = value;
                }
            }
        }

        internal void RecursiveSaveBinaryToStream(Stream f)
        {
            RecursiveSaveBinaryToStreamCore(f);
            f.WriteByte((byte)Type.End);
        }

        private void RecursiveSaveTextToFile(Stream stream, int indentLevel = 0)
        {
            // write header
            WriteIndents(stream, indentLevel);
            WriteString(stream, GetNameForSerialization(), true);
            WriteString(stream, "\n");
            WriteIndents(stream, indentLevel);
            WriteString(stream, "{\n");

            // loop through all our keys writing them to disk
            foreach (KeyValue child in Children)
            {
                if (child.Value == null)
                {
                    child.RecursiveSaveTextToFile(stream, indentLevel + 1);
                }
                else
                {
                    WriteIndents(stream, indentLevel + 1);
                    WriteString(stream, child.GetNameForSerialization(), true);
                    WriteString(stream, "\t\t");
                    WriteString(stream, EscapeText(child.Value), true);
                    WriteString(stream, "\n");
                }
            }

            WriteIndents(stream, indentLevel);
            WriteString(stream, "}\n");
        }

        internal void RecursiveSaveBinaryToStreamCore(Stream f)
        {
            // Only supported types ATM:
            // 1. KeyValue with children (no value itself)
            // 2. String KeyValue
            if (Value == null)
            {
                f.WriteByte((byte)Type.None);
                f.WriteNullTermString(GetNameForSerialization(), Encoding.UTF8);
                foreach (var child in Children)
                {
                    child.RecursiveSaveBinaryToStreamCore(f);
                }
                f.WriteByte((byte)Type.End);
            }
            else
            {
                f.WriteByte((byte)Type.String);
                f.WriteNullTermString(GetNameForSerialization(), Encoding.UTF8);
                f.WriteNullTermString(Value, Encoding.UTF8);
            }
        }

        void WriteIndents(Stream stream, int indentLevel)
        {
            WriteString(stream, new string('\t', indentLevel));
        }

        string GetNameForSerialization()
        {
            if (Name is null)
            {
                throw new InvalidOperationException("Cannot serialise a KeyValue object with a null name!");
            }

            return Name;
        }

        static void WriteString(Stream stream, string str, bool quote = false)
        {
            byte[] bytes = Encoding.UTF8.GetBytes((quote ? "\"" : "") + str.Replace("\"", "\\\"") + (quote ? "\"" : ""));
            stream.Write(bytes, 0, bytes.Length);
        }

        static string EscapeText(string value)
        {
            foreach (var kvp in KeyValueReader.escapedMapping)
            {
                var textToReplace = new string(kvp.Value, 1);
                var escapedReplacement = @"\" + kvp.Key;
                value = value.Replace(textToReplace, escapedReplacement);
            }

            return value;
        }

        static bool TryReadAsBinaryCore(Stream input, KeyValue parent)
        {
            KeyValue current = new KeyValue();
            while (true)
            {
                var type = (Type)input.ReadByte();

                if (type is Type.End or Type.AlternateEnd)
                {
                    break;
                }

                current.Name = input.ReadNullTermString(Encoding.UTF8);

                switch (type)
                {
                    case Type.None:
                        {
                            var didReadChild = TryReadAsBinaryCore(input, current);
                            if (!didReadChild)
                            {
                                return false;
                            }
                            break;
                        }

                    case Type.String:
                        {
                            current.Value = input.ReadNullTermString(Encoding.UTF8);
                            break;
                        }

                    case Type.WideString:
                        {
                            return false;
                        }

                    case Type.Int32:
                    case Type.Color:
                    case Type.Pointer:
                        {
                            current.Value = Convert.ToString(input.ReadInt32());
                            break;
                        }

                    case Type.UInt64:
                        {
                            current.Value = Convert.ToString(input.ReadUInt64());
                            break;
                        }

                    case Type.Float32:
                        {
                            current.Value = Convert.ToString(input.ReadFloat());
                            break;
                        }

                    case Type.Int64:
                        {
                            current.Value = Convert.ToString(input.ReadInt64());
                            break;
                        }

                    default:
                        {
                            return false;
                        }
                }

                parent.Children.Add(current);
                current = new KeyValue();
            }

            return true;
        }

        enum Type : byte
        {
            None = 0,
            String = 1,
            Int32 = 2,
            Float32 = 3,
            Pointer = 4,
            WideString = 5,
            Color = 6,
            UInt64 = 7,
            End = 8,
            Int64 = 10,
            AlternateEnd = 11,
        }
    }
}
