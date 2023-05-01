﻿using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class LOAICONG
    {
        QuanlynhansuEntities db = new QuanlynhansuEntities();
        public tb_LOAICONG getItem(int idloaica)
        {
            return db.tb_LOAICONG.FirstOrDefault(x => x.IDLC == idloaica);
        }
        public List<tb_LOAICONG> getList()
        {
            return db.tb_LOAICONG.ToList();
        }
        public tb_LOAICONG Add(tb_LOAICONG lc)
        {
            try
            {
                db.tb_LOAICONG.Add(lc);
                db.SaveChanges();
                return lc;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public tb_LOAICONG Update(tb_LOAICONG lc)
        {
            try
            {
                var _lc = db.tb_LOAICONG.FirstOrDefault(x => x.IDLC == lc.IDLC);
                _lc.TENLC = lc.TENLC;
                _lc.HESO = lc.HESO;
                _lc.UPDATED_BY = lc.UPDATED_BY;
                _lc.UPDATED_DATE = lc.UPDATED_DATE;
                db.SaveChanges();
                return lc;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public void Delete(int idloaicong, int iduser)
        {

            try
            {
                var _lc = db.tb_LOAICONG.FirstOrDefault(x => x.IDLC == idloaicong);
                _lc.DELETED_BY = iduser;
                _lc.DELETED_DATE = DateTime.Now;
                db.tb_LOAICONG.Remove(_lc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
    }
}
