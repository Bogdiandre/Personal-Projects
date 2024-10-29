using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidationAge : ValidationAttribute
{
    private readonly int _minimumAge;

    public ValidationAge(int minimumAge)
    {
        _minimumAge = minimumAge;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateOfBirth)
        {
            
            int age = DateTime.Now.Year - dateOfBirth.Year;

            
            if (DateTime.Now < dateOfBirth.AddYears(age))
            {
                age--;
            }

            if (age >= _minimumAge)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Employee must be at least {_minimumAge} years old.");
        }

        return new ValidationResult("Invalid date format.");
    }
}

