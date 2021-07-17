using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System;

public class PasswordStringController : MonoBehaviour {

    public Text passwordStrength;
    public InputField inputField;

	// Use this for initialization
	void Start () {
        try
        {
            inputField.onValueChanged.AddListener(delegate { PasswordStrengthCheck(); });
        }
        catch (NullReferenceException ex)
        {
            
        }
	}

    public char PasswordCheck(string text, int charIndex, char addedChar)
    {
        if (inputField.text.Length + addedChar > 2)
        {
            string password = inputField.text + addedChar;
            PasswordScore passwordStrengthScore = PasswordAdvisor.CheckStrength(password);

           // Debug.Log(passwordStrengthScore + ": " + inputField.text + addedChar);

            switch (passwordStrengthScore)
            {
                case PasswordScore.Blank:
                case PasswordScore.VeryWeak:
                case PasswordScore.Weak:
                   //  Debug.Log("weak password");
                    passwordStrength.text = "weak password";
                    passwordStrength.color = new Color(255.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
                    break;
                case PasswordScore.Medium:
                case PasswordScore.Strong:
                case PasswordScore.VeryStrong:
                   //  Debug.Log("strong password");
                    passwordStrength.text = "strong password";
                    passwordStrength.color = new Color(0.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f);
                    break;
            }
        }
        return addedChar;
    }

    public void PasswordStrengthCheck()
    {
        if (inputField.text.Length >= 0)
        {

            string password = inputField.text;
            PasswordScore passwordStrengthScore = PasswordAdvisor.CheckStrength(password);

           //  Debug.Log(passwordStrengthScore + ": " + inputField.text);

            switch (passwordStrengthScore)
            {
                case PasswordScore.Blank:
                    passwordStrength.text = "Try a combination of factors, or increasing length";
                    passwordStrength.color = new Color(0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
                    break;
                case PasswordScore.VeryWeak:
                    // Debug.Log("Very weak password");
                    passwordStrength.text = "Weaker password";
                    passwordStrength.color = new Color(255.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
                    break;
                case PasswordScore.Weak:
                    // Debug.Log("weak password");
                    passwordStrength.text = "Weak password";
                    passwordStrength.color = new Color(255.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
                    break;
                case PasswordScore.Medium:
                    // Debug.Log("Medium password");
                    passwordStrength.text = "Medium password";
                    passwordStrength.color = new Color(120.0f / 255.0f, 120.0f / 255.0f, 0.0f / 255.0f);
                    break;
                case PasswordScore.Strong:
                    // Debug.Log("Strong password");
                    passwordStrength.text = "Strong password";
                    passwordStrength.color = new Color(0.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f);
                    break;
                case PasswordScore.VeryStrong:
                    // Debug.Log("strong password");
                    passwordStrength.text = "Stronger password";
                    passwordStrength.color = new Color(21.0f / 255.0f, 181.0f / 255.0f, 109.0f / 255.0f);
                    break;
                case PasswordScore.Secure:
                    passwordStrength.text = "Long passwords should be your first concern, if nothing else";
                    passwordStrength.color = new Color(21.0f / 255.0f, 181.0f / 255.0f, 109.0f / 255.0f);
                    break;

            }
        }
    }

}

public enum PasswordScore
{
    Blank = 0,
    VeryWeak = 1,
    Weak = 2,
    Medium = 3,
    Strong = 4,
    VeryStrong = 5,
    Secure
}

public class PasswordAdvisor
{
    public static PasswordScore CheckStrength(string password)
    {
        /*int score = 0;

        if (password.Length < 1)
            return PasswordScore.Blank;
        if (password.Length < 4)
            return PasswordScore.VeryWeak;

        if (password.Length >= 8)
            score++;
        if (password.Length >= 12)
            score++;
        if (Regex.IsMatch(password, @"/\d+/", RegexOptions.ECMAScript))
            score++;
        if (Regex.IsMatch(password, @"/[a-z]/", RegexOptions.ECMAScript) && Regex.IsMatch(password, @"/[A-Z]/", RegexOptions.ECMAScript))
            score++;
        if (Regex.IsMatch(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript))
            score++;

        return (PasswordScore)score;
        */


        int score = 0;
        if (password.Length <= 2) return PasswordScore.Blank;
        if (!HasMinimumLength(password, 5)) return PasswordScore.VeryWeak;
        if (HasMinimumLength(password, 8)) score++;
        if (HasMinimumLength(password, 12)) return PasswordScore.Secure;
        if (HasUpperCaseLetter(password) && HasLowerCaseLetter(password)) score++;
        if (HasDigit(password)) score++;
        if (HasSpecialChar(password)) score++;
        if (HasWhiteSpace(password)) score++;
        if (score == 0) return PasswordScore.VeryWeak;
        return (PasswordScore)score;

    }

    public static bool HasMinimumLength(string password, int minLength)
    {
        return password.Length >= minLength;
    }
    public static bool HasUpperCaseLetter(string password)
    {
        return password.Any(c => char.IsUpper(c));
    }
    public static bool HasLowerCaseLetter(string password)
    {
        return password.Any(c => char.IsLower(c));
    }
    public static bool HasDigit(string password)
    {
        return password.Any(c => char.IsDigit(c));
    }
    public static bool HasSpecialChar(string password)
    {
        return password.IndexOfAny("!@#$%^&*?_~-£().,".ToCharArray()) != -1;
    }
    public static bool HasWhiteSpace(string password)
    {
        return password.Any(x => char.IsWhiteSpace(x));
    }
}