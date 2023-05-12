using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UNGLUONG
    {
        QuanlynhansuEntities db = new QuanlynhansuEntities();
        public tb_UNGLUONG getItem(int idul)
        {
            return db.tb_UNGLUONG.FirstOrDefault(x => x.ID == idul);
        }
        public List<tb_UNGLUONG> getList()
        {
            return db.tb_UNGLUONG.ToList();
        }
        public List<UNGLUONG_DTO> getListFull()
        {
            var lstUngLuong = db.tb_UNGLUONG.ToList();
            List<UNGLUONG_DTO> lstDTO = new List<UNGLUONG_DTO>();
            UNGLUONG_DTO ul;
            foreach (var item in lstUngLuong)
            {
                ul = new UNGLUONG_DTO();
                ul.ID = item.ID;
                ul.NGAY = item.NGAY;
                ul.THANG = item.THANG;
                ul.NAM = item.NAM;
                ul.MANV = item.MANV;
                var nv = db.tb_NHANVIEN.FirstOrDefault(x => x.MANV==item.MANV);
                ul.HOTEN = nv.HOTEN;
                ul.SOTIEN = item.SOTIEN;
                ul.GHICHU = item.GHICHU;
                ul.CREATED_BY = item.CREATED_BY;
                ul.CREATED_DATE = item.CREATED_DATE;
                ul.UPDATED_BY = item.UPDATED_BY;
                ul.UPDATED_DATE = item.UPDATED_DATE;
                ul.DELETED_BY = item.DELETED_BY;
                ul.DELETED_DATE = item.DELETED_DATE;
                lstDTO.Add(ul);
            }
            return lstDTO;
        }
        public tb_UNGLUONG Add(tb_UNGLUONG ul)
        {
            try
            {
                db.tb_UNGLUONG.Add(ul);
                db.SaveChanges();
                return ul;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public tb_UNGLUONG Update(tb_UNGLUONG ul)
        {
            try
            {
                var _ul = db.tb_UNGLUONG.FirstOrDefault(x => x.ID == ul.ID);
                _ul.NGAY = ul.NGAY;
                _ul.THANG = ul.THANG;
                _ul.NAM = ul.NAM;
                _ul.MANV = ul.MANV;
                _ul.SOTIEN = ul.SOTIEN;
                _ul.GHICHU = ul.GHICHU;
                _ul.UPDATED_BY = ul.UPDATED_BY;
                _ul.UPDATED_DATE = ul.UPDATED_DATE;
                db.SaveChanges();
                return ul;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public void Delete(int idul, int iduser)
        {

            try
            {
                var _ul = db.tb_UNGLUONG.FirstOrDefault(x => x.ID == idul);
                _ul.DELETED_BY = iduser;
                _ul.DELETED_DATE = DateTime.Now;
                db.tb_UNGLUONG.Remove(_ul);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
    }
}
