using System;
using System.IO;
using System.Runtime.InteropServices;
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

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle GetStdHandle(int nStdHandle);
    }
}
