export default class App {
  constructor(container) {
    this.container = container;
    this.producentskeKuceFilmoviKategorije = [];
  }

  Init() {
    this.CreateCards();
  }

  async FetchData(url) {
    const response = await fetch(url);
    const data = await response.json();
    return data;
  }

  async CreateCards() {
    const data = await this.FetchData(
      "http://localhost:5139/producentske-kuce/kategorije-i-filmovi"
    );
    data.forEach(async (element) => {
      const producentskaKucaContainer = document.createElement("div");
      producentskaKucaContainer.classList.add("producentska-kuca-container");
      this.container.appendChild(producentskaKucaContainer);

      const producentskaKucaLabelContainer = document.createElement("div");
      producentskaKucaLabelContainer.classList.add("label-container");
      producentskaKucaContainer.appendChild(producentskaKucaLabelContainer);

      const producentskaKucaLabel = document.createElement("label");
      producentskaKucaLabel.textContent = element.naziv;
      producentskaKucaLabelContainer.appendChild(producentskaKucaLabel);

      const menuContainer = document.createElement("div");
      menuContainer.classList.add("menu-container");
      producentskaKucaContainer.appendChild(menuContainer);

      const kategorijaContainer = document.createElement("div");
      kategorijaContainer.classList.add("menu-subcontainer");
      menuContainer.appendChild(kategorijaContainer);

      const kategorijaLabel = document.createElement("label");
      kategorijaLabel.textContent = "Kategorija:";
      kategorijaContainer.appendChild(kategorijaLabel);

      const kategorijaSelect = document.createElement("select");
      kategorijaContainer.appendChild(kategorijaSelect);

      const kategorije = new Set();
      element.filmovi.forEach((element) => kategorije.add(element.kategorija));
      const kategorijeNiz = [...kategorije];

      element.filmovi.forEach((_, i) => {
        if (i < kategorijeNiz.length) {
          const kategorijaSelectOption = document.createElement("option");
          kategorijaSelectOption.textContent = kategorijeNiz[i];
          kategorijaSelect.appendChild(kategorijaSelectOption);
        }
      });

      const filmContainer = document.createElement("div");
      filmContainer.classList.add("menu-subcontainer");
      menuContainer.appendChild(filmContainer);

      const filmLabel = document.createElement("label");
      filmLabel.textContent = "Film:";
      filmContainer.appendChild(filmLabel);

      const filmSelect = document.createElement("select");
      filmContainer.appendChild(filmSelect);

      element.filmovi.forEach((element) => {
        const filmSelectOption = document.createElement("option");
        filmSelectOption.textContent = element.naziv;
        filmSelect.appendChild(filmSelectOption);
      });

      const ocenaContainer = document.createElement("div");
      ocenaContainer.classList.add("menu-subcontainer");
      menuContainer.appendChild(ocenaContainer);

      const ocenaLabel = document.createElement("label");
      ocenaLabel.textContent = "Ocena:";
      ocenaContainer.appendChild(ocenaLabel);

      const ocenaInput = document.createElement("input");
      ocenaInput.type = "number";
      ocenaContainer.appendChild(ocenaInput);

      const snimiOcenuButton = document.createElement("button");
      snimiOcenuButton.classList.add("snimi-ocenu-btn");
      snimiOcenuButton.textContent = "Snimi ocenu";
      menuContainer.appendChild(snimiOcenuButton);

      const displayContainer = document.createElement("div");
      displayContainer.classList.add("display-container");
      producentskaKucaContainer.appendChild(displayContainer);

      const data = await this.FetchData(
        "http://localhost:5139/producentske-kuce/" +
          producentskaKucaLabel.textContent +
          "/filmovi-i-ocene"
      );

      const najgoreOcenjeniFilmContainer = document.createElement("div");
      najgoreOcenjeniFilmContainer.classList.add("film-container");
      displayContainer.appendChild(najgoreOcenjeniFilmContainer);

      const najgoreOcenjeniFilmLabel = document.createElement("label");
      najgoreOcenjeniFilmLabel.textContent = data.najgore.naziv;
      najgoreOcenjeniFilmContainer.appendChild(najgoreOcenjeniFilmLabel);

      let prosecnaOcena = Math.round(data.najgore.prosecnaOcena);
      for (let i = 0; i < prosecnaOcena; i++) {
        const ocenaBarChunk = document.createElement("div");
        ocenaBarChunk.classList.add("ocena-bar-chunk");
        najgoreOcenjeniFilmContainer.appendChild(ocenaBarChunk);
      }

      const najgoraOcenaLabel = document.createElement("label");
      najgoraOcenaLabel.textContent = data.najgore.prosecnaOcena.toFixed(1);
      najgoreOcenjeniFilmContainer.appendChild(najgoraOcenaLabel);

      const srednjeOcenjeniFilmContainer = document.createElement("div");
      srednjeOcenjeniFilmContainer.classList.add("film-container");
      displayContainer.appendChild(srednjeOcenjeniFilmContainer);

      const srednjeOcenjeniFilmLabel = document.createElement("label");
      srednjeOcenjeniFilmLabel.textContent = data.srednje.naziv;
      srednjeOcenjeniFilmContainer.appendChild(srednjeOcenjeniFilmLabel);

      prosecnaOcena = Math.round(data.srednje.prosecnaOcena);
      for (let i = 0; i < prosecnaOcena; i++) {
        const ocenaBarChunk = document.createElement("div");
        ocenaBarChunk.classList.add("ocena-bar-chunk");
        srednjeOcenjeniFilmContainer.appendChild(ocenaBarChunk);
      }

      const srednjaOcenaLabel = document.createElement("label");
      srednjaOcenaLabel.textContent = data.srednje.prosecnaOcena.toFixed(1);
      srednjeOcenjeniFilmContainer.appendChild(srednjaOcenaLabel);

      const najboljeOcenjeniFilmContainer = document.createElement("div");
      najboljeOcenjeniFilmContainer.classList.add("film-container");
      displayContainer.appendChild(najboljeOcenjeniFilmContainer);

      const najboljeOcenjeniFilmLabel = document.createElement("label");
      najboljeOcenjeniFilmLabel.textContent = data.najbolje.naziv;
      najboljeOcenjeniFilmContainer.appendChild(najboljeOcenjeniFilmLabel);

      prosecnaOcena = Math.round(data.najbolje.prosecnaOcena);
      for (let i = 0; i < prosecnaOcena; i++) {
        const ocenaBarChunk = document.createElement("div");
        ocenaBarChunk.classList.add("ocena-bar-chunk");
        najboljeOcenjeniFilmContainer.appendChild(ocenaBarChunk);
      }

      const najvecaOcenaLabel = document.createElement("label");
      najvecaOcenaLabel.textContent = data.najbolje.prosecnaOcena.toFixed(1);
      najboljeOcenjeniFilmContainer.appendChild(najvecaOcenaLabel);

      console.log(data);
    });
  }
}
