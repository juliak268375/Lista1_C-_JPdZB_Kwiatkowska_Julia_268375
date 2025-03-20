/* Autor: Julia Kwiatkowska, 268375
 * Linki, z ktorych korzystalam, w celu rozwiazania tego zadania:
 * transkrypcja, translacja: https://pl.khanacademy.org/science/biology/gene-expression-central-dogma/transcription-of-dna-into-rna/a/stages-of-transcription
 * switch: https://www.w3schools.com/cs/cs_switch.php, https://justjoin.it/blog/nowy-switch-w-c-8-0-jak-dziala-property-pattern, https://www.geeksforgeeks.org/switch-statement-in-c-sharp/
 * reverse: https://www.codecademy.com/resources/docs/c-sharp/arrays/reverse
 * kodony mRNA: https://pl.wikipedia.org/wiki/Kodon
 * dictionary: https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/
 * substring: https://www.geeksforgeeks.org/c-sharp-substring-method/
 * nici i kierunki syntezy: https://biomodel.uah.es/en/model4/dna_pl/codons.htm
 * IsNullOrEmpty: https://www.geeksforgeeks.org/c-sharp-isnullorempty-method/
 * toUpper: https://www.codecademy.com/resources/docs/c-sharp/strings/toupper
 * stringComparison: https://www.codeproject.com/Articles/446230/A-Beginners-Tutorial-on-String-Comparison-in-Cshar
 * komentarze napisalam w jezyku polskim, ale nie korzystalam z polskich znakow alfabetycznych
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lista1
{
    /// <summary>
    /// Klasa Zadanie 6 zawiera implementacje metod Komplement (do tworzenia nici matrycowej komplementarnej do nici kodujacej), Transkrybuj (do transkrypcji nici matrycowej na mRNA
    /// oraz Transluj do zwracania kodowanego przez dana sekwencje mRNA bialka.
    /// </summary>
    class Zadanie6
    {
        /// <summary>
        /// Metoda zwracajaca sekwencje nici matrycowej w kolejności 5' -> 3' komplementarna dla zadanej jako parametr sekwencji nici kodujacej.
        /// Dla danej sekwencji nici kodujacej (5'->3') wykonuje dopelnienie  wg. schematu A->T, T->A, C-> G, G->C i odwrocenie.
        /// </summary>
        /// <param name="nicKodujacaDNA">Sekwencja kodujacej nici DNA (5' do 3').</param>
        /// <returns>Sekwencja nici matrycowej DNA (od 5' do 3').</returns>
        /// <exception cref="ArgumentException">Rzuca wyjatek ArgumentException, gdy sekwencja kodujaca zawiera niedozwolone dla zasad azotowych litery (inne niz A,T,G,C) lub gdy nic kodujaca jest pusta.</exception>
        public static string Komplement(string nicKodujacaDNA)
        {
            if (string.IsNullOrEmpty(nicKodujacaDNA))
                throw new ArgumentException("Wprowadzona sekwencja nici kodujacej nie moze byc pusta!");
            //dobranie komplementarnych par zasad (bez odwracania kierunku nici matrycowej)
            char[] matrycowaNicDNA = new char[nicKodujacaDNA.Length];
            for (int i = 0; i < nicKodujacaDNA.Length; i++)
            {
                switch (Char.ToUpper(nicKodujacaDNA[i])) //ignorowanie wielkosci liter
                {
                    case 'A':
                        matrycowaNicDNA[i] = 'T';
                        break;
                    case 'T':
                        matrycowaNicDNA[i] = 'A';
                        break;
                    case 'G':
                        matrycowaNicDNA[i] = 'C';
                        break;
                    case 'C':
                        matrycowaNicDNA[i] = 'G';
                        break;
                    default:
                        throw new ArgumentException("Podano niedozwolona litere dla zasad azotowych (dozwolone: A, T, G, C): " + nicKodujacaDNA[i]);
                }
            }
            //odwrocenie ciagu nici matrycowej z 3'->5' na 5'->3'
            Array.Reverse(matrycowaNicDNA);
            return new string(matrycowaNicDNA);
        }
        /// <summary>
        /// Metoda zwracajaca sekwencje RNA (5'->3') komplementarna dla zadanej jako parametr sekwencji nici matrycowej (w kolejnosci 5'->3'.
        /// W celu uzyskania odpowiedniego kierunku syntezy nici w metodzie nastepuje odwrocenie sekwencji matrycowej (3'->5') a potem dokonywane sa wlasciwe zamiany
        /// A -> U, T -> A, G -> C, C-> G.
        /// </summary>
        /// <param name="nicMatrycowaDNA">Sekwencja matrycowej nici DNA (5' do 3').</param>
        /// <returns>Sekwencja nici mRNA (od 5' do 3').</returns>
        /// <exception cref="ArgumentException">Rzuca wyjatek ArgumentException, gdy sekwencja matrycowa zawiera niedozwolone dla zasad azotowych litery (inne niz A,T,G,C) lub gdy nic matrycowa jest pusta.</exception>
        public static string Transkrybuj(string nicMatrycowaDNA)
        {
            if (string.IsNullOrEmpty(nicMatrycowaDNA))
                throw new ArgumentException("Wprowadzona sekwencja nici matrycowej nie moze byc pusta!");
            //odwrocenie sekwencji matrycowej z 5'->3' na 3'->5', bo taki jest jej naturalny odczyt
            char[] odwrocenie = nicMatrycowaDNA.ToCharArray();
            Array.Reverse(odwrocenie);
            string odwroconaNicMatrycowa = new string(odwrocenie);

            //tworzenie komplementarnej sekwencji mRNA
            char[] sekwencjamRNA = new char[odwroconaNicMatrycowa.Length];
            for (int i = 0; i < odwroconaNicMatrycowa.Length; i++)
            {
                switch (Char.ToUpper(odwroconaNicMatrycowa[i]))
                {
                    case 'A':
                        sekwencjamRNA[i] = 'U';
                        break;
                    case 'T':
                        sekwencjamRNA[i] = 'A';
                        break;
                    case 'G':
                        sekwencjamRNA[i] = 'C';
                        break;
                    case 'C':
                        sekwencjamRNA[i] = 'G';
                        break;
                    default:
                        throw new ArgumentException("Podano niedozwolona litere dla zasad azotowych(dozwolone: A, T, G, C): " + odwroconaNicMatrycowa[i]);

                }
            }
            return new string(sekwencjamRNA);
        }
        /// <summary>
        /// Metoda do translacji mRNA (5' -> 3') na sekwencje bialka. W pierwszej kolejnosci szuka kodonu start (AUG)
        /// a nastepnie tlumaczy kolejne trojki nukleotydow na aminokwasy z wykorzystaniem slownika kodonow. Caly proces translacji konczy sie, gdy zostanie 
        /// napotkany kodon Stop. Wynikiem translacji jest zapis sekwencji bialka w postaci 1-literowych skrotow aminokwasow.
        /// </summary>
        /// <param name="sekwencjamRNA">Sekwencja nici mRNA (5' do 3').</param>
        /// <returns>Sekwencja bialka w formie 1-literowych skrotow aminokwasow.</returns>
        /// <exception cref="ArgumentException">Wyjatek rzucany, wtedy kiedy w sekwencji nici mRNA sa niedozwolone kodony, albo jest ona pusta, albo nie wystpeuje kodon start albo kodon stop.</exception>
        public static string Transluj(string sekwencjamRNA)
        {
            if (string.IsNullOrEmpty(sekwencjamRNA))
                throw new ArgumentException("Wprowadzona sekwencja mRNA nie moze byc pusta!");

            //slownik z kodonami (kluczem jest kodon mRNA a wartoscia aminokwas w postaci 1-literowych skrotow
            Dictionary<string, char> tablicaKodonow = new Dictionary<string, char>
            {
    { "UUU", 'F' },
    { "UCU", 'S' },
    { "UAU", 'Y' },
    { "UGU", 'C' },
    { "UUC", 'F' },
    { "UCC", 'S' },
    { "UAC", 'Y' },
    { "UGC", 'C' },
    { "UUA", 'L' },
    { "UCA", 'S' },
    { "UAA", '*' },
    { "UGA", '*' },
    { "UUG", 'L' },
    { "UCG", 'S' },
    { "UAG", '*' },
    { "UGG", 'W' },
    { "CUU", 'L' },
    { "CCU", 'P' },
    { "CAU", 'H' },
    { "CGU", 'R' },
    { "CUC", 'L' },
    { "CCC", 'P' },
    { "CAC", 'H' },
    { "CGC", 'R' },
    { "CUA", 'L' },
    { "CCA", 'P' },
    { "CAA", 'Q' },
    { "CGA", 'R' },
    { "CUG", 'L' },
    { "CCG", 'P' },
    { "CAG", 'Q' },
    { "CGG", 'R' },
    { "AUU", 'I' },
    { "ACU", 'T' },
    { "AAU", 'N' },
    { "AGU", 'S' },
    { "AUC", 'I' },
    { "ACC", 'T' },
    { "AAC", 'N' },
    { "AGC", 'S' },
    { "AUA", 'I' },
    { "ACA", 'T' },
    { "AAA", 'K' },
    { "AGA", 'R' },
    { "AUG", 'M' },
    { "ACG", 'T' },
    { "AAG", 'K' },
    { "AGG", 'R' },
    { "GUU", 'V' },
    { "GCU", 'A' },
    { "GAU", 'D' },
    { "GGU", 'G' },
    { "GUC", 'V' },
    { "GCC", 'A' },
    { "GAC", 'D' },
    { "GGC", 'G' },
    { "GUA", 'V' },
    { "GCA", 'A' },
    { "GAA", 'E' },
    { "GGA", 'G' },
    { "GUG", 'V' },
    { "GCG", 'A' },
    { "GAG", 'E' },
    { "GGG", 'G' }
};
            //rozpoczecie od kodonu START AUG
            int kodonStart = sekwencjamRNA.IndexOf("AUG", StringComparison.OrdinalIgnoreCase);
            if (kodonStart == -1)
            {
                throw new ArgumentException("W podanej sekwencji mRNA nie wystepuje kodon startu AUG");

            }
            List<string> sekwencjaBialka = new List<string>();
            bool stopZnaleziony = false;
            //odczyt mRNA od znalezionego kodonu start
            for (int i = kodonStart; i <= sekwencjamRNA.Length - 3; i += 3)
            { string kodon = sekwencjamRNA.Substring(i, 3).ToUpper();
                if (!tablicaKodonow.ContainsKey(kodon))
                {
                    throw new ArgumentException("W sekwencji mRNA wystepuje niepoprawny kodon: " + kodon);
                            }
                //w przypadku pojawienia sie kodonu stop koniec translacji
                if (tablicaKodonow[kodon] == '*')
                {
                    stopZnaleziony = true;
                    break;
                }
                sekwencjaBialka.Add(tablicaKodonow[kodon].ToString());

            }
            if(!stopZnaleziony)
            {
                throw new ArgumentException("W podanej sekwencji mRNA nie wystepuje kodon STOP.");
            }
            //laczenie aminokwasow w jeden lancuch
            return string.Join("", sekwencjaBialka);

        }
        /// <summary>
        /// Funkcja testujaca poprawnosc dzialania metody Komplement zarowno dla przypadkow typowych jak i generujacych wyjatki.
        /// </summary>
        public static void TestKomplement()
        {
            Console.WriteLine("Testy dla metody Komplement:");
            try
            {
                //Test nr 1 typowy przypadek sekwencji nici kodujacej
                string matrycowa = Komplement("ATCG");
                Debug.Assert(matrycowa == "CGAT", "Test nr 1 (typowa sekwencja kodujaca) zakonczyl sie niepowodzeniem.");
                Console.WriteLine("Test nr 1 (typowa sekwencja kodujaca) zakonczony sukcesem, 'ATCG' -> " + matrycowa);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 1 (typowa sekwencja kodujaca) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 2  przypadek sekwencji nici kodujacej z 1 znakiem
                string matrycowa = Komplement("A");
                Debug.Assert(matrycowa == "T", "Test nr 2 (sekwencja kodujaca z 1 znakiem) zakonczyl sie niepowodzeniem.");
                Console.WriteLine("Test nr 2 (sekwencja kodujaca z 1 znakiem) zakonczony sukcesem, 'A' -> " + matrycowa);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 2 (sekwencja kodujaca z 1 znakiem) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 3 przypadek sekwencji nici kodujacej z nieprawidlowymi znakami
                Komplement("ABTDW");
                Debug.Assert(false, "Test nr 3 (sekwencja kodujaca z nieprawidlowymi znakami) zakonczyl sie niepowodzeniem, powinien zostac rzucony wyjatek.");
                
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test nr 3 (sekwencja kodujaca z nieprawidlowymi znakami) zakonczyl sie sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 3 (sekwencja kodujaca z nieprawidlowymi znakami) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 4 przypadek pustej sekwencji nici kodujacej 
                Komplement("");
                Debug.Assert(false, "Test nr 4 (pusta sekwencja kodujaca) zakonczyl sie niepowodzeniem, powinien zostac rzucony wyjatek.");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test nr 4 (pusta sekwencja kodujaca) zakonczyl sie sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 4 (pusta sekwencja kodujaca) zakonczyl sie niepowodzeniem." + ex.Message);
            }
        }
        /// <summary>
        /// Funkcja testujaca poprawnosc dzialania metody Transkrybuj zarowno dla przypadkow typowych jak i generujacych wyjatki.
        /// </summary>
        public static void TestTranskrybuj()
        {
            Console.WriteLine("Testy dla metody Transkrybuj:");
            try
            {
                //Test nr 1 typowy przypadek sekwencji nici matrycowej
                string mRNA = Transkrybuj("CGAT");
                Debug.Assert(mRNA == "AUCG", "Test nr 1 (typowa sekwencja matrycowa) zakonczyl sie niepowodzeniem.");
                Console.WriteLine("Test nr 1 (typowa sekwencja matrycowa) zakonczony sukcesem, 'CGAT' -> " + mRNA);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 1 (typowa sekwencja matrycowa) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 2  przypadek sekwencji nici matrycowej z 1 znakiem
                string matrycowa = Transkrybuj("A");
                Debug.Assert(matrycowa == "U", "Test nr 2 (sekwencja matrycowa z 1 znakiem) zakonczyl sie niepowodzeniem.");
                Console.WriteLine("Test nr 2 (sekwencja matrycowa z 1 znakiem) zakonczony sukcesem, 'A' -> " + matrycowa);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 2 (sekwencja matrycowa z 1 znakiem) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 3 przypadek sekwencji nici matrycowej z nieprawidlowymi znakami
                Transkrybuj("ABTDW");
                Debug.Assert(false, "Test nr 3 (sekwencja matrycowa z nieprawidlowymi znakami) zakonczyl sie niepowodzeniem, powinien zostac rzucony wyjatek.");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test nr 3 (sekwencja matrycowa z nieprawidlowymi znakami) zakonczyl sie sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 3 (sekwencja matrycowa z nieprawidlowymi znakami) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 4 przypadek pustej sekwencji nici matrycowej
                Transkrybuj("");
                Debug.Assert(false, "Test nr 4 (pusta sekwencja matrycowa) zakonczyl sie niepowodzeniem, powinien zostac rzucony wyjatek.");

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test nr 4 (pusta sekwencja matrycowa) zakonczyl sie sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 4 (pusta sekwencja matrycowa) zakonczyl sie niepowodzeniem." + ex.Message);
            }
        }
        /// <summary>
        /// Funkcja testujaca poprawnosc dzialania metody Transluj zarowno dla przypadkow typowych jak i generujacych wyjatki.
        /// </summary>
        public static void TestTransluj()
        {
            Console.WriteLine("Testy dla metody Transluj:");
            try
            {
                //Test nr 1 typowy przypadek sekwencji mRNA
                string bialko = Transluj("AUGUUUUCUUAUUAA");
                Debug.Assert(bialko == "MFSY", "Test nr 1 (typowa sekwencja mRNA) zakonczyl sie niepowodzeniem.");
                Console.WriteLine("Test nr 1 (typowa sekwencja mRNA) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 1 (typowa sekwencja mRNA) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 2 dwa kodony start jeden stop
                string bialko = Transluj("AUGUUUUCUUAUAUGUAA");
                Debug.Assert(bialko == "MFSYM", "Test nr 2 (dwa kodony start jeden stop zakonczyl sie niepowodzeniem.");
                Console.WriteLine("Test nr 2 (dwa kodony start jeden stop) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 2 (dwa kodony start jeden stop) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 3 dwa kodony stop
                string bialko = Transluj("AUGUUUUCUUAUAUGUAAAAGUGA");
                Debug.Assert(bialko == "MFSYM", "Test nr 3 (dwa kodony stop) zakonczyl sie niepowodzeniem.");
                Console.WriteLine("Test nr 3 (dwa kodony stop) zakonczony sukcesem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nr 3 (dwa kodony stop) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 4 brak kodonu stop
                string bialko = Transluj("AUGUUUUCUUAUAUG");
                Debug.Assert(false, "Test nr 4 (brak kodonu stop) zakonczyl sie niepowodzeniem.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test nr 4 (brak kodonu stop) zakonczyl sie sukcesem.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nnr 4 (brak kodonu stop) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 5 brak kodonu start
                string bialko = Transluj("UUUUCUUAUUAA");
                Debug.Assert(false, "Test nr 5 (brak kodonu start) zakonczyl sie niepowodzeniem.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test nr 5 (brak kodonu start) zakonczyl sie sukcesem.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nnr 5 (brak kodonu start) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 6 pusta sekwencja mRNA
                string bialko = Transluj("");
                Debug.Assert(false, "Test nr 6 (pusta sekwencja mRNA) zakonczyl sie niepowodzeniem.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test nr 6 (pusta sekwencja mRNA) zakonczyl sie sukcesem.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nnr 6 (pusta sekwencja mRNA) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            try
            {
                //Test nr 7 niedozwolone kodony
                string bialko = Transluj("AUGABCDEFUAA");
                Debug.Assert(false, "Test nr 7 (niedozwolone kodony) zakonczyl sie niepowodzeniem.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Test nr 6 (niedozwolone kodony) zakonczyl sie sukcesem.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test nnr 6 (niedozwolone kodony) zakonczyl sie niepowodzeniem." + ex.Message);
            }
            Console.WriteLine("Udalo sie, wszystkie przeprowadzone testy zakonczyly sie sukcesem!");
        }

        /// <summary>
        /// Punkt wejscia do programu, ktory wywoluje funkcje testujace dzialanie funkcji Komplement, Transkrybuj i Transluj..
        /// </summary>
        /// <param name="args">Argumenty linii polecen.</param>
        public static void Main(string[] args)
        {
            TestKomplement();
            TestTranskrybuj();
            TestTransluj();
            Console.WriteLine("Prosze wcisnac dowolny klawisz, by zakonczyc.");
            Console.ReadKey();
        }
  }
}
