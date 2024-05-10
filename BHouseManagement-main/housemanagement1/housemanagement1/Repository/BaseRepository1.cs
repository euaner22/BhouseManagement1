﻿using housemanagement1.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace housemanagement1.Repository
{
    public class BaseRepository1<T> : IBaseRepository<T>
        where T : class
    {
        public DbContext _db;
        public DbSet<T> _table;
        public object id;

        public DbSet<T> Table()
        {
            return _table;
        }

        public BaseRepository1()
        {
            _db = new bhousemanagementEntities2();
            _table = _db.Set<T>();
        }
        public T Get(Object id)
        {
            return _table.Find(id);
        }
        public List<T> GetAll()
        {
            return _table.ToList();
        }


        public ErrorCode Create(T t)
        {
            try
            {
                _table.Add(t);
                _db.SaveChanges();
                return ErrorCode.Success;

            }
            catch (Exception ex)
            {
                return ErrorCode.Error;
            }
        }
        public ErrorCode Delete(T t)
        {
            try
            {
                var obj = Get(id);
                _table.Remove(obj);
                _db.SaveChanges();
                return ErrorCode.Success;

            }
            catch (Exception ex)
            {
                return ErrorCode.Error;
            }
        }
        public ErrorCode Update(T t)
        {
            try
            {
                var oldOjb = Get(id);
                _db.Entry(oldOjb).CurrentValues.SetValues(t);
                _db.SaveChanges();
                return ErrorCode.Success;

            }
            catch (Exception ex)
            {
                return ErrorCode.Error;
            }
        }

        public ErrorCode Update(object id, T t)
        {
            try
            {
                var existingEntity = Get(id);
                if (existingEntity != null)
                {
                    _db.Entry(existingEntity).CurrentValues.SetValues(t);
                    _db.SaveChanges();
                    return ErrorCode.Success;
                }
                else
                {
                    return ErrorCode.Error; 
                }
            }
            catch (Exception ex)
            {
            
                return ErrorCode.Error;
            }
        }

        public ErrorCode Delete(object id)
        {
            try
            {
                var entity = Get(id);
                if (entity != null)
                {
                    _table.Remove(entity);
                    _db.SaveChanges();
                    return ErrorCode.Success;
                }
                else
                {
                    return ErrorCode.Error; 
                }
            }
            catch (Exception ex)
            {
   
                return ErrorCode.Error;
            }
        }

        internal object GetByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}