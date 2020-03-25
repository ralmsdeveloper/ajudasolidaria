using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AjudaSolidaria.Respository
{
    public class AjudaSolidariaContext : DbContext
    {
        public AjudaSolidariaContext(DbContextOptions<AjudaSolidariaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AjudaSolidariaContext).Assembly);

            HandleNames(modelBuilder);
        }

        private void HandleNames(ModelBuilder modelBuilder)
        {
            string toSnakeCase(string name)
                => Regex
                    .Replace(
                        name,
                        @"([a-z0-9])([A-Z])",
                        "$1_$2",
                        RegexOptions.Compiled)
                    .ToLower(); 

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = toSnakeCase(entity.GetTableName());
                entity.SetTableName(tableName);

                foreach (var property in entity.GetProperties())
                {
                    var columnName = toSnakeCase(property.GetColumnName());
                    property.SetColumnName(columnName);
                }

                foreach (var key in entity.GetKeys())
                {
                    var keyName = toSnakeCase(key.GetName());
                    key.SetName(keyName);
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    var foreignKeyName = toSnakeCase(key.GetConstraintName());
                    key.SetConstraintName(foreignKeyName);
                }

                foreach (var index in entity.GetIndexes())
                {
                    var indexName = toSnakeCase(index.GetName());
                    index.SetName(indexName);
                }
            }
        }
    }
}
