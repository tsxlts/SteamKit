namespace SteamKit.Client.Model.Proto
{
    [global::ProtoBuf.ProtoContract()]
    public partial class NoResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientHello : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint protocol_version
        {
            get => __pbn__protocol_version.GetValueOrDefault();
            set => __pbn__protocol_version = value;
        }
        public bool ShouldSerializeprotocol_version() => __pbn__protocol_version != null;
        public void Resetprotocol_version() => __pbn__protocol_version = null;
        private uint? __pbn__protocol_version;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientHeartBeat : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool send_reply
        {
            get => __pbn__send_reply.GetValueOrDefault();
            set => __pbn__send_reply = value;
        }
        public bool ShouldSerializesend_reply() => __pbn__send_reply != null;
        public void Resetsend_reply() => __pbn__send_reply = null;
        private bool? __pbn__send_reply;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientSecret : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint version
        {
            get => __pbn__version.GetValueOrDefault();
            set => __pbn__version = value;
        }
        public bool ShouldSerializeversion() => __pbn__version != null;
        public void Resetversion() => __pbn__version = null;
        private uint? __pbn__version;

        [global::ProtoBuf.ProtoMember(2)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

        [global::ProtoBuf.ProtoMember(3)]
        public uint deviceid
        {
            get => __pbn__deviceid.GetValueOrDefault();
            set => __pbn__deviceid = value;
        }
        public bool ShouldSerializedeviceid() => __pbn__deviceid != null;
        public void Resetdeviceid() => __pbn__deviceid = null;
        private uint? __pbn__deviceid;

        [global::ProtoBuf.ProtoMember(4, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong nonce
        {
            get => __pbn__nonce.GetValueOrDefault();
            set => __pbn__nonce = value;
        }
        public bool ShouldSerializenonce() => __pbn__nonce != null;
        public void Resetnonce() => __pbn__nonce = null;
        private ulong? __pbn__nonce;

        [global::ProtoBuf.ProtoMember(5)]
        public byte[] hmac
        {
            get => __pbn__hmac;
            set => __pbn__hmac = value;
        }
        public bool ShouldSerializehmac() => __pbn__hmac != null;
        public void Resethmac() => __pbn__hmac = null;
        private byte[] __pbn__hmac;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientLogon : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint protocol_version
        {
            get => __pbn__protocol_version.GetValueOrDefault();
            set => __pbn__protocol_version = value;
        }
        public bool ShouldSerializeprotocol_version() => __pbn__protocol_version != null;
        public void Resetprotocol_version() => __pbn__protocol_version = null;
        private uint? __pbn__protocol_version;

        [global::ProtoBuf.ProtoMember(2)]
        public uint deprecated_obfustucated_private_ip
        {
            get => __pbn__deprecated_obfustucated_private_ip.GetValueOrDefault();
            set => __pbn__deprecated_obfustucated_private_ip = value;
        }
        public bool ShouldSerializedeprecated_obfustucated_private_ip() => __pbn__deprecated_obfustucated_private_ip != null;
        public void Resetdeprecated_obfustucated_private_ip() => __pbn__deprecated_obfustucated_private_ip = null;
        private uint? __pbn__deprecated_obfustucated_private_ip;

        [global::ProtoBuf.ProtoMember(3)]
        public uint cell_id
        {
            get => __pbn__cell_id.GetValueOrDefault();
            set => __pbn__cell_id = value;
        }
        public bool ShouldSerializecell_id() => __pbn__cell_id != null;
        public void Resetcell_id() => __pbn__cell_id = null;
        private uint? __pbn__cell_id;

        [global::ProtoBuf.ProtoMember(4)]
        public uint last_session_id
        {
            get => __pbn__last_session_id.GetValueOrDefault();
            set => __pbn__last_session_id = value;
        }
        public bool ShouldSerializelast_session_id() => __pbn__last_session_id != null;
        public void Resetlast_session_id() => __pbn__last_session_id = null;
        private uint? __pbn__last_session_id;

        [global::ProtoBuf.ProtoMember(5)]
        public uint client_package_version
        {
            get => __pbn__client_package_version.GetValueOrDefault();
            set => __pbn__client_package_version = value;
        }
        public bool ShouldSerializeclient_package_version() => __pbn__client_package_version != null;
        public void Resetclient_package_version() => __pbn__client_package_version = null;
        private uint? __pbn__client_package_version;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string client_language
        {
            get => __pbn__client_language ?? "";
            set => __pbn__client_language = value;
        }
        public bool ShouldSerializeclient_language() => __pbn__client_language != null;
        public void Resetclient_language() => __pbn__client_language = null;
        private string __pbn__client_language;

        [global::ProtoBuf.ProtoMember(7)]
        public uint client_os_type
        {
            get => __pbn__client_os_type.GetValueOrDefault();
            set => __pbn__client_os_type = value;
        }
        public bool ShouldSerializeclient_os_type() => __pbn__client_os_type != null;
        public void Resetclient_os_type() => __pbn__client_os_type = null;
        private uint? __pbn__client_os_type;

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue(false)]
        public bool should_remember_password
        {
            get => __pbn__should_remember_password ?? false;
            set => __pbn__should_remember_password = value;
        }
        public bool ShouldSerializeshould_remember_password() => __pbn__should_remember_password != null;
        public void Resetshould_remember_password() => __pbn__should_remember_password = null;
        private bool? __pbn__should_remember_password;

        [global::ProtoBuf.ProtoMember(9)]
        [global::System.ComponentModel.DefaultValue("")]
        public string wine_version
        {
            get => __pbn__wine_version ?? "";
            set => __pbn__wine_version = value;
        }
        public bool ShouldSerializewine_version() => __pbn__wine_version != null;
        public void Resetwine_version() => __pbn__wine_version = null;
        private string __pbn__wine_version;

        [global::ProtoBuf.ProtoMember(10)]
        public uint deprecated_10
        {
            get => __pbn__deprecated_10.GetValueOrDefault();
            set => __pbn__deprecated_10 = value;
        }
        public bool ShouldSerializedeprecated_10() => __pbn__deprecated_10 != null;
        public void Resetdeprecated_10() => __pbn__deprecated_10 = null;
        private uint? __pbn__deprecated_10;

        [global::ProtoBuf.ProtoMember(11)]
        public CMsgIPAddress obfuscated_private_ip { get; set; }

        [global::ProtoBuf.ProtoMember(20)]
        public uint deprecated_public_ip
        {
            get => __pbn__deprecated_public_ip.GetValueOrDefault();
            set => __pbn__deprecated_public_ip = value;
        }
        public bool ShouldSerializedeprecated_public_ip() => __pbn__deprecated_public_ip != null;
        public void Resetdeprecated_public_ip() => __pbn__deprecated_public_ip = null;
        private uint? __pbn__deprecated_public_ip;

        [global::ProtoBuf.ProtoMember(21)]
        public uint qos_level
        {
            get => __pbn__qos_level.GetValueOrDefault();
            set => __pbn__qos_level = value;
        }
        public bool ShouldSerializeqos_level() => __pbn__qos_level != null;
        public void Resetqos_level() => __pbn__qos_level = null;
        private uint? __pbn__qos_level;

        [global::ProtoBuf.ProtoMember(22, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong client_supplied_steam_id
        {
            get => __pbn__client_supplied_steam_id.GetValueOrDefault();
            set => __pbn__client_supplied_steam_id = value;
        }
        public bool ShouldSerializeclient_supplied_steam_id() => __pbn__client_supplied_steam_id != null;
        public void Resetclient_supplied_steam_id() => __pbn__client_supplied_steam_id = null;
        private ulong? __pbn__client_supplied_steam_id;

        [global::ProtoBuf.ProtoMember(23)]
        public CMsgIPAddress public_ip { get; set; }

        [global::ProtoBuf.ProtoMember(30)]
        public byte[] machine_id
        {
            get => __pbn__machine_id;
            set => __pbn__machine_id = value;
        }
        public bool ShouldSerializemachine_id() => __pbn__machine_id != null;
        public void Resetmachine_id() => __pbn__machine_id = null;
        private byte[] __pbn__machine_id;

        [global::ProtoBuf.ProtoMember(31)]
        [global::System.ComponentModel.DefaultValue(0u)]
        public uint launcher_type
        {
            get => __pbn__launcher_type ?? 0u;
            set => __pbn__launcher_type = value;
        }
        public bool ShouldSerializelauncher_type() => __pbn__launcher_type != null;
        public void Resetlauncher_type() => __pbn__launcher_type = null;
        private uint? __pbn__launcher_type;

        [global::ProtoBuf.ProtoMember(32)]
        [global::System.ComponentModel.DefaultValue(0u)]
        public uint ui_mode
        {
            get => __pbn__ui_mode ?? 0u;
            set => __pbn__ui_mode = value;
        }
        public bool ShouldSerializeui_mode() => __pbn__ui_mode != null;
        public void Resetui_mode() => __pbn__ui_mode = null;
        private uint? __pbn__ui_mode;

        [global::ProtoBuf.ProtoMember(33)]
        [global::System.ComponentModel.DefaultValue(0u)]
        public uint chat_mode
        {
            get => __pbn__chat_mode ?? 0u;
            set => __pbn__chat_mode = value;
        }
        public bool ShouldSerializechat_mode() => __pbn__chat_mode != null;
        public void Resetchat_mode() => __pbn__chat_mode = null;
        private uint? __pbn__chat_mode;

        [global::ProtoBuf.ProtoMember(41)]
        public byte[] steam2_auth_ticket
        {
            get => __pbn__steam2_auth_ticket;
            set => __pbn__steam2_auth_ticket = value;
        }
        public bool ShouldSerializesteam2_auth_ticket() => __pbn__steam2_auth_ticket != null;
        public void Resetsteam2_auth_ticket() => __pbn__steam2_auth_ticket = null;
        private byte[] __pbn__steam2_auth_ticket;

        [global::ProtoBuf.ProtoMember(42)]
        [global::System.ComponentModel.DefaultValue("")]
        public string email_address
        {
            get => __pbn__email_address ?? "";
            set => __pbn__email_address = value;
        }
        public bool ShouldSerializeemail_address() => __pbn__email_address != null;
        public void Resetemail_address() => __pbn__email_address = null;
        private string __pbn__email_address;

        [global::ProtoBuf.ProtoMember(43, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public uint rtime32_account_creation
        {
            get => __pbn__rtime32_account_creation.GetValueOrDefault();
            set => __pbn__rtime32_account_creation = value;
        }
        public bool ShouldSerializertime32_account_creation() => __pbn__rtime32_account_creation != null;
        public void Resetrtime32_account_creation() => __pbn__rtime32_account_creation = null;
        private uint? __pbn__rtime32_account_creation;

        [global::ProtoBuf.ProtoMember(50)]
        [global::System.ComponentModel.DefaultValue("")]
        public string account_name
        {
            get => __pbn__account_name ?? "";
            set => __pbn__account_name = value;
        }
        public bool ShouldSerializeaccount_name() => __pbn__account_name != null;
        public void Resetaccount_name() => __pbn__account_name = null;
        private string __pbn__account_name;

        [global::ProtoBuf.ProtoMember(51)]
        [global::System.ComponentModel.DefaultValue("")]
        public string password
        {
            get => __pbn__password ?? "";
            set => __pbn__password = value;
        }
        public bool ShouldSerializepassword() => __pbn__password != null;
        public void Resetpassword() => __pbn__password = null;
        private string __pbn__password;

        [global::ProtoBuf.ProtoMember(52)]
        [global::System.ComponentModel.DefaultValue("")]
        public string game_server_token
        {
            get => __pbn__game_server_token ?? "";
            set => __pbn__game_server_token = value;
        }
        public bool ShouldSerializegame_server_token() => __pbn__game_server_token != null;
        public void Resetgame_server_token() => __pbn__game_server_token = null;
        private string __pbn__game_server_token;

        [global::ProtoBuf.ProtoMember(60)]
        [global::System.ComponentModel.DefaultValue("")]
        public string login_key
        {
            get => __pbn__login_key ?? "";
            set => __pbn__login_key = value;
        }
        public bool ShouldSerializelogin_key() => __pbn__login_key != null;
        public void Resetlogin_key() => __pbn__login_key = null;
        private string __pbn__login_key;

        [global::ProtoBuf.ProtoMember(70)]
        [global::System.ComponentModel.DefaultValue(false)]
        public bool was_converted_deprecated_msg
        {
            get => __pbn__was_converted_deprecated_msg ?? false;
            set => __pbn__was_converted_deprecated_msg = value;
        }
        public bool ShouldSerializewas_converted_deprecated_msg() => __pbn__was_converted_deprecated_msg != null;
        public void Resetwas_converted_deprecated_msg() => __pbn__was_converted_deprecated_msg = null;
        private bool? __pbn__was_converted_deprecated_msg;

        [global::ProtoBuf.ProtoMember(80)]
        [global::System.ComponentModel.DefaultValue("")]
        public string anon_user_target_account_name
        {
            get => __pbn__anon_user_target_account_name ?? "";
            set => __pbn__anon_user_target_account_name = value;
        }
        public bool ShouldSerializeanon_user_target_account_name() => __pbn__anon_user_target_account_name != null;
        public void Resetanon_user_target_account_name() => __pbn__anon_user_target_account_name = null;
        private string __pbn__anon_user_target_account_name;

        [global::ProtoBuf.ProtoMember(81, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong resolved_user_steam_id
        {
            get => __pbn__resolved_user_steam_id.GetValueOrDefault();
            set => __pbn__resolved_user_steam_id = value;
        }
        public bool ShouldSerializeresolved_user_steam_id() => __pbn__resolved_user_steam_id != null;
        public void Resetresolved_user_steam_id() => __pbn__resolved_user_steam_id = null;
        private ulong? __pbn__resolved_user_steam_id;

        [global::ProtoBuf.ProtoMember(82)]
        public int eresult_sentryfile
        {
            get => __pbn__eresult_sentryfile.GetValueOrDefault();
            set => __pbn__eresult_sentryfile = value;
        }
        public bool ShouldSerializeeresult_sentryfile() => __pbn__eresult_sentryfile != null;
        public void Reseteresult_sentryfile() => __pbn__eresult_sentryfile = null;
        private int? __pbn__eresult_sentryfile;

        [global::ProtoBuf.ProtoMember(83)]
        public byte[] sha_sentryfile
        {
            get => __pbn__sha_sentryfile;
            set => __pbn__sha_sentryfile = value;
        }
        public bool ShouldSerializesha_sentryfile() => __pbn__sha_sentryfile != null;
        public void Resetsha_sentryfile() => __pbn__sha_sentryfile = null;
        private byte[] __pbn__sha_sentryfile;

        [global::ProtoBuf.ProtoMember(84)]
        [global::System.ComponentModel.DefaultValue("")]
        public string auth_code
        {
            get => __pbn__auth_code ?? "";
            set => __pbn__auth_code = value;
        }
        public bool ShouldSerializeauth_code() => __pbn__auth_code != null;
        public void Resetauth_code() => __pbn__auth_code = null;
        private string __pbn__auth_code;

        [global::ProtoBuf.ProtoMember(85)]
        public int otp_type
        {
            get => __pbn__otp_type.GetValueOrDefault();
            set => __pbn__otp_type = value;
        }
        public bool ShouldSerializeotp_type() => __pbn__otp_type != null;
        public void Resetotp_type() => __pbn__otp_type = null;
        private int? __pbn__otp_type;

        [global::ProtoBuf.ProtoMember(86)]
        public uint otp_value
        {
            get => __pbn__otp_value.GetValueOrDefault();
            set => __pbn__otp_value = value;
        }
        public bool ShouldSerializeotp_value() => __pbn__otp_value != null;
        public void Resetotp_value() => __pbn__otp_value = null;
        private uint? __pbn__otp_value;

        [global::ProtoBuf.ProtoMember(87)]
        [global::System.ComponentModel.DefaultValue("")]
        public string otp_identifier
        {
            get => __pbn__otp_identifier ?? "";
            set => __pbn__otp_identifier = value;
        }
        public bool ShouldSerializeotp_identifier() => __pbn__otp_identifier != null;
        public void Resetotp_identifier() => __pbn__otp_identifier = null;
        private string __pbn__otp_identifier;

        [global::ProtoBuf.ProtoMember(88)]
        public bool steam2_ticket_request
        {
            get => __pbn__steam2_ticket_request.GetValueOrDefault();
            set => __pbn__steam2_ticket_request = value;
        }
        public bool ShouldSerializesteam2_ticket_request() => __pbn__steam2_ticket_request != null;
        public void Resetsteam2_ticket_request() => __pbn__steam2_ticket_request = null;
        private bool? __pbn__steam2_ticket_request;

        [global::ProtoBuf.ProtoMember(90)]
        public byte[] sony_psn_ticket
        {
            get => __pbn__sony_psn_ticket;
            set => __pbn__sony_psn_ticket = value;
        }
        public bool ShouldSerializesony_psn_ticket() => __pbn__sony_psn_ticket != null;
        public void Resetsony_psn_ticket() => __pbn__sony_psn_ticket = null;
        private byte[] __pbn__sony_psn_ticket;

        [global::ProtoBuf.ProtoMember(91)]
        [global::System.ComponentModel.DefaultValue("")]
        public string sony_psn_service_id
        {
            get => __pbn__sony_psn_service_id ?? "";
            set => __pbn__sony_psn_service_id = value;
        }
        public bool ShouldSerializesony_psn_service_id() => __pbn__sony_psn_service_id != null;
        public void Resetsony_psn_service_id() => __pbn__sony_psn_service_id = null;
        private string __pbn__sony_psn_service_id;

        [global::ProtoBuf.ProtoMember(92)]
        [global::System.ComponentModel.DefaultValue(false)]
        public bool create_new_psn_linked_account_if_needed
        {
            get => __pbn__create_new_psn_linked_account_if_needed ?? false;
            set => __pbn__create_new_psn_linked_account_if_needed = value;
        }
        public bool ShouldSerializecreate_new_psn_linked_account_if_needed() => __pbn__create_new_psn_linked_account_if_needed != null;
        public void Resetcreate_new_psn_linked_account_if_needed() => __pbn__create_new_psn_linked_account_if_needed = null;
        private bool? __pbn__create_new_psn_linked_account_if_needed;

        [global::ProtoBuf.ProtoMember(93)]
        [global::System.ComponentModel.DefaultValue("")]
        public string sony_psn_name
        {
            get => __pbn__sony_psn_name ?? "";
            set => __pbn__sony_psn_name = value;
        }
        public bool ShouldSerializesony_psn_name() => __pbn__sony_psn_name != null;
        public void Resetsony_psn_name() => __pbn__sony_psn_name = null;
        private string __pbn__sony_psn_name;

        [global::ProtoBuf.ProtoMember(94)]
        public int game_server_app_id
        {
            get => __pbn__game_server_app_id.GetValueOrDefault();
            set => __pbn__game_server_app_id = value;
        }
        public bool ShouldSerializegame_server_app_id() => __pbn__game_server_app_id != null;
        public void Resetgame_server_app_id() => __pbn__game_server_app_id = null;
        private int? __pbn__game_server_app_id;

        [global::ProtoBuf.ProtoMember(95)]
        public bool steamguard_dont_remember_computer
        {
            get => __pbn__steamguard_dont_remember_computer.GetValueOrDefault();
            set => __pbn__steamguard_dont_remember_computer = value;
        }
        public bool ShouldSerializesteamguard_dont_remember_computer() => __pbn__steamguard_dont_remember_computer != null;
        public void Resetsteamguard_dont_remember_computer() => __pbn__steamguard_dont_remember_computer = null;
        private bool? __pbn__steamguard_dont_remember_computer;

        [global::ProtoBuf.ProtoMember(96)]
        [global::System.ComponentModel.DefaultValue("")]
        public string machine_name
        {
            get => __pbn__machine_name ?? "";
            set => __pbn__machine_name = value;
        }
        public bool ShouldSerializemachine_name() => __pbn__machine_name != null;
        public void Resetmachine_name() => __pbn__machine_name = null;
        private string __pbn__machine_name;

        [global::ProtoBuf.ProtoMember(97)]
        [global::System.ComponentModel.DefaultValue("")]
        public string machine_name_userchosen
        {
            get => __pbn__machine_name_userchosen ?? "";
            set => __pbn__machine_name_userchosen = value;
        }
        public bool ShouldSerializemachine_name_userchosen() => __pbn__machine_name_userchosen != null;
        public void Resetmachine_name_userchosen() => __pbn__machine_name_userchosen = null;
        private string __pbn__machine_name_userchosen;

        [global::ProtoBuf.ProtoMember(98)]
        [global::System.ComponentModel.DefaultValue("")]
        public string country_override
        {
            get => __pbn__country_override ?? "";
            set => __pbn__country_override = value;
        }
        public bool ShouldSerializecountry_override() => __pbn__country_override != null;
        public void Resetcountry_override() => __pbn__country_override = null;
        private string __pbn__country_override;

        [global::ProtoBuf.ProtoMember(100)]
        public ulong client_instance_id
        {
            get => __pbn__client_instance_id.GetValueOrDefault();
            set => __pbn__client_instance_id = value;
        }
        public bool ShouldSerializeclient_instance_id() => __pbn__client_instance_id != null;
        public void Resetclient_instance_id() => __pbn__client_instance_id = null;
        private ulong? __pbn__client_instance_id;

        [global::ProtoBuf.ProtoMember(101)]
        [global::System.ComponentModel.DefaultValue("")]
        public string two_factor_code
        {
            get => __pbn__two_factor_code ?? "";
            set => __pbn__two_factor_code = value;
        }
        public bool ShouldSerializetwo_factor_code() => __pbn__two_factor_code != null;
        public void Resettwo_factor_code() => __pbn__two_factor_code = null;
        private string __pbn__two_factor_code;

        [global::ProtoBuf.ProtoMember(102)]
        public bool supports_rate_limit_response
        {
            get => __pbn__supports_rate_limit_response.GetValueOrDefault();
            set => __pbn__supports_rate_limit_response = value;
        }
        public bool ShouldSerializesupports_rate_limit_response() => __pbn__supports_rate_limit_response != null;
        public void Resetsupports_rate_limit_response() => __pbn__supports_rate_limit_response = null;
        private bool? __pbn__supports_rate_limit_response;

        [global::ProtoBuf.ProtoMember(103)]
        [global::System.ComponentModel.DefaultValue("")]
        public string web_logon_nonce
        {
            get => __pbn__web_logon_nonce ?? "";
            set => __pbn__web_logon_nonce = value;
        }
        public bool ShouldSerializeweb_logon_nonce() => __pbn__web_logon_nonce != null;
        public void Resetweb_logon_nonce() => __pbn__web_logon_nonce = null;
        private string __pbn__web_logon_nonce;

        [global::ProtoBuf.ProtoMember(104)]
        public int priority_reason
        {
            get => __pbn__priority_reason.GetValueOrDefault();
            set => __pbn__priority_reason = value;
        }
        public bool ShouldSerializepriority_reason() => __pbn__priority_reason != null;
        public void Resetpriority_reason() => __pbn__priority_reason = null;
        private int? __pbn__priority_reason;

        [global::ProtoBuf.ProtoMember(105)]
        public CMsgClientSecret embedded_client_secret { get; set; }

        [global::ProtoBuf.ProtoMember(106)]
        public bool disable_partner_autogrants
        {
            get => __pbn__disable_partner_autogrants.GetValueOrDefault();
            set => __pbn__disable_partner_autogrants = value;
        }
        public bool ShouldSerializedisable_partner_autogrants() => __pbn__disable_partner_autogrants != null;
        public void Resetdisable_partner_autogrants() => __pbn__disable_partner_autogrants = null;
        private bool? __pbn__disable_partner_autogrants;

        [global::ProtoBuf.ProtoMember(108)]
        [global::System.ComponentModel.DefaultValue("")]
        public string access_token
        {
            get => __pbn__access_token ?? "";
            set => __pbn__access_token = value;
        }
        public bool ShouldSerializeaccess_token() => __pbn__access_token != null;
        public void Resetaccess_token() => __pbn__access_token = null;
        private string __pbn__access_token;

        [global::ProtoBuf.ProtoMember(109)]
        public bool is_chrome_os
        {
            get => __pbn__is_chrome_os.GetValueOrDefault();
            set => __pbn__is_chrome_os = value;
        }
        public bool ShouldSerializeis_chrome_os() => __pbn__is_chrome_os != null;
        public void Resetis_chrome_os() => __pbn__is_chrome_os = null;
        private bool? __pbn__is_chrome_os;

        [global::ProtoBuf.ProtoMember(99)]
        public bool is_steam_box_deprecated
        {
            get => __pbn__is_steam_box_deprecated.GetValueOrDefault();
            set => __pbn__is_steam_box_deprecated = value;
        }
        public bool ShouldSerializeis_steam_box_deprecated() => __pbn__is_steam_box_deprecated != null;
        public void Resetis_steam_box_deprecated() => __pbn__is_steam_box_deprecated = null;
        private bool? __pbn__is_steam_box_deprecated;

        [global::ProtoBuf.ProtoMember(107)]
        public bool is_steam_deck_deprecated
        {
            get => __pbn__is_steam_deck_deprecated.GetValueOrDefault();
            set => __pbn__is_steam_deck_deprecated = value;
        }
        public bool ShouldSerializeis_steam_deck_deprecated() => __pbn__is_steam_deck_deprecated != null;
        public void Resetis_steam_deck_deprecated() => __pbn__is_steam_deck_deprecated = null;
        private bool? __pbn__is_steam_deck_deprecated;

        [global::ProtoBuf.ProtoMember(110)]
        public bool is_tesla_deprecated
        {
            get => __pbn__is_tesla_deprecated.GetValueOrDefault();
            set => __pbn__is_tesla_deprecated = value;
        }
        public bool ShouldSerializeis_tesla_deprecated() => __pbn__is_tesla_deprecated != null;
        public void Resetis_tesla_deprecated() => __pbn__is_tesla_deprecated = null;
        private bool? __pbn__is_tesla_deprecated;

        [global::ProtoBuf.ProtoMember(111)]
        public uint gaming_device_type
        {
            get => __pbn__gaming_device_type.GetValueOrDefault();
            set => __pbn__gaming_device_type = value;
        }
        public bool ShouldSerializegaming_device_type() => __pbn__gaming_device_type != null;
        public void Resetgaming_device_type() => __pbn__gaming_device_type = null;
        private uint? __pbn__gaming_device_type;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientLogonResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue(2)]
        public int eresult
        {
            get => __pbn__eresult ?? 2;
            set => __pbn__eresult = value;
        }
        public bool ShouldSerializeeresult() => __pbn__eresult != null;
        public void Reseteresult() => __pbn__eresult = null;
        private int? __pbn__eresult;

        [global::ProtoBuf.ProtoMember(2)]
        public int legacy_out_of_game_heartbeat_seconds
        {
            get => __pbn__legacy_out_of_game_heartbeat_seconds.GetValueOrDefault();
            set => __pbn__legacy_out_of_game_heartbeat_seconds = value;
        }
        public bool ShouldSerializelegacy_out_of_game_heartbeat_seconds() => __pbn__legacy_out_of_game_heartbeat_seconds != null;
        public void Resetlegacy_out_of_game_heartbeat_seconds() => __pbn__legacy_out_of_game_heartbeat_seconds = null;
        private int? __pbn__legacy_out_of_game_heartbeat_seconds;

        [global::ProtoBuf.ProtoMember(3)]
        public int heartbeat_seconds
        {
            get => __pbn__heartbeat_seconds.GetValueOrDefault();
            set => __pbn__heartbeat_seconds = value;
        }
        public bool ShouldSerializeheartbeat_seconds() => __pbn__heartbeat_seconds != null;
        public void Resetheartbeat_seconds() => __pbn__heartbeat_seconds = null;
        private int? __pbn__heartbeat_seconds;

        [global::ProtoBuf.ProtoMember(4)]
        public uint deprecated_public_ip
        {
            get => __pbn__deprecated_public_ip.GetValueOrDefault();
            set => __pbn__deprecated_public_ip = value;
        }
        public bool ShouldSerializedeprecated_public_ip() => __pbn__deprecated_public_ip != null;
        public void Resetdeprecated_public_ip() => __pbn__deprecated_public_ip = null;
        private uint? __pbn__deprecated_public_ip;

        [global::ProtoBuf.ProtoMember(5, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public uint rtime32_server_time
        {
            get => __pbn__rtime32_server_time.GetValueOrDefault();
            set => __pbn__rtime32_server_time = value;
        }
        public bool ShouldSerializertime32_server_time() => __pbn__rtime32_server_time != null;
        public void Resetrtime32_server_time() => __pbn__rtime32_server_time = null;
        private uint? __pbn__rtime32_server_time;

        [global::ProtoBuf.ProtoMember(6)]
        public uint account_flags
        {
            get => __pbn__account_flags.GetValueOrDefault();
            set => __pbn__account_flags = value;
        }
        public bool ShouldSerializeaccount_flags() => __pbn__account_flags != null;
        public void Resetaccount_flags() => __pbn__account_flags = null;
        private uint? __pbn__account_flags;

        [global::ProtoBuf.ProtoMember(7)]
        public uint cell_id
        {
            get => __pbn__cell_id.GetValueOrDefault();
            set => __pbn__cell_id = value;
        }
        public bool ShouldSerializecell_id() => __pbn__cell_id != null;
        public void Resetcell_id() => __pbn__cell_id = null;
        private uint? __pbn__cell_id;

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue("")]
        public string email_domain
        {
            get => __pbn__email_domain ?? "";
            set => __pbn__email_domain = value;
        }
        public bool ShouldSerializeemail_domain() => __pbn__email_domain != null;
        public void Resetemail_domain() => __pbn__email_domain = null;
        private string __pbn__email_domain;

        [global::ProtoBuf.ProtoMember(9)]
        public byte[] steam2_ticket
        {
            get => __pbn__steam2_ticket;
            set => __pbn__steam2_ticket = value;
        }
        public bool ShouldSerializesteam2_ticket() => __pbn__steam2_ticket != null;
        public void Resetsteam2_ticket() => __pbn__steam2_ticket = null;
        private byte[] __pbn__steam2_ticket;

        [global::ProtoBuf.ProtoMember(10)]
        public int eresult_extended
        {
            get => __pbn__eresult_extended.GetValueOrDefault();
            set => __pbn__eresult_extended = value;
        }
        public bool ShouldSerializeeresult_extended() => __pbn__eresult_extended != null;
        public void Reseteresult_extended() => __pbn__eresult_extended = null;
        private int? __pbn__eresult_extended;

        [global::ProtoBuf.ProtoMember(12)]
        public uint cell_id_ping_threshold
        {
            get => __pbn__cell_id_ping_threshold.GetValueOrDefault();
            set => __pbn__cell_id_ping_threshold = value;
        }
        public bool ShouldSerializecell_id_ping_threshold() => __pbn__cell_id_ping_threshold != null;
        public void Resetcell_id_ping_threshold() => __pbn__cell_id_ping_threshold = null;
        private uint? __pbn__cell_id_ping_threshold;

        [global::ProtoBuf.ProtoMember(13)]
        public bool deprecated_use_pics
        {
            get => __pbn__deprecated_use_pics.GetValueOrDefault();
            set => __pbn__deprecated_use_pics = value;
        }
        public bool ShouldSerializedeprecated_use_pics() => __pbn__deprecated_use_pics != null;
        public void Resetdeprecated_use_pics() => __pbn__deprecated_use_pics = null;
        private bool? __pbn__deprecated_use_pics;

        [global::ProtoBuf.ProtoMember(14)]
        [global::System.ComponentModel.DefaultValue("")]
        public string vanity_url
        {
            get => __pbn__vanity_url ?? "";
            set => __pbn__vanity_url = value;
        }
        public bool ShouldSerializevanity_url() => __pbn__vanity_url != null;
        public void Resetvanity_url() => __pbn__vanity_url = null;
        private string __pbn__vanity_url;

        [global::ProtoBuf.ProtoMember(15)]
        public CMsgIPAddress public_ip { get; set; }

        [global::ProtoBuf.ProtoMember(16)]
        [global::System.ComponentModel.DefaultValue("")]
        public string user_country
        {
            get => __pbn__user_country ?? "";
            set => __pbn__user_country = value;
        }
        public bool ShouldSerializeuser_country() => __pbn__user_country != null;
        public void Resetuser_country() => __pbn__user_country = null;
        private string __pbn__user_country;

        [global::ProtoBuf.ProtoMember(20, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong client_supplied_steamid
        {
            get => __pbn__client_supplied_steamid.GetValueOrDefault();
            set => __pbn__client_supplied_steamid = value;
        }
        public bool ShouldSerializeclient_supplied_steamid() => __pbn__client_supplied_steamid != null;
        public void Resetclient_supplied_steamid() => __pbn__client_supplied_steamid = null;
        private ulong? __pbn__client_supplied_steamid;

        [global::ProtoBuf.ProtoMember(21)]
        [global::System.ComponentModel.DefaultValue("")]
        public string ip_country_code
        {
            get => __pbn__ip_country_code ?? "";
            set => __pbn__ip_country_code = value;
        }
        public bool ShouldSerializeip_country_code() => __pbn__ip_country_code != null;
        public void Resetip_country_code() => __pbn__ip_country_code = null;
        private string __pbn__ip_country_code;

        [global::ProtoBuf.ProtoMember(22)]
        public byte[] parental_settings
        {
            get => __pbn__parental_settings;
            set => __pbn__parental_settings = value;
        }
        public bool ShouldSerializeparental_settings() => __pbn__parental_settings != null;
        public void Resetparental_settings() => __pbn__parental_settings = null;
        private byte[] __pbn__parental_settings;

        [global::ProtoBuf.ProtoMember(23)]
        public byte[] parental_setting_signature
        {
            get => __pbn__parental_setting_signature;
            set => __pbn__parental_setting_signature = value;
        }
        public bool ShouldSerializeparental_setting_signature() => __pbn__parental_setting_signature != null;
        public void Resetparental_setting_signature() => __pbn__parental_setting_signature = null;
        private byte[] __pbn__parental_setting_signature;

        [global::ProtoBuf.ProtoMember(24)]
        public int count_loginfailures_to_migrate
        {
            get => __pbn__count_loginfailures_to_migrate.GetValueOrDefault();
            set => __pbn__count_loginfailures_to_migrate = value;
        }
        public bool ShouldSerializecount_loginfailures_to_migrate() => __pbn__count_loginfailures_to_migrate != null;
        public void Resetcount_loginfailures_to_migrate() => __pbn__count_loginfailures_to_migrate = null;
        private int? __pbn__count_loginfailures_to_migrate;

        [global::ProtoBuf.ProtoMember(25)]
        public int count_disconnects_to_migrate
        {
            get => __pbn__count_disconnects_to_migrate.GetValueOrDefault();
            set => __pbn__count_disconnects_to_migrate = value;
        }
        public bool ShouldSerializecount_disconnects_to_migrate() => __pbn__count_disconnects_to_migrate != null;
        public void Resetcount_disconnects_to_migrate() => __pbn__count_disconnects_to_migrate = null;
        private int? __pbn__count_disconnects_to_migrate;

        [global::ProtoBuf.ProtoMember(26)]
        public int ogs_data_report_time_window
        {
            get => __pbn__ogs_data_report_time_window.GetValueOrDefault();
            set => __pbn__ogs_data_report_time_window = value;
        }
        public bool ShouldSerializeogs_data_report_time_window() => __pbn__ogs_data_report_time_window != null;
        public void Resetogs_data_report_time_window() => __pbn__ogs_data_report_time_window = null;
        private int? __pbn__ogs_data_report_time_window;

        [global::ProtoBuf.ProtoMember(27)]
        public ulong client_instance_id
        {
            get => __pbn__client_instance_id.GetValueOrDefault();
            set => __pbn__client_instance_id = value;
        }
        public bool ShouldSerializeclient_instance_id() => __pbn__client_instance_id != null;
        public void Resetclient_instance_id() => __pbn__client_instance_id = null;
        private ulong? __pbn__client_instance_id;

        [global::ProtoBuf.ProtoMember(28)]
        public bool force_client_update_check
        {
            get => __pbn__force_client_update_check.GetValueOrDefault();
            set => __pbn__force_client_update_check = value;
        }
        public bool ShouldSerializeforce_client_update_check() => __pbn__force_client_update_check != null;
        public void Resetforce_client_update_check() => __pbn__force_client_update_check = null;
        private bool? __pbn__force_client_update_check;

        [global::ProtoBuf.ProtoMember(29)]
        [global::System.ComponentModel.DefaultValue("")]
        public string agreement_session_url
        {
            get => __pbn__agreement_session_url ?? "";
            set => __pbn__agreement_session_url = value;
        }
        public bool ShouldSerializeagreement_session_url() => __pbn__agreement_session_url != null;
        public void Resetagreement_session_url() => __pbn__agreement_session_url = null;
        private string __pbn__agreement_session_url;

        [global::ProtoBuf.ProtoMember(30)]
        public ulong token_id
        {
            get => __pbn__token_id.GetValueOrDefault();
            set => __pbn__token_id = value;
        }
        public bool ShouldSerializetoken_id() => __pbn__token_id != null;
        public void Resettoken_id() => __pbn__token_id = null;
        private ulong? __pbn__token_id;

        [global::ProtoBuf.ProtoMember(31)]
        public ulong family_group_id
        {
            get => __pbn__family_group_id.GetValueOrDefault();
            set => __pbn__family_group_id = value;
        }
        public bool ShouldSerializefamily_group_id() => __pbn__family_group_id != null;
        public void Resetfamily_group_id() => __pbn__family_group_id = null;
        private ulong? __pbn__family_group_id;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientLogOff : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientLoggedOff : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue(2)]
        public int eresult
        {
            get => __pbn__eresult ?? 2;
            set => __pbn__eresult = value;
        }
        public bool ShouldSerializeeresult() => __pbn__eresult != null;
        public void Reseteresult() => __pbn__eresult = null;
        private int? __pbn__eresult;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientPlayingSessionState : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(2)]
        public bool playing_blocked
        {
            get => __pbn__playing_blocked.GetValueOrDefault();
            set => __pbn__playing_blocked = value;
        }
        public bool ShouldSerializeplaying_blocked() => __pbn__playing_blocked != null;
        public void Resetplaying_blocked() => __pbn__playing_blocked = null;
        private bool? __pbn__playing_blocked;

        [global::ProtoBuf.ProtoMember(3)]
        public uint playing_app
        {
            get => __pbn__playing_app.GetValueOrDefault();
            set => __pbn__playing_app = value;
        }
        public bool ShouldSerializeplaying_app() => __pbn__playing_app != null;
        public void Resetplaying_app() => __pbn__playing_app = null;
        private uint? __pbn__playing_app;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientKickPlayingSession : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool only_stop_game
        {
            get => __pbn__only_stop_game.GetValueOrDefault();
            set => __pbn__only_stop_game = value;
        }
        public bool ShouldSerializeonly_stop_game() => __pbn__only_stop_game != null;
        public void Resetonly_stop_game() => __pbn__only_stop_game = null;
        private bool? __pbn__only_stop_game;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientIsLimitedAccount : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool bis_limited_account
        {
            get => __pbn__bis_limited_account.GetValueOrDefault();
            set => __pbn__bis_limited_account = value;
        }
        public bool ShouldSerializebis_limited_account() => __pbn__bis_limited_account != null;
        public void Resetbis_limited_account() => __pbn__bis_limited_account = null;
        private bool? __pbn__bis_limited_account;

        [global::ProtoBuf.ProtoMember(2)]
        public bool bis_community_banned
        {
            get => __pbn__bis_community_banned.GetValueOrDefault();
            set => __pbn__bis_community_banned = value;
        }
        public bool ShouldSerializebis_community_banned() => __pbn__bis_community_banned != null;
        public void Resetbis_community_banned() => __pbn__bis_community_banned = null;
        private bool? __pbn__bis_community_banned;

        [global::ProtoBuf.ProtoMember(3)]
        public bool bis_locked_account
        {
            get => __pbn__bis_locked_account.GetValueOrDefault();
            set => __pbn__bis_locked_account = value;
        }
        public bool ShouldSerializebis_locked_account() => __pbn__bis_locked_account != null;
        public void Resetbis_locked_account() => __pbn__bis_locked_account = null;
        private bool? __pbn__bis_locked_account;

        [global::ProtoBuf.ProtoMember(4)]
        public bool bis_limited_account_allowed_to_invite_friends
        {
            get => __pbn__bis_limited_account_allowed_to_invite_friends.GetValueOrDefault();
            set => __pbn__bis_limited_account_allowed_to_invite_friends = value;
        }
        public bool ShouldSerializebis_limited_account_allowed_to_invite_friends() => __pbn__bis_limited_account_allowed_to_invite_friends != null;
        public void Resetbis_limited_account_allowed_to_invite_friends() => __pbn__bis_limited_account_allowed_to_invite_friends = null;
        private bool? __pbn__bis_limited_account_allowed_to_invite_friends;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_GetPasswordRSAPublicKey_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string account_name
        {
            get => __pbn__account_name ?? "";
            set => __pbn__account_name = value;
        }
        public bool ShouldSerializeaccount_name() => __pbn__account_name != null;
        public void Resetaccount_name() => __pbn__account_name = null;
        private string __pbn__account_name;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_GetPasswordRSAPublicKey_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string publickey_mod
        {
            get => __pbn__publickey_mod ?? "";
            set => __pbn__publickey_mod = value;
        }
        public bool ShouldSerializepublickey_mod() => __pbn__publickey_mod != null;
        public void Resetpublickey_mod() => __pbn__publickey_mod = null;
        private string __pbn__publickey_mod;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string publickey_exp
        {
            get => __pbn__publickey_exp ?? "";
            set => __pbn__publickey_exp = value;
        }
        public bool ShouldSerializepublickey_exp() => __pbn__publickey_exp != null;
        public void Resetpublickey_exp() => __pbn__publickey_exp = null;
        private string __pbn__publickey_exp;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong timestamp
        {
            get => __pbn__timestamp.GetValueOrDefault();
            set => __pbn__timestamp = value;
        }
        public bool ShouldSerializetimestamp() => __pbn__timestamp != null;
        public void Resettimestamp() => __pbn__timestamp = null;
        private ulong? __pbn__timestamp;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_BeginAuthSessionViaCredentials_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string device_friendly_name
        {
            get => __pbn__device_friendly_name ?? "";
            set => __pbn__device_friendly_name = value;
        }
        public bool ShouldSerializedevice_friendly_name() => __pbn__device_friendly_name != null;
        public void Resetdevice_friendly_name() => __pbn__device_friendly_name = null;
        private string __pbn__device_friendly_name;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string account_name
        {
            get => __pbn__account_name ?? "";
            set => __pbn__account_name = value;
        }
        public bool ShouldSerializeaccount_name() => __pbn__account_name != null;
        public void Resetaccount_name() => __pbn__account_name = null;
        private string __pbn__account_name;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string encrypted_password
        {
            get => __pbn__encrypted_password ?? "";
            set => __pbn__encrypted_password = value;
        }
        public bool ShouldSerializeencrypted_password() => __pbn__encrypted_password != null;
        public void Resetencrypted_password() => __pbn__encrypted_password = null;
        private string __pbn__encrypted_password;

        [global::ProtoBuf.ProtoMember(4)]
        public ulong encryption_timestamp
        {
            get => __pbn__encryption_timestamp.GetValueOrDefault();
            set => __pbn__encryption_timestamp = value;
        }
        public bool ShouldSerializeencryption_timestamp() => __pbn__encryption_timestamp != null;
        public void Resetencryption_timestamp() => __pbn__encryption_timestamp = null;
        private ulong? __pbn__encryption_timestamp;

        [global::ProtoBuf.ProtoMember(5)]
        public bool remember_login
        {
            get => __pbn__remember_login.GetValueOrDefault();
            set => __pbn__remember_login = value;
        }
        public bool ShouldSerializeremember_login() => __pbn__remember_login != null;
        public void Resetremember_login() => __pbn__remember_login = null;
        private bool? __pbn__remember_login;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue(EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown)]
        public EAuthTokenPlatformType platform_type
        {
            get => __pbn__platform_type ?? EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown;
            set => __pbn__platform_type = value;
        }
        public bool ShouldSerializeplatform_type() => __pbn__platform_type != null;
        public void Resetplatform_type() => __pbn__platform_type = null;
        private EAuthTokenPlatformType? __pbn__platform_type;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue(ESessionPersistence.k_ESessionPersistence_Persistent)]
        public ESessionPersistence persistence
        {
            get => __pbn__persistence ?? ESessionPersistence.k_ESessionPersistence_Persistent;
            set => __pbn__persistence = value;
        }
        public bool ShouldSerializepersistence() => __pbn__persistence != null;
        public void Resetpersistence() => __pbn__persistence = null;
        private ESessionPersistence? __pbn__persistence;

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue(@"Unknown")]
        public string website_id
        {
            get => __pbn__website_id ?? @"Unknown";
            set => __pbn__website_id = value;
        }
        public bool ShouldSerializewebsite_id() => __pbn__website_id != null;
        public void Resetwebsite_id() => __pbn__website_id = null;
        private string __pbn__website_id;

        [global::ProtoBuf.ProtoMember(9)]
        public CAuthentication_DeviceDetails device_details { get; set; }

        [global::ProtoBuf.ProtoMember(10)]
        [global::System.ComponentModel.DefaultValue("")]
        public string guard_data
        {
            get => __pbn__guard_data ?? "";
            set => __pbn__guard_data = value;
        }
        public bool ShouldSerializeguard_data() => __pbn__guard_data != null;
        public void Resetguard_data() => __pbn__guard_data = null;
        private string __pbn__guard_data;

        [global::ProtoBuf.ProtoMember(11)]
        public uint language
        {
            get => __pbn__language.GetValueOrDefault();
            set => __pbn__language = value;
        }
        public bool ShouldSerializelanguage() => __pbn__language != null;
        public void Resetlanguage() => __pbn__language = null;
        private uint? __pbn__language;

        [global::ProtoBuf.ProtoMember(12)]
        [global::System.ComponentModel.DefaultValue(2)]
        public int qos_level
        {
            get => __pbn__qos_level ?? 2;
            set => __pbn__qos_level = value;
        }
        public bool ShouldSerializeqos_level() => __pbn__qos_level != null;
        public void Resetqos_level() => __pbn__qos_level = null;
        private int? __pbn__qos_level;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_BeginAuthSessionViaCredentials_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong client_id
        {
            get => __pbn__client_id.GetValueOrDefault();
            set => __pbn__client_id = value;
        }
        public bool ShouldSerializeclient_id() => __pbn__client_id != null;
        public void Resetclient_id() => __pbn__client_id = null;
        private ulong? __pbn__client_id;

        [global::ProtoBuf.ProtoMember(2)]
        public byte[] request_id
        {
            get => __pbn__request_id;
            set => __pbn__request_id = value;
        }
        public bool ShouldSerializerequest_id() => __pbn__request_id != null;
        public void Resetrequest_id() => __pbn__request_id = null;
        private byte[] __pbn__request_id;

        [global::ProtoBuf.ProtoMember(3)]
        public float interval
        {
            get => __pbn__interval.GetValueOrDefault();
            set => __pbn__interval = value;
        }
        public bool ShouldSerializeinterval() => __pbn__interval != null;
        public void Resetinterval() => __pbn__interval = null;
        private float? __pbn__interval;

        [global::ProtoBuf.ProtoMember(4)]
        public global::System.Collections.Generic.List<CAuthentication_AllowedConfirmation> allowed_confirmations { get; } = new global::System.Collections.Generic.List<CAuthentication_AllowedConfirmation>();

        [global::ProtoBuf.ProtoMember(5)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string weak_token
        {
            get => __pbn__weak_token ?? "";
            set => __pbn__weak_token = value;
        }
        public bool ShouldSerializeweak_token() => __pbn__weak_token != null;
        public void Resetweak_token() => __pbn__weak_token = null;
        private string __pbn__weak_token;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string agreement_session_url
        {
            get => __pbn__agreement_session_url ?? "";
            set => __pbn__agreement_session_url = value;
        }
        public bool ShouldSerializeagreement_session_url() => __pbn__agreement_session_url != null;
        public void Resetagreement_session_url() => __pbn__agreement_session_url = null;
        private string __pbn__agreement_session_url;

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue("")]
        public string extended_error_message
        {
            get => __pbn__extended_error_message ?? "";
            set => __pbn__extended_error_message = value;
        }
        public bool ShouldSerializeextended_error_message() => __pbn__extended_error_message != null;
        public void Resetextended_error_message() => __pbn__extended_error_message = null;
        private string __pbn__extended_error_message;

    }


    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_BeginAuthSessionViaQR_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string device_friendly_name
        {
            get => __pbn__device_friendly_name ?? "";
            set => __pbn__device_friendly_name = value;
        }
        public bool ShouldSerializedevice_friendly_name() => __pbn__device_friendly_name != null;
        public void Resetdevice_friendly_name() => __pbn__device_friendly_name = null;
        private string __pbn__device_friendly_name;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue(EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown)]
        public EAuthTokenPlatformType platform_type
        {
            get => __pbn__platform_type ?? EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown;
            set => __pbn__platform_type = value;
        }
        public bool ShouldSerializeplatform_type() => __pbn__platform_type != null;
        public void Resetplatform_type() => __pbn__platform_type = null;
        private EAuthTokenPlatformType? __pbn__platform_type;

        [global::ProtoBuf.ProtoMember(3)]
        public CAuthentication_DeviceDetails device_details { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue(@"Unknown")]
        public string website_id
        {
            get => __pbn__website_id ?? @"Unknown";
            set => __pbn__website_id = value;
        }
        public bool ShouldSerializewebsite_id() => __pbn__website_id != null;
        public void Resetwebsite_id() => __pbn__website_id = null;
        private string __pbn__website_id;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_BeginAuthSessionViaQR_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong client_id
        {
            get => __pbn__client_id.GetValueOrDefault();
            set => __pbn__client_id = value;
        }
        public bool ShouldSerializeclient_id() => __pbn__client_id != null;
        public void Resetclient_id() => __pbn__client_id = null;
        private ulong? __pbn__client_id;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string challenge_url
        {
            get => __pbn__challenge_url ?? "";
            set => __pbn__challenge_url = value;
        }
        public bool ShouldSerializechallenge_url() => __pbn__challenge_url != null;
        public void Resetchallenge_url() => __pbn__challenge_url = null;
        private string __pbn__challenge_url;

        [global::ProtoBuf.ProtoMember(3)]
        public byte[] request_id
        {
            get => __pbn__request_id;
            set => __pbn__request_id = value;
        }
        public bool ShouldSerializerequest_id() => __pbn__request_id != null;
        public void Resetrequest_id() => __pbn__request_id = null;
        private byte[] __pbn__request_id;

        [global::ProtoBuf.ProtoMember(4)]
        public float interval
        {
            get => __pbn__interval.GetValueOrDefault();
            set => __pbn__interval = value;
        }
        public bool ShouldSerializeinterval() => __pbn__interval != null;
        public void Resetinterval() => __pbn__interval = null;
        private float? __pbn__interval;

        [global::ProtoBuf.ProtoMember(5)]
        public global::System.Collections.Generic.List<CAuthentication_AllowedConfirmation> allowed_confirmations { get; } = new global::System.Collections.Generic.List<CAuthentication_AllowedConfirmation>();

        [global::ProtoBuf.ProtoMember(6)]
        public int version
        {
            get => __pbn__version.GetValueOrDefault();
            set => __pbn__version = value;
        }
        public bool ShouldSerializeversion() => __pbn__version != null;
        public void Resetversion() => __pbn__version = null;
        private int? __pbn__version;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_DeviceDetails : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string device_friendly_name
        {
            get => __pbn__device_friendly_name ?? "";
            set => __pbn__device_friendly_name = value;
        }
        public bool ShouldSerializedevice_friendly_name() => __pbn__device_friendly_name != null;
        public void Resetdevice_friendly_name() => __pbn__device_friendly_name = null;
        private string __pbn__device_friendly_name;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue(EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown)]
        public EAuthTokenPlatformType platform_type
        {
            get => __pbn__platform_type ?? EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown;
            set => __pbn__platform_type = value;
        }
        public bool ShouldSerializeplatform_type() => __pbn__platform_type != null;
        public void Resetplatform_type() => __pbn__platform_type = null;
        private EAuthTokenPlatformType? __pbn__platform_type;

        [global::ProtoBuf.ProtoMember(3)]
        public int os_type
        {
            get => __pbn__os_type.GetValueOrDefault();
            set => __pbn__os_type = value;
        }
        public bool ShouldSerializeos_type() => __pbn__os_type != null;
        public void Resetos_type() => __pbn__os_type = null;
        private int? __pbn__os_type;

        [global::ProtoBuf.ProtoMember(4)]
        public uint gaming_device_type
        {
            get => __pbn__gaming_device_type.GetValueOrDefault();
            set => __pbn__gaming_device_type = value;
        }
        public bool ShouldSerializegaming_device_type() => __pbn__gaming_device_type != null;
        public void Resetgaming_device_type() => __pbn__gaming_device_type = null;
        private uint? __pbn__gaming_device_type;

        [global::ProtoBuf.ProtoMember(5)]
        public uint client_count
        {
            get => __pbn__client_count.GetValueOrDefault();
            set => __pbn__client_count = value;
        }
        public bool ShouldSerializeclient_count() => __pbn__client_count != null;
        public void Resetclient_count() => __pbn__client_count = null;
        private uint? __pbn__client_count;

        [global::ProtoBuf.ProtoMember(6)]
        public byte[] machine_id
        {
            get => __pbn__machine_id;
            set => __pbn__machine_id = value;
        }
        public bool ShouldSerializemachine_id() => __pbn__machine_id != null;
        public void Resetmachine_id() => __pbn__machine_id = null;
        private byte[] __pbn__machine_id;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue(EAuthTokenAppType.k_EAuthTokenAppType_Unknown)]
        public EAuthTokenAppType app_type
        {
            get => __pbn__app_type ?? EAuthTokenAppType.k_EAuthTokenAppType_Unknown;
            set => __pbn__app_type = value;
        }
        public bool ShouldSerializeapp_type() => __pbn__app_type != null;
        public void Resetapp_type() => __pbn__app_type = null;
        private EAuthTokenAppType? __pbn__app_type;

    }


    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_AllowedConfirmation : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue(EAuthSessionGuardType.k_EAuthSessionGuardType_Unknown)]
        public EAuthSessionGuardType confirmation_type
        {
            get => __pbn__confirmation_type ?? EAuthSessionGuardType.k_EAuthSessionGuardType_Unknown;
            set => __pbn__confirmation_type = value;
        }
        public bool ShouldSerializeconfirmation_type() => __pbn__confirmation_type != null;
        public void Resetconfirmation_type() => __pbn__confirmation_type = null;
        private EAuthSessionGuardType? __pbn__confirmation_type;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string associated_message
        {
            get => __pbn__associated_message ?? "";
            set => __pbn__associated_message = value;
        }
        public bool ShouldSerializeassociated_message() => __pbn__associated_message != null;
        public void Resetassociated_message() => __pbn__associated_message = null;
        private string __pbn__associated_message;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_UpdateAuthSessionWithSteamGuardCode_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong client_id
        {
            get => __pbn__client_id.GetValueOrDefault();
            set => __pbn__client_id = value;
        }
        public bool ShouldSerializeclient_id() => __pbn__client_id != null;
        public void Resetclient_id() => __pbn__client_id = null;
        private ulong? __pbn__client_id;

        [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string code
        {
            get => __pbn__code ?? "";
            set => __pbn__code = value;
        }
        public bool ShouldSerializecode() => __pbn__code != null;
        public void Resetcode() => __pbn__code = null;
        private string __pbn__code;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue(EAuthSessionGuardType.k_EAuthSessionGuardType_Unknown)]
        public EAuthSessionGuardType code_type
        {
            get => __pbn__code_type ?? EAuthSessionGuardType.k_EAuthSessionGuardType_Unknown;
            set => __pbn__code_type = value;
        }
        public bool ShouldSerializecode_type() => __pbn__code_type != null;
        public void Resetcode_type() => __pbn__code_type = null;
        private EAuthSessionGuardType? __pbn__code_type;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_UpdateAuthSessionWithSteamGuardCode_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string agreement_session_url
        {
            get => __pbn__agreement_session_url ?? "";
            set => __pbn__agreement_session_url = value;
        }
        public bool ShouldSerializeagreement_session_url() => __pbn__agreement_session_url != null;
        public void Resetagreement_session_url() => __pbn__agreement_session_url = null;
        private string __pbn__agreement_session_url;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_UpdateAuthSessionWithMobileConfirmation_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public int version
        {
            get => __pbn__version.GetValueOrDefault();
            set => __pbn__version = value;
        }
        public bool ShouldSerializeversion() => __pbn__version != null;
        public void Resetversion() => __pbn__version = null;
        private int? __pbn__version;

        [global::ProtoBuf.ProtoMember(2)]
        public ulong client_id
        {
            get => __pbn__client_id.GetValueOrDefault();
            set => __pbn__client_id = value;
        }
        public bool ShouldSerializeclient_id() => __pbn__client_id != null;
        public void Resetclient_id() => __pbn__client_id = null;
        private ulong? __pbn__client_id;

        [global::ProtoBuf.ProtoMember(3, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(4)]
        public byte[] signature
        {
            get => __pbn__signature;
            set => __pbn__signature = value;
        }
        public bool ShouldSerializesignature() => __pbn__signature != null;
        public void Resetsignature() => __pbn__signature = null;
        private byte[] __pbn__signature;

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue(false)]
        public bool confirm
        {
            get => __pbn__confirm ?? false;
            set => __pbn__confirm = value;
        }
        public bool ShouldSerializeconfirm() => __pbn__confirm != null;
        public void Resetconfirm() => __pbn__confirm = null;
        private bool? __pbn__confirm;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue(ESessionPersistence.k_ESessionPersistence_Persistent)]
        public ESessionPersistence persistence
        {
            get => __pbn__persistence ?? ESessionPersistence.k_ESessionPersistence_Persistent;
            set => __pbn__persistence = value;
        }
        public bool ShouldSerializepersistence() => __pbn__persistence != null;
        public void Resetpersistence() => __pbn__persistence = null;
        private ESessionPersistence? __pbn__persistence;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_UpdateAuthSessionWithMobileConfirmation_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_PollAuthSessionStatus_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong client_id
        {
            get => __pbn__client_id.GetValueOrDefault();
            set => __pbn__client_id = value;
        }
        public bool ShouldSerializeclient_id() => __pbn__client_id != null;
        public void Resetclient_id() => __pbn__client_id = null;
        private ulong? __pbn__client_id;

        [global::ProtoBuf.ProtoMember(2)]
        public byte[] request_id
        {
            get => __pbn__request_id;
            set => __pbn__request_id = value;
        }
        public bool ShouldSerializerequest_id() => __pbn__request_id != null;
        public void Resetrequest_id() => __pbn__request_id = null;
        private byte[] __pbn__request_id;

        [global::ProtoBuf.ProtoMember(3, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong token_to_revoke
        {
            get => __pbn__token_to_revoke.GetValueOrDefault();
            set => __pbn__token_to_revoke = value;
        }
        public bool ShouldSerializetoken_to_revoke() => __pbn__token_to_revoke != null;
        public void Resettoken_to_revoke() => __pbn__token_to_revoke = null;
        private ulong? __pbn__token_to_revoke;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_PollAuthSessionStatus_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong new_client_id
        {
            get => __pbn__new_client_id.GetValueOrDefault();
            set => __pbn__new_client_id = value;
        }
        public bool ShouldSerializenew_client_id() => __pbn__new_client_id != null;
        public void Resetnew_client_id() => __pbn__new_client_id = null;
        private ulong? __pbn__new_client_id;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string new_challenge_url
        {
            get => __pbn__new_challenge_url ?? "";
            set => __pbn__new_challenge_url = value;
        }
        public bool ShouldSerializenew_challenge_url() => __pbn__new_challenge_url != null;
        public void Resetnew_challenge_url() => __pbn__new_challenge_url = null;
        private string __pbn__new_challenge_url;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string refresh_token
        {
            get => __pbn__refresh_token ?? "";
            set => __pbn__refresh_token = value;
        }
        public bool ShouldSerializerefresh_token() => __pbn__refresh_token != null;
        public void Resetrefresh_token() => __pbn__refresh_token = null;
        private string __pbn__refresh_token;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string access_token
        {
            get => __pbn__access_token ?? "";
            set => __pbn__access_token = value;
        }
        public bool ShouldSerializeaccess_token() => __pbn__access_token != null;
        public void Resetaccess_token() => __pbn__access_token = null;
        private string __pbn__access_token;

        [global::ProtoBuf.ProtoMember(5)]
        public bool had_remote_interaction
        {
            get => __pbn__had_remote_interaction.GetValueOrDefault();
            set => __pbn__had_remote_interaction = value;
        }
        public bool ShouldSerializehad_remote_interaction() => __pbn__had_remote_interaction != null;
        public void Resethad_remote_interaction() => __pbn__had_remote_interaction = null;
        private bool? __pbn__had_remote_interaction;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string account_name
        {
            get => __pbn__account_name ?? "";
            set => __pbn__account_name = value;
        }
        public bool ShouldSerializeaccount_name() => __pbn__account_name != null;
        public void Resetaccount_name() => __pbn__account_name = null;
        private string __pbn__account_name;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string new_guard_data
        {
            get => __pbn__new_guard_data ?? "";
            set => __pbn__new_guard_data = value;
        }
        public bool ShouldSerializenew_guard_data() => __pbn__new_guard_data != null;
        public void Resetnew_guard_data() => __pbn__new_guard_data = null;
        private string __pbn__new_guard_data;

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue("")]
        public string agreement_session_url
        {
            get => __pbn__agreement_session_url ?? "";
            set => __pbn__agreement_session_url = value;
        }
        public bool ShouldSerializeagreement_session_url() => __pbn__agreement_session_url != null;
        public void Resetagreement_session_url() => __pbn__agreement_session_url = null;
        private string __pbn__agreement_session_url;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_GetAuthSessionsForAccount_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_GetAuthSessionsForAccount_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<ulong> client_ids { get; } = new global::System.Collections.Generic.List<ulong>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_GetAuthSessionInfo_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong client_id
        {
            get => __pbn__client_id.GetValueOrDefault();
            set => __pbn__client_id = value;
        }
        public bool ShouldSerializeclient_id() => __pbn__client_id != null;
        public void Resetclient_id() => __pbn__client_id = null;
        private ulong? __pbn__client_id;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_GetAuthSessionInfo_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string ip
        {
            get => __pbn__ip ?? "";
            set => __pbn__ip = value;
        }
        public bool ShouldSerializeip() => __pbn__ip != null;
        public void Resetip() => __pbn__ip = null;
        private string __pbn__ip;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string geoloc
        {
            get => __pbn__geoloc ?? "";
            set => __pbn__geoloc = value;
        }
        public bool ShouldSerializegeoloc() => __pbn__geoloc != null;
        public void Resetgeoloc() => __pbn__geoloc = null;
        private string __pbn__geoloc;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string city
        {
            get => __pbn__city ?? "";
            set => __pbn__city = value;
        }
        public bool ShouldSerializecity() => __pbn__city != null;
        public void Resetcity() => __pbn__city = null;
        private string __pbn__city;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string state
        {
            get => __pbn__state ?? "";
            set => __pbn__state = value;
        }
        public bool ShouldSerializestate() => __pbn__state != null;
        public void Resetstate() => __pbn__state = null;
        private string __pbn__state;

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue("")]
        public string country
        {
            get => __pbn__country ?? "";
            set => __pbn__country = value;
        }
        public bool ShouldSerializecountry() => __pbn__country != null;
        public void Resetcountry() => __pbn__country = null;
        private string __pbn__country;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue(EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown)]
        public EAuthTokenPlatformType platform_type
        {
            get => __pbn__platform_type ?? EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown;
            set => __pbn__platform_type = value;
        }
        public bool ShouldSerializeplatform_type() => __pbn__platform_type != null;
        public void Resetplatform_type() => __pbn__platform_type = null;
        private EAuthTokenPlatformType? __pbn__platform_type;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string device_friendly_name
        {
            get => __pbn__device_friendly_name ?? "";
            set => __pbn__device_friendly_name = value;
        }
        public bool ShouldSerializedevice_friendly_name() => __pbn__device_friendly_name != null;
        public void Resetdevice_friendly_name() => __pbn__device_friendly_name = null;
        private string __pbn__device_friendly_name;

        [global::ProtoBuf.ProtoMember(8)]
        public int version
        {
            get => __pbn__version.GetValueOrDefault();
            set => __pbn__version = value;
        }
        public bool ShouldSerializeversion() => __pbn__version != null;
        public void Resetversion() => __pbn__version = null;
        private int? __pbn__version;

        [global::ProtoBuf.ProtoMember(9)]
        [global::System.ComponentModel.DefaultValue(EAuthSessionSecurityHistory.k_EAuthSessionSecurityHistory_Invalid)]
        public EAuthSessionSecurityHistory login_history
        {
            get => __pbn__login_history ?? EAuthSessionSecurityHistory.k_EAuthSessionSecurityHistory_Invalid;
            set => __pbn__login_history = value;
        }
        public bool ShouldSerializelogin_history() => __pbn__login_history != null;
        public void Resetlogin_history() => __pbn__login_history = null;
        private EAuthSessionSecurityHistory? __pbn__login_history;

        [global::ProtoBuf.ProtoMember(10)]
        public bool requestor_location_mismatch
        {
            get => __pbn__requestor_location_mismatch.GetValueOrDefault();
            set => __pbn__requestor_location_mismatch = value;
        }
        public bool ShouldSerializerequestor_location_mismatch() => __pbn__requestor_location_mismatch != null;
        public void Resetrequestor_location_mismatch() => __pbn__requestor_location_mismatch = null;
        private bool? __pbn__requestor_location_mismatch;

        [global::ProtoBuf.ProtoMember(11)]
        public bool high_usage_login
        {
            get => __pbn__high_usage_login.GetValueOrDefault();
            set => __pbn__high_usage_login = value;
        }
        public bool ShouldSerializehigh_usage_login() => __pbn__high_usage_login != null;
        public void Resethigh_usage_login() => __pbn__high_usage_login = null;
        private bool? __pbn__high_usage_login;

        [global::ProtoBuf.ProtoMember(12)]
        [global::System.ComponentModel.DefaultValue(ESessionPersistence.k_ESessionPersistence_Invalid)]
        public ESessionPersistence requested_persistence
        {
            get => __pbn__requested_persistence ?? ESessionPersistence.k_ESessionPersistence_Invalid;
            set => __pbn__requested_persistence = value;
        }
        public bool ShouldSerializerequested_persistence() => __pbn__requested_persistence != null;
        public void Resetrequested_persistence() => __pbn__requested_persistence = null;
        private ESessionPersistence? __pbn__requested_persistence;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgIPAddress : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public uint v4
        {
            get => __pbn__ip.Is(1) ? __pbn__ip.UInt32 : default(uint);
            set => __pbn__ip = new global::ProtoBuf.DiscriminatedUnion32Object(1, value);
        }
        public bool ShouldSerializev4() => __pbn__ip.Is(1);
        public void Resetv4() => global::ProtoBuf.DiscriminatedUnion32Object.Reset(ref __pbn__ip, 1);

        private global::ProtoBuf.DiscriminatedUnion32Object __pbn__ip;

        [global::ProtoBuf.ProtoMember(2)]
        public byte[] v6
        {
            get => __pbn__ip.Is(2) ? ((byte[])__pbn__ip.Object) : default(byte[]);
            set => __pbn__ip = new global::ProtoBuf.DiscriminatedUnion32Object(2, value);
        }
        public bool ShouldSerializev6() => __pbn__ip.Is(2);
        public void Resetv6() => global::ProtoBuf.DiscriminatedUnion32Object.Reset(ref __pbn__ip, 2);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgIPAddressBucket : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public CMsgIPAddress original_ip_address { get; set; }

        [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong bucket
        {
            get => __pbn__bucket.GetValueOrDefault();
            set => __pbn__bucket = value;
        }
        public bool ShouldSerializebucket() => __pbn__bucket != null;
        public void Resetbucket() => __pbn__bucket = null;
        private ulong? __pbn__bucket;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgGCRoutingProtoBufHeader : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong dst_gcid_queue
        {
            get => __pbn__dst_gcid_queue.GetValueOrDefault();
            set => __pbn__dst_gcid_queue = value;
        }
        public bool ShouldSerializedst_gcid_queue() => __pbn__dst_gcid_queue != null;
        public void Resetdst_gcid_queue() => __pbn__dst_gcid_queue = null;
        private ulong? __pbn__dst_gcid_queue;

        [global::ProtoBuf.ProtoMember(2)]
        public uint dst_gc_dir_index
        {
            get => __pbn__dst_gc_dir_index.GetValueOrDefault();
            set => __pbn__dst_gc_dir_index = value;
        }
        public bool ShouldSerializedst_gc_dir_index() => __pbn__dst_gc_dir_index != null;
        public void Resetdst_gc_dir_index() => __pbn__dst_gc_dir_index = null;
        private uint? __pbn__dst_gc_dir_index;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgMulti : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint size_unzipped
        {
            get => __pbn__size_unzipped.GetValueOrDefault();
            set => __pbn__size_unzipped = value;
        }
        public bool ShouldSerializesize_unzipped() => __pbn__size_unzipped != null;
        public void Resetsize_unzipped() => __pbn__size_unzipped = null;
        private uint? __pbn__size_unzipped;

        [global::ProtoBuf.ProtoMember(2)]
        public byte[] message_body
        {
            get => __pbn__message_body;
            set => __pbn__message_body = value;
        }
        public bool ShouldSerializemessage_body() => __pbn__message_body != null;
        public void Resetmessage_body() => __pbn__message_body = null;
        private byte[] __pbn__message_body;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgProtobufWrapped : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public byte[] message_body
        {
            get => __pbn__message_body;
            set => __pbn__message_body = value;
        }
        public bool ShouldSerializemessage_body() => __pbn__message_body != null;
        public void Resetmessage_body() => __pbn__message_body = null;
        private byte[] __pbn__message_body;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgAuthTicket : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint estate
        {
            get => __pbn__estate.GetValueOrDefault();
            set => __pbn__estate = value;
        }
        public bool ShouldSerializeestate() => __pbn__estate != null;
        public void Resetestate() => __pbn__estate = null;
        private uint? __pbn__estate;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue(2u)]
        public uint eresult
        {
            get => __pbn__eresult ?? 2u;
            set => __pbn__eresult = value;
        }
        public bool ShouldSerializeeresult() => __pbn__eresult != null;
        public void Reseteresult() => __pbn__eresult = null;
        private uint? __pbn__eresult;

        [global::ProtoBuf.ProtoMember(3, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(4, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong gameid
        {
            get => __pbn__gameid.GetValueOrDefault();
            set => __pbn__gameid = value;
        }
        public bool ShouldSerializegameid() => __pbn__gameid != null;
        public void Resetgameid() => __pbn__gameid = null;
        private ulong? __pbn__gameid;

        [global::ProtoBuf.ProtoMember(5)]
        public uint h_steam_pipe
        {
            get => __pbn__h_steam_pipe.GetValueOrDefault();
            set => __pbn__h_steam_pipe = value;
        }
        public bool ShouldSerializeh_steam_pipe() => __pbn__h_steam_pipe != null;
        public void Reseth_steam_pipe() => __pbn__h_steam_pipe = null;
        private uint? __pbn__h_steam_pipe;

        [global::ProtoBuf.ProtoMember(6)]
        public uint ticket_crc
        {
            get => __pbn__ticket_crc.GetValueOrDefault();
            set => __pbn__ticket_crc = value;
        }
        public bool ShouldSerializeticket_crc() => __pbn__ticket_crc != null;
        public void Resetticket_crc() => __pbn__ticket_crc = null;
        private uint? __pbn__ticket_crc;

        [global::ProtoBuf.ProtoMember(7)]
        public byte[] ticket
        {
            get => __pbn__ticket;
            set => __pbn__ticket = value;
        }
        public bool ShouldSerializeticket() => __pbn__ticket != null;
        public void Resetticket() => __pbn__ticket = null;
        private byte[] __pbn__ticket;

        [global::ProtoBuf.ProtoMember(8)]
        public byte[] server_secret
        {
            get => __pbn__server_secret;
            set => __pbn__server_secret = value;
        }
        public bool ShouldSerializeserver_secret() => __pbn__server_secret != null;
        public void Resetserver_secret() => __pbn__server_secret = null;
        private byte[] __pbn__server_secret;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CCDDBAppDetailCommon : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string name
        {
            get => __pbn__name ?? "";
            set => __pbn__name = value;
        }
        public bool ShouldSerializename() => __pbn__name != null;
        public void Resetname() => __pbn__name = null;
        private string __pbn__name;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string icon
        {
            get => __pbn__icon ?? "";
            set => __pbn__icon = value;
        }
        public bool ShouldSerializeicon() => __pbn__icon != null;
        public void Reseticon() => __pbn__icon = null;
        private string __pbn__icon;

        [global::ProtoBuf.ProtoMember(6)]
        public bool tool
        {
            get => __pbn__tool.GetValueOrDefault();
            set => __pbn__tool = value;
        }
        public bool ShouldSerializetool() => __pbn__tool != null;
        public void Resettool() => __pbn__tool = null;
        private bool? __pbn__tool;

        [global::ProtoBuf.ProtoMember(7)]
        public bool demo
        {
            get => __pbn__demo.GetValueOrDefault();
            set => __pbn__demo = value;
        }
        public bool ShouldSerializedemo() => __pbn__demo != null;
        public void Resetdemo() => __pbn__demo = null;
        private bool? __pbn__demo;

        [global::ProtoBuf.ProtoMember(8)]
        public bool media
        {
            get => __pbn__media.GetValueOrDefault();
            set => __pbn__media = value;
        }
        public bool ShouldSerializemedia() => __pbn__media != null;
        public void Resetmedia() => __pbn__media = null;
        private bool? __pbn__media;

        [global::ProtoBuf.ProtoMember(9)]
        public bool community_visible_stats
        {
            get => __pbn__community_visible_stats.GetValueOrDefault();
            set => __pbn__community_visible_stats = value;
        }
        public bool ShouldSerializecommunity_visible_stats() => __pbn__community_visible_stats != null;
        public void Resetcommunity_visible_stats() => __pbn__community_visible_stats = null;
        private bool? __pbn__community_visible_stats;

        [global::ProtoBuf.ProtoMember(10)]
        [global::System.ComponentModel.DefaultValue("")]
        public string friendly_name
        {
            get => __pbn__friendly_name ?? "";
            set => __pbn__friendly_name = value;
        }
        public bool ShouldSerializefriendly_name() => __pbn__friendly_name != null;
        public void Resetfriendly_name() => __pbn__friendly_name = null;
        private string __pbn__friendly_name;

        [global::ProtoBuf.ProtoMember(11)]
        [global::System.ComponentModel.DefaultValue("")]
        public string propagation
        {
            get => __pbn__propagation ?? "";
            set => __pbn__propagation = value;
        }
        public bool ShouldSerializepropagation() => __pbn__propagation != null;
        public void Resetpropagation() => __pbn__propagation = null;
        private string __pbn__propagation;

        [global::ProtoBuf.ProtoMember(12)]
        public bool has_adult_content
        {
            get => __pbn__has_adult_content.GetValueOrDefault();
            set => __pbn__has_adult_content = value;
        }
        public bool ShouldSerializehas_adult_content() => __pbn__has_adult_content != null;
        public void Resethas_adult_content() => __pbn__has_adult_content = null;
        private bool? __pbn__has_adult_content;

        [global::ProtoBuf.ProtoMember(13)]
        public bool is_visible_in_steam_china
        {
            get => __pbn__is_visible_in_steam_china.GetValueOrDefault();
            set => __pbn__is_visible_in_steam_china = value;
        }
        public bool ShouldSerializeis_visible_in_steam_china() => __pbn__is_visible_in_steam_china != null;
        public void Resetis_visible_in_steam_china() => __pbn__is_visible_in_steam_china = null;
        private bool? __pbn__is_visible_in_steam_china;

        [global::ProtoBuf.ProtoMember(14)]
        public uint app_type
        {
            get => __pbn__app_type.GetValueOrDefault();
            set => __pbn__app_type = value;
        }
        public bool ShouldSerializeapp_type() => __pbn__app_type != null;
        public void Resetapp_type() => __pbn__app_type = null;
        private uint? __pbn__app_type;

        [global::ProtoBuf.ProtoMember(15)]
        public bool has_adult_content_sex
        {
            get => __pbn__has_adult_content_sex.GetValueOrDefault();
            set => __pbn__has_adult_content_sex = value;
        }
        public bool ShouldSerializehas_adult_content_sex() => __pbn__has_adult_content_sex != null;
        public void Resethas_adult_content_sex() => __pbn__has_adult_content_sex = null;
        private bool? __pbn__has_adult_content_sex;

        [global::ProtoBuf.ProtoMember(16)]
        public bool has_adult_content_violence
        {
            get => __pbn__has_adult_content_violence.GetValueOrDefault();
            set => __pbn__has_adult_content_violence = value;
        }
        public bool ShouldSerializehas_adult_content_violence() => __pbn__has_adult_content_violence != null;
        public void Resethas_adult_content_violence() => __pbn__has_adult_content_violence = null;
        private bool? __pbn__has_adult_content_violence;

        [global::ProtoBuf.ProtoMember(17)]
        public global::System.Collections.Generic.List<uint> content_descriptorids { get; } = new global::System.Collections.Generic.List<uint>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgAppRights : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool edit_info
        {
            get => __pbn__edit_info.GetValueOrDefault();
            set => __pbn__edit_info = value;
        }
        public bool ShouldSerializeedit_info() => __pbn__edit_info != null;
        public void Resetedit_info() => __pbn__edit_info = null;
        private bool? __pbn__edit_info;

        [global::ProtoBuf.ProtoMember(2)]
        public bool publish
        {
            get => __pbn__publish.GetValueOrDefault();
            set => __pbn__publish = value;
        }
        public bool ShouldSerializepublish() => __pbn__publish != null;
        public void Resetpublish() => __pbn__publish = null;
        private bool? __pbn__publish;

        [global::ProtoBuf.ProtoMember(3)]
        public bool view_error_data
        {
            get => __pbn__view_error_data.GetValueOrDefault();
            set => __pbn__view_error_data = value;
        }
        public bool ShouldSerializeview_error_data() => __pbn__view_error_data != null;
        public void Resetview_error_data() => __pbn__view_error_data = null;
        private bool? __pbn__view_error_data;

        [global::ProtoBuf.ProtoMember(4)]
        public bool download
        {
            get => __pbn__download.GetValueOrDefault();
            set => __pbn__download = value;
        }
        public bool ShouldSerializedownload() => __pbn__download != null;
        public void Resetdownload() => __pbn__download = null;
        private bool? __pbn__download;

        [global::ProtoBuf.ProtoMember(5)]
        public bool upload_cdkeys
        {
            get => __pbn__upload_cdkeys.GetValueOrDefault();
            set => __pbn__upload_cdkeys = value;
        }
        public bool ShouldSerializeupload_cdkeys() => __pbn__upload_cdkeys != null;
        public void Resetupload_cdkeys() => __pbn__upload_cdkeys = null;
        private bool? __pbn__upload_cdkeys;

        [global::ProtoBuf.ProtoMember(6)]
        public bool generate_cdkeys
        {
            get => __pbn__generate_cdkeys.GetValueOrDefault();
            set => __pbn__generate_cdkeys = value;
        }
        public bool ShouldSerializegenerate_cdkeys() => __pbn__generate_cdkeys != null;
        public void Resetgenerate_cdkeys() => __pbn__generate_cdkeys = null;
        private bool? __pbn__generate_cdkeys;

        [global::ProtoBuf.ProtoMember(7)]
        public bool view_financials
        {
            get => __pbn__view_financials.GetValueOrDefault();
            set => __pbn__view_financials = value;
        }
        public bool ShouldSerializeview_financials() => __pbn__view_financials != null;
        public void Resetview_financials() => __pbn__view_financials = null;
        private bool? __pbn__view_financials;

        [global::ProtoBuf.ProtoMember(8)]
        public bool manage_ceg
        {
            get => __pbn__manage_ceg.GetValueOrDefault();
            set => __pbn__manage_ceg = value;
        }
        public bool ShouldSerializemanage_ceg() => __pbn__manage_ceg != null;
        public void Resetmanage_ceg() => __pbn__manage_ceg = null;
        private bool? __pbn__manage_ceg;

        [global::ProtoBuf.ProtoMember(9)]
        public bool manage_signing
        {
            get => __pbn__manage_signing.GetValueOrDefault();
            set => __pbn__manage_signing = value;
        }
        public bool ShouldSerializemanage_signing() => __pbn__manage_signing != null;
        public void Resetmanage_signing() => __pbn__manage_signing = null;
        private bool? __pbn__manage_signing;

        [global::ProtoBuf.ProtoMember(10)]
        public bool manage_cdkeys
        {
            get => __pbn__manage_cdkeys.GetValueOrDefault();
            set => __pbn__manage_cdkeys = value;
        }
        public bool ShouldSerializemanage_cdkeys() => __pbn__manage_cdkeys != null;
        public void Resetmanage_cdkeys() => __pbn__manage_cdkeys = null;
        private bool? __pbn__manage_cdkeys;

        [global::ProtoBuf.ProtoMember(11)]
        public bool edit_marketing
        {
            get => __pbn__edit_marketing.GetValueOrDefault();
            set => __pbn__edit_marketing = value;
        }
        public bool ShouldSerializeedit_marketing() => __pbn__edit_marketing != null;
        public void Resetedit_marketing() => __pbn__edit_marketing = null;
        private bool? __pbn__edit_marketing;

        [global::ProtoBuf.ProtoMember(12)]
        public bool economy_support
        {
            get => __pbn__economy_support.GetValueOrDefault();
            set => __pbn__economy_support = value;
        }
        public bool ShouldSerializeeconomy_support() => __pbn__economy_support != null;
        public void Reseteconomy_support() => __pbn__economy_support = null;
        private bool? __pbn__economy_support;

        [global::ProtoBuf.ProtoMember(13)]
        public bool economy_support_supervisor
        {
            get => __pbn__economy_support_supervisor.GetValueOrDefault();
            set => __pbn__economy_support_supervisor = value;
        }
        public bool ShouldSerializeeconomy_support_supervisor() => __pbn__economy_support_supervisor != null;
        public void Reseteconomy_support_supervisor() => __pbn__economy_support_supervisor = null;
        private bool? __pbn__economy_support_supervisor;

        [global::ProtoBuf.ProtoMember(14)]
        public bool manage_pricing
        {
            get => __pbn__manage_pricing.GetValueOrDefault();
            set => __pbn__manage_pricing = value;
        }
        public bool ShouldSerializemanage_pricing() => __pbn__manage_pricing != null;
        public void Resetmanage_pricing() => __pbn__manage_pricing = null;
        private bool? __pbn__manage_pricing;

        [global::ProtoBuf.ProtoMember(15)]
        public bool broadcast_live
        {
            get => __pbn__broadcast_live.GetValueOrDefault();
            set => __pbn__broadcast_live = value;
        }
        public bool ShouldSerializebroadcast_live() => __pbn__broadcast_live != null;
        public void Resetbroadcast_live() => __pbn__broadcast_live = null;
        private bool? __pbn__broadcast_live;

        [global::ProtoBuf.ProtoMember(16)]
        public bool view_marketing_traffic
        {
            get => __pbn__view_marketing_traffic.GetValueOrDefault();
            set => __pbn__view_marketing_traffic = value;
        }
        public bool ShouldSerializeview_marketing_traffic() => __pbn__view_marketing_traffic != null;
        public void Resetview_marketing_traffic() => __pbn__view_marketing_traffic = null;
        private bool? __pbn__view_marketing_traffic;

        [global::ProtoBuf.ProtoMember(17)]
        public bool edit_store_display_content
        {
            get => __pbn__edit_store_display_content.GetValueOrDefault();
            set => __pbn__edit_store_display_content = value;
        }
        public bool ShouldSerializeedit_store_display_content() => __pbn__edit_store_display_content != null;
        public void Resetedit_store_display_content() => __pbn__edit_store_display_content = null;
        private bool? __pbn__edit_store_display_content;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CCuratorPreferences : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint supported_languages
        {
            get => __pbn__supported_languages.GetValueOrDefault();
            set => __pbn__supported_languages = value;
        }
        public bool ShouldSerializesupported_languages() => __pbn__supported_languages != null;
        public void Resetsupported_languages() => __pbn__supported_languages = null;
        private uint? __pbn__supported_languages;

        [global::ProtoBuf.ProtoMember(2)]
        public bool platform_windows
        {
            get => __pbn__platform_windows.GetValueOrDefault();
            set => __pbn__platform_windows = value;
        }
        public bool ShouldSerializeplatform_windows() => __pbn__platform_windows != null;
        public void Resetplatform_windows() => __pbn__platform_windows = null;
        private bool? __pbn__platform_windows;

        [global::ProtoBuf.ProtoMember(3)]
        public bool platform_mac
        {
            get => __pbn__platform_mac.GetValueOrDefault();
            set => __pbn__platform_mac = value;
        }
        public bool ShouldSerializeplatform_mac() => __pbn__platform_mac != null;
        public void Resetplatform_mac() => __pbn__platform_mac = null;
        private bool? __pbn__platform_mac;

        [global::ProtoBuf.ProtoMember(4)]
        public bool platform_linux
        {
            get => __pbn__platform_linux.GetValueOrDefault();
            set => __pbn__platform_linux = value;
        }
        public bool ShouldSerializeplatform_linux() => __pbn__platform_linux != null;
        public void Resetplatform_linux() => __pbn__platform_linux = null;
        private bool? __pbn__platform_linux;

        [global::ProtoBuf.ProtoMember(5)]
        public bool vr_content
        {
            get => __pbn__vr_content.GetValueOrDefault();
            set => __pbn__vr_content = value;
        }
        public bool ShouldSerializevr_content() => __pbn__vr_content != null;
        public void Resetvr_content() => __pbn__vr_content = null;
        private bool? __pbn__vr_content;

        [global::ProtoBuf.ProtoMember(6)]
        public bool adult_content_violence
        {
            get => __pbn__adult_content_violence.GetValueOrDefault();
            set => __pbn__adult_content_violence = value;
        }
        public bool ShouldSerializeadult_content_violence() => __pbn__adult_content_violence != null;
        public void Resetadult_content_violence() => __pbn__adult_content_violence = null;
        private bool? __pbn__adult_content_violence;

        [global::ProtoBuf.ProtoMember(7)]
        public bool adult_content_sex
        {
            get => __pbn__adult_content_sex.GetValueOrDefault();
            set => __pbn__adult_content_sex = value;
        }
        public bool ShouldSerializeadult_content_sex() => __pbn__adult_content_sex != null;
        public void Resetadult_content_sex() => __pbn__adult_content_sex = null;
        private bool? __pbn__adult_content_sex;

        [global::ProtoBuf.ProtoMember(8)]
        public uint timestamp_updated
        {
            get => __pbn__timestamp_updated.GetValueOrDefault();
            set => __pbn__timestamp_updated = value;
        }
        public bool ShouldSerializetimestamp_updated() => __pbn__timestamp_updated != null;
        public void Resettimestamp_updated() => __pbn__timestamp_updated = null;
        private uint? __pbn__timestamp_updated;

        [global::ProtoBuf.ProtoMember(9)]
        public global::System.Collections.Generic.List<uint> tagids_curated { get; } = new global::System.Collections.Generic.List<uint>();

        [global::ProtoBuf.ProtoMember(10)]
        public global::System.Collections.Generic.List<uint> tagids_filtered { get; } = new global::System.Collections.Generic.List<uint>();

        [global::ProtoBuf.ProtoMember(11)]
        [global::System.ComponentModel.DefaultValue("")]
        public string website_title
        {
            get => __pbn__website_title ?? "";
            set => __pbn__website_title = value;
        }
        public bool ShouldSerializewebsite_title() => __pbn__website_title != null;
        public void Resetwebsite_title() => __pbn__website_title = null;
        private string __pbn__website_title;

        [global::ProtoBuf.ProtoMember(12)]
        [global::System.ComponentModel.DefaultValue("")]
        public string website_url
        {
            get => __pbn__website_url ?? "";
            set => __pbn__website_url = value;
        }
        public bool ShouldSerializewebsite_url() => __pbn__website_url != null;
        public void Resetwebsite_url() => __pbn__website_url = null;
        private string __pbn__website_url;

        [global::ProtoBuf.ProtoMember(13)]
        [global::System.ComponentModel.DefaultValue("")]
        public string discussion_url
        {
            get => __pbn__discussion_url ?? "";
            set => __pbn__discussion_url = value;
        }
        public bool ShouldSerializediscussion_url() => __pbn__discussion_url != null;
        public void Resetdiscussion_url() => __pbn__discussion_url = null;
        private string __pbn__discussion_url;

        [global::ProtoBuf.ProtoMember(14)]
        public bool show_broadcast
        {
            get => __pbn__show_broadcast.GetValueOrDefault();
            set => __pbn__show_broadcast = value;
        }
        public bool ShouldSerializeshow_broadcast() => __pbn__show_broadcast != null;
        public void Resetshow_broadcast() => __pbn__show_broadcast = null;
        private bool? __pbn__show_broadcast;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CLocalizationToken : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint language
        {
            get => __pbn__language.GetValueOrDefault();
            set => __pbn__language = value;
        }
        public bool ShouldSerializelanguage() => __pbn__language != null;
        public void Resetlanguage() => __pbn__language = null;
        private uint? __pbn__language;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string localized_string
        {
            get => __pbn__localized_string ?? "";
            set => __pbn__localized_string = value;
        }
        public bool ShouldSerializelocalized_string() => __pbn__localized_string != null;
        public void Resetlocalized_string() => __pbn__localized_string = null;
        private string __pbn__localized_string;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CClanEventUserNewsTuple : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint clanid
        {
            get => __pbn__clanid.GetValueOrDefault();
            set => __pbn__clanid = value;
        }
        public bool ShouldSerializeclanid() => __pbn__clanid != null;
        public void Resetclanid() => __pbn__clanid = null;
        private uint? __pbn__clanid;

        [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong event_gid
        {
            get => __pbn__event_gid.GetValueOrDefault();
            set => __pbn__event_gid = value;
        }
        public bool ShouldSerializeevent_gid() => __pbn__event_gid != null;
        public void Resetevent_gid() => __pbn__event_gid = null;
        private ulong? __pbn__event_gid;

        [global::ProtoBuf.ProtoMember(3, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong announcement_gid
        {
            get => __pbn__announcement_gid.GetValueOrDefault();
            set => __pbn__announcement_gid = value;
        }
        public bool ShouldSerializeannouncement_gid() => __pbn__announcement_gid != null;
        public void Resetannouncement_gid() => __pbn__announcement_gid = null;
        private ulong? __pbn__announcement_gid;

        [global::ProtoBuf.ProtoMember(4)]
        public uint rtime_start
        {
            get => __pbn__rtime_start.GetValueOrDefault();
            set => __pbn__rtime_start = value;
        }
        public bool ShouldSerializertime_start() => __pbn__rtime_start != null;
        public void Resetrtime_start() => __pbn__rtime_start = null;
        private uint? __pbn__rtime_start;

        [global::ProtoBuf.ProtoMember(5)]
        public uint rtime_end
        {
            get => __pbn__rtime_end.GetValueOrDefault();
            set => __pbn__rtime_end = value;
        }
        public bool ShouldSerializertime_end() => __pbn__rtime_end != null;
        public void Resetrtime_end() => __pbn__rtime_end = null;
        private uint? __pbn__rtime_end;

        [global::ProtoBuf.ProtoMember(6)]
        public uint priority_score
        {
            get => __pbn__priority_score.GetValueOrDefault();
            set => __pbn__priority_score = value;
        }
        public bool ShouldSerializepriority_score() => __pbn__priority_score != null;
        public void Resetpriority_score() => __pbn__priority_score = null;
        private uint? __pbn__priority_score;

        [global::ProtoBuf.ProtoMember(7)]
        public uint type
        {
            get => __pbn__type.GetValueOrDefault();
            set => __pbn__type = value;
        }
        public bool ShouldSerializetype() => __pbn__type != null;
        public void Resettype() => __pbn__type = null;
        private uint? __pbn__type;

        [global::ProtoBuf.ProtoMember(8)]
        public uint clamp_range_slot
        {
            get => __pbn__clamp_range_slot.GetValueOrDefault();
            set => __pbn__clamp_range_slot = value;
        }
        public bool ShouldSerializeclamp_range_slot() => __pbn__clamp_range_slot != null;
        public void Resetclamp_range_slot() => __pbn__clamp_range_slot = null;
        private uint? __pbn__clamp_range_slot;

        [global::ProtoBuf.ProtoMember(9)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

        [global::ProtoBuf.ProtoMember(10)]
        public uint rtime32_last_modified
        {
            get => __pbn__rtime32_last_modified.GetValueOrDefault();
            set => __pbn__rtime32_last_modified = value;
        }
        public bool ShouldSerializertime32_last_modified() => __pbn__rtime32_last_modified != null;
        public void Resetrtime32_last_modified() => __pbn__rtime32_last_modified = null;
        private uint? __pbn__rtime32_last_modified;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CClanMatchEventByRange : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint rtime_before
        {
            get => __pbn__rtime_before.GetValueOrDefault();
            set => __pbn__rtime_before = value;
        }
        public bool ShouldSerializertime_before() => __pbn__rtime_before != null;
        public void Resetrtime_before() => __pbn__rtime_before = null;
        private uint? __pbn__rtime_before;

        [global::ProtoBuf.ProtoMember(2)]
        public uint rtime_after
        {
            get => __pbn__rtime_after.GetValueOrDefault();
            set => __pbn__rtime_after = value;
        }
        public bool ShouldSerializertime_after() => __pbn__rtime_after != null;
        public void Resetrtime_after() => __pbn__rtime_after = null;
        private uint? __pbn__rtime_after;

        [global::ProtoBuf.ProtoMember(3)]
        public uint qualified
        {
            get => __pbn__qualified.GetValueOrDefault();
            set => __pbn__qualified = value;
        }
        public bool ShouldSerializequalified() => __pbn__qualified != null;
        public void Resetqualified() => __pbn__qualified = null;
        private uint? __pbn__qualified;

        [global::ProtoBuf.ProtoMember(4)]
        public global::System.Collections.Generic.List<CClanEventUserNewsTuple> events { get; } = new global::System.Collections.Generic.List<CClanEventUserNewsTuple>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CCommunity_ClanAnnouncementInfo : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong gid
        {
            get => __pbn__gid.GetValueOrDefault();
            set => __pbn__gid = value;
        }
        public bool ShouldSerializegid() => __pbn__gid != null;
        public void Resetgid() => __pbn__gid = null;
        private ulong? __pbn__gid;

        [global::ProtoBuf.ProtoMember(2)]
        public ulong clanid
        {
            get => __pbn__clanid.GetValueOrDefault();
            set => __pbn__clanid = value;
        }
        public bool ShouldSerializeclanid() => __pbn__clanid != null;
        public void Resetclanid() => __pbn__clanid = null;
        private ulong? __pbn__clanid;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong posterid
        {
            get => __pbn__posterid.GetValueOrDefault();
            set => __pbn__posterid = value;
        }
        public bool ShouldSerializeposterid() => __pbn__posterid != null;
        public void Resetposterid() => __pbn__posterid = null;
        private ulong? __pbn__posterid;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string headline
        {
            get => __pbn__headline ?? "";
            set => __pbn__headline = value;
        }
        public bool ShouldSerializeheadline() => __pbn__headline != null;
        public void Resetheadline() => __pbn__headline = null;
        private string __pbn__headline;

        [global::ProtoBuf.ProtoMember(5)]
        public uint posttime
        {
            get => __pbn__posttime.GetValueOrDefault();
            set => __pbn__posttime = value;
        }
        public bool ShouldSerializeposttime() => __pbn__posttime != null;
        public void Resetposttime() => __pbn__posttime = null;
        private uint? __pbn__posttime;

        [global::ProtoBuf.ProtoMember(6)]
        public uint updatetime
        {
            get => __pbn__updatetime.GetValueOrDefault();
            set => __pbn__updatetime = value;
        }
        public bool ShouldSerializeupdatetime() => __pbn__updatetime != null;
        public void Resetupdatetime() => __pbn__updatetime = null;
        private uint? __pbn__updatetime;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string body
        {
            get => __pbn__body ?? "";
            set => __pbn__body = value;
        }
        public bool ShouldSerializebody() => __pbn__body != null;
        public void Resetbody() => __pbn__body = null;
        private string __pbn__body;

        [global::ProtoBuf.ProtoMember(8)]
        public int commentcount
        {
            get => __pbn__commentcount.GetValueOrDefault();
            set => __pbn__commentcount = value;
        }
        public bool ShouldSerializecommentcount() => __pbn__commentcount != null;
        public void Resetcommentcount() => __pbn__commentcount = null;
        private int? __pbn__commentcount;

        [global::ProtoBuf.ProtoMember(9)]
        public global::System.Collections.Generic.List<string> tags { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(10)]
        public int language
        {
            get => __pbn__language.GetValueOrDefault();
            set => __pbn__language = value;
        }
        public bool ShouldSerializelanguage() => __pbn__language != null;
        public void Resetlanguage() => __pbn__language = null;
        private int? __pbn__language;

        [global::ProtoBuf.ProtoMember(11)]
        public bool hidden
        {
            get => __pbn__hidden.GetValueOrDefault();
            set => __pbn__hidden = value;
        }
        public bool ShouldSerializehidden() => __pbn__hidden != null;
        public void Resethidden() => __pbn__hidden = null;
        private bool? __pbn__hidden;

        [global::ProtoBuf.ProtoMember(12, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong forum_topic_id
        {
            get => __pbn__forum_topic_id.GetValueOrDefault();
            set => __pbn__forum_topic_id = value;
        }
        public bool ShouldSerializeforum_topic_id() => __pbn__forum_topic_id != null;
        public void Resetforum_topic_id() => __pbn__forum_topic_id = null;
        private ulong? __pbn__forum_topic_id;

        [global::ProtoBuf.ProtoMember(13, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong event_gid
        {
            get => __pbn__event_gid.GetValueOrDefault();
            set => __pbn__event_gid = value;
        }
        public bool ShouldSerializeevent_gid() => __pbn__event_gid != null;
        public void Resetevent_gid() => __pbn__event_gid = null;
        private ulong? __pbn__event_gid;

        [global::ProtoBuf.ProtoMember(14)]
        public int voteupcount
        {
            get => __pbn__voteupcount.GetValueOrDefault();
            set => __pbn__voteupcount = value;
        }
        public bool ShouldSerializevoteupcount() => __pbn__voteupcount != null;
        public void Resetvoteupcount() => __pbn__voteupcount = null;
        private int? __pbn__voteupcount;

        [global::ProtoBuf.ProtoMember(15)]
        public int votedowncount
        {
            get => __pbn__votedowncount.GetValueOrDefault();
            set => __pbn__votedowncount = value;
        }
        public bool ShouldSerializevotedowncount() => __pbn__votedowncount != null;
        public void Resetvotedowncount() => __pbn__votedowncount = null;
        private int? __pbn__votedowncount;

        [global::ProtoBuf.ProtoMember(16)]
        [global::System.ComponentModel.DefaultValue(EBanContentCheckResult.k_EBanContentCheckResult_NotScanned)]
        public EBanContentCheckResult ban_check_result
        {
            get => __pbn__ban_check_result ?? EBanContentCheckResult.k_EBanContentCheckResult_NotScanned;
            set => __pbn__ban_check_result = value;
        }
        public bool ShouldSerializeban_check_result() => __pbn__ban_check_result != null;
        public void Resetban_check_result() => __pbn__ban_check_result = null;
        private EBanContentCheckResult? __pbn__ban_check_result;

        [global::ProtoBuf.ProtoMember(17)]
        public bool banned
        {
            get => __pbn__banned.GetValueOrDefault();
            set => __pbn__banned = value;
        }
        public bool ShouldSerializebanned() => __pbn__banned != null;
        public void Resetbanned() => __pbn__banned = null;
        private bool? __pbn__banned;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CClanEventData : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong gid
        {
            get => __pbn__gid.GetValueOrDefault();
            set => __pbn__gid = value;
        }
        public bool ShouldSerializegid() => __pbn__gid != null;
        public void Resetgid() => __pbn__gid = null;
        private ulong? __pbn__gid;

        [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong clan_steamid
        {
            get => __pbn__clan_steamid.GetValueOrDefault();
            set => __pbn__clan_steamid = value;
        }
        public bool ShouldSerializeclan_steamid() => __pbn__clan_steamid != null;
        public void Resetclan_steamid() => __pbn__clan_steamid = null;
        private ulong? __pbn__clan_steamid;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string event_name
        {
            get => __pbn__event_name ?? "";
            set => __pbn__event_name = value;
        }
        public bool ShouldSerializeevent_name() => __pbn__event_name != null;
        public void Resetevent_name() => __pbn__event_name = null;
        private string __pbn__event_name;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue(EProtoClanEventType.k_EClanOtherEvent)]
        public EProtoClanEventType event_type
        {
            get => __pbn__event_type ?? EProtoClanEventType.k_EClanOtherEvent;
            set => __pbn__event_type = value;
        }
        public bool ShouldSerializeevent_type() => __pbn__event_type != null;
        public void Resetevent_type() => __pbn__event_type = null;
        private EProtoClanEventType? __pbn__event_type;

        [global::ProtoBuf.ProtoMember(5)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string server_address
        {
            get => __pbn__server_address ?? "";
            set => __pbn__server_address = value;
        }
        public bool ShouldSerializeserver_address() => __pbn__server_address != null;
        public void Resetserver_address() => __pbn__server_address = null;
        private string __pbn__server_address;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string server_password
        {
            get => __pbn__server_password ?? "";
            set => __pbn__server_password = value;
        }
        public bool ShouldSerializeserver_password() => __pbn__server_password != null;
        public void Resetserver_password() => __pbn__server_password = null;
        private string __pbn__server_password;

        [global::ProtoBuf.ProtoMember(8)]
        public uint rtime32_start_time
        {
            get => __pbn__rtime32_start_time.GetValueOrDefault();
            set => __pbn__rtime32_start_time = value;
        }
        public bool ShouldSerializertime32_start_time() => __pbn__rtime32_start_time != null;
        public void Resetrtime32_start_time() => __pbn__rtime32_start_time = null;
        private uint? __pbn__rtime32_start_time;

        [global::ProtoBuf.ProtoMember(9)]
        public uint rtime32_end_time
        {
            get => __pbn__rtime32_end_time.GetValueOrDefault();
            set => __pbn__rtime32_end_time = value;
        }
        public bool ShouldSerializertime32_end_time() => __pbn__rtime32_end_time != null;
        public void Resetrtime32_end_time() => __pbn__rtime32_end_time = null;
        private uint? __pbn__rtime32_end_time;

        [global::ProtoBuf.ProtoMember(10)]
        public int comment_count
        {
            get => __pbn__comment_count.GetValueOrDefault();
            set => __pbn__comment_count = value;
        }
        public bool ShouldSerializecomment_count() => __pbn__comment_count != null;
        public void Resetcomment_count() => __pbn__comment_count = null;
        private int? __pbn__comment_count;

        [global::ProtoBuf.ProtoMember(11, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong creator_steamid
        {
            get => __pbn__creator_steamid.GetValueOrDefault();
            set => __pbn__creator_steamid = value;
        }
        public bool ShouldSerializecreator_steamid() => __pbn__creator_steamid != null;
        public void Resetcreator_steamid() => __pbn__creator_steamid = null;
        private ulong? __pbn__creator_steamid;

        [global::ProtoBuf.ProtoMember(12, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong last_update_steamid
        {
            get => __pbn__last_update_steamid.GetValueOrDefault();
            set => __pbn__last_update_steamid = value;
        }
        public bool ShouldSerializelast_update_steamid() => __pbn__last_update_steamid != null;
        public void Resetlast_update_steamid() => __pbn__last_update_steamid = null;
        private ulong? __pbn__last_update_steamid;

        [global::ProtoBuf.ProtoMember(13)]
        [global::System.ComponentModel.DefaultValue("")]
        public string event_notes
        {
            get => __pbn__event_notes ?? "";
            set => __pbn__event_notes = value;
        }
        public bool ShouldSerializeevent_notes() => __pbn__event_notes != null;
        public void Resetevent_notes() => __pbn__event_notes = null;
        private string __pbn__event_notes;

        [global::ProtoBuf.ProtoMember(14)]
        [global::System.ComponentModel.DefaultValue("")]
        public string jsondata
        {
            get => __pbn__jsondata ?? "";
            set => __pbn__jsondata = value;
        }
        public bool ShouldSerializejsondata() => __pbn__jsondata != null;
        public void Resetjsondata() => __pbn__jsondata = null;
        private string __pbn__jsondata;

        [global::ProtoBuf.ProtoMember(15)]
        public CCommunity_ClanAnnouncementInfo announcement_body { get; set; }

        [global::ProtoBuf.ProtoMember(16)]
        public bool published
        {
            get => __pbn__published.GetValueOrDefault();
            set => __pbn__published = value;
        }
        public bool ShouldSerializepublished() => __pbn__published != null;
        public void Resetpublished() => __pbn__published = null;
        private bool? __pbn__published;

        [global::ProtoBuf.ProtoMember(17)]
        public bool hidden
        {
            get => __pbn__hidden.GetValueOrDefault();
            set => __pbn__hidden = value;
        }
        public bool ShouldSerializehidden() => __pbn__hidden != null;
        public void Resethidden() => __pbn__hidden = null;
        private bool? __pbn__hidden;

        [global::ProtoBuf.ProtoMember(18)]
        public uint rtime32_visibility_start
        {
            get => __pbn__rtime32_visibility_start.GetValueOrDefault();
            set => __pbn__rtime32_visibility_start = value;
        }
        public bool ShouldSerializertime32_visibility_start() => __pbn__rtime32_visibility_start != null;
        public void Resetrtime32_visibility_start() => __pbn__rtime32_visibility_start = null;
        private uint? __pbn__rtime32_visibility_start;

        [global::ProtoBuf.ProtoMember(19)]
        public uint rtime32_visibility_end
        {
            get => __pbn__rtime32_visibility_end.GetValueOrDefault();
            set => __pbn__rtime32_visibility_end = value;
        }
        public bool ShouldSerializertime32_visibility_end() => __pbn__rtime32_visibility_end != null;
        public void Resetrtime32_visibility_end() => __pbn__rtime32_visibility_end = null;
        private uint? __pbn__rtime32_visibility_end;

        [global::ProtoBuf.ProtoMember(20)]
        public uint broadcaster_accountid
        {
            get => __pbn__broadcaster_accountid.GetValueOrDefault();
            set => __pbn__broadcaster_accountid = value;
        }
        public bool ShouldSerializebroadcaster_accountid() => __pbn__broadcaster_accountid != null;
        public void Resetbroadcaster_accountid() => __pbn__broadcaster_accountid = null;
        private uint? __pbn__broadcaster_accountid;

        [global::ProtoBuf.ProtoMember(21)]
        public uint follower_count
        {
            get => __pbn__follower_count.GetValueOrDefault();
            set => __pbn__follower_count = value;
        }
        public bool ShouldSerializefollower_count() => __pbn__follower_count != null;
        public void Resetfollower_count() => __pbn__follower_count = null;
        private uint? __pbn__follower_count;

        [global::ProtoBuf.ProtoMember(22)]
        public uint ignore_count
        {
            get => __pbn__ignore_count.GetValueOrDefault();
            set => __pbn__ignore_count = value;
        }
        public bool ShouldSerializeignore_count() => __pbn__ignore_count != null;
        public void Resetignore_count() => __pbn__ignore_count = null;
        private uint? __pbn__ignore_count;

        [global::ProtoBuf.ProtoMember(23, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong forum_topic_id
        {
            get => __pbn__forum_topic_id.GetValueOrDefault();
            set => __pbn__forum_topic_id = value;
        }
        public bool ShouldSerializeforum_topic_id() => __pbn__forum_topic_id != null;
        public void Resetforum_topic_id() => __pbn__forum_topic_id = null;
        private ulong? __pbn__forum_topic_id;

        [global::ProtoBuf.ProtoMember(24)]
        public uint rtime32_last_modified
        {
            get => __pbn__rtime32_last_modified.GetValueOrDefault();
            set => __pbn__rtime32_last_modified = value;
        }
        public bool ShouldSerializertime32_last_modified() => __pbn__rtime32_last_modified != null;
        public void Resetrtime32_last_modified() => __pbn__rtime32_last_modified = null;
        private uint? __pbn__rtime32_last_modified;

        [global::ProtoBuf.ProtoMember(25, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong news_post_gid
        {
            get => __pbn__news_post_gid.GetValueOrDefault();
            set => __pbn__news_post_gid = value;
        }
        public bool ShouldSerializenews_post_gid() => __pbn__news_post_gid != null;
        public void Resetnews_post_gid() => __pbn__news_post_gid = null;
        private ulong? __pbn__news_post_gid;

        [global::ProtoBuf.ProtoMember(26)]
        public uint rtime_mod_reviewed
        {
            get => __pbn__rtime_mod_reviewed.GetValueOrDefault();
            set => __pbn__rtime_mod_reviewed = value;
        }
        public bool ShouldSerializertime_mod_reviewed() => __pbn__rtime_mod_reviewed != null;
        public void Resetrtime_mod_reviewed() => __pbn__rtime_mod_reviewed = null;
        private uint? __pbn__rtime_mod_reviewed;

        [global::ProtoBuf.ProtoMember(27)]
        public uint featured_app_tagid
        {
            get => __pbn__featured_app_tagid.GetValueOrDefault();
            set => __pbn__featured_app_tagid = value;
        }
        public bool ShouldSerializefeatured_app_tagid() => __pbn__featured_app_tagid != null;
        public void Resetfeatured_app_tagid() => __pbn__featured_app_tagid = null;
        private uint? __pbn__featured_app_tagid;

        [global::ProtoBuf.ProtoMember(28)]
        public global::System.Collections.Generic.List<uint> referenced_appids { get; } = new global::System.Collections.Generic.List<uint>();

        [global::ProtoBuf.ProtoMember(29)]
        public uint build_id
        {
            get => __pbn__build_id.GetValueOrDefault();
            set => __pbn__build_id = value;
        }
        public bool ShouldSerializebuild_id() => __pbn__build_id != null;
        public void Resetbuild_id() => __pbn__build_id = null;
        private uint? __pbn__build_id;

        [global::ProtoBuf.ProtoMember(30)]
        [global::System.ComponentModel.DefaultValue("")]
        public string build_branch
        {
            get => __pbn__build_branch ?? "";
            set => __pbn__build_branch = value;
        }
        public bool ShouldSerializebuild_branch() => __pbn__build_branch != null;
        public void Resetbuild_branch() => __pbn__build_branch = null;
        private string __pbn__build_branch;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CBilling_Address : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string first_name
        {
            get => __pbn__first_name ?? "";
            set => __pbn__first_name = value;
        }
        public bool ShouldSerializefirst_name() => __pbn__first_name != null;
        public void Resetfirst_name() => __pbn__first_name = null;
        private string __pbn__first_name;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string last_name
        {
            get => __pbn__last_name ?? "";
            set => __pbn__last_name = value;
        }
        public bool ShouldSerializelast_name() => __pbn__last_name != null;
        public void Resetlast_name() => __pbn__last_name = null;
        private string __pbn__last_name;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string address1
        {
            get => __pbn__address1 ?? "";
            set => __pbn__address1 = value;
        }
        public bool ShouldSerializeaddress1() => __pbn__address1 != null;
        public void Resetaddress1() => __pbn__address1 = null;
        private string __pbn__address1;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string address2
        {
            get => __pbn__address2 ?? "";
            set => __pbn__address2 = value;
        }
        public bool ShouldSerializeaddress2() => __pbn__address2 != null;
        public void Resetaddress2() => __pbn__address2 = null;
        private string __pbn__address2;

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue("")]
        public string city
        {
            get => __pbn__city ?? "";
            set => __pbn__city = value;
        }
        public bool ShouldSerializecity() => __pbn__city != null;
        public void Resetcity() => __pbn__city = null;
        private string __pbn__city;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string us_state
        {
            get => __pbn__us_state ?? "";
            set => __pbn__us_state = value;
        }
        public bool ShouldSerializeus_state() => __pbn__us_state != null;
        public void Resetus_state() => __pbn__us_state = null;
        private string __pbn__us_state;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string country_code
        {
            get => __pbn__country_code ?? "";
            set => __pbn__country_code = value;
        }
        public bool ShouldSerializecountry_code() => __pbn__country_code != null;
        public void Resetcountry_code() => __pbn__country_code = null;
        private string __pbn__country_code;

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue("")]
        public string postcode
        {
            get => __pbn__postcode ?? "";
            set => __pbn__postcode = value;
        }
        public bool ShouldSerializepostcode() => __pbn__postcode != null;
        public void Resetpostcode() => __pbn__postcode = null;
        private string __pbn__postcode;

        [global::ProtoBuf.ProtoMember(9)]
        public int zip_plus4
        {
            get => __pbn__zip_plus4.GetValueOrDefault();
            set => __pbn__zip_plus4 = value;
        }
        public bool ShouldSerializezip_plus4() => __pbn__zip_plus4 != null;
        public void Resetzip_plus4() => __pbn__zip_plus4 = null;
        private int? __pbn__zip_plus4;

        [global::ProtoBuf.ProtoMember(10)]
        [global::System.ComponentModel.DefaultValue("")]
        public string phone
        {
            get => __pbn__phone ?? "";
            set => __pbn__phone = value;
        }
        public bool ShouldSerializephone() => __pbn__phone != null;
        public void Resetphone() => __pbn__phone = null;
        private string __pbn__phone;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPackageReservationStatus : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint packageid
        {
            get => __pbn__packageid.GetValueOrDefault();
            set => __pbn__packageid = value;
        }
        public bool ShouldSerializepackageid() => __pbn__packageid != null;
        public void Resetpackageid() => __pbn__packageid = null;
        private uint? __pbn__packageid;

        [global::ProtoBuf.ProtoMember(2)]
        public int reservation_state
        {
            get => __pbn__reservation_state.GetValueOrDefault();
            set => __pbn__reservation_state = value;
        }
        public bool ShouldSerializereservation_state() => __pbn__reservation_state != null;
        public void Resetreservation_state() => __pbn__reservation_state = null;
        private int? __pbn__reservation_state;

        [global::ProtoBuf.ProtoMember(3)]
        public int queue_position
        {
            get => __pbn__queue_position.GetValueOrDefault();
            set => __pbn__queue_position = value;
        }
        public bool ShouldSerializequeue_position() => __pbn__queue_position != null;
        public void Resetqueue_position() => __pbn__queue_position = null;
        private int? __pbn__queue_position;

        [global::ProtoBuf.ProtoMember(4)]
        public int total_queue_size
        {
            get => __pbn__total_queue_size.GetValueOrDefault();
            set => __pbn__total_queue_size = value;
        }
        public bool ShouldSerializetotal_queue_size() => __pbn__total_queue_size != null;
        public void Resettotal_queue_size() => __pbn__total_queue_size = null;
        private int? __pbn__total_queue_size;

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue("")]
        public string reservation_country_code
        {
            get => __pbn__reservation_country_code ?? "";
            set => __pbn__reservation_country_code = value;
        }
        public bool ShouldSerializereservation_country_code() => __pbn__reservation_country_code != null;
        public void Resetreservation_country_code() => __pbn__reservation_country_code = null;
        private string __pbn__reservation_country_code;

        [global::ProtoBuf.ProtoMember(6)]
        public bool expired
        {
            get => __pbn__expired.GetValueOrDefault();
            set => __pbn__expired = value;
        }
        public bool ShouldSerializeexpired() => __pbn__expired != null;
        public void Resetexpired() => __pbn__expired = null;
        private bool? __pbn__expired;

        [global::ProtoBuf.ProtoMember(7)]
        public uint time_expires
        {
            get => __pbn__time_expires.GetValueOrDefault();
            set => __pbn__time_expires = value;
        }
        public bool ShouldSerializetime_expires() => __pbn__time_expires != null;
        public void Resettime_expires() => __pbn__time_expires = null;
        private uint? __pbn__time_expires;

        [global::ProtoBuf.ProtoMember(8)]
        public uint time_reserved
        {
            get => __pbn__time_reserved.GetValueOrDefault();
            set => __pbn__time_reserved = value;
        }
        public bool ShouldSerializetime_reserved() => __pbn__time_reserved != null;
        public void Resettime_reserved() => __pbn__time_reserved = null;
        private uint? __pbn__time_reserved;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgKeyValuePair : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string name
        {
            get => __pbn__name ?? "";
            set => __pbn__name = value;
        }
        public bool ShouldSerializename() => __pbn__name != null;
        public void Resetname() => __pbn__name = null;
        private string __pbn__name;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string value
        {
            get => __pbn__value ?? "";
            set => __pbn__value = value;
        }
        public bool ShouldSerializevalue() => __pbn__value != null;
        public void Resetvalue() => __pbn__value = null;
        private string __pbn__value;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgKeyValueSet : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<CMsgKeyValuePair> pairs { get; } = new global::System.Collections.Generic.List<CMsgKeyValuePair>();

    }


    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientCMList : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<uint> cm_addresses { get; } = new global::System.Collections.Generic.List<uint>();

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<uint> cm_ports { get; } = new global::System.Collections.Generic.List<uint>();

        [global::ProtoBuf.ProtoMember(3)]
        public global::System.Collections.Generic.List<string> cm_websocket_addresses { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(4)]
        public uint percent_default_to_websocket
        {
            get => __pbn__percent_default_to_websocket.GetValueOrDefault();
            set => __pbn__percent_default_to_websocket = value;
        }
        public bool ShouldSerializepercent_default_to_websocket() => __pbn__percent_default_to_websocket != null;
        public void Resetpercent_default_to_websocket() => __pbn__percent_default_to_websocket = null;
        private uint? __pbn__percent_default_to_websocket;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientSessionToken : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong token
        {
            get => __pbn__token.GetValueOrDefault();
            set => __pbn__token = value;
        }
        public bool ShouldSerializetoken() => __pbn__token != null;
        public void Resettoken() => __pbn__token = null;
        private ulong? __pbn__token;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgGCClient : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

        [global::ProtoBuf.ProtoMember(2)]
        public uint msgtype
        {
            get => __pbn__msgtype.GetValueOrDefault();
            set => __pbn__msgtype = value;
        }
        public bool ShouldSerializemsgtype() => __pbn__msgtype != null;
        public void Resetmsgtype() => __pbn__msgtype = null;
        private uint? __pbn__msgtype;

        [global::ProtoBuf.ProtoMember(3)]
        public byte[] payload
        {
            get => __pbn__payload;
            set => __pbn__payload = value;
        }
        public bool ShouldSerializepayload() => __pbn__payload != null;
        public void Resetpayload() => __pbn__payload = null;
        private byte[] __pbn__payload;

        [global::ProtoBuf.ProtoMember(4, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue("")]
        public string gcname
        {
            get => __pbn__gcname ?? "";
            set => __pbn__gcname = value;
        }
        public bool ShouldSerializegcname() => __pbn__gcname != null;
        public void Resetgcname() => __pbn__gcname = null;
        private string __pbn__gcname;

        [global::ProtoBuf.ProtoMember(6)]
        public uint ip
        {
            get => __pbn__ip.GetValueOrDefault();
            set => __pbn__ip = value;
        }
        public bool ShouldSerializeip() => __pbn__ip != null;
        public void Resetip() => __pbn__ip = null;
        private uint? __pbn__ip;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientGameConnectTokens : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue(10u)]
        public uint max_tokens_to_keep
        {
            get => __pbn__max_tokens_to_keep ?? 10u;
            set => __pbn__max_tokens_to_keep = value;
        }
        public bool ShouldSerializemax_tokens_to_keep() => __pbn__max_tokens_to_keep != null;
        public void Resetmax_tokens_to_keep() => __pbn__max_tokens_to_keep = null;
        private uint? __pbn__max_tokens_to_keep;

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<byte[]> tokens { get; } = new global::System.Collections.Generic.List<byte[]>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientGamesPlayed : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<GamePlayed> games_played { get; } = new global::System.Collections.Generic.List<GamePlayed>();

        [global::ProtoBuf.ProtoMember(2)]
        public uint client_os_type
        {
            get => __pbn__client_os_type.GetValueOrDefault();
            set => __pbn__client_os_type = value;
        }
        public bool ShouldSerializeclient_os_type() => __pbn__client_os_type != null;
        public void Resetclient_os_type() => __pbn__client_os_type = null;
        private uint? __pbn__client_os_type;

        [global::ProtoBuf.ProtoMember(3)]
        public uint cloud_gaming_platform
        {
            get => __pbn__cloud_gaming_platform.GetValueOrDefault();
            set => __pbn__cloud_gaming_platform = value;
        }
        public bool ShouldSerializecloud_gaming_platform() => __pbn__cloud_gaming_platform != null;
        public void Resetcloud_gaming_platform() => __pbn__cloud_gaming_platform = null;
        private uint? __pbn__cloud_gaming_platform;

        [global::ProtoBuf.ProtoMember(4)]
        public bool recent_reauthentication
        {
            get => __pbn__recent_reauthentication.GetValueOrDefault();
            set => __pbn__recent_reauthentication = value;
        }
        public bool ShouldSerializerecent_reauthentication() => __pbn__recent_reauthentication != null;
        public void Resetrecent_reauthentication() => __pbn__recent_reauthentication = null;
        private bool? __pbn__recent_reauthentication;

        [global::ProtoBuf.ProtoContract()]
        public partial class ProcessInfo : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public uint process_id
            {
                get => __pbn__process_id.GetValueOrDefault();
                set => __pbn__process_id = value;
            }
            public bool ShouldSerializeprocess_id() => __pbn__process_id != null;
            public void Resetprocess_id() => __pbn__process_id = null;
            private uint? __pbn__process_id;

            [global::ProtoBuf.ProtoMember(2)]
            public uint process_id_parent
            {
                get => __pbn__process_id_parent.GetValueOrDefault();
                set => __pbn__process_id_parent = value;
            }
            public bool ShouldSerializeprocess_id_parent() => __pbn__process_id_parent != null;
            public void Resetprocess_id_parent() => __pbn__process_id_parent = null;
            private uint? __pbn__process_id_parent;

            [global::ProtoBuf.ProtoMember(3)]
            public bool parent_is_steam
            {
                get => __pbn__parent_is_steam.GetValueOrDefault();
                set => __pbn__parent_is_steam = value;
            }
            public bool ShouldSerializeparent_is_steam() => __pbn__parent_is_steam != null;
            public void Resetparent_is_steam() => __pbn__parent_is_steam = null;
            private bool? __pbn__parent_is_steam;

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class GamePlayed : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public ulong steam_id_gs
            {
                get => __pbn__steam_id_gs.GetValueOrDefault();
                set => __pbn__steam_id_gs = value;
            }
            public bool ShouldSerializesteam_id_gs() => __pbn__steam_id_gs != null;
            public void Resetsteam_id_gs() => __pbn__steam_id_gs = null;
            private ulong? __pbn__steam_id_gs;

            [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
            public ulong game_id
            {
                get => __pbn__game_id.GetValueOrDefault();
                set => __pbn__game_id = value;
            }
            public bool ShouldSerializegame_id() => __pbn__game_id != null;
            public void Resetgame_id() => __pbn__game_id = null;
            private ulong? __pbn__game_id;

            [global::ProtoBuf.ProtoMember(3)]
            public uint deprecated_game_ip_address
            {
                get => __pbn__deprecated_game_ip_address.GetValueOrDefault();
                set => __pbn__deprecated_game_ip_address = value;
            }
            public bool ShouldSerializedeprecated_game_ip_address() => __pbn__deprecated_game_ip_address != null;
            public void Resetdeprecated_game_ip_address() => __pbn__deprecated_game_ip_address = null;
            private uint? __pbn__deprecated_game_ip_address;

            [global::ProtoBuf.ProtoMember(4)]
            public uint game_port
            {
                get => __pbn__game_port.GetValueOrDefault();
                set => __pbn__game_port = value;
            }
            public bool ShouldSerializegame_port() => __pbn__game_port != null;
            public void Resetgame_port() => __pbn__game_port = null;
            private uint? __pbn__game_port;

            [global::ProtoBuf.ProtoMember(5)]
            public bool is_secure
            {
                get => __pbn__is_secure.GetValueOrDefault();
                set => __pbn__is_secure = value;
            }
            public bool ShouldSerializeis_secure() => __pbn__is_secure != null;
            public void Resetis_secure() => __pbn__is_secure = null;
            private bool? __pbn__is_secure;

            [global::ProtoBuf.ProtoMember(6)]
            public byte[] token
            {
                get => __pbn__token;
                set => __pbn__token = value;
            }
            public bool ShouldSerializetoken() => __pbn__token != null;
            public void Resettoken() => __pbn__token = null;
            private byte[] __pbn__token;

            [global::ProtoBuf.ProtoMember(7)]
            [global::System.ComponentModel.DefaultValue("")]
            public string game_extra_info
            {
                get => __pbn__game_extra_info ?? "";
                set => __pbn__game_extra_info = value;
            }
            public bool ShouldSerializegame_extra_info() => __pbn__game_extra_info != null;
            public void Resetgame_extra_info() => __pbn__game_extra_info = null;
            private string __pbn__game_extra_info;

            [global::ProtoBuf.ProtoMember(8)]
            public byte[] game_data_blob
            {
                get => __pbn__game_data_blob;
                set => __pbn__game_data_blob = value;
            }
            public bool ShouldSerializegame_data_blob() => __pbn__game_data_blob != null;
            public void Resetgame_data_blob() => __pbn__game_data_blob = null;
            private byte[] __pbn__game_data_blob;

            [global::ProtoBuf.ProtoMember(9)]
            public uint process_id
            {
                get => __pbn__process_id.GetValueOrDefault();
                set => __pbn__process_id = value;
            }
            public bool ShouldSerializeprocess_id() => __pbn__process_id != null;
            public void Resetprocess_id() => __pbn__process_id = null;
            private uint? __pbn__process_id;

            [global::ProtoBuf.ProtoMember(10)]
            public uint streaming_provider_id
            {
                get => __pbn__streaming_provider_id.GetValueOrDefault();
                set => __pbn__streaming_provider_id = value;
            }
            public bool ShouldSerializestreaming_provider_id() => __pbn__streaming_provider_id != null;
            public void Resetstreaming_provider_id() => __pbn__streaming_provider_id = null;
            private uint? __pbn__streaming_provider_id;

            [global::ProtoBuf.ProtoMember(11)]
            public uint game_flags
            {
                get => __pbn__game_flags.GetValueOrDefault();
                set => __pbn__game_flags = value;
            }
            public bool ShouldSerializegame_flags() => __pbn__game_flags != null;
            public void Resetgame_flags() => __pbn__game_flags = null;
            private uint? __pbn__game_flags;

            [global::ProtoBuf.ProtoMember(12)]
            public uint owner_id
            {
                get => __pbn__owner_id.GetValueOrDefault();
                set => __pbn__owner_id = value;
            }
            public bool ShouldSerializeowner_id() => __pbn__owner_id != null;
            public void Resetowner_id() => __pbn__owner_id = null;
            private uint? __pbn__owner_id;

            [global::ProtoBuf.ProtoMember(13)]
            [global::System.ComponentModel.DefaultValue("")]
            public string vr_hmd_vendor
            {
                get => __pbn__vr_hmd_vendor ?? "";
                set => __pbn__vr_hmd_vendor = value;
            }
            public bool ShouldSerializevr_hmd_vendor() => __pbn__vr_hmd_vendor != null;
            public void Resetvr_hmd_vendor() => __pbn__vr_hmd_vendor = null;
            private string __pbn__vr_hmd_vendor;

            [global::ProtoBuf.ProtoMember(14)]
            [global::System.ComponentModel.DefaultValue("")]
            public string vr_hmd_model
            {
                get => __pbn__vr_hmd_model ?? "";
                set => __pbn__vr_hmd_model = value;
            }
            public bool ShouldSerializevr_hmd_model() => __pbn__vr_hmd_model != null;
            public void Resetvr_hmd_model() => __pbn__vr_hmd_model = null;
            private string __pbn__vr_hmd_model;

            [global::ProtoBuf.ProtoMember(15)]
            [global::System.ComponentModel.DefaultValue(0u)]
            public uint launch_option_type
            {
                get => __pbn__launch_option_type ?? 0u;
                set => __pbn__launch_option_type = value;
            }
            public bool ShouldSerializelaunch_option_type() => __pbn__launch_option_type != null;
            public void Resetlaunch_option_type() => __pbn__launch_option_type = null;
            private uint? __pbn__launch_option_type;

            [global::ProtoBuf.ProtoMember(16)]
            [global::System.ComponentModel.DefaultValue(-1)]
            public int primary_controller_type
            {
                get => __pbn__primary_controller_type ?? -1;
                set => __pbn__primary_controller_type = value;
            }
            public bool ShouldSerializeprimary_controller_type() => __pbn__primary_controller_type != null;
            public void Resetprimary_controller_type() => __pbn__primary_controller_type = null;
            private int? __pbn__primary_controller_type;

            [global::ProtoBuf.ProtoMember(17)]
            [global::System.ComponentModel.DefaultValue("")]
            public string primary_steam_controller_serial
            {
                get => __pbn__primary_steam_controller_serial ?? "";
                set => __pbn__primary_steam_controller_serial = value;
            }
            public bool ShouldSerializeprimary_steam_controller_serial() => __pbn__primary_steam_controller_serial != null;
            public void Resetprimary_steam_controller_serial() => __pbn__primary_steam_controller_serial = null;
            private string __pbn__primary_steam_controller_serial;

            [global::ProtoBuf.ProtoMember(18)]
            [global::System.ComponentModel.DefaultValue(0u)]
            public uint total_steam_controller_count
            {
                get => __pbn__total_steam_controller_count ?? 0u;
                set => __pbn__total_steam_controller_count = value;
            }
            public bool ShouldSerializetotal_steam_controller_count() => __pbn__total_steam_controller_count != null;
            public void Resettotal_steam_controller_count() => __pbn__total_steam_controller_count = null;
            private uint? __pbn__total_steam_controller_count;

            [global::ProtoBuf.ProtoMember(19)]
            [global::System.ComponentModel.DefaultValue(0u)]
            public uint total_non_steam_controller_count
            {
                get => __pbn__total_non_steam_controller_count ?? 0u;
                set => __pbn__total_non_steam_controller_count = value;
            }
            public bool ShouldSerializetotal_non_steam_controller_count() => __pbn__total_non_steam_controller_count != null;
            public void Resettotal_non_steam_controller_count() => __pbn__total_non_steam_controller_count = null;
            private uint? __pbn__total_non_steam_controller_count;

            [global::ProtoBuf.ProtoMember(20)]
            [global::System.ComponentModel.DefaultValue(typeof(ulong), "0")]
            public ulong controller_workshop_file_id
            {
                get => __pbn__controller_workshop_file_id ?? 0ul;
                set => __pbn__controller_workshop_file_id = value;
            }
            public bool ShouldSerializecontroller_workshop_file_id() => __pbn__controller_workshop_file_id != null;
            public void Resetcontroller_workshop_file_id() => __pbn__controller_workshop_file_id = null;
            private ulong? __pbn__controller_workshop_file_id;

            [global::ProtoBuf.ProtoMember(21)]
            [global::System.ComponentModel.DefaultValue(0u)]
            public uint launch_source
            {
                get => __pbn__launch_source ?? 0u;
                set => __pbn__launch_source = value;
            }
            public bool ShouldSerializelaunch_source() => __pbn__launch_source != null;
            public void Resetlaunch_source() => __pbn__launch_source = null;
            private uint? __pbn__launch_source;

            [global::ProtoBuf.ProtoMember(22)]
            public uint vr_hmd_runtime
            {
                get => __pbn__vr_hmd_runtime.GetValueOrDefault();
                set => __pbn__vr_hmd_runtime = value;
            }
            public bool ShouldSerializevr_hmd_runtime() => __pbn__vr_hmd_runtime != null;
            public void Resetvr_hmd_runtime() => __pbn__vr_hmd_runtime = null;
            private uint? __pbn__vr_hmd_runtime;

            [global::ProtoBuf.ProtoMember(23)]
            public CMsgIPAddress game_ip_address { get; set; }

            [global::ProtoBuf.ProtoMember(24)]
            [global::System.ComponentModel.DefaultValue(0u)]
            public uint controller_connection_type
            {
                get => __pbn__controller_connection_type ?? 0u;
                set => __pbn__controller_connection_type = value;
            }
            public bool ShouldSerializecontroller_connection_type() => __pbn__controller_connection_type != null;
            public void Resetcontroller_connection_type() => __pbn__controller_connection_type = null;
            private uint? __pbn__controller_connection_type;

            [global::ProtoBuf.ProtoMember(25)]
            public int game_os_platform
            {
                get => __pbn__game_os_platform.GetValueOrDefault();
                set => __pbn__game_os_platform = value;
            }
            public bool ShouldSerializegame_os_platform() => __pbn__game_os_platform != null;
            public void Resetgame_os_platform() => __pbn__game_os_platform = null;
            private int? __pbn__game_os_platform;

            [global::ProtoBuf.ProtoMember(26)]
            public uint game_build_id
            {
                get => __pbn__game_build_id.GetValueOrDefault();
                set => __pbn__game_build_id = value;
            }
            public bool ShouldSerializegame_build_id() => __pbn__game_build_id != null;
            public void Resetgame_build_id() => __pbn__game_build_id = null;
            private uint? __pbn__game_build_id;

            [global::ProtoBuf.ProtoMember(27)]
            [global::System.ComponentModel.DefaultValue(0u)]
            public uint compat_tool_id
            {
                get => __pbn__compat_tool_id ?? 0u;
                set => __pbn__compat_tool_id = value;
            }
            public bool ShouldSerializecompat_tool_id() => __pbn__compat_tool_id != null;
            public void Resetcompat_tool_id() => __pbn__compat_tool_id = null;
            private uint? __pbn__compat_tool_id;

            [global::ProtoBuf.ProtoMember(28)]
            [global::System.ComponentModel.DefaultValue("")]
            public string compat_tool_cmd
            {
                get => __pbn__compat_tool_cmd ?? "";
                set => __pbn__compat_tool_cmd = value;
            }
            public bool ShouldSerializecompat_tool_cmd() => __pbn__compat_tool_cmd != null;
            public void Resetcompat_tool_cmd() => __pbn__compat_tool_cmd = null;
            private string __pbn__compat_tool_cmd;

            [global::ProtoBuf.ProtoMember(29)]
            public uint compat_tool_build_id
            {
                get => __pbn__compat_tool_build_id.GetValueOrDefault();
                set => __pbn__compat_tool_build_id = value;
            }
            public bool ShouldSerializecompat_tool_build_id() => __pbn__compat_tool_build_id != null;
            public void Resetcompat_tool_build_id() => __pbn__compat_tool_build_id = null;
            private uint? __pbn__compat_tool_build_id;

            [global::ProtoBuf.ProtoMember(30)]
            [global::System.ComponentModel.DefaultValue("")]
            public string beta_name
            {
                get => __pbn__beta_name ?? "";
                set => __pbn__beta_name = value;
            }
            public bool ShouldSerializebeta_name() => __pbn__beta_name != null;
            public void Resetbeta_name() => __pbn__beta_name = null;
            private string __pbn__beta_name;

            [global::ProtoBuf.ProtoMember(31)]
            public uint dlc_context
            {
                get => __pbn__dlc_context.GetValueOrDefault();
                set => __pbn__dlc_context = value;
            }
            public bool ShouldSerializedlc_context() => __pbn__dlc_context != null;
            public void Resetdlc_context() => __pbn__dlc_context = null;
            private uint? __pbn__dlc_context;

            [global::ProtoBuf.ProtoMember(32)]
            public global::System.Collections.Generic.List<CMsgClientGamesPlayed.ProcessInfo> process_id_list { get; } = new global::System.Collections.Generic.List<CMsgClientGamesPlayed.ProcessInfo>();

        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgDPGetNumberOfCurrentPlayers : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgDPGetNumberOfCurrentPlayersResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue(2)]
        public int eresult
        {
            get => __pbn__eresult ?? 2;
            set => __pbn__eresult = value;
        }
        public bool ShouldSerializeeresult() => __pbn__eresult != null;
        public void Reseteresult() => __pbn__eresult = null;
        private int? __pbn__eresult;

        [global::ProtoBuf.ProtoMember(2)]
        public int player_count
        {
            get => __pbn__player_count.GetValueOrDefault();
            set => __pbn__player_count = value;
        }
        public bool ShouldSerializeplayer_count() => __pbn__player_count != null;
        public void Resetplayer_count() => __pbn__player_count = null;
        private int? __pbn__player_count;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientGetAppOwnershipTicket : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint app_id
        {
            get => __pbn__app_id.GetValueOrDefault();
            set => __pbn__app_id = value;
        }
        public bool ShouldSerializeapp_id() => __pbn__app_id != null;
        public void Resetapp_id() => __pbn__app_id = null;
        private uint? __pbn__app_id;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientGetAppOwnershipTicketResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue(2u)]
        public uint eresult
        {
            get => __pbn__eresult ?? 2u;
            set => __pbn__eresult = value;
        }
        public bool ShouldSerializeeresult() => __pbn__eresult != null;
        public void Reseteresult() => __pbn__eresult = null;
        private uint? __pbn__eresult;

        [global::ProtoBuf.ProtoMember(2)]
        public uint app_id
        {
            get => __pbn__app_id.GetValueOrDefault();
            set => __pbn__app_id = value;
        }
        public bool ShouldSerializeapp_id() => __pbn__app_id != null;
        public void Resetapp_id() => __pbn__app_id = null;
        private uint? __pbn__app_id;

        [global::ProtoBuf.ProtoMember(3)]
        public byte[] ticket
        {
            get => __pbn__ticket;
            set => __pbn__ticket = value;
        }
        public bool ShouldSerializeticket() => __pbn__ticket != null;
        public void Resetticket() => __pbn__ticket = null;
        private byte[] __pbn__ticket;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientTicketAuthComplete : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steam_id
        {
            get => __pbn__steam_id.GetValueOrDefault();
            set => __pbn__steam_id = value;
        }
        public bool ShouldSerializesteam_id() => __pbn__steam_id != null;
        public void Resetsteam_id() => __pbn__steam_id = null;
        private ulong? __pbn__steam_id;

        [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong game_id
        {
            get => __pbn__game_id.GetValueOrDefault();
            set => __pbn__game_id = value;
        }
        public bool ShouldSerializegame_id() => __pbn__game_id != null;
        public void Resetgame_id() => __pbn__game_id = null;
        private ulong? __pbn__game_id;

        [global::ProtoBuf.ProtoMember(3)]
        public uint estate
        {
            get => __pbn__estate.GetValueOrDefault();
            set => __pbn__estate = value;
        }
        public bool ShouldSerializeestate() => __pbn__estate != null;
        public void Resetestate() => __pbn__estate = null;
        private uint? __pbn__estate;

        [global::ProtoBuf.ProtoMember(4)]
        public uint eauth_session_response
        {
            get => __pbn__eauth_session_response.GetValueOrDefault();
            set => __pbn__eauth_session_response = value;
        }
        public bool ShouldSerializeeauth_session_response() => __pbn__eauth_session_response != null;
        public void Reseteauth_session_response() => __pbn__eauth_session_response = null;
        private uint? __pbn__eauth_session_response;

        [global::ProtoBuf.ProtoMember(5)]
        public byte[] DEPRECATED_ticket
        {
            get => __pbn__DEPRECATED_ticket;
            set => __pbn__DEPRECATED_ticket = value;
        }
        public bool ShouldSerializeDEPRECATED_ticket() => __pbn__DEPRECATED_ticket != null;
        public void ResetDEPRECATED_ticket() => __pbn__DEPRECATED_ticket = null;
        private byte[] __pbn__DEPRECATED_ticket;

        [global::ProtoBuf.ProtoMember(6)]
        public uint ticket_crc
        {
            get => __pbn__ticket_crc.GetValueOrDefault();
            set => __pbn__ticket_crc = value;
        }
        public bool ShouldSerializeticket_crc() => __pbn__ticket_crc != null;
        public void Resetticket_crc() => __pbn__ticket_crc = null;
        private uint? __pbn__ticket_crc;

        [global::ProtoBuf.ProtoMember(7)]
        public uint ticket_sequence
        {
            get => __pbn__ticket_sequence.GetValueOrDefault();
            set => __pbn__ticket_sequence = value;
        }
        public bool ShouldSerializeticket_sequence() => __pbn__ticket_sequence != null;
        public void Resetticket_sequence() => __pbn__ticket_sequence = null;
        private uint? __pbn__ticket_sequence;

        [global::ProtoBuf.ProtoMember(8, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong owner_steam_id
        {
            get => __pbn__owner_steam_id.GetValueOrDefault();
            set => __pbn__owner_steam_id = value;
        }
        public bool ShouldSerializeowner_steam_id() => __pbn__owner_steam_id != null;
        public void Resetowner_steam_id() => __pbn__owner_steam_id = null;
        private ulong? __pbn__owner_steam_id;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientAuthList : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint tokens_left
        {
            get => __pbn__tokens_left.GetValueOrDefault();
            set => __pbn__tokens_left = value;
        }
        public bool ShouldSerializetokens_left() => __pbn__tokens_left != null;
        public void Resettokens_left() => __pbn__tokens_left = null;
        private uint? __pbn__tokens_left;

        [global::ProtoBuf.ProtoMember(2)]
        public uint last_request_seq
        {
            get => __pbn__last_request_seq.GetValueOrDefault();
            set => __pbn__last_request_seq = value;
        }
        public bool ShouldSerializelast_request_seq() => __pbn__last_request_seq != null;
        public void Resetlast_request_seq() => __pbn__last_request_seq = null;
        private uint? __pbn__last_request_seq;

        [global::ProtoBuf.ProtoMember(3)]
        public uint last_request_seq_from_server
        {
            get => __pbn__last_request_seq_from_server.GetValueOrDefault();
            set => __pbn__last_request_seq_from_server = value;
        }
        public bool ShouldSerializelast_request_seq_from_server() => __pbn__last_request_seq_from_server != null;
        public void Resetlast_request_seq_from_server() => __pbn__last_request_seq_from_server = null;
        private uint? __pbn__last_request_seq_from_server;

        [global::ProtoBuf.ProtoMember(4)]
        public global::System.Collections.Generic.List<CMsgAuthTicket> tickets { get; } = new global::System.Collections.Generic.List<CMsgAuthTicket>();

        [global::ProtoBuf.ProtoMember(5)]
        public global::System.Collections.Generic.List<uint> app_ids { get; } = new global::System.Collections.Generic.List<uint>();

        [global::ProtoBuf.ProtoMember(6)]
        public uint message_sequence
        {
            get => __pbn__message_sequence.GetValueOrDefault();
            set => __pbn__message_sequence = value;
        }
        public bool ShouldSerializemessage_sequence() => __pbn__message_sequence != null;
        public void Resetmessage_sequence() => __pbn__message_sequence = null;
        private uint? __pbn__message_sequence;

        [global::ProtoBuf.ProtoMember(7)]
        public bool filtered
        {
            get => __pbn__filtered.GetValueOrDefault();
            set => __pbn__filtered = value;
        }
        public bool ShouldSerializefiltered() => __pbn__filtered != null;
        public void Resetfiltered() => __pbn__filtered = null;
        private bool? __pbn__filtered;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientAuthListAck : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<uint> ticket_crc { get; } = new global::System.Collections.Generic.List<uint>();

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<uint> app_ids { get; } = new global::System.Collections.Generic.List<uint>();

        [global::ProtoBuf.ProtoMember(3)]
        public uint message_sequence
        {
            get => __pbn__message_sequence.GetValueOrDefault();
            set => __pbn__message_sequence = value;
        }
        public bool ShouldSerializemessage_sequence() => __pbn__message_sequence != null;
        public void Resetmessage_sequence() => __pbn__message_sequence = null;
        private uint? __pbn__message_sequence;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientRequestFreeLicense : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<uint> appids { get; } = new global::System.Collections.Generic.List<uint>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientRequestFreeLicenseResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue(2u)]
        public uint eresult
        {
            get => __pbn__eresult ?? 2u;
            set => __pbn__eresult = value;
        }
        public bool ShouldSerializeeresult() => __pbn__eresult != null;
        public void Reseteresult() => __pbn__eresult = null;
        private uint? __pbn__eresult;

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<uint> granted_packageids { get; } = new global::System.Collections.Generic.List<uint>();

        [global::ProtoBuf.ProtoMember(3)]
        public global::System.Collections.Generic.List<uint> granted_appids { get; } = new global::System.Collections.Generic.List<uint>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_Time_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong sender_time
        {
            get => __pbn__sender_time.GetValueOrDefault();
            set => __pbn__sender_time = value;
        }
        public bool ShouldSerializesender_time() => __pbn__sender_time != null;
        public void Resetsender_time() => __pbn__sender_time = null;
        private ulong? __pbn__sender_time;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_Time_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong server_time
        {
            get => __pbn__server_time.GetValueOrDefault();
            set => __pbn__server_time = value;
        }
        public bool ShouldSerializeserver_time() => __pbn__server_time != null;
        public void Resetserver_time() => __pbn__server_time = null;
        private ulong? __pbn__server_time;

        [global::ProtoBuf.ProtoMember(2)]
        public ulong skew_tolerance_seconds
        {
            get => __pbn__skew_tolerance_seconds.GetValueOrDefault();
            set => __pbn__skew_tolerance_seconds = value;
        }
        public bool ShouldSerializeskew_tolerance_seconds() => __pbn__skew_tolerance_seconds != null;
        public void Resetskew_tolerance_seconds() => __pbn__skew_tolerance_seconds = null;
        private ulong? __pbn__skew_tolerance_seconds;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong large_time_jink
        {
            get => __pbn__large_time_jink.GetValueOrDefault();
            set => __pbn__large_time_jink = value;
        }
        public bool ShouldSerializelarge_time_jink() => __pbn__large_time_jink != null;
        public void Resetlarge_time_jink() => __pbn__large_time_jink = null;
        private ulong? __pbn__large_time_jink;

        [global::ProtoBuf.ProtoMember(4)]
        public uint probe_frequency_seconds
        {
            get => __pbn__probe_frequency_seconds.GetValueOrDefault();
            set => __pbn__probe_frequency_seconds = value;
        }
        public bool ShouldSerializeprobe_frequency_seconds() => __pbn__probe_frequency_seconds != null;
        public void Resetprobe_frequency_seconds() => __pbn__probe_frequency_seconds = null;
        private uint? __pbn__probe_frequency_seconds;

        [global::ProtoBuf.ProtoMember(5)]
        public uint adjusted_time_probe_frequency_seconds
        {
            get => __pbn__adjusted_time_probe_frequency_seconds.GetValueOrDefault();
            set => __pbn__adjusted_time_probe_frequency_seconds = value;
        }
        public bool ShouldSerializeadjusted_time_probe_frequency_seconds() => __pbn__adjusted_time_probe_frequency_seconds != null;
        public void Resetadjusted_time_probe_frequency_seconds() => __pbn__adjusted_time_probe_frequency_seconds = null;
        private uint? __pbn__adjusted_time_probe_frequency_seconds;

        [global::ProtoBuf.ProtoMember(6)]
        public uint hint_probe_frequency_seconds
        {
            get => __pbn__hint_probe_frequency_seconds.GetValueOrDefault();
            set => __pbn__hint_probe_frequency_seconds = value;
        }
        public bool ShouldSerializehint_probe_frequency_seconds() => __pbn__hint_probe_frequency_seconds != null;
        public void Resethint_probe_frequency_seconds() => __pbn__hint_probe_frequency_seconds = null;
        private uint? __pbn__hint_probe_frequency_seconds;

        [global::ProtoBuf.ProtoMember(7)]
        public uint sync_timeout
        {
            get => __pbn__sync_timeout.GetValueOrDefault();
            set => __pbn__sync_timeout = value;
        }
        public bool ShouldSerializesync_timeout() => __pbn__sync_timeout != null;
        public void Resetsync_timeout() => __pbn__sync_timeout = null;
        private uint? __pbn__sync_timeout;

        [global::ProtoBuf.ProtoMember(8)]
        public uint try_again_seconds
        {
            get => __pbn__try_again_seconds.GetValueOrDefault();
            set => __pbn__try_again_seconds = value;
        }
        public bool ShouldSerializetry_again_seconds() => __pbn__try_again_seconds != null;
        public void Resettry_again_seconds() => __pbn__try_again_seconds = null;
        private uint? __pbn__try_again_seconds;

        [global::ProtoBuf.ProtoMember(9)]
        public uint max_attempts
        {
            get => __pbn__max_attempts.GetValueOrDefault();
            set => __pbn__max_attempts = value;
        }
        public bool ShouldSerializemax_attempts() => __pbn__max_attempts != null;
        public void Resetmax_attempts() => __pbn__max_attempts = null;
        private uint? __pbn__max_attempts;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_Status_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue(ETwoFactorStatusFieldFlag.k_ETwoFactorStatusFieldFlag_None)]
        public ETwoFactorStatusFieldFlag include
        {
            get => __pbn__include ?? ETwoFactorStatusFieldFlag.k_ETwoFactorStatusFieldFlag_None;
            set => __pbn__include = value;
        }
        public bool ShouldSerializeinclude() => __pbn__include != null;
        public void Resetinclude() => __pbn__include = null;
        private ETwoFactorStatusFieldFlag? __pbn__include;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_UsageEvent : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint time
        {
            get => __pbn__time.GetValueOrDefault();
            set => __pbn__time = value;
        }
        public bool ShouldSerializetime() => __pbn__time != null;
        public void Resettime() => __pbn__time = null;
        private uint? __pbn__time;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue(ETwoFactorUsageType.k_ETwoFactorUsageType_Unknown)]
        public ETwoFactorUsageType usage_type
        {
            get => __pbn__usage_type ?? ETwoFactorUsageType.k_ETwoFactorUsageType_Unknown;
            set => __pbn__usage_type = value;
        }
        public bool ShouldSerializeusage_type() => __pbn__usage_type != null;
        public void Resetusage_type() => __pbn__usage_type = null;
        private ETwoFactorUsageType? __pbn__usage_type;

        [global::ProtoBuf.ProtoMember(3)]
        public int confirmation_type
        {
            get => __pbn__confirmation_type.GetValueOrDefault();
            set => __pbn__confirmation_type = value;
        }
        public bool ShouldSerializeconfirmation_type() => __pbn__confirmation_type != null;
        public void Resetconfirmation_type() => __pbn__confirmation_type = null;
        private int? __pbn__confirmation_type;

        [global::ProtoBuf.ProtoMember(4)]
        public int confirmation_action
        {
            get => __pbn__confirmation_action.GetValueOrDefault();
            set => __pbn__confirmation_action = value;
        }
        public bool ShouldSerializeconfirmation_action() => __pbn__confirmation_action != null;
        public void Resetconfirmation_action() => __pbn__confirmation_action = null;
        private int? __pbn__confirmation_action;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_Status_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint state
        {
            get => __pbn__state.GetValueOrDefault();
            set => __pbn__state = value;
        }
        public bool ShouldSerializestate() => __pbn__state != null;
        public void Resetstate() => __pbn__state = null;
        private uint? __pbn__state;

        [global::ProtoBuf.ProtoMember(2)]
        public uint inactivation_reason
        {
            get => __pbn__inactivation_reason.GetValueOrDefault();
            set => __pbn__inactivation_reason = value;
        }
        public bool ShouldSerializeinactivation_reason() => __pbn__inactivation_reason != null;
        public void Resetinactivation_reason() => __pbn__inactivation_reason = null;
        private uint? __pbn__inactivation_reason;

        [global::ProtoBuf.ProtoMember(3)]
        public uint authenticator_type
        {
            get => __pbn__authenticator_type.GetValueOrDefault();
            set => __pbn__authenticator_type = value;
        }
        public bool ShouldSerializeauthenticator_type() => __pbn__authenticator_type != null;
        public void Resetauthenticator_type() => __pbn__authenticator_type = null;
        private uint? __pbn__authenticator_type;

        [global::ProtoBuf.ProtoMember(4)]
        public bool authenticator_allowed
        {
            get => __pbn__authenticator_allowed.GetValueOrDefault();
            set => __pbn__authenticator_allowed = value;
        }
        public bool ShouldSerializeauthenticator_allowed() => __pbn__authenticator_allowed != null;
        public void Resetauthenticator_allowed() => __pbn__authenticator_allowed = null;
        private bool? __pbn__authenticator_allowed;

        [global::ProtoBuf.ProtoMember(5)]
        public uint steamguard_scheme
        {
            get => __pbn__steamguard_scheme.GetValueOrDefault();
            set => __pbn__steamguard_scheme = value;
        }
        public bool ShouldSerializesteamguard_scheme() => __pbn__steamguard_scheme != null;
        public void Resetsteamguard_scheme() => __pbn__steamguard_scheme = null;
        private uint? __pbn__steamguard_scheme;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string token_gid
        {
            get => __pbn__token_gid ?? "";
            set => __pbn__token_gid = value;
        }
        public bool ShouldSerializetoken_gid() => __pbn__token_gid != null;
        public void Resettoken_gid() => __pbn__token_gid = null;
        private string __pbn__token_gid;

        [global::ProtoBuf.ProtoMember(7)]
        public bool email_validated
        {
            get => __pbn__email_validated.GetValueOrDefault();
            set => __pbn__email_validated = value;
        }
        public bool ShouldSerializeemail_validated() => __pbn__email_validated != null;
        public void Resetemail_validated() => __pbn__email_validated = null;
        private bool? __pbn__email_validated;

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue("")]
        public string device_identifier
        {
            get => __pbn__device_identifier ?? "";
            set => __pbn__device_identifier = value;
        }
        public bool ShouldSerializedevice_identifier() => __pbn__device_identifier != null;
        public void Resetdevice_identifier() => __pbn__device_identifier = null;
        private string __pbn__device_identifier;

        [global::ProtoBuf.ProtoMember(9)]
        public uint time_created
        {
            get => __pbn__time_created.GetValueOrDefault();
            set => __pbn__time_created = value;
        }
        public bool ShouldSerializetime_created() => __pbn__time_created != null;
        public void Resettime_created() => __pbn__time_created = null;
        private uint? __pbn__time_created;

        [global::ProtoBuf.ProtoMember(10)]
        public uint revocation_attempts_remaining
        {
            get => __pbn__revocation_attempts_remaining.GetValueOrDefault();
            set => __pbn__revocation_attempts_remaining = value;
        }
        public bool ShouldSerializerevocation_attempts_remaining() => __pbn__revocation_attempts_remaining != null;
        public void Resetrevocation_attempts_remaining() => __pbn__revocation_attempts_remaining = null;
        private uint? __pbn__revocation_attempts_remaining;

        [global::ProtoBuf.ProtoMember(11)]
        [global::System.ComponentModel.DefaultValue("")]
        public string classified_agent
        {
            get => __pbn__classified_agent ?? "";
            set => __pbn__classified_agent = value;
        }
        public bool ShouldSerializeclassified_agent() => __pbn__classified_agent != null;
        public void Resetclassified_agent() => __pbn__classified_agent = null;
        private string __pbn__classified_agent;

        [global::ProtoBuf.ProtoMember(12)]
        public bool allow_external_authenticator
        {
            get => __pbn__allow_external_authenticator.GetValueOrDefault();
            set => __pbn__allow_external_authenticator = value;
        }
        public bool ShouldSerializeallow_external_authenticator() => __pbn__allow_external_authenticator != null;
        public void Resetallow_external_authenticator() => __pbn__allow_external_authenticator = null;
        private bool? __pbn__allow_external_authenticator;

        [global::ProtoBuf.ProtoMember(13)]
        public uint time_transferred
        {
            get => __pbn__time_transferred.GetValueOrDefault();
            set => __pbn__time_transferred = value;
        }
        public bool ShouldSerializetime_transferred() => __pbn__time_transferred != null;
        public void Resettime_transferred() => __pbn__time_transferred = null;
        private uint? __pbn__time_transferred;

        [global::ProtoBuf.ProtoMember(14)]
        public uint version
        {
            get => __pbn__version.GetValueOrDefault();
            set => __pbn__version = value;
        }
        public bool ShouldSerializeversion() => __pbn__version != null;
        public void Resetversion() => __pbn__version = null;
        private uint? __pbn__version;

        [global::ProtoBuf.ProtoMember(15, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong last_seen_auth_token_id
        {
            get => __pbn__last_seen_auth_token_id.GetValueOrDefault();
            set => __pbn__last_seen_auth_token_id = value;
        }
        public bool ShouldSerializelast_seen_auth_token_id() => __pbn__last_seen_auth_token_id != null;
        public void Resetlast_seen_auth_token_id() => __pbn__last_seen_auth_token_id = null;
        private ulong? __pbn__last_seen_auth_token_id;

        [global::ProtoBuf.ProtoMember(16)]
        public global::System.Collections.Generic.List<CTwoFactor_UsageEvent> usages { get; } = new global::System.Collections.Generic.List<CTwoFactor_UsageEvent>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_AddAuthenticator_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(2)]
        public ulong authenticator_time
        {
            get => __pbn__authenticator_time.GetValueOrDefault();
            set => __pbn__authenticator_time = value;
        }
        public bool ShouldSerializeauthenticator_time() => __pbn__authenticator_time != null;
        public void Resetauthenticator_time() => __pbn__authenticator_time = null;
        private ulong? __pbn__authenticator_time;

        [global::ProtoBuf.ProtoMember(3, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong serial_number
        {
            get => __pbn__serial_number.GetValueOrDefault();
            set => __pbn__serial_number = value;
        }
        public bool ShouldSerializeserial_number() => __pbn__serial_number != null;
        public void Resetserial_number() => __pbn__serial_number = null;
        private ulong? __pbn__serial_number;

        [global::ProtoBuf.ProtoMember(4)]
        public uint authenticator_type
        {
            get => __pbn__authenticator_type.GetValueOrDefault();
            set => __pbn__authenticator_type = value;
        }
        public bool ShouldSerializeauthenticator_type() => __pbn__authenticator_type != null;
        public void Resetauthenticator_type() => __pbn__authenticator_type = null;
        private uint? __pbn__authenticator_type;

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue("")]
        public string device_identifier
        {
            get => __pbn__device_identifier ?? "";
            set => __pbn__device_identifier = value;
        }
        public bool ShouldSerializedevice_identifier() => __pbn__device_identifier != null;
        public void Resetdevice_identifier() => __pbn__device_identifier = null;
        private string __pbn__device_identifier;

        [global::ProtoBuf.ProtoMember(7)]
        public global::System.Collections.Generic.List<string> http_headers { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue(1u)]
        public uint version
        {
            get => __pbn__version ?? 1u;
            set => __pbn__version = value;
        }
        public bool ShouldSerializeversion() => __pbn__version != null;
        public void Resetversion() => __pbn__version = null;
        private uint? __pbn__version;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_AddAuthenticator_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public byte[] shared_secret
        {
            get => __pbn__shared_secret;
            set => __pbn__shared_secret = value;
        }
        public bool ShouldSerializeshared_secret() => __pbn__shared_secret != null;
        public void Resetshared_secret() => __pbn__shared_secret = null;
        private byte[] __pbn__shared_secret;

        [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong serial_number
        {
            get => __pbn__serial_number.GetValueOrDefault();
            set => __pbn__serial_number = value;
        }
        public bool ShouldSerializeserial_number() => __pbn__serial_number != null;
        public void Resetserial_number() => __pbn__serial_number = null;
        private ulong? __pbn__serial_number;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string revocation_code
        {
            get => __pbn__revocation_code ?? "";
            set => __pbn__revocation_code = value;
        }
        public bool ShouldSerializerevocation_code() => __pbn__revocation_code != null;
        public void Resetrevocation_code() => __pbn__revocation_code = null;
        private string __pbn__revocation_code;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string uri
        {
            get => __pbn__uri ?? "";
            set => __pbn__uri = value;
        }
        public bool ShouldSerializeuri() => __pbn__uri != null;
        public void Reseturi() => __pbn__uri = null;
        private string __pbn__uri;

        [global::ProtoBuf.ProtoMember(5)]
        public ulong server_time
        {
            get => __pbn__server_time.GetValueOrDefault();
            set => __pbn__server_time = value;
        }
        public bool ShouldSerializeserver_time() => __pbn__server_time != null;
        public void Resetserver_time() => __pbn__server_time = null;
        private ulong? __pbn__server_time;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string account_name
        {
            get => __pbn__account_name ?? "";
            set => __pbn__account_name = value;
        }
        public bool ShouldSerializeaccount_name() => __pbn__account_name != null;
        public void Resetaccount_name() => __pbn__account_name = null;
        private string __pbn__account_name;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string token_gid
        {
            get => __pbn__token_gid ?? "";
            set => __pbn__token_gid = value;
        }
        public bool ShouldSerializetoken_gid() => __pbn__token_gid != null;
        public void Resettoken_gid() => __pbn__token_gid = null;
        private string __pbn__token_gid;

        [global::ProtoBuf.ProtoMember(8)]
        public byte[] identity_secret
        {
            get => __pbn__identity_secret;
            set => __pbn__identity_secret = value;
        }
        public bool ShouldSerializeidentity_secret() => __pbn__identity_secret != null;
        public void Resetidentity_secret() => __pbn__identity_secret = null;
        private byte[] __pbn__identity_secret;

        [global::ProtoBuf.ProtoMember(9)]
        public byte[] secret_1
        {
            get => __pbn__secret_1;
            set => __pbn__secret_1 = value;
        }
        public bool ShouldSerializesecret_1() => __pbn__secret_1 != null;
        public void Resetsecret_1() => __pbn__secret_1 = null;
        private byte[] __pbn__secret_1;

        [global::ProtoBuf.ProtoMember(10)]
        public int status
        {
            get => __pbn__status.GetValueOrDefault();
            set => __pbn__status = value;
        }
        public bool ShouldSerializestatus() => __pbn__status != null;
        public void Resetstatus() => __pbn__status = null;
        private int? __pbn__status;

        [global::ProtoBuf.ProtoMember(11)]
        [global::System.ComponentModel.DefaultValue("")]
        public string phone_number_hint
        {
            get => __pbn__phone_number_hint ?? "";
            set => __pbn__phone_number_hint = value;
        }
        public bool ShouldSerializephone_number_hint() => __pbn__phone_number_hint != null;
        public void Resetphone_number_hint() => __pbn__phone_number_hint = null;
        private string __pbn__phone_number_hint;

        [global::ProtoBuf.ProtoMember(12)]
        public int confirm_type
        {
            get => __pbn__confirm_type.GetValueOrDefault();
            set => __pbn__confirm_type = value;
        }
        public bool ShouldSerializeconfirm_type() => __pbn__confirm_type != null;
        public void Resetconfirm_type() => __pbn__confirm_type = null;
        private int? __pbn__confirm_type;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_FinalizeAddAuthenticator_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string authenticator_code
        {
            get => __pbn__authenticator_code ?? "";
            set => __pbn__authenticator_code = value;
        }
        public bool ShouldSerializeauthenticator_code() => __pbn__authenticator_code != null;
        public void Resetauthenticator_code() => __pbn__authenticator_code = null;
        private string __pbn__authenticator_code;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong authenticator_time
        {
            get => __pbn__authenticator_time.GetValueOrDefault();
            set => __pbn__authenticator_time = value;
        }
        public bool ShouldSerializeauthenticator_time() => __pbn__authenticator_time != null;
        public void Resetauthenticator_time() => __pbn__authenticator_time = null;
        private ulong? __pbn__authenticator_time;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string activation_code
        {
            get => __pbn__activation_code ?? "";
            set => __pbn__activation_code = value;
        }
        public bool ShouldSerializeactivation_code() => __pbn__activation_code != null;
        public void Resetactivation_code() => __pbn__activation_code = null;
        private string __pbn__activation_code;

        [global::ProtoBuf.ProtoMember(5)]
        public global::System.Collections.Generic.List<string> http_headers { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(6)]
        public bool validate_sms_code
        {
            get => __pbn__validate_sms_code.GetValueOrDefault();
            set => __pbn__validate_sms_code = value;
        }
        public bool ShouldSerializevalidate_sms_code() => __pbn__validate_sms_code != null;
        public void Resetvalidate_sms_code() => __pbn__validate_sms_code = null;
        private bool? __pbn__validate_sms_code;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_FinalizeAddAuthenticator_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool success
        {
            get => __pbn__success.GetValueOrDefault();
            set => __pbn__success = value;
        }
        public bool ShouldSerializesuccess() => __pbn__success != null;
        public void Resetsuccess() => __pbn__success = null;
        private bool? __pbn__success;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong server_time
        {
            get => __pbn__server_time.GetValueOrDefault();
            set => __pbn__server_time = value;
        }
        public bool ShouldSerializeserver_time() => __pbn__server_time != null;
        public void Resetserver_time() => __pbn__server_time = null;
        private ulong? __pbn__server_time;

        [global::ProtoBuf.ProtoMember(4)]
        public int status
        {
            get => __pbn__status.GetValueOrDefault();
            set => __pbn__status = value;
        }
        public bool ShouldSerializestatus() => __pbn__status != null;
        public void Resetstatus() => __pbn__status = null;
        private int? __pbn__status;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_UpdateTokenVersion_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(2)]
        public uint version
        {
            get => __pbn__version.GetValueOrDefault();
            set => __pbn__version = value;
        }
        public bool ShouldSerializeversion() => __pbn__version != null;
        public void Resetversion() => __pbn__version = null;
        private uint? __pbn__version;

        [global::ProtoBuf.ProtoMember(3)]
        public byte[] signature
        {
            get => __pbn__signature;
            set => __pbn__signature = value;
        }
        public bool ShouldSerializesignature() => __pbn__signature != null;
        public void Resetsignature() => __pbn__signature = null;
        private byte[] __pbn__signature;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_UpdateTokenVersion_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_RemoveAuthenticator_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string revocation_code
        {
            get => __pbn__revocation_code ?? "";
            set => __pbn__revocation_code = value;
        }
        public bool ShouldSerializerevocation_code() => __pbn__revocation_code != null;
        public void Resetrevocation_code() => __pbn__revocation_code = null;
        private string __pbn__revocation_code;

        [global::ProtoBuf.ProtoMember(5)]
        public uint revocation_reason
        {
            get => __pbn__revocation_reason.GetValueOrDefault();
            set => __pbn__revocation_reason = value;
        }
        public bool ShouldSerializerevocation_reason() => __pbn__revocation_reason != null;
        public void Resetrevocation_reason() => __pbn__revocation_reason = null;
        private uint? __pbn__revocation_reason;

        [global::ProtoBuf.ProtoMember(6)]
        public uint steamguard_scheme
        {
            get => __pbn__steamguard_scheme.GetValueOrDefault();
            set => __pbn__steamguard_scheme = value;
        }
        public bool ShouldSerializesteamguard_scheme() => __pbn__steamguard_scheme != null;
        public void Resetsteamguard_scheme() => __pbn__steamguard_scheme = null;
        private uint? __pbn__steamguard_scheme;

        [global::ProtoBuf.ProtoMember(7)]
        public bool remove_all_steamguard_cookies
        {
            get => __pbn__remove_all_steamguard_cookies.GetValueOrDefault();
            set => __pbn__remove_all_steamguard_cookies = value;
        }
        public bool ShouldSerializeremove_all_steamguard_cookies() => __pbn__remove_all_steamguard_cookies != null;
        public void Resetremove_all_steamguard_cookies() => __pbn__remove_all_steamguard_cookies = null;
        private bool? __pbn__remove_all_steamguard_cookies;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_RemoveAuthenticator_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool success
        {
            get => __pbn__success.GetValueOrDefault();
            set => __pbn__success = value;
        }
        public bool ShouldSerializesuccess() => __pbn__success != null;
        public void Resetsuccess() => __pbn__success = null;
        private bool? __pbn__success;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong server_time
        {
            get => __pbn__server_time.GetValueOrDefault();
            set => __pbn__server_time = value;
        }
        public bool ShouldSerializeserver_time() => __pbn__server_time != null;
        public void Resetserver_time() => __pbn__server_time = null;
        private ulong? __pbn__server_time;

        [global::ProtoBuf.ProtoMember(5)]
        public uint revocation_attempts_remaining
        {
            get => __pbn__revocation_attempts_remaining.GetValueOrDefault();
            set => __pbn__revocation_attempts_remaining = value;
        }
        public bool ShouldSerializerevocation_attempts_remaining() => __pbn__revocation_attempts_remaining != null;
        public void Resetrevocation_attempts_remaining() => __pbn__revocation_attempts_remaining = null;
        private uint? __pbn__revocation_attempts_remaining;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_RemoveAuthenticatorViaChallengeStart_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_RemoveAuthenticatorViaChallengeStart_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool success
        {
            get => __pbn__success.GetValueOrDefault();
            set => __pbn__success = value;
        }
        public bool ShouldSerializesuccess() => __pbn__success != null;
        public void Resetsuccess() => __pbn__success = null;
        private bool? __pbn__success;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_RemoveAuthenticatorViaChallengeContinue_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string sms_code
        {
            get => __pbn__sms_code ?? "";
            set => __pbn__sms_code = value;
        }
        public bool ShouldSerializesms_code() => __pbn__sms_code != null;
        public void Resetsms_code() => __pbn__sms_code = null;
        private string __pbn__sms_code;

        [global::ProtoBuf.ProtoMember(2)]
        public bool generate_new_token
        {
            get => __pbn__generate_new_token.GetValueOrDefault();
            set => __pbn__generate_new_token = value;
        }
        public bool ShouldSerializegenerate_new_token() => __pbn__generate_new_token != null;
        public void Resetgenerate_new_token() => __pbn__generate_new_token = null;
        private bool? __pbn__generate_new_token;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue(1u)]
        public uint version
        {
            get => __pbn__version ?? 1u;
            set => __pbn__version = value;
        }
        public bool ShouldSerializeversion() => __pbn__version != null;
        public void Resetversion() => __pbn__version = null;
        private uint? __pbn__version;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CRemoveAuthenticatorViaChallengeContinue_Replacement_Token : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public byte[] shared_secret
        {
            get => __pbn__shared_secret;
            set => __pbn__shared_secret = value;
        }
        public bool ShouldSerializeshared_secret() => __pbn__shared_secret != null;
        public void Resetshared_secret() => __pbn__shared_secret = null;
        private byte[] __pbn__shared_secret;

        [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong serial_number
        {
            get => __pbn__serial_number.GetValueOrDefault();
            set => __pbn__serial_number = value;
        }
        public bool ShouldSerializeserial_number() => __pbn__serial_number != null;
        public void Resetserial_number() => __pbn__serial_number = null;
        private ulong? __pbn__serial_number;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string revocation_code
        {
            get => __pbn__revocation_code ?? "";
            set => __pbn__revocation_code = value;
        }
        public bool ShouldSerializerevocation_code() => __pbn__revocation_code != null;
        public void Resetrevocation_code() => __pbn__revocation_code = null;
        private string __pbn__revocation_code;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string uri
        {
            get => __pbn__uri ?? "";
            set => __pbn__uri = value;
        }
        public bool ShouldSerializeuri() => __pbn__uri != null;
        public void Reseturi() => __pbn__uri = null;
        private string __pbn__uri;

        [global::ProtoBuf.ProtoMember(5)]
        public ulong server_time
        {
            get => __pbn__server_time.GetValueOrDefault();
            set => __pbn__server_time = value;
        }
        public bool ShouldSerializeserver_time() => __pbn__server_time != null;
        public void Resetserver_time() => __pbn__server_time = null;
        private ulong? __pbn__server_time;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string account_name
        {
            get => __pbn__account_name ?? "";
            set => __pbn__account_name = value;
        }
        public bool ShouldSerializeaccount_name() => __pbn__account_name != null;
        public void Resetaccount_name() => __pbn__account_name = null;
        private string __pbn__account_name;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string token_gid
        {
            get => __pbn__token_gid ?? "";
            set => __pbn__token_gid = value;
        }
        public bool ShouldSerializetoken_gid() => __pbn__token_gid != null;
        public void Resettoken_gid() => __pbn__token_gid = null;
        private string __pbn__token_gid;

        [global::ProtoBuf.ProtoMember(8)]
        public byte[] identity_secret
        {
            get => __pbn__identity_secret;
            set => __pbn__identity_secret = value;
        }
        public bool ShouldSerializeidentity_secret() => __pbn__identity_secret != null;
        public void Resetidentity_secret() => __pbn__identity_secret = null;
        private byte[] __pbn__identity_secret;

        [global::ProtoBuf.ProtoMember(9)]
        public byte[] secret_1
        {
            get => __pbn__secret_1;
            set => __pbn__secret_1 = value;
        }
        public bool ShouldSerializesecret_1() => __pbn__secret_1 != null;
        public void Resetsecret_1() => __pbn__secret_1 = null;
        private byte[] __pbn__secret_1;

        [global::ProtoBuf.ProtoMember(10)]
        public int status
        {
            get => __pbn__status.GetValueOrDefault();
            set => __pbn__status = value;
        }
        public bool ShouldSerializestatus() => __pbn__status != null;
        public void Resetstatus() => __pbn__status = null;
        private int? __pbn__status;

        [global::ProtoBuf.ProtoMember(11)]
        public uint steamguard_scheme
        {
            get => __pbn__steamguard_scheme.GetValueOrDefault();
            set => __pbn__steamguard_scheme = value;
        }
        public bool ShouldSerializesteamguard_scheme() => __pbn__steamguard_scheme != null;
        public void Resetsteamguard_scheme() => __pbn__steamguard_scheme = null;
        private uint? __pbn__steamguard_scheme;

        [global::ProtoBuf.ProtoMember(12, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_RemoveAuthenticatorViaChallengeContinue_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool success
        {
            get => __pbn__success.GetValueOrDefault();
            set => __pbn__success = value;
        }
        public bool ShouldSerializesuccess() => __pbn__success != null;
        public void Resetsuccess() => __pbn__success = null;
        private bool? __pbn__success;

        [global::ProtoBuf.ProtoMember(2)]
        public CRemoveAuthenticatorViaChallengeContinue_Replacement_Token replacement_token { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_CreateEmergencyCodes_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string code
        {
            get => __pbn__code ?? "";
            set => __pbn__code = value;
        }
        public bool ShouldSerializecode() => __pbn__code != null;
        public void Resetcode() => __pbn__code = null;
        private string __pbn__code;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_CreateEmergencyCodes_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<string> codes { get; } = new global::System.Collections.Generic.List<string>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_DestroyEmergencyCodes_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_DestroyEmergencyCodes_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_ValidateToken_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string code
        {
            get => __pbn__code ?? "";
            set => __pbn__code = value;
        }
        public bool ShouldSerializecode() => __pbn__code != null;
        public void Resetcode() => __pbn__code = null;
        private string __pbn__code;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CTwoFactor_ValidateToken_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool valid
        {
            get => __pbn__valid.GetValueOrDefault();
            set => __pbn__valid = value;
        }
        public bool ShouldSerializevalid() => __pbn__valid != null;
        public void Resetvalid() => __pbn__valid = null;
        private bool? __pbn__valid;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_GetAvailableValveDiscountPromotions_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string country_code
        {
            get => __pbn__country_code ?? "";
            set => __pbn__country_code = value;
        }
        public bool ShouldSerializecountry_code() => __pbn__country_code != null;
        public void Resetcountry_code() => __pbn__country_code = null;
        private string __pbn__country_code;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_GetAvailableValveDiscountPromotions_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<ValveDiscountPromotionDetails> promotions { get; } = new global::System.Collections.Generic.List<ValveDiscountPromotionDetails>();

        [global::ProtoBuf.ProtoContract()]
        public partial class ValveDiscountPromotionDetails : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public uint promotionid
            {
                get => __pbn__promotionid.GetValueOrDefault();
                set => __pbn__promotionid = value;
            }
            public bool ShouldSerializepromotionid() => __pbn__promotionid != null;
            public void Resetpromotionid() => __pbn__promotionid = null;
            private uint? __pbn__promotionid;

            [global::ProtoBuf.ProtoMember(2)]
            [global::System.ComponentModel.DefaultValue("")]
            public string promotion_description
            {
                get => __pbn__promotion_description ?? "";
                set => __pbn__promotion_description = value;
            }
            public bool ShouldSerializepromotion_description() => __pbn__promotion_description != null;
            public void Resetpromotion_description() => __pbn__promotion_description = null;
            private string __pbn__promotion_description;

            [global::ProtoBuf.ProtoMember(3)]
            public long minimum_cart_amount
            {
                get => __pbn__minimum_cart_amount.GetValueOrDefault();
                set => __pbn__minimum_cart_amount = value;
            }
            public bool ShouldSerializeminimum_cart_amount() => __pbn__minimum_cart_amount != null;
            public void Resetminimum_cart_amount() => __pbn__minimum_cart_amount = null;
            private long? __pbn__minimum_cart_amount;

            [global::ProtoBuf.ProtoMember(4)]
            public long minimum_cart_amount_for_display
            {
                get => __pbn__minimum_cart_amount_for_display.GetValueOrDefault();
                set => __pbn__minimum_cart_amount_for_display = value;
            }
            public bool ShouldSerializeminimum_cart_amount_for_display() => __pbn__minimum_cart_amount_for_display != null;
            public void Resetminimum_cart_amount_for_display() => __pbn__minimum_cart_amount_for_display = null;
            private long? __pbn__minimum_cart_amount_for_display;

            [global::ProtoBuf.ProtoMember(5)]
            public long discount_amount
            {
                get => __pbn__discount_amount.GetValueOrDefault();
                set => __pbn__discount_amount = value;
            }
            public bool ShouldSerializediscount_amount() => __pbn__discount_amount != null;
            public void Resetdiscount_amount() => __pbn__discount_amount = null;
            private long? __pbn__discount_amount;

            [global::ProtoBuf.ProtoMember(6)]
            public int currency_code
            {
                get => __pbn__currency_code.GetValueOrDefault();
                set => __pbn__currency_code = value;
            }
            public bool ShouldSerializecurrency_code() => __pbn__currency_code != null;
            public void Resetcurrency_code() => __pbn__currency_code = null;
            private int? __pbn__currency_code;

            [global::ProtoBuf.ProtoMember(7)]
            public int available_use_count
            {
                get => __pbn__available_use_count.GetValueOrDefault();
                set => __pbn__available_use_count = value;
            }
            public bool ShouldSerializeavailable_use_count() => __pbn__available_use_count != null;
            public void Resetavailable_use_count() => __pbn__available_use_count = null;
            private int? __pbn__available_use_count;

            [global::ProtoBuf.ProtoMember(8)]
            public int promotional_discount_type
            {
                get => __pbn__promotional_discount_type.GetValueOrDefault();
                set => __pbn__promotional_discount_type = value;
            }
            public bool ShouldSerializepromotional_discount_type() => __pbn__promotional_discount_type != null;
            public void Resetpromotional_discount_type() => __pbn__promotional_discount_type = null;
            private int? __pbn__promotional_discount_type;

            [global::ProtoBuf.ProtoMember(9)]
            public int loyalty_reward_id
            {
                get => __pbn__loyalty_reward_id.GetValueOrDefault();
                set => __pbn__loyalty_reward_id = value;
            }
            public bool ShouldSerializeloyalty_reward_id() => __pbn__loyalty_reward_id != null;
            public void Resetloyalty_reward_id() => __pbn__loyalty_reward_id = null;
            private int? __pbn__loyalty_reward_id;

            [global::ProtoBuf.ProtoMember(10)]
            [global::System.ComponentModel.DefaultValue("")]
            public string localized_name_token
            {
                get => __pbn__localized_name_token ?? "";
                set => __pbn__localized_name_token = value;
            }
            public bool ShouldSerializelocalized_name_token() => __pbn__localized_name_token != null;
            public void Resetlocalized_name_token() => __pbn__localized_name_token = null;
            private string __pbn__localized_name_token;

            [global::ProtoBuf.ProtoMember(11)]
            public int max_use_count
            {
                get => __pbn__max_use_count.GetValueOrDefault();
                set => __pbn__max_use_count = value;
            }
            public bool ShouldSerializemax_use_count() => __pbn__max_use_count != null;
            public void Resetmax_use_count() => __pbn__max_use_count = null;
            private int? __pbn__max_use_count;

        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_GetClientWalletDetails_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool include_balance_in_usd
        {
            get => __pbn__include_balance_in_usd.GetValueOrDefault();
            set => __pbn__include_balance_in_usd = value;
        }
        public bool ShouldSerializeinclude_balance_in_usd() => __pbn__include_balance_in_usd != null;
        public void Resetinclude_balance_in_usd() => __pbn__include_balance_in_usd = null;
        private bool? __pbn__include_balance_in_usd;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue(1)]
        public int wallet_region
        {
            get => __pbn__wallet_region ?? 1;
            set => __pbn__wallet_region = value;
        }
        public bool ShouldSerializewallet_region() => __pbn__wallet_region != null;
        public void Resetwallet_region() => __pbn__wallet_region = null;
        private int? __pbn__wallet_region;

        [global::ProtoBuf.ProtoMember(3)]
        public bool include_formatted_balance
        {
            get => __pbn__include_formatted_balance.GetValueOrDefault();
            set => __pbn__include_formatted_balance = value;
        }
        public bool ShouldSerializeinclude_formatted_balance() => __pbn__include_formatted_balance != null;
        public void Resetinclude_formatted_balance() => __pbn__include_formatted_balance = null;
        private bool? __pbn__include_formatted_balance;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_GetWalletDetails_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool has_wallet
        {
            get => __pbn__has_wallet.GetValueOrDefault();
            set => __pbn__has_wallet = value;
        }
        public bool ShouldSerializehas_wallet() => __pbn__has_wallet != null;
        public void Resethas_wallet() => __pbn__has_wallet = null;
        private bool? __pbn__has_wallet;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string user_country_code
        {
            get => __pbn__user_country_code ?? "";
            set => __pbn__user_country_code = value;
        }
        public bool ShouldSerializeuser_country_code() => __pbn__user_country_code != null;
        public void Resetuser_country_code() => __pbn__user_country_code = null;
        private string __pbn__user_country_code;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string wallet_country_code
        {
            get => __pbn__wallet_country_code ?? "";
            set => __pbn__wallet_country_code = value;
        }
        public bool ShouldSerializewallet_country_code() => __pbn__wallet_country_code != null;
        public void Resetwallet_country_code() => __pbn__wallet_country_code = null;
        private string __pbn__wallet_country_code;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string wallet_state
        {
            get => __pbn__wallet_state ?? "";
            set => __pbn__wallet_state = value;
        }
        public bool ShouldSerializewallet_state() => __pbn__wallet_state != null;
        public void Resetwallet_state() => __pbn__wallet_state = null;
        private string __pbn__wallet_state;

        [global::ProtoBuf.ProtoMember(5)]
        public long balance
        {
            get => __pbn__balance.GetValueOrDefault();
            set => __pbn__balance = value;
        }
        public bool ShouldSerializebalance() => __pbn__balance != null;
        public void Resetbalance() => __pbn__balance = null;
        private long? __pbn__balance;

        [global::ProtoBuf.ProtoMember(6)]
        public long delayed_balance
        {
            get => __pbn__delayed_balance.GetValueOrDefault();
            set => __pbn__delayed_balance = value;
        }
        public bool ShouldSerializedelayed_balance() => __pbn__delayed_balance != null;
        public void Resetdelayed_balance() => __pbn__delayed_balance = null;
        private long? __pbn__delayed_balance;

        [global::ProtoBuf.ProtoMember(7)]
        public int currency_code
        {
            get => __pbn__currency_code.GetValueOrDefault();
            set => __pbn__currency_code = value;
        }
        public bool ShouldSerializecurrency_code() => __pbn__currency_code != null;
        public void Resetcurrency_code() => __pbn__currency_code = null;
        private int? __pbn__currency_code;

        [global::ProtoBuf.ProtoMember(8)]
        public uint time_most_recent_txn
        {
            get => __pbn__time_most_recent_txn.GetValueOrDefault();
            set => __pbn__time_most_recent_txn = value;
        }
        public bool ShouldSerializetime_most_recent_txn() => __pbn__time_most_recent_txn != null;
        public void Resettime_most_recent_txn() => __pbn__time_most_recent_txn = null;
        private uint? __pbn__time_most_recent_txn;

        [global::ProtoBuf.ProtoMember(9)]
        public ulong most_recent_txnid
        {
            get => __pbn__most_recent_txnid.GetValueOrDefault();
            set => __pbn__most_recent_txnid = value;
        }
        public bool ShouldSerializemost_recent_txnid() => __pbn__most_recent_txnid != null;
        public void Resetmost_recent_txnid() => __pbn__most_recent_txnid = null;
        private ulong? __pbn__most_recent_txnid;

        [global::ProtoBuf.ProtoMember(10)]
        public long balance_in_usd
        {
            get => __pbn__balance_in_usd.GetValueOrDefault();
            set => __pbn__balance_in_usd = value;
        }
        public bool ShouldSerializebalance_in_usd() => __pbn__balance_in_usd != null;
        public void Resetbalance_in_usd() => __pbn__balance_in_usd = null;
        private long? __pbn__balance_in_usd;

        [global::ProtoBuf.ProtoMember(11)]
        public long delayed_balance_in_usd
        {
            get => __pbn__delayed_balance_in_usd.GetValueOrDefault();
            set => __pbn__delayed_balance_in_usd = value;
        }
        public bool ShouldSerializedelayed_balance_in_usd() => __pbn__delayed_balance_in_usd != null;
        public void Resetdelayed_balance_in_usd() => __pbn__delayed_balance_in_usd = null;
        private long? __pbn__delayed_balance_in_usd;

        [global::ProtoBuf.ProtoMember(12)]
        public bool has_wallet_in_other_regions
        {
            get => __pbn__has_wallet_in_other_regions.GetValueOrDefault();
            set => __pbn__has_wallet_in_other_regions = value;
        }
        public bool ShouldSerializehas_wallet_in_other_regions() => __pbn__has_wallet_in_other_regions != null;
        public void Resethas_wallet_in_other_regions() => __pbn__has_wallet_in_other_regions = null;
        private bool? __pbn__has_wallet_in_other_regions;

        [global::ProtoBuf.ProtoMember(13)]
        public global::System.Collections.Generic.List<int> other_regions { get; } = new global::System.Collections.Generic.List<int>();

        [global::ProtoBuf.ProtoMember(14)]
        [global::System.ComponentModel.DefaultValue("")]
        public string formatted_balance
        {
            get => __pbn__formatted_balance ?? "";
            set => __pbn__formatted_balance = value;
        }
        public bool ShouldSerializeformatted_balance() => __pbn__formatted_balance != null;
        public void Resetformatted_balance() => __pbn__formatted_balance = null;
        private string __pbn__formatted_balance;

        [global::ProtoBuf.ProtoMember(15)]
        [global::System.ComponentModel.DefaultValue("")]
        public string formatted_delayed_balance
        {
            get => __pbn__formatted_delayed_balance ?? "";
            set => __pbn__formatted_delayed_balance = value;
        }
        public bool ShouldSerializeformatted_delayed_balance() => __pbn__formatted_delayed_balance != null;
        public void Resetformatted_delayed_balance() => __pbn__formatted_delayed_balance = null;
        private string __pbn__formatted_delayed_balance;

        [global::ProtoBuf.ProtoMember(16)]
        public int delayed_balance_available_min_time
        {
            get => __pbn__delayed_balance_available_min_time.GetValueOrDefault();
            set => __pbn__delayed_balance_available_min_time = value;
        }
        public bool ShouldSerializedelayed_balance_available_min_time() => __pbn__delayed_balance_available_min_time != null;
        public void Resetdelayed_balance_available_min_time() => __pbn__delayed_balance_available_min_time = null;
        private int? __pbn__delayed_balance_available_min_time;

        [global::ProtoBuf.ProtoMember(17)]
        public int delayed_balance_available_max_time
        {
            get => __pbn__delayed_balance_available_max_time.GetValueOrDefault();
            set => __pbn__delayed_balance_available_max_time = value;
        }
        public bool ShouldSerializedelayed_balance_available_max_time() => __pbn__delayed_balance_available_max_time != null;
        public void Resetdelayed_balance_available_max_time() => __pbn__delayed_balance_available_max_time = null;
        private int? __pbn__delayed_balance_available_max_time;

        [global::ProtoBuf.ProtoMember(18)]
        public int delayed_balance_newest_source
        {
            get => __pbn__delayed_balance_newest_source.GetValueOrDefault();
            set => __pbn__delayed_balance_newest_source = value;
        }
        public bool ShouldSerializedelayed_balance_newest_source() => __pbn__delayed_balance_newest_source != null;
        public void Resetdelayed_balance_newest_source() => __pbn__delayed_balance_newest_source = null;
        private int? __pbn__delayed_balance_newest_source;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_GetAccountLinkStatus_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_GetAccountLinkStatus_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint pwid
        {
            get => __pbn__pwid.GetValueOrDefault();
            set => __pbn__pwid = value;
        }
        public bool ShouldSerializepwid() => __pbn__pwid != null;
        public void Resetpwid() => __pbn__pwid = null;
        private uint? __pbn__pwid;

        [global::ProtoBuf.ProtoMember(2)]
        public uint identity_verification
        {
            get => __pbn__identity_verification.GetValueOrDefault();
            set => __pbn__identity_verification = value;
        }
        public bool ShouldSerializeidentity_verification() => __pbn__identity_verification != null;
        public void Resetidentity_verification() => __pbn__identity_verification = null;
        private uint? __pbn__identity_verification;

        [global::ProtoBuf.ProtoMember(3)]
        public bool performed_age_verification
        {
            get => __pbn__performed_age_verification.GetValueOrDefault();
            set => __pbn__performed_age_verification = value;
        }
        public bool ShouldSerializeperformed_age_verification() => __pbn__performed_age_verification != null;
        public void Resetperformed_age_verification() => __pbn__performed_age_verification = null;
        private bool? __pbn__performed_age_verification;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_CancelLicenseForApp_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_CancelLicenseForApp_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_GetUserCountry_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_GetUserCountry_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string country
        {
            get => __pbn__country ?? "";
            set => __pbn__country = value;
        }
        public bool ShouldSerializecountry() => __pbn__country != null;
        public void Resetcountry() => __pbn__country = null;
        private string __pbn__country;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_CreateFriendInviteToken_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint invite_limit
        {
            get => __pbn__invite_limit.GetValueOrDefault();
            set => __pbn__invite_limit = value;
        }
        public bool ShouldSerializeinvite_limit() => __pbn__invite_limit != null;
        public void Resetinvite_limit() => __pbn__invite_limit = null;
        private uint? __pbn__invite_limit;

        [global::ProtoBuf.ProtoMember(2)]
        public uint invite_duration
        {
            get => __pbn__invite_duration.GetValueOrDefault();
            set => __pbn__invite_duration = value;
        }
        public bool ShouldSerializeinvite_duration() => __pbn__invite_duration != null;
        public void Resetinvite_duration() => __pbn__invite_duration = null;
        private uint? __pbn__invite_duration;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string invite_note
        {
            get => __pbn__invite_note ?? "";
            set => __pbn__invite_note = value;
        }
        public bool ShouldSerializeinvite_note() => __pbn__invite_note != null;
        public void Resetinvite_note() => __pbn__invite_note = null;
        private string __pbn__invite_note;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_CreateFriendInviteToken_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string invite_token
        {
            get => __pbn__invite_token ?? "";
            set => __pbn__invite_token = value;
        }
        public bool ShouldSerializeinvite_token() => __pbn__invite_token != null;
        public void Resetinvite_token() => __pbn__invite_token = null;
        private string __pbn__invite_token;

        [global::ProtoBuf.ProtoMember(2)]
        public ulong invite_limit
        {
            get => __pbn__invite_limit.GetValueOrDefault();
            set => __pbn__invite_limit = value;
        }
        public bool ShouldSerializeinvite_limit() => __pbn__invite_limit != null;
        public void Resetinvite_limit() => __pbn__invite_limit = null;
        private ulong? __pbn__invite_limit;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong invite_duration
        {
            get => __pbn__invite_duration.GetValueOrDefault();
            set => __pbn__invite_duration = value;
        }
        public bool ShouldSerializeinvite_duration() => __pbn__invite_duration != null;
        public void Resetinvite_duration() => __pbn__invite_duration = null;
        private ulong? __pbn__invite_duration;

        [global::ProtoBuf.ProtoMember(4, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public uint time_created
        {
            get => __pbn__time_created.GetValueOrDefault();
            set => __pbn__time_created = value;
        }
        public bool ShouldSerializetime_created() => __pbn__time_created != null;
        public void Resettime_created() => __pbn__time_created = null;
        private uint? __pbn__time_created;

        [global::ProtoBuf.ProtoMember(5)]
        public bool valid
        {
            get => __pbn__valid.GetValueOrDefault();
            set => __pbn__valid = value;
        }
        public bool ShouldSerializevalid() => __pbn__valid != null;
        public void Resetvalid() => __pbn__valid = null;
        private bool? __pbn__valid;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_GetFriendInviteTokens_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_GetFriendInviteTokens_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<CUserAccount_CreateFriendInviteToken_Response> tokens { get; } = new global::System.Collections.Generic.List<CUserAccount_CreateFriendInviteToken_Response>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_ViewFriendInviteToken_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string invite_token
        {
            get => __pbn__invite_token ?? "";
            set => __pbn__invite_token = value;
        }
        public bool ShouldSerializeinvite_token() => __pbn__invite_token != null;
        public void Resetinvite_token() => __pbn__invite_token = null;
        private string __pbn__invite_token;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_ViewFriendInviteToken_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool valid
        {
            get => __pbn__valid.GetValueOrDefault();
            set => __pbn__valid = value;
        }
        public bool ShouldSerializevalid() => __pbn__valid != null;
        public void Resetvalid() => __pbn__valid = null;
        private bool? __pbn__valid;

        [global::ProtoBuf.ProtoMember(2)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong invite_duration
        {
            get => __pbn__invite_duration.GetValueOrDefault();
            set => __pbn__invite_duration = value;
        }
        public bool ShouldSerializeinvite_duration() => __pbn__invite_duration != null;
        public void Resetinvite_duration() => __pbn__invite_duration = null;
        private ulong? __pbn__invite_duration;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_RedeemFriendInviteToken_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string invite_token
        {
            get => __pbn__invite_token ?? "";
            set => __pbn__invite_token = value;
        }
        public bool ShouldSerializeinvite_token() => __pbn__invite_token != null;
        public void Resetinvite_token() => __pbn__invite_token = null;
        private string __pbn__invite_token;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_RedeemFriendInviteToken_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_RevokeFriendInviteToken_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string invite_token
        {
            get => __pbn__invite_token ?? "";
            set => __pbn__invite_token = value;
        }
        public bool ShouldSerializeinvite_token() => __pbn__invite_token != null;
        public void Resetinvite_token() => __pbn__invite_token = null;
        private string __pbn__invite_token;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_RevokeFriendInviteToken_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_RegisterCompatTool_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint compat_tool
        {
            get => __pbn__compat_tool.GetValueOrDefault();
            set => __pbn__compat_tool = value;
        }
        public bool ShouldSerializecompat_tool() => __pbn__compat_tool != null;
        public void Resetcompat_tool() => __pbn__compat_tool = null;
        private uint? __pbn__compat_tool;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CUserAccount_RegisterCompatTool_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEcon_GetInventoryItemsWithDescriptions_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(2)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong contextid
        {
            get => __pbn__contextid.GetValueOrDefault();
            set => __pbn__contextid = value;
        }
        public bool ShouldSerializecontextid() => __pbn__contextid != null;
        public void Resetcontextid() => __pbn__contextid = null;
        private ulong? __pbn__contextid;

        [global::ProtoBuf.ProtoMember(4)]
        public bool get_descriptions
        {
            get => __pbn__get_descriptions.GetValueOrDefault();
            set => __pbn__get_descriptions = value;
        }
        public bool ShouldSerializeget_descriptions() => __pbn__get_descriptions != null;
        public void Resetget_descriptions() => __pbn__get_descriptions = null;
        private bool? __pbn__get_descriptions;

        [global::ProtoBuf.ProtoMember(11)]
        public bool get_asset_properties
        {
            get => __pbn__get_asset_properties.GetValueOrDefault();
            set => __pbn__get_asset_properties = value;
        }
        public bool ShouldSerializeget_asset_properties() => __pbn__get_asset_properties != null;
        public void Resetget_asset_properties() => __pbn__get_asset_properties = null;
        private bool? __pbn__get_asset_properties;

        [global::ProtoBuf.ProtoMember(10)]
        public bool for_trade_offer_verification
        {
            get => __pbn__for_trade_offer_verification.GetValueOrDefault();
            set => __pbn__for_trade_offer_verification = value;
        }
        public bool ShouldSerializefor_trade_offer_verification() => __pbn__for_trade_offer_verification != null;
        public void Resetfor_trade_offer_verification() => __pbn__for_trade_offer_verification = null;
        private bool? __pbn__for_trade_offer_verification;

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue("")]
        public string language
        {
            get => __pbn__language ?? "";
            set => __pbn__language = value;
        }
        public bool ShouldSerializelanguage() => __pbn__language != null;
        public void Resetlanguage() => __pbn__language = null;
        private string __pbn__language;

        [global::ProtoBuf.ProtoMember(6)]
        public FilterOptions filters { get; set; }

        [global::ProtoBuf.ProtoMember(8)]
        public ulong start_assetid
        {
            get => __pbn__start_assetid.GetValueOrDefault();
            set => __pbn__start_assetid = value;
        }
        public bool ShouldSerializestart_assetid() => __pbn__start_assetid != null;
        public void Resetstart_assetid() => __pbn__start_assetid = null;
        private ulong? __pbn__start_assetid;

        [global::ProtoBuf.ProtoMember(9)]
        public int count
        {
            get => __pbn__count.GetValueOrDefault();
            set => __pbn__count = value;
        }
        public bool ShouldSerializecount() => __pbn__count != null;
        public void Resetcount() => __pbn__count = null;
        private int? __pbn__count;

        [global::ProtoBuf.ProtoContract()]
        public partial class FilterOptions : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public global::System.Collections.Generic.List<ulong> assetids { get; } = new global::System.Collections.Generic.List<ulong>();

            [global::ProtoBuf.ProtoMember(2)]
            public global::System.Collections.Generic.List<uint> currencyids { get; } = new global::System.Collections.Generic.List<uint>();

            [global::ProtoBuf.ProtoMember(3)]
            public bool tradable_only
            {
                get => __pbn__tradable_only.GetValueOrDefault();
                set => __pbn__tradable_only = value;
            }
            public bool ShouldSerializetradable_only() => __pbn__tradable_only != null;
            public void Resettradable_only() => __pbn__tradable_only = null;
            private bool? __pbn__tradable_only;

            [global::ProtoBuf.ProtoMember(4)]
            public bool marketable_only
            {
                get => __pbn__marketable_only.GetValueOrDefault();
                set => __pbn__marketable_only = value;
            }
            public bool ShouldSerializemarketable_only() => __pbn__marketable_only != null;
            public void Resetmarketable_only() => __pbn__marketable_only = null;
            private bool? __pbn__marketable_only;

        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEcon_Asset : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

        [global::ProtoBuf.ProtoMember(2)]
        public ulong contextid
        {
            get => __pbn__contextid.GetValueOrDefault();
            set => __pbn__contextid = value;
        }
        public bool ShouldSerializecontextid() => __pbn__contextid != null;
        public void Resetcontextid() => __pbn__contextid = null;
        private ulong? __pbn__contextid;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong assetid
        {
            get => __pbn__assetid.GetValueOrDefault();
            set => __pbn__assetid = value;
        }
        public bool ShouldSerializeassetid() => __pbn__assetid != null;
        public void Resetassetid() => __pbn__assetid = null;
        private ulong? __pbn__assetid;

        [global::ProtoBuf.ProtoMember(4)]
        public ulong classid
        {
            get => __pbn__classid.GetValueOrDefault();
            set => __pbn__classid = value;
        }
        public bool ShouldSerializeclassid() => __pbn__classid != null;
        public void Resetclassid() => __pbn__classid = null;
        private ulong? __pbn__classid;

        [global::ProtoBuf.ProtoMember(5)]
        public ulong instanceid
        {
            get => __pbn__instanceid.GetValueOrDefault();
            set => __pbn__instanceid = value;
        }
        public bool ShouldSerializeinstanceid() => __pbn__instanceid != null;
        public void Resetinstanceid() => __pbn__instanceid = null;
        private ulong? __pbn__instanceid;

        [global::ProtoBuf.ProtoMember(6)]
        public uint currencyid
        {
            get => __pbn__currencyid.GetValueOrDefault();
            set => __pbn__currencyid = value;
        }
        public bool ShouldSerializecurrencyid() => __pbn__currencyid != null;
        public void Resetcurrencyid() => __pbn__currencyid = null;
        private uint? __pbn__currencyid;

        [global::ProtoBuf.ProtoMember(7)]
        public long amount
        {
            get => __pbn__amount.GetValueOrDefault();
            set => __pbn__amount = value;
        }
        public bool ShouldSerializeamount() => __pbn__amount != null;
        public void Resetamount() => __pbn__amount = null;
        private long? __pbn__amount;

        [global::ProtoBuf.ProtoMember(8)]
        public bool missing
        {
            get => __pbn__missing.GetValueOrDefault();
            set => __pbn__missing = value;
        }
        public bool ShouldSerializemissing() => __pbn__missing != null;
        public void Resetmissing() => __pbn__missing = null;
        private bool? __pbn__missing;

        [global::ProtoBuf.ProtoMember(9)]
        public long est_usd
        {
            get => __pbn__est_usd.GetValueOrDefault();
            set => __pbn__est_usd = value;
        }
        public bool ShouldSerializeest_usd() => __pbn__est_usd != null;
        public void Resetest_usd() => __pbn__est_usd = null;
        private long? __pbn__est_usd;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEconItem_DescriptionLine : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string type
        {
            get => __pbn__type ?? "";
            set => __pbn__type = value;
        }
        public bool ShouldSerializetype() => __pbn__type != null;
        public void Resettype() => __pbn__type = null;
        private string __pbn__type;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string value
        {
            get => __pbn__value ?? "";
            set => __pbn__value = value;
        }
        public bool ShouldSerializevalue() => __pbn__value != null;
        public void Resetvalue() => __pbn__value = null;
        private string __pbn__value;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string color
        {
            get => __pbn__color ?? "";
            set => __pbn__color = value;
        }
        public bool ShouldSerializecolor() => __pbn__color != null;
        public void Resetcolor() => __pbn__color = null;
        private string __pbn__color;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string label
        {
            get => __pbn__label ?? "";
            set => __pbn__label = value;
        }
        public bool ShouldSerializelabel() => __pbn__label != null;
        public void Resetlabel() => __pbn__label = null;
        private string __pbn__label;

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue("")]
        public string name
        {
            get => __pbn__name ?? "";
            set => __pbn__name = value;
        }
        public bool ShouldSerializename() => __pbn__name != null;
        public void Resetname() => __pbn__name = null;
        private string __pbn__name;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEconItem_Action : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string link
        {
            get => __pbn__link ?? "";
            set => __pbn__link = value;
        }
        public bool ShouldSerializelink() => __pbn__link != null;
        public void Resetlink() => __pbn__link = null;
        private string __pbn__link;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string name
        {
            get => __pbn__name ?? "";
            set => __pbn__name = value;
        }
        public bool ShouldSerializename() => __pbn__name != null;
        public void Resetname() => __pbn__name = null;
        private string __pbn__name;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEconItem_Description : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public int appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private int? __pbn__appid;

        [global::ProtoBuf.ProtoMember(2)]
        public ulong classid
        {
            get => __pbn__classid.GetValueOrDefault();
            set => __pbn__classid = value;
        }
        public bool ShouldSerializeclassid() => __pbn__classid != null;
        public void Resetclassid() => __pbn__classid = null;
        private ulong? __pbn__classid;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong instanceid
        {
            get => __pbn__instanceid.GetValueOrDefault();
            set => __pbn__instanceid = value;
        }
        public bool ShouldSerializeinstanceid() => __pbn__instanceid != null;
        public void Resetinstanceid() => __pbn__instanceid = null;
        private ulong? __pbn__instanceid;

        [global::ProtoBuf.ProtoMember(4)]
        public bool currency
        {
            get => __pbn__currency.GetValueOrDefault();
            set => __pbn__currency = value;
        }
        public bool ShouldSerializecurrency() => __pbn__currency != null;
        public void Resetcurrency() => __pbn__currency = null;
        private bool? __pbn__currency;

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue("")]
        public string background_color
        {
            get => __pbn__background_color ?? "";
            set => __pbn__background_color = value;
        }
        public bool ShouldSerializebackground_color() => __pbn__background_color != null;
        public void Resetbackground_color() => __pbn__background_color = null;
        private string __pbn__background_color;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string icon_url
        {
            get => __pbn__icon_url ?? "";
            set => __pbn__icon_url = value;
        }
        public bool ShouldSerializeicon_url() => __pbn__icon_url != null;
        public void Reseticon_url() => __pbn__icon_url = null;
        private string __pbn__icon_url;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string icon_url_large
        {
            get => __pbn__icon_url_large ?? "";
            set => __pbn__icon_url_large = value;
        }
        public bool ShouldSerializeicon_url_large() => __pbn__icon_url_large != null;
        public void Reseticon_url_large() => __pbn__icon_url_large = null;
        private string __pbn__icon_url_large;

        [global::ProtoBuf.ProtoMember(8)]
        public global::System.Collections.Generic.List<CEconItem_DescriptionLine> descriptions { get; } = new global::System.Collections.Generic.List<CEconItem_DescriptionLine>();

        [global::ProtoBuf.ProtoMember(9)]
        public bool tradable
        {
            get => __pbn__tradable.GetValueOrDefault();
            set => __pbn__tradable = value;
        }
        public bool ShouldSerializetradable() => __pbn__tradable != null;
        public void Resettradable() => __pbn__tradable = null;
        private bool? __pbn__tradable;

        [global::ProtoBuf.ProtoMember(10)]
        public global::System.Collections.Generic.List<CEconItem_Action> actions { get; } = new global::System.Collections.Generic.List<CEconItem_Action>();

        [global::ProtoBuf.ProtoMember(11)]
        public global::System.Collections.Generic.List<CEconItem_DescriptionLine> owner_descriptions { get; } = new global::System.Collections.Generic.List<CEconItem_DescriptionLine>();

        [global::ProtoBuf.ProtoMember(12)]
        public global::System.Collections.Generic.List<CEconItem_Action> owner_actions { get; } = new global::System.Collections.Generic.List<CEconItem_Action>();

        [global::ProtoBuf.ProtoMember(13)]
        public global::System.Collections.Generic.List<string> fraudwarnings { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(14)]
        [global::System.ComponentModel.DefaultValue("")]
        public string name
        {
            get => __pbn__name ?? "";
            set => __pbn__name = value;
        }
        public bool ShouldSerializename() => __pbn__name != null;
        public void Resetname() => __pbn__name = null;
        private string __pbn__name;

        [global::ProtoBuf.ProtoMember(15)]
        [global::System.ComponentModel.DefaultValue("")]
        public string name_color
        {
            get => __pbn__name_color ?? "";
            set => __pbn__name_color = value;
        }
        public bool ShouldSerializename_color() => __pbn__name_color != null;
        public void Resetname_color() => __pbn__name_color = null;
        private string __pbn__name_color;

        [global::ProtoBuf.ProtoMember(16)]
        [global::System.ComponentModel.DefaultValue("")]
        public string type
        {
            get => __pbn__type ?? "";
            set => __pbn__type = value;
        }
        public bool ShouldSerializetype() => __pbn__type != null;
        public void Resettype() => __pbn__type = null;
        private string __pbn__type;

        [global::ProtoBuf.ProtoMember(17)]
        [global::System.ComponentModel.DefaultValue("")]
        public string market_name
        {
            get => __pbn__market_name ?? "";
            set => __pbn__market_name = value;
        }
        public bool ShouldSerializemarket_name() => __pbn__market_name != null;
        public void Resetmarket_name() => __pbn__market_name = null;
        private string __pbn__market_name;

        [global::ProtoBuf.ProtoMember(18)]
        [global::System.ComponentModel.DefaultValue("")]
        public string market_hash_name
        {
            get => __pbn__market_hash_name ?? "";
            set => __pbn__market_hash_name = value;
        }
        public bool ShouldSerializemarket_hash_name() => __pbn__market_hash_name != null;
        public void Resetmarket_hash_name() => __pbn__market_hash_name = null;
        private string __pbn__market_hash_name;

        [global::ProtoBuf.ProtoMember(19)]
        [global::System.ComponentModel.DefaultValue("")]
        public string market_fee
        {
            get => __pbn__market_fee ?? "";
            set => __pbn__market_fee = value;
        }
        public bool ShouldSerializemarket_fee() => __pbn__market_fee != null;
        public void Resetmarket_fee() => __pbn__market_fee = null;
        private string __pbn__market_fee;

        [global::ProtoBuf.ProtoMember(28)]
        public int market_fee_app
        {
            get => __pbn__market_fee_app.GetValueOrDefault();
            set => __pbn__market_fee_app = value;
        }
        public bool ShouldSerializemarket_fee_app() => __pbn__market_fee_app != null;
        public void Resetmarket_fee_app() => __pbn__market_fee_app = null;
        private int? __pbn__market_fee_app;

        [global::ProtoBuf.ProtoMember(20)]
        public CEconItem_Description contained_item { get; set; }

        [global::ProtoBuf.ProtoMember(21)]
        public global::System.Collections.Generic.List<CEconItem_Action> market_actions { get; } = new global::System.Collections.Generic.List<CEconItem_Action>();

        [global::ProtoBuf.ProtoMember(22)]
        public bool commodity
        {
            get => __pbn__commodity.GetValueOrDefault();
            set => __pbn__commodity = value;
        }
        public bool ShouldSerializecommodity() => __pbn__commodity != null;
        public void Resetcommodity() => __pbn__commodity = null;
        private bool? __pbn__commodity;

        [global::ProtoBuf.ProtoMember(23)]
        public int market_tradable_restriction
        {
            get => __pbn__market_tradable_restriction.GetValueOrDefault();
            set => __pbn__market_tradable_restriction = value;
        }
        public bool ShouldSerializemarket_tradable_restriction() => __pbn__market_tradable_restriction != null;
        public void Resetmarket_tradable_restriction() => __pbn__market_tradable_restriction = null;
        private int? __pbn__market_tradable_restriction;

        [global::ProtoBuf.ProtoMember(24)]
        public int market_marketable_restriction
        {
            get => __pbn__market_marketable_restriction.GetValueOrDefault();
            set => __pbn__market_marketable_restriction = value;
        }
        public bool ShouldSerializemarket_marketable_restriction() => __pbn__market_marketable_restriction != null;
        public void Resetmarket_marketable_restriction() => __pbn__market_marketable_restriction = null;
        private int? __pbn__market_marketable_restriction;

        [global::ProtoBuf.ProtoMember(25)]
        public bool marketable
        {
            get => __pbn__marketable.GetValueOrDefault();
            set => __pbn__marketable = value;
        }
        public bool ShouldSerializemarketable() => __pbn__marketable != null;
        public void Resetmarketable() => __pbn__marketable = null;
        private bool? __pbn__marketable;

        [global::ProtoBuf.ProtoMember(26)]
        public global::System.Collections.Generic.List<CEconItem_Tag> tags { get; } = new global::System.Collections.Generic.List<CEconItem_Tag>();

        [global::ProtoBuf.ProtoMember(27)]
        [global::System.ComponentModel.DefaultValue("")]
        public string item_expiration
        {
            get => __pbn__item_expiration ?? "";
            set => __pbn__item_expiration = value;
        }
        public bool ShouldSerializeitem_expiration() => __pbn__item_expiration != null;
        public void Resetitem_expiration() => __pbn__item_expiration = null;
        private string __pbn__item_expiration;

        [global::ProtoBuf.ProtoMember(30)]
        [global::System.ComponentModel.DefaultValue("")]
        public string market_buy_country_restriction
        {
            get => __pbn__market_buy_country_restriction ?? "";
            set => __pbn__market_buy_country_restriction = value;
        }
        public bool ShouldSerializemarket_buy_country_restriction() => __pbn__market_buy_country_restriction != null;
        public void Resetmarket_buy_country_restriction() => __pbn__market_buy_country_restriction = null;
        private string __pbn__market_buy_country_restriction;

        [global::ProtoBuf.ProtoMember(31)]
        [global::System.ComponentModel.DefaultValue("")]
        public string market_sell_country_restriction
        {
            get => __pbn__market_sell_country_restriction ?? "";
            set => __pbn__market_sell_country_restriction = value;
        }
        public bool ShouldSerializemarket_sell_country_restriction() => __pbn__market_sell_country_restriction != null;
        public void Resetmarket_sell_country_restriction() => __pbn__market_sell_country_restriction = null;
        private string __pbn__market_sell_country_restriction;

        [global::ProtoBuf.ProtoMember(32)]
        public bool @sealed
        {
            get => __pbn__sealed.GetValueOrDefault();
            set => __pbn__sealed = value;
        }
        public bool ShouldSerializesealed() => __pbn__sealed != null;
        public void Resetsealed() => __pbn__sealed = null;
        private bool? __pbn__sealed;

        [global::ProtoBuf.ProtoMember(33)]
        public CEconItem_ContainerProperties container_properties { get; set; }

        [global::ProtoBuf.ProtoMember(34)]
        [global::System.ComponentModel.DefaultValue("")]
        public string market_bucket_group_name
        {
            get => __pbn__market_bucket_group_name ?? "";
            set => __pbn__market_bucket_group_name = value;
        }
        public bool ShouldSerializemarket_bucket_group_name() => __pbn__market_bucket_group_name != null;
        public void Resetmarket_bucket_group_name() => __pbn__market_bucket_group_name = null;
        private string __pbn__market_bucket_group_name;

        [global::ProtoBuf.ProtoMember(35)]
        [global::System.ComponentModel.DefaultValue("")]
        public string market_bucket_group_id
        {
            get => __pbn__market_bucket_group_id ?? "";
            set => __pbn__market_bucket_group_id = value;
        }
        public bool ShouldSerializemarket_bucket_group_id() => __pbn__market_bucket_group_id != null;
        public void Resetmarket_bucket_group_id() => __pbn__market_bucket_group_id = null;
        private string __pbn__market_bucket_group_id;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEconItem_Tag : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string category
        {
            get => __pbn__category ?? "";
            set => __pbn__category = value;
        }
        public bool ShouldSerializecategory() => __pbn__category != null;
        public void Resetcategory() => __pbn__category = null;
        private string __pbn__category;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string internal_name
        {
            get => __pbn__internal_name ?? "";
            set => __pbn__internal_name = value;
        }
        public bool ShouldSerializeinternal_name() => __pbn__internal_name != null;
        public void Resetinternal_name() => __pbn__internal_name = null;
        private string __pbn__internal_name;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string localized_category_name
        {
            get => __pbn__localized_category_name ?? "";
            set => __pbn__localized_category_name = value;
        }
        public bool ShouldSerializelocalized_category_name() => __pbn__localized_category_name != null;
        public void Resetlocalized_category_name() => __pbn__localized_category_name = null;
        private string __pbn__localized_category_name;

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue("")]
        public string localized_tag_name
        {
            get => __pbn__localized_tag_name ?? "";
            set => __pbn__localized_tag_name = value;
        }
        public bool ShouldSerializelocalized_tag_name() => __pbn__localized_tag_name != null;
        public void Resetlocalized_tag_name() => __pbn__localized_tag_name = null;
        private string __pbn__localized_tag_name;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string color
        {
            get => __pbn__color ?? "";
            set => __pbn__color = value;
        }
        public bool ShouldSerializecolor() => __pbn__color != null;
        public void Resetcolor() => __pbn__color = null;
        private string __pbn__color;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEconItem_ClassIdentifiers : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong classid
        {
            get => __pbn__classid.GetValueOrDefault();
            set => __pbn__classid = value;
        }
        public bool ShouldSerializeclassid() => __pbn__classid != null;
        public void Resetclassid() => __pbn__classid = null;
        private ulong? __pbn__classid;

        [global::ProtoBuf.ProtoMember(2)]
        public ulong instanceid
        {
            get => __pbn__instanceid.GetValueOrDefault();
            set => __pbn__instanceid = value;
        }
        public bool ShouldSerializeinstanceid() => __pbn__instanceid != null;
        public void Resetinstanceid() => __pbn__instanceid = null;
        private ulong? __pbn__instanceid;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEconItem_ContainerProperties : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<CEconItem_ClassIdentifiers> contained_items { get; } = new global::System.Collections.Generic.List<CEconItem_ClassIdentifiers>();

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<CEconItem_Tag> search_tags { get; } = new global::System.Collections.Generic.List<CEconItem_Tag>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEconItem_AssetProperty : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint propertyid
        {
            get => __pbn__propertyid.GetValueOrDefault();
            set => __pbn__propertyid = value;
        }
        public bool ShouldSerializepropertyid() => __pbn__propertyid != null;
        public void Resetpropertyid() => __pbn__propertyid = null;
        private uint? __pbn__propertyid;

        [global::ProtoBuf.ProtoMember(2)]
        public long int_value
        {
            get => __pbn__int_value.GetValueOrDefault();
            set => __pbn__int_value = value;
        }
        public bool ShouldSerializeint_value() => __pbn__int_value != null;
        public void Resetint_value() => __pbn__int_value = null;
        private long? __pbn__int_value;

        [global::ProtoBuf.ProtoMember(3)]
        public float float_value
        {
            get => __pbn__float_value.GetValueOrDefault();
            set => __pbn__float_value = value;
        }
        public bool ShouldSerializefloat_value() => __pbn__float_value != null;
        public void Resetfloat_value() => __pbn__float_value = null;
        private float? __pbn__float_value;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string string_value
        {
            get => __pbn__string_value ?? "";
            set => __pbn__string_value = value;
        }
        public bool ShouldSerializestring_value() => __pbn__string_value != null;
        public void Resetstring_value() => __pbn__string_value = null;
        private string __pbn__string_value;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEconItem_AssetAccessory : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong classid
        {
            get => __pbn__classid.GetValueOrDefault();
            set => __pbn__classid = value;
        }
        public bool ShouldSerializeclassid() => __pbn__classid != null;
        public void Resetclassid() => __pbn__classid = null;
        private ulong? __pbn__classid;

        [global::ProtoBuf.ProtoMember(2)]
        public ulong instanceid
        {
            get => __pbn__instanceid.GetValueOrDefault();
            set => __pbn__instanceid = value;
        }
        public bool ShouldSerializeinstanceid() => __pbn__instanceid != null;
        public void Resetinstanceid() => __pbn__instanceid = null;
        private ulong? __pbn__instanceid;

        [global::ProtoBuf.ProtoMember(3)]
        public global::System.Collections.Generic.List<CEconItem_AssetProperty> standalone_properties { get; } = new global::System.Collections.Generic.List<CEconItem_AssetProperty>();

        [global::ProtoBuf.ProtoMember(4)]
        public global::System.Collections.Generic.List<CEconItem_AssetProperty> parent_relationship_properties { get; } = new global::System.Collections.Generic.List<CEconItem_AssetProperty>();

        [global::ProtoBuf.ProtoMember(5)]
        public global::System.Collections.Generic.List<CEconItem_AssetAccessory> nested_accessories { get; } = new global::System.Collections.Generic.List<CEconItem_AssetAccessory>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEconItem_AssetProperties : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

        [global::ProtoBuf.ProtoMember(2)]
        public ulong contextid
        {
            get => __pbn__contextid.GetValueOrDefault();
            set => __pbn__contextid = value;
        }
        public bool ShouldSerializecontextid() => __pbn__contextid != null;
        public void Resetcontextid() => __pbn__contextid = null;
        private ulong? __pbn__contextid;

        [global::ProtoBuf.ProtoMember(3)]
        public ulong assetid
        {
            get => __pbn__assetid.GetValueOrDefault();
            set => __pbn__assetid = value;
        }
        public bool ShouldSerializeassetid() => __pbn__assetid != null;
        public void Resetassetid() => __pbn__assetid = null;
        private ulong? __pbn__assetid;

        [global::ProtoBuf.ProtoMember(4)]
        public global::System.Collections.Generic.List<CEconItem_AssetProperty> asset_properties { get; } = new global::System.Collections.Generic.List<CEconItem_AssetProperty>();

        [global::ProtoBuf.ProtoMember(5)]
        public global::System.Collections.Generic.List<CEconItem_AssetAccessory> asset_accessories { get; } = new global::System.Collections.Generic.List<CEconItem_AssetAccessory>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEcon_GetInventoryItemsWithDescriptions_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<CEcon_Asset> assets { get; } = new global::System.Collections.Generic.List<CEcon_Asset>();

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<CEconItem_Description> descriptions { get; } = new global::System.Collections.Generic.List<CEconItem_Description>();

        [global::ProtoBuf.ProtoMember(3)]
        public global::System.Collections.Generic.List<CEcon_Asset> missing_assets { get; } = new global::System.Collections.Generic.List<CEcon_Asset>();

        [global::ProtoBuf.ProtoMember(7)]
        public global::System.Collections.Generic.List<CEconItem_AssetProperties> asset_properties { get; } = new global::System.Collections.Generic.List<CEconItem_AssetProperties>();

        [global::ProtoBuf.ProtoMember(4)]
        public bool more_items
        {
            get => __pbn__more_items.GetValueOrDefault();
            set => __pbn__more_items = value;
        }
        public bool ShouldSerializemore_items() => __pbn__more_items != null;
        public void Resetmore_items() => __pbn__more_items = null;
        private bool? __pbn__more_items;

        [global::ProtoBuf.ProtoMember(5)]
        public ulong last_assetid
        {
            get => __pbn__last_assetid.GetValueOrDefault();
            set => __pbn__last_assetid = value;
        }
        public bool ShouldSerializelast_assetid() => __pbn__last_assetid != null;
        public void Resetlast_assetid() => __pbn__last_assetid = null;
        private ulong? __pbn__last_assetid;

        [global::ProtoBuf.ProtoMember(6)]
        public uint total_inventory_count
        {
            get => __pbn__total_inventory_count.GetValueOrDefault();
            set => __pbn__total_inventory_count = value;
        }
        public bool ShouldSerializetotal_inventory_count() => __pbn__total_inventory_count != null;
        public void Resettotal_inventory_count() => __pbn__total_inventory_count = null;
        private uint? __pbn__total_inventory_count;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEcon_GetTradeOfferAccessToken_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool generate_new_token
        {
            get => __pbn__generate_new_token.GetValueOrDefault();
            set => __pbn__generate_new_token = value;
        }
        public bool ShouldSerializegenerate_new_token() => __pbn__generate_new_token != null;
        public void Resetgenerate_new_token() => __pbn__generate_new_token = null;
        private bool? __pbn__generate_new_token;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEcon_GetTradeOfferAccessToken_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string trade_offer_access_token
        {
            get => __pbn__trade_offer_access_token ?? "";
            set => __pbn__trade_offer_access_token = value;
        }
        public bool ShouldSerializetrade_offer_access_token() => __pbn__trade_offer_access_token != null;
        public void Resettrade_offer_access_token() => __pbn__trade_offer_access_token = null;
        private string __pbn__trade_offer_access_token;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEcon_ClientGetItemShopOverlayAuthURL_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string return_url
        {
            get => __pbn__return_url ?? "";
            set => __pbn__return_url = value;
        }
        public bool ShouldSerializereturn_url() => __pbn__return_url != null;
        public void Resetreturn_url() => __pbn__return_url = null;
        private string __pbn__return_url;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEcon_ClientGetItemShopOverlayAuthURL_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string url
        {
            get => __pbn__url ?? "";
            set => __pbn__url = value;
        }
        public bool ShouldSerializeurl() => __pbn__url != null;
        public void Reseturl() => __pbn__url = null;
        private string __pbn__url;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEcon_GetAssetClassInfo_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string language
        {
            get => __pbn__language ?? "";
            set => __pbn__language = value;
        }
        public bool ShouldSerializelanguage() => __pbn__language != null;
        public void Resetlanguage() => __pbn__language = null;
        private string __pbn__language;

        [global::ProtoBuf.ProtoMember(2)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

        [global::ProtoBuf.ProtoMember(3)]
        public global::System.Collections.Generic.List<CEconItem_ClassIdentifiers> classes { get; } = new global::System.Collections.Generic.List<CEconItem_ClassIdentifiers>();

        [global::ProtoBuf.ProtoMember(4)]
        public bool high_pri
        {
            get => __pbn__high_pri.GetValueOrDefault();
            set => __pbn__high_pri = value;
        }
        public bool ShouldSerializehigh_pri() => __pbn__high_pri != null;
        public void Resethigh_pri() => __pbn__high_pri = null;
        private bool? __pbn__high_pri;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEcon_GetAssetClassInfo_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<CEconItem_Description> descriptions { get; } = new global::System.Collections.Generic.List<CEconItem_Description>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEcon_GetAssetPropertySchema_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint appid
        {
            get => __pbn__appid.GetValueOrDefault();
            set => __pbn__appid = value;
        }
        public bool ShouldSerializeappid() => __pbn__appid != null;
        public void Resetappid() => __pbn__appid = null;
        private uint? __pbn__appid;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string language
        {
            get => __pbn__language ?? "";
            set => __pbn__language = value;
        }
        public bool ShouldSerializelanguage() => __pbn__language != null;
        public void Resetlanguage() => __pbn__language = null;
        private string __pbn__language;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEconItem_AssetPropertySchema : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint id
        {
            get => __pbn__id.GetValueOrDefault();
            set => __pbn__id = value;
        }
        public bool ShouldSerializeid() => __pbn__id != null;
        public void Resetid() => __pbn__id = null;
        private uint? __pbn__id;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string name
        {
            get => __pbn__name ?? "";
            set => __pbn__name = value;
        }
        public bool ShouldSerializename() => __pbn__name != null;
        public void Resetname() => __pbn__name = null;
        private string __pbn__name;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue(EAssetPropertyType.k_EAssetPropertyType_Unknown)]
        public EAssetPropertyType type
        {
            get => __pbn__type ?? EAssetPropertyType.k_EAssetPropertyType_Unknown;
            set => __pbn__type = value;
        }
        public bool ShouldSerializetype() => __pbn__type != null;
        public void Resettype() => __pbn__type = null;
        private EAssetPropertyType? __pbn__type;

        [global::ProtoBuf.ProtoMember(4)]
        public float float_min
        {
            get => __pbn__float_min.GetValueOrDefault();
            set => __pbn__float_min = value;
        }
        public bool ShouldSerializefloat_min() => __pbn__float_min != null;
        public void Resetfloat_min() => __pbn__float_min = null;
        private float? __pbn__float_min;

        [global::ProtoBuf.ProtoMember(5)]
        public float float_max
        {
            get => __pbn__float_max.GetValueOrDefault();
            set => __pbn__float_max = value;
        }
        public bool ShouldSerializefloat_max() => __pbn__float_max != null;
        public void Resetfloat_max() => __pbn__float_max = null;
        private float? __pbn__float_max;

        [global::ProtoBuf.ProtoMember(6)]
        public long int_min
        {
            get => __pbn__int_min.GetValueOrDefault();
            set => __pbn__int_min = value;
        }
        public bool ShouldSerializeint_min() => __pbn__int_min != null;
        public void Resetint_min() => __pbn__int_min = null;
        private long? __pbn__int_min;

        [global::ProtoBuf.ProtoMember(7)]
        public long int_max
        {
            get => __pbn__int_max.GetValueOrDefault();
            set => __pbn__int_max = value;
        }
        public bool ShouldSerializeint_max() => __pbn__int_max != null;
        public void Resetint_max() => __pbn__int_max = null;
        private long? __pbn__int_max;

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue("")]
        public string localized_label
        {
            get => __pbn__localized_label ?? "";
            set => __pbn__localized_label = value;
        }
        public bool ShouldSerializelocalized_label() => __pbn__localized_label != null;
        public void Resetlocalized_label() => __pbn__localized_label = null;
        private string __pbn__localized_label;

        [global::ProtoBuf.ProtoMember(9)]
        public bool hide_from_description
        {
            get => __pbn__hide_from_description.GetValueOrDefault();
            set => __pbn__hide_from_description = value;
        }
        public bool ShouldSerializehide_from_description() => __pbn__hide_from_description != null;
        public void Resethide_from_description() => __pbn__hide_from_description = null;
        private bool? __pbn__hide_from_description;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CEcon_GetAssetPropertySchema_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<CEconItem_AssetPropertySchema> property_schemas { get; } = new global::System.Collections.Generic.List<CEconItem_AssetPropertySchema>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_AccessToken_GenerateForApp_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string refresh_token
        {
            get => __pbn__refresh_token ?? "";
            set => __pbn__refresh_token = value;
        }
        public bool ShouldSerializerefresh_token() => __pbn__refresh_token != null;
        public void Resetrefresh_token() => __pbn__refresh_token = null;
        private string __pbn__refresh_token;

        [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue(ETokenRenewalType.k_ETokenRenewalType_None)]
        public ETokenRenewalType renewal_type
        {
            get => __pbn__renewal_type ?? ETokenRenewalType.k_ETokenRenewalType_None;
            set => __pbn__renewal_type = value;
        }
        public bool ShouldSerializerenewal_type() => __pbn__renewal_type != null;
        public void Resetrenewal_type() => __pbn__renewal_type = null;
        private ETokenRenewalType? __pbn__renewal_type;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_AccessToken_GenerateForApp_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string access_token
        {
            get => __pbn__access_token ?? "";
            set => __pbn__access_token = value;
        }
        public bool ShouldSerializeaccess_token() => __pbn__access_token != null;
        public void Resetaccess_token() => __pbn__access_token = null;
        private string __pbn__access_token;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string refresh_token
        {
            get => __pbn__refresh_token ?? "";
            set => __pbn__refresh_token = value;
        }
        public bool ShouldSerializerefresh_token() => __pbn__refresh_token != null;
        public void Resetrefresh_token() => __pbn__refresh_token = null;
        private string __pbn__refresh_token;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_MigrateMobileSession_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string token
        {
            get => __pbn__token ?? "";
            set => __pbn__token = value;
        }
        public bool ShouldSerializetoken() => __pbn__token != null;
        public void Resettoken() => __pbn__token = null;
        private string __pbn__token;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string signature
        {
            get => __pbn__signature ?? "";
            set => __pbn__signature = value;
        }
        public bool ShouldSerializesignature() => __pbn__signature != null;
        public void Resetsignature() => __pbn__signature = null;
        private string __pbn__signature;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_MigrateMobileSession_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string refresh_token
        {
            get => __pbn__refresh_token ?? "";
            set => __pbn__refresh_token = value;
        }
        public bool ShouldSerializerefresh_token() => __pbn__refresh_token != null;
        public void Resetrefresh_token() => __pbn__refresh_token = null;
        private string __pbn__refresh_token;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string access_token
        {
            get => __pbn__access_token ?? "";
            set => __pbn__access_token = value;
        }
        public bool ShouldSerializeaccess_token() => __pbn__access_token != null;
        public void Resetaccess_token() => __pbn__access_token = null;
        private string __pbn__access_token;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_RefreshToken_Enumerate_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue(false)]
        public bool include_revoked
        {
            get => __pbn__include_revoked ?? false;
            set => __pbn__include_revoked = value;
        }
        public bool ShouldSerializeinclude_revoked() => __pbn__include_revoked != null;
        public void Resetinclude_revoked() => __pbn__include_revoked = null;
        private bool? __pbn__include_revoked;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_RefreshToken_Enumerate_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<RefreshTokenDescription> refresh_tokens { get; } = new global::System.Collections.Generic.List<RefreshTokenDescription>();

        [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong requesting_token
        {
            get => __pbn__requesting_token.GetValueOrDefault();
            set => __pbn__requesting_token = value;
        }
        public bool ShouldSerializerequesting_token() => __pbn__requesting_token != null;
        public void Resetrequesting_token() => __pbn__requesting_token = null;
        private ulong? __pbn__requesting_token;

        [global::ProtoBuf.ProtoContract()]
        public partial class TokenUsageEvent : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public uint time
            {
                get => __pbn__time.GetValueOrDefault();
                set => __pbn__time = value;
            }
            public bool ShouldSerializetime() => __pbn__time != null;
            public void Resettime() => __pbn__time = null;
            private uint? __pbn__time;

            [global::ProtoBuf.ProtoMember(2)]
            public CMsgIPAddress ip { get; set; }

            [global::ProtoBuf.ProtoMember(3)]
            [global::System.ComponentModel.DefaultValue("")]
            public string locale
            {
                get => __pbn__locale ?? "";
                set => __pbn__locale = value;
            }
            public bool ShouldSerializelocale() => __pbn__locale != null;
            public void Resetlocale() => __pbn__locale = null;
            private string __pbn__locale;

            [global::ProtoBuf.ProtoMember(4)]
            [global::System.ComponentModel.DefaultValue("")]
            public string country
            {
                get => __pbn__country ?? "";
                set => __pbn__country = value;
            }
            public bool ShouldSerializecountry() => __pbn__country != null;
            public void Resetcountry() => __pbn__country = null;
            private string __pbn__country;

            [global::ProtoBuf.ProtoMember(5)]
            [global::System.ComponentModel.DefaultValue("")]
            public string state
            {
                get => __pbn__state ?? "";
                set => __pbn__state = value;
            }
            public bool ShouldSerializestate() => __pbn__state != null;
            public void Resetstate() => __pbn__state = null;
            private string __pbn__state;

            [global::ProtoBuf.ProtoMember(6)]
            [global::System.ComponentModel.DefaultValue("")]
            public string city
            {
                get => __pbn__city ?? "";
                set => __pbn__city = value;
            }
            public bool ShouldSerializecity() => __pbn__city != null;
            public void Resetcity() => __pbn__city = null;
            private string __pbn__city;

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class RefreshTokenDescription : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
            public ulong token_id
            {
                get => __pbn__token_id.GetValueOrDefault();
                set => __pbn__token_id = value;
            }
            public bool ShouldSerializetoken_id() => __pbn__token_id != null;
            public void Resettoken_id() => __pbn__token_id = null;
            private ulong? __pbn__token_id;

            [global::ProtoBuf.ProtoMember(2)]
            [global::System.ComponentModel.DefaultValue("")]
            public string token_description
            {
                get => __pbn__token_description ?? "";
                set => __pbn__token_description = value;
            }
            public bool ShouldSerializetoken_description() => __pbn__token_description != null;
            public void Resettoken_description() => __pbn__token_description = null;
            private string __pbn__token_description;

            [global::ProtoBuf.ProtoMember(3)]
            public uint time_updated
            {
                get => __pbn__time_updated.GetValueOrDefault();
                set => __pbn__time_updated = value;
            }
            public bool ShouldSerializetime_updated() => __pbn__time_updated != null;
            public void Resettime_updated() => __pbn__time_updated = null;
            private uint? __pbn__time_updated;

            [global::ProtoBuf.ProtoMember(4)]
            [global::System.ComponentModel.DefaultValue(EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown)]
            public EAuthTokenPlatformType platform_type
            {
                get => __pbn__platform_type ?? EAuthTokenPlatformType.k_EAuthTokenPlatformType_Unknown;
                set => __pbn__platform_type = value;
            }
            public bool ShouldSerializeplatform_type() => __pbn__platform_type != null;
            public void Resetplatform_type() => __pbn__platform_type = null;
            private EAuthTokenPlatformType? __pbn__platform_type;

            [global::ProtoBuf.ProtoMember(5)]
            public bool logged_in
            {
                get => __pbn__logged_in.GetValueOrDefault();
                set => __pbn__logged_in = value;
            }
            public bool ShouldSerializelogged_in() => __pbn__logged_in != null;
            public void Resetlogged_in() => __pbn__logged_in = null;
            private bool? __pbn__logged_in;

            [global::ProtoBuf.ProtoMember(6)]
            public uint os_platform
            {
                get => __pbn__os_platform.GetValueOrDefault();
                set => __pbn__os_platform = value;
            }
            public bool ShouldSerializeos_platform() => __pbn__os_platform != null;
            public void Resetos_platform() => __pbn__os_platform = null;
            private uint? __pbn__os_platform;

            /// <summary>
            /// <see cref="EAuthSessionGuardType"/>
            /// </summary>
            [global::ProtoBuf.ProtoMember(7)]
            public uint auth_type
            {
                get => __pbn__auth_type.GetValueOrDefault();
                set => __pbn__auth_type = value;
            }
            public bool ShouldSerializeauth_type() => __pbn__auth_type != null;
            public void Resetauth_type() => __pbn__auth_type = null;
            private uint? __pbn__auth_type;

            [global::ProtoBuf.ProtoMember(8)]
            public uint gaming_device_type
            {
                get => __pbn__gaming_device_type.GetValueOrDefault();
                set => __pbn__gaming_device_type = value;
            }
            public bool ShouldSerializegaming_device_type() => __pbn__gaming_device_type != null;
            public void Resetgaming_device_type() => __pbn__gaming_device_type = null;
            private uint? __pbn__gaming_device_type;

            [global::ProtoBuf.ProtoMember(9)]
            public TokenUsageEvent first_seen { get; set; }

            [global::ProtoBuf.ProtoMember(10)]
            public TokenUsageEvent last_seen { get; set; }

            [global::ProtoBuf.ProtoMember(11)]
            public int os_type
            {
                get => __pbn__os_type.GetValueOrDefault();
                set => __pbn__os_type = value;
            }
            public bool ShouldSerializeos_type() => __pbn__os_type != null;
            public void Resetos_type() => __pbn__os_type = null;
            private int? __pbn__os_type;

        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_Token_Revoke_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string token
        {
            get => __pbn__token ?? "";
            set => __pbn__token = value;
        }
        public bool ShouldSerializetoken() => __pbn__token != null;
        public void Resettoken() => __pbn__token = null;
        private string __pbn__token;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue(EAuthTokenRevokeAction.k_EAuthTokenRevokePermanent)]
        public EAuthTokenRevokeAction revoke_action
        {
            get => __pbn__revoke_action ?? EAuthTokenRevokeAction.k_EAuthTokenRevokePermanent;
            set => __pbn__revoke_action = value;
        }
        public bool ShouldSerializerevoke_action() => __pbn__revoke_action != null;
        public void Resetrevoke_action() => __pbn__revoke_action = null;
        private EAuthTokenRevokeAction? __pbn__revoke_action;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_Token_Revoke_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_RefreshToken_Revoke_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong token_id
        {
            get => __pbn__token_id.GetValueOrDefault();
            set => __pbn__token_id = value;
        }
        public bool ShouldSerializetoken_id() => __pbn__token_id != null;
        public void Resettoken_id() => __pbn__token_id = null;
        private ulong? __pbn__token_id;

        [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue(EAuthTokenRevokeAction.k_EAuthTokenRevokePermanent)]
        public EAuthTokenRevokeAction revoke_action
        {
            get => __pbn__revoke_action ?? EAuthTokenRevokeAction.k_EAuthTokenRevokePermanent;
            set => __pbn__revoke_action = value;
        }
        public bool ShouldSerializerevoke_action() => __pbn__revoke_action != null;
        public void Resetrevoke_action() => __pbn__revoke_action = null;
        private EAuthTokenRevokeAction? __pbn__revoke_action;

        [global::ProtoBuf.ProtoMember(4)]
        public byte[] signature
        {
            get => __pbn__signature;
            set => __pbn__signature = value;
        }
        public bool ShouldSerializesignature() => __pbn__signature != null;
        public void Resetsignature() => __pbn__signature = null;
        private byte[] __pbn__signature;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CAuthentication_RefreshToken_Revoke_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetPrivacySettings_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPrivacySettings : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        /// <summary>
        /// <see cref="EPrivacySettingsState"/>
        /// </summary>
        [global::ProtoBuf.ProtoMember(1)]
        public int privacy_state
        {
            get => __pbn__privacy_state.GetValueOrDefault();
            set => __pbn__privacy_state = value;
        }
        public bool ShouldSerializeprivacy_state() => __pbn__privacy_state != null;
        public void Resetprivacy_state() => __pbn__privacy_state = null;
        private int? __pbn__privacy_state;

        [global::ProtoBuf.ProtoMember(2)]
        public int privacy_state_inventory
        {
            get => __pbn__privacy_state_inventory.GetValueOrDefault();
            set => __pbn__privacy_state_inventory = value;
        }
        public bool ShouldSerializeprivacy_state_inventory() => __pbn__privacy_state_inventory != null;
        public void Resetprivacy_state_inventory() => __pbn__privacy_state_inventory = null;
        private int? __pbn__privacy_state_inventory;

        [global::ProtoBuf.ProtoMember(3)]
        public int privacy_state_gifts
        {
            get => __pbn__privacy_state_gifts.GetValueOrDefault();
            set => __pbn__privacy_state_gifts = value;
        }
        public bool ShouldSerializeprivacy_state_gifts() => __pbn__privacy_state_gifts != null;
        public void Resetprivacy_state_gifts() => __pbn__privacy_state_gifts = null;
        private int? __pbn__privacy_state_gifts;

        [global::ProtoBuf.ProtoMember(4)]
        public int privacy_state_ownedgames
        {
            get => __pbn__privacy_state_ownedgames.GetValueOrDefault();
            set => __pbn__privacy_state_ownedgames = value;
        }
        public bool ShouldSerializeprivacy_state_ownedgames() => __pbn__privacy_state_ownedgames != null;
        public void Resetprivacy_state_ownedgames() => __pbn__privacy_state_ownedgames = null;
        private int? __pbn__privacy_state_ownedgames;

        [global::ProtoBuf.ProtoMember(5)]
        public int privacy_state_playtime
        {
            get => __pbn__privacy_state_playtime.GetValueOrDefault();
            set => __pbn__privacy_state_playtime = value;
        }
        public bool ShouldSerializeprivacy_state_playtime() => __pbn__privacy_state_playtime != null;
        public void Resetprivacy_state_playtime() => __pbn__privacy_state_playtime = null;
        private int? __pbn__privacy_state_playtime;

        [global::ProtoBuf.ProtoMember(6)]
        public int privacy_state_friendslist
        {
            get => __pbn__privacy_state_friendslist.GetValueOrDefault();
            set => __pbn__privacy_state_friendslist = value;
        }
        public bool ShouldSerializeprivacy_state_friendslist() => __pbn__privacy_state_friendslist != null;
        public void Resetprivacy_state_friendslist() => __pbn__privacy_state_friendslist = null;
        private int? __pbn__privacy_state_friendslist;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetPrivacySettings_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public CPrivacySettings privacy_settings { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetOwnedGames_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(2)]
        public bool include_appinfo
        {
            get => __pbn__include_appinfo.GetValueOrDefault();
            set => __pbn__include_appinfo = value;
        }
        public bool ShouldSerializeinclude_appinfo() => __pbn__include_appinfo != null;
        public void Resetinclude_appinfo() => __pbn__include_appinfo = null;
        private bool? __pbn__include_appinfo;

        [global::ProtoBuf.ProtoMember(3)]
        public bool include_played_free_games
        {
            get => __pbn__include_played_free_games.GetValueOrDefault();
            set => __pbn__include_played_free_games = value;
        }
        public bool ShouldSerializeinclude_played_free_games() => __pbn__include_played_free_games != null;
        public void Resetinclude_played_free_games() => __pbn__include_played_free_games = null;
        private bool? __pbn__include_played_free_games;

        [global::ProtoBuf.ProtoMember(4)]
        public global::System.Collections.Generic.List<uint> appids_filter { get; } = new global::System.Collections.Generic.List<uint>();

        [global::ProtoBuf.ProtoMember(5)]
        public bool include_free_sub
        {
            get => __pbn__include_free_sub.GetValueOrDefault();
            set => __pbn__include_free_sub = value;
        }
        public bool ShouldSerializeinclude_free_sub() => __pbn__include_free_sub != null;
        public void Resetinclude_free_sub() => __pbn__include_free_sub = null;
        private bool? __pbn__include_free_sub;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue(true)]
        public bool skip_unvetted_apps
        {
            get => __pbn__skip_unvetted_apps ?? true;
            set => __pbn__skip_unvetted_apps = value;
        }
        public bool ShouldSerializeskip_unvetted_apps() => __pbn__skip_unvetted_apps != null;
        public void Resetskip_unvetted_apps() => __pbn__skip_unvetted_apps = null;
        private bool? __pbn__skip_unvetted_apps;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string language
        {
            get => __pbn__language ?? "";
            set => __pbn__language = value;
        }
        public bool ShouldSerializelanguage() => __pbn__language != null;
        public void Resetlanguage() => __pbn__language = null;
        private string __pbn__language;

        [global::ProtoBuf.ProtoMember(8)]
        public bool include_extended_appinfo
        {
            get => __pbn__include_extended_appinfo.GetValueOrDefault();
            set => __pbn__include_extended_appinfo = value;
        }
        public bool ShouldSerializeinclude_extended_appinfo() => __pbn__include_extended_appinfo != null;
        public void Resetinclude_extended_appinfo() => __pbn__include_extended_appinfo = null;
        private bool? __pbn__include_extended_appinfo;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetOwnedGames_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint game_count
        {
            get => __pbn__game_count.GetValueOrDefault();
            set => __pbn__game_count = value;
        }
        public bool ShouldSerializegame_count() => __pbn__game_count != null;
        public void Resetgame_count() => __pbn__game_count = null;
        private uint? __pbn__game_count;

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<Game> games { get; } = new global::System.Collections.Generic.List<Game>();

        [global::ProtoBuf.ProtoContract()]
        public partial class Game : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public int appid
            {
                get => __pbn__appid.GetValueOrDefault();
                set => __pbn__appid = value;
            }
            public bool ShouldSerializeappid() => __pbn__appid != null;
            public void Resetappid() => __pbn__appid = null;
            private int? __pbn__appid;

            [global::ProtoBuf.ProtoMember(2)]
            [global::System.ComponentModel.DefaultValue("")]
            public string name
            {
                get => __pbn__name ?? "";
                set => __pbn__name = value;
            }
            public bool ShouldSerializename() => __pbn__name != null;
            public void Resetname() => __pbn__name = null;
            private string __pbn__name;

            [global::ProtoBuf.ProtoMember(3)]
            public int playtime_2weeks
            {
                get => __pbn__playtime_2weeks.GetValueOrDefault();
                set => __pbn__playtime_2weeks = value;
            }
            public bool ShouldSerializeplaytime_2weeks() => __pbn__playtime_2weeks != null;
            public void Resetplaytime_2weeks() => __pbn__playtime_2weeks = null;
            private int? __pbn__playtime_2weeks;

            [global::ProtoBuf.ProtoMember(4)]
            public int playtime_forever
            {
                get => __pbn__playtime_forever.GetValueOrDefault();
                set => __pbn__playtime_forever = value;
            }
            public bool ShouldSerializeplaytime_forever() => __pbn__playtime_forever != null;
            public void Resetplaytime_forever() => __pbn__playtime_forever = null;
            private int? __pbn__playtime_forever;

            [global::ProtoBuf.ProtoMember(5)]
            [global::System.ComponentModel.DefaultValue("")]
            public string img_icon_url
            {
                get => __pbn__img_icon_url ?? "";
                set => __pbn__img_icon_url = value;
            }
            public bool ShouldSerializeimg_icon_url() => __pbn__img_icon_url != null;
            public void Resetimg_icon_url() => __pbn__img_icon_url = null;
            private string __pbn__img_icon_url;

            [global::ProtoBuf.ProtoMember(7)]
            public bool has_community_visible_stats
            {
                get => __pbn__has_community_visible_stats.GetValueOrDefault();
                set => __pbn__has_community_visible_stats = value;
            }
            public bool ShouldSerializehas_community_visible_stats() => __pbn__has_community_visible_stats != null;
            public void Resethas_community_visible_stats() => __pbn__has_community_visible_stats = null;
            private bool? __pbn__has_community_visible_stats;

            [global::ProtoBuf.ProtoMember(8)]
            public int playtime_windows_forever
            {
                get => __pbn__playtime_windows_forever.GetValueOrDefault();
                set => __pbn__playtime_windows_forever = value;
            }
            public bool ShouldSerializeplaytime_windows_forever() => __pbn__playtime_windows_forever != null;
            public void Resetplaytime_windows_forever() => __pbn__playtime_windows_forever = null;
            private int? __pbn__playtime_windows_forever;

            [global::ProtoBuf.ProtoMember(9)]
            public int playtime_mac_forever
            {
                get => __pbn__playtime_mac_forever.GetValueOrDefault();
                set => __pbn__playtime_mac_forever = value;
            }
            public bool ShouldSerializeplaytime_mac_forever() => __pbn__playtime_mac_forever != null;
            public void Resetplaytime_mac_forever() => __pbn__playtime_mac_forever = null;
            private int? __pbn__playtime_mac_forever;

            [global::ProtoBuf.ProtoMember(10)]
            public int playtime_linux_forever
            {
                get => __pbn__playtime_linux_forever.GetValueOrDefault();
                set => __pbn__playtime_linux_forever = value;
            }
            public bool ShouldSerializeplaytime_linux_forever() => __pbn__playtime_linux_forever != null;
            public void Resetplaytime_linux_forever() => __pbn__playtime_linux_forever = null;
            private int? __pbn__playtime_linux_forever;

            [global::ProtoBuf.ProtoMember(20)]
            public int playtime_deck_forever
            {
                get => __pbn__playtime_deck_forever.GetValueOrDefault();
                set => __pbn__playtime_deck_forever = value;
            }
            public bool ShouldSerializeplaytime_deck_forever() => __pbn__playtime_deck_forever != null;
            public void Resetplaytime_deck_forever() => __pbn__playtime_deck_forever = null;
            private int? __pbn__playtime_deck_forever;

            [global::ProtoBuf.ProtoMember(11)]
            public uint rtime_last_played
            {
                get => __pbn__rtime_last_played.GetValueOrDefault();
                set => __pbn__rtime_last_played = value;
            }
            public bool ShouldSerializertime_last_played() => __pbn__rtime_last_played != null;
            public void Resetrtime_last_played() => __pbn__rtime_last_played = null;
            private uint? __pbn__rtime_last_played;

            [global::ProtoBuf.ProtoMember(12)]
            [global::System.ComponentModel.DefaultValue("")]
            public string capsule_filename
            {
                get => __pbn__capsule_filename ?? "";
                set => __pbn__capsule_filename = value;
            }
            public bool ShouldSerializecapsule_filename() => __pbn__capsule_filename != null;
            public void Resetcapsule_filename() => __pbn__capsule_filename = null;
            private string __pbn__capsule_filename;

            [global::ProtoBuf.ProtoMember(13)]
            [global::System.ComponentModel.DefaultValue("")]
            public string sort_as
            {
                get => __pbn__sort_as ?? "";
                set => __pbn__sort_as = value;
            }
            public bool ShouldSerializesort_as() => __pbn__sort_as != null;
            public void Resetsort_as() => __pbn__sort_as = null;
            private string __pbn__sort_as;

            [global::ProtoBuf.ProtoMember(14)]
            public bool has_workshop
            {
                get => __pbn__has_workshop.GetValueOrDefault();
                set => __pbn__has_workshop = value;
            }
            public bool ShouldSerializehas_workshop() => __pbn__has_workshop != null;
            public void Resethas_workshop() => __pbn__has_workshop = null;
            private bool? __pbn__has_workshop;

            [global::ProtoBuf.ProtoMember(15)]
            public bool has_market
            {
                get => __pbn__has_market.GetValueOrDefault();
                set => __pbn__has_market = value;
            }
            public bool ShouldSerializehas_market() => __pbn__has_market != null;
            public void Resethas_market() => __pbn__has_market = null;
            private bool? __pbn__has_market;

            [global::ProtoBuf.ProtoMember(16)]
            public bool has_dlc
            {
                get => __pbn__has_dlc.GetValueOrDefault();
                set => __pbn__has_dlc = value;
            }
            public bool ShouldSerializehas_dlc() => __pbn__has_dlc != null;
            public void Resethas_dlc() => __pbn__has_dlc = null;
            private bool? __pbn__has_dlc;

            [global::ProtoBuf.ProtoMember(17)]
            public bool has_leaderboards
            {
                get => __pbn__has_leaderboards.GetValueOrDefault();
                set => __pbn__has_leaderboards = value;
            }
            public bool ShouldSerializehas_leaderboards() => __pbn__has_leaderboards != null;
            public void Resethas_leaderboards() => __pbn__has_leaderboards = null;
            private bool? __pbn__has_leaderboards;

            [global::ProtoBuf.ProtoMember(18)]
            public global::System.Collections.Generic.List<uint> content_descriptorids { get; } = new global::System.Collections.Generic.List<uint>();

            [global::ProtoBuf.ProtoMember(19)]
            public int playtime_disconnected
            {
                get => __pbn__playtime_disconnected.GetValueOrDefault();
                set => __pbn__playtime_disconnected = value;
            }
            public bool ShouldSerializeplaytime_disconnected() => __pbn__playtime_disconnected != null;
            public void Resetplaytime_disconnected() => __pbn__playtime_disconnected = null;
            private int? __pbn__playtime_disconnected;

        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetPlayerLinkDetails_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<ulong> steamids { get; } = new global::System.Collections.Generic.List<ulong>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetPlayerLinkDetails_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<PlayerLinkDetails> accounts { get; } = new global::System.Collections.Generic.List<PlayerLinkDetails>();

        [global::ProtoBuf.ProtoContract()]
        public partial class PlayerLinkDetails : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public AccountPublicData public_data { get; set; }

            [global::ProtoBuf.ProtoMember(2)]
            public AccountPrivateData private_data { get; set; }

            [global::ProtoBuf.ProtoContract()]
            public partial class AccountPublicData : global::ProtoBuf.IExtensible
            {
                private global::ProtoBuf.IExtension __pbn__extensionData;
                global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                    => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

                [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize, IsRequired = true)]
                public ulong steamid { get; set; }

                [global::ProtoBuf.ProtoMember(2)]
                public int visibility_state
                {
                    get => __pbn__visibility_state.GetValueOrDefault();
                    set => __pbn__visibility_state = value;
                }
                public bool ShouldSerializevisibility_state() => __pbn__visibility_state != null;
                public void Resetvisibility_state() => __pbn__visibility_state = null;
                private int? __pbn__visibility_state;

                [global::ProtoBuf.ProtoMember(3)]
                public int privacy_state
                {
                    get => __pbn__privacy_state.GetValueOrDefault();
                    set => __pbn__privacy_state = value;
                }
                public bool ShouldSerializeprivacy_state() => __pbn__privacy_state != null;
                public void Resetprivacy_state() => __pbn__privacy_state = null;
                private int? __pbn__privacy_state;

                [global::ProtoBuf.ProtoMember(4)]
                public int profile_state
                {
                    get => __pbn__profile_state.GetValueOrDefault();
                    set => __pbn__profile_state = value;
                }
                public bool ShouldSerializeprofile_state() => __pbn__profile_state != null;
                public void Resetprofile_state() => __pbn__profile_state = null;
                private int? __pbn__profile_state;

                [global::ProtoBuf.ProtoMember(7)]
                public uint ban_expires_time
                {
                    get => __pbn__ban_expires_time.GetValueOrDefault();
                    set => __pbn__ban_expires_time = value;
                }
                public bool ShouldSerializeban_expires_time() => __pbn__ban_expires_time != null;
                public void Resetban_expires_time() => __pbn__ban_expires_time = null;
                private uint? __pbn__ban_expires_time;

                [global::ProtoBuf.ProtoMember(8)]
                public uint account_flags
                {
                    get => __pbn__account_flags.GetValueOrDefault();
                    set => __pbn__account_flags = value;
                }
                public bool ShouldSerializeaccount_flags() => __pbn__account_flags != null;
                public void Resetaccount_flags() => __pbn__account_flags = null;
                private uint? __pbn__account_flags;

                [global::ProtoBuf.ProtoMember(9)]
                public byte[] sha_digest_avatar
                {
                    get => __pbn__sha_digest_avatar;
                    set => __pbn__sha_digest_avatar = value;
                }
                public bool ShouldSerializesha_digest_avatar() => __pbn__sha_digest_avatar != null;
                public void Resetsha_digest_avatar() => __pbn__sha_digest_avatar = null;
                private byte[] __pbn__sha_digest_avatar;

                [global::ProtoBuf.ProtoMember(10)]
                [global::System.ComponentModel.DefaultValue("")]
                public string persona_name
                {
                    get => __pbn__persona_name ?? "";
                    set => __pbn__persona_name = value;
                }
                public bool ShouldSerializepersona_name() => __pbn__persona_name != null;
                public void Resetpersona_name() => __pbn__persona_name = null;
                private string __pbn__persona_name;

                [global::ProtoBuf.ProtoMember(11)]
                [global::System.ComponentModel.DefaultValue("")]
                public string profile_url
                {
                    get => __pbn__profile_url ?? "";
                    set => __pbn__profile_url = value;
                }
                public bool ShouldSerializeprofile_url() => __pbn__profile_url != null;
                public void Resetprofile_url() => __pbn__profile_url = null;
                private string __pbn__profile_url;

                [global::ProtoBuf.ProtoMember(12)]
                public bool content_country_restricted
                {
                    get => __pbn__content_country_restricted.GetValueOrDefault();
                    set => __pbn__content_country_restricted = value;
                }
                public bool ShouldSerializecontent_country_restricted() => __pbn__content_country_restricted != null;
                public void Resetcontent_country_restricted() => __pbn__content_country_restricted = null;
                private bool? __pbn__content_country_restricted;

            }

            [global::ProtoBuf.ProtoContract()]
            public partial class AccountPrivateData : global::ProtoBuf.IExtensible
            {
                private global::ProtoBuf.IExtension __pbn__extensionData;
                global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                    => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

                [global::ProtoBuf.ProtoMember(1)]
                public int persona_state
                {
                    get => __pbn__persona_state.GetValueOrDefault();
                    set => __pbn__persona_state = value;
                }
                public bool ShouldSerializepersona_state() => __pbn__persona_state != null;
                public void Resetpersona_state() => __pbn__persona_state = null;
                private int? __pbn__persona_state;

                [global::ProtoBuf.ProtoMember(2)]
                public uint persona_state_flags
                {
                    get => __pbn__persona_state_flags.GetValueOrDefault();
                    set => __pbn__persona_state_flags = value;
                }
                public bool ShouldSerializepersona_state_flags() => __pbn__persona_state_flags != null;
                public void Resetpersona_state_flags() => __pbn__persona_state_flags = null;
                private uint? __pbn__persona_state_flags;

                [global::ProtoBuf.ProtoMember(3)]
                public uint time_created
                {
                    get => __pbn__time_created.GetValueOrDefault();
                    set => __pbn__time_created = value;
                }
                public bool ShouldSerializetime_created() => __pbn__time_created != null;
                public void Resettime_created() => __pbn__time_created = null;
                private uint? __pbn__time_created;

                [global::ProtoBuf.ProtoMember(4, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
                public ulong game_id
                {
                    get => __pbn__game_id.GetValueOrDefault();
                    set => __pbn__game_id = value;
                }
                public bool ShouldSerializegame_id() => __pbn__game_id != null;
                public void Resetgame_id() => __pbn__game_id = null;
                private ulong? __pbn__game_id;

                [global::ProtoBuf.ProtoMember(5, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
                public ulong game_server_steam_id
                {
                    get => __pbn__game_server_steam_id.GetValueOrDefault();
                    set => __pbn__game_server_steam_id = value;
                }
                public bool ShouldSerializegame_server_steam_id() => __pbn__game_server_steam_id != null;
                public void Resetgame_server_steam_id() => __pbn__game_server_steam_id = null;
                private ulong? __pbn__game_server_steam_id;

                [global::ProtoBuf.ProtoMember(6)]
                public uint game_server_ip_address
                {
                    get => __pbn__game_server_ip_address.GetValueOrDefault();
                    set => __pbn__game_server_ip_address = value;
                }
                public bool ShouldSerializegame_server_ip_address() => __pbn__game_server_ip_address != null;
                public void Resetgame_server_ip_address() => __pbn__game_server_ip_address = null;
                private uint? __pbn__game_server_ip_address;

                [global::ProtoBuf.ProtoMember(7)]
                public uint game_server_port
                {
                    get => __pbn__game_server_port.GetValueOrDefault();
                    set => __pbn__game_server_port = value;
                }
                public bool ShouldSerializegame_server_port() => __pbn__game_server_port != null;
                public void Resetgame_server_port() => __pbn__game_server_port = null;
                private uint? __pbn__game_server_port;

                [global::ProtoBuf.ProtoMember(8)]
                [global::System.ComponentModel.DefaultValue("")]
                public string game_extra_info
                {
                    get => __pbn__game_extra_info ?? "";
                    set => __pbn__game_extra_info = value;
                }
                public bool ShouldSerializegame_extra_info() => __pbn__game_extra_info != null;
                public void Resetgame_extra_info() => __pbn__game_extra_info = null;
                private string __pbn__game_extra_info;

                [global::ProtoBuf.ProtoMember(9)]
                [global::System.ComponentModel.DefaultValue("")]
                public string account_name
                {
                    get => __pbn__account_name ?? "";
                    set => __pbn__account_name = value;
                }
                public bool ShouldSerializeaccount_name() => __pbn__account_name != null;
                public void Resetaccount_name() => __pbn__account_name = null;
                private string __pbn__account_name;

                [global::ProtoBuf.ProtoMember(10, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
                public ulong lobby_steam_id
                {
                    get => __pbn__lobby_steam_id.GetValueOrDefault();
                    set => __pbn__lobby_steam_id = value;
                }
                public bool ShouldSerializelobby_steam_id() => __pbn__lobby_steam_id != null;
                public void Resetlobby_steam_id() => __pbn__lobby_steam_id = null;
                private ulong? __pbn__lobby_steam_id;

                [global::ProtoBuf.ProtoMember(11)]
                [global::System.ComponentModel.DefaultValue("")]
                public string rich_presence_kv
                {
                    get => __pbn__rich_presence_kv ?? "";
                    set => __pbn__rich_presence_kv = value;
                }
                public bool ShouldSerializerich_presence_kv() => __pbn__rich_presence_kv != null;
                public void Resetrich_presence_kv() => __pbn__rich_presence_kv = null;
                private string __pbn__rich_presence_kv;

                [global::ProtoBuf.ProtoMember(12, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
                public ulong broadcast_session_id
                {
                    get => __pbn__broadcast_session_id.GetValueOrDefault();
                    set => __pbn__broadcast_session_id = value;
                }
                public bool ShouldSerializebroadcast_session_id() => __pbn__broadcast_session_id != null;
                public void Resetbroadcast_session_id() => __pbn__broadcast_session_id = null;
                private ulong? __pbn__broadcast_session_id;

                [global::ProtoBuf.ProtoMember(13)]
                public uint watching_broadcast_accountid
                {
                    get => __pbn__watching_broadcast_accountid.GetValueOrDefault();
                    set => __pbn__watching_broadcast_accountid = value;
                }
                public bool ShouldSerializewatching_broadcast_accountid() => __pbn__watching_broadcast_accountid != null;
                public void Resetwatching_broadcast_accountid() => __pbn__watching_broadcast_accountid = null;
                private uint? __pbn__watching_broadcast_accountid;

                [global::ProtoBuf.ProtoMember(14)]
                public uint watching_broadcast_appid
                {
                    get => __pbn__watching_broadcast_appid.GetValueOrDefault();
                    set => __pbn__watching_broadcast_appid = value;
                }
                public bool ShouldSerializewatching_broadcast_appid() => __pbn__watching_broadcast_appid != null;
                public void Resetwatching_broadcast_appid() => __pbn__watching_broadcast_appid = null;
                private uint? __pbn__watching_broadcast_appid;

                [global::ProtoBuf.ProtoMember(15)]
                public uint watching_broadcast_viewers
                {
                    get => __pbn__watching_broadcast_viewers.GetValueOrDefault();
                    set => __pbn__watching_broadcast_viewers = value;
                }
                public bool ShouldSerializewatching_broadcast_viewers() => __pbn__watching_broadcast_viewers != null;
                public void Resetwatching_broadcast_viewers() => __pbn__watching_broadcast_viewers = null;
                private uint? __pbn__watching_broadcast_viewers;

                [global::ProtoBuf.ProtoMember(16)]
                [global::System.ComponentModel.DefaultValue("")]
                public string watching_broadcast_title
                {
                    get => __pbn__watching_broadcast_title ?? "";
                    set => __pbn__watching_broadcast_title = value;
                }
                public bool ShouldSerializewatching_broadcast_title() => __pbn__watching_broadcast_title != null;
                public void Resetwatching_broadcast_title() => __pbn__watching_broadcast_title = null;
                private string __pbn__watching_broadcast_title;

                [global::ProtoBuf.ProtoMember(17)]
                public uint last_logoff_time
                {
                    get => __pbn__last_logoff_time.GetValueOrDefault();
                    set => __pbn__last_logoff_time = value;
                }
                public bool ShouldSerializelast_logoff_time() => __pbn__last_logoff_time != null;
                public void Resetlast_logoff_time() => __pbn__last_logoff_time = null;
                private uint? __pbn__last_logoff_time;

                [global::ProtoBuf.ProtoMember(18)]
                public uint last_seen_online
                {
                    get => __pbn__last_seen_online.GetValueOrDefault();
                    set => __pbn__last_seen_online = value;
                }
                public bool ShouldSerializelast_seen_online() => __pbn__last_seen_online != null;
                public void Resetlast_seen_online() => __pbn__last_seen_online = null;
                private uint? __pbn__last_seen_online;

                [global::ProtoBuf.ProtoMember(19)]
                public int game_os_type
                {
                    get => __pbn__game_os_type.GetValueOrDefault();
                    set => __pbn__game_os_type = value;
                }
                public bool ShouldSerializegame_os_type() => __pbn__game_os_type != null;
                public void Resetgame_os_type() => __pbn__game_os_type = null;
                private int? __pbn__game_os_type;

                [global::ProtoBuf.ProtoMember(20)]
                public int game_device_type
                {
                    get => __pbn__game_device_type.GetValueOrDefault();
                    set => __pbn__game_device_type = value;
                }
                public bool ShouldSerializegame_device_type() => __pbn__game_device_type != null;
                public void Resetgame_device_type() => __pbn__game_device_type = null;
                private int? __pbn__game_device_type;

                [global::ProtoBuf.ProtoMember(21)]
                [global::System.ComponentModel.DefaultValue("")]
                public string game_device_name
                {
                    get => __pbn__game_device_name ?? "";
                    set => __pbn__game_device_name = value;
                }
                public bool ShouldSerializegame_device_name() => __pbn__game_device_name != null;
                public void Resetgame_device_name() => __pbn__game_device_name = null;
                private string __pbn__game_device_name;

                [global::ProtoBuf.ProtoMember(22)]
                public bool game_is_private
                {
                    get => __pbn__game_is_private.GetValueOrDefault();
                    set => __pbn__game_is_private = value;
                }
                public bool ShouldSerializegame_is_private() => __pbn__game_is_private != null;
                public void Resetgame_is_private() => __pbn__game_is_private = null;
                private bool? __pbn__game_is_private;

            }

        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetNicknameList_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetNicknameList_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<PlayerNickname> nicknames { get; } = new global::System.Collections.Generic.List<PlayerNickname>();

        [global::ProtoBuf.ProtoContract()]
        public partial class PlayerNickname : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
            public uint accountid
            {
                get => __pbn__accountid.GetValueOrDefault();
                set => __pbn__accountid = value;
            }
            public bool ShouldSerializeaccountid() => __pbn__accountid != null;
            public void Resetaccountid() => __pbn__accountid = null;
            private uint? __pbn__accountid;

            [global::ProtoBuf.ProtoMember(2)]
            [global::System.ComponentModel.DefaultValue("")]
            public string nickname
            {
                get => __pbn__nickname ?? "";
                set => __pbn__nickname = value;
            }
            public bool ShouldSerializenickname() => __pbn__nickname != null;
            public void Resetnickname() => __pbn__nickname = null;
            private string __pbn__nickname;

        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetPerFriendPreferences_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class PerFriendPreferences : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public uint accountid
        {
            get => __pbn__accountid.GetValueOrDefault();
            set => __pbn__accountid = value;
        }
        public bool ShouldSerializeaccountid() => __pbn__accountid != null;
        public void Resetaccountid() => __pbn__accountid = null;
        private uint? __pbn__accountid;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string nickname
        {
            get => __pbn__nickname ?? "";
            set => __pbn__nickname = value;
        }
        public bool ShouldSerializenickname() => __pbn__nickname != null;
        public void Resetnickname() => __pbn__nickname = null;
        private string __pbn__nickname;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue(ENotificationSetting.k_ENotificationSettingNotifyUseDefault)]
        public ENotificationSetting notifications_showingame
        {
            get => __pbn__notifications_showingame ?? ENotificationSetting.k_ENotificationSettingNotifyUseDefault;
            set => __pbn__notifications_showingame = value;
        }
        public bool ShouldSerializenotifications_showingame() => __pbn__notifications_showingame != null;
        public void Resetnotifications_showingame() => __pbn__notifications_showingame = null;
        private ENotificationSetting? __pbn__notifications_showingame;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue(ENotificationSetting.k_ENotificationSettingNotifyUseDefault)]
        public ENotificationSetting notifications_showonline
        {
            get => __pbn__notifications_showonline ?? ENotificationSetting.k_ENotificationSettingNotifyUseDefault;
            set => __pbn__notifications_showonline = value;
        }
        public bool ShouldSerializenotifications_showonline() => __pbn__notifications_showonline != null;
        public void Resetnotifications_showonline() => __pbn__notifications_showonline = null;
        private ENotificationSetting? __pbn__notifications_showonline;

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue(ENotificationSetting.k_ENotificationSettingNotifyUseDefault)]
        public ENotificationSetting notifications_showmessages
        {
            get => __pbn__notifications_showmessages ?? ENotificationSetting.k_ENotificationSettingNotifyUseDefault;
            set => __pbn__notifications_showmessages = value;
        }
        public bool ShouldSerializenotifications_showmessages() => __pbn__notifications_showmessages != null;
        public void Resetnotifications_showmessages() => __pbn__notifications_showmessages = null;
        private ENotificationSetting? __pbn__notifications_showmessages;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue(ENotificationSetting.k_ENotificationSettingNotifyUseDefault)]
        public ENotificationSetting sounds_showingame
        {
            get => __pbn__sounds_showingame ?? ENotificationSetting.k_ENotificationSettingNotifyUseDefault;
            set => __pbn__sounds_showingame = value;
        }
        public bool ShouldSerializesounds_showingame() => __pbn__sounds_showingame != null;
        public void Resetsounds_showingame() => __pbn__sounds_showingame = null;
        private ENotificationSetting? __pbn__sounds_showingame;

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue(ENotificationSetting.k_ENotificationSettingNotifyUseDefault)]
        public ENotificationSetting sounds_showonline
        {
            get => __pbn__sounds_showonline ?? ENotificationSetting.k_ENotificationSettingNotifyUseDefault;
            set => __pbn__sounds_showonline = value;
        }
        public bool ShouldSerializesounds_showonline() => __pbn__sounds_showonline != null;
        public void Resetsounds_showonline() => __pbn__sounds_showonline = null;
        private ENotificationSetting? __pbn__sounds_showonline;

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue(ENotificationSetting.k_ENotificationSettingNotifyUseDefault)]
        public ENotificationSetting sounds_showmessages
        {
            get => __pbn__sounds_showmessages ?? ENotificationSetting.k_ENotificationSettingNotifyUseDefault;
            set => __pbn__sounds_showmessages = value;
        }
        public bool ShouldSerializesounds_showmessages() => __pbn__sounds_showmessages != null;
        public void Resetsounds_showmessages() => __pbn__sounds_showmessages = null;
        private ENotificationSetting? __pbn__sounds_showmessages;

        [global::ProtoBuf.ProtoMember(9)]
        [global::System.ComponentModel.DefaultValue(ENotificationSetting.k_ENotificationSettingNotifyUseDefault)]
        public ENotificationSetting notifications_sendmobile
        {
            get => __pbn__notifications_sendmobile ?? ENotificationSetting.k_ENotificationSettingNotifyUseDefault;
            set => __pbn__notifications_sendmobile = value;
        }
        public bool ShouldSerializenotifications_sendmobile() => __pbn__notifications_sendmobile != null;
        public void Resetnotifications_sendmobile() => __pbn__notifications_sendmobile = null;
        private ENotificationSetting? __pbn__notifications_sendmobile;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetPerFriendPreferences_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<PerFriendPreferences> preferences { get; } = new global::System.Collections.Generic.List<PerFriendPreferences>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetLastPlayedTimes_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint min_last_played
        {
            get => __pbn__min_last_played.GetValueOrDefault();
            set => __pbn__min_last_played = value;
        }
        public bool ShouldSerializemin_last_played() => __pbn__min_last_played != null;
        public void Resetmin_last_played() => __pbn__min_last_played = null;
        private uint? __pbn__min_last_played;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetLastPlayedTimes_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<Game> games { get; } = new global::System.Collections.Generic.List<Game>();

        [global::ProtoBuf.ProtoContract()]
        public partial class Game : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public int appid
            {
                get => __pbn__appid.GetValueOrDefault();
                set => __pbn__appid = value;
            }
            public bool ShouldSerializeappid() => __pbn__appid != null;
            public void Resetappid() => __pbn__appid = null;
            private int? __pbn__appid;

            [global::ProtoBuf.ProtoMember(2)]
            public uint last_playtime
            {
                get => __pbn__last_playtime.GetValueOrDefault();
                set => __pbn__last_playtime = value;
            }
            public bool ShouldSerializelast_playtime() => __pbn__last_playtime != null;
            public void Resetlast_playtime() => __pbn__last_playtime = null;
            private uint? __pbn__last_playtime;

            [global::ProtoBuf.ProtoMember(3)]
            public int playtime_2weeks
            {
                get => __pbn__playtime_2weeks.GetValueOrDefault();
                set => __pbn__playtime_2weeks = value;
            }
            public bool ShouldSerializeplaytime_2weeks() => __pbn__playtime_2weeks != null;
            public void Resetplaytime_2weeks() => __pbn__playtime_2weeks = null;
            private int? __pbn__playtime_2weeks;

            [global::ProtoBuf.ProtoMember(4)]
            public int playtime_forever
            {
                get => __pbn__playtime_forever.GetValueOrDefault();
                set => __pbn__playtime_forever = value;
            }
            public bool ShouldSerializeplaytime_forever() => __pbn__playtime_forever != null;
            public void Resetplaytime_forever() => __pbn__playtime_forever = null;
            private int? __pbn__playtime_forever;

            [global::ProtoBuf.ProtoMember(5)]
            public uint first_playtime
            {
                get => __pbn__first_playtime.GetValueOrDefault();
                set => __pbn__first_playtime = value;
            }
            public bool ShouldSerializefirst_playtime() => __pbn__first_playtime != null;
            public void Resetfirst_playtime() => __pbn__first_playtime = null;
            private uint? __pbn__first_playtime;

            [global::ProtoBuf.ProtoMember(6)]
            public int playtime_windows_forever
            {
                get => __pbn__playtime_windows_forever.GetValueOrDefault();
                set => __pbn__playtime_windows_forever = value;
            }
            public bool ShouldSerializeplaytime_windows_forever() => __pbn__playtime_windows_forever != null;
            public void Resetplaytime_windows_forever() => __pbn__playtime_windows_forever = null;
            private int? __pbn__playtime_windows_forever;

            [global::ProtoBuf.ProtoMember(7)]
            public int playtime_mac_forever
            {
                get => __pbn__playtime_mac_forever.GetValueOrDefault();
                set => __pbn__playtime_mac_forever = value;
            }
            public bool ShouldSerializeplaytime_mac_forever() => __pbn__playtime_mac_forever != null;
            public void Resetplaytime_mac_forever() => __pbn__playtime_mac_forever = null;
            private int? __pbn__playtime_mac_forever;

            [global::ProtoBuf.ProtoMember(8)]
            public int playtime_linux_forever
            {
                get => __pbn__playtime_linux_forever.GetValueOrDefault();
                set => __pbn__playtime_linux_forever = value;
            }
            public bool ShouldSerializeplaytime_linux_forever() => __pbn__playtime_linux_forever != null;
            public void Resetplaytime_linux_forever() => __pbn__playtime_linux_forever = null;
            private int? __pbn__playtime_linux_forever;

            [global::ProtoBuf.ProtoMember(16)]
            public int playtime_deck_forever
            {
                get => __pbn__playtime_deck_forever.GetValueOrDefault();
                set => __pbn__playtime_deck_forever = value;
            }
            public bool ShouldSerializeplaytime_deck_forever() => __pbn__playtime_deck_forever != null;
            public void Resetplaytime_deck_forever() => __pbn__playtime_deck_forever = null;
            private int? __pbn__playtime_deck_forever;

            [global::ProtoBuf.ProtoMember(9)]
            public uint first_windows_playtime
            {
                get => __pbn__first_windows_playtime.GetValueOrDefault();
                set => __pbn__first_windows_playtime = value;
            }
            public bool ShouldSerializefirst_windows_playtime() => __pbn__first_windows_playtime != null;
            public void Resetfirst_windows_playtime() => __pbn__first_windows_playtime = null;
            private uint? __pbn__first_windows_playtime;

            [global::ProtoBuf.ProtoMember(10)]
            public uint first_mac_playtime
            {
                get => __pbn__first_mac_playtime.GetValueOrDefault();
                set => __pbn__first_mac_playtime = value;
            }
            public bool ShouldSerializefirst_mac_playtime() => __pbn__first_mac_playtime != null;
            public void Resetfirst_mac_playtime() => __pbn__first_mac_playtime = null;
            private uint? __pbn__first_mac_playtime;

            [global::ProtoBuf.ProtoMember(11)]
            public uint first_linux_playtime
            {
                get => __pbn__first_linux_playtime.GetValueOrDefault();
                set => __pbn__first_linux_playtime = value;
            }
            public bool ShouldSerializefirst_linux_playtime() => __pbn__first_linux_playtime != null;
            public void Resetfirst_linux_playtime() => __pbn__first_linux_playtime = null;
            private uint? __pbn__first_linux_playtime;

            [global::ProtoBuf.ProtoMember(17)]
            public uint first_deck_playtime
            {
                get => __pbn__first_deck_playtime.GetValueOrDefault();
                set => __pbn__first_deck_playtime = value;
            }
            public bool ShouldSerializefirst_deck_playtime() => __pbn__first_deck_playtime != null;
            public void Resetfirst_deck_playtime() => __pbn__first_deck_playtime = null;
            private uint? __pbn__first_deck_playtime;

            [global::ProtoBuf.ProtoMember(12)]
            public uint last_windows_playtime
            {
                get => __pbn__last_windows_playtime.GetValueOrDefault();
                set => __pbn__last_windows_playtime = value;
            }
            public bool ShouldSerializelast_windows_playtime() => __pbn__last_windows_playtime != null;
            public void Resetlast_windows_playtime() => __pbn__last_windows_playtime = null;
            private uint? __pbn__last_windows_playtime;

            [global::ProtoBuf.ProtoMember(13)]
            public uint last_mac_playtime
            {
                get => __pbn__last_mac_playtime.GetValueOrDefault();
                set => __pbn__last_mac_playtime = value;
            }
            public bool ShouldSerializelast_mac_playtime() => __pbn__last_mac_playtime != null;
            public void Resetlast_mac_playtime() => __pbn__last_mac_playtime = null;
            private uint? __pbn__last_mac_playtime;

            [global::ProtoBuf.ProtoMember(14)]
            public uint last_linux_playtime
            {
                get => __pbn__last_linux_playtime.GetValueOrDefault();
                set => __pbn__last_linux_playtime = value;
            }
            public bool ShouldSerializelast_linux_playtime() => __pbn__last_linux_playtime != null;
            public void Resetlast_linux_playtime() => __pbn__last_linux_playtime = null;
            private uint? __pbn__last_linux_playtime;

            [global::ProtoBuf.ProtoMember(18)]
            public uint last_deck_playtime
            {
                get => __pbn__last_deck_playtime.GetValueOrDefault();
                set => __pbn__last_deck_playtime = value;
            }
            public bool ShouldSerializelast_deck_playtime() => __pbn__last_deck_playtime != null;
            public void Resetlast_deck_playtime() => __pbn__last_deck_playtime = null;
            private uint? __pbn__last_deck_playtime;

            [global::ProtoBuf.ProtoMember(15)]
            public uint playtime_disconnected
            {
                get => __pbn__playtime_disconnected.GetValueOrDefault();
                set => __pbn__playtime_disconnected = value;
            }
            public bool ShouldSerializeplaytime_disconnected() => __pbn__playtime_disconnected != null;
            public void Resetplaytime_disconnected() => __pbn__playtime_disconnected = null;
            private uint? __pbn__playtime_disconnected;

        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetTimeSSAAccepted_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_GetTimeSSAAccepted_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint time_ssa_accepted
        {
            get => __pbn__time_ssa_accepted.GetValueOrDefault();
            set => __pbn__time_ssa_accepted = value;
        }
        public bool ShouldSerializetime_ssa_accepted() => __pbn__time_ssa_accepted != null;
        public void Resettime_ssa_accepted() => __pbn__time_ssa_accepted = null;
        private uint? __pbn__time_ssa_accepted;

        [global::ProtoBuf.ProtoMember(2)]
        public uint time_ssa_updated
        {
            get => __pbn__time_ssa_updated.GetValueOrDefault();
            set => __pbn__time_ssa_updated = value;
        }
        public bool ShouldSerializetime_ssa_updated() => __pbn__time_ssa_updated != null;
        public void Resettime_ssa_updated() => __pbn__time_ssa_updated = null;
        private uint? __pbn__time_ssa_updated;

        [global::ProtoBuf.ProtoMember(3)]
        public uint time_chinassa_accepted
        {
            get => __pbn__time_chinassa_accepted.GetValueOrDefault();
            set => __pbn__time_chinassa_accepted = value;
        }
        public bool ShouldSerializetime_chinassa_accepted() => __pbn__time_chinassa_accepted != null;
        public void Resettime_chinassa_accepted() => __pbn__time_chinassa_accepted = null;
        private uint? __pbn__time_chinassa_accepted;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_AcceptSSA_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue(EAgreementType.k_EAgreementType_Invalid)]
        public EAgreementType agreement_type
        {
            get => __pbn__agreement_type ?? EAgreementType.k_EAgreementType_Invalid;
            set => __pbn__agreement_type = value;
        }
        public bool ShouldSerializeagreement_type() => __pbn__agreement_type != null;
        public void Resetagreement_type() => __pbn__agreement_type = null;
        private EAgreementType? __pbn__agreement_type;

        [global::ProtoBuf.ProtoMember(2)]
        public uint time_signed_utc
        {
            get => __pbn__time_signed_utc.GetValueOrDefault();
            set => __pbn__time_signed_utc = value;
        }
        public bool ShouldSerializetime_signed_utc() => __pbn__time_signed_utc != null;
        public void Resettime_signed_utc() => __pbn__time_signed_utc = null;
        private uint? __pbn__time_signed_utc;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CPlayer_AcceptSSA_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CFriendMessages_GetRecentMessages_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid1
        {
            get => __pbn__steamid1.GetValueOrDefault();
            set => __pbn__steamid1 = value;
        }
        public bool ShouldSerializesteamid1() => __pbn__steamid1 != null;
        public void Resetsteamid1() => __pbn__steamid1 = null;
        private ulong? __pbn__steamid1;

        [global::ProtoBuf.ProtoMember(2, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid2
        {
            get => __pbn__steamid2.GetValueOrDefault();
            set => __pbn__steamid2 = value;
        }
        public bool ShouldSerializesteamid2() => __pbn__steamid2 != null;
        public void Resetsteamid2() => __pbn__steamid2 = null;
        private ulong? __pbn__steamid2;

        [global::ProtoBuf.ProtoMember(3)]
        public uint count
        {
            get => __pbn__count.GetValueOrDefault();
            set => __pbn__count = value;
        }
        public bool ShouldSerializecount() => __pbn__count != null;
        public void Resetcount() => __pbn__count = null;
        private uint? __pbn__count;

        [global::ProtoBuf.ProtoMember(4)]
        public bool most_recent_conversation
        {
            get => __pbn__most_recent_conversation.GetValueOrDefault();
            set => __pbn__most_recent_conversation = value;
        }
        public bool ShouldSerializemost_recent_conversation() => __pbn__most_recent_conversation != null;
        public void Resetmost_recent_conversation() => __pbn__most_recent_conversation = null;
        private bool? __pbn__most_recent_conversation;

        [global::ProtoBuf.ProtoMember(5, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public uint rtime32_start_time
        {
            get => __pbn__rtime32_start_time.GetValueOrDefault();
            set => __pbn__rtime32_start_time = value;
        }
        public bool ShouldSerializertime32_start_time() => __pbn__rtime32_start_time != null;
        public void Resetrtime32_start_time() => __pbn__rtime32_start_time = null;
        private uint? __pbn__rtime32_start_time;

        [global::ProtoBuf.ProtoMember(6)]
        public bool bbcode_format
        {
            get => __pbn__bbcode_format.GetValueOrDefault();
            set => __pbn__bbcode_format = value;
        }
        public bool ShouldSerializebbcode_format() => __pbn__bbcode_format != null;
        public void Resetbbcode_format() => __pbn__bbcode_format = null;
        private bool? __pbn__bbcode_format;

        [global::ProtoBuf.ProtoMember(7)]
        public uint start_ordinal
        {
            get => __pbn__start_ordinal.GetValueOrDefault();
            set => __pbn__start_ordinal = value;
        }
        public bool ShouldSerializestart_ordinal() => __pbn__start_ordinal != null;
        public void Resetstart_ordinal() => __pbn__start_ordinal = null;
        private uint? __pbn__start_ordinal;

        [global::ProtoBuf.ProtoMember(8)]
        public uint time_last
        {
            get => __pbn__time_last.GetValueOrDefault();
            set => __pbn__time_last = value;
        }
        public bool ShouldSerializetime_last() => __pbn__time_last != null;
        public void Resettime_last() => __pbn__time_last = null;
        private uint? __pbn__time_last;

        [global::ProtoBuf.ProtoMember(9)]
        public uint ordinal_last
        {
            get => __pbn__ordinal_last.GetValueOrDefault();
            set => __pbn__ordinal_last = value;
        }
        public bool ShouldSerializeordinal_last() => __pbn__ordinal_last != null;
        public void Resetordinal_last() => __pbn__ordinal_last = null;
        private uint? __pbn__ordinal_last;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CFriendMessages_GetRecentMessages_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<FriendMessage> messages { get; } = new global::System.Collections.Generic.List<FriendMessage>();

        [global::ProtoBuf.ProtoMember(4)]
        public bool more_available
        {
            get => __pbn__more_available.GetValueOrDefault();
            set => __pbn__more_available = value;
        }
        public bool ShouldSerializemore_available() => __pbn__more_available != null;
        public void Resetmore_available() => __pbn__more_available = null;
        private bool? __pbn__more_available;

        [global::ProtoBuf.ProtoContract()]
        public partial class FriendMessage : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public uint accountid
            {
                get => __pbn__accountid.GetValueOrDefault();
                set => __pbn__accountid = value;
            }
            public bool ShouldSerializeaccountid() => __pbn__accountid != null;
            public void Resetaccountid() => __pbn__accountid = null;
            private uint? __pbn__accountid;

            [global::ProtoBuf.ProtoMember(2)]
            public uint timestamp
            {
                get => __pbn__timestamp.GetValueOrDefault();
                set => __pbn__timestamp = value;
            }
            public bool ShouldSerializetimestamp() => __pbn__timestamp != null;
            public void Resettimestamp() => __pbn__timestamp = null;
            private uint? __pbn__timestamp;

            [global::ProtoBuf.ProtoMember(3)]
            [global::System.ComponentModel.DefaultValue("")]
            public string message
            {
                get => __pbn__message ?? "";
                set => __pbn__message = value;
            }
            public bool ShouldSerializemessage() => __pbn__message != null;
            public void Resetmessage() => __pbn__message = null;
            private string __pbn__message;

            [global::ProtoBuf.ProtoMember(4)]
            public uint ordinal
            {
                get => __pbn__ordinal.GetValueOrDefault();
                set => __pbn__ordinal = value;
            }
            public bool ShouldSerializeordinal() => __pbn__ordinal != null;
            public void Resetordinal() => __pbn__ordinal = null;
            private uint? __pbn__ordinal;

            [global::ProtoBuf.ProtoMember(5)]
            public global::System.Collections.Generic.List<MessageReaction> reactions { get; } = new global::System.Collections.Generic.List<MessageReaction>();

            [global::ProtoBuf.ProtoContract()]
            public partial class MessageReaction : global::ProtoBuf.IExtensible
            {
                private global::ProtoBuf.IExtension __pbn__extensionData;
                global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                    => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

                [global::ProtoBuf.ProtoMember(1)]
                [global::System.ComponentModel.DefaultValue(EMessageReactionType.k_EMessageReactionType_Invalid)]
                public EMessageReactionType reaction_type
                {
                    get => __pbn__reaction_type ?? EMessageReactionType.k_EMessageReactionType_Invalid;
                    set => __pbn__reaction_type = value;
                }
                public bool ShouldSerializereaction_type() => __pbn__reaction_type != null;
                public void Resetreaction_type() => __pbn__reaction_type = null;
                private EMessageReactionType? __pbn__reaction_type;

                [global::ProtoBuf.ProtoMember(2)]
                [global::System.ComponentModel.DefaultValue("")]
                public string reaction
                {
                    get => __pbn__reaction ?? "";
                    set => __pbn__reaction = value;
                }
                public bool ShouldSerializereaction() => __pbn__reaction != null;
                public void Resetreaction() => __pbn__reaction = null;
                private string __pbn__reaction;

                [global::ProtoBuf.ProtoMember(3)]
                public global::System.Collections.Generic.List<uint> reactors { get; } = new global::System.Collections.Generic.List<uint>();

            }

        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CFriendMessages_SendMessage_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid
        {
            get => __pbn__steamid.GetValueOrDefault();
            set => __pbn__steamid = value;
        }
        public bool ShouldSerializesteamid() => __pbn__steamid != null;
        public void Resetsteamid() => __pbn__steamid = null;
        private ulong? __pbn__steamid;

        [global::ProtoBuf.ProtoMember(2)]
        public int chat_entry_type
        {
            get => __pbn__chat_entry_type.GetValueOrDefault();
            set => __pbn__chat_entry_type = value;
        }
        public bool ShouldSerializechat_entry_type() => __pbn__chat_entry_type != null;
        public void Resetchat_entry_type() => __pbn__chat_entry_type = null;
        private int? __pbn__chat_entry_type;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string message
        {
            get => __pbn__message ?? "";
            set => __pbn__message = value;
        }
        public bool ShouldSerializemessage() => __pbn__message != null;
        public void Resetmessage() => __pbn__message = null;
        private string __pbn__message;

        [global::ProtoBuf.ProtoMember(4)]
        public bool contains_bbcode
        {
            get => __pbn__contains_bbcode.GetValueOrDefault();
            set => __pbn__contains_bbcode = value;
        }
        public bool ShouldSerializecontains_bbcode() => __pbn__contains_bbcode != null;
        public void Resetcontains_bbcode() => __pbn__contains_bbcode = null;
        private bool? __pbn__contains_bbcode;

        [global::ProtoBuf.ProtoMember(5)]
        public bool echo_to_sender
        {
            get => __pbn__echo_to_sender.GetValueOrDefault();
            set => __pbn__echo_to_sender = value;
        }
        public bool ShouldSerializeecho_to_sender() => __pbn__echo_to_sender != null;
        public void Resetecho_to_sender() => __pbn__echo_to_sender = null;
        private bool? __pbn__echo_to_sender;

        [global::ProtoBuf.ProtoMember(6)]
        public bool low_priority
        {
            get => __pbn__low_priority.GetValueOrDefault();
            set => __pbn__low_priority = value;
        }
        public bool ShouldSerializelow_priority() => __pbn__low_priority != null;
        public void Resetlow_priority() => __pbn__low_priority = null;
        private bool? __pbn__low_priority;

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue("")]
        public string client_message_id
        {
            get => __pbn__client_message_id ?? "";
            set => __pbn__client_message_id = value;
        }
        public bool ShouldSerializeclient_message_id() => __pbn__client_message_id != null;
        public void Resetclient_message_id() => __pbn__client_message_id = null;
        private string __pbn__client_message_id;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CFriendMessages_SendMessage_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string modified_message
        {
            get => __pbn__modified_message ?? "";
            set => __pbn__modified_message = value;
        }
        public bool ShouldSerializemodified_message() => __pbn__modified_message != null;
        public void Resetmodified_message() => __pbn__modified_message = null;
        private string __pbn__modified_message;

        [global::ProtoBuf.ProtoMember(2)]
        public uint server_timestamp
        {
            get => __pbn__server_timestamp.GetValueOrDefault();
            set => __pbn__server_timestamp = value;
        }
        public bool ShouldSerializeserver_timestamp() => __pbn__server_timestamp != null;
        public void Resetserver_timestamp() => __pbn__server_timestamp = null;
        private uint? __pbn__server_timestamp;

        [global::ProtoBuf.ProtoMember(3)]
        public uint ordinal
        {
            get => __pbn__ordinal.GetValueOrDefault();
            set => __pbn__ordinal = value;
        }
        public bool ShouldSerializeordinal() => __pbn__ordinal != null;
        public void Resetordinal() => __pbn__ordinal = null;
        private uint? __pbn__ordinal;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string message_without_bb_code
        {
            get => __pbn__message_without_bb_code ?? "";
            set => __pbn__message_without_bb_code = value;
        }
        public bool ShouldSerializemessage_without_bb_code() => __pbn__message_without_bb_code != null;
        public void Resetmessage_without_bb_code() => __pbn__message_without_bb_code = null;
        private string __pbn__message_without_bb_code;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CFriendMessages_AckMessage_Notification : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong steamid_partner
        {
            get => __pbn__steamid_partner.GetValueOrDefault();
            set => __pbn__steamid_partner = value;
        }
        public bool ShouldSerializesteamid_partner() => __pbn__steamid_partner != null;
        public void Resetsteamid_partner() => __pbn__steamid_partner = null;
        private ulong? __pbn__steamid_partner;

        [global::ProtoBuf.ProtoMember(2)]
        public uint timestamp
        {
            get => __pbn__timestamp.GetValueOrDefault();
            set => __pbn__timestamp = value;
        }
        public bool ShouldSerializetimestamp() => __pbn__timestamp != null;
        public void Resettimestamp() => __pbn__timestamp = null;
        private uint? __pbn__timestamp;

    }

    [global::ProtoBuf.ProtoContract()]
    public class CSteamNotification_GetSteamNotifications_Request : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool include_hidden { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public int language { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public bool include_confirmation_count { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public bool include_pinned_counts { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public bool include_read { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public bool count_only { get; set; }
    }

    [global::ProtoBuf.ProtoContract()]
    public class CSteamNotification_GetSteamNotifications_Response : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public List<SteamNotificationData> notifications { get; } = new List<SteamNotificationData>();

        [global::ProtoBuf.ProtoMember(2)]
        public int confirmation_count { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public uint pending_gift_count { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public uint pending_friend_count { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public uint unread_count { get; set; }

        [global::ProtoBuf.ProtoMember(7)]
        public uint pending_family_invite_count { get; set; }
    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientUserNotifications : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<Notification> notifications { get; } = new global::System.Collections.Generic.List<Notification>();

        [global::ProtoBuf.ProtoContract()]
        public partial class Notification : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            /// <summary>
            /// <see cref="EUserNotificationType"/>
            /// </summary>
            [global::ProtoBuf.ProtoMember(1)]
            public uint user_notification_type
            {
                get => __pbn__user_notification_type.GetValueOrDefault();
                set => __pbn__user_notification_type = value;
            }
            public bool ShouldSerializeuser_notification_type() => __pbn__user_notification_type != null;
            public void Resetuser_notification_type() => __pbn__user_notification_type = null;
            private uint? __pbn__user_notification_type;

            [global::ProtoBuf.ProtoMember(2)]
            public uint count
            {
                get => __pbn__count.GetValueOrDefault();
                set => __pbn__count = value;
            }
            public bool ShouldSerializecount() => __pbn__count != null;
            public void Resetcount() => __pbn__count = null;
            private uint? __pbn__count;

        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientWalletInfoUpdate : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool has_wallet
        {
            get => __pbn__has_wallet.GetValueOrDefault();
            set => __pbn__has_wallet = value;
        }
        public bool ShouldSerializehas_wallet() => __pbn__has_wallet != null;
        public void Resethas_wallet() => __pbn__has_wallet = null;
        private bool? __pbn__has_wallet;

        [global::ProtoBuf.ProtoMember(2)]
        public int balance
        {
            get => __pbn__balance.GetValueOrDefault();
            set => __pbn__balance = value;
        }
        public bool ShouldSerializebalance() => __pbn__balance != null;
        public void Resetbalance() => __pbn__balance = null;
        private int? __pbn__balance;

        [global::ProtoBuf.ProtoMember(3)]
        public int currency
        {
            get => __pbn__currency.GetValueOrDefault();
            set => __pbn__currency = value;
        }
        public bool ShouldSerializecurrency() => __pbn__currency != null;
        public void Resetcurrency() => __pbn__currency = null;
        private int? __pbn__currency;

        [global::ProtoBuf.ProtoMember(4)]
        public int balance_delayed
        {
            get => __pbn__balance_delayed.GetValueOrDefault();
            set => __pbn__balance_delayed = value;
        }
        public bool ShouldSerializebalance_delayed() => __pbn__balance_delayed != null;
        public void Resetbalance_delayed() => __pbn__balance_delayed = null;
        private int? __pbn__balance_delayed;

        [global::ProtoBuf.ProtoMember(5)]
        public long balance64
        {
            get => __pbn__balance64.GetValueOrDefault();
            set => __pbn__balance64 = value;
        }
        public bool ShouldSerializebalance64() => __pbn__balance64 != null;
        public void Resetbalance64() => __pbn__balance64 = null;
        private long? __pbn__balance64;

        [global::ProtoBuf.ProtoMember(6)]
        public long balance64_delayed
        {
            get => __pbn__balance64_delayed.GetValueOrDefault();
            set => __pbn__balance64_delayed = value;
        }
        public bool ShouldSerializebalance64_delayed() => __pbn__balance64_delayed != null;
        public void Resetbalance64_delayed() => __pbn__balance64_delayed = null;
        private long? __pbn__balance64_delayed;

        [global::ProtoBuf.ProtoMember(7)]
        public int realm
        {
            get => __pbn__realm.GetValueOrDefault();
            set => __pbn__realm = value;
        }
        public bool ShouldSerializerealm() => __pbn__realm != null;
        public void Resetrealm() => __pbn__realm = null;
        private int? __pbn__realm;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientGMSServerQuery : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public uint app_id
        {
            get => __pbn__app_id.GetValueOrDefault();
            set => __pbn__app_id = value;
        }
        public bool ShouldSerializeapp_id() => __pbn__app_id != null;
        public void Resetapp_id() => __pbn__app_id = null;
        private uint? __pbn__app_id;

        [global::ProtoBuf.ProtoMember(2)]
        public uint geo_location_ip
        {
            get => __pbn__geo_location_ip.GetValueOrDefault();
            set => __pbn__geo_location_ip = value;
        }
        public bool ShouldSerializegeo_location_ip() => __pbn__geo_location_ip != null;
        public void Resetgeo_location_ip() => __pbn__geo_location_ip = null;
        private uint? __pbn__geo_location_ip;

        [global::ProtoBuf.ProtoMember(3)]
        public uint region_code
        {
            get => __pbn__region_code.GetValueOrDefault();
            set => __pbn__region_code = value;
        }
        public bool ShouldSerializeregion_code() => __pbn__region_code != null;
        public void Resetregion_code() => __pbn__region_code = null;
        private uint? __pbn__region_code;

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string filter_text
        {
            get => __pbn__filter_text ?? "";
            set => __pbn__filter_text = value;
        }
        public bool ShouldSerializefilter_text() => __pbn__filter_text != null;
        public void Resetfilter_text() => __pbn__filter_text = null;
        private string __pbn__filter_text;

        [global::ProtoBuf.ProtoMember(5)]
        public uint max_servers
        {
            get => __pbn__max_servers.GetValueOrDefault();
            set => __pbn__max_servers = value;
        }
        public bool ShouldSerializemax_servers() => __pbn__max_servers != null;
        public void Resetmax_servers() => __pbn__max_servers = null;
        private uint? __pbn__max_servers;

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string sdr_ping_location
        {
            get => __pbn__sdr_ping_location ?? "";
            set => __pbn__sdr_ping_location = value;
        }
        public bool ShouldSerializesdr_ping_location() => __pbn__sdr_ping_location != null;
        public void Resetsdr_ping_location() => __pbn__sdr_ping_location = null;
        private string __pbn__sdr_ping_location;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgGMSClientServerQueryResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<Server> servers { get; } = new global::System.Collections.Generic.List<Server>();

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string error
        {
            get => __pbn__error ?? "";
            set => __pbn__error = value;
        }
        public bool ShouldSerializeerror() => __pbn__error != null;
        public void Reseterror() => __pbn__error = null;
        private string __pbn__error;

        [global::ProtoBuf.ProtoMember(3)]
        public Server default_server_data { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public global::System.Collections.Generic.List<string> server_strings { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoContract()]
        public partial class Server : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public uint deprecated_server_ip
            {
                get => __pbn__deprecated_server_ip.GetValueOrDefault();
                set => __pbn__deprecated_server_ip = value;
            }
            public bool ShouldSerializedeprecated_server_ip() => __pbn__deprecated_server_ip != null;
            public void Resetdeprecated_server_ip() => __pbn__deprecated_server_ip = null;
            private uint? __pbn__deprecated_server_ip;

            [global::ProtoBuf.ProtoMember(2)]
            public uint query_port
            {
                get => __pbn__query_port.GetValueOrDefault();
                set => __pbn__query_port = value;
            }
            public bool ShouldSerializequery_port() => __pbn__query_port != null;
            public void Resetquery_port() => __pbn__query_port = null;
            private uint? __pbn__query_port;

            [global::ProtoBuf.ProtoMember(3)]
            public uint auth_players
            {
                get => __pbn__auth_players.GetValueOrDefault();
                set => __pbn__auth_players = value;
            }
            public bool ShouldSerializeauth_players() => __pbn__auth_players != null;
            public void Resetauth_players() => __pbn__auth_players = null;
            private uint? __pbn__auth_players;

            [global::ProtoBuf.ProtoMember(4)]
            public CMsgIPAddress server_ip { get; set; }

            [global::ProtoBuf.ProtoMember(6, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
            public ulong steam_id
            {
                get => __pbn__steam_id.GetValueOrDefault();
                set => __pbn__steam_id = value;
            }
            public bool ShouldSerializesteam_id() => __pbn__steam_id != null;
            public void Resetsteam_id() => __pbn__steam_id = null;
            private ulong? __pbn__steam_id;

            [global::ProtoBuf.ProtoMember(7)]
            public uint revision
            {
                get => __pbn__revision.GetValueOrDefault();
                set => __pbn__revision = value;
            }
            public bool ShouldSerializerevision() => __pbn__revision != null;
            public void Resetrevision() => __pbn__revision = null;
            private uint? __pbn__revision;

            [global::ProtoBuf.ProtoMember(8)]
            public uint players
            {
                get => __pbn__players.GetValueOrDefault();
                set => __pbn__players = value;
            }
            public bool ShouldSerializeplayers() => __pbn__players != null;
            public void Resetplayers() => __pbn__players = null;
            private uint? __pbn__players;

            [global::ProtoBuf.ProtoMember(9)]
            public uint game_port
            {
                get => __pbn__game_port.GetValueOrDefault();
                set => __pbn__game_port = value;
            }
            public bool ShouldSerializegame_port() => __pbn__game_port != null;
            public void Resetgame_port() => __pbn__game_port = null;
            private uint? __pbn__game_port;

            [global::ProtoBuf.ProtoMember(10, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
            public uint sdr_popid
            {
                get => __pbn__sdr_popid.GetValueOrDefault();
                set => __pbn__sdr_popid = value;
            }
            public bool ShouldSerializesdr_popid() => __pbn__sdr_popid != null;
            public void Resetsdr_popid() => __pbn__sdr_popid = null;
            private uint? __pbn__sdr_popid;

            [global::ProtoBuf.ProtoMember(32)]
            [global::System.ComponentModel.DefaultValue("")]
            public string sdr_ping_location
            {
                get => __pbn__sdr_ping_location ?? "";
                set => __pbn__sdr_ping_location = value;
            }
            public bool ShouldSerializesdr_ping_location() => __pbn__sdr_ping_location != null;
            public void Resetsdr_ping_location() => __pbn__sdr_ping_location = null;
            private string __pbn__sdr_ping_location;

            [global::ProtoBuf.ProtoMember(11)]
            public uint flags
            {
                get => __pbn__flags.GetValueOrDefault();
                set => __pbn__flags = value;
            }
            public bool ShouldSerializeflags() => __pbn__flags != null;
            public void Resetflags() => __pbn__flags = null;
            private uint? __pbn__flags;

            [global::ProtoBuf.ProtoMember(12)]
            public uint app_id
            {
                get => __pbn__app_id.GetValueOrDefault();
                set => __pbn__app_id = value;
            }
            public bool ShouldSerializeapp_id() => __pbn__app_id != null;
            public void Resetapp_id() => __pbn__app_id = null;
            private uint? __pbn__app_id;

            [global::ProtoBuf.ProtoMember(13)]
            public uint max_players
            {
                get => __pbn__max_players.GetValueOrDefault();
                set => __pbn__max_players = value;
            }
            public bool ShouldSerializemax_players() => __pbn__max_players != null;
            public void Resetmax_players() => __pbn__max_players = null;
            private uint? __pbn__max_players;

            [global::ProtoBuf.ProtoMember(14)]
            public uint bots
            {
                get => __pbn__bots.GetValueOrDefault();
                set => __pbn__bots = value;
            }
            public bool ShouldSerializebots() => __pbn__bots != null;
            public void Resetbots() => __pbn__bots = null;
            private uint? __pbn__bots;

            [global::ProtoBuf.ProtoMember(15)]
            public uint spectator_port
            {
                get => __pbn__spectator_port.GetValueOrDefault();
                set => __pbn__spectator_port = value;
            }
            public bool ShouldSerializespectator_port() => __pbn__spectator_port != null;
            public void Resetspectator_port() => __pbn__spectator_port = null;
            private uint? __pbn__spectator_port;

            [global::ProtoBuf.ProtoMember(16)]
            [global::System.ComponentModel.DefaultValue("")]
            public string gamedir_str
            {
                get => __pbn__gamedir_str ?? "";
                set => __pbn__gamedir_str = value;
            }
            public bool ShouldSerializegamedir_str() => __pbn__gamedir_str != null;
            public void Resetgamedir_str() => __pbn__gamedir_str = null;
            private string __pbn__gamedir_str;

            [global::ProtoBuf.ProtoMember(17)]
            public uint gamedir_strindex
            {
                get => __pbn__gamedir_strindex.GetValueOrDefault();
                set => __pbn__gamedir_strindex = value;
            }
            public bool ShouldSerializegamedir_strindex() => __pbn__gamedir_strindex != null;
            public void Resetgamedir_strindex() => __pbn__gamedir_strindex = null;
            private uint? __pbn__gamedir_strindex;

            [global::ProtoBuf.ProtoMember(18)]
            [global::System.ComponentModel.DefaultValue("")]
            public string map_str
            {
                get => __pbn__map_str ?? "";
                set => __pbn__map_str = value;
            }
            public bool ShouldSerializemap_str() => __pbn__map_str != null;
            public void Resetmap_str() => __pbn__map_str = null;
            private string __pbn__map_str;

            [global::ProtoBuf.ProtoMember(19)]
            public uint map_strindex
            {
                get => __pbn__map_strindex.GetValueOrDefault();
                set => __pbn__map_strindex = value;
            }
            public bool ShouldSerializemap_strindex() => __pbn__map_strindex != null;
            public void Resetmap_strindex() => __pbn__map_strindex = null;
            private uint? __pbn__map_strindex;

            [global::ProtoBuf.ProtoMember(20)]
            [global::System.ComponentModel.DefaultValue("")]
            public string name_str
            {
                get => __pbn__name_str ?? "";
                set => __pbn__name_str = value;
            }
            public bool ShouldSerializename_str() => __pbn__name_str != null;
            public void Resetname_str() => __pbn__name_str = null;
            private string __pbn__name_str;

            [global::ProtoBuf.ProtoMember(21)]
            public uint name_strindex
            {
                get => __pbn__name_strindex.GetValueOrDefault();
                set => __pbn__name_strindex = value;
            }
            public bool ShouldSerializename_strindex() => __pbn__name_strindex != null;
            public void Resetname_strindex() => __pbn__name_strindex = null;
            private uint? __pbn__name_strindex;

            [global::ProtoBuf.ProtoMember(22)]
            [global::System.ComponentModel.DefaultValue("")]
            public string game_description_str
            {
                get => __pbn__game_description_str ?? "";
                set => __pbn__game_description_str = value;
            }
            public bool ShouldSerializegame_description_str() => __pbn__game_description_str != null;
            public void Resetgame_description_str() => __pbn__game_description_str = null;
            private string __pbn__game_description_str;

            [global::ProtoBuf.ProtoMember(23)]
            public uint game_description_strindex
            {
                get => __pbn__game_description_strindex.GetValueOrDefault();
                set => __pbn__game_description_strindex = value;
            }
            public bool ShouldSerializegame_description_strindex() => __pbn__game_description_strindex != null;
            public void Resetgame_description_strindex() => __pbn__game_description_strindex = null;
            private uint? __pbn__game_description_strindex;

            [global::ProtoBuf.ProtoMember(24)]
            [global::System.ComponentModel.DefaultValue("")]
            public string version_str
            {
                get => __pbn__version_str ?? "";
                set => __pbn__version_str = value;
            }
            public bool ShouldSerializeversion_str() => __pbn__version_str != null;
            public void Resetversion_str() => __pbn__version_str = null;
            private string __pbn__version_str;

            [global::ProtoBuf.ProtoMember(25)]
            public uint version_strindex
            {
                get => __pbn__version_strindex.GetValueOrDefault();
                set => __pbn__version_strindex = value;
            }
            public bool ShouldSerializeversion_strindex() => __pbn__version_strindex != null;
            public void Resetversion_strindex() => __pbn__version_strindex = null;
            private uint? __pbn__version_strindex;

            [global::ProtoBuf.ProtoMember(26)]
            [global::System.ComponentModel.DefaultValue("")]
            public string gametype_str
            {
                get => __pbn__gametype_str ?? "";
                set => __pbn__gametype_str = value;
            }
            public bool ShouldSerializegametype_str() => __pbn__gametype_str != null;
            public void Resetgametype_str() => __pbn__gametype_str = null;
            private string __pbn__gametype_str;

            [global::ProtoBuf.ProtoMember(27)]
            public uint gametype_strindex
            {
                get => __pbn__gametype_strindex.GetValueOrDefault();
                set => __pbn__gametype_strindex = value;
            }
            public bool ShouldSerializegametype_strindex() => __pbn__gametype_strindex != null;
            public void Resetgametype_strindex() => __pbn__gametype_strindex = null;
            private uint? __pbn__gametype_strindex;

            [global::ProtoBuf.ProtoMember(30)]
            [global::System.ComponentModel.DefaultValue("")]
            public string spectator_name_str
            {
                get => __pbn__spectator_name_str ?? "";
                set => __pbn__spectator_name_str = value;
            }
            public bool ShouldSerializespectator_name_str() => __pbn__spectator_name_str != null;
            public void Resetspectator_name_str() => __pbn__spectator_name_str = null;
            private string __pbn__spectator_name_str;

            [global::ProtoBuf.ProtoMember(31)]
            public uint spectator_name_strindex
            {
                get => __pbn__spectator_name_strindex.GetValueOrDefault();
                set => __pbn__spectator_name_strindex = value;
            }
            public bool ShouldSerializespectator_name_strindex() => __pbn__spectator_name_strindex != null;
            public void Resetspectator_name_strindex() => __pbn__spectator_name_strindex = null;
            private uint? __pbn__spectator_name_strindex;

        }

        [global::ProtoBuf.ProtoContract()]
        public enum EFlags
        {
            k_EFlag_HasPassword = 1,
            k_EFlag_Secure = 2,
        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientAccountInfo : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string persona_name
        {
            get => __pbn__persona_name ?? "";
            set => __pbn__persona_name = value;
        }
        public bool ShouldSerializepersona_name() => __pbn__persona_name != null;
        public void Resetpersona_name() => __pbn__persona_name = null;
        private string __pbn__persona_name;

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string ip_country
        {
            get => __pbn__ip_country ?? "";
            set => __pbn__ip_country = value;
        }
        public bool ShouldSerializeip_country() => __pbn__ip_country != null;
        public void Resetip_country() => __pbn__ip_country = null;
        private string __pbn__ip_country;

        [global::ProtoBuf.ProtoMember(5)]
        public int count_authed_computers
        {
            get => __pbn__count_authed_computers.GetValueOrDefault();
            set => __pbn__count_authed_computers = value;
        }
        public bool ShouldSerializecount_authed_computers() => __pbn__count_authed_computers != null;
        public void Resetcount_authed_computers() => __pbn__count_authed_computers = null;
        private int? __pbn__count_authed_computers;

        [global::ProtoBuf.ProtoMember(7)]
        public uint account_flags
        {
            get => __pbn__account_flags.GetValueOrDefault();
            set => __pbn__account_flags = value;
        }
        public bool ShouldSerializeaccount_flags() => __pbn__account_flags != null;
        public void Resetaccount_flags() => __pbn__account_flags = null;
        private uint? __pbn__account_flags;

        [global::ProtoBuf.ProtoMember(15)]
        [global::System.ComponentModel.DefaultValue("")]
        public string steamguard_machine_name_user_chosen
        {
            get => __pbn__steamguard_machine_name_user_chosen ?? "";
            set => __pbn__steamguard_machine_name_user_chosen = value;
        }
        public bool ShouldSerializesteamguard_machine_name_user_chosen() => __pbn__steamguard_machine_name_user_chosen != null;
        public void Resetsteamguard_machine_name_user_chosen() => __pbn__steamguard_machine_name_user_chosen = null;
        private string __pbn__steamguard_machine_name_user_chosen;

        [global::ProtoBuf.ProtoMember(16)]
        public bool is_phone_verified
        {
            get => __pbn__is_phone_verified.GetValueOrDefault();
            set => __pbn__is_phone_verified = value;
        }
        public bool ShouldSerializeis_phone_verified() => __pbn__is_phone_verified != null;
        public void Resetis_phone_verified() => __pbn__is_phone_verified = null;
        private bool? __pbn__is_phone_verified;

        [global::ProtoBuf.ProtoMember(17)]
        public uint two_factor_state
        {
            get => __pbn__two_factor_state.GetValueOrDefault();
            set => __pbn__two_factor_state = value;
        }
        public bool ShouldSerializetwo_factor_state() => __pbn__two_factor_state != null;
        public void Resettwo_factor_state() => __pbn__two_factor_state = null;
        private uint? __pbn__two_factor_state;

        [global::ProtoBuf.ProtoMember(18)]
        public bool is_phone_identifying
        {
            get => __pbn__is_phone_identifying.GetValueOrDefault();
            set => __pbn__is_phone_identifying = value;
        }
        public bool ShouldSerializeis_phone_identifying() => __pbn__is_phone_identifying != null;
        public void Resetis_phone_identifying() => __pbn__is_phone_identifying = null;
        private bool? __pbn__is_phone_identifying;

        [global::ProtoBuf.ProtoMember(19)]
        public bool is_phone_needing_reverify
        {
            get => __pbn__is_phone_needing_reverify.GetValueOrDefault();
            set => __pbn__is_phone_needing_reverify = value;
        }
        public bool ShouldSerializeis_phone_needing_reverify() => __pbn__is_phone_needing_reverify != null;
        public void Resetis_phone_needing_reverify() => __pbn__is_phone_needing_reverify = null;
        private bool? __pbn__is_phone_needing_reverify;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientEmailAddrInfo : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string email_address
        {
            get => __pbn__email_address ?? "";
            set => __pbn__email_address = value;
        }
        public bool ShouldSerializeemail_address() => __pbn__email_address != null;
        public void Resetemail_address() => __pbn__email_address = null;
        private string __pbn__email_address;

        [global::ProtoBuf.ProtoMember(2)]
        public bool email_is_validated
        {
            get => __pbn__email_is_validated.GetValueOrDefault();
            set => __pbn__email_is_validated = value;
        }
        public bool ShouldSerializeemail_is_validated() => __pbn__email_is_validated != null;
        public void Resetemail_is_validated() => __pbn__email_is_validated = null;
        private bool? __pbn__email_is_validated;

        [global::ProtoBuf.ProtoMember(3)]
        public bool email_validation_changed
        {
            get => __pbn__email_validation_changed.GetValueOrDefault();
            set => __pbn__email_validation_changed = value;
        }
        public bool ShouldSerializeemail_validation_changed() => __pbn__email_validation_changed != null;
        public void Resetemail_validation_changed() => __pbn__email_validation_changed = null;
        private bool? __pbn__email_validation_changed;

        [global::ProtoBuf.ProtoMember(4)]
        public bool credential_change_requires_code
        {
            get => __pbn__credential_change_requires_code.GetValueOrDefault();
            set => __pbn__credential_change_requires_code = value;
        }
        public bool ShouldSerializecredential_change_requires_code() => __pbn__credential_change_requires_code != null;
        public void Resetcredential_change_requires_code() => __pbn__credential_change_requires_code = null;
        private bool? __pbn__credential_change_requires_code;

        [global::ProtoBuf.ProtoMember(5)]
        public bool password_or_secretqa_change_requires_code
        {
            get => __pbn__password_or_secretqa_change_requires_code.GetValueOrDefault();
            set => __pbn__password_or_secretqa_change_requires_code = value;
        }
        public bool ShouldSerializepassword_or_secretqa_change_requires_code() => __pbn__password_or_secretqa_change_requires_code != null;
        public void Resetpassword_or_secretqa_change_requires_code() => __pbn__password_or_secretqa_change_requires_code = null;
        private bool? __pbn__password_or_secretqa_change_requires_code;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientVanityURLChangedNotification : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string vanity_url
        {
            get => __pbn__vanity_url ?? "";
            set => __pbn__vanity_url = value;
        }
        public bool ShouldSerializevanity_url() => __pbn__vanity_url != null;
        public void Resetvanity_url() => __pbn__vanity_url = null;
        private string __pbn__vanity_url;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientRequestWebAPIAuthenticateUserNonce : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue(-1)]
        public int token_type
        {
            get => __pbn__token_type ?? -1;
            set => __pbn__token_type = value;
        }
        public bool ShouldSerializetoken_type() => __pbn__token_type != null;
        public void Resettoken_type() => __pbn__token_type = null;
        private int? __pbn__token_type;

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class CMsgClientRequestWebAPIAuthenticateUserNonceResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue(2)]
        public int eresult
        {
            get => __pbn__eresult ?? 2;
            set => __pbn__eresult = value;
        }
        public bool ShouldSerializeeresult() => __pbn__eresult != null;
        public void Reseteresult() => __pbn__eresult = null;
        private int? __pbn__eresult;

        [global::ProtoBuf.ProtoMember(11)]
        [global::System.ComponentModel.DefaultValue("")]
        public string webapi_authenticate_user_nonce
        {
            get => __pbn__webapi_authenticate_user_nonce ?? "";
            set => __pbn__webapi_authenticate_user_nonce = value;
        }
        public bool ShouldSerializewebapi_authenticate_user_nonce() => __pbn__webapi_authenticate_user_nonce != null;
        public void Resetwebapi_authenticate_user_nonce() => __pbn__webapi_authenticate_user_nonce = null;
        private string __pbn__webapi_authenticate_user_nonce;

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue(-1)]
        public int token_type
        {
            get => __pbn__token_type ?? -1;
            set => __pbn__token_type = value;
        }
        public bool ShouldSerializetoken_type() => __pbn__token_type != null;
        public void Resettoken_type() => __pbn__token_type = null;
        private int? __pbn__token_type;

    }
}
