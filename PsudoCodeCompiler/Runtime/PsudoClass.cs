using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PseudoCodeCompiler.Runtime
{
    [TypeConverterAttribute(typeof(ClassObjectConverter))]
    public class PsudoClass : ICustomTypeDescriptor
    {
        public string Name { get; set; }
        public Dictionary<string, PsudoMethod> Methods { get; set; }
        public Dictionary<string, object> ClassVariables { get; set; }

        public PsudoClass(string name)
        {
            this.Name = name;
            this.Methods = new Dictionary<string, PsudoMethod>();
            this.ClassVariables = new Dictionary<string, object>();
        }

        #region ICustomTypeDescriptor Members

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            List<PropertyDescriptor> propList = new List<PropertyDescriptor>();
            // Get Global Variables
            propList.AddRange(GetVarProps());

            PropertyDescriptorCollection props =
                new PropertyDescriptorCollection(propList.ToArray());

            return props;
        }

        protected List<PropertyDescriptor> GetVarProps()
        {
            List<PropertyDescriptor> propList = new List<PropertyDescriptor>();

            foreach (var variable in this.ClassVariables)
            {
                PropertyDescriptor desc = TypeDescriptor.CreateProperty(
                    this.GetType(), variable.Key, variable.Value.GetType());
                propList.Add(new ProgramPropertyDescriptor(desc, "Class Variables", variable.Value));

            }
            return propList;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion
    }

    public class ClassObjectConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(PsudoClass))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return "Object Type: " + (value as PsudoClass).Name;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
