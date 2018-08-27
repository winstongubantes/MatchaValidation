using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Matcha.Validation
{
    public class StrongPasswordRule : ValidationRule<string>
    {
        private PasswordScore _score;

        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }

        public StrongPasswordRule(string propertyName) : base(propertyName)
        {
        }

        public PasswordScore Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }

        protected override bool CheckValue(string value)
        {
            Score = CheckStrength(value);
            ValidationMessage = GetScoreLabel();
            var scoreVal = (int) Score;
            return scoreVal > 2;
        }

        private string GetScoreLabel()
        {
            switch (Score)
            {
                case PasswordScore.Blank:
                case PasswordScore.VeryWeak:
                    return "Password is very weak";
                case PasswordScore.Weak:
                    return "Password is  weak";
                case PasswordScore.Medium:
                    return "Password is good";
                case PasswordScore.Strong:
                    return "Password is strong";
                case PasswordScore.VeryStrong:
                    return "Password is very strong";
            }

            return string.Empty;
        }

        private PasswordScore CheckStrength(string password)
        {
            int score = 0;

            if(string.IsNullOrWhiteSpace(password)) return PasswordScore.Blank;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;
            if (password.Length < 8)
                return PasswordScore.Weak;
            if (password.Length >= 8)
                return PasswordScore.Medium;
            if (password.Length >= 12)
                return PasswordScore.Strong;
            if (Regex.IsMatch(password, @"[\d]", RegexOptions.ECMAScript))
                return PasswordScore.Strong;
            if (Regex.IsMatch(password, @"[a-z]", RegexOptions.ECMAScript) ||
                Regex.IsMatch(password, @"[A-Z]", RegexOptions.ECMAScript))
                return PasswordScore.VeryStrong;
            if (Regex.IsMatch(password, @"[~`!@#$%\^\&\*\(\)\-_\+=\[\{\]\}\|\\;:'\""<\,>\.\?\/£]", RegexOptions.ECMAScript))
                return PasswordScore.VeryStrong;

            return (PasswordScore)score;
        }
    }
}
