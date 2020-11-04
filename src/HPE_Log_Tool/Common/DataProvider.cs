using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPE_Log_Tool.Common
{
    public class DataProvider
    {
        public static List<LS_Shift> GetShifts(bool isAll = false)
        {
            List<LS_Shift> shiftList = new List<LS_Shift>();
            try
            {
                using (Model1 db = new Model1())
                {
                    shiftList = db.LS_Shift.ToList();
                    if (isAll)
                    {
                        LS_Shift shiftall = new LS_Shift();
                        shiftall.ShiftID = 0;
                        shiftall.Name = "All";
                        shiftList.Insert(0, shiftall);
                    }
                }
            }
            catch (Exception)
            {

            }
            return shiftList;

        }
    }
}
