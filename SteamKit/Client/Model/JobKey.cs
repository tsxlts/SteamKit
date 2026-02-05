using System.Diagnostics.CodeAnalysis;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class JobKey
    {
        /// <summary>
        /// 
        /// </summary>
        public JobKey(object msg) : this(msg, 18446744073709551615ul)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="jobId"></param>
        public JobKey(object msg, ulong jobId)
        {
            this.Msg = msg;
            this.JobId = jobId;
        }

        /// <summary>
        /// 
        /// </summary>
        public object Msg { get; }

        /// <summary>
        /// 
        /// </summary>
        public ulong JobId { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null || !(obj is JobKey other))
            {
                return false;
            }

            return Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(JobKey other)
        {
            return other.Msg.Equals(Msg) && other.JobId.Equals(JobId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Msg.GetHashCode(), JobId.GetHashCode());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(JobKey a, JobKey b)
        {
            return Equals(a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(JobKey a, JobKey b)
        {
            return !(a == b);
        }
    }
}
