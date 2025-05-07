# Track’n’Work

**Track’n’Work** ist eine ASP.NET MVC-Webanwendung zur Projektzeiterfassung im Bauhof-Kontext. Die Applikation wurde speziell für die Anforderungen kommunaler Bauhöfe entwickelt und ermöglicht es, Mitarbeitereinsätze, Fahrzeuge, Geräte und Haushaltsstellen strukturiert zu erfassen – inklusive moderner, sicherer Authentifizierung.

---

## 🔧 Technologie-Stack

- **Frontend & Backend**: ASP.NET MVC (.NET Framework 4.8)
- **Authentifizierung**: [Supabase Auth](https://supabase.com/)
- **Datenbank**: Supabase (PostgreSQL)
- **Hosting**: IONOS Webhosting (Root-Verzeichnis)
- **Clientseitige Komponenten**: Vanilla JS, HTML/CSS, Bootstrap-ähnliches Design
- **Deployment-Ziel**: https://tracknwork.de

---

## ✨ Features

- Benutzer-Login mit Supabase Auth (E-Mail + Passwort)
- Multimandantenfähigkeit per Bauhof-ID (4-stellig, hexadezimal)
- Benutzerstruktur mit Rollen (`admin`, `user`) via Supabase user_metadata
- Tokens werden per Cookie gesetzt (serverseitig via SetToken)
- Responsive und mobiloptimiertes Login-Interface (an Apple Design angelehnt)
- Sessionsteuerung über Cookies (nicht localStorage)
- Absicherung über Middleware-/Layout-Redirects bei fehlendem Token

---

## 🔐 Login-Mechanismus (Ablauf)

1. **Benutzer gibt ein:**
   - Benutzername
   - Bauhof-ID (z. B. `B001`)
   - Passwort

2. **JavaScript generiert daraus die E-Mail:**

   ```
   benutzername@bauhofid.tracknwork.de
   ```

3. **Login mit Supabase Auth (signInWithPassword)**

4. **Erhalt des Access Tokens**

5. **Token wird über einen geschützten POST-Request an den Controller `/Authentication/SetToken` geschickt**

6. **ASP.NET setzt den `supabase_token`-Cookie serverseitig**

7. **Der Cookie wird vom Backend (z. B. im `HomeController`) gelesen und validiert**

---

## 🧠 Benutzerverwaltung

- Supabase Admin User können via Node.js CLI erstellt werden
- Benutzer-Metadaten enthalten:
  - `display_name`
  - `bauhof_id`
  - `role`
- Anmeldung nur möglich mit registrierten Nutzern
- Keine Selbstregistrierung durch Endnutzer vorgesehen

---

## ⚙️ Lokale Entwicklung

### Voraussetzungen:

- Visual Studio 2022 oder höher
- .NET Framework 4.8
- Supabase-Projekt mit aktivierter Auth

### Setup:

1. Projekt aus GitHub klonen
2. Supabase-Anmeldedaten in `Login.cshtml` eintragen
3. Startprojekt auf `TrackNWork` setzen
4. Startseite ist automatisch die Login-Seite

---

## 🚀 Hosting bei IONOS

- Anwendung läuft direkt im `/`-Root-Verzeichnis
- Cookies funktionieren mit `SameSite=Lax`, `Secure`, `HttpOnly=false`
- IONOS erfordert serverseitiges Setzen von Cookies (JS wird ignoriert!)
- Weiterleitungen und Tokenprüfungen werden via `_Layout.cshtml` realisiert

---

## 📂 Ordnerstruktur (Ausschnitt)

```
/Controllers
  - AuthenticationController.cs
  - HomeController.cs

/Views
  /Authentication
    - Login.cshtml
  /Home
    - Index.cshtml

/Scripts
  - supabase-js.js

/App_Data
  - token-log.txt (nur Debugzwecke)
```

---

## 📌 Nächste Schritte (Roadmap)

- [ ] Adminbereich zum Verwalten von Einträgen, Geräten, Fahrzeugen
- [ ] Übersicht für Mitarbeiterdaten
- [ ] Offline-Fähigkeit / PWA-Funktionalität
- [ ] Erweiterung der Benutzerrollenverwaltung
- [ ] Internationalisierung vorbereiten (derzeit nur Deutsch)

---

## 💬 Kontakt

Erstellt von [@Smudoo7](https://github.com/Smudoo7)
