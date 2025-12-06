using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return _context.QrTableDatas
                .FirstOrDefault(q => q.Id == id);
        }

        // ⭐ NEW: Get codes of a specific product
        public IEnumerable<QrTableData> GetByProduct(int productId)
        {
            return _context.QrTableDatas
                .Where(q => q.ProductId == productId)
                .OrderByDescending(q => q.CreatedAt)
                .ToList();
        }

        public void Add(QrTableData data)
        {
            _context.QrTableDatas.Add(data);
            _context.SaveChanges();
        }

        public void Update(QrTableData qrData)
        {
            var existing = _context.QrTableDatas.Find(qrData.Id);

            if (existing != null)
            {
                existing.ProductId = qrData.ProductId;
                existing.CodeValue = qrData.CodeValue;
                existing.CodeType = qrData.CodeType;
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
