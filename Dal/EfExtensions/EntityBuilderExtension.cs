using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;

namespace Dal.EfExtensions
{
    public static class EntityBuilderExtension
    {
        public static void HasEnumStringValuesCheckConstraint<TEntity, TEnum>(this EntityTypeBuilder<TEntity> entityTypeBuilder, Expression<Func<TEntity, TEnum>> property, ValueConverter<TEnum, string> converter)
            where TEntity : class
            where TEnum : struct, Enum
        {
            entityTypeBuilder.HasEnumValuesCheckConstraint(property, converter, true);
        }

        public static void HasEnumValuesCheckConstraint<TEntity, TEnum, TValue>(this EntityTypeBuilder<TEntity> entityTypeBuilder, Expression<Func<TEntity, TEnum>> property, ValueConverter<TEnum, TValue> converter, bool escape)
            where TEntity : class
            where TEnum : struct, Enum
        {
            Func<TEnum, TValue> convertTo = converter.ConvertToProviderExpression.Compile();

            string tableName = entityTypeBuilder.Metadata.GetTableName();
            string columnName = entityTypeBuilder.Property(property).Metadata.GetColumnName();

            TValue[] allowedValues = Enum.GetValues<TEnum>()
                .Select(convertTo)
                .ToArray();


            string sqlStringValues = string.Join(",", allowedValues.Select(x => escape ? $"'{x}'" : x.ToString()));

            entityTypeBuilder.ToTable(x => x.HasCheckConstraint($"CK_{tableName}_{columnName}_Has_Allowed_Values", $"\"{columnName}\" in ({sqlStringValues})"));
        }

        public static void HasPositiveCheckConstraint<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, Expression<Func<TEntity, decimal>> property)
            where TEntity : class
        {
            string tableName = entityTypeBuilder.Metadata.GetTableName();
            string columnName = entityTypeBuilder.Property(property).Metadata.GetColumnName();

            entityTypeBuilder.ToTable(x => x.HasCheckConstraint($"CK_{tableName}_{columnName}_Is_Positive", $"\"{columnName}\" > 0"));
        }

        public static void HasNotNegativeCheckConstraint<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, Expression<Func<TEntity, decimal>> property)
            where TEntity : class
        {
            string tableName = entityTypeBuilder.Metadata.GetTableName();
            string columnName = entityTypeBuilder.Property(property).Metadata.GetColumnName();

            entityTypeBuilder.ToTable(x => x.HasCheckConstraint($"CK_{tableName}_{columnName}_Is_Positive", $"\"{columnName}\" >= 0"));
        }

        public static void HasNotNegativeCheckConstraint<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, Expression<Func<TEntity, int?>> property)
            where TEntity : class
        {
            string tableName = entityTypeBuilder.Metadata.GetTableName();
            string columnName = entityTypeBuilder.Property(property).Metadata.GetColumnName();

            entityTypeBuilder.ToTable(x => x.HasCheckConstraint($"CK_{tableName}_{columnName}_Is_Positive", $"\"{columnName}\" >= 0"));
        }
    }
}
