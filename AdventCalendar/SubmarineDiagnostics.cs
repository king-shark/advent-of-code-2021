using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventCalendar
{
    public class SubmarineDiagnostics
    {
        public List<string> submarineData = new List<string>();
        public Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
        static string gammaStr = "", epsilonStr = "";
        public SubmarineDiagnostics(string filename)
        {
            string line;
            StreamReader reader = File.OpenText(filename);
            while ((line = reader.ReadLine()) != null)
            {
                submarineData.Add(line);
            }
        }
        public int PowerConsumption()
        {
            foreach (var item in submarineData)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (keyValuePairs.ContainsKey(i))
                    {
                        keyValuePairs[i] = keyValuePairs[i] + Int32.Parse(item.Substring(i, 1));
                    }
                    else { keyValuePairs.Add(i, Int32.Parse(item.Substring(i, 1))); }
                }
            }
            for (int i = 0; i < keyValuePairs.Count; i++)
            {
                //Debug.Print("Position {0} Count: {1}", i, keyValuePairs[i]);
                if (keyValuePairs[i] > submarineData.Count / 2) { gammaStr = gammaStr + "1"; epsilonStr = epsilonStr + "0"; }
                else { gammaStr = gammaStr + "0"; epsilonStr = epsilonStr + "1"; }
            }
            Debug.Print("Gamma: {0} Epsilon: {1}", gammaStr, epsilonStr);
            Debug.Print("Gamma Decimal: {0} Epsilon Decimal: {1}", Convert.ToInt32(gammaStr, 2), Convert.ToInt32(epsilonStr, 2));
            return Convert.ToInt32(gammaStr, 2) * Convert.ToInt32(epsilonStr, 2);
        }
        public int LifeSupportRating()
        {
            string oxygen = GetOxygenString(submarineData);
            string co2 = GetCo2String(submarineData);

            Debug.Print("O2: {0} CO2: {1}", oxygen, co2);
            Debug.Print("O2 Decimal: {0} Co2 Decimal: {1}", Convert.ToInt32(oxygen, 2), Convert.ToInt32(co2, 2));
            return Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2);
        }
        public string GetOxygenString(List<string> oxygenList)
        {
            int i = 0;
            do
            {
                List<string> list1 = oxygenList.Where(o => o.Substring(i, 1).Equals("1")).ToList();
                List<string> list2 = oxygenList.Where(o => o.Substring(i, 1).Equals("0")).ToList();
                if (list1.Count >= list2.Count) { oxygenList = list1; }
                else { oxygenList = list2; }
                i++;
            } while (oxygenList.Count > 1 && i < oxygenList[0].Length);

            return oxygenList[0];
        }
        public string GetCo2String(List<string> co2List)
        {
            int i = 0;
            do
            {
                List<string> list1 = co2List.Where(o => o.Substring(i, 1).Equals("0")).ToList();
                List<string> list2 = co2List.Where(o => o.Substring(i, 1).Equals("1")).ToList();
                if (list1.Count <= list2.Count) { co2List = list1; }
                else { co2List = list2; }
                i++;
            } while (co2List.Count > 1 && i < co2List[0].Length);
            return co2List[0];
        }
    }
}
