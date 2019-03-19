using System;
using System.Collections.Generic;
using System.Text;

namespace IGI_1
{
    public class Orders
    {
        public int ID { get; set; }
        public Books Book { get; set; }
        public Customers Customer{ get; set; }
        public string RecieveDate { get; set; }
        public string CompleteDate { get; set; }
        public int Count { get; set; }
    }
}
