namespace CPUSchedulerPart2
{
    class Process
    {
        public string Name { get; set; }
        public int ArrivalTime { get; set; }
        public int ExecuteTime { get; set; }  //czas wykonywania 
        public int Priority { get; set; }     //priorytet
        public int ServiceTime { get; set; }  // czas przetwarzania
        public int WaitTime { get; set; }  
    }
}
