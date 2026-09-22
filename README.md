# AI-DrivenSoftware-271492
KursRepository zu Kurs AI-Driven Software Development – Konzeption bis zum Test der ppedv AG


## Geschichte der KI

- KI gibt es bereits seit den 50ern
- Unterschied zwischen regelbasierten System und lernenden System
- Bei lernenden System Unterscheidung zwischen schwacher und starkter AI (ANI, AGI)
- Bsp. Regelbasiertes System [ELIZA](https://de.wikipedia.org/wiki/ELIZA) (1964)
- 1997 IBM Deep Blue schlägt Schachweltmeister Kasparov
- 2015 Mit CUDA (2007) von Nvidia werden neuronale Netze trainiert (Matrizenberechnungen) 
- 2016 TayTweets von Microsoft war nur ein Tag online
- 2017 erschien ein Paper über GPT von Google: Besondere Architektur eines Neuronalen Netzes mit Transformer-Architektur
- Ende 2022 erscheint ChatGPT als kommerzielles Produkt (frei) mit GPT3.5 Turbo
- Alignment mittels RLHF: Menschen sagen der KI was gut oder schlecht
- Weiterentwicklungen wie Multimodalität, Reasoning, Destillation


## Prompt Engineering

- Mehr Kontext bedeutet bessere Ergebnisse
- Nicht deterministisch: Ergebnisse sind nicht zwangsläufig reproduzierbar
- Der KI eine Rolle (Persona) zuweisen
- Ziel beschreiben, welches ich erreichen will
- Format der Ausgabe spezifizieren
- Beispiele der KI geben, wie man etwas haben möchte


## Vorbehalte/Probleme beim Prompt Engineering

- Kontextfenster beachten (Lost-in-the-Middle, Fabulierung, Halluzinationen)
- Mit einem Handoff-Prompts in eine neue Session gehen für frischen Kontext und bessere Ergebnisse 
- KI soll Quellen recherchieren und zugeben, wenn sie sich unsicher ist
- Trick: Die KI fragen, wie ein guter Prompt aussieht und als Assistent zur Prompterstellung benutzen


## Security & Safety

- Prompt-Injection: Im einfachsten Fall der KI sagen, dass sie alles vorher besprochene vergessen soll und ab jetzt XY machen soll (sollte eigentlich vom Anbieter abgesichert sein)
- Jailbreaking, [DAN (Do Anything Now)](https://github.com/0xk1h0/ChatGPT_DAN), Red-Teaming und "Wettrüsten"
- Generative KI muss gegen Missbrauch abgesichert sein (Rezepte für Drogen, Waffen bauen, Hatespeech...) R
- EU AI Act: 
	- Risikobasierter Ansatz um AI zu klassifizieren
	- Transparenz und Nachvollziebarkeit
	- Verantwortung und Haftung
	- Ethische Grundsätze
