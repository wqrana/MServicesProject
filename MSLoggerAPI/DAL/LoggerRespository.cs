using Microsoft.EntityFrameworkCore;
using MSLoggerAPI.Model;

namespace MSLoggerAPI.DAL
{
    public class LoggerRespository : IGenericRespository<AppLogger>
    {
        private LoggerDBContext dbContext;

       public LoggerRespository(LoggerDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public AppLogger Add(AppLogger entity)
        {
            try
            {
               dbContext.Add<AppLogger>(entity);
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
           var AppLogger = dbContext.Find<AppLogger>(id);
           dbContext.Remove<AppLogger>(AppLogger);
           var ret = dbContext.SaveChangesAsync();
           ret.Wait();
           return ret.Result;
        }

        public AppLogger GetById(int id)
        {
           var entityTask = dbContext.FindAsync<AppLogger>(id);
            if (entityTask.Result != null) 
            { 
                return entityTask.Result;
            }
            else
            {
                return null;
            }
           
        }

        public IEnumerable<AppLogger> GetAll()
        {
           var appLoggersTask =  dbContext.AppLogger.ToListAsync<AppLogger>();
            if (appLoggersTask.Result != null)
            {
                return appLoggersTask.Result;
            }
            return null;
        }

        public int Update(AppLogger entity)
        {
           dbContext.Update<AppLogger>(entity);
           var res = dbContext.SaveChangesAsync();
           res.Wait();
           return res.Result;
        }

    }
}
