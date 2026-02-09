using SteamKit.Internal;

namespace SteamKit.Types
{
    /// <summary>
    /// MachineId
    /// </summary>
    public class MachineId : MessageObject
    {
        /// <summary>
        /// 
        /// </summary>
        public MachineId() : base()
        {
            KeyValues["BB3"] = new KeyValue();
            KeyValues["FF2"] = new KeyValue();
            KeyValues["3B3"] = new KeyValue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetBB3(string value)
        {
            KeyValues["BB3"].Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetFF2(string value)
        {
            KeyValues["FF2"].Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Set3B3(string value)
        {
            KeyValues["3B3"].Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Set333(string value)
        {
            KeyValues["333"] = new KeyValue(value: value);
        }
    }
}
