namespace SteamKit.Types
{
    internal class GlobalId : IEquatable<GlobalId>
    {
        BitVector64 gidBits;

        /// <summary>
        /// 
        /// </summary>
        public GlobalId() : this(ulong.MaxValue)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gid"></param>
        public GlobalId(ulong gid)
        {
            this.gidBits = new BitVector64(gid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gid"></param>
        public static implicit operator ulong(GlobalId gid)
        {
            ArgumentNullException.ThrowIfNull(gid);

            return gid.gidBits.Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gid"></param>
        public static implicit operator GlobalId(ulong gid)
        {
            return new GlobalId(gid);
        }

        /// <summary>
        /// 
        /// </summary>
        public ulong Sequential
        {
            get { return gidBits[0, 0xFFFFF]; }
            set { gidBits[0, 0xFFFFF] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ulong StartTime
        {
            get
            {
                uint startTime = (uint)gidBits[20, 0x3FFFFFFF];
                return startTime;
            }
            set
            {
                ulong startTime = value;
                gidBits[20, 0x3FFFFFFF] = startTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public uint ProcessId
        {
            get { return (uint)gidBits[50, 0xF]; }
            set { gidBits[50, 0xF] = (ulong)value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public uint BoxId
        {
            get { return (uint)gidBits[54, 0x3FF]; }
            set { gidBits[54, 0x3FF] = (ulong)value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ulong Value
        {
            get { return gidBits.Data; }
            set { gidBits.Data = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is not GlobalId gid)
            {
                return false;
            }

            return gidBits.Data == gid.gidBits.Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public bool Equals(GlobalId? gid)
        {
            if (gid is null)
            {
                return false;
            }

            return gidBits.Data == gid.gidBits.Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(GlobalId? a, GlobalId? b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.gidBits.Data == b.gidBits.Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(GlobalId? a, GlobalId? b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return gidBits.Data.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
