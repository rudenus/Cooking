using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dal.EfExtensions
{
    public static class PropertyBuilderExtension
    {
        public static PropertyBuilder<TEnum> HasMaxLengthOfMaxLength<TEnum>(this PropertyBuilder<TEnum> propertyBuilder, ValueConverter<TEnum, string> converter)
            where TEnum : struct, Enum
        {
            Func<TEnum, string> convertTo = converter.ConvertToProviderExpression.Compile();

            int maxLength = Enum.GetValues<TEnum>()
                .Select(x => convertTo(x))
                .Max(x => x.Length);

            return propertyBuilder.HasMaxLength(maxLength);
        }
    }
}
