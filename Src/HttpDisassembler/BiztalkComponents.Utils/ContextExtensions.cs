using System;
using Microsoft.BizTalk.Message.Interop;

namespace BizTalkComponents.Utils.ContextExtensions
{
    public static class ContextExtensions
    {
        public static bool TryRead(this IBaseMessageContext ctx ,ContextProperty property, out object val)
        {
            return ((val = ctx.Read(property.PropertyName, property.PropertyNamespace)) != null);
        }

        public static void Promote(this IBaseMessageContext ctx, ContextProperty property, object val)
        {
            ctx.Promote(property.PropertyName,property.PropertyNamespace,val);
        }

        public static void Write(this IBaseMessageContext ctx, ContextProperty property, object val)
        {
            ctx.Write(property.PropertyName, property.PropertyNamespace,val);
        }

        public static void Copy(this IBaseMessageContext ctx, ContextProperty source, ContextProperty destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }

            object sourceValue;

            if (ctx.TryRead(source, out sourceValue))
            {
                throw new InvalidOperationException("Could not find the specified source property in BizTalk context.");
            }

            ctx.Promote(destination, sourceValue);
        }
    }

    public class ContextProperty
    {
        public ContextProperty(string propertyName, string propertyNamespace)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            if (string.IsNullOrEmpty(propertyNamespace))
            {
                throw new ArgumentNullException("propertyNamespace");
            }

            PropertyName = propertyName;
            PropertyNamespace = propertyNamespace;
        }

        public ContextProperty(string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                throw new ArgumentNullException("property");
            }

            if (!property.Contains("#"))
            {
                throw new ArgumentException("The property path {0} is not valid", property);
            }

            PropertyNamespace = property.Split('#')[0];
            PropertyName = property.Split('#')[1];
        }

        public string PropertyName { get; private set; }
        public string PropertyNamespace { get; private set; }

        public string ToPropertyString()
        {
            return string.Format("{0}#{1}", PropertyNamespace, PropertyName);
        }
    }
}
