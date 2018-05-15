// Helpers/Settings.cs
using System;
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


		//********************* the status of logging ***************
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


        //************************* current user email ***************
		#region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string AccountEmail
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


        //*********************** current reminder date ****************************
		#region Setting Constants

        private const string SettingsDate = "reminderDate";
        private static readonly DateTime SettingsReminderDefault = DateTime.Now;

        #endregion


		public static DateTime ReminderDate
        {
            get
            {
				return AppSettings.GetValueOrDefault(SettingsDate, SettingsReminderDefault);
            }
            set
            {
				AppSettings.AddOrUpdateValue(SettingsDate, value);
            }
        }


	}
}