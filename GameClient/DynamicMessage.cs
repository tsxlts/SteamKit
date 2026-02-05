using System.Text;
using Google.Protobuf;

namespace GameClient
{
    public class ProtobufField
    {
        public int FieldNumber { get; set; }
        public WireFormat.WireType WireType { get; set; }
        public object Value { get; set; }
        public string ValueType { get; set; }
    }

    public class ProtobufParser
    {
        public static List<ProtobufField> ParseProtobuf(byte[] data)
        {
            var fields = new List<ProtobufField>();
            var input = new CodedInputStream(data);

            while (!input.IsAtEnd)
            {
                var tag = input.ReadTag();
                if (tag == 0) break;

                var fieldNumber = WireFormat.GetTagFieldNumber(tag);
                var wireType = WireFormat.GetTagWireType(tag);

                var field = new ProtobufField
                {
                    FieldNumber = fieldNumber,
                    WireType = wireType
                };

                try
                {
                    switch (wireType)
                    {
                        case WireFormat.WireType.Varint:
                            var varintValue = input.ReadUInt64();
                            field.Value = varintValue;
                            field.ValueType = "Varint";
                            break;

                        case WireFormat.WireType.Fixed64:
                            var fixed64Value = input.ReadDouble();
                            field.Value = fixed64Value;
                            field.ValueType = "Fixed64/Double";
                            break;

                        case WireFormat.WireType.LengthDelimited:
                            var length = input.ReadLength();
                            var bytes = input.ReadBytes();
                            field.Value = bytes;

                            // 尝试推断类型
                            if (IsLikelyString(bytes.ToByteArray()))
                            {
                                field.ValueType = "String";
                                field.Value = bytes.ToStringUtf8();
                            }
                            else if (IsLikelyNestedMessage(bytes.ToByteArray()))
                            {
                                field.ValueType = "Nested Message";
                                field.Value = ParseProtobuf(bytes.ToByteArray());
                            }
                            else
                            {
                                field.ValueType = "Bytes";
                            }
                            break;

                        case WireFormat.WireType.Fixed32:
                            var fixed32Value = input.ReadFloat();
                            field.Value = fixed32Value;
                            field.ValueType = "Fixed32/Float";
                            break;

                        default:
                            field.Value = "Unsupported wire type";
                            field.ValueType = "Unknown";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    field.Value = $"Error reading field: {ex.Message}";
                    field.ValueType = "Error";
                }

                fields.Add(field);
            }

            return fields;
        }

        private static bool IsLikelyString(byte[] bytes)
        {
            try
            {
                Encoding.UTF8.GetString(bytes);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsLikelyNestedMessage(byte[] bytes)
        {
            // 简单的启发式判断：如果包含有效的 protobuf 标签，可能是嵌套消息
            if (bytes.Length < 2) return false;

            try
            {
                // 尝试解析为嵌套消息
                var nestedFields = ParseProtobuf(bytes);
                return nestedFields.Count > 0;
            }
            catch
            {
                return false;
            }
        }

    }
}
