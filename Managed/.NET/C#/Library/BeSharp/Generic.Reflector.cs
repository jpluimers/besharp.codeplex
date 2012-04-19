using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Text;

using NameValuePair = System.Collections.Generic.KeyValuePair<string, string>;
using NameValuePairList = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>;

namespace BeSharp.Generic
{
    public class Reflector
    {
        // http://abdullin.com/journal/2008/12/13/how-to-find-out-variable-or-parameter-name-in-c.html
        public static string GetName<T>(T item)
        {
            string result = Generic.TypeCache<T>.Name;
            return result;
        }

        public static NameValuePair GetNameValue<T>(T item)
        {
            string name = GetName(item);
            string value = getValueString(item, name);
            NameValuePair result = new NameValuePair(name, value);
            return result;
        }

        public static NameValuePairList GetNameValues<T>(T items)
        {
            NameValuePairList result = new NameValuePairList();
            string[] names = Generic.TypeCache<T>.Names;
            foreach (string name in names)
            {
                string value = getValueString(items, name);
                NameValuePair item = new NameValuePair(name, value);
                result.Add(item);
            }
            return result;
        }

        public const string Colon = ":";

        public static string GetNameSeparatorValue<T>(T item, string separator = Colon)
        {
            string name = Generic.TypeCache<T>.Name;
            string result = GetNameSeparatorValue<T>(item, separator, name);
            return result;
        }

        public static string GetNameSeparatorTabValues<T>(T items, string separator = Colon)
        {
            string[] names = Generic.TypeCache<T>.Names;
            int maxNameLength = 0;
            foreach (string name in names)
            {
                int nameLength = name.Length;
                if (nameLength > maxNameLength)
                    maxNameLength = nameLength;
            }

            StringBuilder result = new StringBuilder();
            foreach (string name in names)
            {
                int padLength = 1 + maxNameLength - name.Length;
                int tabs = 1 + (padLength / 8); // 8 spaces per tab
                string tabsSeparator = separator + Tabs(tabs);
                string nameSeparatorValue = GetNameSeparatorValue<T>(items, tabsSeparator, name);
                result.AppendLine(nameSeparatorValue);
            }

            return result.ToString();
        }

        public static string Tabs(int n) // TODO ##jpl: move to a central strings helper
        {
            return new String('\t', n);
        }

        private static string GetNameSeparatorValue<T>(T item, string separator, string name)
        {
            object value = getValue<T>(item, name);
            string nameSeparatorValue = string.Format("{0}{1}{2}", name, separator, value);
            return nameSeparatorValue;
        }

        private static object getValue<T>(T item, string name)
        {
            MemberInfo[] memberInfos = typeof(T).GetMember(name);
            int memberInfosLength = memberInfos.Length;
            if (memberInfosLength != 1)
                throw new ArgumentException(string.Format("parameter {0} with name {1} should return 1 member with that name, but returns {2} members", "item", name, memberInfosLength));
            MemberInfo memberInfo = memberInfos[0];
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            object value;
            if (null != propertyInfo)
                value = propertyInfo.GetValue(item, null);
            else
                value = item;
            return value;
        }

        private static string getValueString<T>(T item, string name)
        {
            object value = getValue(item, name);
            string result = (null == value) ? string.Empty : value.ToString();
            return result;
        }

        public static string GetFullMethodNameAndParameters(params object[] values)
        {
            MethodBase method = getMethodBase(parentParentStackFrame);
            string methodName = getMethodName(method);
            StringBuilder result = new StringBuilder(methodName);

            ParameterInfo[] parameterInfos = method.GetParameters();

            result.Append("(");
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                if (i > 0)
                    result.Append(", ");

                string type;
                try
                {
                    type = parameterInfos[i].ParameterType.Name; // FullName;
                }
                catch (Exception ex)
                {
                    type = ex.ToString();
                }

                string name;
                try
                {
                    name = parameterInfos[i].Name;
                }
                catch (Exception ex)
                {
                    name = ex.ToString();
                }

                string value;
                try
                {
                    if (i < values.Length)
                    {
                        value = values[i].ToString();
                    }
                    else
                        value = "<empty>";
                }
                catch (Exception ex)
                {
                    value = ex.ToString();
                }

                result.AppendFormat("{0} {1} = {2}", type, name, value);
            }
            result.Append(")");
            return result.ToString();
        }

        public static string MethodName
        {
            get
            {
                string result = GetMethodName(1);
                return result;
            }
        }

        public static string GetMethodName(int skipStackFrames = 0)
        {
            string result = getMethodName(parentParentStackFrame + skipStackFrames);
            return result;
        }

        public static string GetFullMethodName(int skipStackFrames = 0)
        {
            string result = getMethodName(parentParentStackFrame + skipStackFrames, true);
            return result;
        }

        public static string GetPropertyName(int skipStackFrames = 0)
        {
            string methodName = getMethodName(parentParentStackFrame + skipStackFrames);
            string result = stripPropertyMethodPrefixToStripFromGetMethodName(methodName);
            return result;
        }

        public static string GetFullPropertyName(int skipStackFrames = 0)
        {
            string methodName = getMethodName(parentParentStackFrame + skipStackFrames, true);
            string result = stripPropertyMethodPrefixToStripFromGetMethodName(methodName);
            return result;
        }

        // http://stackoverflow.com/a/44215/29290
        private const int parentParentStackFrame = 2;
        private static string getMethodName(int skipFrames, bool fullMethodName = false)
        {
            MethodBase method = getMethodBase(skipFrames + 1);
            string result = getMethodName(method, fullMethodName);
            return result;
        }

        private static string getMethodName(MethodBase method, bool fullMethodName = false)
        {
            string result = method.Name;
            if (fullMethodName)
            {
                Type declaringType = method.DeclaringType;
                if (null != declaringType)
                    result = string.Format("{0}.{1}", declaringType.FullName, result);
            }
            return result;
        }

        private static MethodBase getMethodBase(int skipFrames)
        {
            StackFrame stackFrame = new StackFrame(skipFrames);
            MethodBase method = stackFrame.GetMethod();
            return method;
        }

        private const string get_PropertyMethodPrefixToStripFromGetMethodName = "get_";

        private static string stripPropertyMethodPrefixToStripFromGetMethodName(string methodName)
        {
            return methodName.Substring(get_PropertyMethodPrefixToStripFromGetMethodName.Length); // strip the get_ or set_ part
        }

        public static List<string> GetPublicStringConstants<T>() where T : class
        {
            IEnumerable<FieldInfo> fieldInfos = GetPublicStringConstantFieldInfos<T>();
            List<string> result = getMemberNames(fieldInfos);
            return result;
        }

        public static IEnumerable<FieldInfo> GetPublicStringConstantFieldInfos<T>() where T : class
        {
            var result = getPublicStaticDeclaredOnlyFieldInfos<T>(fieldInfo =>
                (fieldInfo.IsLiteral && // literal constant
                (fieldInfo.FieldType.FullName == typeof(string).FullName))
                )
                ;
            return result;
        }

        public static bool NameMatchesValue(FieldInfo fieldInfo)
        {
            bool result = fieldInfo.Name == getMemberValueString(fieldInfo);
            return result;
        }

        private static FieldInfo[] getPublicStaticDeclaredOnlyFieldInfos<T>() where T : class
        {
            FieldInfo[] publicStaticDeclaredOnlyFieldInfos = typeof(T).GetFields(
                                        BindingFlags.Public // only public
                                        | BindingFlags.Static // statics include consts
                                        | BindingFlags.DeclaredOnly // only the ConfigKeyString class
                                        );
            return publicStaticDeclaredOnlyFieldInfos;
        }

        private static IEnumerable<FieldInfo> getPublicStaticDeclaredOnlyFieldInfos<T>(Func<FieldInfo, bool> predicate) where T : class
        {
            FieldInfo[] publicStaticDeclaredOnlyFieldInfos = getPublicStaticDeclaredOnlyFieldInfos<T>();
            IEnumerable<FieldInfo> result = publicStaticDeclaredOnlyFieldInfos.Where(predicate);
            return result;
        }

        private static string getMemberValueString(FieldInfo fieldInfo)
        {
            string fieldInfoName = GetName(new { fieldInfo });
            if (null == fieldInfo)
                throw new ArgumentNullException(fieldInfoName);
            if (!fieldInfo.IsStatic)
                throw new ArgumentException(string.Format("fieldInfo {0} must be static", fieldInfo.Name), fieldInfoName);
            object member = (fieldInfo.IsLiteral) ?
                fieldInfo.GetRawConstantValue()
                :
                fieldInfo.GetValue(null);
            string result = member.ToString();
            return result;
        }

        private static List<string> getMemberNames(IEnumerable<FieldInfo> fieldInfos)
        {
            List<string> result = new List<string>();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                result.Add(getMemberValueString(fieldInfo));
            }
            return result;
        }
    }
}
