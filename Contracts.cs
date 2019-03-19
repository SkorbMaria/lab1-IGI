using System;
using System.Collections.Generic;
using System.Text;

namespace IGI_1
{
    public class Contracts
    {
        public int ID { get; set; }
        public string DateStart { get; set; }
        public int Term { get; set; }
        public string DateEnd { get; set; }
        public Autors Autor { get; set; }
        public Books Book { get; set; }
        /*public string About { get; set; }
        public override string ToString()
        {
            return $"{ID}: {About}";
        }*/
    }
}
