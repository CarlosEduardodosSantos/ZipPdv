using System;
using System.Collections.Generic;
using System.Reflection;
using SAT.Interface;

namespace SAT
{
    public sealed class SatFactory
    {
        static Dictionary<string, Assembly> dic = new Dictionary<string, Assembly>();

        public static ISat CreateSat(string _satMarca)
        {
            string[] assemblyFullName = typeof(ISat).Assembly.ManifestModule.Name.Split('.');
            string assemblyName = "Zip.Sat";//assemblyFullName[assemblyFullName.Length - 2];
            string classNamespace = "SAT.Modelo";

            Assembly assembly;
            if (dic.ContainsKey(assemblyName))
                assembly = dic[assemblyName];
            else
            {
                assembly = Assembly.Load(assemblyName);
                dic[assemblyName] = assembly;
            }
            var ret = (ISat)(assembly).CreateInstance(classNamespace + "." + _satMarca);
            if (ret == null)
                throw new Exception("Assembly not found in SatFactory: " + typeof(ISat).FullName);
            return ret;
        }
    }
}
