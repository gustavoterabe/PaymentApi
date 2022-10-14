using System.Net.Mail;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;
using Payment.Domain.Models;

namespace Payment.Application.Validators;

public class SalesmanValidator : AbstractValidator<Salesman>
{
    public SalesmanValidator()
    {
        RuleFor(sm => sm.Document)
            .NotEmpty().WithMessage("The field document is required!")
            .Length(11, 14).WithMessage("The field document must contain a CPF");

        RuleFor(sm => sm.Name.Trim())
            .NotEmpty().WithMessage("Field name is required!");
        RuleFor(sm => sm.Name.Trim().Length)
            .GreaterThan(3).WithMessage("The name must contain at least 3 letters");

        RuleFor(sm => sm.Email)
            .Must(IsEmail).WithMessage("Email invalid!");

        RuleFor(sm => sm.Document)
            .Must(IsCpf).WithMessage("CPF invalid");

    }

    private static bool IsEmail(string email)
    {
        const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                               + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                               + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        
        Regex regex = new(pattern, RegexOptions.IgnoreCase);

        return regex.IsMatch(email.Trim());
    }
    
    private static bool IsCpf(string cpf)
    {
        int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;
        
        string tempCpf = cpf.Substring(0, 9);
        int sum = 0;
        for(int i=0; i<9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
        
        int rest = sum % 11;
        if ( rest < 2 )
            rest = 0;
        else
            rest = 11 - rest;
        
        string digit = rest.ToString();
        
        sum = 0;
        tempCpf += digit;

        for(int i=0; i<10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
        
        rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;
        
        digit += rest.ToString();
        
        return cpf.EndsWith(digit);
    }
}