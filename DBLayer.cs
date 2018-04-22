﻿using MySql.Data.MySqlClient;
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
                    //string sql = "SELECT PlayerId,PlayerName FROM Player";
                    string sql = "SELECT Player.PlayerId,PlayerName,SUM(Value), CellId FROM Player INNER JOIN Player_has_Cash ON Player.PlayerId = Player_has_Cash.PlayerId INNER JOIN Cash ON Player_has_Cash.CashId = Cash.CashId INNER JOIN GameSession_has_player ON Player.PlayerId = GameSession_has_player.PlayerId GROUP BY PlayerName";
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

        //20180422
        public static DataTable GetPlayerPositionFromMySQL(int playerid)
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "SELECT CellId FROM GameSession_has_player WHERE PlayerId=" + playerid.ToString();
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

        //20180422
        public static void SetPlayerPositionToMySQL(int playerid,int position)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    string sql = "UPDATE GameSession_has_player SET CellId = " + position.ToString() +  " WHERE PlayerId = " + playerid.ToString() + " AND GameSessionId = 1;";
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


        /* list-style. obsolete
        public static List<Player> GetPlayerListsFromMySQL()
        {
            try
            {
                //metodi palauttaa listan auto-olioita joitten tiedot haettu mysql
                List<Player> players = new List<Player>();
                //luodaan yhteys tietokantaan
                string connStr = GetConnectionString();
                string sql = "SELECT PlayerId,PlayerName FROM Player";
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Player player = new Player();
                            player.Id = rdr.GetInt16(0);
                            player.Name = rdr.GetString(1);
                            players.Add(player);
                        }
                    }
                }
                return players;
            }
            catch
            {
                throw;
            }
        }
        */

        private static string GetConnectionString()
        {
            //string dbIpAddress = TTOS0300_UI_Programming_Collaboration.Properties.Settings.Default.ip;
            //string dbPort = TTOS0300_UI_Programming_Collaboration.Properties.Settings.Default.port;
            //string dbSchema = TTOS0300_UI_Programming_Collaboration.Properties.Settings.Default.database;
            //string dbPassword = TTOS0300_UI_Programming_Collaboration.Properties.Settings.Default.password;
            //string dbUser = TTOS0300_UI_Programming_Collaboration.Properties.Settings.Default.user;
            //return string.Format("Data source={0};Port={1};Initial catalog={2};user={3};password={4}", dbIpAddress, dbPort, dbSchema, dbUser, dbPassword);
            
            string dbIpAddress = "mysql.labranet.jamk.fi";
            string dbSchema = "L2912_2";
            string dbPassword = "q4ARIboJAkdZeErWozcP13NbCCtojxx6";
            string dbUser = "L2912";
            return string.Format("Data source={0};Initial catalog={1};user={2};password={3}", dbIpAddress, dbSchema, dbUser, dbPassword);
            
        }
    }
}