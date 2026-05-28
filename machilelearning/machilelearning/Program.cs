using System.ComponentModel;
using System.Transactions;

namespace Percettronev1
{
    internal class Program
    {
        const int domande = 5;
        const double theresold = 0.5;
        const double learning_rate = 0.1;
        static double[] pesi = new double[domande] {0.7, 0.6, 0.5, 0.3, 0.4 };
        static double bias = 0.5;
        static void Main(string[] args)
        {
            string scelta = "";

            Console.WriteLine("premere a se vuoi addestrare il percettrone e p se vuoi usarlo");
            scelta = Console.ReadLine();

            if(scelta == "p")
            {
                int[] input = new int [domande];
                Console.WriteLine("artista famoso? (1=si, 0=no);");
                input[0] = int.Parse(Console.ReadLine());
                Console.WriteLine("bel meteo? (1=si, 0=no);");
                input[1] = int.Parse(Console.ReadLine());
                Console.WriteLine("amici presenti? (1=si, 0=no);");
                input[2] = int.Parse(Console.ReadLine());
                Console.WriteLine("cibo buono? (1=si, 0=no);");
                input[3] = int.Parse(Console.ReadLine());
                Console.WriteLine("si puo bere? (1=si, 0=no);");
                input[4] = int.Parse(Console.ReadLine());

                int decisione = prevedi(input);

                if (decisione == 0)
                    Console.WriteLine("non andare al concerto");
                else Console.WriteLine("vai al concerto");

            }
            else
            {
                StreamReader sr = new StreamReader("Esempio.txt");
                
                int carattere;

                while ((carattere=sr.Read()) != -1)
                {
                    int[] input = new int[domande];
                    input[0] = sr.Read() - 48;
                    input[1] = sr.Read() - 48;
                    input[2] = sr.Read() - 48;
                    input[3] = sr.Read() - 48;
                    input[4] = sr.Read() - 48;
                    int risposta = sr.Read() - 48;

                    double sum = bias;

                    for (int j = 0; j < domande; j++)
                        sum += input[j] * pesi[j];

                    int output = attivazione(sum);
                    int error = risposta - output;

                    for(int j = 0; j < domande; j++)
                    {
                        pesi[j] += learning_rate * error * input[j];
                    }

                    bias += learning_rate * error;
                }

                Console.WriteLine("i pesi calcolati sono:" + pesi[0] + " " + pesi[1] + " " + pesi[2] + " " + pesi[3] + " " + pesi[4]);
            }

        }

        static int attivazione(double x)
        {
            if (x > theresold)
                return 1;
            else return 0;
        }

        static int prevedi(int[] input)
        {
            double somma = bias;

            for(int i=0; i<domande; i++)
            {
                somma += input[i] * pesi[i];
            }
            return attivazione(somma);
        }


    }
}