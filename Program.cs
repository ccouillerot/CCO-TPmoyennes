using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace HNI_TPmoyennes
{
    class Eleve
    {
        public string prenom { get; } = string.Empty;
        public string nom { get; } = string.Empty;
        private List<Note> notes = new List<Note>();
        private Classe EstDansClasse;

        public Eleve(string iprenom, string inom, Classe classe)
        {
            prenom = iprenom;
            nom = inom;
            EstDansClasse = classe;
        }

        public void ajouterNote(Note note)
        {
            if (notes.Count >= 200)
                Console.Write("Le maximum du nombre de note à été atteint : " + note.note + " est ignorée\n");
            else
                notes.Add(note);
        }

        public float moyenneGeneral()
        {
            float moyenne = 0;
        
            for (int i = 0; i < EstDansClasse.matieres.Count; i++)
            {
                moyenne += moyenneMatiere(i);
            }
            return (float)Math.Truncate(moyenne/EstDansClasse.matieres.Count * 100) / 100f;
        }

        public float moyenneMatiere(int matiere)
        {
            float moyenne = 0;
            int count = 0;
            foreach (Note note in notes)
            {
                if (note.matiere == matiere)
                {
                    moyenne += note.note;
                    count++;
                }
            }
            return (float)Math.Truncate(moyenne/count * 100) / 100f;
        }
    }
    class Classe
    {
        public string nomClasse { get; } = string.Empty;
        public List<Eleve> eleves = new List<Eleve>();
        public List<string> matieres = new List<string>();

        public Classe(string name)
        {
            nomClasse = name;
        }

        public void ajouterEleve(string prenom, string nom)
        {
            if (eleves.Count >= 30)
                Console.Write("La Classe est pleine : " + prenom + " " + nom + " est ignoré(e)\n");
            else
                eleves.Add(new Eleve(prenom, nom, this));
        }
        public void ajouterMatiere(string nom)
        {
            if (matieres.Count >= 30)
                Console.Write("Il y a déjà trop de matières enseignées : " + nom + " est ignorée\n");
            else
                matieres.Add(nom);
        }
        public float  moyenneMatiere(int matiere)
        {
            float moyenne = 0;
            foreach (Eleve eleve in eleves)
            {
                moyenne += eleve.moyenneMatiere(matiere);
            }
            return (float)Math.Truncate(moyenne/eleves.Count * 100) / 100f;
        }
        public float moyenneGeneral()
        {
            float moyenne = 0;
            for (int i = 0; i < matieres.Count; i++)
            {
                moyenne += moyenneMatiere(i);
            }
            return (float)Math.Truncate(moyenne/matieres.Count * 100) / 100f;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Création d'une classe
            Classe sixiemeA = new Classe("6eme A");
            // Ajout des élèves à la classe
            sixiemeA.ajouterEleve("Jean", "RAGE");
            sixiemeA.ajouterEleve("Paul", "HAAR");
            sixiemeA.ajouterEleve("Sibylle", "BOQUET");
            sixiemeA.ajouterEleve("Annie", "CROCHE");
            sixiemeA.ajouterEleve("Alain", "PROVISTE");
            sixiemeA.ajouterEleve("Justin", "TYDERNIER");
            sixiemeA.ajouterEleve("Sacha", "TOUILLE");
            sixiemeA.ajouterEleve("Cesar", "TICHO");
            sixiemeA.ajouterEleve("Guy", "DON");
            // Ajout de matières étudiées par la classe
            sixiemeA.ajouterMatiere("Francais");
            sixiemeA.ajouterMatiere("Anglais");
            sixiemeA.ajouterMatiere("Physique/Chimie");
            sixiemeA.ajouterMatiere("Histoire");
            Random random = new Random();
            // Ajout de 5 notes à chaque élève et dans chaque matière
            for (int ieleve = 0; ieleve < sixiemeA.eleves.Count; ieleve++)
            {
                for (int matiere = 0; matiere < sixiemeA.matieres.Count; matiere++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        sixiemeA.eleves[ieleve].ajouterNote(new Note(matiere, (float)((6.5 +
                       random.NextDouble() * 34)) / 2.0f));
                        // Note minimale = 3
                    }
                }
            }

            Eleve eleve = sixiemeA.eleves[6];
            // Afficher la moyenne d'un élève dans une matière
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            eleve.moyenneMatiere(1) + "\n");
            // Afficher la moyenne générale du même élève
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne Generale : " + eleve.moyenneGeneral() + "\n");
            // Afficher la moyenne de la classe dans une matière
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            sixiemeA.moyenneMatiere(1) + "\n");
            // Afficher la moyenne générale de la classe
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne Generale : " + sixiemeA.moyenneGeneral() + "\n");
            Console.Read();
        }
    }
}



