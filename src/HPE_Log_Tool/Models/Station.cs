using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPE_Log_Tool.Models
{
    public class Station
    {
        int id;
        string code;
        string name;

        public Station(int id, string code, string name)
        {
            this.id = id;
            this.code = code;
            this.name = name;
        }
    }
}
