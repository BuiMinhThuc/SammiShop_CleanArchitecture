namespace SammiShop_CleanArchitecture.Domain.Constants
{
    public static class ConstantDomain
    {

        public const int MIN_USERNAME_LENGTH = 3;

        public const int MAX_USERNAME_LENGTH = 50;

        public const int MIN_PASSWORD_LENGTH = 8;

        public const int MAX_PASSWORD_LENGTH = 20;

        public const int MAX_PAGESIZE_LENGTH = 50;

        public const int MIN_PAGESIZE_LENGTH = 1;

        public const int MIN_PAGENUMBER_LENGTH = 1;

        public const string PASSWORD_REGEX = "[!@#$%^&*()_+{}\\[\\]:;<>,.?/~`]";

        public const string PHONENUMBER_REGEX = @"^\+?[0-9]{10,15}$";

        public const int TIME_EXPIRED_EMAIL = 5;

    }
}
