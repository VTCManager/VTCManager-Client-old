using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    class Translation
    {
        public string login_username;
        public string version;
        public string waiting_for_ets;
        public string logout;
        public string login;
        public string ready;
        public string password;
        public string login_failed;
        public string update_part1;
        public string update_part2;
        public string update_avail_window;
        public string car_lb;
        public string truck_lb;
        public string wait_ets2_is_ready;
        public string freight_lb;
        public string depature_lb;
        public string destination_lb;
        public string no_cargo_lb;
        public string settings_lb;
        public string exit_lb;
        public string topmenuaccount_lb;
        public string not_avail;
        public string error_window;
        public string update_message;
        public string update_caption;
        public string traffic_main_lb;
        public string statistic_panel_topic;
        public string driven_tours_lb;
        public string act_bank_balance;
        public string user_company_lb;
        public string no_company_text;
        public string jb_canc_lb;
        public string save_info;
        public string discord_rpc_tra_p1;
        public string discord_rpc_tra_p2;
        public string progress;
        public string speed_setup_box;
        public string settings_window;
        public string speeding;

        public Translation(String language)
        {
            version = "Version: 1.1.0";
            if (language == "Deutsch (Deutschland)") {
                speeding = " KM/H";
                waiting_for_ets = "Warte auf ETS2/ATS...";
                logout = "Abmelden";
                login = "Anmelden";
                ready = "Bereit";
                password = "Passwort";
                login_username = "Benutzername (nur für Beta-Tester)";
                login_failed = "Benutzername oder Passwort falsch!";
                update_part1 = "Es ist ein neues Update(Version ";
                update_part2 = ") für VTCManager verfügbar! Bitte aktualisiere VTCManager.";
                update_avail_window = "Update verfügbar";
                car_lb = "Auto: ";
                truck_lb = "LKW: ";
                wait_ets2_is_ready = "Initialisierung...";
                freight_lb = "Fracht: ";
                depature_lb = "Startort: ";
                destination_lb = "Zielort: ";
                no_cargo_lb = "Freifahrt";
                settings_lb = "Einstellungen";
                exit_lb = "Beenden";
                topmenuaccount_lb = "Account";
                not_avail = "Demnächst verfügbar";
                error_window = "Fehler";
                update_message = "Änderungen in Version 1.1.0:\n" +
                    "- Credits hinzugefügt\n" +
                    "- Geschwindigkeit zwischen km/h und mph wählbar\n" +
                    "- Wir haben aufgeräumt! VTCManager verbraucht jetzt\n" +
                    "  noch weniger Ressourcen! Yeah!\n" +
                    "- Discord RPC-Unterstützung";
                update_caption = "Änderungen in Version 1.1.0\n";
                traffic_main_lb = "Verkehr";
                statistic_panel_topic = "Statistik ";
                driven_tours_lb = "gefahrene Touren: ";
                act_bank_balance = "aktueller Kontostand: ";
                user_company_lb = "angestellt bei: ";
                no_company_text = "Selbstständig";
                jb_canc_lb = "Dein letzter Auftrag wurde abgebrochen!";
                save_info = "Starte VTCManager neu, damit die Änderungen wirksam werden!";
                discord_rpc_tra_p1 = "Aktuelle Tour von ";
                discord_rpc_tra_p2 = " nach ";
                progress = "Fortschritt: ";
                speed_setup_box = "Geschwindigkeit in mph?";
                settings_window = "Einstellungen";
            } else
            {
                speeding = " mp/h";
                waiting_for_ets = "Waiting for ETS2...";
                logout = "Logout";
                login = "Login";
                ready = "Ready";
                login_username = "Username (only for beta user)";
                password = "Password";
                login_failed = "Username or password is wrong!";
                update_part1 = "An new update(version ";
                update_part2 = ") is available! Please update VTCManager.";
                update_avail_window = "Update available";
                car_lb = "Car: ";
                truck_lb = "Truck: ";
                wait_ets2_is_ready = "Initialization...";
                freight_lb = "Freight: ";
                depature_lb = "Departure: ";
                destination_lb = "Destination: ";
                no_cargo_lb = "Driving without freigt";
                settings_lb = "Settings";
                exit_lb = "Exit";
                topmenuaccount_lb = "Account";
                not_avail = "Available soon";
                error_window = "Error";
                update_message = "Changes in version 1.1.0:\n" +
"- Credits available\n" +
                    "- Speed is now selectable between km / h and mph\n" +
                    "- We cleaned up! VTCManager now uses even\n" +
                    "  fewer resources! Yeah!\n" +
                    "- using Discord RPC";
                update_caption = "Changes in version 1.1.0\n";
                traffic_main_lb = "Traffic";
                statistic_panel_topic = "Statistics ";
                driven_tours_lb = "driven tours: ";
                act_bank_balance = "bank balance: ";
                user_company_lb = "employed by: ";
                no_company_text = "self-employed";
                jb_canc_lb = "Your last job was canceled!";
                save_info = "Please restart VTCManager to save the changes!";
                discord_rpc_tra_p1 = "Current tour from ";
                discord_rpc_tra_p2 = " to ";
                progress = "Progress: ";
                speed_setup_box = "Speed in mph?";
                settings_window = "Settings";
            }
            
        }
    }
}
