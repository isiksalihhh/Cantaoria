namespace Cantaoria.Application.Models.Enums
{
    using System;

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class RoleStringAttribute : Attribute
    {
        public string Value { get; }

        public RoleStringAttribute(string value)
        {
            Value = value;
        }
    }
    public static class RoleEnumHelper
    {
        public static string GetRoleString(RolesEnum role)
        {
            var fieldInfo = role.GetType().GetField(role.ToString());

            var attribute = (RoleStringAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(RoleStringAttribute));

            return attribute?.Value ?? role.ToString();
        }
    }

    public enum RolesEnum
    {
        [RoleString("Yönetici")]
        Admin = 1,
        [RoleString("Müşteri")]
        Customer = 4
    }
    //RolesEnum role = RolesEnum.Admin;
    //string roleString = RoleEnumHelper.GetRoleString(role);
}
