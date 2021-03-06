﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using System.Text;
using Newtonsoft.Json.Linq;
//using System.IO.Compression.FileSystem;


namespace PUBG_Replay_Manager
{
    public partial class Main : Form
    {
        public string replayloc = Environment.GetEnvironmentVariable("localappdata") + "\\TslGame\\Saved\\Demos";
        public string prog_name = "PUBG Replay Manager";
        public string profile_link = "http://steamcommunity.com/profiles/";
        public string profile_id = "76561198050446061";
        public string[] info_teammate_1 = {
                    "uniqueId",
                    "playerName",
                    "teamNumber",
                    "ranking",
                    "headShots",
                    "numKills",
                    "totalGivenDamage",
                    "longestKill",
                    "MoveDistance" };
        public string[] info_teammate_2 = {
                    "uniqueId",
                    "playerName",
                    "teamNumber",
                    "ranking",
                    "headShots",
                    "numKills",
                    "totalGivenDamage",
                    "longestKill",
                    "MoveDistance" };
        public string[] info_teammate_3 = {
                    "uniqueId",
                    "playerName",
                    "teamNumber",
                    "ranking",
                    "headShots",
                    "numKills",
                    "totalGivenDamage",
                    "longestKill",
                    "MoveDistance" };
        public string[] info_teammate_4 = {
                    "uniqueId",
                    "playerName",
                    "teamNumber",
                    "ranking",
                    "headShots",
                    "numKills",
                    "totalGivenDamage",
                    "longestKill",
                    "MoveDistance" };
        public Main()
        {
            InitializeComponent();
            RefreshReplayList();
        }
        static public int get_char_unicode_code(char character)
        {
            UTF32Encoding encoding = new UTF32Encoding();
            byte[] bytes = encoding.GetBytes(character.ToString().ToCharArray());
            return BitConverter.ToInt32(bytes, 0);
        }
        static string CaesarCipher(string value, int shift)
        {
            char[] encodedCharArray = value.ToCharArray();
            string decodedString = "";
            int decode = 0;
            foreach (char item in encodedCharArray)
            {
                if (get_char_unicode_code(item) > 0)
                {
                    decode = get_char_unicode_code(item) + shift;
                    decodedString = decodedString + Convert.ToChar(decode);
                    //Console.WriteLine("Orginal: " + item + " New: " + Convert.ToChar(decode));
                }
            }
            return decodedString;
        }
        public void RefreshReplayList()
        {
            replayList.Items.Clear();
            if (Directory.Exists(replayloc))
            {
                foreach (string replay in Directory.GetDirectories(replayloc))
                {
                    if(replay.Contains("match."))
                    {
                        replayList.Items.Add(replay.Replace(replayloc + "\\", ""));
                    }
                }
            }
            replayList.Refresh();
        }
        public void RefreshInfoGroups(string[] newInfo)
        {
            lengthInMins.Text = newInfo[0];
            networkVerison.Text = newInfo[1];
            matchType.Text = newInfo[4];
            gameVerison.Text = newInfo[5];
            serverRegion.Text = newInfo[6];
            recordingSize.Text = newInfo[10];
            timeRecorded.Text = newInfo[12];
            if (newInfo[13] == "false")
            {
                isLive.Checked = false;
            }
            if (newInfo[13] == "true")
            {
                isLive.Checked = true;
            }
            if (newInfo[14] == "false")
            {
                isIncomplete.Checked = false;
            }
            if (newInfo[14] == "true")
            {
                isIncomplete.Checked = true;
            }
            if (newInfo[15] == "false")
            {
                IsServerRecording.Checked = false;
            }
            if (newInfo[15] == "true")
            {
                IsServerRecording.Checked = true;
            }
            if (newInfo[16] == "false")
            {
                fileLocked.Checked = false;
            }
            if (newInfo[16] == "true")
            {
                fileLocked.Checked = true;
            }
            teamInfo.Text = newInfo[17];
            if (newInfo[18].Contains("765611"))
            {
                profile_id = newInfo[18];
            }
            recordingUser.Text = newInfo[19];
            profileLink.Text = newInfo[19];
            mapName.Text = newInfo[20];
            //if (newInfo[20] == "Desert_Main")
            //{
            //    mapImage.Image = Properties.Resources.miramar;
            //    mapImage.Load();
            //    mapImage.Visible = true;
            //}
            //if (newInfo[20] == "Erangel_Main")
            //{
            //    mapImage.Image = Properties.Resources.erangel;
            //    mapImage.Load();
            //    mapImage.Visible = true;
            //}

            fileSize.Text = newInfo[21];
            serverId.Text = newInfo[22];
            if (newInfo[23] == "true")
            {
                diedorwon.Checked = false;
            }
            weatherType.Text = newInfo[24];
            totalPlayers.Text = newInfo[25];
            totalTeams.Text = newInfo[26];
            rankNum.Text = newInfo[27];
            if (newInfo[19] == info_teammate_1[1])
            {
                headShots.Text = info_teammate_1[4];
                kills.Text = info_teammate_1[5];
                dmgHandedOut.Text = info_teammate_1[6];
                longestKill.Text = info_teammate_1[7];
                distanceWalked.Text = info_teammate_1[8];
            }
            if (newInfo[19] == info_teammate_2[1])
            {
                headShots.Text = info_teammate_2[4];
                kills.Text = info_teammate_2[5];
                dmgHandedOut.Text = info_teammate_2[6];
                longestKill.Text = info_teammate_2[7];
                distanceWalked.Text = info_teammate_2[8];
            }
            if (newInfo[19] == info_teammate_3[1])
            {
                headShots.Text = info_teammate_3[4];
                kills.Text = info_teammate_3[5];
                dmgHandedOut.Text = info_teammate_3[6];
                longestKill.Text = info_teammate_3[7];
                distanceWalked.Text = info_teammate_3[8];
            }
            if (newInfo[19] == info_teammate_4[1])
            {
                headShots.Text = info_teammate_4[4];
                kills.Text = info_teammate_4[5];
                dmgHandedOut.Text = info_teammate_4[6];
                longestKill.Text = info_teammate_4[7];
                distanceWalked.Text = info_teammate_4[8];
            }

            //-----------------------------------------------------------------------
            //---------------------TeamMates-----------------------------------------
            //-----------------------------------------------------------------------
            if((info_teammate_1[0] == "uniqueId") || (info_teammate_1[0] == "[unknown]"))
            {
                tm1_pubgname.Text = "[unknown]";
                tm1_steamid.Text = "[unknown]";
                tm1_headshots.Text = "[unknown]";
                tm1_kills.Text = "[unknown]";
            }                                        
            if ((info_teammate_2[0] == "uniqueId") || (info_teammate_2[0] == "[unknown]"))
            {                                       
                tm2_pubgname.Text = "[unknown]";    
                tm2_steamid.Text = "[unknown]";     
                tm2_headshots.Text = "[unknown]";   
                tm2_kills.Text = "[unknown]";       
            }                                       
            if ((info_teammate_3[0] == "uniqueId") || (info_teammate_3[0] == "[unknown]"))
            {                                      
                tm3_pubgname.Text = "[unknown]";   
                tm3_steamid.Text = "[unknown]";    
                tm3_headshots.Text = "[unknown]";  
                tm3_kills.Text = "[unknown]";      
            }
            if ((info_teammate_4[0] == "uniqueId") || (info_teammate_4[0] == "[unknown]"))
            {     
                tm4_pubgname.Text = "[unknown]";
                tm4_steamid.Text = "[unknown]";
                tm4_headshots.Text = "[unknown]";
                tm4_kills.Text = "[unknown]";
            }     
            if (info_teammate_1[0] != "uniqueId")
            {
                tm1_pubgname.Text = info_teammate_1[1];
                tm1_steamid.Text = info_teammate_1[0];
                tm1_headshots.Text = info_teammate_1[4];
                tm1_kills.Text = info_teammate_1[5];
            }
            if (info_teammate_2[0] != "uniqueId")
            {
                tm2_pubgname.Text = info_teammate_2[1];
                tm2_steamid.Text = info_teammate_2[0];
                tm2_headshots.Text = info_teammate_2[4];
                tm2_kills.Text = info_teammate_2[5];
            }
            if (info_teammate_3[0] != "uniqueId")
            {
                tm3_pubgname.Text = info_teammate_3[1];
                tm3_steamid.Text = info_teammate_3[0];
                tm3_headshots.Text = info_teammate_3[4];
                tm3_kills.Text = info_teammate_3[5];
            }
            if (info_teammate_4[0] != "uniqueId")
            {
                tm4_pubgname.Text = info_teammate_4[1];
                tm4_steamid.Text = info_teammate_4[0];
                tm4_headshots.Text = info_teammate_4[4];
                tm4_kills.Text = info_teammate_4[5];
            }

        }

        private void replayList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(replayloc+ "\\" + replayList.SelectedItem))
            {
                RefreshInfoGroups(ReadReplayInfo(replayloc + "\\" + replayList.SelectedItem));
                openSelectedReplay.Enabled = true;
                zipReplay.Enabled = true;
                steamidStrip.Enabled = true;
            }
            else
            {
                RefreshReplayList();
                openSelectedReplay.Enabled = false;
                zipReplay.Enabled = false;
                steamidStrip.Enabled = false;
            }
        }

        private void openReplayFolder_Click(object sender, EventArgs e)
        {
            Process.Start(replayloc+"\\");
        }

        private void openSelectedReplay_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(replayloc + "\\" + replayList.SelectedItem))
            {
                Process.Start(replayloc + "\\" + replayList.SelectedItem);
            }
            else
            {
                RefreshReplayList();
                openSelectedReplay.Enabled = false;
                zipReplay.Enabled = false;
                steamidStrip.Enabled = false;
            }
        }
        private string[] ReadReplayInfo(string directory_of_recording)
        {
            string[] placeholder = { "0", "0", "0", "0", "[unknown]", "[unknown]", "[unknown]", "[unknown]", "[unknown]", "[unknown]", "0", "0", "[unknown]", "false", "true", "false", "false", "[unknown]", "[unknown]", "[unknown]", "[unknown]", "[unknown]", "[unknown]" };
            if (File.Exists(directory_of_recording + "\\PUBG.replayinfo"))
            {
                string[] ReplayInfoFile = File.ReadAllLines(directory_of_recording + "\\PUBG.replayinfo");
                Console.WriteLine("==============================Reading Replay Info===================================");
                int i = 0;
                foreach (string line in ReplayInfoFile)
                {
                    Console.WriteLine("Line " + i + ": "+line);
                }
                Console.WriteLine("===========================Finished Reading Replay Info==============================");
                string[] info = {
                    "LengthInMS", //Length of the recording in miliseconds
                    "NetworkVersion", //??????
                    "Changelist", //??????
                    "FriendlyName", //String with lots of match info
                    "MatchType",//(Derived from FriendlyName) offical or ??????? (possibly custom)
                    "UpdateType",//(Derived from FriendlyName) 2018-01, 2017-pre6, 2017-test
                    "ServerLocation",//(Derived from FriendlyName) NA or ?????????
                    "TeamType",//(Derived from FriendlyName) unused in favor of mode, squad or solo or ????
                    "DateRecorded",//(Derived from FriendlyName) unused in favor of TimeStamp
                    "UUID",//(Derived from FriendlyName) Some kind of uuid? maybe for looking up matchs? Unsure.
                    "DemoFileLastOffset", //[more likely this one]File size of the recording (in bytes) or time between recordings
                    "SizeInBytes",//?????? its always 0
                    "TimeStamp",//Unix Time (in Miliseconds)
                    "bIsLive",//LiveStreaming detection?
                    "bIncomplete",//Detection for corrupt files?
                    "bIsServerRecording", //Heavy possibly the server also records a copy of the entire match
                    "bShouldKeep",//True = Pubg won't delete the file for a new one and it prevents the user from deleting it to
                    "Mode",//Solo or Sqaud (16)
                    "RecordUserId", //The recording user (SteamID 64)
                    "RecordUserNickname", //The recording user (In-Game Username)
                    "MapName", //name of the map (Desert_Main or Erangel_Main)
                    "FolderSize", //Size of the Replay Folder
                    "ServerID", //the server id 
                    "AllDeadOrWin", //Some kind of check to see of all of the sqaud died or you won (should techinally always be true) (Added in the Second 1.0 Update)
                    "Weather", //Stripped from the replaysummary files
                    "TotalPlayers", //Stripped from the replaysummary files - Total players in a match
                    "TotalTeams",//Stripped from the replaysummary files - Total teams in a match
                    "numberofTeammates",//Stripped from the replaysummary files - teammates in a match
                    "rank",//Stripped from the replaysummary files - Rank in a match (27)
                    "replayType",
                };
                if (ReplayInfoFile[0] == "")//Some dumb f**king bulls**t playerunkown did, half the replay files are like this
                {
                    info[28] = "Second 1.0 Update";
                    //--------------------------------------------------
                    //-------------------Length in Ms ------------------
                    //--------------------------------------------------
                    string lengthflag = ReplayInfoFile[2].Remove(0, 15);
                    lengthflag = lengthflag.Remove(lengthflag.Length - 1, 1);
                    int length = 0;
                    if (!int.TryParse(lengthflag, out length))
                    {
                        MessageBox.Show("Unable to parse LengthInMS!" + Environment.NewLine + prog_name + " will now exit!", "Serious Error!");
                        Environment.Exit(-1);
                    }
                    if (length < 60000)
                    {
                        length /= 1000; //miliseconds to seconds
                        info[0] = length.ToString() + " Secs";
                    }
                    else
                    {
                        length /= 60000; //miliseconds to mintues
                        info[0] = length.ToString() + " Mins";
                    }
                    //--------------------------------------------------
                    //-------------------Network Version----------------
                    //--------------------------------------------------
                    string networkverflag = ReplayInfoFile[3].Remove(0, 19);
                    networkverflag = networkverflag.Remove(networkverflag.Length - 1, 1);
                    info[1] = networkverflag;
                    //--------------------------------------------------
                    //-------------------Changelist---------------------
                    //--------------------------------------------------
                    string changelistflag = ReplayInfoFile[4].Remove(0, 15);
                    changelistflag = changelistflag.Remove(changelistflag.Length - 1, 1);
                    info[2] = changelistflag;
                    //--------------------------------------------------
                    //-------------------FriendlyName-------------------
                    //--------------------------------------------------
                    string FriendlyNameflag = ReplayInfoFile[5].Remove(0, 18);
                    FriendlyNameflag = FriendlyNameflag.Remove(FriendlyNameflag.Length - 2, 2);
                    string[] temp = FriendlyNameflag.Split('.');
                    info[3] = FriendlyNameflag; //match.bro.official.2017 - test.na.squad.2017.12.05.470ec7f1 - c342 - 46ad - 81c9 - e9117f27b44a
                    if (temp[2] == "official")
                    {
                        info[4] = "Official";//official
                    }
                    if (temp[2] == "custom")
                    {
                        info[4] = "Custom";//official
                    }
                    info[5] = temp[3]; //2017-test
                    info[6] = temp[4].ToUpper(); //na
                    info[7] = temp[5]; //sqaud
                    info[8] = temp[6] + "-" + temp[7] + "-" + temp[8]; //2017.12.05
                    info[9] = temp[9]; //470ec7f1-c342-46ad-81c9-e9117f27b44a
                    info[22] = temp[9].Remove(0, 30); //27b44a

                    //--------------------------------------------------
                    //-------------------DemoFileLastOffset-------------
                    //--------------------------------------------------
                    string DemoFileLastOffsetflag = ReplayInfoFile[6].Remove(0, 23);
                    DemoFileLastOffsetflag = DemoFileLastOffsetflag.Remove(DemoFileLastOffsetflag.Length - 1, 1);
                    int size = 0;
                    if (!int.TryParse(DemoFileLastOffsetflag, out size))
                    {
                        MessageBox.Show("Unable to parse DemoFileLastOffset!" + Environment.NewLine + prog_name + " will now exit!", "Serious Error!");
                        Environment.Exit(-1);
                    }
                    if (size > 1000000)
                    {
                        size /= 1000000;
                        info[10] = size.ToString() + " MB";
                    }
                    else
                    {
                        info[10] = size.ToString() + " Bytes";
                    }
                    //--------------------------------------------------
                    //-------------------SizeInBytes--------------------
                    //--------------------------------------------------
                    string SizeInBytesflag = ReplayInfoFile[7].Remove(0, 16);
                    SizeInBytesflag = SizeInBytesflag.Remove(SizeInBytesflag.Length - 1, 1);
                    info[11] = SizeInBytesflag;
                    //--------------------------------------------------
                    //-------------------TimeStamp----------------------
                    //--------------------------------------------------
                    string timecreateflag = ReplayInfoFile[8].Remove(0, 14);
                    timecreateflag = timecreateflag.Remove(timecreateflag.Length - 1, 1);
                    double time = 0;
                    if (!double.TryParse(timecreateflag, out time))
                    {
                        MessageBox.Show("Unable to parse TimeStamp!" + Environment.NewLine + prog_name + " will now exit!", "Serious Error!");
                        Environment.Exit(-1);
                    }
                    double timestamp = time;
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dateTime = dateTime.AddMilliseconds(timestamp).ToLocalTime();
                    info[12] = dateTime.ToString();
                    //--------------------------------------------------
                    //-------------------isLive-------------------------
                    //--------------------------------------------------
                    string isliveflag = ReplayInfoFile[9].Remove(0, 12);
                    isliveflag = isliveflag.Remove(isliveflag.Length - 1, 1);
                    info[13] = isliveflag;
                    //--------------------------------------------------
                    //-------------------Incomplete---------------------
                    //--------------------------------------------------
                    string incompleteflag = ReplayInfoFile[10].Remove(0, 16);
                    incompleteflag = incompleteflag.Remove(incompleteflag.Length - 1, 1);
                    info[14] = incompleteflag;
                    //--------------------------------------------------
                    //-------------------InServerRecording--------------
                    //--------------------------------------------------
                    string InServerRecordingflag = ReplayInfoFile[11].Remove(0, 23);
                    InServerRecordingflag = InServerRecordingflag.Remove(InServerRecordingflag.Length - 1, 1);
                    info[15] = InServerRecordingflag;
                    //--------------------------------------------------
                    //--------------------Keep--------------------------
                    //--------------------------------------------------
                    string keepflag = ReplayInfoFile[12].Remove(0, 16);
                    keepflag = keepflag.Remove(keepflag.Length - 1, 1);
                    info[16] = keepflag;
                    //--------------------------------------------------
                    //--------------------Mode--------------------------
                    //--------------------------------------------------
                    string modeflag = ReplayInfoFile[13].Remove(ReplayInfoFile[13].Length - 2, 2).Remove(0, 10);
                    if (modeflag == "solo")
                    {
                        info[17] = "Solo";
                    }
                    else if (modeflag == "solo-fpp")
                    {
                        info[17] = "Solo (First person)";
                    }
                    else if (modeflag == "duo")
                    {
                        info[17] = "Duo";
                    }
                    else if (modeflag == "duo-fpp")
                    {
                        info[17] = "Duo (First person)";
                    }
                    else if (modeflag == "squad")
                    {
                        info[17] = "Squad";
                    }
                    else if (modeflag == "squad-fpp")
                    {
                        info[17] = "Squad (First person)";
                    }
                    else
                    {
                        info[17] = "Unknown";
                    }
                    //--------------------------------------------------
                    //--------------------RecordUserId------------------
                    //--------------------------------------------------
                    string RecordUserIDflag = ReplayInfoFile[14].Remove(0, 18);
                    RecordUserIDflag = RecordUserIDflag.Remove(RecordUserIDflag.Length - 2, 2);
                    if (RecordUserIDflag.Contains("765611"))
                    {
                        info[18] = RecordUserIDflag;
                    }
                    else
                    {
                        info[18] = "[User ID is not supported in this replay]";
                    }
                    //--------------------------------------------------
                    //--------------------RecordUserNickName------------------
                    //--------------------------------------------------
                    string RecordUserNickNameflag = ReplayInfoFile[15].Remove(0, 24);
                    RecordUserNickNameflag = RecordUserNickNameflag.Remove(RecordUserNickNameflag.Length - 2, 2);
                    info[19] = RecordUserNickNameflag;
                    //--------------------------------------------------
                    //--------------------MapName------------------
                    //--------------------------------------------------
                    string MapNameflag = ReplayInfoFile[16].Remove(ReplayInfoFile[16].Length - 1, 1).Remove(0, 13);
                    if (MapNameflag == "Erangel_Main")
                    {
                        info[20] = "Erangel";
                    }
                    else
                    {
                        info[20] = "Miramar";
                    }
                    //--------------------------------------------------
                    //--------------------FolderSize--------------------
                    //--------------------------------------------------
                    double dirsize = GetDirectorySize(replayloc + "\\" + replayList.SelectedItem + "\\");
                    if (dirsize < 1048576)
                    {
                        info[21] = Math.Truncate(dirsize).ToString() + " Bytes";
                    }
                    else
                    {
                        dirsize /= 1048576;
                        info[21] = Math.Truncate(dirsize).ToString() + " MB";
                    }
                    //--------------------------------------------------
                    //--------------------AllDeadOrWin------------------
                    //--------------------------------------------------
                    if (ReplayInfoFile.Length >= 18)
                    {
                        string AllDeadOrWin = ReplayInfoFile[17].Remove(0, 18);
                        info[23] = AllDeadOrWin;
                    }
                    else
                    {
                        info[23] = "false";
                    }
                }
                else
                {
                    info[28] = "Pre Second 1.0 Update";
                    //--------------------------------------------------
                    //-------------------Length in Ms ------------------
                    //--------------------------------------------------
                    string lengthflag = ReplayInfoFile[1].Remove(0, 15);
                    lengthflag = lengthflag.Remove(lengthflag.Length - 1, 1);
                    int length = 0;
                    if (!int.TryParse(lengthflag, out length))
                    {
                        MessageBox.Show("Unable to parse LengthInMS!" + Environment.NewLine + prog_name + " will now exit!", "Serious Error!");
                        Environment.Exit(-1);
                    }
                    if (length < 60000)
                    {
                        length /= 1000; //miliseconds to seconds
                        info[0] = length.ToString() + " Secs";
                    }
                    else
                    {
                        length /= 60000; //miliseconds to mintues
                        info[0] = length.ToString() + " Mins";
                    }
                    //--------------------------------------------------
                    //-------------------Network Version----------------
                    //--------------------------------------------------
                    string networkverflag = ReplayInfoFile[2].Remove(0, 19);
                    networkverflag = networkverflag.Remove(networkverflag.Length - 1, 1);
                    info[1] = networkverflag;
                    //--------------------------------------------------
                    //-------------------Changelist---------------------
                    //--------------------------------------------------
                    string changelistflag = ReplayInfoFile[3].Remove(0, 15);
                    changelistflag = changelistflag.Remove(changelistflag.Length - 1, 1);
                    info[2] = changelistflag;
                    //--------------------------------------------------
                    //-------------------FriendlyName-------------------
                    //--------------------------------------------------
                    string FriendlyNameflag = ReplayInfoFile[4].Remove(0, 18);
                    FriendlyNameflag = FriendlyNameflag.Remove(FriendlyNameflag.Length - 2, 2);
                    string[] temp = FriendlyNameflag.Split('.');
                    info[3] = FriendlyNameflag; //match.bro.official.2017 - test.na.squad.2017.12.05.470ec7f1 - c342 - 46ad - 81c9 - e9117f27b44a
                    if (temp[2] == "official")
                    {
                        info[4] = "Official";//official
                    }
                    if (temp[2] == "custom")
                    {
                        info[4] = "Custom";//official
                    }
                    info[5] = temp[3]; //2017-test
                    info[6] = temp[4].ToUpper(); //na
                    info[7] = temp[5]; //sqaud
                    info[8] = temp[6] + "-" + temp[7] + "-" + temp[8]; //2017.12.05
                    info[9] = temp[9]; //470ec7f1-c342-46ad-81c9-e9117f27b44a
                    info[22] = temp[9].Remove(0, 30); //27b44a

                    //--------------------------------------------------
                    //-------------------DemoFileLastOffset-------------
                    //--------------------------------------------------
                    string DemoFileLastOffsetflag = ReplayInfoFile[5].Remove(0, 23);
                    DemoFileLastOffsetflag = DemoFileLastOffsetflag.Remove(DemoFileLastOffsetflag.Length - 1, 1);
                    int size = 0;
                    if (!int.TryParse(DemoFileLastOffsetflag, out size))
                    {
                        MessageBox.Show("Unable to parse DemoFileLastOffset!" + Environment.NewLine + prog_name + " will now exit!", "Serious Error!");
                        Environment.Exit(-1);
                    }
                    if (size > 1000000)
                    {
                        size /= 1000000;
                        info[10] = size.ToString() + " MB";
                    }
                    else
                    {
                        info[10] = size.ToString() + " Bytes";
                    }
                    //--------------------------------------------------
                    //-------------------SizeInBytes--------------------
                    //--------------------------------------------------
                    string SizeInBytesflag = ReplayInfoFile[6].Remove(0, 16);
                    SizeInBytesflag = SizeInBytesflag.Remove(SizeInBytesflag.Length - 1, 1);
                    info[11] = SizeInBytesflag;
                    //--------------------------------------------------
                    //-------------------TimeStamp----------------------
                    //--------------------------------------------------
                    string timecreateflag = ReplayInfoFile[7].Remove(0, 14);
                    timecreateflag = timecreateflag.Remove(timecreateflag.Length - 1, 1);
                    double time = 0;
                    if (!double.TryParse(timecreateflag, out time))
                    {
                        MessageBox.Show("Unable to parse TimeStamp!" + Environment.NewLine + prog_name + " will now exit!", "Serious Error!");
                        Environment.Exit(-1);
                    }
                    double timestamp = time;
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dateTime = dateTime.AddMilliseconds(timestamp).ToLocalTime();
                    info[12] = dateTime.ToString();
                    //--------------------------------------------------
                    //-------------------isLive-------------------------
                    //--------------------------------------------------
                    string isliveflag = ReplayInfoFile[8].Remove(0, 12);
                    isliveflag = isliveflag.Remove(isliveflag.Length - 1, 1);
                    info[13] = isliveflag;
                    //--------------------------------------------------
                    //-------------------Incomplete---------------------
                    //--------------------------------------------------
                    string incompleteflag = ReplayInfoFile[9].Remove(0, 16);
                    incompleteflag = incompleteflag.Remove(incompleteflag.Length - 1, 1);
                    info[14] = incompleteflag;
                    //--------------------------------------------------
                    //-------------------InServerRecording--------------
                    //--------------------------------------------------
                    string InServerRecordingflag = ReplayInfoFile[10].Remove(0, 23);
                    InServerRecordingflag = InServerRecordingflag.Remove(InServerRecordingflag.Length - 1, 1);
                    info[15] = InServerRecordingflag;
                    //--------------------------------------------------
                    //--------------------Keep--------------------------
                    //--------------------------------------------------
                    string keepflag = ReplayInfoFile[11].Remove(0, 16);
                    keepflag = keepflag.Remove(keepflag.Length - 1, 1);
                    info[16] = keepflag;
                    //--------------------------------------------------
                    //--------------------Mode--------------------------
                    //--------------------------------------------------
                    string modeflag = ReplayInfoFile[12].Remove(ReplayInfoFile[12].Length - 2, 2).Remove(0, 10);
                    if (modeflag == "solo")
                    {
                        info[17] = "Solo";
                    }
                    else if (modeflag == "solo-fpp")
                    {
                        info[17] = "Solo (First person)";
                    }
                    else if (modeflag == "duo")
                    {
                        info[17] = "Duo";
                    }
                    else if (modeflag == "duo-fpp")
                    {
                        info[17] = "Duo (First person)";
                    }
                    else if (modeflag == "squad")
                    {
                        info[17] = "Squad";
                    }
                    else if (modeflag == "squad-fpp")
                    {
                        info[17] = "Squad (First person)";
                    }
                    else
                    {
                        info[17] = "Unknown";
                    }
                    //--------------------------------------------------
                    //--------------------RecordUserId------------------
                    //--------------------------------------------------
                    string RecordUserIDflag = ReplayInfoFile[13].Remove(0, 18);
                    RecordUserIDflag = RecordUserIDflag.Remove(RecordUserIDflag.Length - 2, 2);
                    if (RecordUserIDflag.Contains("765611"))
                    {
                        info[18] = RecordUserIDflag;
                    }
                    else
                    {
                        info[18] = "[User ID is not supported in this replay]";
                    }
                    //--------------------------------------------------
                    //--------------------RecordUserNickName------------------
                    //--------------------------------------------------
                    string RecordUserNickNameflag = ReplayInfoFile[14].Remove(0, 24);
                    RecordUserNickNameflag = RecordUserNickNameflag.Remove(RecordUserNickNameflag.Length - 2, 2);
                    info[19] = RecordUserNickNameflag;
                    //--------------------------------------------------
                    //--------------------MapName------------------
                    //--------------------------------------------------
                    string MapNameflag = ReplayInfoFile[15].Remove(ReplayInfoFile[15].Length - 1, 1).Remove(0, 13);
                    MapNameflag = MapNameflag.Remove(MapNameflag.Length - 1, 1);
                    if (MapNameflag == "Erangel_Main")
                    {
                        info[20] = "Erangel";
                    }
                    else
                    {
                        info[20] = "Miramar";
                    }
                    //--------------------------------------------------
                    //--------------------FolderSize--------------------
                    //--------------------------------------------------
                    double dirsize = GetDirectorySize(replayloc + "\\" + replayList.SelectedItem + "\\");
                    if (dirsize < 1048576)
                    {
                        info[21] = Math.Truncate(dirsize).ToString() + " Bytes";
                    }
                    else
                    {
                        dirsize /= 1048576;
                        info[21] = Math.Truncate(dirsize).ToString() + " MB";
                    }
                    //--------------------------------------------------
                    //--------------------AllDeadOrWin------------------
                    //--------------------------------------------------
                    if (ReplayInfoFile.Length >= 18)
                    {
                        string AllDeadOrWin = ReplayInfoFile[16].Remove(0, 18);
                        info[23] = AllDeadOrWin;
                    }
                    else
                    {
                        info[23] = "false";
                    }

                }
                List<string> replaySummaryList = new List<string>();
                foreach (string replay in Directory.GetFiles(directory_of_recording + "\\data"))
                {
                    if (replay.Contains("ReplaySummary"))
                    {
                        replaySummaryList.Add(replay);
                    }
                }
                replaySummaryList.Reverse();
                JObject testJSON = JObject.Parse(CaesarCipher(File.ReadAllLines(replaySummaryList.ToArray()[0])[0], +1).Remove(0, 2));
                //--------------------------------------------------
                //--------------------Weather------------------------
                //--------------------------------------------------
                if (testJSON["weatherName"].ToString() == "Weather_Clear_02")
                {
                    info[24] = "Sunny Clear";
                }
                else if(testJSON["weatherName"].ToString() == "Weather_Desert_Sunrise")
                {
                    info[24] = "Sunrise";
                }
                else if(testJSON["weatherName"].ToString() == "Weather_Desert_Clear")
                {
                    info[24] = "Sunny";
                }
                else if (testJSON["weatherName"].ToString() == "Weather_Clear")
                {
                    info[24] = "Sunny";
                }
                else if (testJSON["weatherName"].ToString() == "Weather_Dark")
                {
                    info[24] = "Sunset";
                }
                else
                {
                    info[24] = testJSON["weatherName"].ToString();
                }
                //--------------------------------------------------
                //--------------------Number of players------------------------
                //--------------------------------------------------
                info[25] = testJSON["numPlayers"].ToString();
                //--------------------------------------------------
                //--------------------Number of teams------------------------
                //--------------------------------------------------
                info[26] = testJSON["numTeams"].ToString();
                info_teammate_1[0] = testJSON["playerStateSummaries"][0]["uniqueId"].ToString();
                info_teammate_1[1] = testJSON["playerStateSummaries"][0]["playerName"].ToString();
                info_teammate_1[2] = testJSON["playerStateSummaries"][0]["teamNumber"].ToString();
                info_teammate_1[3] = testJSON["playerStateSummaries"][0]["ranking"].ToString();
                info[27] = testJSON["playerStateSummaries"][0]["ranking"].ToString();
                info_teammate_1[4] = testJSON["playerStateSummaries"][0]["headShots"].ToString();
                info_teammate_1[5] = testJSON["playerStateSummaries"][0]["numKills"].ToString();
                info_teammate_1[6] = testJSON["playerStateSummaries"][0]["totalGivenDamages"].ToString();
                info_teammate_1[7] = testJSON["playerStateSummaries"][0]["longestDistanceKill"].ToString();
                info_teammate_1[8] = testJSON["playerStateSummaries"][0]["totalMovedDistanceMeter"].ToString();
                try
                {
                    info_teammate_2[0] = testJSON["playerStateSummaries"][1]["uniqueId"].ToString();
                    info_teammate_2[1] = testJSON["playerStateSummaries"][1]["playerName"].ToString();
                    info_teammate_2[2] = testJSON["playerStateSummaries"][1]["teamNumber"].ToString();
                    info_teammate_2[3] = testJSON["playerStateSummaries"][1]["ranking"].ToString();
                    info_teammate_2[4] = testJSON["playerStateSummaries"][1]["headShots"].ToString();
                    info_teammate_2[5] = testJSON["playerStateSummaries"][1]["numKills"].ToString();
                    info_teammate_2[6] = testJSON["playerStateSummaries"][1]["totalGivenDamages"].ToString();
                    info_teammate_2[7] = testJSON["playerStateSummaries"][1]["longestDistanceKill"].ToString();
                    info_teammate_2[8] = testJSON["playerStateSummaries"][1]["totalMovedDistanceMeter"].ToString();
                }
                catch (ArgumentOutOfRangeException)
                {
                    info_teammate_2[0] = "[unknown]";
                    info_teammate_2[1] = "[unknown]";
                    info_teammate_2[2] = "[unknown]";
                    info_teammate_2[3] = "[unknown]";
                    info_teammate_2[4] = "[unknown]";
                    info_teammate_2[5] = "[unknown]";
                    info_teammate_2[6] = "[unknown]";
                    info_teammate_2[7] = "[unknown]";
                    info_teammate_2[8] = "[unknown]";
                    foreach (string obj in info)
                    {
                        Console.WriteLine(obj);
                    }
                    return info;
                }
                try
                {
                    info_teammate_3[0] = testJSON["playerStateSummaries"][2]["uniqueId"].ToString();
                    info_teammate_3[1] = testJSON["playerStateSummaries"][2]["playerName"].ToString();
                    info_teammate_3[2] = testJSON["playerStateSummaries"][2]["teamNumber"].ToString();
                    info_teammate_3[3] = testJSON["playerStateSummaries"][2]["ranking"].ToString();
                    info_teammate_3[4] = testJSON["playerStateSummaries"][2]["headShots"].ToString();
                    info_teammate_3[5] = testJSON["playerStateSummaries"][2]["numKills"].ToString();
                    info_teammate_3[6] = testJSON["playerStateSummaries"][2]["totalGivenDamages"].ToString();
                    info_teammate_3[7] = testJSON["playerStateSummaries"][2]["longestDistanceKill"].ToString();
                    info_teammate_3[8] = testJSON["playerStateSummaries"][2]["totalMovedDistanceMeter"].ToString();
                }
                catch (ArgumentOutOfRangeException)
                {
                    info_teammate_3[0] = "[unknown]";
                    info_teammate_3[1] = "[unknown]";
                    info_teammate_3[2] = "[unknown]";
                    info_teammate_3[3] = "[unknown]";
                    info_teammate_3[4] = "[unknown]";
                    info_teammate_3[5] = "[unknown]";
                    info_teammate_3[6] = "[unknown]";
                    info_teammate_3[7] = "[unknown]";
                    info_teammate_3[8] = "[unknown]";
                    foreach (string obj in info)
                    {
                        Console.WriteLine(obj);
                    }
                    return info;
                }
                try
                {
                    info_teammate_4[0] = testJSON["playerStateSummaries"][3]["uniqueId"].ToString();
                    info_teammate_4[1] = testJSON["playerStateSummaries"][3]["playerName"].ToString();
                    info_teammate_4[2] = testJSON["playerStateSummaries"][3]["teamNumber"].ToString();
                    info_teammate_4[3] = testJSON["playerStateSummaries"][3]["ranking"].ToString();
                    info_teammate_4[4] = testJSON["playerStateSummaries"][3]["headShots"].ToString();
                    info_teammate_4[5] = testJSON["playerStateSummaries"][3]["numKills"].ToString();
                    info_teammate_4[6] = testJSON["playerStateSummaries"][3]["totalGivenDamages"].ToString();
                    info_teammate_4[7] = testJSON["playerStateSummaries"][3]["longestDistanceKill"].ToString();
                    info_teammate_4[8] = testJSON["playerStateSummaries"][3]["totalMovedDistanceMeter"].ToString();
                }
                catch (ArgumentOutOfRangeException)
                {
                    info_teammate_4[0] = "[unknown]";
                    info_teammate_4[1] = "[unknown]";
                    info_teammate_4[2] = "[unknown]";
                    info_teammate_4[3] = "[unknown]";
                    info_teammate_4[4] = "[unknown]";
                    info_teammate_4[5] = "[unknown]";
                    info_teammate_4[6] = "[unknown]";
                    info_teammate_4[7] = "[unknown]";
                    info_teammate_4[8] = "[unknown]";
                    foreach (string obj in info)
                    {
                        Console.WriteLine(obj);
                    }
                    return info;
                }
                foreach (string obj in info)
                {
                    Console.WriteLine(obj);
                }
                return info;
            }
            MessageBox.Show("PUBG.replayinfo for this recording was not found!" + Environment.NewLine + "Recording may be corrupt!", "Waring!");

            return placeholder;
        }

        private void zipReplay_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2.  
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "ZIP File|*.zip";
            saveFileDialog1.Title = "Save a PUBG Replay as a ZIP File...";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                string startPath = replayloc + "\\" + replayList.SelectedItem + "\\";//folder to add
                string zipPath = saveFileDialog1.FileName;//URL for your ZIP file
                ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);
            }
        }
        private double GetDirectorySize(string directory)
        {
            double foldersize = 0;
            foreach (string dir in Directory.GetDirectories(directory))
            {
                GetDirectorySize(dir);
            }

            foreach (FileInfo file in new DirectoryInfo(directory).GetFiles())
            {
                foldersize += file.Length;
            }

            return foldersize;
        }

        private void steamidStrip_Click(object sender, EventArgs e)
        {
            SteamID steamid = new SteamID(replayloc + "\\" + replayList.SelectedItem + "\\");
            steamid.ShowDialog();
        }

        private void importReplay_Click(object sender, EventArgs e)
        {
            OpenFileDialog importReplayDialog = new OpenFileDialog();
            importReplayDialog.Filter = "ZIP Files|*.zip";
            importReplayDialog.Title = "Select a PUBG Replay in a ZIP File...";

            // Show the Dialog.  
            // If the user clicked OK in the dialog and  
            // a .CUR file was selected, open it.  
            if (importReplayDialog.ShowDialog() == DialogResult.OK)
            {
                ZipFile.ExtractToDirectory(importReplayDialog.FileName, replayloc);
                MessageBox.Show("Success!", "Imported Replay!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                // Assign the cursor in the Stream to the Form's Cursor property.  
                //this.Cursor = new Cursor(openFileDialog1.OpenFile());
            }
        }

        private void replayListRefresh_Click(object sender, EventArgs e)
        {
            RefreshReplayList();
        }

        private void killFileDecoder_Click(object sender, EventArgs e)
        {
            KillFeed steamid = new KillFeed(replayloc + "\\" + replayList.SelectedItem + "\\");
            steamid.ShowDialog();
        }

        private void profileLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            Process.Start("http://steamcommunity.com/profiles/"+profile_id);
        }
    }
}
