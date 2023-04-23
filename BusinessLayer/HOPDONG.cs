using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class HOPDONG
    {
        QuanlynhansuEntities db = new QuanlynhansuEntities();
        public tb_HOPDONG getItem(string sohd)
        {
            return db.tb_HOPDONG.FirstOrDefault(x => x.SOHD == sohd);
        }
        public List<tb_HOPDONG> getList()
        {
            return db.tb_HOPDONG.ToList();
        }

        public List<HOPDONG_DTO> getListFull()
        {
            List<tb_HOPDONG> lstHD = db.tb_HOPDONG.ToList();
            List<HOPDONG_DTO> lstHDDTO = new List<HOPDONG_DTO>();
            HOPDONG_DTO hdDTO;
            foreach (var item in lstHD)
            {
                hdDTO = new HOPDONG_DTO();
                hdDTO.SOHD = item.SOHD;
                hdDTO.NGAYBATDAU = item.NGAYBATDAU;
                hdDTO.NGAYKETTHUC = item.NGAYKETTHUC;
                hdDTO.NGAYKY = item.NGAYKY;
                hdDTO.LANKY = item.LANKY;
                hdDTO.HESOLUONG = item.HESOLUONG;
                hdDTO.NOIDUNG = item.NOIDUNG;
                hdDTO.MANV = item.MANV;
                hdDTO.THOIHAN = item.THOIHAN;
                var nv = db.tb_NHANVIEN.FirstOrDefault(n => n.MANV == item.MANV);
                hdDTO.HOTEN = nv.HOTEN;
                hdDTO.CCCD = nv.CCCD;
                hdDTO.CREATED_BY = item.CREATED_BY;
                hdDTO.CREATED_DATE = item.CREATED_DATE;
                hdDTO.UPDATED_BY = item.UPDATED_BY;
                hdDTO.UPDATED_DATE = item.UPDATED_DATE;
                hdDTO.DELETED_BY = item.DELETED_BY;
                hdDTO.DELETED_DATE = item.DELETED_DATE;
                hdDTO.MACTY = item.MACTY;
               
                lstHDDTO.Add(hdDTO);

            }
            return lstHDDTO;

        }

        public tb_HOPDONG Add(tb_HOPDONG hd)
        {
            try
            {
                db.tb_HOPDONG.Add(hd);
                db.SaveChanges();
                return hd;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public tb_HOPDONG Update(tb_HOPDONG hd)
        {
            try
            {
                var _hd = db.tb_HOPDONG.FirstOrDefault(x => x.SOHD == hd.SOHD);
                _hd.NGAYBATDAU = hd.NGAYBATDAU;
                _hd.NGAYKETTHUC = hd.NGAYKETTHUC;
                _hd.NGAYKY = hd.NGAYKY;
                _hd.LANKY = hd.LANKY;
                _hd.HESOLUONG = hd.HESOLUONG;
                _hd.MANV = hd.MANV;
                _hd.NOIDUNG = hd.NOIDUNG;
                _hd.THOIHAN = hd.THOIHAN;
                _hd.MACTY = hd.MACTY;
                _hd.UPDATED_BY = hd.UPDATED_BY;
                _hd.UPDATED_DATE = hd.UPDATED_DATE;

                db.SaveChanges();
                return hd;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public void Delete(string sohd, int manv)
        {

            try
            {
                var _hd = db.tb_HOPDONG.FirstOrDefault(x => x.SOHD == sohd);
                _hd.DELETED_BY = manv;
                _hd.DELETED_DATE =DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        public string MaSoHopDong()
        {
            var _hd = db.tb_HOPDONG.OrderByDescending(x => x.CREATED_DATE).FirstOrDefault();
            if (_hd != null)
            {
                return _hd.SOHD;
            }
            else
                return "00000";
        }
    }
}
