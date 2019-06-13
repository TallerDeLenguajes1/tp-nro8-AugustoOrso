using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Empleados
{
    class Program
    {
        public enum estadoCivil { Soltero, Casado };
        public enum genero { Femenino, Masculino };
        public enum cargo { Auxiliar, Administrativo, Ingeniero, Especialista, Investigador };

        public struct datos
        {
            public string nombre;
            public string apellido;
            public DateTime fechaNacimiento;
            public estadoCivil estado;
            public genero gen;
            public DateTime fechaIngreso;
            public float sueldo;
            public cargo carg;

            public datos(string _nombre, string _apellido, DateTime _fechaNac, estadoCivil _estadoCivil, genero _genero, DateTime _fechaIngreso, float _sueldo, cargo _cargo)
            {
                nombre = _nombre;
                apellido = _apellido;
                fechaNacimiento = _fechaNac;
                estado = _estadoCivil;
                gen = _genero;
                fechaIngreso = _fechaIngreso;
                sueldo = _sueldo;
                carg = _cargo;

            }

            public void Mostrar(datos Empleado)
            {
                Console.WriteLine(Empleado.nombre);
                Console.WriteLine(Empleado.apellido);
                Console.WriteLine(Empleado.fechaNacimiento.ToShortDateString());
                Console.WriteLine(Empleado.estado);
                Console.WriteLine(Empleado.gen);
                Console.WriteLine(Empleado.fechaIngreso.ToShortDateString());
                Console.WriteLine(Empleado.sueldo);
                Console.WriteLine(Empleado.carg);

            }

            public int tiempo(DateTime Fecha) // Funcion para calculo de tiempo en años (edad y Antiguedad)
            {
                DateTime inicio = new DateTime(1, 1, 1);
                DateTime hoy = DateTime.Today;
                TimeSpan edad = (hoy - Fecha);
                int tiempo = (inicio + edad).Year;
                return tiempo;
            }

            public int jubilacion(genero _genere, DateTime FechaNac)
            {
                int añosfaltantes;

                int edad = tiempo(FechaNac);

                if (_genere == 0)
                {
                    añosfaltantes = 60 - edad;
                }
                else
                {
                    añosfaltantes = 65 - edad;
                }
                return añosfaltantes;
            }

            public double Salario(int antiguedad, cargo _cargo, int hijos, estadoCivil _estado)
            {
                double salario, basico = this.sueldo, adicional = 0;


                if (antiguedad < 20)
                {
                    adicional = adicional + (antiguedad * 0.02 * basico);

                }
                else
                {
                    adicional = adicional + (basico * 0.25);
                }

                if (Convert.ToInt32(_cargo) == 2 || Convert.ToInt32(_cargo) == 3)
                {
                    adicional = adicional + (adicional * 0.5);
                }
                if (Convert.ToInt32(_estado) == 1 && hijos > 2)
                {
                    adicional = adicional + 5000;
                }

                salario = basico + adicional;
                return salario;
            }

            public static void escribirEnArchivo(string ruta, datos _empleado)
            {
                string fecha;
                using (StreamWriter file = new StreamWriter(ruta, true))
                {
                    Random rnd = new Random();
                    file.Write(_empleado.nombre + ";");
                    file.Write(_empleado.apellido + ";");
                    fecha = Convert.ToDateTime(_empleado.fechaNacimiento).ToString("dd/MM/yyyy");
                    file.Write(fecha + ";");
                    file.Write(_empleado.estado + ";");
                    file.Write(_empleado.gen + ";");
                    fecha = Convert.ToDateTime(_empleado.fechaIngreso).ToString("dd/MM/yyyy");
                    file.Write(fecha + ";");
                    file.Write(_empleado.sueldo + ";");
                    file.Write(_empleado.carg + ";");
                    int hijos = rnd.Next(0, 10);
                    file.Write(hijos + ";");
                    file.Write("\n");
                    file.Close();
                }
            }
            static void Main(string[] args)
            {
                datos nuevoEmpleado;
                int dia, mes, año;
                estadoCivil estado;
                genero Gen;
                cargo carg;
                int cont = 0;
                double total = 0;
                string ruta = @"C:\Users\Turbo LTSB 64\Desktop\Facultad\Programador\Taller de Lenguaje 1\Practica\TP-8\tp-nro8-AugustoOrso\TP-8-Orso\Registro.csv";
                string[] Apellidos = new string[] { "Blanco", "Sanagua", "Orso" };
                string[] NombresMasc = new string[] { "Juan", "Pablo", "Sebastian" };
                string[] NombresFem = new string[] { "Tatiana", "Maria", "Sofia" };

                Random rnd = new Random();
                int hijos = rnd.Next(0, 10);
                List<datos> ListadeEmpleados = new List<datos>();


                if (File.Exists(ruta))
                {
                    Console.Write("Registro.csv ya existe");
                }
                else
                {
                   File.Create(ruta);
                }

                for (int i = 0; i < 20; i++)
                {
                    dia = rnd.Next(1, 31);
                    mes = rnd.Next(1, 13);
                    año = rnd.Next(1970, 2000);

                    DateTime fecha = new DateTime(año, mes, dia);

                    dia = rnd.Next(1, 31);
                    mes = rnd.Next(1, 13);
                    año = rnd.Next(2000, 2019);

                    DateTime fechaIn = new DateTime(año, mes, dia);

                    estado = (estadoCivil)rnd.Next(0, 1);
                    Gen = (genero)rnd.Next(0, 1);
                    carg = (cargo)rnd.Next(0, 5);

                    if (Gen == genero.Masculino)
                    {
                        nuevoEmpleado = new datos(NombresMasc[rnd.Next(0, 3)], Apellidos[rnd.Next(0, 3)], fecha, estado, Gen, fechaIn, rnd.Next(10000, 20000), carg);
                    }
                    else
                    {
                        nuevoEmpleado = new datos(NombresFem[rnd.Next(0, 3)], Apellidos[rnd.Next(0, 3)], fecha, estado, Gen, fechaIn, rnd.Next(10000, 20000), carg);
                    }

                    ListadeEmpleados.Add(nuevoEmpleado);

                }
               // string Empleados = Convert.ToString(ListadeEmpleados);

               // File.WriteAllLines(ruta, Empleados);

                foreach (datos empleado in ListadeEmpleados)
                {
                    empleado.Mostrar(empleado);
                    Console.Write("\n");
                    Console.WriteLine("La edad es: {0}", empleado.tiempo(empleado.fechaNacimiento));
                    Console.Write("\n");
                    Console.WriteLine("La antiguedad es: {0}", empleado.tiempo(empleado.fechaIngreso));
                    Console.Write("\n");
                    empleado.jubilacion(empleado.gen, empleado.fechaNacimiento);
                    Console.Write("\n");
                    Console.WriteLine("El salario del empleado es: ${0}", empleado.Salario(empleado.tiempo(empleado.fechaIngreso), empleado.carg, hijos, empleado.estado));
                    Console.Write("\n");

                    total = total + empleado.Salario(empleado.tiempo(empleado.fechaIngreso), empleado.carg, hijos, empleado.estado);
                    escribirEnArchivo(ruta, empleado);
                    Console.ReadKey();
                    cont = cont + 1;
                }
                Console.Write("\n");
                Console.WriteLine("La empresa tiene {0} empleados.", cont);
                Console.Write("\n");
                Console.WriteLine("La empresa gasta ${0} en salarios.", total);

                

                Console.ReadKey();

            }

        }
    }
}

