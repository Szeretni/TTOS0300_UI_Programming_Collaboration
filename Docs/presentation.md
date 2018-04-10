# TODO
# POISTA LOPULLISESTA VERSIOSTA
* ON DELETE ON UPDATE
* queries
* mysql-palvelimen luonnista ja käytöstä raportointi
* view
# END OF TODO

# Peliklooni

## Hannu Oksman, Ville Paananen, Antti Tarvainen

### Vaatimusmäärittely

[Vaatimusmäärittely](/Vaatimusmäärittely)

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
* ![PK](../Images/phc_pk.PNG)
* Uniikki indeksi pelille ja ei-pelaajalle. Estää esim. saman rahan olemassaolon usealla pelaajalla samassa pelissä.
* ![UQ](../Images/phc_i.PNG)

### DDL ja DML

[schema](../SQLmaterials/schema.sql)

### Datan lisäys

[insert into](../SQLmaterials/insert_into.sql)
