using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;


namespace FillHdd
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Wrong arguments");
                Console.WriteLine("Usage: FillHdd FileThatWillBeUsedToFillHdd NumberOfMBToLeaveFreeOnHardDisk");
                Console.WriteLine("Example: FillHdd d:\\FillHdd\\data.bin 30");
                Console.ReadLine();
                return;
            }
            var megabytesToLeaveFree = Convert.ToInt64(args[1])*1024*1024;
            Fill(args[0], megabytesToLeaveFree);
        }


        public static void Fill(string fileName, long megabytesToLeaveFree)
        {
            var fi = new FileInfo(fileName);
            var root = fi.Directory.Root;
            var driveInfos = DriveInfo.GetDrives();
            var driveInfo = driveInfos.Where(x => String.Equals(x.RootDirectory.FullName , root.FullName,StringComparison.OrdinalIgnoreCase)).First();
            var amountToFill = driveInfo.TotalFreeSpace - megabytesToLeaveFree;
            using (MemoryMappedFile.CreateFromFile(fileName, FileMode.Create, "tt", amountToFill))
            {
            }
        }
    }
}