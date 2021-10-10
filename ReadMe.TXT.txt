Mark Greenwood

Questions:

CPEN 333								
								
Part 1								
								
Q1	Array Size		Time(MM:SS.ms)	Single Thread	Multi Thread			
	10			0	0.09			
	10^2			0	0.1			
	10^3			0	0.13			
	10^4			0	0.12			
	10^5			0.04	0.13			
	10^6			0.51	0.24			
	10^7			4.15	1.67			
								
Q2	I expected that my code would speed up linearly with the number of threads. Ie with 6 threads it would go 6x as fast. 							
Q3	I did not obtain the speedup I expected as there is some overhead involved with creating threads and waiting for other threads to sync with eachother to combine their information.							
								
Q4		# of Threads	Time	Speed Up Factor				
		1 ST	0.48	1.00				
		1	0.52	0.92				
		5	0.2	2.40				
		10	0.24	2.00				
		25	0.4	1.20				
		50	0.67	0.72				
		100	1.25	0.38				
		200	1.85	0.26				
		300	2.75	0.17				
		400	3.7	0.13				
		500	4.5	0.11				
		750	7.05	0.07				
		1000	8.66	0.06				
								
								
Part 2 								
	No of cores 6							
	1000 samples	3.184						
	3 decimal places 	1,000,000						
								
	Samples	ST	MT	Speed up Factor				
	10^3	0	0.07	0.00				
	10^4	0	0.07	0.00				
	10^5	0.03	0.1	0.30				
	10^6	0.33	0.34	0.97				
	10^7	3.24	0.93	3.48				
	10^8	28.93	6.54	4.42				
								
Q1	Splitting up work between threads requires more thinking on the coders part to figure out how to synch the threads and pass information either between different threads or a thread and the main thread							
								
Q2	When designing concurrent code you need to be aware of things that could access the same variable at the same time. In addition you shouldn’t "lock" too large of a section of code as it can slow down the entire process, instead you should only lock the interactions with shared variables							
								
Q3	I think you would need 1E14 samples to get 7 digits of pi accurately. Based on some reasearch it seems to be around 10^(2N) samples required for N digits. The world record for Pi estimation is 1E13 digits. Which would require 10^(2*1E13) Samples with a monte-carlo method, which is unfathomable.							
	3.1415926							
