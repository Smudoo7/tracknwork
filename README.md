# Track’n’Work

**Track’n’Work** ist eine ASP.NET MVC-Webanwendung zur Projektzeiterfassung im kommunalen Bauhof-Kontext.  
Sie ermöglicht die strukturierte Erfassung von Mitarbeitereinsätzen, verwendeten Fahrzeugen, Geräten sowie Haushaltsstellen – inklusive sicherer Authentifizierung mit Supabase.

---

## 🔧 Technologie-Stack

- **Frontend & Backend**: ASP.NET MVC (Framework 4.8)
- **Authentifizierung**: Supabase Auth
- **Datenbank**: Supabase (PostgreSQL)
- **Hosting**: IONOS Webhosting (Root-Verzeichnis)
- **UI**: Vanilla JS, HTML/CSS, Bootstrap-ähnlicher Stil
- **Ziel-Domain**: [https://tracknwork.de](https://tracknwork.de)

---

## ✨ Features

- Benutzer-Login mit Supabase Auth (E-Mail + Passwort)
- Multimandantenfähigkeit (Bauhof-ID, z. B. `B001`)
- Benutzerrollen: `admin`, `user` (über `user_metadata`)
- Tokens via Cookie (gesetzt durch Server mit `SetToken`)
- Responsives Login-Interface im Apple-Stil
- Sessionsteuerung per `supabase_token`-Cookie
- Schutz vor unberechtigtem Zugriff über Layout-Middleware

---

## 🔐 Login-Mechanismus

1. Nutzer gibt ein:
   - **Benutzername**
   - **Bauhof-ID** (z. B. `B001`)
   - **Passwort**
2. JavaScript generiert die E-Mail:
   ```
   benutzername@bauhofid.tracknwork.de
   ```
3. Supabase `signInWithPassword` → Token wird empfangen
4. Der Access Token wird per POST an `/Authentication/SetToken` übermittelt
5. ASP.NET speichert das Token als **`supabase_token`-Cookie**
6. Der Cookie wird im Backend ausgelesen und geprüft

---

## 🧠 Benutzerverwaltung

- Benutzer werden zentral in Supabase erstellt (z. B. per Node.js CLI)
- Keine Selbstregistrierung
- Benutzer-Metadaten:
  - `display_name`
  - `bauhof_id`
  - `role`

---

## ⚙️ Lokale Entwicklung

### Voraussetzungen:

- Visual Studio 2022 oder neuer
- .NET Framework 4.8
- Supabase-Projekt mit aktivierter Auth

### Setup:

1. Repository klonen
2. Supabase-Keys in `Login.cshtml` eintragen
3. Startprojekt: **TrackNWork**
4. Standard-Startseite: `Login.cshtml`

---

## 🚀 Hosting bei IONOS

- Anwendung läuft im Root (`/`) des Webspace
- Cookies müssen mit `SameSite=Lax`, `Secure`, `HttpOnly=false` gesetzt werden
- Serverseitiges Setzen von Cookies erforderlich (Client-JS wird blockiert)
- Tokenprüfung & Weiterleitung im `_Layout.cshtml`

---

## 📂 Ordnerstruktur (Auszug)

```
/Controllers
  └── AuthenticationController.cs
  └── HomeController.cs

/Views
  └── /Authentication/Login.cshtml
  └── /Home/Index.cshtml

/Scripts
  └── supabase-js.js

/App_Data
  └── token-log.txt (nur zu Debugzwecken)
```

---

## 📌 Nächste Schritte (Roadmap)

- ✅ Adminbereich: Geräte, Fahrzeuge, Haushaltsstellen
- ⏳ Übersicht: Mitarbeitereinsätze
- ⏳ Offline-Funktion (PWA)
- ⏳ Erweiterte Rollenverwaltung
- ⏳ Vorbereitungen für Internationalisierung (derzeit: Deutsch)

---

## 💬 Kontakt

Erstellt von [@Smudoo7](https://github.com/Smudoo7)
