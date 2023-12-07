using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Dapper.Infrastructure.DapperContext;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration configuration;
        private readonly DatabaseContext _dbContext;
        public OrderRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddAsync(OrderModel entity)
        {
            entity.AddedOn = DateTime.Now;
            var sql = "Insert into Products (Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = _dbContext.CreateConnection())
            {
             
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            using (var connection = _dbContext.CreateConnection())
            {
                //connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<OrderModel>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            using (var connection = _dbContext.CreateConnection())
            {
               // connection.Open();
                var result = await connection.QueryAsync<OrderModel>(sql);
                return result.ToList();
            }
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = _dbContext.CreateConnection())
            {
                //connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<OrderModel>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(OrderModel entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var connection = _dbContext.CreateConnection())
            {
                //connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
