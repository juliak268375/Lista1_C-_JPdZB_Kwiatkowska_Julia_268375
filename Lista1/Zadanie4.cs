/* Autor: Julia Kwiatkowska, 268375
 * Linki, z ktorych korzystalam, w celu rozwiazania tego zadania:
 * ciag Fibonacciego: https://pl.wikipedia.org/wiki/Ci%C4%85g_Fibonacciego
 * typy liczb: https://learn.microsoft.com/pl-pl/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
 * add: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.add?view=net-9.0
 * komentarze napisalam w jezyku polskim, ale nie korzystalam z polskich znakow alfabetycznych
 */
using System;
using System.Diagnostics;


namespace Lista1
{
    /// <summary>
    /// Klasa Zadanie 4 z implementacja metod pozwalajacych na obliczenie n pierwszych elementow ciagu Fobonacciego z wykorzystaniem rekurencji i iteracji oraz zawiera odpowiednie sprawdzenie poprawnosci jej dzialania.
    /// </summary>
    class Zadanie4
    {
        /// <summary>
        /// Metoda do iteracyjnej implementacji obliczania n pierwszych elementow ciagu Fibonacciego.
        /// Przyjmuje jako pierwszy element 0, jako drugi 1, zgodnie z definicja ciagu Fibonacciego (0,1,1,2,3,5...).
        /// W przypadku, gdy zadane n jest <= 0 rzucany jest wyjatek ArgumentException.
        /// </summary>
        /// <param name="n">Liczba n pierwszych elementow ciagu Fibonacciego do wygenerowania (n > 0)</param>
        /// <returns>Lista typu long, ktora zawiera n pierwszych elementow ciagu Fibonacciego</returns>
        /// <exception cref="ArgumentException">Wyjatek rzucany, gdy n <=0. </exception>
        public static List<long> FibonacciIteracyjnie(int n)
        {
            if (n <= 0)
                throw new ArgumentException("Liczba elementow ciagu Fibonacciego do wygenerowania musi byc wieksza od zera.", nameof(n));
            List<long> fibonacciIter = new List<long>(n);

            // Do listy elementow dodany pierwszy element (0)
            fibonacciIter.Add(0);
            if (n == 1)
                return fibonacciIter;

            // Do listy elementow dodany drugi element (1)
            fibonacciIter.Add(1);

            // Dla każdego kolejnego elementu ciagu wykorzystana iteracja
            for (int i = 2; i<n; i++)
            {
                long nastepnyElement = fibonacciIter[i - 1] + fibonacciIter[i - 2];
                fibonacciIter.Add(nastepnyElement);
            }
            return fibonacciIter;
        }
        /// <summary>
        /// Funkcja pomocnicza rekurencyjna, ktora zwraca n-ty element ciagu Fibonacciego
        /// F(0)=0, F(1)=1, F(n)=F(n-1)+F(n-2) dla n>=0. Wyjatek rzucany, gdy n jest ujemne
        /// </summary>
        /// <param name="n">Indeks elementu ciagu Fibonacciego (n>=0)</param>
        /// <returns>zwraca n-ty element ciagu Fibonacciego</returns>
        /// <exception cref="ArgumentException">Wyjatek rzucany, gdy n<0.</exception>
        private static long FibonacciRekurencyjnieObliczenie(int n)
        {
            if (n < 0)
                throw new ArgumentException("n nie moze byc ujemne", nameof(n));
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            return FibonacciRekurencyjnieObliczenie(n - 1) + FibonacciRekurencyjnieObliczenie(n - 2);
        }


        /// <summary>
        /// Metoda do rekurencyjnej implementacji obliczania n pierwszych elementow ciagu Fibonacciego.
        /// Dla kazdego indeksu i (0<=i<n) wywoluje ona funkcje FibonacciRekurencyjnieObliczenie(i).
        /// W przypadku, gdy zadane n jest <= 0 rzucany jest wyjatek ArgumentException.
        /// </summary>
        /// <param name="n">Liczba n pierwszych elementow ciagu Fibonacciego do wygenerowania (n > 0)</param>
        /// <returns>Lista typu long, ktora zawiera n pierwszych elementow ciagu Fibonacciego</returns>
        /// <exception cref="ArgumentException">Wyjatek rzucany, gdy n<=0.</exception>
        public static List<long> FibonacciRekurencyjnie(int n)
        {
            if (n <= 0)
                throw new ArgumentException("Liczba elementow ciagu Fibonacciego do wygenerowania musi byc wieksza od zera.", nameof(n));
            List<long> fibonacciRekur = new List<long>(n);
            for(int i = 0; i<n; i++)
            {
                fibonacciRekur.Add(FibonacciRekurencyjnieObliczenie(i));
            }
            return fibonacciRekur;
        }

        /// <summary>
        /// Funkcja testujaca poprawnosc dzialania iteracyjnej implementacji obliczania n elementow ciagu Fibonacciego.
        /// Testuje zarowno przypadki typowe, brzegowy, jak i niepoprawne argumenty.
        /// Wyniki testow sa wypisywane na konsole i zastosowano asercje w celu automatycznej weryfikacji poprawnosci.
        /// </summary>

        public static void TestFibonacciIteracyjnie()
        {
            try
            {
                // Test nr 1 dla typowego przypadku n = 6
                int n = 6;
                List<long> fibonacci = FibonacciIteracyjnie(n);
                List<long> oczekiwany = new List<long> { 0, 1, 1, 2, 3, 5 };
                Debug.Assert(fibonacci.SequenceEqual(oczekiwany), "Test nr 1 dla metody iteracyjnej (przypadek typowy dla n=6) zakonczony niepowodzeniem!");
                Console.WriteLine("Test nr 1 dla metody iteracyjnej (przypadek typowy dla n=6) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 1 dla metody iteracyjnej (przypadek typowy dla n=6) zakonczony niepowodzeniem: " + ex.Message);
            }
            try
            {
                // Test nr 2 dla brzegowego przypadku n = 1
                int n =1;
                List<long> fibonacci = FibonacciIteracyjnie(n);
                Debug.Assert(fibonacci.Count == 1 && fibonacci[0] == 0, "Test nr 2 dla metody iteracyjnej (przypadek brzegowy dla n=1) zakonczony niepowodzeniem!");
                Console.WriteLine("Test nr 2 dla metody iteracyjnej (przypadek brzegowy dla n=1) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 2 dla metody iteracyjnej (przypadek brzegowy dla n=1) zakonczony niepowodzeniem: " + ex.Message);
            }
            try
            {
                // Test nr 3 dla niepoprawnego n = 0 (oczekiwane rzucenie wyjatku)
                FibonacciIteracyjnie(0);
                Console.WriteLine("Test nr 3 dla metody iteracyjnej (dla n = 0) zakonczony niepowodzeniem.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Test nr 3 dla metody iteracyjnej (dla n = 0) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 3 dla metody iteracyjnej (dla n = 0) zakonczony niepowodzeniem." + ex.Message);
            }
            try
            {
                // Test nr 4 dla niepoprawnego n = -3 (oczekiwane rzucenie wyjatku)
                FibonacciIteracyjnie(-3);
                Console.WriteLine("Test nr 4 dla metody iteracyjnej (dla n = -3) zakonczony niepowodzeniem.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Test nr 4 dla metody iteracyjnej (dla n = -3) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 4 dla metody iteracyjnej (dla n = -3) zakonczony niepowodzeniem." + ex.Message);
            }

        }
        /// <summary>
        /// Funkcja testujaca poprawnosc dzialania rekurencyjnej implementacji obliczania n elementow ciagu Fibonacciego.
        /// Testuje zarowno przypadki typowe, brzegowy jak i niepoprawne argumenty.
        /// Wyniki testow sa wypisywane na konsole i zastosowano asercje w celu automatycznej weryfikacji poprawnosci.
        /// </summary>

        public static void TestFibonacciRekurencyjnie()
        {
            try
            {
                // Test nr 1 dla typowego przypadku n = 6
                int n = 6;
                List<long> fibonacci = FibonacciRekurencyjnie(n);
                List<long> oczekiwany = new List<long> { 0, 1, 1, 2, 3, 5 };
                Debug.Assert(fibonacci.SequenceEqual(oczekiwany), "Test nr 1 dla metody rekurencyjnejjnej (przypadek typowy dla n=6) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 1 dla metody rekurencyjnej (przypadek typowy dla n=6) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 1 dla metody rekurencyjnej (przypadek typowy dla n=6) zakonczony niepowodzeniem: " + ex.Message);
            }
            try
            {
                // Test nr 2 dla brzegowego przypadku n = 1
                int n = 1;
                List<long> fibonacci = FibonacciRekurencyjnie(n);
                Debug.Assert(fibonacci.Count == 1 && fibonacci[0] == 0, "Test nr 2 dla metody rekurencyjnej (przypadek brzegowy dla n=1) zakonczony niepowodzeniem!");
                Console.WriteLine("Test nr 2 dla metody rekurencyjnej (przypadek brzegowy dla n=1) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 2 dla metody rekurencyjnej (przypadek brzegowy dla n=1) zakonczony niepowodzeniem: " + ex.Message);
            }
            try
            {
                // Test nr 3 dla niepoprawnego n = 0 (oczekiwane rzucenie wyjatku)
                FibonacciRekurencyjnie(0);
                Console.WriteLine("Test nr 3 dla metody rekurencyjnej (dla n = 0) zakonczony niepowodzeniem.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Test nr 3 dla metody rekurencyjnej (dla n = 0) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 3 dla metody rekurencyjnej (dla n = 0) zakonczony niepowodzeniem." + ex.Message);
            }
            try
            {
                // Test nr 4 dla niepoprawnego n = -3 (oczekiwane rzucenie wyjatku)
                FibonacciRekurencyjnie(-3);
                Console.WriteLine("Test nr 4 dla metody rekurencyjnej (dla n = -3) zakonczony niepowodzeniem.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Test nr 4 dla metody rekurencyjnej (dla n = -3) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 4 dla metody rekurencyjnej (dla n = -3) zakonczony niepowodzeniem." + ex.Message);
            }
            Console.WriteLine("Udalo sie, wszystkie przeprowadzone testy zakonczyly sie sukcesem!");
        }

        /// <summary>
        /// Punkt wejscia do programu, ktory wywoluje funkcje testujaca dzialanie funkcji zwracajacej n elementow ciagu Fibonacciego iterayjnie i rekurencyjnie wyznaczonych.
        /// </summary>
        /// <param name="args">Argumenty linii polecen.</param>
        /*public static void Main(string[] args)
        {
            //Wywolanie funkcji testujacych obliczanie pierwszych n elementow ciagu Fibonacciego iteracyjnie i rekurencyjnie.
            TestFibonacciIteracyjnie();
            TestFibonacciRekurencyjnie();
            Console.WriteLine("Prosze wcisnac dowolny klawisz, by zakonczyc.");
            Console.ReadKey();
        }*/
    }
}
