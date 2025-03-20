/* Autor: Julia Kwiatkowska, 268375
 * Linki, z ktorych korzystalam, w celu rozwiazania tego zadania:
 * warunek budowy trojkata: https://zadaniacke.pl/teoria/warunek-budowy-trojkata/
 * wzor Herona na pole trojkata: https://pl.wikipedia.org/wiki/Wz%C3%B3r_Herona
 * Debug.Assert: https://learn.microsoft.com/pl-pl/dotnet/api/system.diagnostics.debug.assert?view=net-8.0
 * Math.Abs: https://learn.microsoft.com/pl-pl/dotnet/api/system.math.abs?view=net-9.0
 * metody w C#: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/methods
 * dokumentacja kodu w C#: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
 * komentarze napisalam w jezyku polskim, ale nie korzystalam z polskich znakow alfabetycznych
 */
using System.Diagnostics;

namespace Lista1
{
    /// <summary>
    /// Klasa Zadanie 1 zawiera implementacje metody pozwalajacej na obliczenie pola trojkata wykorzystujac wzor Herona oraz zawiera odpowiednie sprawdzenie poprawnosci jej dzialania. 
    /// </summary>
    class Zadanie1
    {
        /// <summary>
        /// Metoda Heron oblicza pole trojkata z wykorzystaniem wzoru Herona, sprawdzajac przy tym
        /// czy podane boki trojkata spelniaja warunek budowy trojkata i czy sa dodatnie, jesli nie rzuca wyjatek.
        /// </summary>
        /// <param name="a"> Bok trojkata a.</param>
        /// <param name="b">Bok trojkata b.</param>
        /// <param name="c">Bok trojkata c.</param>
        /// <returns>Obliczone pole trojkata.</returns>
        /// <exception cref="ArgumentException">
        /// Wyjatek rzucany, wtedy kiedy boki trojkata nie spelniaja warunku budowy trojkata lub gdy sa ujemne, lub rowne zero.
        /// </exception>
        public static double Heron(double a, double b, double c)
        {
            // Sprawdzenie, czy podane boki sa wieksze od zera.
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentException("Boki trojkata nie moga byc <= 0, musza to byc wartosci dodatnie!");
            }
            // Sprawdzenie, czy podane boki spelniaja warunek budowy trojkata.
            if (a + b <= c || b + c <= a || a + c <= b)
            {
                throw new ArgumentException("Boki trójkąta muszą spełniać tzw. warunek budowy trojkata, tzn. najdluzszy z bokow musi byc krotszy od sumy dlugosci dwoch pozostalych!");
            }
            // Obliczenie polowy obwodu trojkata (oznaczenie p zgodne z nomenklatura zastosowana w Wikipedii).
            double p = (a + b + c) / 2;

            // Obliczenie pola trojkata z wykorzystaniem wzoru Herona.
            double S = Math.Sqrt(p * (p - a)*(p - b)*(p - c));
            return S;
        }
        /// <summary>
        /// Funkcja testujaca poprawnosc dzialania metody Heron.
        /// Jej zadaniem jest sprawdzenie dzialania tej metody zarowno dla przypadkow typowych, jak i brzegowych.
        /// Funkcja testowa zostala napisana tak, by wyniki testow byly wypisywane na konsole. Zastosowano asercje w celu automatycznej weryfikacji poprawnosci.
        /// </summary>
        public static void TestHeron()
        {
            try
            {
                // Test nr 1 dla typowego przypadku trojkata prostokatnego (pitagorejskiego) o bokach 6, 8, 10 (pole powinno wynieść 24).
                double S = Heron(6, 8, 10);
                Debug.Assert(Math.Abs(S - 24) < 1e-9, "Test nr 1 (typowego trójkata pitagorejskiego o bokach 6, 8, 10) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 1 (typowego trójkata pitagorejskiego o bokach 6, 8, 10) zakonczony sukcesem! Oczekiwane pole wynosi: 24, a obliczone: " + S);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 1 (typowego trójkata pitagorejskiego o bokach 6, 8, 10) zakonczony niepowodzeniem." + ex.Message);
            }

            try
            {
                // Test dla trojkata rownobocznego o bokach 4, 4, 4 (pole powinno wynieść 4 pierw z 3).
                // Wzor na pole trojkata rownobocznego: a^2*(pier z 3/4).
                double S = Heron(4, 4, 4);
                double teoretycznePole = Math.Sqrt(3) / 4 * (4 * 4);
                Debug.Assert(Math.Abs(S - teoretycznePole) < 1e-9, "Test nr 2 (trojkata rownobocznego o bokach 4, 4, 4) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 2 (trojkata rownobocznego o bokach 4, 4, 4) zakonczony sukcesem! Oczekiwane pole wynosi: " + teoretycznePole + ", a obliczone pole: " + S);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 2 (trojkata rownobocznego o bokach 4, 4, 4) zakonczony niepowodzeniem." + ex.Message);
            }


            try
            {
                // Test dla trojkata o bardzo małych bokach 0.0001, 0.0001, 0.0001 - przypadek graniczny.
                double S = Heron(0.0001, 0.0001, 0.0001);
                double teoretycznePole = Math.Sqrt(3) / 4 * (0.0001 * 0.0001);
                Debug.Assert(Math.Abs(S - teoretycznePole) < 1e-9, "Test nr 3 (trojkat o bardzo malych bokach 0.0001, 0.0001, 0.0001) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 3 (trojkat o bardzo malych bokach 0.0001, 0.0001, 0.0001) zakonczony sukcesem! Oczekiwane pole wynosi: " + teoretycznePole + ", a obliczone pole: " + S);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 3 (trojkat o bardzo malych bokach 0.0001, 0.0001, 0.0001) zakonczony niepowodzeniem." + ex.Message);
            }


            // Test sprawdzajacy rzucenie wyjatku, gdy jeden z bokow trojkata jest ujemny.
            try
            {
                Heron(-2, 3, 4);
                Debug.Assert(false, "Test nr 4 sie nie powiodl, powinien wystapic wyjatek zwiazany z ujemnym bokiem");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine("Test nr 4 się powiodl - zostal rzucony wlasciwy wyjatek dla ujemnego boku: " + ex.Message);
            }

            // Test sprawdzajacy rzucenie wyjatku, gdy jeden z bokow trojkata jest zerem.
            try
            {
                Heron(2, 0, 4);
                Debug.Assert(false, "Test nr 5 sie nie powiodl, powinien wystapic wyjatek zwiazany z zerowym bokiem");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test nr 5 się powiodl - zostal rzucony wlasciwy wyjatek dla zerowego boku: " + ex.Message);
            }

            // Test sprawdzajacy rzucenie wyjatku, gdy boki nie spelniaja warunku budowy trojkata.
            try
            {
                Heron(3, 5, 8);
                Debug.Assert(false, "Test nr 6 sie nie powiodl, powinien wystapic wyjatek zwiazany z tym, ze podane boki nie spelniaja warunku budowy trojkata");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test nr 6 się powiodl - zostal rzucony wlasciwy wyjatek dla bokow niespelniajacych warunku budowy trojkata: " + ex.Message);
            }
            Console.WriteLine("Udalo sie, wszystkie przeprowadzone testy zakonczyly sie sukcesem!");
        }
        /// <summary>
        /// Punkt wejscia do programu, ktory wywoluje funkcje testujaca dzialanie funkcji obliczajacej pole trojkata z wykorzystaniem wzoru Herona.
        /// </summary>
        /// <param name="args">Argumenty linii polecen.</param>
      public static void Main(string[] args)
        {
            TestHeron();
            Console.WriteLine("Prosze wcisnac dowolny klawisz, by zakonczyc.");
            Console.ReadKey();
        }

    }
}
