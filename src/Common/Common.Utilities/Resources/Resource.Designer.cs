﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Common.Utilities.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Common.Utilities.Resources.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bad request.
        /// </summary>
        public static string BadRequest {
            get {
                return ResourceManager.GetString("BadRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error occurred during password change.
        /// </summary>
        public static string ChangePasswordError {
            get {
                return ResourceManager.GetString("ChangePasswordError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Database unexpected error occurred while processing the request.
        /// </summary>
        public static string DatabaseError {
            get {
                return ResourceManager.GetString("DatabaseError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Object cannot be deleted.
        /// </summary>
        public static string DeletionError {
            get {
                return ResourceManager.GetString("DeletionError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is already taken.
        /// </summary>
        public static string EmailTaken {
            get {
                return ResourceManager.GetString("EmailTaken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Forbidden.
        /// </summary>
        public static string Forbidden {
            get {
                return ResourceManager.GetString("Forbidden", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error occurred during password reset code generation.
        /// </summary>
        public static string GeneratePasswordResetCodeError {
            get {
                return ResourceManager.GetString("GeneratePasswordResetCodeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Institution with given id &apos;{0}&apos; already exists.
        /// </summary>
        public static string InstitutionAlreadyExists {
            get {
                return ResourceManager.GetString("InstitutionAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Institution with given id &apos;{0}&apos; not found.
        /// </summary>
        public static string InstitutionNotFound {
            get {
                return ResourceManager.GetString("InstitutionNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Active.
        /// </summary>
        public static string InstitutionStateActive {
            get {
                return ResourceManager.GetString("InstitutionStateActive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Deleted.
        /// </summary>
        public static string InstitutionStateDeleted {
            get {
                return ResourceManager.GetString("InstitutionStateDeleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Inactive.
        /// </summary>
        public static string InstitutionStateInactive {
            get {
                return ResourceManager.GetString("InstitutionStateInactive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New.
        /// </summary>
        public static string InstitutionStateNew {
            get {
                return ResourceManager.GetString("InstitutionStateNew", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid full name.
        /// </summary>
        public static string InvalidFullName {
            get {
                return ResourceManager.GetString("InvalidFullName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot change user state from &apos;{0}&apos; to &apos;{1}&apos;.
        /// </summary>
        public static string InvalidUserStateTransition {
            get {
                return ResourceManager.GetString("InvalidUserStateTransition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; not found.
        /// </summary>
        public static string NotFound {
            get {
                return ResourceManager.GetString("NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Object not found.
        /// </summary>
        public static string ObjectNotFound {
            get {
                return ResourceManager.GetString("ObjectNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error occurred during password reset.
        /// </summary>
        public static string PasswordResetError {
            get {
                return ResourceManager.GetString("PasswordResetError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Object cannot be restored.
        /// </summary>
        public static string RestorationError {
            get {
                return ResourceManager.GetString("RestorationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Server error.
        /// </summary>
        public static string ServerError {
            get {
                return ResourceManager.GetString("ServerError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unauthorized.
        /// </summary>
        public static string Unauthorized {
            get {
                return ResourceManager.GetString("Unauthorized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error occurred during user activation.
        /// </summary>
        public static string UserActivationError {
            get {
                return ResourceManager.GetString("UserActivationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User with given id &apos;{0}&apos; not found.
        /// </summary>
        public static string UserNotFound {
            get {
                return ResourceManager.GetString("UserNotFound", resourceCulture);
            }
        }
    }
}
