namespace Assets.Scripts.Client.Resource
{
    using System.Collections.Generic;

    public static class SessionResource
    {
        private static Dictionary<string, string> Resource { get; set; } = new Dictionary<string, string>()
        {
            { "Error_Happened", "An error has occured.Please restart the application and try again."},
            { "Password_Or_Email_Wrong", "Your e-mail or your password are wrong."},
            { "Player_Mail_Format", "Your e-mail must be valid."},
            { "Player_Mail_Max_Length", "Your e-mail can't go over 100 characters."},
            { "Player_Mail_Required", "Your e-mail is required."},
            { "Player_Mail_Unique", "Your e-mail already exist."},
            { "Player_Password_Regex", "Your password must contains at least 1 majuscule, 1 uppercase, 1 lowercase and 1 digit and must be between 8 and 16 characters."},
            { "Player_Password_Required", "Your password is required."},
            { "Player_Pseudo_Max_Length", "Your pseudonym must contain a maximum of 20 characters."},
            { "Player_Pseudo_Min_Length", "Your pseudonym must be at least 3 characters long."},
        };

        public static string Get(string key)
        {
            return Resource.ContainsKey(key) ? Resource[key] : string.Empty;
        }
    }
}