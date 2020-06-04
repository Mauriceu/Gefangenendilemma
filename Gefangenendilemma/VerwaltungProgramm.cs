﻿using System;
using System.Collections.Generic;
using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{
    class VerwaltungProgramm
    {
        /// <summary>
        /// Diese Liste enthält alle Gefangene/Strategien
        /// </summary>
        private static List<BasisStrategie> _strategien;

        private static int punkte1, punkte2, gesamtPunkte1, gesamtPunkte2;

        static void Main(string[] args)
        {
            //bekannt machen der ganzen strategien
            _strategien = new List<BasisStrategie>();
            _strategien.Add(new GrollStrategie());
            _strategien.Add(new VerrateImmerStrategie());
            _strategien.Add(new Strategie1());
            _strategien.Add(new Strategie2());
            _strategien.Add(new Strategie3());

            string eingabe = null;
            do
            {
                // Begrüßung
                Console.WriteLine("Willkommen zum Gefangenendilemma");
                Console.WriteLine("0 - Verhör zwischen 2 Gefangene");
                Console.WriteLine("1 - Automatisches Verhör zwischen 2 Strategien.");
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
                    case "X":
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
            int runde, schwere;

            Console.WriteLine("Willkommen zum Verhör zwischen 2 Strategien");
            for (int i = 0; i < _strategien.Count; i++)
            {
                Console.WriteLine($"{i} - {_strategien[i].Name()}");
            }

            Console.WriteLine("Wählen Sie ihre 2 Gefangene:");
            st1 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 1. Strategie", 0, _strategien.Count);
            st2 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 2. Strategie", 0, _strategien.Count);
            runde = VerwaltungKram.EingabeZahlMinMax("Wie viele Runden sollen diese verhört werden?", 1, 101);
            schwere = VerwaltungKram.EingabeZahlMinMax("Wie schwer sind die Verstöße? (2=schwer)", 0, 3);

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
            
            Console.WriteLine($"Verhör zwischen {strategie1.Name()} und {strategie2.Name()} für {runde} Runden.");


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
            Console.WriteLine();
            Console.WriteLine($"{strategie1.Name()} hat {punkte1} Punkte erhalten.");
            Console.WriteLine($"{strategie2.Name()} hat {punkte2} Punkte erhalten.");
            Console.WriteLine("Somit hat {0} gewonnen.", punkte1 < punkte2 ? strategie1.Name() : strategie2.Name());
           
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

        static void AutomatischerVerhoerer()
        {
            int st1, st2;
            int schwere = 0;

            Console.WriteLine("Willkommen zum Verhör zwischen 2 Strategien");
            for (int i = 0; i < _strategien.Count; i++)
            {
                Console.WriteLine($"{i} - {_strategien[i].Name()}");
            }

            Console.WriteLine("Wählen Sie ihre 2 Gefangene:");
            st1 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 1. Strategie", 0, _strategien.Count);
            st2 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 2. Strategie", 0, _strategien.Count);

            for (int a = 0; a < 3; a++)
            {
                int runde = 5;
                
                    for (int b = 0; b < 3; b++)
                    {
                        punkte1 = 0;
                        punkte2 = 0;
                        Verhoer(st1, st2, runde, schwere);

                        switch (runde)
                        {
                            case 5:
                                runde = 25;
                                gesamtPunkte1 = punkte1 * 20;
                                gesamtPunkte2 = punkte2 * 20;
                                break;
                            case 25:
                                runde = 100;
                                gesamtPunkte1 = punkte1 * 4;
                                gesamtPunkte2 = punkte2 * 4;
                                break;
                            case 100:
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
            Console.WriteLine($"{strategie1.Name()} hat {gesamtPunkte1} Punkte erhalten.");
            Console.WriteLine($"{strategie2.Name()} hat {gesamtPunkte2} Punkte erhalten.");
        }
        
    }
}