
# Instrukcja Obsługi Aplikacji

Aplikacja została napisana w **C#** (backend) oraz **React** (frontend). W tej instrukcji opisano kroki potrzebne do uruchomienia aplikacji. Nie jest wymagana baza danych, ponieważ aplikacja korzysta z **InMemory Database**.

## Wymagania Wstępne

Przed rozpoczęciem upewnij się, że masz zainstalowane:

1. **.NET SDK** (wersja zgodna z aplikacją).
2. **Node.js** (zalecana wersja LTS) oraz **npm**.
3. Narzędzie do zarządzania wersjami, np. **Git**, jeśli kod aplikacji pochodzi z repozytorium.

---

## Krok 1: Klonowanie Repozytorium (opcjonalne)

Jeżeli aplikacja znajduje się w repozytorium Git, wykonaj poniższe kroki:

1. Otwórz terminal.
2. Skopiuj repozytorium za pomocą komendy:
   ```bash
   git clone <URL_REPOZYTORIUM>
   ```
3. Przejdź do folderu z aplikacją:
   ```bash
   cd <NAZWA_FOLDERU>
   ```

---

## Krok 2: Uruchamianie Backend'u

1. Przejdź do folderu z projektem backendu:
   ```bash
   cd backend
   ```
2. Uruchom aplikację przy użyciu komendy:
   ```bash
   dotnet run
   ```
3. Upewnij się, że aplikacja uruchomiła się poprawnie. Powinieneś zobaczyć w terminalu komunikaty o uruchomieniu serwera.

---

## Krok 3: Uruchamianie Frontend'u

1. Przejdź do folderu z projektem frontendowym:
   ```bash
   cd frontend
   ```
2. Zainstaluj wymagane zależności:
   ```bash
   npm install
   ```
3. Uruchom aplikację React za pomocą komendy:
   ```bash
   npm run dev
   ```
4. Po uruchomieniu w terminalu pojawi się adres lokalnego serwera (np. `http://localhost:3000`). Otwórz przeglądarkę i wpisz podany adres.

---

## Uwagi

1. **InMemory Database**:
   - Nie jest wymagana dodatkowa konfiguracja bazy danych.
   - Dane przechowywane są wyłącznie w pamięci i znikają po wyłączeniu aplikacji.

2. **Porty**:
   - Backend zazwyczaj działa na porcie **5000** lub **5001**.
   - Frontend zazwyczaj działa na porcie **3000**.

3. **Problemy z uprawnieniami**:
   - Jeśli pojawią się problemy z uprawnieniami podczas uruchamiania, upewnij się, że masz odpowiednie uprawnienia do wykonywania poleceń lub uruchom terminal z uprawnieniami administratora.

---

## Przykład Testowego Uruchomienia

### Backend:
```bash
cd backend
dotnet run
```

### Frontend:
```bash
cd frontend
npm install
npm run dev
```

Po wykonaniu tych kroków aplikacja powinna być dostępna w przeglądarce.

---

## Weryfikacja Działania

1. Otwórz przeglądarkę i przejdź pod adres frontendu (np. `http://localhost:3000`).
2. Sprawdź podstawowe funkcjonalności aplikacji, takie jak dodawanie danych, wyświetlanie list, itp.
3. Jeżeli napotkasz problemy, sprawdź logi w terminalu backendu i frontend'u.

---

## Zamknięcie Aplikacji

1. Aby zatrzymać backend, naciśnij `Ctrl + C` w terminalu.
2. Aby zatrzymać frontend, naciśnij `Ctrl + C` w terminalu.

---

## Dodatkowe Informacje

- Dokumentacja kodu oraz dodatkowe instrukcje mogą być dostępne w folderze projektu (np. plik `README.md`).
- W razie problemów skontaktuj się z autorem aplikacji lub zespołem wsparcia technicznego.
