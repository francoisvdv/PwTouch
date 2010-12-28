using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;

namespace PwTouchInputProvider
{
    public static class DetectorManager
    {
        static CSharpCodeProvider csCompiler = new CSharpCodeProvider();

        static string[] References = new string[] {
            "System.dll", "System.Drawing.dll", "System.Xml.dll", "System.Windows.Forms.dll",
            "PwTouchInputProvider.dll",
            "AForge.dll", 
            "AForge.Imaging.dll", "AForge.Imaging.Formats.dll",
            "AForge.Video.dll", "AForge.Video.DirectShow.dll" };

        public static string DetectorDirectory
        {
            get { return Global.AppDataFolder + @"\DetectorScripts\"; }
        }

        public static DetectorBase LoadDetectorWithoutExceptions(string assemblyPath)
        {
            try
            {
                return LoadDetector(assemblyPath);
            }
            catch
            {
                return null;
            }
        }
        public static DetectorBase LoadDetector(string assemblyPath)
        {
            byte[] rawAssembly;
            using (FileStream fs = new FileStream(assemblyPath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                rawAssembly = new byte[fs.Length];
                fs.Read(rawAssembly, 0, (int)fs.Length);
            }

            Assembly asm = Assembly.Load(rawAssembly);

            if (asm == null)
                return null;

            DetectorBase detector = DetectorManager.LoadDetector(asm);
            if (detector == null)
            {
                throw new TypeLoadException("Kon detector niet vinden in assembly.");
            }

            return detector;
        }
        public static DetectorBase LoadDetector(Assembly asm)
        {
            foreach (Type t in asm.GetTypes())
            {
                if (t.BaseType == typeof(DetectorBase))
                    return (DetectorBase)Activator.CreateInstance(t);
            }

            return null;
        }

        public static string GetScript(string fileName)
        {
            return File.ReadAllText(DetectorDirectory + fileName + ".txt");
        }
        public static string GetDefaultScript()
        {
            string filePath = "PwTouchInputProvider.DefaultDetector.txt";
            Stream fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filePath);

            using (StreamReader sr = new StreamReader(fileStream))
            {
                return sr.ReadToEnd();
            }
        }

        public static void SaveScriptText(string fileName, string script)
        {
            if (!Directory.Exists(DetectorDirectory))
                Directory.CreateDirectory(DetectorDirectory);

            using (FileStream fs = new FileStream(DetectorDirectory + fileName + ".txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(script);
                }
            }
        }
        public static CompilerResults SaveScriptAssembly(string fileName, string script)
        {
            if (!Directory.Exists(DetectorDirectory))
                Directory.CreateDirectory(DetectorDirectory);

            CompilerParameters parameters = new CompilerParameters(References, DetectorDirectory + fileName + ".dll");
            parameters.GenerateInMemory = true;
            CompilerResults r = csCompiler.CompileAssemblyFromSource(parameters, new string[] { script });
            return r;
        }
    }
}
