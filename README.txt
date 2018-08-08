Test di sviluppo di un Solitario per WhatWapp Entertainment.

1) Gameplay funzionante in modalit� portrait
- Attualmente settato per 10/16 come la maggior parte dei dispositivi mobile in commercio
- Il gioco si avvia atunomamente, ma � possibile riavviare la partita dal men� di pausa.
- Il gioco � vinto quanto tutte i mazzi che ospitano i "semi" separatamente, presentano 13 carte con il K in cima
- E' possibile muovere una sola carta per volta (come nel solitario tradizionale)
- Le colonne possono ospitare carte di qualunque seme, purch� in ordine discendente
- I mazzi dei "semi" possono ospitare solo carte dello stesso seme, purch� in ordine ascendente

2) Animazioni delle carte
- Sia la rotazione che lo spostamento avvengono in 0.2 secondi.

3) Il punteggio � calcolato come segue:
- Posizionare una carta nel mazzo dei semi +10 punti
- Posizionare una carta in una colonna partendo dal mazzo delle carte pescate +5 punti
- Rivelare una carta coperta in una colonna +10 punti

4) Annullare le mosse
- Si possono annullare tutte le mosse fino a resettare la partita
- Le carte pescate non possono essere nuovamente rimesse nel mazzo (non mi sembrava coerente poter vedere la carta e metterla a posto...)
	- Comunque sia la parte di codice che fa questa operazione � disponibile, ma commentata.

5) Menu di pausa
- Si accede dal pulsante "ingranaggio". Si chiude con la "X" al suo interno. Almeno cos� mi � parso di capire dal video che ho preso per reference.
- Durante la "pausa" le interazioni con le carte e i bottoni al di fuori della pausa non sono disponibili

6) Modalit� a 3 carte
- Il gioco parte con la modalit� a carta singola, ma � possibile cambiare in qualunque momento dal men� di pausa

7) Aiuti
- Una carta che � possibile muovere lampeggia di grigio se si clicca su "aiuti".
- Se non sono possibili mosse valide, la partita termina automaticamente (con "sconfitta").
- Pescare dal mazzo non � mai suggerito (mi sembrava molto scontato...) e finch� questi ha carte la partita non pu� essere persa.

Nota:
- L'uso di alcuni asset grafici non mi era particolarmente chiaro; ho quindi creato il necessario con quanto mi � stato fornito.
- La partita usa un solo mazzo da 52 carte e i jolly non sono inclusi (non saprei come usarli...).