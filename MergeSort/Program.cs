using System;
using System.Diagnostics;
using System.Threading;

namespace MergeSort
{
    public class Program
    {

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
           
            //Console.WriteLine();
            //PrintArray(LA);
            //Console.WriteLine();
            //PrintArray(RA);
            

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

                int ARRAY_SIZE = 1000;
                int num_array = 10;
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
                int[][] Splitted_array = split_array(arrayMultiThread, num_array);
                int middle = num_array / 2;

                int[] threadArray = new int[num_array];

            //MergeSort(Splitted_array[1], 0, Splitted_array[1].Length-1); //-1 is to prevent array from going out of bounds
            PrintArray(Splitted_array[1]);
            
            Thread t0 = new Thread(() => MergeSort(Splitted_array[0], 0, Splitted_array[0].Length - 1));
            t0.Start();
            
            Thread t1 = new Thread(() => MergeSort(Splitted_array[1], 0, Splitted_array[1].Length-1));
            t1.Start();
           
            Thread t2 = new Thread(() => MergeSort(Splitted_array[2], 0, Splitted_array[2].Length - 1));
            t2.Start();

            Thread t3 = new Thread(() => MergeSort(Splitted_array[3], 0, Splitted_array[3].Length - 1));
            t3.Start();

            Thread t4 = new Thread(() => MergeSort(Splitted_array[4], 0, Splitted_array[4].Length - 1));
            t4.Start();

            Thread t5 = new Thread(() => MergeSort(Splitted_array[5], 0, Splitted_array[5].Length - 1));
            t5.Start();

            Thread t6 = new Thread(() => MergeSort(Splitted_array[6], 0, Splitted_array[6].Length - 1));
            t6.Start();

            Thread t7 = new Thread(() => MergeSort(Splitted_array[7], 0, Splitted_array[7].Length - 1));
            t7.Start();
            
            Thread t8 = new Thread(() => MergeSort(Splitted_array[8], 0, Splitted_array[8].Length - 1));
            t8.Start();

            Thread t9 = new Thread(() => MergeSort(Splitted_array[9], 0, Splitted_array[9].Length - 1));
            t9.Start();

            t0.Join();
            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();
            t6.Join();
            t7.Join();
            t8.Join();
            t9.Join();

            int[] multiThreadSorted = new int[ARRAY_SIZE];
            int pointer = 0;
            Splitted_array[0].CopyTo(multiThreadSorted, pointer);
            pointer = pointer + Splitted_array[0].Length;
            Splitted_array[1].CopyTo(multiThreadSorted, pointer);
            pointer = pointer + Splitted_array[1].Length;
            Splitted_array[2].CopyTo(multiThreadSorted, pointer);
            pointer = pointer + Splitted_array[2].Length;
            Splitted_array[3].CopyTo(multiThreadSorted, pointer);
            pointer = pointer + Splitted_array[3].Length;
            Splitted_array[4].CopyTo(multiThreadSorted, pointer);
            pointer = pointer + Splitted_array[4].Length;
            Splitted_array[5].CopyTo(multiThreadSorted, pointer);
            pointer = pointer + Splitted_array[5].Length;
            Splitted_array[6].CopyTo(multiThreadSorted, pointer);
            pointer = pointer + Splitted_array[6].Length;
            Splitted_array[7].CopyTo(multiThreadSorted, pointer);
            pointer = pointer + Splitted_array[7].Length;
            Splitted_array[8].CopyTo(multiThreadSorted, pointer);
            pointer = pointer + Splitted_array[8].Length;
            Splitted_array[9].CopyTo(multiThreadSorted, pointer);

            Console.WriteLine(IsSorted(Splitted_array[0]));
            Console.WriteLine(IsSorted(Splitted_array[1]));
            Console.WriteLine(IsSorted(Splitted_array[2]));
            Console.WriteLine(IsSorted(Splitted_array[3]));
            Console.WriteLine(IsSorted(Splitted_array[4]));
            Console.WriteLine(IsSorted(Splitted_array[5]));
            Console.WriteLine(IsSorted(Splitted_array[6]));
            Console.WriteLine(IsSorted(Splitted_array[7]));
            Console.WriteLine(IsSorted(Splitted_array[8]));
            Console.WriteLine(IsSorted(Splitted_array[9]));

            Merge(multiThreadSorted, 0, 199, 99);
            Merge(multiThreadSorted, 200, 399, 299);
            Merge(multiThreadSorted, 400, 599, 499);
            Merge(multiThreadSorted, 600, 799, 699);
            Merge(multiThreadSorted, 800, 999, 899);

            Merge(multiThreadSorted, 0, 399, 199);
            Merge(multiThreadSorted, 400, 799, 599);

            Merge(multiThreadSorted, 0, 799, 399);
           
            Merge(multiThreadSorted, 0, 999, 799);

            Console.WriteLine();
            Console.WriteLine(IsSorted(multiThreadSorted));









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
                Console.WriteLine("RunTime " + elapsedTime);

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
