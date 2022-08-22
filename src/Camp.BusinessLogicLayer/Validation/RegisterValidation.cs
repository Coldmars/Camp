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
                throw new ValidateException("Invalid register model.");

            if (roleId <= 0 ||
                roleId > ((int)Roles.Role.Volunteer))
                throw new ValidateException("Invalid role.");

            if (model.Name is null)
                throw new ValidateException("Name is required.");

            if (roleId == ((int)Roles.Role.Squad))
                ValidateSquadModel(model);
        }

        private void ValidateSquadModel(RegisterUserModel model)
        {
            if (model.Location is null ||
                model.PhoneNumber is null)
            {
                throw new ValidateException("Location and Phone fields are required.");
            }

            new PhoneNumberValidation().Validate(model.PhoneNumber);
        }
    }
}
