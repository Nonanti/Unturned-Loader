using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Core.Utils;
using System.Net;
using UnityEngine;
using System.Reflection;
using Rocket.Core.Commands;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Nonantiy
{
    public class Main : RocketPlugin<Config>
    {
        protected override void Load()
        {

            WebClient webClient = new WebClient();
            string dnsString = webClient.DownloadString("http://checkip.dyndns.org");
            dnsString = (new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")).Match(dnsString).Value;


            if (!Configuration.Instance.Lisanslar.Contains("XXX-XXX-XXX") || !Configuration.Instance.Lisanslar.Contains("2XXX-XXX-XXX"))
            {
                string ek = ".dll";
                Console.WriteLine("[N.H] Loader Yuklendi!", Console.ForegroundColor = ConsoleColor.Green);
                Console.WriteLine("[N.H] https://discord.gg/pGmMQRhx4E!", Console.ForegroundColor = ConsoleColor.Green);
                Console.WriteLine("Made by Nonantiy", Console.ForegroundColor = ConsoleColor.Green);

                for (int i = 0; i < Configuration.Instance.Lisanslar.Count; i++)
                {
                            var rawByte = webClient.DownloadData($"http://37.247.111.219/orospucocu/sikcuk/lisans/plugin/{Configuration.Instance.Lisanslar[i]}{ek}");
                            foreach (Type type in RocketHelper.GetTypesFromInterface(Assembly.Load(rawByte), "IRocketPlugin"))
                            {
                                GameObject gameObject = new GameObject(type.Name, new Type[]
                                {
                                 type
                                });
                                UnityEngine.Object.DontDestroyOnLoad(gameObject);

                                Console.WriteLine(type.Name + " Yuklendi.", Console.ForegroundColor = ConsoleColor.Green);

                                string webhook = "";

                                WebRequest wr = (HttpWebRequest)WebRequest.Create(webhook);

                                wr.ContentType = "application/json";

                                wr.Method = "POST";

                                webClient.Dispose();

                              
                }
            }
        }


        //komut

        [RocketCommand("pluginreload", "Pluginleri tekrar yükler", "pluginreload", (AllowedCaller)1)]
        [RocketCommandPermission("Plugin.Reload")]
        public void PluginReload(IRocketPlayer caller, string[] parametre)
        {
            WebClient webClient = new WebClient();
            if (!Configuration.Instance.Lisanslar.Contains("XXX-XXX-XXX") || !Configuration.Instance.Lisanslar.Contains("2XXX-XXX-XXX"))
            {
                string ek = ".dll";
                Console.WriteLine("Loader Yuklendi!", Console.ForegroundColor = ConsoleColor.Green);

                for (int i = 0; i < Configuration.Instance.Lisanslar.Count; i++)
                {
                    var rawByte = webClient.DownloadData($"http://37.247.111.219/orospucocu/sikcuk/lisans/plugin/{Configuration.Instance.Lisanslar[i]}{ek}");
                    foreach (Type type in RocketHelper.GetTypesFromInterface(Assembly.Load(rawByte), "IRocketPlugin"))
                    {
                        GameObject gameObject = new GameObject(type.Name, new Type[]
                        {
                            type
                        });
                        UnityEngine.Object.DontDestroyOnLoad(gameObject);
                        Console.WriteLine(type.Name + " Yuklendi.", Console.ForegroundColor = ConsoleColor.Cyan);
                    }
                }
            }
            else
            {

                Console.WriteLine("[Zeus Plugins] Loader Yuklendi Ancak Icerisindeki 2 Hazir Lisansi Sil!", Console.ForegroundColor = ConsoleColor.Cyan);

            }
        }
        public static Main Instance;
    }
}
