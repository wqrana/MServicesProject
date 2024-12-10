using Microsoft.EntityFrameworkCore;
using MSProductAPI.Model;

namespace MSProductAPI.DAL
{
    public class ProductRespository : IGenericRespository<Product>
    {
        private readonly ProductDBContext dbContext;

        public ProductRespository(ProductDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Product Add(Product entity)
        {
            try
            {
                var existingEntity = dbContext.Products.Where(x => x.Code == entity.Code).FirstOrDefault();
                if (existingEntity == null)
                {
                    dbContext.Products.Add(entity);
                    var res = dbContext.SaveChangesAsync();
                    res.Wait();
                    if (res.Result > 0)
                        return entity;
                    else
                        throw new Exception("Something went wrong while saving Product data");
                }
                else
                {
                    throw new Exception("Product already exists for this data");
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
                var product = dbContext.Find<Product>(id);
                dbContext.Remove<Product>(product);
                var ret = dbContext.SaveChangesAsync();
                ret.Wait();
                return ret.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            try
            {
                var prodListData = dbContext.Products.ToListAsync();
                prodListData.Wait();
                return prodListData.Result.ToList<Product>();
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

        public Product GetById(int id)
        {
            try
            {
              var prodData = dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
              prodData.Wait();
              return prodData.Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Product entity)
        {
            try
            {
                dbContext.Update<Product>(entity);
                var res = dbContext.SaveChangesAsync();
                res.Wait();
                return res.Result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
