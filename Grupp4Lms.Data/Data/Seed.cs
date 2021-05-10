using Bogus;
using Grupp4Lms.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grupp4Lms.Data.Data
{
    /// <summary>
    /// Seed klass som skapar data i databasen
    /// </summary>
    public class Seed
    {
        /// <summary>
        /// Async metod som skapar data i databasen
        /// </summary>
        /// <param name="services">IServiceProvider</param>
        /// <returns>Task</returns>
        public static async Task InitAsync(IServiceProvider services)
        {
            using (var db = services.GetRequiredService<ApplicationDbContext>())
            {
                Faker fake = new Faker("sv");

                if (db.Forfattar.Any())
                    return;

                db.Database.EnsureDeleted();
                db.Database.Migrate();

                // Skapa nivåer

                #region Skapa nivåer

                // Skapa nivåer
                List<Niva> lsNivaer = new List<Niva>();
                Niva niva1 = new Niva();
                niva1.Namn = "Nybörjare";
                lsNivaer.Add(niva1);

                Niva niva2 = new Niva();
                niva2.Namn = "Medel";
                lsNivaer.Add(niva2);

                Niva niva3 = new Niva();
                niva3.Namn = "Avancerad";
                lsNivaer.Add(niva3);

                Niva niva4 = new Niva();
                niva4.Namn = "Annat";
                lsNivaer.Add(niva4);

                await db.Niva.AddRangeAsync(lsNivaer);

                #endregion // End of skapa nivåer

                // Skapa ämnen               

                #region Skapa ämnen

                // Jag vill ha ämne Annat först i listan av ämnen
                Amne amne1 = new Amne();
                amne1.Namn = "Annat";
                db.Amne.Add(amne1);
                db.SaveChanges();


                List<Amne> lsAmnen = new List<Amne>();

                Amne amne2 = new Amne();
                amne2.Namn = "Biografier";
                lsAmnen.Add(amne2);

                Amne amne3 = new Amne();
                amne3.Namn = "Data och IT";
                lsAmnen.Add(amne3);

                Amne amne4 = new Amne();
                amne4.Namn = "Deckare";
                lsAmnen.Add(amne4);

                Amne amne5 = new Amne();
                amne5.Namn = "Djur och Natur";
                lsAmnen.Add(amne5);

                Amne amne6 = new Amne();
                amne6.Namn = "Ekonomi och ledarskap";
                lsAmnen.Add(amne6);

                Amne amne7 = new Amne();
                amne7.Namn = "Familj och Hälsa";
                lsAmnen.Add(amne7);

                Amne amne8 = new Amne();
                amne8.Namn = "Film, radio och TV";
                lsAmnen.Add(amne8);

                Amne amne9 = new Amne();
                amne9.Namn = "Filosofi och religion";
                lsAmnen.Add(amne9);

                Amne amne10 = new Amne();
                amne10.Namn = "Hem och trädgård";
                lsAmnen.Add(amne10);

                Amne amne11 = new Amne();
                amne11.Namn = "Historia och arkeologi";
                lsAmnen.Add(amne11);

                Amne amne12 = new Amne();
                amne12.Namn = "Juridik";
                lsAmnen.Add(amne12);

                Amne amne13 = new Amne();
                amne13.Namn = "Kultur";
                lsAmnen.Add(amne13);

                Amne amne14 = new Amne();
                amne14.Namn = "Läromedel";
                lsAmnen.Add(amne14);

                Amne amne15 = new Amne();
                amne15.Namn = "Mat och dryck";
                lsAmnen.Add(amne15);

                Amne amne16 = new Amne();
                amne16.Namn = "Medicin";
                lsAmnen.Add(amne16);

                Amne amne17 = new Amne();
                amne17.Namn = "Naturvetenskap och teknik";
                lsAmnen.Add(amne17);

                Amne amne18 = new Amne();
                amne18.Namn = "Psykologi och pedagogik";
                lsAmnen.Add(amne18);

                Amne amne19 = new Amne();
                amne19.Namn = "Resor";
                lsAmnen.Add(amne19);

                Amne amne20 = new Amne();
                amne20.Namn = "Samhälle och politik";
                lsAmnen.Add(amne20);

                Amne amne21 = new Amne();
                amne21.Namn = "Skönlitteratur";
                lsAmnen.Add(amne21);

                Amne amne22 = new Amne();
                amne22.Namn = "Sport, fritid och hobby";
                lsAmnen.Add(amne22);

                Amne amne23 = new Amne();
                amne23.Namn = "Språk och Ordböcker";
                lsAmnen.Add(amne23);

                Amne amne24 = new Amne();
                amne24.Namn = "Uppslagsverk och referenser";
                lsAmnen.Add(amne24);

                db.Amne.AddRange(lsAmnen);
                await db.SaveChangesAsync();

                #endregion // End of skapa ämnen

                // Skapa lite böcker

                #region Skapa böcker inom Data och IT böcker

                Amne dataAmne = db.Amne.Where(n => n.Namn == "Data och IT").FirstOrDefault();
                int amneId = dataAmne.AmneId;

                Niva bokNiva = db.Niva.Where(n => n.Namn == "Avancerad").FirstOrDefault();

                // Skapa bok 1
                // Skapa författare
                Forfattare forfattar1 = new Forfattare();
                forfattar1.ForNamn = "Daniel";
                forfattar1.EfterNamn = "Akenine";
                forfattar1.FodelseDatum = new DateTime(1974, 1, 17);
                db.Forfattar.Add(forfattar1);
                //db.SaveChanges();


                // Skapa litteratur
                Litteratur litteratur1 = new Litteratur();
                litteratur1.AmneId = amneId;
                litteratur1.NivaId = bokNiva.NivaId;

                litteratur1.Titel = "Fundamentals of IT architecture";
                litteratur1.Url = "https://www.bokus.com/bok/9789176972458/fundamentals-of-it-architecture/";
                litteratur1.UtgivningsDatum = new DateTime(2021, 05, 05);
                litteratur1.Beskrivning = "The bestseller in IT architecture. If you want to understand IT architecture, this book is for you. It covers many different areas, from strategy to business, technology, and software architecture. Are you working as an IT architect, student, developer, CTO, project manager, or IT professional? If so, this book will help you understand how IT architecture can help address the complexity in a modern organization. It will give you a deeper understanding of the concepts, methods, tools, models, organizations, and frameworks involved. It also covers the social and human aspects of working as an IT architect.";

                forfattar1.Litteratur = new List<Litteratur>();
                forfattar1.Litteratur.Add(litteratur1);

                litteratur1.Forfattare = new List<Forfattare>();
                litteratur1.Forfattare.Add(forfattar1);
                await db .Litteratur.AddAsync(litteratur1);


                // Skapa bok 2
                // Skapa författare

                Forfattare forfattar2 = new Forfattare();
                forfattar2.ForNamn = "Joseph";
                forfattar2.EfterNamn = "Albahari";
                forfattar2.FodelseDatum = new DateTime(1965, 1, 11);
                db.Forfattar.Add(forfattar2);
                db.SaveChanges();


                // Skapa litteratur
                Litteratur litteratur2 = new Litteratur();
                litteratur2.AmneId = amneId;
                litteratur2.NivaId = bokNiva.NivaId;

                litteratur2.Titel = "C# 9.0 in a Nutshell";
                litteratur2.Url = "https://www.bokus.com/bok/9781098100964/c-90-in-a-nutshell/";
                litteratur2.UtgivningsDatum = new DateTime(2021, 03, 16);
                litteratur2.Beskrivning = "When you have questions about C# 9.0 or .NET 5, this best-selling guide has the answers you need. C# is a language of unusual flexibility and breadth, but with its continual growth there's so much more to learn. In the tradition of O'Reilly's Nutshell guides, this thoroughly updated edition is simply the best one-volume reference to the C# language available today. Organized around concepts and use cases, C# 9.0 in a Nutshell provides intermediate and advanced programmers with a concise map of C# and .NET that also plumbs significant depths. Get up to speed on C#, from syntax and variables to advanced topics such as pointers, records, closures, and patterns Dig deep into LINQ with three chapters dedicated to the topic Explore concurrency and asynchrony, advanced threading, and parallel programming Work with .NET features, including regular expressions, networking, spans, reflection, and cryptography";

                forfattar2.Litteratur = new List<Litteratur>();
                forfattar2.Litteratur.Add(litteratur2);

                litteratur2.Forfattare = new List<Forfattare>();
                litteratur2.Forfattare.Add(forfattar2);
                await db .Litteratur.AddAsync(litteratur2);



                // Skapa bok 3
                // Skapa författare
                Forfattare forfattar3 = new Forfattare();
                forfattar3.ForNamn = "Thomas";
                forfattar3.EfterNamn = "Lee";
                forfattar3.FodelseDatum = new DateTime(1963, 1, 11);
                db.Forfattar.Add(forfattar3);
                //db.SaveChanges();


                // Skapa litteratur
                Litteratur litteratur3 = new Litteratur();
                litteratur3.AmneId = amneId;
                litteratur3.NivaId = bokNiva.NivaId;

                litteratur3.Titel = "PowerShell 7 for IT Professionals";
                litteratur3.Url = "https://www.bokus.com/bok/9781119644729/powershell-7-for-it-professionals/";
                litteratur3.UtgivningsDatum = new DateTime(2021, 02, 25);
                litteratur3.Beskrivning = "Take advantage of everything Microsoft's new PowerShell 7 has to offer PowerShell 7 for IT Pros is your guide to using PowerShell 7, the open source, cross-platform version of Windows PowerShell. Windows IT professionals can begin setting up automation in PowerShell 7, which features many improvements over the early version of PowerShell Core and Windows PowerShell. PowerShell 7 users can enjoy the high level of compatibility with the Windows PowerShell modules they rely on today. This book shows IT professionals-especially Windows administrators and developers-how to use PowerShell7 to engage in their most important tasks, such as managing networking, using AD/DNS/DHCP, leveraging Azure, and more. To make it easy to learn everything PowerShell 7 has to offer, this book includes robust examples, each containing sample code so readers can follow along. Scripts are based on PowerShell 7 running on Windows 10 19H1 or later and Windows Server 2019. * Learn to navigate the PowerShell 7 administrative environment * Use PowerShell 7 to automate networking, Active Directory, Windows storage, shared data, and more * Run Windows Update, IIS, Hyper-V, and WMI and CIM cmdlets within PowerShell 7";

                forfattar3.Litteratur = new List<Litteratur>();
                forfattar3.Litteratur.Add(litteratur3);

                litteratur3.Forfattare = new List<Forfattare>();
                litteratur3.Forfattare.Add(forfattar3);
                await db.Litteratur.AddAsync(litteratur3);

                await db.SaveChangesAsync();

                #endregion // End of region Skapa några Data och IT böcker

                #region Skapa böcker inom Biografi

                dataAmne = db.Amne.Where(n => n.Namn == "Biografier").FirstOrDefault();
                amneId = dataAmne.AmneId;

                bokNiva = db.Niva.Where(n => n.Namn == "Medel").FirstOrDefault();

                // Skapa författare
                forfattar1 = new Forfattare();
                forfattar1.ForNamn = "Hanif";
                forfattar1.EfterNamn = "Azizi";
                forfattar1.FodelseDatum = new DateTime(1985, 5, 5);
                db.Forfattar.Add(forfattar1);
                //db.SaveChanges();


                // Skapa litteratur
                litteratur1 = new Litteratur();
                litteratur1.AmneId = amneId;
                litteratur1.NivaId = bokNiva.NivaId;

                litteratur1.Titel = "Förortssnuten";
                litteratur1.Url = "https://www.bokus.com/bok/9789100184605/forortssnuten/";
                litteratur1.UtgivningsDatum = new DateTime(2021, 3, 31);
                litteratur1.Beskrivning = "Hanif Azizi växer upp på en militärbas i Iraks öken. Hans föräldrar är krigare för den iranska rebellrörelsen Folkets Mujahedin och kampen mot Khomeini genomsyrar hela tillvaron. Efter att pappan dödats i kriget tar nioårige Hanif sin lillebror i handen och påbörjar en flykt som så småningom tar honom till Sverige. Han har svårt att finna sig tillrätta i det nya landet och i tonåren får han kontakt med den terroriststämplade rebellrörelsen på nytt. Lockad av gemenskap och en möjlig återförening med sin mamma åker han till Irak för att bli en krigare i Folkets Mujahedin. Berättelsen kunde ha slutat här, men något händer som får livet att vända om. Istället återvänder Hanif till Sverige - och utbildar sig till polis. Han söker en tjänst i Rinkeby, och får snart vara med om saker han aldrig trodde skulle kunna ske i Sverige.";

                forfattar1.Litteratur = new List<Litteratur>();
                forfattar1.Litteratur.Add(litteratur1);

                litteratur1.Forfattare = new List<Forfattare>();
                litteratur1.Forfattare.Add(forfattar1);
                await db.Litteratur.AddAsync(litteratur1);


                // Skapa författare
                forfattar2 = new Forfattare();
                forfattar2.ForNamn = "Edward";
                forfattar2.EfterNamn = "Snowden";
                forfattar2.FodelseDatum = new DateTime(1983, 7, 11);
                db.Forfattar.Add(forfattar2);
                //db.SaveChanges();


                // Skapa litteratur
                litteratur2 = new Litteratur();
                litteratur2.AmneId = amneId;
                litteratur2.NivaId = bokNiva.NivaId;

                litteratur2.Titel = "Permanent Record";
                litteratur2.Url = "https://www.bokus.com/bok/9781529035650/permanent-record/";
                litteratur2.UtgivningsDatum = new DateTime(2019, 9, 17);
                litteratur2.Beskrivning = "THE SUNDAY TIMES TOP TEN BESTSELLER Edward Snowden, the man who risked everything to expose the US government's system of mass surveillance, reveals for the first time the story of his life, including how he helped to build that system and what motivated him to try to bring it down. In 2013, twenty-nine-year-old Edward Snowden shocked the world when he broke with the American intelligence establishment and revealed that the United States government was secretly pursuing the means to collect every single phone call, text message, and email. The result would be an unprecedented system of mass surveillance with the ability to pry into the private lives of every person on earth. Six years later, Snowden reveals for the very first time how he helped to build this system and why he was moved to expose it. Spanning the bucolic Beltway suburbs of his childhood and the clandestine CIA and NSA postings of his adulthood, Permanent Record is the extraordinary account of a bright young man who...";

                forfattar2.Litteratur = new List<Litteratur>();
                forfattar2.Litteratur.Add(litteratur2);

                litteratur2.Forfattare = new List<Forfattare>();
                litteratur2.Forfattare.Add(forfattar2);
                await db.Litteratur.AddAsync(litteratur2);


                // Skapa författare
                forfattar3 = new Forfattare();
                forfattar3.ForNamn = "Johan";
                forfattar3.EfterNamn = "Gustafsson";
                forfattar3.FodelseDatum = new DateTime(1975, 1, 11);
                db.Forfattar.Add(forfattar3);
                //db.SaveChanges();


                // Skapa litteratur
                litteratur3 = new Litteratur();
                litteratur3.AmneId = amneId;
                litteratur3.NivaId = bokNiva.NivaId;

                litteratur3.Titel = "Ett fängelse utan murar";
                litteratur3.Url = "https://www.bokus.com/bok/9789180020701/ett-fangelse-utan-murar/";
                litteratur3.UtgivningsDatum = new DateTime(2021, 4, 1);
                litteratur3.Beskrivning = "Mina ögon mötte för ett ögonblick gevärets mynning och det svarta stålet stirrade tillbaka. Blodet rusade i tinningen. Vi stod där och tittade på varandra. Ett lätt tryck med hans finger, en impuls skulle ha räckt. Långsamt höjde jag händerna för att visa att jag gav mig. I det ögonblicket förändrades allt. Under en resa genom Afrika kidnappas Johan Gustafsson i Timbuktu, den legendariska utposten mot Sahara. Han och två medfångar förs genom ett oändligt ökenlandskap, en förflyttning i både tid och rum, till ett universum där liv och död kretsar kring religionen och ett heligt krig. Ett fängelse utan murar är en berättelse om 2039 dagar i fångenskap, om att överleva i den mest hopplösa av situationer, om att upprätthålla sitt människovärde.Och om att fortsätta drömma om att en dag få komma hem igen.";

                forfattar3.Litteratur = new List<Litteratur>();
                forfattar3.Litteratur.Add(litteratur3);

                litteratur3.Forfattare = new List<Forfattare>();
                litteratur3.Forfattare.Add(forfattar3);
                await db.Litteratur.AddAsync(litteratur3);

                await db.SaveChangesAsync();

                #endregion // End of Skapa böcker inom Biografi

                #region Skapa böcker inom Språk och ordböcker

                dataAmne = db.Amne.Where(n => n.Namn == "Språk och Ordböcker").FirstOrDefault();
                amneId = dataAmne.AmneId;

                bokNiva = db.Niva.Where(n => n.Namn == "Nybörjare").FirstOrDefault();

                // Skapa författare
                forfattar1 = new Forfattare();
                forfattar1.ForNamn = "Ann";
                forfattar1.EfterNamn = "Skansholm";
                forfattar1.FodelseDatum = new DateTime(1985, 1, 1);
                //db.Forfattar.Add(forfattar1);


                // Skapa litteratur
                litteratur1 = new Litteratur();
                litteratur1.AmneId = amneId;
                litteratur1.NivaId = bokNiva.NivaId;

                litteratur1.Titel = "Handbok i uppsatsskrivande - - för psykologiämnet";
                litteratur1.Url = "https://www.bokus.com/bok/9789144136400/handbok-i-uppsatsskrivande-for-psykologiamnet/";
                litteratur1.UtgivningsDatum = new DateTime(2021, 4, 9);
                litteratur1.Beskrivning = "Den akademiska uppsatsen utgör avslutningen på en akademisk utbildning. Uppsatsen består av många olika delar och att förstå hur dessa relaterar till varandra kan vara svårt. I Handbok i uppsatsskrivande redogör författarna på ett pedagogiskt sätt för de delar som bygger upp en akademisk uppsats - från problemställning och tidigare forskning, via val av teori och metod till resultat och analys.";

                forfattar1.Litteratur = new List<Litteratur>();
                forfattar1.Litteratur.Add(litteratur1);

                litteratur1.Forfattare = new List<Forfattare>();
                litteratur1.Forfattare.Add(forfattar1);
                await db.Litteratur.AddAsync(litteratur1);

                await db.SaveChangesAsync();

                #endregion // End of Språk och ordböcker

                #region Skapa böcker inom Familj och Hälsa

                // 3 böcker med samma författare
                dataAmne = db.Amne.Where(n => n.Namn == "Familj och Hälsa").FirstOrDefault();
                amneId = dataAmne.AmneId;

                bokNiva = db.Niva.Where(n => n.Namn == "Nybörjare").FirstOrDefault();

                // Skapa författare
                forfattar1 = new Forfattare();
                forfattar1.ForNamn = "Anders";
                forfattar1.EfterNamn = "Hansen";
                forfattar1.FodelseDatum = new DateTime(1974, 1, 24);
                db.Forfattar.Add(forfattar1);
                //db.SaveChanges();


                // Skapa litteratur
                litteratur1 = new Litteratur();
                litteratur1.AmneId = amneId;
                litteratur1.NivaId = bokNiva.NivaId;

                litteratur1.Titel = "Hjärnstark : hur motion och träning stärker din hjärna";
                litteratur1.Url = "https://www.bokus.com/bok/9789175038452/hjarnstark-hur-motion-och-traning-starker-din-hjarna/";
                litteratur1.UtgivningsDatum = new DateTime(2018, 5, 3);
                litteratur1.Beskrivning = "Vill du bli mer stresstålig, må bättre, förbättra ditt minne och bli mer kreativ och intelligent? Se då till att röra på dig! Regelbunden träning har nämligen visat sig vara den bästa hjärngympan som finns, bättre än sudoku, korsord och alla tänkbara kosttillskott tillsammans. Man vet i dag att hjärnan är enormt föränderlig.Det bildas ständigt nya hjärnceller och nya kopplingar skapas och försvinner.Allt du gör, till och med varje tanke du tänker, förändrar din hjärna lite grann.Och det gör i allra högsta grad även motion och träning.När du rör på dig blir du inte bara piggare och mår bättre, det påverkar också koncentrationen, minnet, måendet, sömnen, kreativiteten och stresståligheten -till och med din personlighet och intelligens. Du tänker helt enkelt snabbare och kan lägga i en extra mental växel, till exempel för att kunna koncentrera dig när det är stökigt omkring dig.Och alla som rör på sig får de här fördelarna, barn, vuxna och gamla. I Hjärnstark visar Anders Hansen, överläkare i psykiatri och författare till boken Hälsa på recept, vilka mekanismer i din hjärna som omvandlar promenaden eller löprundan ti...";

                litteratur1.Forfattare = new List<Forfattare>();
                litteratur1.Forfattare.Add(forfattar1);

                forfattar1.Litteratur = new List<Litteratur>();
                forfattar1.Litteratur.Add(litteratur1);

                await db.Litteratur.AddAsync(litteratur1);


                // Skapa litteratur
                litteratur2 = new Litteratur();
                litteratur2.AmneId = amneId;
                litteratur2.NivaId = bokNiva.NivaId;

                litteratur2.Titel = "Fördel ADHD : var på skalan ligger du?";
                litteratur2.Url = "https://www.bokus.com/bok/9789174246605/fordel-adhd-var-pa-skalan-ligger-du/";
                litteratur2.UtgivningsDatum = new DateTime(2017, 9, 12);
                litteratur2.Beskrivning = "Känner du någon som är driven, kreativ, orädd, ifrågasättande, flexibel, envis och initiativrik? Som ofta tänker utanför boxen och verkar kunna skaka av sig nästan vilken motgång som helst? Kanske känner du igen dig själv? Förmodligen blir du lite förvånad när du hör att dessa positiva egenskaper är typiska för personer med ADHD! Vi som så länge har betraktat denna århundradets diagnos som något som bara för med sig problem. I dag tror man att dessa delvis nedärvda personlighetsdrag var nödvändiga för att våra förfäder skulle överleva.Den som inte orkade jaga sitt byte eller kunde hyperfokusera på savannen svalt ihjäl eller blev själv ett byte för vilda djur. Det var helt enkelt en stor fördel att ha vissa egenskaper, som vi i dag betraktar som en diagnos. Vi vet också att vi alla har mer eller mindre drag av ADHD och att vi alla ligger på skalan -vissa långt borta i den ena änden, andra i den helt motsatta";

                forfattar1.Litteratur.Add(litteratur2);

                litteratur2.Forfattare = new List<Forfattare>();
                litteratur2.Forfattare.Add(forfattar1);
                await db.Litteratur.AddAsync(litteratur2);


                // Skapa litteratur
                litteratur3 = new Litteratur();
                litteratur3.AmneId = amneId;
                litteratur3.NivaId = bokNiva.NivaId;

                litteratur3.Titel = "Skärmhjärnan : hur en hjärna i osynk med sin tid kan göra oss stressade, deprimerade och ångestfyllda";
                litteratur3.Url = "https://www.bokus.com/bok/9789178871582/skarmhjarnan-hur-en-hjarna-i-osynk-med-sin-tid-kan-gora-oss-stressade-deprimerade-och-angestfyllda/";
                litteratur3.UtgivningsDatum = new DateTime(2020, 11, 12);
                litteratur3.Beskrivning = "Den psykiska ohälsan håller på att ta över som det stora hälsohotet i vår tid. Ett högt tempo, konstant stress och en digital livsstil med ständig uppkoppling börjar få konsekvenser för vår hjärna. För hur mycket du än gillar att kolla bildflödet på instagram, nyheterna i mobilen eller filmer på plattan, är din hjärna inte anpassad till det som dagens samhälle för med sig. Den är helt enkelt i osynk med vår tid! Men det betyder inte att du står maktlös och att det saknas lösningar - med lite mer kunskap om hur hjärnan fungerar kommer du snart att inse att det egentligen handlar om ganska enkla och basala saker. Den mänskliga hjärnan är skapad i en helt annan tid, och kanske borde vi visa den lite mer hänsyn. Så följ med på en spännande resa och få en helt ny bild av det som händer och sker i ditt huvud!";

                forfattar1.Litteratur.Add(litteratur3);

                litteratur3.Forfattare = new List<Forfattare>();
                litteratur3.Forfattare.Add(forfattar1);
                await db.Litteratur.AddAsync(litteratur3);

                await db.SaveChangesAsync();

                #endregion // End of region Skapa böcker inom Familj och Hälsa

                #region Skapa böcker inom Psykologi och pedagogik

                // 2 böcker med samma författare
                dataAmne = db.Amne.Where(n => n.Namn == "Psykologi och pedagogik").FirstOrDefault();
                amneId = dataAmne.AmneId;

                bokNiva = db.Niva.Where(n => n.Namn == "Medel").FirstOrDefault();

                // Skapa författare
                forfattar1 = new Forfattare();
                forfattar1.ForNamn = "Thomas";
                forfattar1.EfterNamn = "Ericsson";
                forfattar1.FodelseDatum = new DateTime(1965, 9, 19);
                db.Forfattar.Add(forfattar1);
                //db.SaveChanges();


                // Skapa litteratur
                litteratur1 = new Litteratur();
                litteratur1.AmneId = amneId;
                litteratur1.NivaId = bokNiva.NivaId;

                litteratur1.Titel = "Omgiven av dåliga chefer : varför bra ledarskap är så sällsynt";
                litteratur1.Url = "https://www.bokus.com/bok/9789137152264/omgiven-av-daliga-chefer-varfor-bra-ledarskap-ar-sa-sallsynt/";
                litteratur1.UtgivningsDatum = new DateTime(2018, 9, 26);
                litteratur1.Beskrivning = "I Omgiven av dåliga chefer får du lära dig att handskas med de mest hopplösa chefer du kan föreställa dig. Thomas Erikson, författare till succéböckerna Omgiven av idioter och Omgiven av psykopater, vet vad han talar om. Själv har han varit både chef och anställd och i sin roll som ledarskapsutvecklare har han mött alla typer av chefer. Om dina barn inte beter sig som förväntat kan du alltid ändra taktik i din uppfostran. Om du har en bekant som visar sig vara en skitstövel kan du givetvis gå därifrån. Men om däremot din chef uppträder orättvist och ställer orimliga krav är det plötsligt inte lika enkelt att veta vad man ska ta sig till.Vi har alla varit där.Chefen och du tycks inte komma från samma planet.Och även om du inte råkat ut för chefen från helvetet kanske du har fått vissa obehagliga överraskningar i mötet med din chef.Kanske har du tilldelats stort ansvar men inga befogenheter? Kanske har du och din chef olika personlighetsprofiler och fungerar på totalt olika sätt? Och kanske misstänker du ibland att din chef faktiskt är ett hopplöst fall. Lugn.Det finns lösningar. På ett lättsamt och underhållande sätt visar boken hur människors olika beteenden kan påverkas. Och går också igenom vad som utmärker en exemplarisk ledarty";
               
                forfattar1.Litteratur = new List<Litteratur>();
                forfattar1.Litteratur.Add(litteratur1);

                litteratur1.Forfattare = new List<Forfattare>();
                litteratur1.Forfattare.Add(forfattar1);

                await db.Litteratur.AddAsync(litteratur1);


                // Skapa litteratur
                litteratur2 = new Litteratur();
                litteratur2.AmneId = amneId;
                litteratur2.NivaId = bokNiva.NivaId;

                litteratur2.Titel = "Omgiven av idioter : hur man förstår dem som inte går att förstå";
                litteratur2.Url = "https://www.bokus.com/bok/9789175038407/omgiven-av-idioter-hur-man-forstar-dem-som-inte-gar-att-forsta/";
                litteratur2.UtgivningsDatum = new DateTime(2018, 4, 9);
                litteratur2.Beskrivning = "Omgiven av idioter beskriver konkret och underhållande en av världens mest spridda metoder för att sortera olikheterna inom mänsklig kommunikation. Med vetenskapliga grund och genom vardagens möten ger den dig konkreta hjälpmedel att förstå de viktigaste skillnaderna mellan olika kommunikationsstilar. Den visar på en av de vanligaste orsakerna till konflikter i vardagen: dålig kommunikation. Och den ger dig som läsare svaret på frågan: Vad gör jag åt det?";
                litteratur2.Forfattare = new List<Forfattare>();

                forfattar1.Litteratur.Add(litteratur2);

                litteratur2.Forfattare.Add(forfattar1);
                await db.Litteratur.AddAsync(litteratur2);

                await db.SaveChangesAsync();

                #endregion // End of region Skapa böcker inom Psykologi och pedagogik
            }
        }
    }
}
