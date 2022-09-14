using Camp.Common.Exceptions;

namespace Camp.BusinessLogicLayer.Validation
{
    public class UrlValidation
    {
        public void Validate(string url)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new ValidateException("Invalid url.", "Invalid_Url");
        }
    }
}
