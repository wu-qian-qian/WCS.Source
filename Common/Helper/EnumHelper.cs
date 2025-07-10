using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class EnumHelper
    {
        /// <summary>
        /// 获取 枚举值的注释
        /// </summary>
        /// <param name="thisEnum"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum thisEnum)
        {
            var descriptionAttribute = thisEnum.GetEnumAttribute<DescriptionAttribute>();
            return descriptionAttribute?.Description;
        }


        /// <summary>
        /// 获取对应 枚举值特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisEnum"></param>
        /// <returns></returns>
        public static T GetEnumAttribute<T>(this Enum thisEnum) where T : Attribute
        {
            Type type = thisEnum.GetType();
            FieldInfo fieldInfo = type.GetField(thisEnum.ToString());
            if (fieldInfo != null)
            {
                object attrObject = fieldInfo.GetCustomAttribute(typeof(T));
                return attrObject as T;
            }
            return null;
        }
    }
}
