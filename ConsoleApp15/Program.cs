using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BizLayer;
using DataLayer;
using Mapster;
using MapsterMapper;

namespace ConsoleApp15
{
    class Program
    {
        static void Main(string[] args)
        {
            var simpleInfo = new SimpleInfo()
            {
                Id = 1,
                Name = "test"
            };

            var simpleBiz = simpleInfo.Adapt<Simple>();
            //Simple simpleBiz = new Simple();
            //simpleInfo.Adapt(simpleBiz);

            var config = new TypeAdapterConfig();
            var assemblies = GetAssemblies();
            //映射规则
            config.Scan(assemblies);

            var mapper = new Mapper(config); //务必将mapper设为单实例, 保存到IOC容器中

            

            var simple = mapper.Map<Simple>(simpleInfo);
            var view = mapper.Map<SimpleView>(simple);



            var info = new UserInfo()
            {
                Age = 8,
                like = "fasf",
                Name = "Nestor",
                Sex = "Male"
            };

            var dto = mapper.Map<User>(info);
            var userView = mapper.Map<UserView>(dto);
        }

        private static Assembly[] GetAssemblies()
        {
            var results = new List<Assembly>();

            // AppDomain.CurrentDomain.GetAssemblies() does not work here because some DLLs might not be loaded into 
            // the current app domain yet, so enumerate all the DLLs in current app folder
            var assemblyFolder = AppDomain.CurrentDomain.RelativeSearchPath;
            if (string.IsNullOrWhiteSpace(assemblyFolder))
            {
                assemblyFolder = AppDomain.CurrentDomain.BaseDirectory;
            }

            var dllFiles = Directory.GetFiles(assemblyFolder, "*.dll").ToList();
            var exeFiles = Directory.GetFiles(assemblyFolder, "*.exe").ToList();
            dllFiles.AddRange(exeFiles);
            foreach (var file in dllFiles)
            {
                try
                {
                    var assembly = Assembly.Load(Path.GetFileNameWithoutExtension(file));

                    if (!IsSystemAssembly(assembly))
                    {
                        results.Add(assembly);
                    }
                }
                catch
                {
                    // Squash any exceptions
                }
            }
            return results.ToArray();
        }

        private static bool IsSystemAssembly(Assembly assembly)
        {
            if (assembly == null
                || assembly.FullName.StartsWith("mscorlib.")
                || assembly.FullName.StartsWith("System.")
                || assembly.FullName.StartsWith("Microsoft."))
            {
                return true;
            }
            return false;
        }
    }
}