using System;
using System.Collections.Generic;
using System.Linq;
using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{
    class VerwaltungProgramm
    {
        /// <summary>
        /// Diese Liste enthält alle Gefangene/Strategien
        /// </summary>
        private static List<BasisStrategie> _strategien;

        private static int punkte1, punkte2, gesamtPunkte1, gesamtPunkte2, schwere, runde;
        private static bool automatischesVerhoer;

        static void Main(string[] args)
        {
            //bekannt machen der ganzen strategien
            _strategien = new List<BasisStrategie>();
            _strategien.Add(new GrollStrategie());
            _strategien.Add(new VerrateImmerStrategie());
            _strategien.Add(new Strategie1());
            _strategien.Add(new Strategie2());
            _strategien.Add(new Strategie3());
            // Mensch vs Maschine
            _strategien.Add(new Strategie4());



            string eingabe = null;
            do
            {
                // Begrüßung
                Console.WriteLine("Willkommen zum Gefangenendilemma");
                Console.WriteLine("0 - Verhör zwischen 2 Gefangene");
                Console.WriteLine("1 - Automatisches Verhör zwischen 2 Strategien.");
                Console.WriteLine("2 - Mensch vs Computer.");
                Console.WriteLine("3 - Turnier");
                Console.WriteLine("X - Beenden");

                // Eingabe
                Console.Write("Treffen Sie ihre Option: ");
                eingabe = Console.ReadLine();

                // Auswerten der Eingabe
                switch (eingabe)
                {
                    case "0":
                        Gefangene2();
                        break;
                    case "1":
                        AutomatischerVerhoerer();
                        break;
                    case "2":
                        MenschGegenMaschine();
                        break;
                    case "3":
                        Turnier();
                        break;
                    default:
                        Console.WriteLine($"Eingabe {eingabe} nicht erkannt.");
                        break;
                }
            } while (!"x".Equals(eingabe?.ToLower()));
        }
        
        static void Gefangene2()
        {
            int st1, st2;

            // Punkte reset
            punkte1 = 0;
            punkte2 = 0;

            automatischesVerhoer = false;
            
            Console.WriteLine("Willkommen zum Verhör zwischen 2 Strategien");
            for (int i = 0; i < _strategien.Count; i++)
            {
                Console.WriteLine($"{i} - {_strategien[i].Name()}");
            }

            Console.WriteLine("Wählen Sie ihre 2 Gefangene:");
            st1 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 1. Strategie", 0, _strategien.Count);
            st2 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 2. Strategie", 0, _strategien.Count);
            runde = VerwaltungKram.EingabeZahlMinMax("Wie viele Runden sollen diese verhört werden?", 1, 101);
            schwere = VerwaltungKram.EingabeZahlMinMax("Wie schwer sind die Verstöße? (0=leicht, 1=mittel, 2=schwer)", 0, 3);

            Verhoer(st1, st2, runde, schwere);
        }

        /// <summary>
        /// Startet ein Verhör zwischen der Strategie an der Position st1 und Position st2 über die Länge von runde und der Schwere schwere
        /// </summary>
        /// <param name="st1"></param>
        /// <param name="st2"></param>
        /// <param name="runde"></param>
        /// <param name="schwere"></param>
        static void Verhoer(int st1, int st2, int runde, int schwere)
        {
            //holt die beiden Strategien aus der Collection.
            BasisStrategie strategie1 = _strategien[st1];
            BasisStrategie strategie2 = _strategien[st2];
            int reaktion1 = BasisStrategie.NochNichtVerhoert;
            int reaktion2 = BasisStrategie.NochNichtVerhoert;

            //beide Strategien über den Start informieren (Also es wird die Startmethode aufgerufen)
            strategie1.Start(runde, schwere);
            strategie2.Start(runde, schwere);


            //start
            for (int i = 0; i < runde; i++)
            {
                //beide verhören
                int aktReaktion1 = strategie1.Verhoer(reaktion2);
                int aktReaktion2 = strategie2.Verhoer(reaktion1);

                //punkte berechnen
                switch (schwere)
                {
                    case 0:
                        Leicht(aktReaktion1, aktReaktion2);
                        break;
                    case 1:
                        Mittel(aktReaktion1, aktReaktion2);
                        break;
                    case 2:
                        Schwer(aktReaktion1, aktReaktion2);
                        break;
                }

                //reaktion für den nächsten durchlauf merken
                reaktion1 = aktReaktion1;
                reaktion2 = aktReaktion2;
            }

            //ausgabe
            if (automatischesVerhoer)
            {
                switch (runde)
                {
                    case 5:
                        punkte1 *= 20;
                        punkte2 *= 20;
                        break;
                    case 25:
                        punkte1 *= 4;
                        punkte2 *= 4;
                        break;
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"{strategie1.Name()} hat {punkte1} Punkte erhalten.");
                Console.WriteLine($"{strategie2.Name()} hat {punkte2} Punkte erhalten.");
                Console.WriteLine("Somit hat {0} gewonnen.", punkte1 < punkte2 ? strategie1.Name() : strategie2.Name());
            }
            

           
        }

        /// <summary>
        /// Berechnet für schwere Verstöße die Punkte und verwendet die 2 letzten Eingabeparameter als Rückgabe
        /// </summary>
        /// <param name="aktReaktion1"></param>
        /// <param name="aktReaktion2"></param>
        /// <param name="punkte1"></param>
        /// <param name="punkte2"></param>
        static void Leicht(int aktReaktion1, int aktReaktion2)
        {
            if (aktReaktion1 == BasisStrategie.Kooperieren && aktReaktion2 == BasisStrategie.Kooperieren)
            {
                punkte1 += 3;
                punkte2 += 3;
            }
            else if (aktReaktion1 == BasisStrategie.Verrat && aktReaktion2 == BasisStrategie.Kooperieren)
            {
                punkte1 += 0;
                punkte2 += 9;
            }
            else if (aktReaktion1 == BasisStrategie.Kooperieren && aktReaktion2 == BasisStrategie.Verrat)
            {
                punkte1 += 9;
                punkte2 += 0;
            }
            else
            {
                punkte1 += 6;
                punkte2 += 6;
            }
        }

        static void Mittel(int aktReaktion1, int aktReaktion2)
        {
            if (aktReaktion1 == BasisStrategie.Kooperieren && aktReaktion2 == BasisStrategie.Kooperieren)
            {
                punkte1 += 10;
                punkte2 += 10;
            }
            else if (aktReaktion1 == BasisStrategie.Verrat && aktReaktion2 == BasisStrategie.Kooperieren)
            {
                punkte1 += 8;
                punkte2 += 0;
            }
            else if (aktReaktion1 == BasisStrategie.Kooperieren && aktReaktion2 == BasisStrategie.Verrat)
            {
                punkte1 += 0;
                punkte2 += 8;
            }
            else
            {
                punkte1 += 4;
                punkte2 += 4;
            }
        }

        static void Schwer(int aktReaktion1, int aktReaktion2)
        {
            if (aktReaktion1 == BasisStrategie.Kooperieren && aktReaktion2 == BasisStrategie.Kooperieren)
            {
                punkte1 += 4;
                punkte2 += 4;
            }
            else if (aktReaktion1 == BasisStrategie.Verrat && aktReaktion2 == BasisStrategie.Kooperieren)
            {
                punkte1 += 0;
                punkte2 += 10;
            }
            else if (aktReaktion1 == BasisStrategie.Kooperieren && aktReaktion2 == BasisStrategie.Verrat)
            {
                punkte1 += 10;
                punkte2 += 0;
            }
            else
            {
                punkte1 += 8;
                punkte2 += 8;
            }
        }


        static void Turnier()
        {
            Console.WriteLine();
            Console.WriteLine("Turnier beginnt!");
            Console.WriteLine();

            automatischesVerhoer = true;

            int st1 = 0, st2 = 0;


            var punkte = new Dictionary<string, int>();
            foreach (var str in _strategien)
            {
                punkte.Add(str.Name(), 0);
            }
            
            
            for (int x = 0; x < _strategien.Count; x++)
            {
                st1 = x;

                for (int y = 1; y < _strategien.Count - x; y++)
                {
                    st2 = y + x;
                    schwere = 0;

                    //für jeden Schwierigkeitsgrad 1 Durchlauf
                    for (int a = 0; a < 3; a++)
                    {
                        //beginnt mit 5 Runden
                        runde = 5;

                        //jeweils 1 Durchlauf für 5, 25, 100 Runden
                        for (int b = 0; b < 3; b++)
                        {
                            //setzt Punkte zurück
                            punkte1 = 0;
                            punkte2 = 0;

                            string strategie1 = _strategien[st1].Name();
                            string strategie2 = _strategien[st2].Name();

                            //startet das Verhoer
                            Verhoer(st1, st2, runde, schwere);
                            
                            var strat1 = from val in punkte where val.Key == "strategie1" select val.Value;
                            var strat2 = from val in punkte where val.Key == "strategie2" select val.Value;
                            
                            
                            foreach( var strat in strat1 )Console.WriteLine("strat1 " + strat);
                            
                            
                            switch (runde)
                            {
                                //beim ersten Durchlauf (5R) werden die erreichten Punkte mal 20 gerechnet und auf die gesamtPunktzahl addiert
                                case 5:

                                    punkte[strategie1] += punkte1;
                                    punkte[strategie2] += punkte2;

                                    TurnierAusgabe(strategie1, strategie2);

                                    runde = 25;
                                    break;
                                //beim zweiten Durchlauf (25R) werden die erreichten Punkte mal 4 gerechnet und auf die gesamtPunktzahl addiert
                                case 25:

                                    punkte[strategie1] += punkte1;
                                    punkte[strategie2] += punkte2;

                                    TurnierAusgabe(strategie1, strategie2);

                                    runde = 100;
                                    break;
                                //beim dritten Durchlauf werden die erreichten Punkte auf die Gesamtpunktzahl addiert
                                case 100:
                                    punkte[strategie1] += punkte1;
                                    punkte[strategie2] += punkte2;

                                    TurnierAusgabe(strategie1, strategie2);
                                    break;
                            }
                        }
                        schwere++;
                    }
                }
            }
            
            Console.WriteLine("---------------------------------------------------");
            //sortiert die Punkte in aufsteigender Reihenfolge
            var sortedList = 
                from pair in punkte
                orderby pair.Value
                select pair;

            //managing
            bool first = true;
            string best = "";
            
            foreach (var value in sortedList)
            {
                //wenn es der erste eintrag ist (der kleinste, weil die liste sortiert wurde)
                if (first)
                {
                    first = false;
                    //speichere den strategie-namen
                    best = value.Key;
                }
                Console.WriteLine("{0} hat insgesamt {1} Punkte erhalten.", value.Key, value.Value);
            }
            
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine($"{best} hat gewonnen");
            Console.WriteLine("-------------------------------------------------------");
        }

        public static void TurnierAusgabe(string stat1, string stat2)
        {

            Console.WriteLine($"Runden: {runde}, Schwierigkeit: {schwere}");
            Console.WriteLine($"'{stat1}' erreichte {punkte1} Punkte.");
            Console.WriteLine($"'{stat2}' erreichte {punkte2} Punkte.");
            Console.WriteLine("Somit hat '{0}' gewonnen.", punkte1 < punkte2 ? stat1 : stat2);
            Console.WriteLine("*********************************************************************************");

        }

        static void AutomatischerVerhoerer()
        {
            //0=leicht, 1=mittel, 2=schwer
            int schwere = 0;

            automatischesVerhoer = true;

            gesamtPunkte1 = 0;
            gesamtPunkte2 = 0;
            
            Console.WriteLine("Willkommen zum Verhör zwischen 2 Strategien");
            for (int i = 0; i < _strategien.Count; i++)
            {
                Console.WriteLine($"{i} - {_strategien[i].Name()}");
            }

            Console.WriteLine("Wählen Sie ihre 2 Gefangene:");
            int st1 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 1. Strategie", 0, _strategien.Count);
            int st2 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 2. Strategie", 0, _strategien.Count);

            //für jeden Schwierigkeitsgrad 1 Durchlauf
            for (int a = 0; a < 3; a++)
            {
                //beginnt mit 5 Runden
                int runde = 5;
                
                //jewils 1 Durchlauf für 5, 25, 100 Runden
                for (int b = 0; b < 3; b++)
                {
                    //setzt Punkte zurück
                    punkte1 = 0;
                    punkte2 = 0;
                    
                    //startet das Verhoer
                    Verhoer(st1, st2, runde, schwere);

                    switch (runde)
                    {
                        //beim ersten Durchlauf (5R) werden die erreichten Punkte mal 20 gerechnet und auf die gesamtPunktzahl addiert
                        case 5:
                            runde = 25;
                            gesamtPunkte1 += punkte1;
                            gesamtPunkte2 += punkte2;
                            break;
                        //beim zweiten Durchlauf (25R) werden die erreichten Punkte mal 4 gerechnet und auf die gesamtPunktzahl addiert
                        case 25:
                            runde = 100;
                            gesamtPunkte1 += punkte1;
                            gesamtPunkte2 += punkte2;
                            break;
                        //beim dritten Durchlauf werden die erreichten Punkte auf die Gesamtpunktzahl addiert
                        case 100:
                            runde = 0;
                            gesamtPunkte1 += punkte1;
                            gesamtPunkte2 += punkte2;
                            break;
                    }
                }
                schwere++;
            }
            
            
            BasisStrategie strategie1 = _strategien[st1];
            BasisStrategie strategie2 = _strategien[st2];
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"{strategie1.Name()} hat insgesamt {gesamtPunkte1} Punkte erhalten.");
            Console.WriteLine($"{strategie2.Name()} hat insgesamt {gesamtPunkte2} Punkte erhalten.");
            Console.WriteLine("Somit hat {0} gewonnen.", gesamtPunkte1 < gesamtPunkte2 ? strategie1.Name() : strategie2.Name());
            Console.WriteLine();
        }

        static void MenschGegenMaschine()
        {
            int maschine;
            int runde, schwere;


            Console.Clear();
            Console.WriteLine("Willkommen bei Mensch vs. Maschine.");
            for (int i = 0; i < _strategien.Count; i++)
            {
                Console.WriteLine($"{i} - {_strategien[i].Name()}");
            }

            // Anforderung von Benutzereingaben
            maschine = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die Strategie ihres Gegners aus", 0, _strategien.Count);
            runde = VerwaltungKram.EingabeZahlMinMax("Wie viele Runden sollen verhört werden?", 1, 101);
            schwere = VerwaltungKram.EingabeZahlMinMax("Wie schwer sind die Verstöße? (0=leicht, 1=mittel, 2=schwer)", 0, 3);

            //holt die gewählte Maschinenstrategie aus der Collection.
            BasisStrategie strategieMaschine = _strategien[maschine];
            int reaktionMaschine = BasisStrategie.NochNichtVerhoert;
            int reaktionMensch = BasisStrategie.NochNichtVerhoert;

            //Aufruf der Start-Methode der Maschinenstrategie
            strategieMaschine.Start(runde, schwere);

            Console.Clear();

            // Start der Verhörung
            for (int i = 0; i < runde; i++)
            {
                Console.Clear();
                Console.Write("Sie spielen auf");

                switch (schwere)
                {
                    case 0:
                        Console.WriteLine(" leicht.");
                        break;
                    case 1:
                        Console.WriteLine(" mittel.");
                        break;
                    case 2:
                        Console.WriteLine(" schwer");
                        break;
                }

                Console.WriteLine("Verbleibende Runden: " + (runde - i - 1));

                switch (reaktionMaschine)
                {
                    case 0:
                        Console.WriteLine("In der letzten Runde hat ihr Gegner kooperiert.");
                        break;
                    case 1:
                        Console.WriteLine("In der letzten Runde hat ihr Gegner verraten.");
                        break;
                }

                //beide verhören
                int aktReaktionMensch = VerwaltungKram.EingabeZahlMinMax("Welche Aktion möchten Sie diese Runde wählen? [0] Kooperieren [1] Verraten", 0, 2);
                int aktReaktionMaschine = strategieMaschine.Verhoer(reaktionMensch);



                // Punktberechnung
                switch (schwere)
                {
                    case 0:
                        Leicht(aktReaktionMaschine, aktReaktionMensch);
                        break;
                    case 1:
                        Mittel(aktReaktionMaschine, aktReaktionMensch);
                        break;
                    case 2:
                        Schwer(aktReaktionMaschine, aktReaktionMensch);
                        break;
                }

                // Zuweisung der letzten Reaktion der beiden für den nächsten Durchlauf
                reaktionMaschine = aktReaktionMaschine;
                reaktionMensch = aktReaktionMensch;
            }

            Console.Clear();
            Console.WriteLine("Punkte der Maschine: " + punkte1);
            Console.WriteLine("Punkte des Menschen: " + punkte2);
            if (punkte1 > punkte2)
            {
                Console.WriteLine("Der Mensch hat gewonnen!");
            }
            else if (punkte2 > punkte1)
            {
                Console.WriteLine("Die Maschine hat gewonnen!");
            }
            else
            {
                Console.WriteLine("Es ist ein Unentschieden!");
            }
            Console.Read();

        }
        
    }
}