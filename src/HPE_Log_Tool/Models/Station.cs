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
            this.Id = id;
            this.Code = code;
            this.name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
    }
}
