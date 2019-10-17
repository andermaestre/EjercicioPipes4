using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;

namespace Hijo
{
    class Program
    {
        static void Main(string[] args)
        {
            int IdPadre;
            int cont;
            using (AnonymousPipeClientStream pipe = new AnonymousPipeClientStream(PipeDirection.In, args[0]))
            {
                using (StreamReader sr = new StreamReader(pipe))
                {
                    IdPadre = int.Parse(sr.ReadLine());
                    cont = int.Parse(sr.ReadLine());

                }
            }
            if (cont % 2 == 0)
            {
                ProcessStartInfo psi = new ProcessStartInfo();
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
                        
                        sw.WriteLine(cont+1);
                        

                    }
                }
                
            }
            if (cont == 1) {
                Console.SetCursorPosition(0, 10);
            }else if (cont == 2)
            {
                Console.SetCursorPosition(15, 10);
            }
            else
            {
                Console.SetCursorPosition(20, 20);
            }
            Console.WriteLine("Hijo{0}", cont);
            Console.SetCursorPosition(3, Console.WindowHeight - 5 + cont);
            Console.WriteLine("Yo soy el hijo {0}, mi padre es 	PID={1}, yo soy PID={2}", cont, IdPadre, Process.GetCurrentProcess().Id);
            cont++;
        }
    }
}
