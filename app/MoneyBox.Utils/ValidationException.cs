namespace MoneyBox.Utils
{
    using System;
    using System.Collections.Generic;

    public class ValidationException : Exception
    {
        public ValidationException(IDictionary<string, string> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            if (errors.Count == 0)
            {
                throw new ArgumentException("Errors must not be empty", nameof(errors));
            }

            this.Errors = errors;
        }

        public IDictionary<string, string> Errors { get; set; }
    }
}
