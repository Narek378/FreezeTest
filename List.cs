using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreezeTest
{
    class List
    {


        public static List<Datas> Datas = new List<Datas>();

        public static void InitialiseData()
        {
            foreach (string key in ConfigurationManager.AppSettings.Keys)
            {

                Datas.Add(new Datas
                {
                    PartnerName = key,
                    Url = ConfigurationManager.AppSettings[key]
                });



            }
        }


    }

}

