using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;

namespace CPUSchedulerPart1
{
    public partial class MainView : Form
    {
        static string change;
        
        public MainView()
        {
            InitializeComponent();
            btnFCFSO.Enabled = false;
            btnSJFO.Enabled = false;
            btnFCFSZ.Enabled = false;
            btnSJFZ.Enabled = false;
        }
        List<Process> processList = new List<Process>();
        void ReadProcesses() 
        {
            using(StreamReader sr = new StreamReader(@"..\..\..\ProcessList1.txt"))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    string[] s = line.Split(' ');
                    var p = new Process();
                    p.Name = s[0];
                    p.ArrivalTime = Convert.ToInt32(s[1]);
                    p.ExecuteTime = Convert.ToInt32(s[2]);
                    processList.Add(p);
                }
            }
        }

        void WriteProcesses( List<Process> list, int time, float avgServT, float avgWaitT, string type) // zapisuje dane do pliku Outputdata.t
        {
            using (StreamWriter sw = new StreamWriter((@"..\..\..\Outputdata.txt"), true))
            {
                sw.WriteLine("Dane wyjsciowe dla algorytmu " + type);
                foreach (Process p in list)
                {
                    sw.WriteLine("Proces nr {0}: czas oczekiwania: {1}[ms] czas przetwarzania: {2}[ms] ", p.Name, p.WaitTime, p.ServiceTime);
                }
                sw.WriteLine("\nCzas do którego wszystkie procesy się wykonały: {0}[ms]\r\nśredni czas przetwarzania: {1}[ms]\r\nśredni czas oczekiwania: {2}[ms]\r\n", time, avgServT, avgWaitT);
            }
        }

        void FCFSZ() 
        {
            List<Process> result = processList.OrderBy(x => x.ArrivalTime).ToList();
            int time;
            ClearChart(chart1);
            result[0].WaitTime = 0;
            result[0].ServiceTime = result[0].ExecuteTime;
            time = result[0].ServiceTime;
            chart1.Name = "wykres Gantta";
            chart1.ChartAreas[0].AxisY.Title = "czas [ms]";
            chart1.ChartAreas[0].AxisX.Title = "nr procesu";
            txt1.AppendText(result[0].Name + "  czas oczekiwania : " + result[0].WaitTime + "[ms] czas przetwarzania: " + result[0].ServiceTime + "[ms]\n");
            chart1.Series["s1"].Points.AddXY(result[0].Name, result[0].ArrivalTime, time);
            for (int i = 1; i < result.Count(); i++)
            {
                txt1.AppendText(result[i].Name + " ");
                if(result[i].ArrivalTime < time)
                {
                    result[i].WaitTime = Math.Abs(result[i].ArrivalTime - time);
                    time += result[i].ExecuteTime;
                }
                else
                {
                    result[i].WaitTime = 0;
                    time = result[i].ExecuteTime + result[i].ArrivalTime;
                }
                result[i].ServiceTime = time - result[i].ArrivalTime;
                txt1.AppendText(" czas oczekiwania : " + result[i].WaitTime + "[ms] czas przetwarzania: " + result[i].ServiceTime + "[ms] \n");
                chart1.Series["s1"].Points.AddXY(result[i].Name, time - result[i].ExecuteTime , time);
            }
            CalculateAverage(result, time, "FCFS - zamknięta pula");
        }

        void FCFSO()  
        {
            bool decision = true;
            List<Process> result = processList.OrderBy(x => x.ArrivalTime).ToList();
            int time = result[0].ServiceTime;
            result[0].WaitTime = 0;
            result[0].ServiceTime = result[0].ExecuteTime;
            ClearChart(chart1);
            chart1.Series["s1"].Points.AddXY(result[0].Name, result[0].ArrivalTime, time);
            chart1.Name = "wykres Gantta";
            chart1.ChartAreas[0].AxisY.Title = "czas [ms]";
            chart1.ChartAreas[0].AxisX.Title = "nr procesu";
            change = Interaction.InputBox("Czy dodac nowy proces? (wpisz 'tak', aby dodać lub 'nie' aby zamknąć pulę procesów) ", " ", "", -1, -1);
            for(int i = 1; i < result.Count(); i++)
            {
                if(decision)
                {
                    if(change == "tak")
                    {
                        Process p = new Process();
                        p.Name = Convert.ToString(result.Count() + 1);
                        p.ArrivalTime = Convert.ToInt32(Interaction.InputBox(@"Podaj czas przyjścia procesu, nie mniejszy niż  " + time + "[ms]", "Dodawanie nowego prcesu.", "", -1, -1));
                        p.ExecuteTime = Convert.ToInt32(Interaction.InputBox("Podaj czas wykonywania procesu: ", "Dodawanie nowego prcesu.", "", -1, -1));
                        result.Add(p);
                        result = result.OrderBy(x => x.ArrivalTime).ToList();
                        change = Interaction.InputBox("Czy dodac nowy proces? ", " ", "", -1, -1);
                    }
                    else if(change == "nie")
                    {
                        MessageBox.Show("Nie będzie już możliwości dodawania kolejnych procesów");
                        decision = false;
                    }
                }
                txt1.AppendText(result[i].Name + " ");
                if(result[i].ArrivalTime < time)
                {
                    result[i].WaitTime = Math.Abs(result[i].ArrivalTime - time);
                    time += result[i].ExecuteTime;
                }
                else
                {
                    result[i].WaitTime = 0;
                    time = result[i].ExecuteTime + result[i].ArrivalTime;
                }
                result[i].ServiceTime = time - result[i].ArrivalTime;
                txt1.AppendText(" czas oczekiwania : " + result[i].WaitTime + "[ms] czas przetwarzania: " + result[i].ServiceTime + "[ms] \n");
                chart1.Series["s1"].Points.AddXY(result[i].Name, time - result[i].ExecuteTime, time);
            }
            CalculateAverage(result, time, "FCFS - otwarta pula");
        }

        void SJFZ()
         {
            List<Process> processList1 = new List<Process>();
            List<Process> result = new List<Process>();
            Process p1 = new Process();
            processList1 = processList.OrderBy(x => x.ExecuteTime).ToList();
            processList1 = processList1.OrderBy(x => x.ArrivalTime).ToList();
            int allSorted = processList1.Count() - 1;
            p1.Name = processList1[0].Name;
            p1.ArrivalTime = processList1[0].ArrivalTime;
            p1.ExecuteTime = processList1[0].ExecuteTime;
            p1.WaitTime = 0;
            p1.ServiceTime = processList1[0].ExecuteTime;
            result.Add(p1);

            int time = result[0].ArrivalTime + result[0].ExecuteTime;
            ClearChart(chart1);
            chart1.Name = "wykres Gantta";
            chart1.ChartAreas[0].AxisY.Title = "czas [ms]";
            chart1.ChartAreas[0].AxisX.Title = "nr procesu";
            chart1.Series["s1"].Points.AddXY(p1.Name, p1.ArrivalTime, time);
            txt1.AppendText(result[0].Name + "  czas oczekiwania : " + result[0].WaitTime + "[ms] czas przetwarzania: " + result[0].ServiceTime + "[ms]\n");
            processList1.RemoveAt(0);
            while( allSorted != 0)
            {
                for(int i = 0; i < processList1.Count(); i++)
                {
                    if(processList1.Any(x => x.ArrivalTime <= time ))
                    {
                        List<Process> temporalList = new List<Process>();
                        foreach (Process item in processList1)
                        {
                            if(item.ArrivalTime <= time)
                                temporalList.Add(item);
                            
                        }
                        temporalList = temporalList.OrderBy(x => x.ExecuteTime).ToList();
                        Process p = new Process();
                        p.Name = temporalList[0].Name;
                        p.ArrivalTime = temporalList[0].ArrivalTime;
                        p.ExecuteTime = temporalList[0].ExecuteTime;
                        p.WaitTime = time - p.ArrivalTime;
                        p.ServiceTime = p.WaitTime + p.ExecuteTime;
                        result.Add(p);
                        time += p.ExecuteTime;
                        txt1.AppendText(p.Name + "  czas oczekiwania : " + p.WaitTime + "[ms] czas przetwarzania: " + p.ServiceTime + "[ms]\n");
                        processList1.RemoveAt(processList1.FindIndex(x => x.Name == p.Name));
                        chart1.Series["s1"].Points.AddXY(p.Name, time - p.ExecuteTime, time);
                        allSorted--;
                    }
                    else
                    {
                        processList1 = processList1.OrderBy(x => x.ExecuteTime).ToList();
                        processList1 = processList1.OrderBy(x => x.ArrivalTime).ToList();
                        processList1[0].WaitTime = 0;
                        processList1[0].ServiceTime = processList1[0].ExecuteTime;
                        result.Add(processList1[0]);
                        time = processList1[0].ArrivalTime + processList1[0].ExecuteTime;
                        chart1.Series["s1"].Points.AddXY(processList1[0].Name, time - processList1[0].ExecuteTime, time);
                        txt1.AppendText(processList1[0].Name + "  czas oczekiwania : " + processList1[0].WaitTime + "[ms] czas przetwarzania: " + processList1[0].ServiceTime + "[ms]\n");
                        processList1.RemoveAt(processList1.FindIndex(x => x.Name == processList1[0].Name ));
                        allSorted--;
                    }                  
                }      
            }
            CalculateAverage(result, time, "SJF - zamknięta pula");
        }

        void SJFO() 
        {
            List<Process> processList1 = new List<Process>();
            List<Process> result = new List<Process>();
            Process p1 = new Process();
            bool decision = true;
            processList1 = processList.OrderBy(x => x.ExecuteTime).ToList();
            processList1 = processList1.OrderBy(x => x.ArrivalTime).ToList();
            int allSorted = processList1.Count() - 1;
            p1.Name = processList1[0].Name;
            p1.ArrivalTime = processList1[0].ArrivalTime;
            p1.ExecuteTime = processList1[0].ExecuteTime;
            p1.WaitTime = 0;
            p1.ServiceTime = processList1[0].ExecuteTime;
            result.Add(p1);
            int time = result[0].ArrivalTime + result[0].ExecuteTime;
            
            ClearChart(chart1);
            chart1.Name = "wykres Gantta";
            chart1.ChartAreas[0].AxisY.Title = "czas [ms]";
            chart1.ChartAreas[0].AxisX.Title = "nr procesu";
            chart1.Series["s1"].Points.AddXY(p1.Name, p1.ArrivalTime, time);
            txt1.AppendText(result[0].Name + "  czas oczekiwania : " + result[0].WaitTime + "[ms] czas przetwarzania: " + result[0].ServiceTime + "[ms]\n");
            processList1.RemoveAt(0);
            change = Interaction.InputBox("Czy dodac nowy proces? (wpisz 'tak', aby dodać lub 'nie' aby zamknąć pulę procesów) ", " ", "", -1, -1);
            while(allSorted != 0)
            {
                if(decision)
                {
                    if(change == "tak")
                    {
                        Process p = new Process();
                        p.Name = Convert.ToString(processList.Count() + 1);
                        p.ArrivalTime = Convert.ToInt32(Interaction.InputBox(@"Podaj czas przyjścia procesu", "Dodawanie nowego prcesu.", "", -1, -1));
                        p.ExecuteTime = Convert.ToInt32(Interaction.InputBox("Podaj czas wykonywania procesu: ", "Dodawanie nowego prcesu.", "", -1, -1));
                        processList1.Add(p);
                        change = Interaction.InputBox("Czy dodac nowy proces? ", " ", "", -1, -1);
                        allSorted++;
                    }
                    else if(change == "nie")
                    {
                        MessageBox.Show("Nie będzie już możliwości dodawania kolejnych procesów");
                        decision = false;
                    }
                }
                for(int i = 0; i < processList1.Count(); i++)
                {
                    if(processList1.Any(x => x.ArrivalTime <= time))
                    {
                        List<Process> temporalList = new List<Process>();
                        foreach (Process item in processList1)
                        {
                            if(item.ArrivalTime <= time)
                                temporalList.Add(item);                           
                        }
                        temporalList = temporalList.OrderBy(x => x.ExecuteTime).ToList();
                        Process p = new Process();
                        p.Name = temporalList[0].Name;
                        p.ArrivalTime = temporalList[0].ArrivalTime;
                        p.ExecuteTime = temporalList[0].ExecuteTime;
                        p.WaitTime = time - p.ArrivalTime;
                        p.ServiceTime = p.WaitTime + p.ExecuteTime;
                        result.Add(p);
                        time += p.ExecuteTime;
                        txt1.AppendText(p.Name + "  czas oczekiwania : " + p.WaitTime + "[ms] czas przetwarzania: " + p.ServiceTime + "[ms]\n");
                        processList1.RemoveAt(processList1.FindIndex(x => x.Name == p.Name));
                        chart1.Series["s1"].Points.AddXY(p.Name, time - p.ExecuteTime, time);
                        allSorted--;
                    }
                    else
                    {
                        processList1 = processList1.OrderBy(x => x.ExecuteTime).ToList();
                        processList1 = processList1.OrderBy(x => x.ArrivalTime).ToList();
                        processList1[0].WaitTime = 0;
                        processList1[0].ServiceTime = processList1[0].ExecuteTime;
                        result.Add(processList1[0]);
                        time = processList1[0].ArrivalTime + processList1[0].ExecuteTime;
                        txt1.AppendText(processList1[0].Name + "  czas oczekiwania : " + processList1[0].WaitTime + "[ms] czas przetwarzania: " + processList1[0].ServiceTime + "[ms]\n");
                        chart1.Series["s1"].Points.AddXY(processList1[0].Name, time - processList1[0].ExecuteTime, time);
                        processList1.RemoveAt(0);
                        allSorted--;
                    }
                }
            }
            CalculateAverage(result, time, "SJF - otwarta pula");
        }
        void CalculateAverage(List<Process> result, int time , string str) 
        {
            txt1.AppendText("Calkowity czas, do którego wykonały się wszystkie procesy:" + time + "[ms]\n");
            float sumServiceTime = 0f;
            float sumWaitTime = 0f;
            foreach (Process item in result)
            {
                sumServiceTime += item.ServiceTime;
                sumWaitTime += item.WaitTime;
            }
            float averageServiceTime = sumServiceTime / result.Count;
            float averageWaitTime = sumWaitTime / result.Count;
            txt1.AppendText("Sredni czas przetwarzania : " + averageServiceTime + "[ms]\r\nSredni czas oczekiwania : " + averageWaitTime + "[ms]\r\n");
            if(checkBox1.Checked)
                WriteProcesses(result, time, averageServiceTime, averageWaitTime, str);
        }
        void ClearChart(System.Windows.Forms.DataVisualization.Charting.Chart chart ) 
        {
            foreach (var series in chart.Series)
                series.Points.Clear();       
        }
        void btnLoad_Click(object sender, EventArgs e)
        {
            ReadProcesses();
            foreach (Process p in processList)
                txt1.AppendText(p.Name + " czas przyjscia: " + p.ArrivalTime + "[ms] czas procesora: " + p.ExecuteTime + "[ms]\n");   
            btnLoad.Enabled = false;
            btnFCFSO.Enabled = true;
            btnSJFO.Enabled = true;
            btnFCFSZ.Enabled = true;
            btnSJFZ.Enabled = true;
        }
        
        void btnFCFSZ_Click(object sender, EventArgs e)
        {
            FCFSZ();
        }

        void btnFCFSO_Click(object sender, EventArgs e)
        {
            FCFSO();
        }

        void btnSJFZ_Click(object sender, EventArgs e)
        {
            SJFZ();
        }

        void btnSJFO_Click(object sender, EventArgs e)
        {
            SJFO();
        }

        void Form1_Load(object sender, EventArgs e)
        {

        }
        
        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}