Long numbers is project for Programming 2 by Václav Kouřil
I'll be using Residue vectors for quick addition, substraction and multiplication.

================================================================= Next parts are taken from github wiki =================================================================
======================================== https://github.com/vaclavkouril/long-numbers/wiki/Program%C3%A1torsk%C3%A1-dokumentace =========================================

Uživatelská dokumentace
Popis

LongNumber class podporující základní operace s nezápornými celými čísly. Využívá reziduálních vektorů které zrychlují operace sčítání, odčítání a násobení.Bohužel nevýhodou je převedení čísla z pro člověka čitelného tvaru a zpět avšak operace samotné by měli být rychlé při správném zvolení vzájemně nesoudělných čísel využívaných pro modulární aritmetiku. Podrobnosti viz Donald Knuth: Seminumerical algorithms (The Art of Computer Programming, Volume 2).
Konstrukce objektu
1. new LongNumber(string bigNum)

bigNum je číslo menší než 2^68. Do primes se definují základní předdefinované hodnoty
2. new LongNumber(string bigNum, List<long> primes)

bigNum je číslo odpovídající zvolenenému rozmezí při volbě navzájem nesoudělných čísel zvolených v listu primes. primes by se měla volit na základě principů modulární aritmetiky viz Donald Knuth: Seminumerical algorithms (The Art of Computer Programming, Volume 2).
Metody

    ToString() = navrátí hodnotu velkého čísla ve formátu string(pomalejší)
    ToBigInteger() = navrátí hodnotu velkého čísla ve formátu BigInteger ze System.Numerics (rychlejší)

Standartní operace

    LongNumber a + LongNumber b = a+b
    LongNumber a - LongNumber b = a-b
    LongNumber a \* LongNumber b = a*b
    LongNumber a / LongNumber b = a/b
    Power( LongNumber number, int exponent) = number^exponent

========================================================================================================================================================================
Programátorská dokumentace


LongNumber class
Popis

Class podporující základní operace s nezápornými celými čísly. Fungující na základě modulární aritmetiky viz Donald Knuth: Seminumerical algorithms (The Art of Computer Programming, Volume 2).
Konstrukce nového objektu třídy
1. new LongNumber(string bigNum)

bigNum je číslo menší než 2^68. Do primes se definují základní předdefinované hodnoty
2. new LongNumber(string bigNum, List<long> primes)

bigNum je číslo odpovídající zvolenenému rozmezí při volbě navzájem nesoudělných čísel zvolených v listu primes. primes by se měla volit na základě principů modulární aritmetiky viz Donald Knuth: Seminumerical algorithms (The Art of Computer Programming, Volume 2).
Algoritmus

Při konstrukci se nejdříve načtou primes do lokální proměnné _primes, pomocí nichž se následně vydělí bigNum za využití StringModulo z jejíchž výsledků se následně vytvoří _vector.
Obsah
Hodnoty

    _primes

private static List<long> _primes
Popis

Proměnná ukládající nesoudělná čísla sloužící k rozkladu velkého čísla na rezidua. Čísla se přidají při vytvoření nového LongNumber objektu.
Základní hodnoty

2^30 - 1, 2^29 - 1, 2^27 - 1, 2^23 - 1, 2^19 - 1, 2^17 - 1, 2^13 - 1, 2^11 - 1
Doporučení

Bylo by nejlepší mít největší číslo na prvním místě v List.

    _vector

private List<long> _vector
Popis

Proměnná udržující hodnoty vstupního čísla string bigNum při konstrukci nového LongNumber objektu modulo všemi čísly v _primes.

Metody
Veřejné

    ToBigInteger

public BigInteger ToBigInteger()
Popis

Metoda převede číslo reprezentované vektorem reziduí zpět které je ve formátu BigInteger ze System.Numerics
Poznámky

Metoda je rychlejší než ToString, avšak výsledek je totožný.
Algotimus

Číslo se hledá pomocí opakovaného přidávání prvního čísla z _primes k prvnímu číslu z _vector a postupnému modulo dalšími prvky z _primes dokud se všechna neshodují s příslušnými zbytky z _vector.

    ToString

public new string ToString()
Popis

Metoda převede číslo reprezentované vektorem reziduí zpět které je ve formátu string. Nevyužívá BigInteger narozdíl od ToBigInteger.
Poznámky

Metoda je pomalejší než ToBigInteger, avšak výsledek je totožný.
Algoritmus

Funguje na základě stejného algoritmu jako ToBigInteger, avšak pro znovu příčítání využívaná funkce StringNumAddition a modulo pomocí StringModulo.

    Power

public static LongNumber Power(LongNumber number, int exponent)
Popis

number je umocněno na exponent.
Algoritmus

Jedná se o loop, který dělá operaci násobení result * number exponent-krát, kde result je výsledek začínající na hodnotě number^0. Výsledek se převede zpět na formát LongNumber.


Operátory

    +

public static LongNumber operator +(LongNumber num1, LongNumber num2)
Popis

Sečte dvě LongNumber
Algoritmus

Sečte vektory num1 a num2 po složkách a aplikuje modulo pomocí LongModulo příslušné hodnoty v _primes.

    -

public static LongNumber operator -(LongNumber num1, LongNumber num2)
Popis

Odečte LongNumber num2 od num1.
Algoritmus

Odečte prvky vektorů num2 od num1 po složkách a aplikuje modulo pomocí LongModulo příslušné hodnoty v _primes.

    *

public static LongNumber operator *(LongNumber num1, LongNumber num2)
Popis

Vynásobí po složkách vektory num1 s num2
Algoritmus

Metoda má uvnitř sebe static string Multiply(string num1, string num2) která vynásobí dvě velká čísla mezi sebou. Tuto funkci násobení využije pro vynásobení dvou odpovídajících složek na které je poté aplikováno StringModulo kde tento zbytek po dělení příslušnou hodnotou z _primes a uloží se do nového čísla, které je poté vráceno jako výsledek.
Využití Power


    /

public static LongNumber operator /(LongNumber numerator, LongNumber nominator)
Popis

Celočíselně vydělí numerator pomocí nominator
Algoritmus

Pomocí StringNumAddition se přidává x-krát nominator, dokud IsGreaterOrEqual neurčí, že výsledný součet je větší než numerator, počet x je pak výsledkem


Privátní

    LongModulo

private static long LongModulo(long dividend, long divisor)
Popis

Metoda vypočítající zbytek po dělení dividend pomocí divisor.
Algoritmus

LongModulo vypočítá zbytek po dělení který je vždy v rozmezí od 0 do divisor-1.
Využití
Operátory

    +
    -

    StringModulo

private static long StringModulo(string num, long a)
Popis

Metoda vypočítající zbytek po dělení string num pomocí long a.
Algoritmus

StringModulo pracuje s postupným modulo přes cifry, jednu po druhé.
Využití
Operátory

    Konstruktor LongNumber
    ToString
    *


    StringNumAddition

private string StringNumAddition(string num1, string num2)
Popis

Sečte dvě libovolně dlouhá čísla cifru po cifře
Algoritmus

Metoda obsahuje vnitřní metodu List<int> ForLoop(char[] biggerNum, char[] smallerNum) která převede pole char číslovek a sečte zleva do prava vyžaduje tedy v parametrech číslo reprezentováni zprava doleva (jednotky na indexu 0). Výsledek z ForLoop se nakonec převede do chtěného string formátu ve standatní reprezentaci (jednotky na nejvyšším indexu).
Využití

    ToString


    IsGreaterOrEqual

private static bool IsGreaterOrEqual(string num1, string num2)
Popis

Metoda zjistí zda num1 >= num2
Algoritmus

Zjistí jaké číslo je delší, popřípadě jde zleva doprava a hledá to s větší číslicí, nebo vrátí true pokud se rovnají.
Využití
Operátory

    *
