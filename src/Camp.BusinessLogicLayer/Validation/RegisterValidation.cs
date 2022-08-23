using Camp.Common.Exceptions;
using Camp.Common.Models;
using Camp.DataAccess.Enums;

namespace Camp.BusinessLogicLayer.Validation
{
    public class RegisterValidation
    {
        public void ValidateModel(RegisterUserModel model, int roleId)
        {
            if (model is null)
                throw new ValidateException("Invalid register model.", "Invalid_Model");

            if (roleId <= 0 ||
                roleId > ((int)Roles.Role.Volunteer))
                throw new ValidateException("Invalid role.", "Invalid_Role");

            if (model.Name is null)
                throw new ValidateException("Name is required.", "Invalid_Name");

            if (roleId == ((int)Roles.Role.Squad))
                ValidateSquadModel(model);
        }

        private void ValidateSquadModel(RegisterUserModel model)
        {

            if (model.Location is null)
            {
                throw new ValidateException("Location is required.", "Invalid_Location");
            }

            if (model.PhoneNumber is null)
            {
                throw new ValidateException("Phone number is required.", "Invalid_Phone");
            }

            new PhoneNumberValidation().Validate(model.PhoneNumber);
        }
    }
}
