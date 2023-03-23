using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Udemy.AdvertisementApp.Common.Enums;
using Udemy.AdvertisementApp.DataAccess.Contexts;
using Udemy.AdvertisementApp.DataAccess.Interfaces;
using Udemy.AdvertisementApp.Entities;

namespace Udemy.AdvertisementApp.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        //context UOW'ten gelecek
        private readonly AdvertisementAppContext _context;

        public Repository(AdvertisementAppContext context)
        {
            _context = context;
        }
        //orderby sıralayarak gelsin veriler istiyorum bunun için bana enum gerekecek.
        //veri getirme için 3 seçenek mevcut
        //bütün veriyi getirme
        //bütün veriyi sıralayarak getirme
        //bütün veriyi filtre uygulayarak getirme
        //asnotracking()
        public async Task<List<T>> GetAllAsync()
        {
            //bütün listeyi getiren metot
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        //filtreli metot
        public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>> filter)
        {
            return await _context.Set<T>().Where(filter).AsNoTracking().ToListAsync();
        }
        //sıralamalı metot ama default olarak DESC enum'ını alıyor değer olarak.
        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T,TKey>> selector, OrderByType orderByType = OrderByType.DESC)
        {
            return orderByType == OrderByType.ASC ? await _context.Set<T>().AsNoTracking().OrderBy(selector).ToListAsync() : await _context.Set<T>().AsNoTracking().OrderByDescending(selector).ToListAsync();
        }
        //hem filtreli hemde sıralamalı (default olarak DESC enum alan)
        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T,bool>> filter, Expression<Func<T,TKey>> selector, OrderByType orderByType = OrderByType.DESC)
        {
            return orderByType == OrderByType.ASC ? await _context.Set<T>().Where(filter).AsNoTracking().OrderBy(selector).ToListAsync() : await _context.Set<T>().Where(filter).AsNoTracking().OrderByDescending(selector).ToListAsync();
        }
        //FİND METODU (ID İLE BULMA Kayıdı)
        public async Task<T> FindAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        //ID İLE BULMAK İSTEMİYORSAM İLGİLİ KAYIDI VEYA ASNOTRACKİNG İLE BULMASINI İSTİYORSAM
        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, bool AsNoTracking = false)
        {
            return !AsNoTracking ? await _context.Set<T>().SingleOrDefaultAsync(filter) : await _context.Set<T>().SingleOrDefaultAsync(filter);
        } 
        //Queryable metotu
        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>().AsQueryable();
        }
        //Remove metotu
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        //Create metotu
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        //Update metotu
        public void Update(T entity, T unchanged)
        {
            //değiştirilmemiş datayı dbden al yeni datalarla değiştir.
            _context.Entry(unchanged).CurrentValues.SetValues(entity);
        }
    }
}
