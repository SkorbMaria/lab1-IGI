using System;
using System.Collections.Generic;
using System.Text;

namespace IGI_1
{
    public class Books
    {
        public int ID { get; set; }
        public string Cypher { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int Price { get; set; }
        public int Sell { get; set; }
        public int Fee { get; set; }
        public override string ToString()
        {
            return $"{ID}: {Cypher} {Name} {Sell}";
        }
    }
}
