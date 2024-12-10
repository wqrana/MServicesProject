using Microsoft.EntityFrameworkCore;
using MSCustomerAPI.Model;

namespace MSCustomerAPI.DAL
{
    public class CustomerRespository : IGenericRespository<Customer>
    {
        private CustomerDBContext dbContext;

       public CustomerRespository(CustomerDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Customer Add(Customer entity)
        {
            try
            {
               dbContext.Add<Customer>(entity);
               var res= dbContext.SaveChangesAsync();
                res.Wait();
                if (res.Result > 0)
                    return entity;
                else
                    throw new Exception("Something went wrong while saving data");

            }
            catch (Exception ex)
            {
                throw ex;      
            }
                
        }

        public int Delete(int id)
        {
           var customer = dbContext.Find<Customer>(id);
           dbContext.Remove<Customer>(customer);
           var ret = dbContext.SaveChangesAsync();
           ret.Wait();
           return ret.Result;
        }

        public Customer GetById(int id)
        {
           var entityTask = dbContext.FindAsync<Customer>(id);
            if (entityTask.Result != null) 
            { 
                return entityTask.Result;
            }
            else
            {
                return null;
            }
           
        }

        public IEnumerable<Customer> GetAll()
        {
           var customersTask =  dbContext.Customer.ToListAsync<Customer>();
            if (customersTask.Result != null)
            {
                return customersTask.Result;
            }
            return null;
        }

        public int Update(Customer entity)
        {
           dbContext.Update<Customer>(entity);
           var res = dbContext.SaveChangesAsync();
           res.Wait();
           return res.Result;
        }

    }
}
