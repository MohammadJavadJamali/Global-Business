using System;
using Domain.DTO;
using Domain.Model;

namespace Application.Helpers
{
    public static class MapUserHelper
    {
        public static AppUser MapAppUser(RegisterDto registerDto, AppUser parentUser) =>
             new AppUser()
             {

                 Email = registerDto.Email,
                 ParentId = parentUser.Id,
                 RegisterDate = DateTime.Now,
                 LastName = registerDto.LastName,
                 UserName = registerDto.UserName,
                 FirstName = registerDto.FirstName,
                 PhoneNumber = registerDto.PhonNumber,
                 AccountBalance = registerDto.AccountBalance,
                 IntroductionCode = StringGenerator.RandomString(),

             };
    }
}
