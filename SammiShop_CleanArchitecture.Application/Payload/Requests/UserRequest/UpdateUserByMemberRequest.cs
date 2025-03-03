﻿namespace SammiShop_CleanArchitecture.Application.Payload.Requests.UserRequest
{
    public class UpdateUserByMemberRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UrlAvt { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? FullName { get; set; }
    }
}
