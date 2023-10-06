using System.ComponentModel.DataAnnotations;

namespace Product__Application.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]

    public class FileSizeAttribute:ValidationAttribute
    {
        private readonly long _maxBytes;

        public FileSizeAttribute(long maxBytes)
        {
            _maxBytes = maxBytes;
        }

        public override bool IsValid(object value)
        {
            if (value is IFormFile file)
            {
                if (file.Length <= _maxBytes)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
