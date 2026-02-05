
namespace SteamKit
{
    /// <summary>
    /// JobFailedException
    /// </summary>
    public class JobFailedException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobId"></param>
        public JobFailedException(ulong jobId) : this(jobId, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="message"></param>
        public JobFailedException(ulong jobId, string? message) : base(message)
        {
            JobId = jobId;
        }

        /// <summary>
        /// 
        /// </summary>
        public ulong JobId { get; set; }
    }
}
