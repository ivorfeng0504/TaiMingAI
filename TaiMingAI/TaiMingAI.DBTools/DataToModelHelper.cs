using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiMingAI.DataHelper
{
    public class DataToModelHelper
    {
        public static void DataSetToModel<T>(DataSet dataSet)
        {
            foreach (var item in dataSet.Tables)
            {

            }
            var t = typeof(T);
        }
    }
}
