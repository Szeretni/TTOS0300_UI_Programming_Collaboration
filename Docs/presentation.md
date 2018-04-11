
# TODO. POISTA LOPULLISESTA VERSIOSTA
* DONE SCHEMA10, ei vielä sql_csharp_ui dbssä: ON DELETE ON UPDATE
* queries
* mysql-palvelimen luonnista ja käytöstä raportointi
* DONE SCHEMA10, ei vielä sql_csharp_ui dbssä: view
* Lisää jokaiseen tauluun tietoa ja tee tarvittavat kyselyt (mieluiten kaksi, joista toinen on monen taulun kysely ja toinen jokin summakysely).
* if jos type 0 ei voi omistaa
* DONE proseduuri tehty, lisätty queries.sql
# END OF TODO

# Peliklooni

## Hannu Oksman, Ville Paananen, Antti Tarvainen

### Vaatimusmäärittely

[Vaatimusmäärittely-dokumentti](/Vaatimusmäärittely)

### Käyttötapauskaavio

![use case](../Images/ttos0300_use_case.png)

### Käsite-ehdokkaista tehty käsitemalli

![uml](../Images/monopoliuml.png)

#### Huomiot
* Mallintaa tarpeettoman tarkasti fyysistä lautapeliä, jossa Raha, Tontit ja Nappula ovat sidottuja Pelaajaan, joka tuo ne Pelikertaan.
* Ei salli rinnakkaisia pelejä, vaan pitää pelata sarjassa.

### Draw.io:sta Workbenchiin

![asgfölh](../Images/monopoliasgfölh.PNG)

#### Huomiot
* Moni-moneen-yhteydet purettu.
* Edelleen pelaaja kuljettaa muita käsitteitä.

### Vain yksi moni-moneen-välitaulu

![asgfölh](../Images/monopoliumlmysql.PNG)

#### Huomiot
* Pelaaja ei enää tuo rahoja ym. peliin.
* Käsitteisiin lisätty Owner. Tästä seuraa, että vaikka rahat voivat kuulua moniin peleihin, niin ne eivät voi tehdä sitä samanaikaisesti.

### Final

![ER-kaavio](../Images/monopolifinal.PNG)

#### Huomiot
* Välitauluissa on pelaaja ja peli, jotka yhdessä kunkin käsitteen kanssa muodostavat pääavaimen. Esim. rahat voivat olla nyt monessa pelissä samanaikaisesti.
* ![same cash different game](../Images/same_cash_different_game.PNG)
* ![PK](../Images/phc_pk.PNG)
* Uniikki indeksi pelille ja ei-pelaajalle. Estää esim. saman rahan olemassaolon usealla pelaajalla samassa pelissä.
* ![UQ](../Images/phc_i.PNG)
* ![same cash same game](../Images/same_cash_same_game_error.PNG)

### DDL ja DML

[schema.sql](../SQLmaterials/schema.sql)

### Datan lisäys

[insert_into.sql](../SQLmaterials/insert_into.sql)

### Rivit lisäyksen jälkeen

![rows](../Images/nbr_of_table_rows.PNG)

### Vyörytys

![same cash same game](../Images/on_update_toimii.png)

### Näkymä

![same cash same game](../Images/create_view.PNG)


### Kysely

![number of games per player](../Images/games_per_player.PNG)

### Proseduuri

![unbyuable_cells](../Images/delimiter.PNG)
