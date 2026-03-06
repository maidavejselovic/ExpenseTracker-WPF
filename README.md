ExpenseTracker
Opis projekta

Ovo je jednostavna WPF desktop aplikacija za evidenciju rashoda.
Omogućava pregled, dodavanje, izmenu i brisanje rashoda, kao i praćenje statusa plaćenosti i ukupnog iznosa.

Funkcionalnosti
Prikaz liste rashoda prilikom pokretanja aplikacije (DataGrid).
Dodavanje novog rashoda.
Izmena postojećeg rashoda.
Brisanje selektovanog rashoda sa potvrdom.
Obeležavanje rashoda kao plaćen (IsPaid).
Pretraga rashoda po opisu.
Prikaz ukupnog iznosa svih rashoda (automatski).

Baza podataka

Projekat koristi Code First pristup sa Entity Framework-om.
To znači da bazu nije potrebno praviti ručno – prilikom prvog pokretanja projekta, baza se automatski kreira zajedno sa tabelom Expenses i svim kolonama.

Ipak, SQL skripta `ExpenseDB.sql` je priložena u folderu `SQL/` ukoliko neko želi da ručno ručno kreirati bazu.

Pokretanje aplikacije
Otvoriti Visual Studio.
Otvoriti solution ExpenseTracker.sln.
(Opcionalno) Ako koristite MySQL, proverite konekciju u App.config.
Pokrenuti aplikaciju (F5).
Prilikom prvog pokretanja, baza i tabela Expenses će se automatski kreirati zahvaljujući Code First pristupu.