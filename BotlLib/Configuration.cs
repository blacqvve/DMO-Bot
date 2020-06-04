// Decompiled with JetBrains decompiler
// Type: DMOReaper.Configuration
// Assembly: DMO Reaper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572F02F5-7920-4E84-A8BE-03324AAC1898
// Assembly location: C:\Users\yigit\Desktop\update\DMO Reaper.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace BotLib
{
     public class Configuration
     {
          public static bool isConfigured = false;
          public static int CONFIG_LEVEL = 0;
          public static int DEFAULT_JITTER_TIME = 10000;
          public static int DEFAULT_JITTER_TIME_MOVEMENT = 2000;
          public static int HP_PERCENTAGE_RECOVER = 0;
          public static int DS_PERCENTAGE_RECOVER = 0;
          public static bool CHANGE_TARGET_AFTER_DEATH = false;
          public static bool RETURN_TO_ORIGINAL_POS = false;
          public static string TARGET_KEY;
          public static string ATTACK_KEY;
          public static string PICKUP_KEY;
          public static string SKILL_KEY_1;
          public static string SKILL_KEY_2;
          public static string CONSUMABLE_1;
          public static string CONSUMABLE_2;

          public static string[] configrationString;
          public static bool SaveToFile(string[] configs)
          {
               if (configs.Length<0)
                    return false;
               SaveFileDialog saveFileDialog = new SaveFileDialog();
               saveFileDialog.Title = "Select folder";
               saveFileDialog.DefaultExt = "botcfg";
               saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
               saveFileDialog.Filter = "botcfg files (*.botcfg)|*.botcfg|All files (*.*)|*.*";
               if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return false;
               try
               {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                    formatter.Serialize(stream, configs);
                    stream.Close();
               }
               catch (IOException ex)
               {
                    int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
               }
            
               return true;
          }
          public static bool ReadFromFile()
          {
               OpenFileDialog openFileDialog = new OpenFileDialog();
               openFileDialog.Title = "Select File To Load";
               openFileDialog.DefaultExt = "botcfg";
               openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
               openFileDialog.Filter = "botcfg files (*.botcfg)|*.botcfg|All files(*.*)|*.*";
               if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return false;
               try
               {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                    configrationString = (string[])formatter.Deserialize(stream);
                    stream.Close();
               }
               catch (IOException ex)
               {
                    int num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
               }
               return true;
          }

          private static void CreateConfigFile(string[] args)
          {
               configrationString = args;
          }
          public static bool configure(string[] configs)
          {
               int num = 0;
               foreach (string config in configs)
               {
                    if (config.Contains("TARGET"))
                    {
                         Configuration.TARGET_KEY = config.Split(':')[1];
                         ++num;
                    }
                    if (config.Contains("ATTACK"))
                    {
                         Configuration.ATTACK_KEY = config.Split(':')[1];
                         ++num;
                    }
                    if (config.Contains("PICKUP"))
                    {
                         Configuration.PICKUP_KEY = config.Split(':')[1];
                         ++num;
                    }
                    if (config.Contains("SKILL1"))
                    {
                         Configuration.SKILL_KEY_1 = config.Split(':')[1];
                         ++num;
                    }
                    if (config.Contains("SKILL2"))
                    {
                         Configuration.SKILL_KEY_2 = config.Split(':')[1];
                         ++num;
                    }
                    if (config.Contains("CONSUMABLE1"))
                    {
                         Configuration.CONSUMABLE_1 = config.Split(':')[1];
                         ++num;
                    }
                    if (config.Contains("CONSUMABLE2"))
                    {
                         Configuration.CONSUMABLE_2 = config.Split(':')[1];
                         ++num;
                    }
                    if (config.Contains("MOVEMENT_INTERVAL"))
                    {
                         Configuration.DEFAULT_JITTER_TIME = int.Parse(config.Split(':')[1]);
                         ++num;
                    }
                    if (config.Contains("MOVEMENT_DURATION"))
                    {
                         Configuration.DEFAULT_JITTER_TIME_MOVEMENT = int.Parse(config.Split(':')[1]);
                         ++num;
                    }
                    if (config.Contains("HP_PERCENT"))
                    {
                         Configuration.HP_PERCENTAGE_RECOVER = int.Parse(config.Split(':')[1]);
                         ++num;
                    }
                    if (config.Contains("DS_PERCENT"))
                    {
                         Configuration.DS_PERCENTAGE_RECOVER = int.Parse(config.Split(':')[1]);
                         ++num;
                    }
                    if (config.Contains("CHANGE_T"))
                    {
                         Configuration.CHANGE_TARGET_AFTER_DEATH = int.Parse(config.Split(':')[1]) == 1;
                         ++num;
                    }
                    if (config.Contains("RETURN_ORIGIN"))
                    {
                         Configuration.RETURN_TO_ORIGINAL_POS = int.Parse(config.Split(':')[1]) == 1;
                         ++num;
                    }
               }
               if (num != 13)
                    return false;
               Configuration.isConfigured = true;
               Configuration.CONFIG_LEVEL = num;
               CreateConfigFile(configs);
               return true;
          }
     }
}
