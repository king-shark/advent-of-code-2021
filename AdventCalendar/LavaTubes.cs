//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net.Http.Headers;

//namespace AdventCalendar
//{
//    public class LavaTubes
//    {
//        List<List<Node>> Map { get; set; }
//        public LavaTubes(string filename)
//        {
//            string line;
//            Map = new List<List<Node>>();
//            StreamReader reader = File.OpenText(filename);
            
//            while ((line = reader.ReadLine()) != null)
//            {
//                string[] lineArray = line.Split('',);
//                List<Node> temp = new List<Node>();
//                foreach (string item in lineArray)
//                {
//                    temp.Add(new Node { })
//                    Map.Add()
//                }
//            }
//        }
//        public int GetLowPoints()
//        {
//            return 0;
//        }
//    }
//}
