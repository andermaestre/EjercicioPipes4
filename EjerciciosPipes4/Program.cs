using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;

namespace EjerciciosPipes4
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            int cont = 1;
            ProcessStartInfo psi = new ProcessStartInfo();
            Console.SetCursorPosition(7, 1);
            Console.WriteLine("padre");
            for (int i=0;i<2;i++)
            {
                using (AnonymousPipeServerStream pipe = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable))
                {
                    using (StreamWriter sw = new StreamWriter(pipe))
                    {

                        Process p = new Process();
                        psi.FileName = "..\\..\\..\\Hijo\\bin\\Debug\\Hijo.exe";
                        psi.Arguments = pipe.GetClientHandleAsString();
                        psi.UseShellExecute = false;
                        p.StartInfo = psi;
                        p.Start();
                        sw.WriteLine(Process.GetCurrentProcess().Id);
                        sw.WriteLine(cont);
                        
                        cont++;
                    }
                }
                
            }
            Console.ReadLine();

        }
    }
}
