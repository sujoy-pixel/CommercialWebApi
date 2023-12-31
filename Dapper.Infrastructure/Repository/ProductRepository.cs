﻿using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Dapper.Infrastructure.DapperContext;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _dbCon;
        public ProductRepository(DatabaseContext _dbConnection)
        {
            _dbCon = _dbConnection;
        }
        public async Task<int> AddAsync(Product entity)
        {
            entity.AddedOn = DateTime.Now;
            var sql = "Insert into Products (Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var connection = _dbCon.CreateConnection())
            {
                //connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            using (var connection = _dbCon.CreateConnection())
            {
                //connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
           // var sql = "SELECT * FROM [TestDB].[dbo].[tbl_Student_Info]";
            using (var connection = _dbCon.CreateConnection())
            {              
                var result = await connection.QueryAsync<Product>("sp_GetProduct",commandType:CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            DynamicParameters _param = new DynamicParameters();
            _param.Add("id", id,DbType.Int32);
            //var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = _dbCon.CreateConnection())
            {
  
                var result = await connection.QuerySingleOrDefaultAsync<Product>("sp_GetProduct",_param, commandType:CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var connection = _dbCon.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
