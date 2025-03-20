/* Autor: Julia Kwiatkowska, 268375
 * Linki, z ktorych korzystalam, w celu rozwiazania tego zadania:
 * multizbior definicja: https://sjp.pl/multizbi%C3%B3r
 * List: https://learn.microsoft.com/pl-pl/dotnet/csharp/tour-of-csharp/tutorials/list-collection
 * ArgumentNullException: https://learn.microsoft.com/pl-pl/dotnet/api/system.argumentnullexception?view=net-7.0
 * try, catch, throw: https://learn.microsoft.com/pl-pl/dotnet/csharp/language-reference/statements/exception-handling-statements
 * for, foreach: https://learn.microsoft.com/pl-pl/dotnet/csharp/language-reference/statements/iteration-statements
 * Math.Min: https://learn.microsoft.com/pl-pl/dotnet/api/system.math.min?view=net-8.0
 * Dictionary: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-9.0
 * ContainsKey: https://learn.microsoft.com/pl-pl/dotnet/api/system.collections.generic.dictionary-2.containskey?view=net-8.0
 * komentarze napisalam w jezyku polskim, ale nie korzystalam z polskich znakow alfabetycznych
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista1
{
    /// <summary>
    /// Klasa Zadanie 2 zawiera implementacje metody pozwalajacej na zwracanie czesci wspolnej dwoch multizbiorow oraz zawiera odpowiednie sprawdzenie poprawnosci jej dzialania. 
    /// </summary>
    class Zadanie2
    {
        /// <summary>
        /// Metoda wspolne zwraca czesc wspolna dwoch multizbiorow x i y (liste int).
        /// Idea dzialania jest nastepujaca: dla kazdego elementu, ktory znajduje sie w obu listach (multizbiorach) dodajemy go do wyniku min(liczba wystapien w x, liczba wystapien w y) razy.
        /// Ponadto zaimpelmentowano rowniez sprawdzenie, czy argumenty przekazane do metody nie sa nullami.
        /// </summary>
        /// <param name="x">Pierwszy multizbior liczb calkowitych x.</param>
        /// <param name="y">Drugi multizbior liczb calkowitych y.</param>
        /// <returns>Lista liczb calkowitych, ktora stanowi czesc wspolna dwoch multizbiorow</returns>
        /// <exception cref="ArgumentNullException">Wyjatek rzucany, w momencie, gdy ktorys z przekazanych argumentow (multizbiorow) jest nullem.</exception>
        public static List<int> Wspolne(List<int> x, List<int>y)
        {
            if (x == null)
                throw new ArgumentNullException(nameof(x), "Argument multizbior x nie moze byc nullem.");
            if (y == null)
                throw new ArgumentNullException(nameof(y), "Argument multizbior y nie moze byc nullem.");

            // zliczenie wystapien elementow w multizbiorze x
            Dictionary<int, int> zliczanieX = new Dictionary<int, int>();
            foreach (int item in x)
            {
                if (zliczanieX.ContainsKey(item))
                    zliczanieX[item]++;
                else
                    zliczanieX[item] = 1;
            }
            // zliczenie wystapien elementow w multizbiorze y
            Dictionary<int, int> zliczanieY = new Dictionary<int, int>();
            foreach (int item in y)
            {
                if (zliczanieY.ContainsKey(item))
                    zliczanieY[item]++;
                else
                    zliczanieY[item] = 1;
            }

            // okreslenie czesci wspolnej dwoch multizbiorow
            List<int> czescWspolna = new List<int>();
            foreach (var para in zliczanieX)
            {
                int element = para.Key;
                int liczbaWystapienX = para.Value;
                if (zliczanieY.ContainsKey(element))
                {
                    int liczbaWystapienY = zliczanieY[element];
                    int minimalnaLiczbaWystapien = Math.Min(liczbaWystapienX, liczbaWystapienY);
                    for (int i =0; i < minimalnaLiczbaWystapien; i++)
                    {
                        czescWspolna.Add(element);
                    }
                }
            }
            return czescWspolna;


        }
        /// <summary>
        /// Funkcja testujaca poprawnosc dzialania metody Wspolne.
        /// Jej zadaniem jest sprawdzenie dzialania tej metody zarowno dla przypadkow typowych, jak i brzegowych.
        /// Funkcja testowa zostala napisana tak, by wyniki testow byly wypisywane na konsole. Zastosowano asercje w celu automatycznej weryfikacji poprawnosci.
        /// </summary>
        public static void TestWspolne()
        {
            try
            {
                // Test dla typowego przypadku: dwa multizbiory niepuste z czesciowo wspolnymi elementami
                List<int> zb1 = new List<int> { 2, 3, 3, 4, 5 };
                List<int> zb2 = new List<int> { 3, 3, 4, 4, 6 };
                List<int> spodziewanyWynik = new List<int> { 3, 3, 4 };
                List<int> wynik = Wspolne(zb1, zb2);
                spodziewanyWynik.Sort();
                wynik.Sort();
                Debug.Assert(wynik.SequenceEqual(spodziewanyWynik));
                Console.WriteLine("Test nr 1 (dwa multizbiory niepuste z czesciowo wspolnymi elementami) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 1 (dwa multizbiory niepuste z czesciowo wspolnymi elementami) zakonczony niepowodzeniem. ");
            }
            try
            {
                // Test dla przypadku: dwa multizbiory niepuste z wszystkimi rownolicznymi wspolnymi elementami
                List<int> zb1 = new List<int> { 1, 2, 3, 4, 5};
                List<int> zb2 = new List<int> { 1, 2, 3, 4, 5 };
                List<int> spodziewanyWynik = new List<int> { 1, 2, 3, 4, 5};
                List<int> wynik = Wspolne(zb1, zb2);
                spodziewanyWynik.Sort();
                wynik.Sort();
                Debug.Assert(wynik.SequenceEqual(spodziewanyWynik));
                Console.WriteLine("Test nr 2 (dwa multizbiory niepuste z wszystkimi rownolicznymi wspolnymi elementami) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 2 (dwa multizbiory niepuste z wszystkimi rownolicznymi wspolnymi elementami) zakonczony niepowodzeniem. ");
            }
            try
            {
                // Test dla przypadku: dwa multizbiory niepuste z wszystkimi ale roznolicznymi wspolnymi elementami
                List<int> zb1 = new List<int> { 1, 1, 1, 2, 3, 4, 5 };
                List<int> zb2 = new List<int> { 1, 2, 3, 4, 5 };
                List<int> spodziewanyWynik = new List<int> { 1, 2, 3, 4, 5 };
                List<int> wynik = Wspolne(zb1, zb2);
                spodziewanyWynik.Sort();
                wynik.Sort();
                Debug.Assert(wynik.SequenceEqual(spodziewanyWynik), "Test nr 3 (dwa multizbiory niepuste z wszystkimi ale roznolicznymi wspolnymi elementami) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 3 (dwa multizbiory niepuste z wszystkimi ale roznolicznymi wspolnymi elementami) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 3 (dwa multizbiory niepuste z wszystkimi ale roznolicznymi wspolnymi elementami) zakonczony niepowodzeniem.");
            }
            try
            {
                // Test dla przypadku: dwa multizbiory jednoelementowe z jednym wspolnym elementem
                List<int> zb1 = new List<int> {1};
                List<int> zb2 = new List<int> {1};
                List<int> spodziewanyWynik = new List<int> { 1};
                List<int> wynik = Wspolne(zb1, zb2);
                spodziewanyWynik.Sort();
                wynik.Sort();
                Debug.Assert(wynik.SequenceEqual(spodziewanyWynik), "Test nr 4 (dwa multizbiory jednoelementowe z jednym wspolnym elementem) zakonczony niepowodzeniem. ");
                Console.WriteLine("Test nr 4 (dwa multizbiory jednoelementowe z jednym wspolnym elementem) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 4 (dwa multizbiory jednoelementowe z jednym wspolnym elementem) zakonczony niepowodzeniem. ");
            }
            try
            {
                // Test dla przypadku: dwa multizbiory jednoelementowe bez wspolnego elementu
                List<int> zb1 = new List<int> { 1 };
                List<int> zb2 = new List<int> { 2 };
                List<int> spodziewanyWynik = new List<int> { };
                List<int> wynik = Wspolne(zb1, zb2);
                spodziewanyWynik.Sort();
                wynik.Sort();
                Debug.Assert(wynik.SequenceEqual(spodziewanyWynik), "Test nr 5 (dwa multizbiory jednoelementowe bez wspolnego elementu) zakonczony niepowodzeniem. ");
                Console.WriteLine("Test nr 5 (dwa multizbiory jednoelementowe bez wspolnego elementu) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 5 (dwa multizbiory jednoelementowe bez wspolnego elementu) zakonczony niepowodzeniem. ");
            }
            try
            {
                // Test dla przypadku: dwa multizbiory wieloelementowe bez wspolnego elementu
                List<int> zb1 = new List<int> { 1, 2, 3, 4 };
                List<int> zb2 = new List<int> { 5, 6, 7, 7, 8 };
                List<int> spodziewanyWynik = new List<int> { };
                List<int> wynik = Wspolne(zb1, zb2);
                spodziewanyWynik.Sort();
                wynik.Sort();
                Debug.Assert(wynik.SequenceEqual(spodziewanyWynik), "Test nr 6 (dwa multizbiory wieloelementowe bez wspolnego elementu) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 6 (dwa multizbiory wieloelementowe bez wspolnego elementu) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 6 (dwa multizbiory wieloelementowe bez wspolnego elementu) zakonczony niepowodzeniem.");
            }
            try
            {
                // Test dla przypadku: pierwszy multizbior pusty
                List<int> zb1 = new List<int> { };
                List<int> zb2 = new List<int> { 5, 6};
                List<int> spodziewanyWynik = new List<int> { };
                List<int> wynik = Wspolne(zb1, zb2);
                spodziewanyWynik.Sort();
                wynik.Sort();
                Debug.Assert(wynik.SequenceEqual(spodziewanyWynik));
                Console.WriteLine("Test nr 7 (pierwszy multizbior pusty) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 7 (pierwszy multizbior pusty) zakonczony niepowodzeniem. ");
            }
            try
            {
                // Test dla przypadku: drugi multizbior pusty
                List<int> zb1 = new List<int> { 4, 5, 8};
                List<int> zb2 = new List<int> { };
                List<int> spodziewanyWynik = new List<int> { };
                List<int> wynik = Wspolne(zb1, zb2);
                spodziewanyWynik.Sort();
                wynik.Sort();
                Debug.Assert(wynik.SequenceEqual(spodziewanyWynik), "Test nr 8 (drugi multizbior pusty) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 8 (drugi multizbior pusty) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 8 (drugi multizbior pusty) zakonczony niepowodzeniem.");
            }
            try
            {
                // Test dla przypadku: oba multizbiory puste
                List<int> zb1 = new List<int> { };
                List<int> zb2 = new List<int> { };
                List<int> spodziewanyWynik = new List<int> { };
                List<int> wynik = Wspolne(zb1, zb2);
                spodziewanyWynik.Sort();
                wynik.Sort();
                Debug.Assert(wynik.SequenceEqual(spodziewanyWynik), "Test nr 9 (oba multizbiory puste) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 9 (oba multizbiory puste) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 9 (oba multizbiory puste) zakonczony niepowodzeniem.");
            }
            try
            {
                // Test dla przypadku: argument x przekazany jako null
                Wspolne(null, new List<int> { 7, 8, 9, 10 });
                Console.WriteLine("Test nr 10 (argument x przekazany jako null) zakonczony niepowodzeniem.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Test nr 10 (argument x przekazany jako null) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 10 (argument x przekazany jako null) zakonczony niepowodzeniem.");
            }
            try
            {
                // Test dla przypadku: argument y przekazany jako null
                Wspolne(new List<int> { 7, 8, 9, 10 }, null);
                Console.WriteLine("Test nr 11 (argument y przekazany jako null) zakonczony niepowodzeniem.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Test nr 11 (argument y przekazany jako null) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 11 (argument y przekazany jako null) zakonczony niepowodzeniem.");
            }
            try
            {
                // Test dla przypadku: argumenty x i y przekazane jako null
                Wspolne(null, null);
                Console.WriteLine("Test nr 12 (argumenty x i y przekazane jako null) zakonczony niepowodzeniem.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Test nr 12 (argumenty x i y przekazane jako null) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 12 (argument x i y przekazane jako null) zakonczony niepowodzeniem.");
            }

            Console.WriteLine("Udalo sie, wszystkie przeprowadzone testy zakonczyly sie sukcesem!");

        }
        /// <summary>
        /// Punkt wejscia do programu, ktory wywoluje funkcje testujaca dzialanie metody wyznaczajacej wspolna czesc dwoch zmultizbiorow.
        /// </summary>
        /// <param name="args">Argumenty linii polecen.</param>
       public static void Main(string[] args)
        {
            TestWspolne();
            Console.WriteLine("Prosze wcisnac dowolny klawisz, by zakonczyc.");
            Console.ReadKey();
        }

    }
}
