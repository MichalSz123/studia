print("Prosty kalkulator")

# Pobieranie pierwszej liczby
liczba1 = int(input("Podaj pierwszą liczbę: "))

# Pobieranie drugiej liczby
liczba2 = int(input("Podaj drugą liczbę: "))

# Pobieranie operacji
operacja = input("Podaj operację (+, -, *, /): ")

# Obliczanie wyniku na podstawie wybranej operacji
wynik = 0.0

if operacja == "+":
    wynik = liczba1 + liczba2
elif operacja == "-":
    wynik = liczba1 - liczba2
elif operacja == "*":
    wynik = liczba1 * liczba2
elif operacja == "/":
    if liczba2 != 0:
        wynik = liczba1 / liczba2
    else:
        print("Błąd: nie można dzielić przez zero!")
        exit() 
else:
    print("Błąd: nieznana operacja!")
    exit()
    
print(f"Wynik: {wynik}")
