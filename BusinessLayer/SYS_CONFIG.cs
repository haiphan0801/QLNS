using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_CONFIG
    {
        QuanlynhansuEntities db = new QuanlynhansuEntities();
        public tb_CONFIG getItem(string name)
        {
            return db.tb_CONFIG.FirstOrDefault(x => x.NAME==name);
        }
    }
}
