using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using Microsoft.Win32.SafeHandles;

namespace Commander.net
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                Console.Error.WriteLine("Commander.net started");
                var handle = GetStdHandle(-11);
                var strom = new FileStream(handle, FileAccess.Write);
                
                while (true)
                {
                    var line = Console.ReadLine();
                    switch (line) 
                    {
                        case "getDrives":
                            var items = GetDriveItems();
                            var result = Serialize(items);
                            strom.Write(result, 0, result.Length);
                            strom.Flush();
                            break;
                        case "exit":
                            return;
                    }
                }
            }
            catch (Exception e) 
            {
                Console.Error.WriteLine(e);
            }
            finally
            {
                Console.Error.WriteLine("Commander.net stopped");
            }
        }
        
        static DriveItem[] GetDriveItems()
        {
            return DriveInfo.GetDrives()
                .Where(n => n.IsReady)
                .OrderBy(n => n.Name).Select(n => new DriveItem
                {
                    name = n.Name,
                    description = n.VolumeLabel,
                    size = n.TotalSize,
                    isNetworkDrive = n.DriveType == DriveType.Network
                }).ToArray();
        }

        static byte[] Serialize(object data) 
        {
            var type = data.GetType();
            var jason = new DataContractJsonSerializer(type);
            var memStm = new MemoryStream();
            jason.WriteObject(memStm, data);
            memStm.Capacity = (int)memStm.Length;
            return memStm.GetBuffer();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle GetStdHandle(int nStdHandle);
    }
}
