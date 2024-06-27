using System.Collections.Generic;
using System.IO;
using System.Text;
using Lando.Core.Utilities;

namespace Lando.Plugins.Generators.Editor
{
    public static class ClassGenerator
    {
        public enum AccessModifier
        {
            Public,
            Private,
            Protected,
            Internal
        }
        
        private static string GetString(AccessModifier accessModifier) => accessModifier switch
        {
            AccessModifier.Public => "public",
            AccessModifier.Private => "private",
            AccessModifier.Protected => "protected",
            AccessModifier.Internal => "internal",
            _ => "public"
        };
        
        public class Member
        {
            public string Type;
            public string Name;
            public AccessModifier Access = AccessModifier.Public;
            public bool IsStatic;
            public bool IsReadonly;
            public bool IsConst;
            public string Value;
            
            public string Contents
            {
                get
                {
                    StringBuilder stringBuilder = new();
                    
                    stringBuilder.Append($"{GetString(Access)} {(IsStatic ? "static" : "")} {(IsReadonly ? "readonly" : "")} {(IsConst ? "const" : "")} {Type} {Name}");
                    
                    if (!string.IsNullOrEmpty(Value))
                        stringBuilder.Append($" = {Value}");
                    
                    stringBuilder.Append(";");
                    
                    return stringBuilder.ToString();
                }
            }
        }
        
        public class Class
        {
            public string Namespace;
            public AccessModifier Access = AccessModifier.Public;
            public bool IsPartial;
            public bool IsStatic;
            public string Name;
            public List<Member> Members = new();
            
            private int _identLevel;
            
            public string Contents
            {
                get
                {
                    StringBuilder stringBuilder = new();
                    
                    if (!string.IsNullOrEmpty(Namespace))
                    {
                        AddLine($"namespace {Namespace}");
                        OpenCurlyBrackets();
                    }
                    
                    AddLine($"{GetString(Access)}{(IsPartial ? " partial" : "")}{(IsStatic ? " static" : "")} class {Name}");
                    OpenCurlyBrackets();
                    
                    foreach (Member member in Members)
                        AddLine(member.Contents);
                    
                    CloseCurlyBrackets();
                    
                    if (!string.IsNullOrEmpty(Namespace))
                        CloseCurlyBrackets();
                    
                    return stringBuilder.ToString();
                    
                    string Ident(int level) => StringUtilities.Indent(level);
                    void OpenCurlyBrackets() => stringBuilder.AppendLine($"{Ident(_identLevel++)}{{");
                    void CloseCurlyBrackets() => stringBuilder.AppendLine($"{Ident(--_identLevel)}}}");
                    void AddLine(string line) => stringBuilder.AppendLine($"{Ident(_identLevel)}{line}");
                }
            }
            
            public string GenerateFile(string path, string fileName)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                
                string fullPath = $"{path}/{fileName}.cs";
                File.WriteAllText(fullPath, Contents);
                return fullPath;
            }
        }
    }
}