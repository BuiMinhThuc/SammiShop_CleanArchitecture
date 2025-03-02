namespace SammiShop_CleanArchitecture.Persistence.Constants
{
    public static class ConstantPersistence
    {
        public const string ADMIN_ID = "f1bf2c9a-7565-466f-97d2-d4a65afecd1c";

        public const string MEMBER_ID = "4a1d6151-c03d-4fd4-88ed-485bdbbcb12b";

        public const int EXPIRED_TIME_TOKEN = 1;

        public const int EXPIRED_TIME_OTP_EMAIL = 5;

        public const int EXPIRED_TIME_REFRESHTOKEN = 10;

        public const string INVALID_TOKEN = "Token không hợp lệ";

        public const string NOT_FOUND_TOKEN = "RefreshToken không tồn tại trong database";

        public const string TOKEN_NOT_EXPIRED = "Token không hợp lệ";

        public const string USER_NOT_EXIST = "Người dùng không tồn tại";

        public const string RENEW_TOKEN_SUCCESS = "Làm mới token thành công";

        public static string CONTENT_MAIL_REGISTER(string otp)
            => $"Mã kích hoạt của quý khách là : {otp}." +
             $"\nMã sẽ hết hạn sau {EXPIRED_TIME_OTP_EMAIL} !";

        public const string SUBJECT_EMAIL_REGISTER = "SAMMISHOP - XÁC NHẬN ĐĂNG KÍ TÀI KHOẢN !";

        public const string ACTIVATE_USER_SUCCESS = "Kích hoạt tài khoản thành công !";

        public const string LINK_AVATAR_DEFAULT = "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=";



    }
}
