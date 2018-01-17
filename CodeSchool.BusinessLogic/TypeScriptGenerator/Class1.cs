//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace CodeSchool.BusinessLogic.TypeScriptGenerator
//{
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
//    public class TypeScriptModelAttribute : Attribute { }

//    [AttributeUsage(AttributeTargets.Class)]
//    public class TypeScriptServiceAttribute: Attribute { }

//    public static class BetaGenerator
//    {
//        private const string ClassTemplate = @"
//import { Mapper } from '../ utils / helpers';
//export class #name#ViewModel {
//    constructor(model?: #name#ViewModel) {
//        if (model) {
//            Mapper.map(model, this);
//        }
//    }

//    #properties#
//}";

//        public static void Test(string modelFolder)
//        {
//            var assembly = Assembly.Load("CodeSchool.Web");
//            var modelTypes =
//                assembly.GetTypes().Where(t => t.GetCustomAttribute<TypeScriptModelAttribute>() != null);
//            var serviceTypes = assembly.GetTypes().Where(t => t.GetCustomAttribute<TypeScriptServiceAttribute>() != null);

//            foreach (var modelType in modelTypes)
//            {
//                if (modelType.IsEnum)
//                {
//                    var enums = modelType.GetEnumNames();
//                    foreach (var enumName in enums)
//                    {
                        
//                    }
//                }
//                else
//                {
//                    var publicProperties = modelType.GetProperties();
//                    var tsProperties = new StringBuilder();
//                    var tsClass = ClassTemplate;
//                    foreach (var publicProperty in publicProperties)
//                    {
//                        var tsType = GetTsType(publicProperty.PropertyType);
//                        tsProperties.AppendLine($"{ToPascalNotation(publicProperty.Name)}: {tsType};");
//                    }

//                    tsClass = tsClass
//                        .Replace("#name#", modelType.Name)
//                        .Replace("#properties#", tsProperties.ToString());

//                    //File.WriteAllText(modelFolder + "/" + ToPascalNotation(modelType.Name) + ".ts", tsClass);
//                }
//            }
//        }

//        private static string GetTsType(Type type)
//        {
//            if (type == typeof(Int32)
//                || type == typeof(Int64)
//                || type == typeof(Double) 
//                || type == typeof(Decimal))
//            {
//                return "number";
//            }
//            else if (type == typeof(Boolean))
//            {
//                return "boolean";
//            }
//            else if (type == typeof(String))
//            {
//                return "string";
//            }

//            return "any";
//        }

//        private static string ToPascalNotation(string name)
//        {
//            var firstLetter = new String(new[] {name[0]});
//            return name.Replace(name[0], firstLetter.ToLower()[0]);
//        }
//    }
//}
