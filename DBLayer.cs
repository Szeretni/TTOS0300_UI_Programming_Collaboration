using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTOS0300_UI_Programming_Collaboration
{
    class DBLayer
    {
        public static DataTable GetPlayersFromMySQL()
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT PlayerId,PlayerName FROM Player";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        //player's for current game
        public static DataTable GetGamesPlayersFromMySQL()
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT Player.PlayerId,PlayerName FROM Player INNER JOIN GameSession_has_player ON GameSession_has_player.PlayerId = Player.PlayerId WHERE GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetCellsFromMySQL()
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    //string sql = "SELECT Cell.CellId,Name,Rent,Price,SerieId,CellTypeId,Player_has_Cell.PlayerId AS Owner FROM Cell LEFT OUTER JOIN Player_has_Cell ON Cell.CellId = Player_has_Cell.CellId WHERE GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId;
                    string sql = "SELECT CellId,Name,Rent,Price,SerieId,CellTypeId FROM Cell";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetCellOwnerFromMySQL()
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT PlayerId,CellId FROM Player_has_Cell WHERE GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId;
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetGameSessionsFromMySQL()
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT GameSessionId FROM GameSession";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetPlayerPositionFromMySQL(int playerid)
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT CellId FROM GameSession_has_player WHERE PlayerId=" + playerid.ToString() + " AND GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId .ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static void SetPlayerPositionToMySQL(int playerid,int position)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "UPDATE GameSession_has_player SET CellId = " + position.ToString() + " WHERE PlayerId = " + playerid.ToString() + " AND GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId.ToString();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader mysqldr;
                    conn.Open();
                    mysqldr = cmd.ExecuteReader();
                    while (mysqldr.Read())
                    {

                    }
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetPlayerRentPaidFromMySQL(int playerId)
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT RentPaid FROM GameSession_has_player WHERE PlayerId=" + playerId.ToString() + " AND GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static void SetRentPaidToMySQL(int playerId, bool rentPaid)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "UPDATE GameSession_has_player SET RentPaid = " + rentPaid.ToString() + " WHERE PlayerId = " + playerId.ToString() + " AND GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId.ToString();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader mysqldr;
                    conn.Open();
                    mysqldr = cmd.ExecuteReader();
                    while (mysqldr.Read())
                    {

                    }
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetPlayerCashFromMySQL(int playerId)
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT PlayerCash FROM GameSession_has_player WHERE PlayerId=" + playerId.ToString() + " AND GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static void SetPlayerCashToMySQL(int playerId, int cash)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "UPDATE GameSession_has_player SET PlayerCash = " + cash.ToString() + " WHERE PlayerId = " + playerId.ToString() + " AND GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId.ToString();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader mysqldr;
                    conn.Open();
                    mysqldr = cmd.ExecuteReader();
                    while (mysqldr.Read())
                    {

                    }
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public static void SetCurrentPlayerIdToMySQL(int playerid)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "UPDATE GameSession SET CurrentPlayerId = " + playerid.ToString() + " WHERE GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId.ToString();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader mysqldr;
                    conn.Open();
                    mysqldr = cmd.ExecuteReader();
                    while (mysqldr.Read())
                    {

                    }
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        //this is used for new games (needs new gameid)
        public static void SetCurrentPlayerIdToMySQL(int playerid, int gamesessionid)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "UPDATE GameSession SET CurrentPlayerId = " + playerid.ToString() + " WHERE GameSessionId = " + gamesessionid.ToString();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader mysqldr;
                    conn.Open();
                    mysqldr = cmd.ExecuteReader();
                    while (mysqldr.Read())
                    {

                    }
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetCurrentPlayerIdFromMySQL()
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT CurrentPlayerId FROM GameSession WHERE GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetPlayersDieRolledFlagFromMySQL(int playerid)
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT DieRolled FROM GameSession_has_player WHERE PlayerId = " + playerid.ToString() + " AND GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId.ToString();
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static void SetPlayerDieRolledFlagToMySQL(int playerid, bool dieRolled)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "UPDATE GameSession_has_player SET DieRolled = " + dieRolled.ToString() + " WHERE PlayerId = " + playerid.ToString() + " AND GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId.ToString();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader mysqldr;
                    conn.Open();
                    mysqldr = cmd.ExecuteReader();
                    while (mysqldr.Read())
                    {

                    }
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        //updates hotel and housecounts to db
        public static void SetCellBuildingCountsToMySQL(int cellid, int hotelcount, int housecount)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "UPDATE Player_has_Cell SET HotelCount = " + hotelcount.ToString() + ", HouseCount = " + housecount.ToString() + " WHERE CellId = " + cellid.ToString() + " AND GameSessionId = " + Properties.Settings.Default.settingsCurrentGameId.ToString();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader mysqldr;
                    conn.Open();
                    mysqldr = cmd.ExecuteReader();
                    while (mysqldr.Read())
                    {

                    }
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public static void SetCellOwnerToMySQL(int playerid, int cellid)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "INSERT INTO Player_has_Cell (PlayerId,CellId,GameSessionId) VALUES (" + playerid.ToString() + "," + cellid.ToString() + "," + Properties.Settings.Default.settingsCurrentGameId + ")";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader mysqldr;
                    conn.Open();
                    mysqldr = cmd.ExecuteReader();
                    while (mysqldr.Read())
                    {

                    }
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetGameIdsFromMySQL()
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT GameSessionId FROM GameSession;";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static void SetNewGameIdToMySQL(int gamesessionid, int playerid)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "INSERT INTO GameSession (GameSessionId, CurrentPlayerId) VALUES (" + gamesessionid + "," + playerid + ")";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader mysqldr;
                    conn.Open();
                    mysqldr = cmd.ExecuteReader();
                    while (mysqldr.Read())
                    {

                    }
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public static DataTable GetPlayerIdsFromMySQL()
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT PlayerId,PlayerName FROM Player";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.Fill(dt);

                    return dt;
                }
            }
            catch
            {
                throw;
            }
        }

        public static void SetPlayerToNewGameToMySQL(int gameSessionId, int playerId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "INSERT INTO GameSession_has_player (GameSessionId,PlayerId,CellId,PlayerCash,DieRolled) VALUES (" + gameSessionId + "," + playerId + ",1,1500,0)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader mysqldr;
                    conn.Open();
                    mysqldr = cmd.ExecuteReader();
                    while (mysqldr.Read())
                    {

                    }
                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        private static string GetConnectionString()
        {
            string dbIpAddress = TTOS0300_UI_Programming_Collaboration.Properties.Settings.Default.ip;
            string dbPort = TTOS0300_UI_Programming_Collaboration.Properties.Settings.Default.port;
            string dbSchema = TTOS0300_UI_Programming_Collaboration.Properties.Settings.Default.database;
            string dbPassword = TTOS0300_UI_Programming_Collaboration.Properties.Settings.Default.password;
            string dbUser = TTOS0300_UI_Programming_Collaboration.Properties.Settings.Default.user;
            return string.Format("Data source={0};Port={1};Initial catalog={2};user={3};password={4}", dbIpAddress, dbPort, dbSchema, dbUser, dbPassword);
        }
    }
}