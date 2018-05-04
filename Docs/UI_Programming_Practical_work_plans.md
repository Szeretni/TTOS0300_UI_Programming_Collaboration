🔖 Tietoa tekijöistä

Antti Tarvainen L4623 ja Hannu Oksman K1234.

🔖 Sovelluksen yleiskuvaus

Klooni lautapelistä virtuaalisena, pelitila tallentuu reaaliaikaisesti SQL-tietokantaan.

🔖 Kenelle sovellus on suunnattu, kohdeyleisö

Opettajalle ja työnantajille.

🔖 Käyttöympäristö ja käytetyt teknologiat

* Windows
* Tietokannat-kurssin harjoitustyöksi suunniteltu MySQL-tietokanta MySQL-palvelimella
* C#
* WPF
* XAML
* MySQL
* Tarkemmista käyttöliittymäkomponenteista jne. ei ole selvyyttä tässä vaiheessa. Niitä täytyy tutkia työn edetessä.

🔖 Lista toteutettavista toiminnoista

![use case](/Images/ttos0300_use_case.png)

* Kortin veto pelaajan tullessa sattuma tai yhteismaaruutuun
* Lopetus-nappi
* Uuden pelin luonti
* Pelin lataus
* Nopan heitto
* Katujen yms. ostaminen
* Rakennusten rakentaminen tonteille (Taloja 4, hotelleita 1 jos taloja on jo 4 tontilla)
* Rahan lainaaminen pankilta
* Rahan maksaminen/saaminen
* Lainan maksu
* Omistettujen tonttien katselu.

🔖 Sovelluksen keskeiset käsitteet ja niiden väliset suhteet.

![er](/Images/monopolifinal.PNG)

* Tietokannan keskeinen tavoite on mahdollistaa useat rinnakkaiset pelit, joka toteutuu. Esimerkiksi tiettyä peliruutua ei ole sidottu tiettyyn peliin, niitä käytetään joka pelissä. Sitten taas tietyn pelaajan tietyn ruudun omistaminen tietyssä pelissä hoituu Player_has_Cell avulla.

🔖 Työnjako

Dokumentaatio: Antti ja Hannu.

Grafiikka: Antti ja Hannu.

Tietokantasuunnittelu ja toteutus: Antti ja Hannu.

Ohjelmointi: Antti ja Hannu.
