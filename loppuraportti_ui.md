# Loppuraportti: Lautapeli-klooni. (Monopoli)
Hannu Oksman L2912
Antti Tarvainen L4623

### 1. Asennus

* Mitä asioita tehtävä ja huomioitava asennuksessa

   Tietokantayhteyden toimivuus: Ohjelman käyttämä tietokanta on Antin Raspberry Pi:lle asennetulla MySQL-palvelimella. Tällä hetkellä tietokantayhteyden "connection string" on lähdekoodissa, mikä ei ole tietoturvallista. Toisaalta Antti voi vaihtaa mm. salasanan välittömästi, varmuuskopioida tietokannan ja tietokannan luontitiedostot ovat täällä. Näin ollen emme ole pitäneet kiireellisenä tehtävänä muuttaa yhteysasetuksia.
   
   Vaatii Internet-yhteyden.

* Käytetyt=tarvittavat .NET Frameworkin ulkopuoliset kirjastot tai kilkkeet

   Tarvitsimme MySql.Data -NuGet-paketin MySQL-komentoja varten.

* Konfiguroitavat asiat

   Ei tarvitse tehdä mitään. Palvelin käyttää standardiporttia 3306, jonka pitäisi olla käyttäjän omassa verkossa auki.


### 2. Tietoa ohjelmasta (mitä tekee, miksi etc)

* Listaa toteutetut toiminnalliset vaatimukset

  1. Noppien heittäminen painikkeesta (kertoo tuloksen, ei noppa-animaatiota).
  2. Voi ostaa tontteja, kun siirtyy ostettavalle tontille ja se ei ole toisen pelaajan omistuksessa.
  1. Ei ole pakko ostaa tonttia.
  3. Voi rakentaa taloja tonteille, jos on kaikki saman sarjan tontit.
  4. Voi rakentaa hotellin tontille, jos tontilla on neljä taloa.
  5. Voi katsoa rakennus-valikon kautta mitä tontteja omistaa.
  6. Pelaajat voivat heittää noppaa vain kerran vuorossa.
  7. Pelaajan täytyy heittää noppaa ennen kuin lopettaa vuoron.
  8. Pelaaja maksaa toiselle pelaajalle vuokraa, kun menee toisen pelaajan omistamalle tontille.
  9. Pelaaja saa rahaa kun ohittaa lähtöruudun kulkematta vankilan kautta.
  10. Pelaaja joutuu vankilaan, jos joutuu "Go to Jail"-ruutuun.

* Listaa toteuttamatta jääneet toiminnalliset vaatimukset

  1. Toistaiseksi puuttuu uuden pelin luonnin käyttöliittymä. Koodi on olemassa.
  2. Toistaiseksi ei voi ladata peliä tietokannasta käyttöliittymän kautta.
  3. Toistaiseksi uusi animaatio aiheuttaa kaatumisen, jos pelaajan nappula menisi lähtöruutuun ja sen ohi.
  4. Toistaiseksi pelaaja ei voi valita pelinappulaa.

* Listaa toiminnallisuus joka toteuttiin ohi/yli alkuperäisten vaatimusten

  Ei ole.

* Listaa ei-toiminnalliset vaatimukset sekä mahdolliset reunaehdot/rajoitukset
  1. Rajapinnat. Käyttää tietokantapalvelinta, koska halusimme yhdistää tämän tietokanta-opintojaksoon.
  2. Rajapinnat. Vaatii Internet-yhteyden, koska käyttää tietokantapalvelinta.
  3. Käytettävyys. Käyttöliittymä on skaalautuva.
  4. Turvallisuus. Palvelimen kirjautumistiedot ovat lähdekoodissa, pitäisi siirtää pois.
  5. Muokattavuus. Peliin voi tehdä muutoksia tietokantaa muokkaamalla. Esim. tonttien nimet tulevat tietokannasta. Mahdollistaa nopeat muutokset.
  6. Suorituskyky. Ohjelma ei jatkuvasti piirrä ruutua uudestaan, joten se toimii nopeasti.


### 3. Kuvaruutukaappaukset tärkeimmistä käyttöliittymistä + lyhyet käyttöohjeet jollei "ilmiselvää"
![mainwindow](/Images/mainwindow.PNG)

### 4. Ohjelman tarvitsemat /mukana tulevat tiedostot/tietokannat

  1. Ohjelma käyttää Antin Raspilla olevaa tietokantapalvelinta.
  2. Pelinappulat ovat kuvia, mutta ne ovat exe:ssa.


* Kuvaukset tietokannoista ml. tietokanta-kaavio

 [Tietokantaraportti](/Docs/loppuraportti.md)
![er](/Images/monopolifinal.PNG)

* Laita tarvittaessa mukaan tietokannan luontiskriptit ja testidatan lisäysskriptit

   Ne löytyvät Tietokantaraportista.

* Huomioitavaa käytössä

   Tarvitsee Internet-yhteyden.

### 5. Tiedossa olevat ongelmat ja bugit sekä jatkokehitysideat

* Kaatuu tällä hetkellä, kun nappulan pitäisi mennä lähtöruudun kautta.
* Toteuttaa toteuttamatta jääneet toiminnallisuudet.
* Nettimoninpeli eri koneilla.
* Entity Frameworkin käyttöönotto.

### 6. Mitä opittu, mitkä olivat suurimmat haasteet, mitä kannattaisi tutkia/opiskella lisää jne

* Kannattaisi tutkia Entity Frameworkia, opintojaksolla siihen käytettiin kymmenisen minuuttia.
* Kannattaisi tutkia enemmän peruskomponentteja ja niiden käyttöä.
* Kannattaisi tutkia enemmän eventhandlereita ja niiden käyttöä.
* Kannattaisi tutkia enemmän delegaatteja ja niiden käyttöä.

### 7. Tekijät, vastuiden ja työmäärän jakautuminen


### 8. Tekijöiden ehdotus arvosanaksi, ja perustelut sille
