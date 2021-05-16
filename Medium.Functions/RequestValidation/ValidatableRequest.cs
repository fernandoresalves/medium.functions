using FluentValidation.Results;
using System.Collections.Generic;

namespace Medium.Functions
{
    public class ValidatableRequest<T>
    {
        public T Value { get; set; }
        public bool IsValid { get; set; }
        public IList<ValidationFailure> Errors { get; set; }
    }
}
