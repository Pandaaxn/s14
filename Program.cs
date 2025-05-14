using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static s14.Program;

namespace s14
{
    internal class Program
    {

        public enum menu
        {
            Agregar = 1, Consultar, Eliminar, Actualizar, Mostrar, Contar, EnviarCorreo
        }
        static Dictionary<int, string> alumnosprogra = new Dictionary<int, string>();

        static void Main(string[] args)
        {
            while (true)
            {
                switch (men())
                {
                    case menu.Agregar:
                        agregar();
                        break;
                    case menu.Consultar:
                        consultar();
                        break;
                    case menu.Eliminar:
                        Eliminar();
                        break;
                    case menu.Actualizar:
                        actualizar();
                        break;
                    case menu.Mostrar:
                        Mostrar();
                        break;
                    case menu.Contar:
                        Console.WriteLine($"El numero de elementos es: {contar()}");
                        break;
                    case menu.EnviarCorreo:
                        EnviarCorreo();
                        break;
                    default:
                        break;

                }
            }
        }
        static menu men()
        {
            Console.WriteLine("1)Agregar");
            Console.WriteLine("2)Consultar");
            Console.WriteLine("3)Eliminar");
            Console.WriteLine("4)Actualizar");
            Console.WriteLine("5)Mostrar");
            Console.WriteLine("6)Contar");
            Console.WriteLine("7)Enviar correo");
            menu opc = (menu)Convert.ToInt32(Console.ReadLine());
            return opc;

        }
        static void agregar()
        {
            Console.WriteLine("Matricula");
            int matricula = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Nombre");
            string nombre = Console.ReadLine();
            alumnosprogra.Add(matricula, nombre);

        }
        static void consultar()
        {
            foreach (var i in alumnosprogra)
            {
                Console.WriteLine($"nombre{i}");
            }

        }
        static void Mostrar()
        {
            foreach (var i in alumnosprogra)
            {
                Console.WriteLine($"Matricula{i.Key}");
                Console.WriteLine($"Matricula{i.Value}");

            }
        }
        static void Eliminar()
        {
            Console.WriteLine("Matricula");
            int matricula = Convert.ToInt32(Console.ReadLine());
            if (alumnosprogra.Remove(matricula))

            {
                Console.WriteLine("Removido con exito");
            }
            else
            {
                Console.WriteLine("No existe esa matricula");
            }
        }
        static void actualizar()
        {
            Console.WriteLine("Matricula del alumno a actualzar");
            int matricula = Convert.ToInt32(Console.ReadLine());

            if (alumnosprogra.ContainsKey(matricula))
            {
                Console.WriteLine("Nuevo Nombre");
                string nuevonombre = Console.ReadLine();
                alumnosprogra[matricula] = nuevonombre;
                Console.WriteLine("alumno actualizado con exito");
            }
            else
            {
                Console.WriteLine("matricula no encontrada");
            }

        }
        static double contar()
        {
            return alumnosprogra.Count();

        }
        static void EnviarCorreo()
        {
            string remitente = "113449@alumnouninter.mx";
            string contraseña = "Lkjhmn27";
            string destinante = "ecorrales@uninter.edu.mx";

            StringBuilder cuerpo = new StringBuilder();
            cuerpo.AppendLine("Lista de alumnos registrados");
            foreach (var alumno in alumnosprogra)
            {
                cuerpo.AppendLine($"matricula: {alumno.Key}, nombre: {alumno.Value}");
            }
            MailMessage mensaje = new MailMessage(remitente, destinante, "Lista de alumnos", cuerpo.ToString());
            SmtpClient cliente = new SmtpClient("smtp.office365.com", 587)
            {
                Credentials = new NetworkCredential(remitente, contraseña),
                EnableSsl = true

            };
            try
            {
                cliente.Send(mensaje);
                Console.WriteLine("Correo enviado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo" + ex.Message);
            }
        }

    }
}
