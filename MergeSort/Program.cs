using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace MergeSort
{
    public class Program
    {
        //NOTE: My mergesort functions is taken from my CPSC 259 lab for mergesort
        // a helper function to print your array
        static void PrintArray(int[] myArray)
        {
            Console.Write("[");
            for (int i = 0; i < myArray.Length; i++)
            {
                Console.WriteLine("{0} ", myArray[i]);

            }
            Console.Write("]");
            Console.WriteLine();

        }


        /*********************** Methods **********************
             *****************************************************/
        /*
        implement Merge method. This method takes two sorted array and
        and constructs a sorted array in the size of combined arrays
        */
        public static void Merge(int[] arr, int l, int r, int m)
        {

            // TODO :implement

            int i, j, k;
            int n1 = m - l + 1;
            int n2 = r - m;

            /* create temp arrays */
            int[] LA = new int[n1];
            int[] RA = new int[n2];

            /* Copy data to temp arrays L[] and R[] */
            for (i = 0; i < n1; i++)
                LA[i] = arr[l + i];
            for (j = 0; j < n2; j++)
                RA[j] = arr[m + 1 + j];
           
                       

            /* Merge the temp arrays back into arr[l..r]*/
            i = 0; // Initial index of first subarray
            j = 0; // Initial index of second subarray
            k = l; // Initial index of merged subarray
            while (i < n1 && j < n2)
            {
                if (LA[i] <= RA[j])
                {
                    arr[k] = LA[i];
                    i++;
                }
                else
                {
                    arr[k] = RA[j];
                    j++;
                }
                k++;
            }

            /* Copy the remaining elements of L[], if there
            are any */
            while (i < n1)
            {
                arr[k] = LA[i];
                i++;
                k++;
            }

            /* Copy the remaining elements of R[], if there
            are any */
            while (j < n2)
            {
                arr[k] = RA[j];
                j++;
                k++;
            }
        }




        /*
        implement MergeSort method: takes an integer array by reference
        and makes some recursive calls to intself and then sorts the array
        */


        public static void MergeSort(int[] A, int L, int R)
        {

            if (L < R)
            {
                // Same as (l+r)/2, but avoids overflow for
                // large l and h
                int m = L + (R - L) / 2;

                // Sort first and second halves
                MergeSort(A, L, m);
                MergeSort(A, m + 1, R);

                Merge(A, L, R, m);


            }
        }
        public static void Main(string[] args)
        {

                int ARRAY_SIZE = 1000000;
                int Max_Val = 1000;
                int num_array = 1000;
                int MTArraySize = ARRAY_SIZE / num_array;
                int[] arraySingleThread = new int[ARRAY_SIZE];




                // TODO : Use the "Random" class in a for loop to initialize an array

                // Instantiate random number generator using system-supplied value as seed.
                var rand = new Random();

                for (int i = 0; i < ARRAY_SIZE; i++)
                {
                    arraySingleThread[i] = rand.Next(Max_Val);
                    //Console.WriteLine(arraySingleThread[i]);
                }
            
                // copy array by value.. You can also use array.copy()
                int[] arrayMultiThread = new int[ARRAY_SIZE];
                arraySingleThread.CopyTo (arrayMultiThread,0);
                int[][] Splitted_array = split_array(arrayMultiThread, num_array);
                int middle = num_array / 2;

                int[] threadArray = new int[num_array];

           
            Stopwatch stopWatchMT = new Stopwatch();
            //TODO :start the stopwatch
            stopWatchMT.Start();
         




            //Copied from example
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < num_array; i++)
            {
                int j = i;
                
               
                Thread thread = new Thread(() => MergeSort(Splitted_array[j], 0, Splitted_array[j].Length - 1));
                thread.Name = string.Format("Thread{0}", j + 1);
                thread.Start();
                threads.Add(thread);

            }
            //join all the threads
            foreach (Thread thread in threads)
                thread.Join();

            //End of example from lecture

            //Append Splitted Arrays
            int[] multiThreadSorted = new int[ARRAY_SIZE];
            int pointer = 0;
            for (int k = 0; k < num_array; k++)
            {
                Splitted_array[k].CopyTo(multiThreadSorted, pointer);
                pointer = pointer + Splitted_array[k].Length;
            }


            //Merging All the multithreaded 
            int countma = num_array;
            int numsorted = 0;
            while (countma > 2)
            {
                for (int i = 0; i < countma; i = i + 2)
                {
                    int L = ARRAY_SIZE / countma * i;
                    int R = ARRAY_SIZE / countma * (i + 2) - 1;
                    int M = ARRAY_SIZE / countma * (i + 1) - 1;
                    if (R > ARRAY_SIZE)
                        break;
                    //Console.WriteLine();
                    //Console.WriteLine(L);
                    //Console.WriteLine(R);
                    //Console.WriteLine(M);
                    //Console.WriteLine(i);
                    //Console.WriteLine(countma);
                    //Console.WriteLine();
                    Merge(multiThreadSorted, L, R, M);
                }
                countma = countma / 2;
            }

            //Merging the Last 3 Arrays
            int TransP1 = (int)(ARRAY_SIZE * 0.4 - 1);
            int TransP2 = (int)(ARRAY_SIZE * 0.8 - 1);

            Merge(multiThreadSorted, 0, TransP2, TransP1);
            Merge(multiThreadSorted, 0, ARRAY_SIZE-1, TransP2);

            //Console.Write(IsSorted(multiThreadSorted));

            
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



            /*TODO : Use the  "Stopwatch" class to measure the duration of time that
                it takes to sort an array using one-thread merge sort and
                multi-thead merge sort
            */
            Stopwatch stopWatch = new Stopwatch();





                //TODO :start the stopwatch
                stopWatch.Start();
                MergeSort(arraySingleThread, 0, arraySingleThread.Length-1);
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

                //TODO: Multi Threading Merge Sort


            

            // a helper function to confirm your array is sorted
            // returns boolean True if the array is sorted
            static bool IsSorted(int[] a)
            {
                int j = a.Length - 1;
                if (j < 1) return true;
                int ai = a[0], i = 1;
                while (i <= j && ai <= (ai = a[i])) i++;
                return i > j;
            }
            
            
            static int[][] split_array(int[] a, int num_array)
            {
                int numrow = new int();
                numrow = a.Length / num_array; //This plus 1 adds an additional slot for remainder/truncation of the division
                int[][] sp_array = new int[num_array][];
                int pointer = 0;

                for (int i = 0; i < num_array && pointer < a.Length; i++)
                {
                    sp_array[i] = new int[numrow];
                    for (int j = 0; j < numrow && pointer < a.Length; j++)
                    {
                        sp_array[i][j] = a[pointer];
                        pointer++;
                        //Console.WriteLine(sp_array[i][j]);
                        //Console.WriteLine(a.Length);
                        //Console.WriteLine(pointer);
                        //Console.WriteLine();
                    }
                }

                return sp_array;
            }

        }




    }


}
