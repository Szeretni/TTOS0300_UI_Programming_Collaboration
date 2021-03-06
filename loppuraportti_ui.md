# Loppuraportti: Lautapeli-klooni. (Monopoli)
Hannu Oksman L2912 ja Antti Tarvainen L4623

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
  11. Pitämällä kursoria tonttien nimen tai hinnan päällä näkee kuka omistaa tontin.
  12. Toisen pelin voi ladata valikosta.
  13. Sattumakorteille yksinkertainen toiminnallisuus.
  14. Voi luoda uuden pelin.
  15. Voi ladata vanhan pelin.
  16. Voi sulkea ohjelman.

* Listaa toteuttamatta jääneet toiminnalliset vaatimukset

  1. Toistaiseksi puuttuu pelin poistaminen.
  4. Toistaiseksi pelaaja ei voi valita pelinappulaa.
  5. Toistaiseksi puuttuu uuden pelaajan luonti.
  6. Toistaiseksi puuttuu pelaajan poistaminen.
  7. Toistaiseksi puuttuu pelin päättymislogiikka.

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

* Toteuttaa toteuttamatta jääneet toiminnallisuudet.
* Nettimoninpeli eri koneilla.
* Entity Frameworkin käyttöönotto.
* Koodia pitäisi aliohjelmoida enemmän.
* Koodia pitäisi siirtää main:sta enemmän muualle.
* Taloja pitäisi pystyä rakentamaan vain tasaisesti saman värin tonteille.
* Go to Jail animaatio toimii, mutta ei niin kuin pitäisi.
* Lähtöruudun ohikulku heittää yhdellä ruudulla.
* Jos esim. tontin ostamisen valikko on esillä ja hover peliruutuja, niin ostovalikko katoaa.
* Harvoin uuden pelin lataamisen jälkeen tulee RecreateCanvas:n Out of Index -virhe.

### 6. Mitä opittu, mitkä olivat suurimmat haasteet, mitä kannattaisi tutkia/opiskella lisää jne

* Opittu käyttöliittymän piirtämään elementtejä koodissa.
* Opittu monikerrosarkkitehtuurin käyttöä ja havaittu siihen liittyviä etuja, kuten jäsentyneempi koodi. Osittain tämä on keksen, main:ssa on vielä paljon mitä voisi siirtää business layer:lle.
* Opittu käyttämään tietokantaa ohjelmassa.
* Opittu Data Binding.
* Opittu kuinka monimutkainen ja monipuolinen suhteellisen yksinkertainenkin sovellus voi olla.
* Opittu poimimaan useamman kerroksen alla olevaa informaatiota käyttämällä sender.
* Opittu jatkuvasti havaitsemaan miten asiat voisi tehdä paremmin.
* Haasteet liittyivät oikeastaan kaikki käyttöliittymän piirtämiseen.
* Haaste oli myös miten työstää yhdessä samanaikaisesti ja välttää merge conflict versionhallinnassa.
* Kannattaisi tutkia Entity Frameworkia, opintojaksolla siihen käytettiin kymmenisen minuuttia.
* Kannattaisi tutkia enemmän peruskomponentteja ja niiden käyttöä.
* Kannattaisi tutkia enemmän eventhandlereita ja niiden käyttöä.
* Kannattaisi tutkia enemmän delegaatteja ja niiden käyttöä.
* Kannattaisi tutkia git-versionhallintaa ja kokeilla mm. branch:n käyttöä.

### 7. Tekijät, vastuiden ja työmäärän jakautuminen

* Hannu Oksman L2912
* Antti Tarvainen L4623
* Työmäärä jakautui tasaisesti ja molemmat teki kaikkea. Antti jonkin verran enemmän käyttöliittymän piirtämistä ja Hanne jonkin verran enemmän tietokantaa ja muita tasoja.

### 8. Tekijöiden ehdotus arvosanaksi, ja perustelut sille

5. Työ on monipuolinen, kattava, käyttää ulkopuolista dataa sekä käsittelee ja näyttää sitä eri tavoin ohjelman sisällä. Työssä on käytetty monikerrosarkkitehtuuria ja useita luokkia selkeyttämään ja helpottamaan tiedon käsittelyä. Koodia on myös kommentoitu. Käyttöliittymä on selkeä ja skaalautuva. Tunnemme hyvin työmme ja pystymme keskustelemaan siitä, joka kertoo paneutumisesta ja suuresta työmäärästä. Lisäksi työssä on käytetty mm. settings.settings, App.xaml ja App.xaml.cs -tiedostoja hyödyksi. Olemme käyttäneet GitHub-palvelua versionhallinnassa, joka osoittautui erittäin hyväksi ratkaisuksi. Tekijät saavat helposti git bash -ohjelman avulla hallittua paikallisia versioita ja työ on valmiiksi työnantajien nähtävillä. Tämä projekti on opettanut paljon ja antaa näkemystä miten seuraavan lukuvuoden projekteja voi tehdä tehokkaammin ja paremmin. Kiitettävää on myös se, että työssä on käytetty muilta opintojaksoilta opittua ja käytetty omaa tietokantapalvelinta. Hyvää on myös puutteiden ja jatkokehitysideoiden tunnistaminen ja tunnustaminen.
