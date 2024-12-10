using Microsoft.EntityFrameworkCore;
using MSSalesAPI.Model;
using System.Linq;

namespace MSSalesAPI.DAL
{
    public class POSalesRespository : IPOSalesRespository<POSales, POSalesDetail>
    {
        private readonly POSalesDBContext dbContext;

        public POSalesRespository(POSalesDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public POSales Add(POSales entity)
        {
            try
            {
                var existingEntity = dbContext.POSaless.Where(s=>s.InvoiceNo==entity.InvoiceNo).FirstOrDefault();
                if (existingEntity == null)
                {
                    dbContext.POSaless.Add(entity);
                    var res = dbContext.SaveChangesAsync();
                    res.Wait();
                    if (res.Result > 0)
                        return entity;
                    else
                        throw new Exception("Something went wrong while saving POSales data");
                }
                else
                {
                    throw new Exception("POSales record is already exists with the same Invoice No");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public POSalesDetail AddPOSalesDetail(POSalesDetail entity)
        {
            try
            {
                var existingEntity = dbContext.POSalesDetails.Where(s => s.ProductId == entity.ProductId && s.POSalesId == entity.POSalesId).FirstOrDefault();
                if (existingEntity == null)
                {
                    dbContext.POSalesDetails.Add(entity);
                    var res = dbContext.SaveChangesAsync();
                    res.Wait();
                    if (res.Result > 0)
                        return entity;
                    else
                        throw new Exception("Something went wrong while saving POSales Detail data");
                }
                else
                {
                    throw new Exception("POSales detail record is already exists with the same Product");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public POSalesDetail AddSalesDetail(POSalesDetail entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            try
            {
                var pOSales = dbContext.Find<POSales>(id);
                dbContext.Remove<POSales>(pOSales);
                var ret = dbContext.SaveChangesAsync();
                ret.Wait();
                return ret.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteSalesDetail(int id)
        {
            try
            {
                var pOSalesDetail = dbContext.Find<POSalesDetail>(id);
                dbContext.Remove<POSalesDetail>(pOSalesDetail);
                var ret = dbContext.SaveChangesAsync();
                ret.Wait();
                return ret.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<POSales> GetAll()
        {
            try
            {
                var allPOSalesData = dbContext.POSaless.Include(x => x.POSalesDetails).ToListAsync();
                allPOSalesData.Wait();
                return allPOSalesData.Result.ToList<POSales>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<POSalesDetail> GetAllDetailsBySalesId(int id)
        {
            try
            {
                var allPOSalesDetailData = dbContext.POSalesDetails.Where(x => x.POSalesId == id).ToListAsync();
                allPOSalesDetailData.Wait();
                return allPOSalesDetailData.Result.ToList<POSalesDetail>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public POSales GetById(int id)
        {
            try
            {
                var pOSalesById = dbContext.POSaless.Where(w => w.Id == id).Include(x => x.POSalesDetails).FirstOrDefaultAsync();
                pOSalesById.Wait();
                return pOSalesById.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public POSalesDetail GetSalesDetailById(int id)
        {
            try
            {
                var pOSalesDetailById = dbContext.POSalesDetails.Where(w => w.Id == id).FirstOrDefaultAsync();
                pOSalesDetailById.Wait();
                return pOSalesDetailById.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(POSales entity)
        {
            try
            {
                var existingEntity = dbContext.POSaless.Where(x => x.InvoiceNo == entity.InvoiceNo && x.Id != entity.Id).FirstOrDefault();
                if (existingEntity == null)
                {
                    dbContext.Update<POSales>(entity);
                    var res = dbContext.SaveChangesAsync();
                    res.Wait();
                    return res.Result;
                }
                else
                {
                    throw new Exception("POSales record is already exists with same Invoice No");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateSalesDetail(POSalesDetail entity)
        {
            try
            {
                var existingEntity = dbContext.POSalesDetails.Where(x => x.POSalesId == entity.POSalesId && x.ProductId == entity.ProductId && x.Id != entity.Id).FirstOrDefault();
                if (existingEntity == null)
                {
                    dbContext.Update<POSalesDetail>(entity);
                    var res = dbContext.SaveChangesAsync();
                    res.Wait();
                    return res.Result;
                }
                else
                {
                    throw new Exception("POSales Detail record is already exists with same Product");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
