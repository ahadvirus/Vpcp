using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vpcp.Kernel.Extensions;

public static class MySqlConfigurationExtension
{
    public static PropertyBuilder<T> HasCollation<T>(
        [NotNull] this PropertyBuilder<T> propertyBuilder,
        [NotNull] string collation)
    {
        propertyBuilder.Metadata.SetOrRemoveAnnotation("MySQL:Collation", (object)collation);
        return propertyBuilder;
    }

    public static PropertyBuilder<T> HasCharSet<T>(
        [NotNull] this PropertyBuilder<T> propertyBuilder,
        [NotNull] string charset)
    {
        propertyBuilder.Metadata.SetOrRemoveAnnotation("MySQL:Charset", (object)charset);
        return propertyBuilder;
    }
}