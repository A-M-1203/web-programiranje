export default class App {
  constructor(container) {
    this.container = container;
    this.proizvodi = new Map();
  }

  async Init() {
    await this.#CreateSelectKategorija();
    const storeContainer = document.createElement("div");
    storeContainer.classList.add("store-container");
    this.container.appendChild(storeContainer);

    const proizvodInfoContainer = document.createElement("div");
    proizvodInfoContainer.classList.add("proizvod-info-container");
    storeContainer.appendChild(proizvodInfoContainer);

    const proizvodiCardsContainer = document.createElement("div");
    proizvodiCardsContainer.classList.add("proizvodi-cards-container");
    storeContainer.appendChild(proizvodiCardsContainer);

    const korpaContainer = document.createElement("div");
    korpaContainer.classList.add("korpa-container");
    storeContainer.appendChild(korpaContainer);

    const selectKategorija = document.querySelector(".select-kategorija");
    selectKategorija.addEventListener("change", async () => {
      const izabranaKategorija =
        selectKategorija.options[selectKategorija.selectedIndex];
      proizvodiCardsContainer.innerHTML = "";
      if (izabranaKategorija.textContent !== "") {
        await this.#CreateProizvodiCards(izabranaKategorija.textContent);
      }
    });
  }

  async #CreateSelectKategorija() {
    const selectKategorijaContainer = document.createElement("div");
    selectKategorijaContainer.classList.add("select-kategorija-container");
    this.container.appendChild(selectKategorijaContainer);

    const selectKategorija = document.createElement("select");
    selectKategorija.classList.add("select-kategorija");
    selectKategorijaContainer.appendChild(selectKategorija);

    const selectKategorijaEmptyOption = document.createElement("option");
    selectKategorijaEmptyOption.textContent = "";
    selectKategorija.appendChild(selectKategorijaEmptyOption);

    const response = await fetch("https://localhost:7138/api/kategorije");
    const naziviKategorija = await response.json();
    naziviKategorija.forEach((nazivKategorije) => {
      const selectKategorijaOption = document.createElement("option");
      selectKategorijaOption.textContent = nazivKategorije;
      selectKategorija.appendChild(selectKategorijaOption);
    });
  }

  async #CreateProizvodiCards(izabranaKategorija) {
    const proizvodiCardsContainer = document.querySelector(
      ".proizvodi-cards-container"
    );

    const response = await fetch(
      `https://localhost:7138/api/proizvodi/${izabranaKategorija
        .toLowerCase()
        .replace(" ", "%20")}`
    );
    const proizvodi = await response.json();
    this.proizvodi.clear();
    proizvodi.forEach((proizvod) => {
      this.proizvodi.set(proizvod.id, proizvod);
      const proizvodCard = document.createElement("div");
      proizvodCard.classList.add("proizvod-card");
      proizvodiCardsContainer.appendChild(proizvodCard);

      const proizvodiCardLabelsAndValuesContainer =
        document.createElement("div");
      proizvodiCardLabelsAndValuesContainer.classList.add(
        "proizvodi-labels-values-container"
      );
      proizvodCard.appendChild(proizvodiCardLabelsAndValuesContainer);

      const proizvodiCardLabelsContainer = document.createElement("div");
      proizvodiCardLabelsContainer.classList.add("labels-container");
      proizvodiCardLabelsAndValuesContainer.appendChild(
        proizvodiCardLabelsContainer
      );

      const nazivProizvodaLabel = document.createElement("label");
      nazivProizvodaLabel.textContent = "Naziv:";
      proizvodiCardLabelsContainer.appendChild(nazivProizvodaLabel);

      const kolicinaProizvodaLabel = document.createElement("label");
      kolicinaProizvodaLabel.textContent = "Kolicina:";
      proizvodiCardLabelsContainer.appendChild(kolicinaProizvodaLabel);

      const cenaProizvodaLabel = document.createElement("label");
      cenaProizvodaLabel.textContent = "Cena(kom):";
      proizvodiCardLabelsContainer.appendChild(cenaProizvodaLabel);

      const opisProizvodaLabel = document.createElement("label");
      opisProizvodaLabel.textContent = "Opis:";
      proizvodiCardLabelsContainer.appendChild(opisProizvodaLabel);

      const proizvodiCardValuesContainer = document.createElement("div");
      proizvodiCardValuesContainer.classList.add("values-container");
      proizvodiCardLabelsAndValuesContainer.appendChild(
        proizvodiCardValuesContainer
      );

      const nazivProizvodaValue = document.createElement("label");
      nazivProizvodaValue.textContent = proizvod.naziv;
      proizvodiCardValuesContainer.appendChild(nazivProizvodaValue);

      const kolicinaProizvodaValue = document.createElement("label");
      kolicinaProizvodaValue.textContent = proizvod.kolicina;
      proizvodiCardValuesContainer.appendChild(kolicinaProizvodaValue);

      const cenaProizvodaValue = document.createElement("label");
      cenaProizvodaValue.textContent = proizvod.cena;
      proizvodiCardValuesContainer.appendChild(cenaProizvodaValue);

      const opisProizvodaValue = document.createElement("label");
      opisProizvodaValue.textContent = proizvod.kratakOpis;
      proizvodiCardValuesContainer.appendChild(opisProizvodaValue);

      const kupiButtonContainer = document.createElement("div");
      kupiButtonContainer.classList.add("kupi-btn-container");
      proizvodCard.appendChild(kupiButtonContainer);

      const kupiButton = document.createElement("button");
      kupiButton.dataset.proizvodId = proizvod.id;
      kupiButton.textContent = "Kupi";
      kupiButtonContainer.appendChild(kupiButton);
    });
  }
}
