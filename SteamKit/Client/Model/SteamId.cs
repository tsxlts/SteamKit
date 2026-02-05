using SteamKit.Types;

namespace SteamKit.Client.Model
{
    /// <summary>
    /// SteamId
    /// </summary>
    public class SteamId
    {
        readonly BitVector64 steamid;

        /// <summary>
        /// The account instance value when representing all instanced <see cref="SteamId">SteamIDs</see>.
        /// </summary>
        public const uint AllInstances = 0;

        /// <summary>
        /// The account instance value for a desktop <see cref="SteamId"/>.
        /// </summary>
        public const uint DesktopInstance = 1;

        /// <summary>
        /// The account instance value for a console <see cref="SteamId"/>.
        /// </summary>
        public const uint ConsoleInstance = 2;

        /// <summary>
        /// The account instance for mobile or web based <see cref="SteamId">SteamIDs</see>.
        /// </summary>
        public const uint WebInstance = 4;

        /// <summary>
        /// Masking value used for the account id.
        /// </summary>
        public const uint AccountIdMask = 0xFFFFFFFF;

        /// <summary>
        /// Masking value used for packing chat instance flags into a <see cref="SteamId"/>.
        /// </summary>
        public const uint AccountInstanceMask = 0x000FFFFF;

        static readonly Dictionary<EAccountType, char> AccountTypeChars = new Dictionary<EAccountType, char>
        {
            { EAccountType.AnonGameServer, 'A' },
            { EAccountType.GameServer, 'G' },
            { EAccountType.Multiseat, 'M' },
            { EAccountType.Pending, 'P' },
            { EAccountType.ContentServer, 'C' },
            { EAccountType.Clan, 'g' },
            { EAccountType.Chat, 'T' }, // Lobby chat is 'L', Clan chat is 'c'
            { EAccountType.Invalid, 'I' },
            { EAccountType.Individual, 'U' },
            { EAccountType.AnonUser, 'a' },
        };

        const char UnknownAccountTypeChar = 'i';

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="SteamId"/> class.
        /// </summary>
        public SteamId() : this(0)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="SteamId"/> class.
        /// </summary>
        /// <param name="unAccountID">The account ID.</param>
        /// <param name="eUniverse">The universe.</param>
        /// <param name="eAccountType">The account type.</param>
        public SteamId(uint unAccountID, EUniverse eUniverse, EAccountType eAccountType) : this()
        {
            Set(unAccountID, eUniverse, eAccountType);
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="SteamId"/> class.
        /// </summary>
        /// <param name="unAccountID">The account ID.</param>
        /// <param name="unInstance">The instance.</param>
        /// <param name="eUniverse">The universe.</param>
        /// <param name="eAccountType">The account type.</param>
        public SteamId(uint unAccountID, uint unInstance, EUniverse eUniverse, EAccountType eAccountType) : this()
        {
            InstancedSet(unAccountID, unInstance, eUniverse, eAccountType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SteamId"/> class.
        /// </summary>
        /// <param name="id">The 64bit integer to assign this SteamID from.</param>
        public SteamId(ulong id) => this.steamid = new BitVector64(id);

        /// <summary>
        /// Converts this SteamID into it's 64bit integer form.
        /// </summary>
        /// <returns>A 64bit integer representing this SteamID.</returns>
        public ulong ConvertToUInt64() => steamid.Data;

        /// <summary>
        /// Sets the various components of this SteamID instance.
        /// </summary>
        /// <param name="unAccountID">The account ID.</param>
        /// <param name="eUniverse">The universe.</param>
        /// <param name="eAccountType">The account type.</param>
        public void Set(uint unAccountID, EUniverse eUniverse, EAccountType eAccountType)
        {
            this.AccountId = unAccountID;
            this.AccountUniverse = eUniverse;
            this.AccountType = eAccountType;

            if (eAccountType == EAccountType.Clan || eAccountType == EAccountType.GameServer)
            {
                this.AccountInstance = 0;
            }
            else
            {
                this.AccountInstance = DesktopInstance;
            }
        }

        /// <summary>
        /// Sets the various components of this SteamID instance.
        /// </summary>
        /// <param name="unAccountID">The account ID.</param>
        /// <param name="unInstance">The instance.</param>
        /// <param name="eUniverse">The universe.</param>
        /// <param name="eAccountType">The account type.</param>
        public void InstancedSet(uint unAccountID, uint unInstance, EUniverse eUniverse, EAccountType eAccountType)
        {
            this.AccountId = unAccountID;
            this.AccountUniverse = eUniverse;
            this.AccountType = eAccountType;
            this.AccountInstance = unInstance;
        }

        /// <summary>
        /// Renders this instance into it's Steam2 "STEAM_" or Steam3 representation.
        /// </summary>
        /// <param name="steam3">If set to <c>true</c>, the Steam3 rendering will be returned; otherwise, the Steam2 STEAM_ rendering.</param>
        /// <returns>
        /// A string Steam2 "STEAM_" representation of this SteamID, or a Steam3 representation.
        /// </returns>
        public string Render(bool steam3 = true)
        {
            if (steam3)
            {
                return RenderSteam3();
            }

            return RenderSteam2();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString() => Render();

        /// <summary>
        /// Performs an implicit conversion from <see cref="SteamId"/> to <see cref="ulong"/>.
        /// </summary>
        /// <param name="steamId">The SteamID.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ulong(SteamId steamId)
        {
            if (steamId is null)
            {
                throw new ArgumentNullException(nameof(steamId));
            }

            return steamId.steamid.Data;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="ulong"/> to <see cref="SteamId"/>.
        /// </summary>
        /// <param name="steamId">A 64bit integer representing the SteamID.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator SteamId(ulong steamId) => new SteamId(steamId);

        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        /// <value>
        /// The account id.
        /// </value>
        public uint AccountId
        {
            get => (uint)steamid[0, 0xFFFFFFFF];
            set => steamid[0, 0xFFFFFFFF] = value;
        }

        /// <summary>
        /// Gets or sets the account instance.
        /// </summary>
        /// <value>
        /// The account instance.
        /// </value>
        public uint AccountInstance
        {
            get => (uint)steamid[32, 0xFFFFF];
            set => steamid[32, 0xFFFFF] = (ulong)value;
        }

        /// <summary>
        /// Gets or sets the account type.
        /// </summary>
        /// <value>
        /// The account type.
        /// </value>
        public EAccountType AccountType
        {
            get => (EAccountType)steamid[52, 0xF];
            set => steamid[52, 0xF] = (ulong)value;
        }

        /// <summary>
        /// Gets or sets the account universe.
        /// </summary>
        /// <value>
        /// The account universe.
        /// </value>
        public EUniverse AccountUniverse
        {
            get => (EUniverse)steamid[56, 0xFF];
            set => steamid[56, 0xFF] = (ulong)value;
        }

        private string RenderSteam2()
        {
            switch (AccountType)
            {
                case EAccountType.Invalid:
                case EAccountType.Individual:
                    var universeDigit = (AccountUniverse <= EUniverse.Public) ? "0" : Enum.Format(typeof(EUniverse), AccountUniverse, "D");
                    return $"STEAM_{universeDigit}:{AccountId & 1}:{AccountId >> 1}";
                default:
                    return Convert.ToString(this)!;
            }
        }

        private string RenderSteam3()
        {
            if (!AccountTypeChars.TryGetValue(AccountType, out var accountTypeChar))
            {
                accountTypeChar = UnknownAccountTypeChar;
            }

            if (AccountType == EAccountType.Chat)
            {
                if (((ChatInstanceFlags)AccountInstance).HasFlag(ChatInstanceFlags.Clan))
                {
                    accountTypeChar = 'c';
                }
                else if (((ChatInstanceFlags)AccountInstance).HasFlag(ChatInstanceFlags.Lobby))
                {
                    accountTypeChar = 'L';
                }
            }

            bool renderInstance = false;

            switch (AccountType)
            {
                case EAccountType.AnonGameServer:
                case EAccountType.Multiseat:
                    renderInstance = true;
                    break;

                case EAccountType.Individual:
                    renderInstance = (AccountInstance != DesktopInstance);
                    break;
            }

            if (renderInstance)
            {
                return $"[{accountTypeChar}:{(uint)AccountUniverse}:{AccountId}:{AccountInstance}]";
            }

            return $"[{accountTypeChar}:{(uint)AccountUniverse}:{AccountId}]";
        }

        /// <summary>
        /// Represents various flags a chat <see cref="SteamId"/> may have, packed into its instance.
        /// </summary>
        [Flags]
        public enum ChatInstanceFlags : uint
        {
            /// <summary>
            /// This flag is set for clan based chat <see cref="SteamId">SteamIDs</see>.
            /// </summary>
            Clan = (AccountInstanceMask + 1) >> 1,
            /// <summary>
            /// This flag is set for lobby based chat <see cref="SteamId">SteamIDs</see>.
            /// </summary>
            Lobby = (AccountInstanceMask + 1) >> 2,
            /// <summary>
            /// This flag is set for matchmaking lobby based chat <see cref="SteamId">SteamIDs</see>.
            /// </summary>
            MMSLobby = (AccountInstanceMask + 1) >> 3,
        }
    }
}
