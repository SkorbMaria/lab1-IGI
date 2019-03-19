using System;
using System.Collections.Generic;
using System.Text;

namespace IGI_1
{
    public class Autors
    {
        public int ID { get; set; }
        public string About { get; set; }
        public override string ToString()
        {
            return $"{ID}: {About}";
        }
    }
}
