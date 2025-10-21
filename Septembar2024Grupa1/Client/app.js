export default class App {
  constructor(container) {
    this.container = container;
  }

  async Init() {
    const leftContainer = document.createElement("div");
    leftContainer.classList.add("left-container");
    this.container.appendChild(leftContainer);

    const rightContainer = document.createElement("div");
    rightContainer.classList.add("right-container");
    this.container.appendChild(rightContainer);

    await this.#CreateBirajStanContainer(leftContainer);
  }

  async #CreateBirajStanContainer(leftContainer) {
    const birajStanContainer = document.createElement("div");
    birajStanContainer.classList.add("biraj-stan-container");
    leftContainer.appendChild(birajStanContainer);

    const birajStanLabelSelectContainer = document.createElement("div");
    birajStanLabelSelectContainer.classList.add(
      "biraj-stan-label-select-container"
    );
    birajStanContainer.appendChild(birajStanLabelSelectContainer);

    const birajStanLabelContainer = document.createElement("div");
    birajStanLabelSelectContainer.appendChild(birajStanLabelContainer);

    const birajStanLabel = document.createElement("label");
    birajStanLabel.textContent = "Biraj stan:";
    birajStanLabelContainer.appendChild(birajStanLabel);

    const birajStanSelectContainer = document.createElement("div");
    birajStanLabelSelectContainer.appendChild(birajStanSelectContainer);

    const birajStanSelect = document.createElement("select");
    birajStanSelect.classList.add("biraj-stan-select");
    birajStanSelectContainer.appendChild(birajStanSelect);

    const birajStanSelectEmptyOption = document.createElement("option");
    birajStanSelectEmptyOption.textContent = "";
    birajStanSelect.appendChild(birajStanSelectEmptyOption);

    let response = await fetch("https://localhost:7160/api/stanovi/ids");
    const stanoviIds = await response.json();
    stanoviIds.forEach((stanId) => {
      const birajStanSelectOption = document.createElement("option");
      birajStanSelectOption.textContent = stanId;
      birajStanSelect.appendChild(birajStanSelectOption);
    });

    const prikazInformacijaButtonContainer = document.createElement("div");
    prikazInformacijaButtonContainer.classList.add("prikaz-info-btn-container");
    birajStanContainer.appendChild(prikazInformacijaButtonContainer);

    const prikazInformacijaButton = document.createElement("button");
    prikazInformacijaButton.classList.add("prikaz-info-btn");
    prikazInformacijaButton.textContent = "Prikaz informacija";
    prikazInformacijaButtonContainer.appendChild(prikazInformacijaButton);

    const stanInfoContainer = document.createElement("div");
    stanInfoContainer.classList.add("stan-info-container");
    leftContainer.appendChild(stanInfoContainer);

    const div = document.createElement("div");
    stanInfoContainer.appendChild(div);

    prikazInformacijaButton.addEventListener("click", async () => {
      const odabraniStanId =
        birajStanSelect.options[birajStanSelect.selectedIndex].textContent;

      stanInfoContainer.innerHTML = "";
      const rightContainer = document.querySelector(".right-container");
      rightContainer.innerHTML = "";
      if (odabraniStanId != "") {
        const response = await fetch(
          `https://localhost:7160/api/stanovi/${odabraniStanId}`
        );
        const stan = await response.json();
        this.#CreateStanInfoContainer(stanInfoContainer, stan);
        this.#CreateRacuniInfoCards(rightContainer, stan);
      }
    });
  }

  #CreateStanInfoContainer(stanInfoContainer, stan) {
    const infoContainer = document.createElement("div");
    infoContainer.classList.add("info-container");
    stanInfoContainer.appendChild(infoContainer);

    const labelsContainer = document.createElement("div");
    labelsContainer.classList.add("labels-container");
    infoContainer.appendChild(labelsContainer);

    const valuesContainer = document.createElement("div");
    valuesContainer.classList.add("values-container");
    infoContainer.appendChild(valuesContainer);

    const brojStanaLabel = document.createElement("label");
    brojStanaLabel.textContent = "Broj stana:";
    labelsContainer.appendChild(brojStanaLabel);

    const brojStanaValue = document.createElement("label");
    brojStanaValue.textContent = stan.id;
    valuesContainer.appendChild(brojStanaValue);

    const imeVlasnikaLabel = document.createElement("label");
    imeVlasnikaLabel.textContent = "Ime vlasnika:";
    labelsContainer.appendChild(imeVlasnikaLabel);

    const imeVlasnikaValue = document.createElement("label");
    imeVlasnikaValue.textContent = stan.imeVlasnika;
    valuesContainer.appendChild(imeVlasnikaValue);

    const povrsinaStanaLabel = document.createElement("label");
    povrsinaStanaLabel.textContent = "Povrsina(m2):";
    labelsContainer.appendChild(povrsinaStanaLabel);

    const povrsinaStanaValue = document.createElement("label");
    povrsinaStanaValue.textContent = stan.povrsina;
    valuesContainer.appendChild(povrsinaStanaValue);

    const brojClanovaLabel = document.createElement("label");
    brojClanovaLabel.textContent = "Broj clanova:";
    labelsContainer.appendChild(brojClanovaLabel);

    const brojClanovaValue = document.createElement("label");
    brojClanovaValue.textContent = stan.brojClanova;
    valuesContainer.appendChild(brojClanovaValue);

    const ukupnoZaduzenjeButtonContainer = document.createElement("div");
    ukupnoZaduzenjeButtonContainer.classList.add(
      "ukupno-zaduzenje-btn-container"
    );
    stanInfoContainer.appendChild(ukupnoZaduzenjeButtonContainer);

    const ukupnoZaduzenjeButton = document.createElement("button");
    ukupnoZaduzenjeButton.textContent = "Izracunaj ukupno zaduzenje";
    ukupnoZaduzenjeButtonContainer.appendChild(ukupnoZaduzenjeButton);
    ukupnoZaduzenjeButton.addEventListener("click", async () => {
      const response = await fetch(
        `https://localhost:7160/api/stanovi/${stan.id}/troskovi`
      );
      const ukupniNeizmireniTroskovi = await response.json();
      ukupnoZaduzenjeButton.textContent = ukupniNeizmireniTroskovi;
      ukupnoZaduzenjeButton.disabled = true;
    });
  }

  #CreateRacuniInfoCards(rightContainer, stan) {
    stan.racuni.forEach((racun) => {
      const racunCard = document.createElement("div");
      racunCard.classList.add("racun-card");
      if (racun.placen == "Da") {
        racunCard.classList.add("racun-placen");
      } else {
        racunCard.classList.add("racun-nije-placen");
      }
      rightContainer.appendChild(racunCard);

      const labelsContainer = document.createElement("div");
      labelsContainer.classList.add("labels-container");
      racunCard.appendChild(labelsContainer);

      const valuesContainer = document.createElement("div");
      valuesContainer.classList.add("values-container");
      racunCard.appendChild(valuesContainer);

      const mesecIzdavanjaLabel = document.createElement("label");
      mesecIzdavanjaLabel.textContent = "Mesec:";
      labelsContainer.appendChild(mesecIzdavanjaLabel);

      const mesecIzdavanjaValue = document.createElement("label");
      mesecIzdavanjaValue.textContent = racun.mesecIzdavanja;
      valuesContainer.appendChild(mesecIzdavanjaValue);

      const vodaLabel = document.createElement("label");
      vodaLabel.textContent = "Voda:";
      labelsContainer.appendChild(vodaLabel);

      const vodaValue = document.createElement("label");
      vodaValue.textContent = racun.cenaVode;
      valuesContainer.appendChild(vodaValue);

      const strujaLabel = document.createElement("label");
      strujaLabel.textContent = "Struja:";
      labelsContainer.appendChild(strujaLabel);

      const strujaValue = document.createElement("label");
      strujaValue.textContent = racun.cenaStruje;
      valuesContainer.appendChild(strujaValue);

      const komunalijeLabel = document.createElement("label");
      komunalijeLabel.textContent = "Komunalne usluge:";
      labelsContainer.appendChild(komunalijeLabel);

      const komunalijeValue = document.createElement("label");
      komunalijeValue.textContent = racun.cenaKomunalija;
      valuesContainer.appendChild(komunalijeValue);

      const placenLabel = document.createElement("label");
      placenLabel.textContent = "Placen:";
      labelsContainer.appendChild(placenLabel);

      const placenValue = document.createElement("label");
      placenValue.textContent = racun.placen;
      valuesContainer.appendChild(placenValue);
    });
  }
}
