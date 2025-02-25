namespace SammiShop_CleanArchitecture.Domain.Constants
{
    public static class ConstantDomain
    {
        public const int MIN_USERNAME = 3; //Độ dài tối thiểu của 1 tên đăng nhập

        public const int MAX_USERNAME = 50; //Độ dài tối đa của 1 tên đăng nhập

        public const int MIN_PASSWORD = 8; //Độ dài tối thiểu của mật khẩu

        public const int MAX_PASSWORD = 20; //Độ dài tối đa của mật khẩu

        public const string PASSWORD_REGEX = "[!@#$%^&*()_+{}\\[\\]:;<>,.?/~`]"; //Regex kiểm tra mật khẩu

        public const string NUMBER_REGEX = @"^\+?[0-9]{10,15}$"; //Regex kiểm tra số điện thoại

    }
}
