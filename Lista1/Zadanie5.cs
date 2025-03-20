/* Autor: Julia Kwiatkowska, 268375
 * Linki, z ktorych korzystalam, w celu rozwiazania tego zadania:
 * problem Collatza: https://pl.wikipedia.org/wiki/Problem_Collatza
 * while: https://www.w3schools.com/cs/cs_while_loop.php
 * list: https://www.geeksforgeeks.org/c-sharp-list-class/
 * RemoveRange: https://learn.microsoft.com/pl-pl/dotnet/api/system.collections.generic.list-1.removerange?view=net-8.0
 * operatory: https://anonco.pl/kurs-c-operatory-arytmetyczne/
 * komentarze napisalam w jezyku polskim, ale nie korzystalam z polskich znakow alfabetycznych
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lista1
{
    /// <summary>
    /// Klasa Zadanie 5 z implementacja metody zwracajacej liste elementow ciagu Collatza dla zadanej wartosci c0 oraz zawiera odpowiednie sprawdzenie poprawnosci jej dzialania.
    /// </summary>
    class Zadanie5
    {
        /// <summary>
        /// Metoda zwraca liste elementow ciagu Collatza dla zadanej wartosci parametru c0 przed wpadnieciem w cykl (4,2,1).
        /// </summary>
        /// <param name="c0">Poczatkowa wartosc parametru c0 (musi to byc dodatnia liczba naturalna).</param>
        /// <returns>Lista wszystkich kolejnych elementow ciagu Collatza przed wpadnieciem w cykl (4,2,1)</returns>
        /// <exception cref="ArgumentException">Wyjatek rzucany w momencie, gdy c0 nie jest dodatnia liczba naturalna.</exception>
        public static List<long> Collatz(long c0)
        {
            if (c0 < 1)
                throw new ArgumentException("c0 musi być dowolną dodatnią liczbą naturalną tzn. >=1");
            List<long> listaElementow = new List<long>();
            listaElementow.Add(c0);

            // do momentu, az nie pojawi sie cciag 4,2,1 na koncu listy generowanie kolejnych elementow
            while (true)
            {
                long ostatniElement = listaElementow[listaElementow.Count - 1];
                long nastepnyElement;

                if (ostatniElement % 2 == 0) //przypadek dla parzystych
                {
                    nastepnyElement = ostatniElement / 2; //wzor dla parzystych
                }
                else //przypadek dla nieparzystych
                {
                    nastepnyElement = 3 * ostatniElement + 1; //wzor dla nieparzystych
                }
                listaElementow.Add(nastepnyElement);

                //sprawdzenie, czy trzy ostatnie elementy to 4,2,1 - oznacza to wtedy ze wpada w cykl 
                if (listaElementow.Count >= 3)
                {
                    int n = listaElementow.Count;
                    if (listaElementow[n - 3] == 4 && listaElementow[n - 2] == 2 && listaElementow[n - 1] == 1)
                    {
                        //jesli tak jest to nastepuje usuniecie cyklu z konca i przerwanie dalszego wykonywania
                        listaElementow.RemoveRange(n - 3, 3);
                        break;
                    }
                }
            }
            return listaElementow;
        }
        /// <summary>
        /// Funkcja tetsujaca dzialanie funkcji Collatz, zarowno dla przypadkow typowych, jak i brzegowych. Testuje rowniez sprawdzanie rzucania wyjatkow dla niepoprawnej wartosci argumentu.
        /// Wyniki testow sa wypisywane na konsole i zastosowano asercje w celu automatycznej weryfikacji poprawnosci.
        /// </summary>
        public static void TestCollatz()
        {
            try
            {
                // Test nr 1: dla przypadku c0=1
                long c0 = 1;
                List<long> ciagCol = Collatz(c0);
                Debug.Assert(ciagCol.Count == 1, "Dla zadanego c0=1 powinien pozostac 1 element.");
                Debug.Assert(ciagCol[0] == 1, "Jedyny element powinien wynosic 1.");
                Console.WriteLine("Test nr 1 (dla c0=1) zakonczony skucesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 1 (c0=1) zakonczony niepowodzeniem: " + ex.Message);
            }

            try
            {
                // Test nr 2: dla przypadku c0=2
                long c0 = 2;
                List<long> ciagCol = Collatz(c0);
                Debug.Assert(ciagCol.Count == 2, "Dla zadanego c0=2 powinny zostac 2 elementy.");
                Debug.Assert(ciagCol[0] == 2 && ciagCol[1] == 1, "Ciag powinien wygladac taak: [2, 1].");
                Console.WriteLine("Test nr 2 (dla c0=2) zakonczony skucesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 2 (c0=2) zakonczony niepowodzeniem: " + ex.Message);
            }
            try
            {
                // Test nr 3: dla przypadku c0=3
                long c0 = 3;
                List<long> ciagCol = Collatz(c0);
                Debug.Assert(ciagCol.Count == 5, "Dla zadanego c0=3 powinno zostac 5 elementow.");
                Debug.Assert(ciagCol[0] == 3 && ciagCol[4] == 8, "Ciag powinien miec pierwszy element rowny 3 a ostatni 8.");
                Console.WriteLine("Test nr 3 (dla c0=3) zakonczony skucesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 3 (c0=3) zakonczony niepowodzeniem: " + ex.Message);
            }
            try
            {
                // Test nr 4: dla przypadku c0=10
                long c0 = 10;
                List<long> ciagCol = Collatz(c0);
                Debug.Assert(ciagCol.Count == 4, "Dla zadanego c0=10 powinny zostac 4 elementy.");
                Debug.Assert(ciagCol[0] == 10 && ciagCol[ciagCol.Count - 1] == 8, "Ciag powinien miec pierwszy element rowny 10 a ostatni (przed cyklem 4,2,1) 8.");
                Console.WriteLine("Test nr 4 (dla c0=10) zakonczony skucesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 4 (c0=10) zakonczony niepowodzeniem: " + ex.Message);
            }
            try
            {
                // Test nr 5: dla przypadku nieporawnego c0=0
                long c0 = 0;
                List<long> ciagCol = Collatz(c0);
                Debug.Assert(false, "Dla przypadku c0=0 powinien zostac rzucony wyjatek ArgumentException.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test dla przypadku c0=0 zakonczony sukcesem! : " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test dla przypadku c0=0 zakonczony niepowodzeniem: " + ex.Message);
            }

            try
            {
                // Test nr 6: dla przypadku nieporawnego c0 < 0
                long c0 = -5;
                List<long> ciagCol = Collatz(c0);
                Debug.Assert(false, "Dla przypadku c0=-5 powinien zostac rzucony wyjatek ArgumentException.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test dla przypadku brzegowego c0=-5 zakonczony sukcesem! : " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test dla przypadku brzegowego c0=-5 zakonczony niepowodzeniem: " + ex.Message);
            }
            Console.WriteLine("Udalo sie, wszystkie przeprowadzone testy zakonczyly sie sukcesem!");
        }
        /// <summary>
        /// Funkcja pomocnicza do wyznaczenia maksymalnej dlugosci ciagu Collatza, maksymalnej wartosci elementu ciagu zanim ciag wpada w cykl (4,2,1) i wyznaczenie odpowiadajacych im c0.
        /// </summary>
        /// <param name="poczatek">Poczatkowa wartosc przedzialu do sprawdzenia (wlacznie).</param>
        /// <param name="koniec">Koncowa wartosc przedzialu do sprawdzenia (wlacznie).</param>
        /// <param name="maxDlugoscC0">Wartosc c0, dla ktorej ciag osiaga najwieksza dlugosc.</param>
        /// <param name="maxDlugoscCiagu">Maksymalna dlugosc ciagu dla danego przedzialu c0.</param>
        /// <param name="maxWartoscC0">Wartosc c0, dla ktorej ciag osiaga maksymalna wartosc elementu.</param>
        /// <param name="maxWartosc">Maksymalna wartosc elementu ciagu osiagnietaa w zadanym przedziale c0.</param>
        /// <exception cref="ArgumentException">Wyjatek rzucany, gdy podany zostanie nieprawidlowy przedzial wartosci c0.</exception>

        private static void AnalizaCollatz(long poczatek, long koniec, out long maxDlugoscC0, out int maxDlugoscCiagu, out long maxWartoscC0, out long maxWartosc)
        {
            if (poczatek < 1 || koniec < 1 || koniec < poczatek)
                throw new ArgumentException("Podano nieprawidlowy przedzial wartosci.");
            maxDlugoscC0 = poczatek;
            maxDlugoscCiagu = 0;
            maxWartoscC0 = poczatek;
            maxWartosc = 0;

            for (long c0 = poczatek; c0 <= koniec; c0++)
            {
                List<long> ciagCol = Collatz(c0);

                //dlugosc ciagu
                int dlugosc = ciagCol.Count;
                //maksymalna wartosc elementu w ciagu
                long maxWartoscLokalna = 0;
                foreach (long wartosc in ciagCol)
                {
                    if (wartosc > maxWartoscLokalna) maxWartoscLokalna = wartosc;
                }

                //sprawdzenie czy jest nowa maksymalna dlugosc ciagu Collatza
                if (dlugosc > maxDlugoscCiagu)
                {
                    maxDlugoscCiagu = dlugosc;
                    maxDlugoscC0 = c0;
                }
                //sprawdzenie czy jest nowa maksymalna wartosc
                if (maxWartoscLokalna > maxWartosc)
                {
                    maxWartosc = maxWartoscLokalna;
                    maxWartoscC0 = c0;
                }
            }
        }


        /// <summary>
        /// Punkt wejscia do programu, ktory wywoluje funkcje testujaca dzialanie funkcji Collatz i wywolanie funkcji wyznaczajacej maksymalna wartosc elementu i dlugosc ciagu dla zadanego c0.
        /// </summary>
        /// <param name="args">Argumenty linii polecen.</param>
       /* public static void Main(string[] args)
        {
            //Wywolanie funkcji testujacej z asercjami
            TestCollatz();

            /*szukanie maksymalnej dlugosci ciagu i maksymalnej wartosci elementu ciagu przed wpadnieciem cykl i odpowiadajace im c0
            sprawdzenie w ciagach Collatza dla co z przedzialu [1 do 1000], wniosek jest taki, ze dla c0=1 ciag szybko trafia w cykl (4,2,1), natomiast dla bardzo duzych wartosci c0
            w praktyce nie ma ograniczen, ale zajmuje to wiecej czasu obliczen i rozmiar listy staje sie bardzo duzy
            AnalizaCollatz(
                poczatek: 1,
                koniec: 1000,
                out long maxDlugoscC0,
                out int maxDlugoscCiagu,
                out long maxWartoscC0,
                out long maxWartosc
                );
            Console.WriteLine("Dla c0 w przedziale [1 do 1000]:");
            Console.WriteLine("Dlugosc najdluzszego ciagu Collatza przed wpadnieciem w cykl to: " + maxDlugoscCiagu + " osiagnieta dla c0 rownego " + maxDlugoscC0);
            Console.WriteLine("Maksymalna wartosc elementu ciagu Collatza przed wpadnieciem w cykl to: " + maxWartosc + " osiagnieta dla c0 rownego " + maxWartoscC0);


            Console.WriteLine("Prosze wcisnac dowolny klawisz, by zakonczyc.");
            Console.ReadKey();
        }*/
    }
}
    
