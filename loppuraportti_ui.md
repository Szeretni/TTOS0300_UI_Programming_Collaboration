# Loppuraportti: Lautapeli-klooni. (Monopoli)
Hannu Oksman L2912
Antti Tarvainen L4623

## 1. Asennus

* Mitä asioita tehtävä ja huomioitava asennuksessa

   Tietokantayhteyden toimivuus: Ohjelman käyttämä tietokanta on Antin Raspberry Pi:lle asennetulla MySQL-palvelimella. Tällä hetkellä tietokantayhteyden "connection string" on lähdekoodissa, mikä ei ole tietoturvallista. Toisaalta Antti voi vaihtaa mm. salasanan välittömästi, varmuuskopioida tietokannan ja tietokannan luontitiedostot ovat täällä. Näin ollen emme ole pitäneet kiireellisenä tehtävänä muuttaa yhteysasetuksia.

* Käytetyt=tarvittavat .NET Frameworkin ulkopuoliset kirjastot tai kilkkeet

   Tarvitsimme MySql.Data -NuGet-paketin MySQL-komentoja varten.

* Konfiguroitavat asiat

   Ei tarvitse tehdä mitään. Palvelin käyttää standardiporttia 3306, jonka pitäisi olla käyttäjän omassa verkossa auki.


## 2. Tietoa ohjelmasta (mitä tekee, miksi etc)

* Listaa toteutetut toiminnalliset vaatimukset

  1. Nopan heitto

* Listaa toteuttamatta jääneet toiminnalliset vaatimukset

* Listaa toiminnallisuus joka toteuttiin ohi/yli alkuperäisten vaatimusten

* Listaa ei-toiminnalliset vaatimukset sekä mahdolliset reunaehdot/rajoitukset



3. Kuvaruutukaappaukset tärkeimmistä käyttöliittymistä + lyhyet käyttöohjeet jollei "ilmiselvää"


4. Ohjelman tarvitsemat /mukana tulevat tiedostot/tietokannat


kuvaukset tietokannoista ml. tietokanta-kaavio
laita tarvittaessa mukaan tietokannan luontiskriptit ja testidatan lisäysskriptit
Huomioitavaa käytössä



5. Tiedossa olevat ongelmat ja bugit sekä jatkokehitysideat


6. Mitä opittu, mitkä olivat suurimmat haasteet, mitä kannattaisi tutkia/opiskella lisää jne


7. Tekijät, vastuiden ja työmäärän jakautuminen


8. Tekijöiden ehdotus arvosanaksi, ja perustelut sille
