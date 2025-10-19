import Prodavnica from "./prodavnica.js";
import Hamburger from "./hamburger.js";

export default class App {
  constructor(container) {
    this._container = container;
    this.prodavnice = [];
    this.naziviSastojaka = [];
  }

  async Init() {
    // nazivi prodavnica
    let response = await fetch("https://localhost:7115/prodavnice/nazivi");
    const naziviProdavnica = await response.json();
    naziviProdavnica.forEach((prodavnica) => {
      this.prodavnice.push(new Prodavnica(prodavnica));
    });

    // nazivi sastojaka
    response = await fetch("https://localhost:7115/sastojci");
    const sastojci = await response.json();
    sastojci.forEach((sastojak) => {
      this.naziviSastojaka.push(sastojak);
    });

    // nazivi hamburgera
    for (const prodavnica of this.prodavnice) {
      // razmak menjamo sa %20 zato sto u URL-u ne sme da bude razmak
      response = await fetch(
        `https://localhost:7115/prodavnice/${prodavnica.naziv.replace(
          " ",
          "%20"
        )}/nazivi-hamburgera`
      );

      const hamburgeri = await response.json();
      hamburgeri.forEach((hamburger) => {
        prodavnica.hamburgeri.push(new Hamburger(hamburger));
      });
    }

    this.#CreateOdabirSastojaka();

    // hamburgeri i njihovi prilozi
    for (const prodavnica of this.prodavnice) {
      // razmak menjamo sa %20 zato sto u URL-u ne sme da bude razmak
      response = await fetch(
        `https://localhost:7115/hamburgeri/${prodavnica.naziv.replace(
          " ",
          "%20"
        )}`
      );

      const hamburgeri = await response.json();
      prodavnica.hamburgeri.forEach((hamburger, i) => {
        hamburger.id = hamburgeri[i].id;
        hamburger.naziv = hamburgeri[i].naziv;
        hamburger.prodat = hamburgeri[i].prodat;
        hamburger.sastojci = hamburgeri[i].sastojci;
      });
    }

    this.#CreateHamburgersDisplay();
  }

  #CreateOdabirSastojaka() {
    this.prodavnice.forEach((prodavnica) => {
      const prodavnicaContainer = document.createElement("div");
      prodavnicaContainer.classList.add(
        "prodavnica-container",
        prodavnica.naziv.toLowerCase().replace(" ", "-")
      );
      this._container.appendChild(prodavnicaContainer);

      const odabirSastojakaContainer = document.createElement("div");
      odabirSastojakaContainer.classList.add("odabir-sastojaka-container");
      prodavnicaContainer.appendChild(odabirSastojakaContainer);

      const nazivProdavniceContainer = document.createElement("div");
      nazivProdavniceContainer.classList.add("naziv-prodavnice-container");
      odabirSastojakaContainer.appendChild(nazivProdavniceContainer);

      const nazivProdavniceLabel = document.createElement("label");
      nazivProdavniceLabel.textContent = prodavnica.naziv;
      nazivProdavniceContainer.appendChild(nazivProdavniceLabel);

      const menuContainer = document.createElement("div");
      menuContainer.classList.add("menu-container");
      odabirSastojakaContainer.appendChild(menuContainer);

      // sastojak
      const sastojakContainer = document.createElement("div");
      sastojakContainer.classList.add("sastojak-container");
      menuContainer.appendChild(sastojakContainer);

      const sastojakLabel = document.createElement("label");
      sastojakLabel.textContent = "Sastojak:";
      sastojakContainer.appendChild(sastojakLabel);

      const sastojakSelect = document.createElement("select");
      sastojakContainer.appendChild(sastojakSelect);

      this.naziviSastojaka.forEach((sastojak) => {
        const sastojakSelectOption = document.createElement("option");
        sastojakSelectOption.textContent = sastojak.toLowerCase();
        sastojakSelect.appendChild(sastojakSelectOption);
      });

      // hamburger
      const hamburgerContainer = document.createElement("div");
      hamburgerContainer.classList.add("hamburger-container");
      menuContainer.appendChild(hamburgerContainer);

      const hamburgerLabel = document.createElement("label");
      hamburgerLabel.textContent = "Hamburger:";
      hamburgerContainer.appendChild(hamburgerLabel);

      const hamburgerSelect = document.createElement("select");
      hamburgerContainer.appendChild(hamburgerSelect);

      prodavnica.hamburgeri.forEach((hamburger) => {
        const hamburgerSelectOption = document.createElement("option");
        hamburgerSelectOption.textContent = hamburger.naziv;
        hamburgerSelect.appendChild(hamburgerSelectOption);
      });

      // kolicina
      const kolicinaContainer = document.createElement("div");
      kolicinaContainer.classList.add("kolicina-container");
      menuContainer.appendChild(kolicinaContainer);

      const kolicinaLabel = document.createElement("label");
      kolicinaLabel.textContent = "Kolicina:";
      kolicinaContainer.appendChild(kolicinaLabel);

      const kolicinaInput = document.createElement("input");
      kolicinaInput.type = "number";
      kolicinaContainer.appendChild(kolicinaInput);

      const dodajButtonContainer = document.createElement("div");
      dodajButtonContainer.classList.add("dodaj-btn-container");
      menuContainer.appendChild(dodajButtonContainer);

      const dodajButton = document.createElement("button");
      dodajButton.textContent = "Dodaj";
      dodajButtonContainer.appendChild(dodajButton);
    });
  }

  // # za privatne clanove klase
  #CreateHamburgersDisplay() {
    this.prodavnice.forEach((prodavnica) => {
      const prodavnicaContainer = document.querySelector(
        `.${prodavnica.naziv.toLowerCase().replace(" ", "-")}`
      );
      const hamburgeriContainer = document.createElement("div");
      hamburgeriContainer.classList.add("hamburgeri-container");
      prodavnicaContainer.appendChild(hamburgeriContainer);

      console.log(prodavnica.hamburgeri);
      prodavnica.hamburgeri.forEach((hamburger) => {
        // container
        const hamContainer = document.createElement("div");
        hamContainer.classList.add("ham-container");
        hamburgeriContainer.appendChild(hamContainer);

        // gornja zemicka
        const topHleb = document.createElement("div");
        topHleb.classList.add("hleb", "sastojak");
        topHleb.textContent = "hleb";
        hamContainer.appendChild(topHleb);

        // sastojci (prilozi) izmedju zemicki
        const sastojciContainer = document.createElement("div");
        sastojciContainer.classList.add("sastojci-container");
        hamContainer.appendChild(sastojciContainer);
        if (hamburger.prodat == false && hamburger.sastojci != null) {
          hamburger.sastojci.forEach((sastojak) => {
            const sastojakElement = document.createElement("div");
            sastojakElement.classList.add("sastojak");
            sastojakElement.textContent = sastojak.naziv.toLowerCase();
            sastojakElement.style.height = `${sastojak.debljina}px`;
            if (sastojak.naziv === "Salata") {
              sastojakElement.style.backgroundColor = "lightgreen";
            } else if (sastojak.naziv === "Meso") {
              sastojakElement.style.backgroundColor = "brown";
            } else if (sastojak.naziv === "Paradajz") {
              sastojakElement.style.backgroundColor = "red";
            } else if (sastojak.naziv === "Sir") {
              sastojakElement.style.backgroundColor = "lightyellow";
            } else {
              sastojakElement.style.backgroundColor = "lightblue";
            }

            sastojciContainer.appendChild(sastojakElement);
          });
        }

        // donja zemicka
        const bottomHleb = document.createElement("div");
        bottomHleb.classList.add("hleb", "sastojak");
        bottomHleb.textContent = "hleb";
        hamContainer.appendChild(bottomHleb);

        // dugme kupi
        const kupiButton = document.createElement("button");
        kupiButton.textContent = "Kupi";
        kupiButton.dataset.hamburgerId = hamburger.id;
        kupiButton.addEventListener("click", async (event) => {
          // + ispred string-a kovertuje string u number
          const hamburgerId = +event.target.dataset.hamburgerId;

          await fetch("https://localhost:7115/hamburgeri/prodaj", {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              id: hamburgerId,
              prodat: true,
            }),
          });

          sastojciContainer.innerHTML = "";
        });
        hamContainer.appendChild(kupiButton);

        // naziv hamburgera
        const hamLabelContainer = document.createElement("div");
        hamLabelContainer.classList.add("ham-label-container");
        hamContainer.appendChild(hamLabelContainer);

        const hamLabel = document.createElement("label");
        hamLabel.textContent = hamburger.naziv;
        hamLabelContainer.appendChild(hamLabel);
      });
    });
  }
}
