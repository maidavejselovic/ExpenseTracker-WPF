# ExpenseTracker - opis projekta

Ovo je jednostavna WPF desktop aplikacija za evidenciju rashoda.
Omogućava pregled, dodavanje, izmenu i brisanje rashoda, kao i praćenje statusa plaćenosti i ukupnog iznosa.

### **Funkcionalnosti:**

- Prikaz liste rashoda prilikom pokretanja aplikacije (DataGrid).
- Dodavanje novog rashoda.
- Izmena postojećeg rashoda.
- Brisanje selektovanog rashoda sa potvrdom.
- Obeležavanje rashoda kao plaćen (IsPaid).
- Pretraga rashoda po opisu.
- Prikaz ukupnog iznosa svih rashoda (automatski).

### **Baza podataka:**
Projekat koristi Code First pristup sa Entity Framework-om.
To znači da bazu nije potrebno praviti ručno – prilikom prvog pokretanja projekta, baza se automatski kreira zajedno sa tabelom Expenses i svim kolonama.

### **Tehnički detalji:**
- **Data Binding**: koristi se za povezivanje UI elemenata sa ViewModel-ima.
- **ObservableCollection**: koristi se u MainWindowViewModel za listu rashoda.
- **ICommand**: koristi se za dugmad (Save, Edit, Delete).
- **Validacija**: dugme Save je onemogućeno dok obavezna polja nisu popunjena.

### **Pokretanje aplikacije:**
1. Kloniraj ili preuzmi projekat sa GitHub-a.
2. Otvoriti Visual Studio.
O3. tvoriti solution ExpenseTracker.sln.
4. (Opcionalno) Ako koristite MySQL, proverite konekciju u App.config.
5. Pokrenuti aplikaciju (F5).
Prilikom prvog pokretanja, baza i tabela Expenses će se automatski kreirati zahvaljujući Code First pristupu.