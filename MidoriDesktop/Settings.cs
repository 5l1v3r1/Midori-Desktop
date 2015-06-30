﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MidoriDesktop
{
    class Settings
    {
        public static bool
        HotkeyImageCtrl = false,
        HotkeyImageAlt = false,
        HotkeyImageShift = false,

        HotkeyVideoCtrl = false,
        HotkeyVideoAlt = false,
        HotkeyVideoShift = false;

        public static int HotkeyImage, HotkeyVideo;

        static string file;
        public static void Initialize()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Midori\";
            file = folder + "settings";

            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            if (!File.Exists(file))
            {
                FileStream stream = File.Create(file);

                byte[] buffer = Encoding.ASCII.GetBytes("HotkeyImage=1:0:1:67\nHotkeyVideo=1:0:1:86");
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
                stream.Dispose();

                HotkeyImageCtrl = HotkeyImageShift = HotkeyVideoCtrl = HotkeyVideoShift = true;
                HotkeyImage = 67;
                HotkeyVideo = 86;
            }
            else
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    string[] spl = line.Split('=');

                    switch (spl[0])
                    {
                        case "HotkeyImage":
                            string[] spl2 = spl[1].Split(':');
                            if (spl2.Length != 4) break;

                            HotkeyImageCtrl = (spl2[0] == "1");
                            HotkeyImageAlt = (spl2[1] == "1");
                            HotkeyImageShift = (spl2[2] == "1");
                            HotkeyImage = Int32.Parse(spl2[3]);
                            break;
                        case "HotkeyVideo":
                            string[] spl2v = spl[1].Split(':');
                            if (spl2v.Length != 4) break;

                            HotkeyVideoCtrl = (spl2v[0] == "1");
                            HotkeyVideoAlt = (spl2v[1] == "1");
                            HotkeyVideoShift = (spl2v[2] == "1");
                            HotkeyVideo = Int32.Parse(spl2v[3]);
                            break;
                    }
                }
            }
        }

        public static void Save()
        {
            FileStream stream = (File.Exists(file) ? File.OpenWrite(file) : File.Create(file));

            byte[] buffer = Encoding.ASCII.GetBytes(
                String.Format("HotkeyImage={0}:{1}:{2}:{3}\nHotkeyVideo={4}:{5}:{6}:{7}",
                (HotkeyImageCtrl ? 1 : 0),
                (HotkeyImageAlt ? 1 : 0),
                (HotkeyImageShift ? 1 : 0),
                HotkeyImage,
                (HotkeyVideoCtrl ? 1 : 0),
                (HotkeyVideoAlt ? 1 : 0),
                (HotkeyVideoShift ? 1 : 0),
                HotkeyVideo)
            );
            stream.Write(buffer, 0, buffer.Length);

            stream.Close();
            stream.Dispose();
        }
    }
}
