using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyStoreDesktop.Services.QrTableDataService
{
    public class QrTableDataService : IQrTableDataService
    {
        private readonly DatabaseHelper _context;

        public QrTableDataService()
        {
            _context = new DatabaseHelper();
        }

        public IEnumerable<QrTableData> GetAll()
        {
            return _context.QrTableDatas
                .OrderByDescending(q => q.CreatedAt)
                .ToList();
        }

        public QrTableData GetById(int id)
        {
            return _context.QrTableDatas.FirstOrDefault(q => q.Id == id);
        }

        public void Add(QrTableData qrData)
        {
            _context.QrTableDatas.Add(qrData);
            _context.SaveChanges();
        }

        public void Update(QrTableData qrData)
        {
            var existing = _context.QrTableDatas.Find(qrData.Id);
            if (existing != null)
            {
                existing.ProductId = qrData.ProductId;
                existing.QrCode = qrData.QrCode;
                existing.CreatedAt = qrData.CreatedAt;

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var qrData = _context.QrTableDatas.Find(id);
            if (qrData != null)
            {
                _context.QrTableDatas.Remove(qrData);
                _context.SaveChanges();
            }
        }
    }
}
