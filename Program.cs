using System;
using System.Threading.Tasks;

namespace Gradini
{
	internal class Program
	{
		//chiedere lunghezza scalinata e generare+caricare array
		//l'array scalinata ha indice = posizione e contenuto = azione
		//azione = movimento  -(N/2)<X<(N/2)
		//genera pedina in pos 0
		//pedina fa i movimenti
		//se la posizione d'arrivo è maggiore di N-1, si muove indietro del numero di gradini eccedenti, se è minore di 0 si muove in  avanti del numero di gradini mancanti, rispetto alla posizione in cui si trova.
		//se il numero indicato è 0 scivola indietro di una posizione (nel caso della prima posizione sale un gradino).
		//il gioco finisce quando la pedina raggiunge l'ultima posizione.
		//individuare e documentare un possibile controllo dei casi di loop. In caso di loop il gioco si dovrà fermare e indicare i passi effettuati prima di entrare in loop.
		static void Main()
		{
			Start();
			do
			{
				Console.Clear();
				//
				//LUNGHEZZA SCALINATA
				int lun;
				Console.Write("Inserire lunghezza della scalinata di questa partita(esclusa lo start): ");
				//Console.Write(Console.CursorLeft); //= 72
				//
				//CONTROLLI E GRAFICA
				while (!int.TryParse(Console.ReadLine(), out lun) || !(lun >= 2 && lun <= 30))
				{//bad input
					Task.Delay(300).Wait();
					Console.SetCursorPosition(72, 1);
					Console.Write("numero intero tra 2 e 30");
					Console.SetCursorPosition(72, 0);
					Console.Write(new string(' ', Console.WindowWidth));
					Console.SetCursorPosition(72, 0);
				}
				Console.SetCursorPosition(72, 1);
				Console.Write(new string(' ', Console.WindowWidth - 72)); // ( 0, 2)
				Task.Delay(500).Wait();

				//
				//GENERARE E CARICARE ARRAY SCALINATA
				Random rnd = new Random();
				int[] sca = new int[lun + 1]; //crea la fine al numero scelto.
				for (int i = 0; i < sca.Length; i++)
				{
					sca[i] = rnd.Next(-lun / 2, lun / 2 + 1); // se 10 crea da -5 a 5, se 11 crea da -5 a 5
					if (sca[i] == 0) sca[i] = -1; //se l'azione è 0 fa -1
				}

				//
				//GENERA PEDINA
				int ped = 0; //pedina è in posizione 0



				//
				//INIZIO GAME
				Console.WriteLine("Questa è la scalinata con le sue azioni");
				//non so perchè azzera entrambe    int[] scc = sca; //array per colorare la scalinata, il calcolo delle posizioni varia a seconda dei numeri random, che poi possono essere azzerati
				int[] scc = new int[sca.Length];
				for(int i = 0; i < sca.Length; i++) scc[i] = sca[i];
				bool err = false; //segna se si ha vinto o se c'è stato un errore
				Console.WriteLine($"\n\nLa pedina parte sullo scalino {ped}; su questo scalino c'è scritto {sca[ped]}");
				int y = Console.CursorTop;
				do
				{//
				 //STAMPA SCALINATA E PEDINA
					Console.SetCursorPosition(0, 3);
					Pedina(scc, ped);
					y++;
					Console.SetCursorPosition(0, y);

					//avanzamento
					Console.ReadKey();

					(ped, sca[ped]) = (ped + sca[ped], 0); //è una tupla, scambia i valori senza variabili temporali
														   //serve a fare il movimento e ad azzerare lo scalino lasciato

					//
					//CONTROLLI MOVIMENTO PEDINA
					//
					//se è oltre il massimo
					//if (ped > lun) ped = lun - (ped - lun); vecchio calcolo
					if (ped > lun) ped = 2 * lun - ped;

					//se è prima di 0
					if (ped < 0) ped = -ped;

					//se l'azione è 0 fa -1
					//fatto in caricamento array

					//CONTROLLO CICLO INFINITO
					if (sca[ped] == 0)
					{//FINE CODICE PER ERRORE
						err = true;
						Console.WriteLine($"La pedina è ora sullo scalino {ped}; su questo scalino ci sei già passato");
						break;
					}

					//VITTORIA
					if (ped == lun)
					{
						Console.WriteLine($"La pedina è ora sullo scalino {ped}; sei sull'ultimo scalino");
						break;
					}
					else//SEGNA IN CONSOLE MOVIMENTO E AZIONE CHE SARà ESEGUITA AL CICLO DOPO
						Console.WriteLine($"La pedina è ora sullo scalino {ped}; su questo scalino c'è scritto {sca[ped]}");

				} while (true);

				//
				//FINE GAME e RIPETI GAME
				if (err == true) Console.WriteLine("Questo game non si può finire");
				else Console.WriteLine("HAI VINTO");
				Console.Write("vuoi ricominciare? (scrivi (y)es per ricominciare, qualsiasi altra cosa per no ");
				if (Console.ReadKey().KeyChar != 'y') break;
			} while (true);
			Console.Clear();
		}
		//stampa pre programma
		static void Start()
		{
			Console.BackgroundColor = ConsoleColor.Red;//setta il colore di sfondo
			Console.Clear(); //colora tutto
			Console.ForegroundColor = ConsoleColor.Black;//setta il colore del carattere
			Console.SetCursorPosition(10, 1);//setta la posizione del cursore;
			Console.Write("GIOCO DELLA SCALINATA\n\n press any key to continue");
			Console.ReadKey();//press any key to continue
			Console.BackgroundColor = ConsoleColor.White;
			Console.Clear();
			//
			Console.BackgroundColor = ConsoleColor.Red;
			Console.Write("prima di cominciare mettere schermo intero e poi premere un tasto per continuare");
			Console.ReadKey();
			Console.BackgroundColor = ConsoleColor.White;
			Console.Clear();
		}
		//funzione segna dove sta la pedina
		static void Pedina(int[] scc, int ped)
		{
			for (int i = 0; i < scc.Length; i++)
			{
				if (i == ped)
                {
					Console.BackgroundColor= ConsoleColor.Red;
					Console.Write(scc[i]);
					Console.BackgroundColor = ConsoleColor.White;
					Console.Write(" | ");
				}
				else Console.Write(scc[i] + " | ");
			}
		}
	}
}