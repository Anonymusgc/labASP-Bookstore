using System.ComponentModel.DataAnnotations;

namespace labASP.Models;

public class IsbnAttribute :  ValidationAttribute
{
    private bool RequireControllSum ;

    public IsbnAttribute(bool RequireControllSum)
    {
        this.RequireControllSum = RequireControllSum;
    }

    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            ErrorMessage = $"Isbn nie może mieć wartości null";
            return false;
        }
        if (value is not string)
        {
            ErrorMessage = $"Isbn musi być podany jako łańcuch";
            return false;
            
        }
        string isbn = ((string)value).Replace("-", "").Replace(" ", "").Trim();
        if (isbn.Length != 10 && isbn.Length != 13)
        {
            ErrorMessage = "Isbn musi miec długośc 10 lub 13";
            return false;
        }
        
        for (int i = 0; i < isbn.Length; i++)
        {
            bool isLastCharXInIsbn10 = (isbn.Length == 10 && i == 9 && (isbn[i] == 'X' || isbn[i] == 'x'));
            if (!char.IsDigit(isbn[i]) && !isLastCharXInIsbn10)
            {
                ErrorMessage = "Isbn może zawierać wyłącznie cyfry (oraz myślniki lub spacje)";
                return false;
            }
        }

        if (RequireControllSum)
        {
            if (isbn.Length == 13)
            {
                int sum = 0;
                for (int i = 0; i < 13; i++)
                {
                    int digit = isbn[i] - '0';
                    int weight = (i % 2 == 0) ? 1 : 3;
                    sum += digit * weight;
                }

                if (sum % 10 != 0)
                {
                    ErrorMessage = "Nieprawidłowa suma kontrolna ISBN-13";
                    return false;
                }
            }
            else if (isbn.Length == 10)
            {
                int sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    sum += (isbn[i] - '0') * (10 - i);
                }

                int lastDigit = (isbn[9] == 'X' || isbn[9] == 'x') ? 10 : (isbn[9] - '0');
                sum += lastDigit;

                if (sum % 11 != 0)
                {
                    ErrorMessage = "Nieprawidłowa suma kontrolna ISBN-10";
                    return false;
                } 
            }
        }
        
        return true;
    }
}