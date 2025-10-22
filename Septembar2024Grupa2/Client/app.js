import Proizvod from "./proizvod.js";

export default class App {
  constructor(container) {
    this.container = container;
    this.proizvodi = new Map();
    this.korpa = [];
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

    const korpaItemsContainer = document.createElement("div");
    korpaItemsContainer.classList.add("items");
    korpaContainer.appendChild(korpaItemsContainer);

    const korpaUkupnoContainer = document.createElement("div");
    korpaUkupnoContainer.classList.add("item-container");
    korpaContainer.appendChild(korpaUkupnoContainer);

    const ukupnoLabelContainer = document.createElement("div");
    ukupnoLabelContainer.classList.add("values-container");
    korpaUkupnoContainer.appendChild(ukupnoLabelContainer);

    const ukupnoLabel = document.createElement("label");
    ukupnoLabel.classList.add("ukupno-label");

    ukupnoLabelContainer.appendChild(ukupnoLabel);

    const ukupnoValueContainer = document.createElement("div");
    ukupnoValueContainer.classList.add("info-labels");
    korpaUkupnoContainer.appendChild(ukupnoValueContainer);

    const ukupnoValue = document.createElement("label");
    ukupnoValue.classList.add("ukupno-value");
    ukupnoValueContainer.appendChild(ukupnoValue);

    const naruciButtonContainer = document.createElement("div");
    naruciButtonContainer.classList.add("naruci-btn-container");
    korpaContainer.appendChild(naruciButtonContainer);

    const selectKategorija = document.querySelector(".select-kategorija");
    selectKategorija.addEventListener("change", async () => {
      const izabranaKategorija =
        selectKategorija.options[selectKategorija.selectedIndex];
      proizvodiCardsContainer.innerHTML = "";
      proizvodInfoContainer.innerHTML = "";
      korpaItemsContainer.innerHTML = "";
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
      const proizvodCard = document.createElement("div");
      proizvodCard.classList.add("proizvod-card");
      proizvodCard.dataset.proizvodId = proizvod.id;
      proizvodCard.addEventListener("click", (event) => {
        const proizvodInfoContainer = document.querySelector(
          ".proizvod-info-container"
        );
        proizvodInfoContainer.innerHTML = "";
        this.proizvodi.forEach((value, _) => {
          value.card.classList.remove("proizvod-card-clicked");
        });

        let proizvodId = 0;
        if (event.target.classList.contains("proizvod-card")) {
          event.target.classList.add("proizvod-card-clicked");
          proizvodId = event.target.dataset.proizvodId;
        } else if (
          event.target.parentNode.classList.contains("proizvod-card")
        ) {
          event.target.parentNode.classList.add("proizvod-card-clicked");
          proizvodId = event.target.parentNode.dataset.proizvodId;
        }
        proizvodId = Number(proizvodId);
        if (proizvodId !== 0) {
          const proizvod = this.proizvodi.get(proizvodId);
          const nazivInfoContainer = document.createElement("div");
          proizvodInfoContainer.appendChild(nazivInfoContainer);

          const nazivInfoLabel = document.createElement("label");
          nazivInfoLabel.classList.add("info-labels");
          nazivInfoLabel.textContent = "Naziv: ";
          nazivInfoContainer.appendChild(nazivInfoLabel);

          const nazivInfoValue = document.createElement("label");
          nazivInfoValue.classList.add("info-values");
          nazivInfoValue.textContent = proizvod.naziv;
          nazivInfoContainer.appendChild(nazivInfoValue);

          const duziOpisInfoContainer = document.createElement("div");
          proizvodInfoContainer.appendChild(duziOpisInfoContainer);

          const duziOpisInfoLabel = document.createElement("label");
          duziOpisInfoLabel.textContent = proizvod.duziOpis;
          duziOpisInfoContainer.appendChild(duziOpisInfoLabel);

          const cenaInfoContainer = document.createElement("div");
          proizvodInfoContainer.appendChild(cenaInfoContainer);

          const cenaInfoLabel = document.createElement("label");
          cenaInfoLabel.classList.add("info-labels");
          cenaInfoLabel.textContent = "Cena: ";
          cenaInfoContainer.appendChild(cenaInfoLabel);

          const cenaInfoValue = document.createElement("label");
          cenaInfoValue.classList.add("info-values");
          cenaInfoValue.textContent = `${proizvod.cena} dinara`;
          cenaInfoContainer.appendChild(cenaInfoValue);

          const kolicinaInfoContainer = document.createElement("div");
          proizvodInfoContainer.appendChild(kolicinaInfoContainer);

          const kolicinaInfoLabel = document.createElement("label");
          kolicinaInfoLabel.classList.add("info-labels");
          kolicinaInfoLabel.textContent = "Preostala kolicina: ";
          kolicinaInfoContainer.appendChild(kolicinaInfoLabel);

          const kolicinaInfoValue = document.createElement("label");
          kolicinaInfoValue.classList.add("info-values");
          kolicinaInfoValue.textContent = `${proizvod.kolicina} komada`;
          kolicinaInfoContainer.appendChild(kolicinaInfoValue);
        }
      });
      proizvodiCardsContainer.appendChild(proizvodCard);

      this.proizvodi.set(
        proizvod.id,
        new Proizvod(
          proizvod.naziv,
          proizvod.kolicina,
          proizvod.cena,
          proizvod.kratakOpis,
          proizvod.duziOpis,
          proizvodCard
        )
      );
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
      kolicinaProizvodaValue.classList.add(`label-${proizvod.id}`);
      kolicinaProizvodaValue.textContent = proizvod.kolicina;
      proizvodiCardValuesContainer.appendChild(kolicinaProizvodaValue);

      const cenaProizvodaValue = document.createElement("label");
      cenaProizvodaValue.textContent = `${proizvod.cena} din`;
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

      const ukupnoValue = document.querySelector(".ukupno-value");
      const ukupnoLabel = document.querySelector(".ukupno-label");
      kupiButton.addEventListener("click", (event) => {
        const proizvodId = Number(event.target.dataset.proizvodId);
        const proizvod = this.proizvodi.get(proizvodId);
        if (proizvod.kolicina > 0) {
          ukupnoLabel.textContent = "Ukupno:";
          const kolicinaLabel = document.querySelector(`.label-${proizvodId}`);
          proizvod.kolicina -= 1;
          kolicinaLabel.textContent = proizvod.kolicina;

          const korpaItemsContainer = document.querySelector(".items");
          korpaItemsContainer.classList.add("korpa-items-container");
          const itemContainer = document.createElement("div");
          itemContainer.classList.add("item-container");
          korpaItemsContainer.appendChild(itemContainer);

          const nazivProizvodaContainer = document.createElement("div");
          itemContainer.appendChild(nazivProizvodaContainer);

          const nazivProizvodaLabel = document.createElement("label");
          nazivProizvodaLabel.textContent = proizvod.naziv;
          nazivProizvodaContainer.appendChild(nazivProizvodaLabel);

          const cenaProizvodaContainer = document.createElement("div");
          itemContainer.appendChild(cenaProizvodaContainer);

          const cenaProizvodaLabel = document.createElement("label");
          cenaProizvodaLabel.classList.add("info-labels");
          cenaProizvodaLabel.textContent = `Cena: ${proizvod.cena} din`;
          cenaProizvodaContainer.appendChild(cenaProizvodaLabel);

          this.korpa.push(proizvodId);
          let ukupno = 0;
          this.korpa.forEach((proizvodId) => {
            const proizvod = this.proizvodi.get(proizvodId);
            ukupno += proizvod.cena;
            ukupnoValue.textContent = `Cena: ${ukupno} din`;
          });
        }
      });
      kupiButtonContainer.appendChild(kupiButton);
    });
    const naruciButtonContainer = document.querySelector(
      ".naruci-btn-container"
    );
    const naruciButton = document.createElement("button");
    naruciButton.textContent = "Naruci";
    naruciButtonContainer.appendChild(naruciButton);

    naruciButton.addEventListener("click", async () => {
      const response = await fetch("https://localhost:7138/api/kupovine", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          proizvodiIds: this.korpa,
        }),
      });

      console.log(response);
    });
  }
}
