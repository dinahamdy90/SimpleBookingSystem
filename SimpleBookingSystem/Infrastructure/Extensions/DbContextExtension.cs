using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Infrastructure.ErrorCodes;
using System.Net;
using System.Text.Json;

namespace SimpleBookingSystem.Infrastructure.Extensions
{
    public static class DbContextExtension
    {
        public static async Task<T> PostAsync<T>(this DbContext context, T entity) where T : class
        {
            if (entity != null)
            {
                context.Add(entity);
                var result = await context.SaveChangesAsync();
                if (result <= 0)
                {
                    throw new ApiException(HttpStatusCode.InternalServerError, DatabaseErrorCodes.DbUpdateException.ToString());
                }
                return entity;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, DatabaseErrorCodes.EntityNullException.ToString());
        }
        public static async Task<IEnumerable<T>> PostRangeAsync<T>(this DbContext context, IEnumerable<T> entities) where T : class
        {
            if (entities != null)
            {
                context.AddRange(entities);
                var result = await context.SaveChangesAsync();
                if (result <= 0)
                {
                        throw new ApiException(HttpStatusCode.InternalServerError, DatabaseErrorCodes.DbUpdateException.ToString());
                }
                return entities;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, DatabaseErrorCodes.EntityNullException.ToString());
        }
        public static async Task<T> PutAsync<T>(this DbContext context, T entity) where T : class
        {
            if (entity != null)
            {
                context.Attach(entity);
                context.Update(entity);
                var result = await context.SaveChangesAsync();
                if (result <= 0)
                {
                        throw new ApiException(HttpStatusCode.InternalServerError, DatabaseErrorCodes.DbUpdateException.ToString());
                }
                return entity;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, DatabaseErrorCodes.EntityNullException.ToString());

        }

        public static async Task<IEnumerable<T>> PutRangeAsync<T>(this DbContext context, IEnumerable<T> entities) where T : class
        {
            if (entities != null)
            {
                context.AttachRange(entities);
                context.UpdateRange(entities);
                var result = await context.SaveChangesAsync();
                if (result <= 0)
                {
                        throw new ApiException(HttpStatusCode.InternalServerError, DatabaseErrorCodes.DbUpdateException.ToString());
                }
                return entities;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, DatabaseErrorCodes.EntityNullException.ToString());

        }
        public static async Task<int> DeleteByEntityAsync<T>(this DbContext context, T entity) where T : class
        {
            if (entity != null)
            {
                context.Remove(entity);
                var result = await context.SaveChangesAsync();
                if (result <= 0)
                {
                    throw new ApiException(HttpStatusCode.InternalServerError, DatabaseErrorCodes.DbUpdateException.ToString());
                }
                return result;
            }
            else
                throw new ApiException(HttpStatusCode.InternalServerError, DatabaseErrorCodes.EntityNullException.ToString());

        }

    }
}