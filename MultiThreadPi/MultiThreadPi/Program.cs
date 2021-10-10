using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;


namespace MultiThreadPi
{
    class MainClass
    {
        public static readonly object hitLock = new object();
        static void Main(string[] args)
        {
            long numberOfSamples = 100000000;
            long SampleCapacityMT = numberOfSamples/6;
            long hits = 0;
            double[] hitsMT = new double[1];
            hitsMT[0] = 0;
            double PIMT = 0;
            var rand = new Random();
            Stopwatch stopWatchMT = new Stopwatch();
            Stopwatch stopWatch = new Stopwatch();





            //TODO :start the stopwatch
            stopWatch.Start();
            double pi = EstimatePI(numberOfSamples, ref hits);
            Console.WriteLine(hits);
            Console.WriteLine(pi);
            //TODO :Stop the stopwatch
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            //  PrintArray(arraySingleThread);

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("ST RunTime " + elapsedTime);





            //TODO :start the stopwatch
            stopWatchMT.Start();
           
            

            //Copied from example
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < numberOfSamples/SampleCapacityMT; i++)
            {

                Thread thread = new Thread(() => HitorMiss(SampleCapacityMT,hitsMT));
                thread.Name = string.Format("Thread{0}", i + 1);
                thread.Start();
                threads.Add(thread);

            }
            //join all the threads
            foreach (Thread thread in threads)
                thread.Join();

            //End of example from lecture

           
            lock (hitLock)
            {
                Console.WriteLine(hitsMT[0]);
                PIMT = (double) hitsMT[0] / numberOfSamples * 4;
                Console.WriteLine(PIMT);

            }

            //TODO :Stop the stopwatch
            stopWatchMT.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan tsMT = stopWatchMT.Elapsed;
            //  PrintArray(arraySingleThread);

            // Format and display the TimeSpan value.
            string elapsedTimeMT = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                tsMT.Hours, tsMT.Minutes, tsMT.Seconds,
                tsMT.Milliseconds / 10);
            Console.WriteLine("MT RunTime " + elapsedTimeMT);


             double[,] sampleArr = GenerateSamples(numberOfSamples);

        }
        static double[,] GenerateSamples(long numSamples)
        {
            // Implement  
            var rand = new Random();
            double[,] arr = new double[numSamples, 2];

            for (int i = 0; i < numSamples; i++)
                for (int j = 0; j < 2; j++)
                    arr[i, j] = rand.NextDouble() * (double)2 - (double)1;
            return arr;
        }
        static double EstimatePI(long numSamples, ref long hit)
        {
            double[,] SampleArr = GenerateSamples(numSamples);
            double radius = 0;
            double pi = 0;
      

            for (int i = 0; i < numSamples - 1; i++)
            {
                radius = Math.Pow(Math.Pow(SampleArr[i, 0], 2) + Math.Pow(SampleArr[i, 1], 2), 0.5);
                // Console.WriteLine(radius);
                if (radius <= 1)
                    hit++;
            }
            pi = (double)hit / numSamples * 4;
            return pi;
        }
        static void HitorMiss(long numSamples, double[] hits)
        {

            
                double[,] SampleArr = GenerateSamples(numSamples);
                double radius = 0;
                int h = 0;


                for (int i = 0; i < numSamples - 1; i++)
                {
                    radius = Math.Pow(Math.Pow(SampleArr[i, 0], 2) + Math.Pow(SampleArr[i, 1], 2), 0.5);
                    // Console.WriteLine(radius);
                    if (radius <= 1)
                        h++;
                }
   
            lock (hitLock)
            {     
                hits[0] = hits[0] + h;
            }
        }
        public static void StartThread(long numSamples, double[] hits)
        {
            Thread t = new Thread(new ThreadStart(() => HitorMiss(numSamples, hits)));
            t.Start();
            
        }

        
    }
}
