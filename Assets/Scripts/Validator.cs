using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System;

public class Validator : MonoBehaviour {

	public static string EmailValid(InputField email)
	{
		bool isEmail = Regex.IsMatch(email.text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
		{	
			return IsInvalidCheck(isEmail, email);
        }
	}

	public static string IsIntAndPositive(InputField field)
	{
        int number = 0;
        bool valid = IsInt(field.text,ref number);

        if (valid)
		{
            if(IsNumberNegative(number)) valid = false;
		}

        return IsInvalidCheck(valid, field);

	}

    public static string IsIFloatAndPositive(InputField field)
    {
        float number = 0;
        bool valid = IsFloat(field.text, ref number);

        if (valid)
        {
            if (IsNumberNegative(number)) valid = false;
        }

        return IsInvalidCheck(valid, field);

    }

    public static bool IsInt(string input, ref int number)
    {
        bool isInt = int.TryParse(input, out number);
        return isInt;

    }

    public static bool IsFloat(string input, ref float number)
    {
        bool isFloat = float.TryParse(input, out number);
        return isFloat;

    }

    public static bool IsNumberNegative(float number)
    {
        return (number < 0);
    }

    public static string IsPhoneNumber(InputField field)
    {
        IsIntAndPositive(field);
        bool valid = (field.text.Length==10)? true : false;

        return IsInvalidCheck(valid, field);
    }

    public static string Empty(InputField field)
	{
		return (field.text.Length<1)?"Required " + field.name + "\n": null;
	}

	public static string PasswordMatch(InputField password,InputField confirmPassword)
	{
		return (password.text != confirmPassword.text)?"Required passwords don't match\n": null;
	}
	public static string CheckEmptyFields(InputField[] fields)
	{
		string errorMessages = null;

		foreach(InputField field in fields)
		{
			errorMessages += Empty(field);	
		}
		return errorMessages;
	}

    public static string IsInvalidCheck(bool check, InputField field)
    {
        return (!check) ? "Invalid " + field.name + "\n" : null;
    }


	public static string FieldsValidtionsExplorer(InputField email,InputField num,InputField password,InputField confirmPassword )
	{
		return(EmailValid(email) + IsIntAndPositive(num) + PasswordMatch(password,confirmPassword)); 
	}

    public static string FieldsValidtionsPromoter(InputField email, InputField telNo, InputField password, InputField confirmPassword)
    {
        return (EmailValid(email) + IsPhoneNumber(telNo) + PasswordMatch(password, confirmPassword));
    }

    public static string FieldsValidtionsEvents(InputField price)
    {
        return (IsIFloatAndPositive(price));
    }
}
