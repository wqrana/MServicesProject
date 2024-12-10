using Microsoft.EntityFrameworkCore;
using MSProductAPI.Model;
using System.Linq;

namespace MSProductAPI.DAL
{
    public class ProductRateRespository : IProductRateRespository<ProductRate>
    {
        private readonly ProductDBContext dbContext;

        public ProductRateRespository(ProductDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ProductRate Add(ProductRate entity)
        {
            try
            {
                var existingEntity = dbContext.ProductRates.Where(x => x.ProductId == entity.ProductId && x.EffectiveDate==entity.EffectiveDate).FirstOrDefault();
                if (existingEntity == null)
                {
                    dbContext.ProductRates.Add(entity);
                    var res = dbContext.SaveChangesAsync();
                    res.Wait();
                    if (res.Result > 0)
                        return entity;
                    else
                        throw new Exception("Something went wrong while saving Product Effective Rate data");
                }
                else
                {
                    throw new Exception("Product Rate is already exists with same effective Date");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int id)
        {
            try
            {
                var productRate = dbContext.Find<ProductRate>(id);
                dbContext.Remove<ProductRate>(productRate);
                var ret = dbContext.SaveChangesAsync();
                ret.Wait();
                return ret.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProductRate> GetAll()
        {
            try
            {
                var allProdRateData = dbContext.ProductRates.Include(x=>x.Product).ToListAsync();
                allProdRateData.Wait();
                return allProdRateData.Result.ToList<ProductRate>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProductRate> GetAllEffectiveRate()
        {
            try
            {
                IList<ProductRate> effectiveProductRateRet = new List<ProductRate>();
                var allProdRateData = dbContext.ProductRates.Where(w => w.EffectiveDate <= DateTime.Today).OrderByDescending(o=>o.EffectiveDate).Include(x => x.Product).AsEnumerable<ProductRate>().GroupBy(g => g.ProductId);
                foreach(var productRates in allProdRateData)
                {
                    var effectiveRate = productRates.FirstOrDefault();                   
                    if (effectiveRate != null)
                    {
                        effectiveProductRateRet.Add(effectiveRate);
                    }
                }

               return effectiveProductRateRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProductRate> GetAllRateByProductId(int id)
        {
            try
            {
                var allRateByProductId = dbContext.ProductRates.Where(w=>w.ProductId==id).Include(x => x.Product).ToListAsync();
                allRateByProductId.Wait();
                return allRateByProductId.Result.ToList<ProductRate>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductRate GetById(int id)
        {
            try
            {
                var prodRateById = dbContext.ProductRates.Where(w=>w.Id==id).Include(x => x.Product).FirstOrDefaultAsync();
                prodRateById.Wait();
                return prodRateById.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductRate GetEffectiveRateByProductId(int id)
        {
            try
            {
                IList<ProductRate> effectiveProductRateRet = new List<ProductRate>();
                var prodAllRatesData = dbContext.ProductRates.Where(w=>w.ProductId==id && w.EffectiveDate<=DateTime.Today).Include(x => x.Product);
                var effRateByProductId= prodAllRatesData.OrderByDescending(o=>o.EffectiveDate).FirstOrDefaultAsync();

                effRateByProductId.Wait();                

                return effRateByProductId.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(ProductRate entity)
        {
            try
            {
                var existingEntity = dbContext.ProductRates.Where(x => x.ProductId == entity.ProductId && x.EffectiveDate == entity.EffectiveDate && x.Id!=entity.Id).FirstOrDefault();
                if (existingEntity == null)
                {
                    dbContext.Update<ProductRate>(entity);
                    var res = dbContext.SaveChangesAsync();
                    res.Wait();
                    return res.Result;
                }
                else
                {
                    throw new Exception("Product Rate is already exists with same effective Date");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
