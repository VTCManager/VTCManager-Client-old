using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTCManager_1._0._0
{
    [Serializable]
    public class SettingsDataObject
    {
        // Fields
        private string m_oSaveLoginData;
        private string m_oAccount;
        private string m_oPassword;

        // Properties
        public string SaveLoginData
        {
            get =>
                this.m_oSaveLoginData;
            set =>
                this.m_oSaveLoginData = value;
        }

        public string Account
        {
            get =>
                this.m_oAccount;
            set =>
                this.m_oAccount = value;
        }

        public string Password
        {
            get =>
                this.m_oPassword;
            set =>
                this.m_oPassword = value;
        }
    }



}
