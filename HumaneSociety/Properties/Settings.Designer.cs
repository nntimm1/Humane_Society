﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HumaneSociety.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.4.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
<<<<<<< HEAD
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=NICK-T-PC\\SQLEXPRESS;Initial Catalog=HumaneSociety;Integrated Securit" +
            "y=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False")]
=======
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=FATIMA0F1F\\SQLEXPRESS;Initial Catalog=HumaneSociety;Integrated Securi" +
            "ty=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False")]
>>>>>>> 4fd4e51c212bf1f9e9a390e27d5b6859706eb3a4
        public string HumaneSocietyConnectionString {
            get {
                return ((string)(this["HumaneSocietyConnectionString"]));
            }
        }
    }
}
