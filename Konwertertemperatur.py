print("Wybierz jednostkę temperatury, z której chcesz konwertować(C lub F):")

# Pobierz jednostkę temperatury od użytkownika
wejscie = input()
jednostka = wejscie[0].upper() if wejscie else ""

# Sprawdzenie czy użytkownik podaje poprawną jednostkę i wykonanie odpowiedniej konwersji
if jednostka == 'C':
    print("\nPodaj temperaturę w stopniach Celsjusza:")
    celsjusz = float(input())
    fahrenheit = (celsjusz * 1.8) + 32
    print(f"Temperatura w stopniach Fahrenheita: {fahrenheit}")
    
elif jednostka == 'F':
    print("\nPodaj temperaturę w stopniach Fahrenheita:")
    fahrenheit = float(input())
    celsjusz = (fahrenheit - 32) / 1.8
    print(f"Temperatura w stopniach Celsjusza: {celsjusz}")
    
else:
    print("\nNieprawidłowa jednostka. Proszę wybrać C lub F.")
    