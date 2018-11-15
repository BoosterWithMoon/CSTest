using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractWavelengthArr
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfSpectrometers;                  // actually attached and talking to us

            OmniDriver.CCoWrapper wrapper = new OmniDriver.CCoWrapper();
            // obsolete wrapper.CreateWrapper();                    // this is the object through which we will access all of Omnidriver's capabilities

            numberOfSpectrometers = wrapper.openAllSpectrometers(); // Gets an array of spectrometer objects
            if (numberOfSpectrometers == 0) {
                wrapper.closeAllSpectrometers();
                return;
            } 
            else if (numberOfSpectrometers == 1)
            {
                Console.WriteLine("Starting wavelength extraction...\n\n");
                int pixelCount = wrapper.getNumberOfPixels(0);
                double[] wavelength = new double[pixelCount];
                wavelength = wrapper.getWavelengths(0);

                string path = System.IO.Directory.GetCurrentDirectory();
                System.IO.FileStream fileStream = new System.IO.FileStream(path+"\\wavelength.txt", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileStream);
                for(int i=0; i<pixelCount; i++)
                {
                    sw.WriteLine(wavelength[i].ToString());
                }

                Console.WriteLine("Wavelength extraction has been done.");
                Console.WriteLine("Please input enter key to exit this program.");
                Console.ReadLine();

                Console.WriteLine("GIT TESTING!!");

                wrapper.closeAllSpectrometers();
                sw.Close();
                fileStream.Close();
            }
        }
    }
}
