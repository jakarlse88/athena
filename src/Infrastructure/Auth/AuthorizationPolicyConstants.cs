using System;

namespace Athena.Infrastructure.Auth
{
    public static class AuthorizationPolicyConstants
    {
        internal const string PermissionsClaimName = "permissions";

        internal const string TechniqueReadPermission = "HasTechniqueReadPermission";
        internal const string TechniqueWritePermission = "HasTechniqueWritePermission";
        internal const string TechniqueUpdatePermission = "HasTechniqueUpdatePermission";
        internal const string TechniqueDeletePermission = "HasTechniqueDeletePermission";

        internal const string TechniqueCategoryReadPermission = "HasTechniqueCategoryReadPermission";
        internal const string TechniqueCategoryWritePermission = "HasTechniqueCategoryWritePermission";
        internal const string TechniqueCategoryUpdatePermission = "HasTechniqueCategoryUpdatePermission";
        internal const string TechniqueCategoryDeletePermission = "HasTechniqueCategoryDeletePermission";

        internal const string TechniqueTypeReadPermission = "HasTechniqueTypeReadPermission";
        internal const string TechniqueTypeWritePermission = "HasTechniqueTypeWritePermission";
        internal const string TechniqueTypeUpdatePermission = "HasTechniqueTypeUpdatePermission";
        internal const string TechniqueTypeDeletePermission = "HasTechniqueTypeDeletePermission";
        
        public static string MapConfigKeyToPolicyName(string key) =>
            key switch
            {
                null => throw new ArgumentNullException(nameof(key)),
                "TechniqueReadPermission" => TechniqueReadPermission,
                "TechniqueWritePermission" => TechniqueWritePermission,
                "TechniqueUpdatePermission" => TechniqueUpdatePermission,
                "TechniqueDeletePermission" => TechniqueDeletePermission,
                "TechniqueCategoryReadPermission" => TechniqueCategoryReadPermission,
                "TechniqueCategoryWritePermission" => TechniqueCategoryWritePermission,
                "TechniqueCategoryUpdatePermission" => TechniqueCategoryUpdatePermission,
                "TechniqueCategoryDeletePermission" => TechniqueCategoryDeletePermission,
                "TechniqueTypeReadPermission" => TechniqueTypeReadPermission,
                "TechniqueTypeWritePermission" => TechniqueTypeWritePermission,
                "TechniqueTypeUpdatePermission" => TechniqueTypeUpdatePermission,
                "TechniqueTypeDeletePermission" => TechniqueTypeDeletePermission,
                _ => throw new ArgumentException($"No policy found for the key '{key}'")
            };
    }
}