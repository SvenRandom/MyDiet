// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MyDiet.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsLog = "IsLoggedin";
		private static readonly bool SettingsLogDefault = false;

		#endregion


		public static bool LogStateSettings
		{
			get
			{
				return (bool)AppSettings.GetValueOrDefault(SettingsLog, SettingsLogDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsLog, value);
			}
		}

		#region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string AccountSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }
	}
}