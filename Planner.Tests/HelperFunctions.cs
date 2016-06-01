using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Planner.Tests
{
    [ExcludeFromCodeCoverage]
    public static class HelperFunctions
    {
        public static void AssertHasSamePropertyValues<T>(this T obj1, T obj2) where T : class
        {
            if (obj1 != null && obj2 != null)
            {
                bool assertionsMade = false;
                foreach (PropertyInfo property in obj1.GetType().GetProperties().Where(property => property.IsComparable()))
                {
                    Assert.IsTrue(property.HasEqualValues(obj1, obj2), string.Format("Value of property '{0}' in objects don't match", property.Name));
                    assertionsMade = true;
                }

                if (assertionsMade == false)
                {
                    Assert.Inconclusive("No Fields or Properties found to compare");
                }
            }
            else
            {
                Assert.Inconclusive("Cannot compare nulls");
            }
        }

        public static bool HasSamePropertyValues<T>(this T obj1, T obj2) where T : class
        {
            return obj1.GetType().GetProperties().Where(property => property.IsComparable()).All(property => property.HasEqualValues(obj1, obj2));
        }

        private static bool IsComparable(this PropertyInfo propertyInfo)
        {
            return propertyInfo.CanRead && (propertyInfo.MemberType == MemberTypes.Field || propertyInfo.MemberType == MemberTypes.Property);
        }

        private static bool HasEqualValues(this PropertyInfo property, object obj1, object obj2)
        {
            return Equals(property.GetValue(obj1, null), property.GetValue(obj2, null));
        }
    }
}