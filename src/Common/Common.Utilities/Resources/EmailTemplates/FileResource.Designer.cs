﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Common.Utilities.Resources.EmailTemplates {
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
    public class FileResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FileResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Common.Utilities.Resources.EmailTemplates.FileResource", typeof(FileResource).Assembly);
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
        ///   Looks up a localized string similar to Enscool - Account and Institution activation ⏱️✅
        ///&lt;div&gt;
        ///    &lt;p&gt;Hello, {{user_name}} 👋🏽&lt;/p&gt;
        ///
        ///    Thank you for choosing Enscool. We are happy to have you on board! 😊🎉&lt;br&gt;
        ///    You have successfully registered your institution on our website.&lt;br&gt;
        ///    &lt;br&gt;
        ///    Your account and institution are not yet activated&lt;br&gt;
        ///    Please click on the link below to activate them ⬇️&lt;br&gt;
        ///    &lt;a href=&quot;{{activation_link}}&quot;&gt;Activate! 🚀&lt;/a&gt;&lt;br&gt;
        ///
        ///    &lt;br&gt;&lt;br&gt;
        ///
        ///    &lt;div&gt;
        ///        Best regards 💜 &lt;br&gt;
        ///        Enscoo [rest of string was truncated]&quot;;.
        /// </summary>
        public static string InstitutionRegisteredEmailTemplate {
            get {
                return ResourceManager.GetString("InstitutionRegisteredEmailTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enscool - Account activation ⏱️✅
        ///&lt;div&gt;
        ///    &lt;p&gt;Hello, {{user_name}} 👋🏽&lt;/p&gt;
        ///
        ///    You have been added to Enscool as &lt;strong&gt;{{role}}&lt;/strong&gt;! We are happy to have you on board! 😊🎉&lt;br&gt;
        ///    Your account has been created, but you still need to activate it.
        ///    &lt;br&gt;
        ///    Please click on the link below to activate your account ⬇️&lt;br&gt;
        ///    &lt;a href=&quot;{{activation_link}}&quot;&gt;Activate! 🚀&lt;/a&gt;&lt;br&gt;
        ///
        ///    &lt;br&gt;&lt;br&gt;
        ///
        ///    &lt;div&gt;
        ///        Best regards 💜 &lt;br&gt;
        ///        Enscool
        ///    &lt;/div&gt;
        ///&lt;/div&gt;.
        /// </summary>
        public static string InstitutionUserCreatedEmailTemplate {
            get {
                return ResourceManager.GetString("InstitutionUserCreatedEmailTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enscool - Reset Password Code 🔒🗝️
        ///&lt;div&gt;
        ///    &lt;p&gt;Hello, {{user_name}} 👋🏽&lt;/p&gt;
        ///
        ///    It seems that you forgot or lost your password. No worries, we got you covered! 😎
        ///    &lt;br&gt;&lt;br&gt;
        ///    Here is your password reset code:
        ///    &lt;span style=&quot;background-color: #6366f1; color: white; padding: 8px 12px; border-radius: 4px; font-weight: bold; -webkit-user-select: all; -moz-user-select: all; -ms-user-select: all; user-select: all; cursor: pointer;&quot;&gt;
        ///        {{reset_code}}
        ///    &lt;/span&gt;
        ///    &lt;br&gt;&lt;br&gt;
        ///    &lt;strong [rest of string was truncated]&quot;;.
        /// </summary>
        public static string PasswordResetCodeEmailTemplate {
            get {
                return ResourceManager.GetString("PasswordResetCodeEmailTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;div style=&quot;font-family: Arial, sans-serif; color: #202020; padding-top: 20px&quot;&gt;
        ///    &lt;div style=&quot;margin: auto; min-width: 350px; max-width: 700px; border-radius: 20px; background-color: #f5f5f5&quot;&gt;
        ///        &lt;div style=&quot;padding: 20px; font-size: 16px; line-height: 1.5em&quot;&gt;
        ///            {{template}}
        ///        &lt;/div&gt;
        ///    &lt;/div&gt;
        ///&lt;/div&gt;.
        /// </summary>
        public static string WrapperTemplate {
            get {
                return ResourceManager.GetString("WrapperTemplate", resourceCulture);
            }
        }
    }
}
