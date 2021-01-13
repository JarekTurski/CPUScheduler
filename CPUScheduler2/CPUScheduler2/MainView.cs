using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CPUSchedulerPart2     
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
        }
        int timeSlice = 5;
        List<Process> processList = new List<Process>();
        void ReadProcesses()
        {
            using (StreamReader sr = new StreamReader(@"..\..\..\ProcessListP2.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] s = line.Split(null);
                    var p = new Process();
                    p.Name = s[0];
                    p.ArrivalTime = Convert.ToInt32(s[1]);
                    p.ExecuteTime = Convert.ToInt32(s[2]);
                    p.Priority = Convert.ToInt32(s[3]);
                    processList.Add(p);
                }
                sr.Close();
            }
        }
        void WriteProcesses(List<Process> list, int time, float avgServT, float avgWaitT)
        {
            using (StreamWriter sw = new StreamWriter((@"..\..\..\Outputdata.txt"), true))
            {
                sw.WriteLine("Dane wyjściowe dla szeregowania priorytetowego ");
                foreach (Process p in list)
                {
                    sw.WriteLine("Proces nr {0}: czas oczekiwania: {1}[ms] czas przetwarzania: {2}[ms] priorytet: {3} ", p.Name, p.WaitTime, p.ServiceTime, p.Priority);
                }
                sw.WriteLine("Czas do którego wszystkie procesy się wykonały: {0}[ms]\r\nśredni czas przetwarzania: {1}[ms]\r\nśredni czas oczekiwania: {2}[ms]\r\n", time, avgServT, avgWaitT);
            }
        }
        void PP()   
        {
            List<Process> processList1 = new List<Process>();
            processList1 = processList.OrderBy(x => x.Priority).ToList();
            processList1 = processList1.OrderBy(x => x.ArrivalTime).ToList();
            List<Process> result = new List<Process>();
            Process p1 = new Process
            {
                Name = processList1[0].Name,
                ArrivalTime = processList1[0].ArrivalTime,
                ExecuteTime = processList1[0].ExecuteTime,
                WaitTime = 0,
                ServiceTime = processList1[0].ExecuteTime,
                Priority = processList1[0].Priority
            };
            result.Add(p1);
            int time = result[0].ArrivalTime + result[0].ExecuteTime;
            int allSorted = processList1.Count() - 1;
            ClearChart(chart1);
            chart1.Name = "wykres Gantta";
            chart1.ChartAreas[0].AxisY.Title = "czas [ms]";
            chart1.ChartAreas[0].AxisX.Title = "nr procesu";
            chart1.Series["s1"].Points.AddXY(p1.Name, p1.ArrivalTime, time);
            textBox1.AppendText(result[0].Name + "  czas oczekiwania : " + result[0].WaitTime + "[ms] czas przetwarzania: " + result[0].ServiceTime + "[ms] priorytet: " + result[0].Priority+"\r\n");
            processList1.RemoveAt(0);
            List<Process> temporalList = new List<Process>();
            while (allSorted != 0)
            {
                for (int i = 0; i < processList1.Count(); i++)
                {
                    if (processList1.Any(x => x.ArrivalTime <= time))
                    {
                        foreach (Process item in processList1)
                        {
                            if (item.ArrivalTime <= time)
                                temporalList.Add(item);
                        }
                        foreach (Process item in temporalList)
                        {
                            int index = processList1.FindIndex(x => x.Name == item.Name);
                            if (processList1[index].ArrivalTime >= time - result[result.Count()-1].ExecuteTime)
                            {
                                
                                int q = (int)((time - processList1[index].ArrivalTime) / timeSlice);
                                if (processList1[index].Priority - q > 0)
                                   processList1[index].Priority -= q;
                                else
                                    processList1[index].Priority = 0;

                                processList1[index].WaitTime += time - processList1[index].ArrivalTime - timeSlice*q;
                            }
                            else
                            {
                                processList1[index].WaitTime += result[result.Count() - 1].ExecuteTime;
                                int q = (int)(processList1[index].WaitTime / timeSlice);
                                if (processList1[index].Priority - q > 0)
                                    processList1[index].Priority -= q;
                                else
                                    processList1[index].Priority = 0;
                                
                                processList1[index].WaitTime -= timeSlice*q;
                            }
                            item.Priority = processList1[index].Priority;
                            item.WaitTime = processList1[index].WaitTime;
                        }
                        temporalList = temporalList.OrderBy(x => x.ArrivalTime).ToList();
                        temporalList = temporalList.OrderBy(x => x.Priority).ToList();
                        Process p = new Process();
                        p.Name = temporalList[0].Name;
                        p.ArrivalTime = temporalList[0].ArrivalTime;
                        p.ExecuteTime = temporalList[0].ExecuteTime;
                        p.WaitTime = time - p.ArrivalTime;
                        p.ServiceTime = p.WaitTime + p.ExecuteTime;
                        p.Priority = temporalList[0].Priority;
                        result.Add(p);
                        time += p.ExecuteTime;
                        textBox1.AppendText(p.Name + "  czas oczekiwania : " + p.WaitTime + "[ms] czas przetwarzania: " + p.ServiceTime + "[ms] priorytet: " + p.Priority +"\r\n");
                        processList1.RemoveAt(processList1.FindIndex(x => x.Name == p.Name));
                        chart1.Series["s1"].Points.AddXY(p.Name, time - p.ExecuteTime, time);
                        allSorted--;
                    }
                    else
                    {
                        processList1 = processList1.OrderBy(x => x.Priority).ToList();
                        processList1 = processList1.OrderBy(x => x.ArrivalTime).ToList();
                        processList1[0].WaitTime = 0;
                        processList1[0].ServiceTime = processList1[0].ExecuteTime;
                        result.Add(processList1[0]);
                        time = processList1[0].ArrivalTime + processList1[0].ExecuteTime;
                        chart1.Series["s1"].Points.AddXY(processList1[0].Name, time - processList1[0].ExecuteTime, time);
                        textBox1.AppendText(processList1[0].Name + "  czas oczekiwania : " + processList1[0].WaitTime + "[ms] czas przetwarzania: " + processList1[0].ServiceTime + "[ms] priorytet: " + processList1[0].Priority+"\r\n");
                        processList1.RemoveAt(processList1.FindIndex(x => x.Name == processList1[0].Name));
                        allSorted--;
                    }
                    temporalList.Clear();
                }
            }
            CalculateAverage(result, time);
            processList.Clear();
            ReadProcesses();
        }
        void CalculateAverage( List<Process> result , int t)
        {
            textBox1.AppendText("Calkowity czas wykonania wszystkich procesow:" + t + "[ms]\n");
            float sumServiceTime = 0f;
            float sumWaitTime = 0f;
            foreach (Process item in result)
            {
                sumServiceTime += item.ServiceTime;
                sumWaitTime += item.WaitTime;
            }
            float averageServiceTime = sumServiceTime / result.Count;
            float averageWaitTime = sumWaitTime / result.Count;
            textBox1.AppendText("Sredni czas przetwarzania : " + averageServiceTime + "[ms]\r\nSredni czas oczekiwania : " + averageWaitTime + "[ms]\r\n");
            if (checkBox1.Checked)
                WriteProcesses(result, t, averageServiceTime, averageWaitTime);
        }
        void ClearChart(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }
        }

        void btnLoad_Click(object sender, EventArgs e)
        {
            ReadProcesses();
            foreach (Process p in processList)
            {
                textBox1.AppendText(p.Name + " czas przyjscia: " + p.ArrivalTime + "[ms] czas procesora: " + p.ExecuteTime + "[ms] priorytet: " + p.Priority +"\r\n");
            }
            btnLoad.Enabled = false;
        }

        void btnPriorityAlg_Click(object sender, EventArgs e)
        {
            PP();
        }
    }
}
