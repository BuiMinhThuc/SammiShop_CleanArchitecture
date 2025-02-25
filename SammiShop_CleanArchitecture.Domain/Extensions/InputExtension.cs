using SammiShop_CleanArchitecture.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SammiShop_CleanArchitecture.Domain.Extensions
{
    public static class InputExtension
    {
        public static bool IsValidUsername(this string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }
            if (username.Length < ConstantDomain.MIN_USERNAME || username.Length > ConstantDomain.MAX_USERNAME)
            {
                return false;
            }
            foreach (char c in username)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsPassWord(this string password)
        {


            if (password.Length < ConstantDomain.MIN_PASSWORD 
                || password.Length > ConstantDomain.MAX_PASSWORD 
                || !new Regex(ConstantDomain.PASSWORD_REGEX).IsMatch(password))
                return false;
            else
                return true;
        }
        public static bool IsValiEmail(this string email)
        {

            var CheckEmail = new EmailAddressAttribute();
            return CheckEmail.IsValid(email);
        }

        public static bool IsValidPhoneNumber(this string phoneNumber)
        {

            if (phoneNumber == null)
            {
                return false;
            }
            if (Regex.IsMatch(phoneNumber, ConstantDomain.NUMBER_REGEX))
            {
                return false;

            }
            return true;
        }

        // TODO: Check if the image is valid
        /*public static bool IsImage(IFormFile imageFile)
        {
            int maxSizeInBytes = (2 * 1024 * 768);
            try
            {
                using (var image = SixLabors.ImageSharp.Image.Load<Rg32>(imageFile.OpenReadStream()))
                {
                    if (image.Width > 0 && image.Height > 0)
                    {
                        if (imageFile.Length <= maxSizeInBytes)
                        {
                            return true;
                        }
                        if (imageFile.Length > maxSizeInBytes)
                        {
                            throw new NotImplementedException("Kích thước file quá lớn");
                        }
                    }
                }
            }
            catch
            {
                if (imageFile.Length > maxSizeInBytes)
                {
                    throw new NotImplementedException("Kích thước file quá lớn");
                }
                else
                {
                    throw new NotImplementedException("File này không phải file có định dạng ảnh");

                }
            }
            return false;
        }*/
    }
}
