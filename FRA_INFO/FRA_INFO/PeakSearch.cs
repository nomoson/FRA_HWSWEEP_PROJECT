using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
namespace PeakSearch
{
    public class Peak_Search
    {
        public int smoothwidth, peakgroup, smoothtype, ends;
        public float SlopeThreshold, AmpThreshold;
        public Peak_Search(int PID) // constructor
        {
            switch (PID)
            {
                case 14: //7374 parameters
                    SlopeThreshold = 0.8F; AmpThreshold = -5.0F;
                    break;
                case 9: //7371 parameters
                    SlopeThreshold = 2.0F; AmpThreshold = -5.0F; ;
                    break;
                default: //7374 parameters
                    SlopeThreshold = 0.8F; AmpThreshold = -5.0F; ;
                    break;
            }
            smoothwidth = 1; peakgroup = 3; smoothtype = 1; ends = 0;
        }

        public List<List<float>> startPeakSearch(string filename)
        {

            //float SlopeThreshold = 0.8F, AmpThreshold = -5.0F;  //7374 parameters
            //float SlopeThreshold = 2.0F, AmpThreshold = -5.0F; //7371 parameters
            //int smoothwidth = 1, peakgroup = 3, smoothtype = 1, ends = 0;
            int i = 0, counter = 0; 
            string line;

            // Read the file and count & display lines.  
            // FileStream fs = new FileStream(@"D:\1_Project\PeakSearch\7375.txt", FileMode.Open);
            FileStream fs = new FileStream(filename, FileMode.Open);
            StreamReader file = new StreamReader(fs, Encoding.Default);
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                counter++;
            }
            fs.Position = 0;
            //System.Console.WriteLine("There were {0} Raw lines.", counter);
            //Console.WriteLine("-------------------------");
            // Suspend the screen.  
            System.Console.ReadLine();

            /* load txt file to x-y-z vector */
            string[] s2 = new string[3];

            // Assign x,y,z vector size
            float[] x = new float[counter];
            float[] y = new float[counter];
            float[] d = new float[counter];
            int[] z = new int[counter];

            line = file.ReadLine();
            while (line != null)
            {
                s2 = line.Split(' ');
                x[i] = float.Parse(s2[0]);
                y[i] = float.Parse(s2[1]);
                z[i] = int.Parse(s2[2]);
                //System.Console.WriteLine(x[i]);
                i++;
                line = file.ReadLine();
            }
            // System.Console.ReadLine();
            file.Close();
            fs.Close();
            //
            /*  quote function */
            if (smoothwidth > 1)
            {
                d = fcn.fastsmooth(fcn.deriv(y), smoothwidth, smoothtype, ends);
            }
            else
            {
                d = fcn.deriv(y);
            }
            //
            //foreach (float j in d)
            //{
            //    System.Console.WriteLine(j);
            //}
            // Console.WriteLine("Here are {0} first derivatives", counter);
            // Console.WriteLine("------------------------------");
            // Console.ReadKey();
            //
            // Assign result summary vector
            List<System.Collections.Generic.List<float>> Plist = new List<List<float>>(); //2D list 
            int vectorlength = y.Length;
            int peak = 1;
            int pindex;
            float PeakX = 0, PeakY = 0, PeakZ = 0;
            // Round and double2int
            double smth = Math.Round((double)smoothwidth / 2f, MidpointRounding.AwayFromZero);
            int int_smth = Convert.ToInt32(smth);
            double pkgp = Math.Round((double)peakgroup / 2f, MidpointRounding.AwayFromZero);
            int int_pkgp = Convert.ToInt32(pkgp);
            double n = Math.Round(((double)peakgroup / 2f + 1f), MidpointRounding.AwayFromZero);
            int int_n = Convert.ToInt32(n);
            //
            for (int j = 2 * int_smth - 2; j < y.Length - smoothwidth - 1; j++)
            {
                if (Math.Sign(d[j]) > Math.Sign(d[j + 1]))
                {
                    if ((d[j] - d[j + 1]) > SlopeThreshold)
                    {
                        float[] xx = new float[peakgroup];
                        float[] yy = new float[peakgroup];
                        float[] zz = new float[peakgroup];
                        //
                        for (int k = 0; k < peakgroup; k++)
                        {
                            int groupindex = j + k - int_n + 3;
                            if (groupindex < 1) groupindex = 1;
                            if (groupindex > vectorlength) groupindex = vectorlength;
                            xx[k] = x[groupindex];
                            yy[k] = y[groupindex];
                            zz[k] = z[groupindex];
                        }
                        //
                        if (peakgroup < 4)
                        {
                            PeakY = yy.Max();
                        }
                        else
                        {
                            PeakY = fcn.sum(yy) / yy.Length;
                        }
                        pindex = fcn.val2ind(yy, PeakY);
                        PeakX = xx[pindex]; PeakZ = zz[pindex];
                        //
                        if (Double.IsNaN(PeakX) || Double.IsNaN(PeakY) || Double.IsNaN(PeakZ) || PeakY < AmpThreshold)
                        {
                            //Skip this peak
                        }
                        else
                        {
                            Plist.Add(new List<float>(4) { peak, PeakX, PeakY, PeakZ });
                            peak = peak + 1; // Move on to next peak
                        }
                    }
                }
            }
            //List<float>[] PSUMY = Plist.ToArray();
             return Plist;
  
            //Console.WriteLine(Plist[1][3]);
       //     Console.ReadKey();
        }
    }
}
