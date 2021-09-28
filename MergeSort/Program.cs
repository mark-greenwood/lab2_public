using System;
using System.Diagnostics;
using System.Threading;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {

            int ARRAY_SIZE = 1000000000;
            int[] arraySingleThread = new int[ARRAY_SIZE];




            // TODO : Use the "Random" class in a for loop to initialize an array

            // Instantiate random number generator using system-supplied value as seed.
            var rand = new Random();

            for (int i = 0; i < ARRAY_SIZE; i++)
            {
                arraySingleThread[i] = rand.Next(1000);
                //Console.WriteLine(arraySingleThread[i]);
            }
            
            // copy array by value.. You can also use array.copy()
            int[] arrayMultiThread = new int[ARRAY_SIZE];
            arraySingleThread.CopyTo (arrayMultiThread,0);


            /*TODO : Use the  "Stopwatch" class to measure the duration of time that
                it takes to sort an array using one-thread merge sort and
                multi-thead merge sort
            */
                Stopwatch stopWatch = new Stopwatch();



            //TODO :start the stopwatch
            stopWatch.Start();
            mergeSort(arraySingleThread, 0, arraySingleThread.Length-1);
            //TODO :Stop the stopwatch
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
          //  PrintArray(arraySingleThread);

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

            //TODO: Multi Threading Merge Sort







            /*********************** Methods **********************
             *****************************************************/
            /*
            implement Merge method. This method takes two sorted array and
            and constructs a sorted array in the size of combined arrays
            */
        static void merge(int[] arr, int l, int r, int m)
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
            

        static void mergeSort(int[] A, int L, int R)
        {

            if (L < R)
            {
                // Same as (l+r)/2, but avoids overflow for
                // large l and h
                int m = L + (R - L) / 2;

                // Sort first and second halves
                mergeSort(A, L, m);
                mergeSort(A, m + 1, R);

                merge(A, L, R, m);

              
            }
        }


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
            

        }


    }

}
