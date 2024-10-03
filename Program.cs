using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LHPCI
{
    class Program
    {
        public static void Main(string[] args)
        {
            string inputfile = args[0];
            string objects = File.ReadAllText(inputfile);
            // Split the input file into lines
            string[] lines = objects.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string outputfile = args[1];
            string bigoutput = "";
            foreach (string line in lines)
            {
                if (line == "")
                {
                    continue;
                }
                string[] parts = line.Split('\t');
                //0 is PCI, ignore
                //1 is vendor ID in hex
                //2 is vendor name, ignore
                //3 is device ID in hex
                //4 is device name
                //5 is Vendor Device, ignore

                try
                {
                string final = $"{{0x{parts[1]}, 0x{parts[3]}, \"{parts[4]}\"}},\n";
                bigoutput += final;
                }
                catch
                {
                    Console.WriteLine("Error parsing line: " + line);
                }
            }
            File.WriteAllText(outputfile, bigoutput);
        }
    }
}

// Example text to parse: "PCI	10de	NVIDIA Corporation	28b8	AD107GLM [RTX 2000 Ada Generation Laptop GPU]	Vendor Device"
// First hex number is the vendor ID, second hex number is the device ID, the Text after the second hex number is the name of the device, stop parsing at Vendor Device
// Example output: {0x10DE, 0x28B8, "AD107GLM [RTX 2000 Ada Generation Laptop GPU]"},