using Camp.Common.Exceptions;
using PhoneNumbers;

namespace Camp.BusinessLogicLayer.Validation
{
    public class PhoneNumberValidation
    {
        private const string RegionCode = "RU";

        public void Validate(string phone)
        {
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber phoneNumber = phoneUtil.Parse(phone, RegionCode);

            bool isValidNumber = phoneUtil.IsValidNumber(phoneNumber);

            if (!isValidNumber)
                throw new ValidateException("Invalid phone number");
        }
    }
}
