﻿using Common.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Common.EfCoreDataAccess
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly EfCoreDbContext Context;
        private IDbContextTransaction Transaction;

        public EfCoreUnitOfWork(EfCoreDbContext context)
        {
            Context = context;
        }

        public async Task BeginTransactionAsync()
        {
            Transaction = await Context.Database.BeginTransactionAsync();
        }
        public Task CommitTransactionAsync()
        {
            return Transaction.CommitAsync();
        }
        public Task RollbackTransactionAsync()
        {
            return Transaction.RollbackAsync();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException duce)
            {
                //todo: log
                throw duce;
            }
            catch (DbUpdateException due)
            {
                throw due;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ClearTracker()
        {
            Context.ChangeTracker.Clear();
        }

        #region IDisposable implementation

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {

            if (disposedValue)
            {
                return;
            }
            if (disposing)
            {
                Transaction?.Dispose();
                Context.Dispose();
            }
            disposedValue = true;
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        #endregion IDisposable implementation
    }
}
