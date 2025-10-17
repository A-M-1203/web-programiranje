export default class App {
  constructor(container) {
    this.automobili = [];
    this.container = container;
  }

  Init() {
    this.RenderLeftSide();
    this.RenderRightSide();
  }

  RenderLeftSide() {
    const containerLeft = document.createElement("div");
    containerLeft.classList.add("left");

    const rentalContainer = this.RentalContainer();
    const filterContainer = this.FilterContainer();

    containerLeft.appendChild(rentalContainer);
    containerLeft.appendChild(filterContainer);

    this.container.appendChild(containerLeft);
  }

  RentalContainer() {
    const rentalContainer = document.createElement("div");
    rentalContainer.classList.add("rental");
    this.RentalLabels(rentalContainer);
    this.RentalInputs(rentalContainer);
    return rentalContainer;
  }

  RentalLabels(rentalContainer) {
    const rentalLabels = document.createElement("div");
    rentalLabels.classList.add("labels");

    // Ime i prezime label
    const imePrezimeLabel = document.createElement("label");
    imePrezimeLabel.textContent = "Ime i prezime:";
    rentalLabels.appendChild(imePrezimeLabel);

    // JMBG label
    const jmbgLabel = document.createElement("label");
    jmbgLabel.textContent = "JMBG:";
    rentalLabels.appendChild(jmbgLabel);

    // Broj vozacke label
    const brojVozackeLabel = document.createElement("label");
    brojVozackeLabel.textContent = "Broj vozacke dozvole:";
    rentalLabels.appendChild(brojVozackeLabel);

    // Broj dana label
    const brojDanaLabel = document.createElement("label");
    brojDanaLabel.textContent = "Broj dana:";
    rentalLabels.appendChild(brojDanaLabel);

    rentalContainer.appendChild(rentalLabels);
  }

  RentalInputs(rentalContainer) {
    const rentalInputs = document.createElement("div");
    rentalInputs.classList.add("inputs");

    // Ime i prezime input
    const imePrezimeInput = document.createElement("input");
    imePrezimeInput.type = "text";
    imePrezimeInput.classList.add("imeprezime-input");
    rentalInputs.appendChild(imePrezimeInput);

    // JMBG input
    const jmbgInput = document.createElement("input");
    jmbgInput.type = "number";
    jmbgInput.classList.add("jmbg-input");
    rentalInputs.appendChild(jmbgInput);

    // Broj vozacke input
    const brojVozackeInput = document.createElement("input");
    brojVozackeInput.type = "number";
    brojVozackeInput.classList.add("brojvozacke-input");
    rentalInputs.appendChild(brojVozackeInput);

    // Broj dana input
    const brojDanaInput = document.createElement("input");
    brojDanaInput.type = "number";
    brojDanaInput.classList.add("brojdana-input");
    rentalInputs.appendChild(brojDanaInput);

    rentalContainer.appendChild(rentalInputs);
  }

  FilterContainer() {
    const filterContainer = document.createElement("div");
    filterContainer.classList.add("filter");
    this.FilterLabels(filterContainer);
    this.FilterInputs(filterContainer);
    return filterContainer;
  }

  FilterLabels(filterContainer) {
    const filterLabels = document.createElement("div");
    filterLabels.classList.add("labels");

    // Predjena kilometraza label
    const kilometrazaLabel = document.createElement("label");
    kilometrazaLabel.textContent = "Predjena kilometraza:";
    filterLabels.appendChild(kilometrazaLabel);

    // Broj sedista label
    const brojSedistaLabel = document.createElement("label");
    brojSedistaLabel.textContent = "Broj sedista:";
    filterLabels.appendChild(brojSedistaLabel);

    // Cena label
    const cenaLabel = document.createElement("label");
    cenaLabel.textContent = "Cena:";
    filterLabels.appendChild(cenaLabel);

    // Model label
    const modelLabel = document.createElement("label");
    modelLabel.textContent = "Model:";
    filterLabels.appendChild(modelLabel);

    filterContainer.appendChild(filterLabels);
  }

  FilterInputs(filterContainer) {
    const filterInputs = document.createElement("div");
    filterInputs.classList.add("inputs");

    // Predjena kilometraza input
    const kilometrazaInput = document.createElement("input");
    kilometrazaInput.type = "number";
    kilometrazaInput.classList.add("kilometraza-input");
    filterInputs.appendChild(kilometrazaInput);

    // Broj sedista input
    const brojSedistaInput = document.createElement("input");
    brojSedistaInput.type = "number";
    brojSedistaInput.classList.add("brojsedista-input");
    filterInputs.appendChild(brojSedistaInput);

    // Cena input
    const cenaInput = document.createElement("input");
    cenaInput.type = "number";
    cenaInput.classList.add("cena-input");
    filterInputs.appendChild(cenaInput);

    // Model select
    const modelSelect = document.createElement("select");
    modelSelect.classList.add("model-select");
    filterInputs.appendChild(modelSelect);

    // Ubacivanje modela automobila u model select
    this.GetModels(modelSelect);

    // filter button
    const filterButton = document.createElement("button");
    filterButton.classList.add("filter-btn");
    filterButton.textContent = "Filtriraj prikaz";
    filterButton.addEventListener(
      "click",
      this.GetFilteredAutomobili.bind(this)
    );

    filterContainer.appendChild(filterInputs);
    filterContainer.appendChild(filterButton);
  }

  async GetFilteredAutomobili() {
    const queryString = new URL("http://localhost:5175/automobili");

    const kilometrazaInput = this.container.querySelector(".kilometraza-input");
    if (kilometrazaInput.value != "") {
      queryString.searchParams.set("kilometraza", kilometrazaInput.value);
    }

    const brojSedistaInput = this.container.querySelector(".brojsedista-input");
    if (brojSedistaInput.value != "") {
      queryString.searchParams.set("brojSedista", brojSedistaInput.value);
    }

    const cenaInput = this.container.querySelector(".cena-input");
    if (cenaInput.value != "") {
      queryString.searchParams.set("cena", cenaInput.value);
    }

    const modelSelect = this.container.querySelector(".model-select");
    if (modelSelect.value != "") {
      queryString.searchParams.set("model", modelSelect.value);
    }

    const response = await fetch(queryString);
    const data = await response.json();

    this.automobili = [];
    const containerRight = this.container.querySelector(".right");
    containerRight.innerHTML = "";
    data.forEach((object) => {
      const card = this.CreateCard(object);
      containerRight.appendChild(card);
      this.automobili.push(object);
    });
  }

  async GetModels(modelSelect) {
    const url = "http://localhost:5175/automobili/modeli";
    const response = await fetch(url);
    const data = await response.json();
    const emptyOption = document.createElement("option");
    emptyOption.value = "";
    emptyOption.textContent = "";
    modelSelect.appendChild(emptyOption);
    data.forEach((model) => {
      const option = document.createElement("option");
      option.value = model;
      option.textContent = model;
      modelSelect.appendChild(option);
    });
  }

  RenderRightSide() {
    const containerRight = document.createElement("div");
    containerRight.classList.add("right");

    this.container.appendChild(containerRight);
  }

  CreateCardNames() {
    const cardNames = document.createElement("div");
    cardNames.classList.add("labels");

    const modelName = document.createElement("label");
    modelName.textContent = "Model";
    cardNames.appendChild(modelName);

    const kilometrazaName = document.createElement("label");
    kilometrazaName.textContent = "Kilometraza";
    cardNames.appendChild(kilometrazaName);

    const godisteName = document.createElement("label");
    godisteName.textContent = "Godiste";
    cardNames.appendChild(godisteName);

    const brojSedistaName = document.createElement("label");
    brojSedistaName.textContent = "Broj sedista";
    cardNames.appendChild(brojSedistaName);

    const cenaPoDanuName = document.createElement("label");
    cenaPoDanuName.textContent = "Cena po danu";
    cardNames.appendChild(cenaPoDanuName);

    const iznajmljenName = document.createElement("label");
    iznajmljenName.textContent = "Iznajmljen";
    cardNames.appendChild(iznajmljenName);

    return cardNames;
  }

  CreateCardValues() {
    const cardValues = document.createElement("div");
    cardValues.classList.add("inputs");
    return cardValues;
  }

  CreateCard(object) {
    const cardNames = this.CreateCardNames();

    const card = document.createElement("div");
    card.classList.add("card");

    card.appendChild(cardNames);

    // -----------------------------------

    const cardValues = this.CreateCardValues();

    const modelValue = document.createElement("label");
    modelValue.textContent = object.model;
    cardValues.appendChild(modelValue);

    const kilometrazaValue = document.createElement("label");
    kilometrazaValue.textContent = object.kilometraza;
    cardValues.appendChild(kilometrazaValue);

    const godisteValue = document.createElement("label");
    godisteValue.textContent = object.godiste;
    cardValues.appendChild(godisteValue);

    const brojSedistaValue = document.createElement("label");
    brojSedistaValue.textContent = object.brojSedista;
    cardValues.appendChild(brojSedistaValue);

    const cenaPoDanuValue = document.createElement("label");
    cenaPoDanuValue.textContent = object.cenaPoDanu + "e";
    cardValues.appendChild(cenaPoDanuValue);

    const iznajmljenValue = document.createElement("label");
    iznajmljenValue.textContent = object.trenutnoIznajmljen;
    cardValues.appendChild(iznajmljenValue);

    const iznajmiButton = document.createElement("button");
    iznajmiButton.textContent = "Iznajmi";
    iznajmiButton.classList.add("iznajmi-btn");
    iznajmiButton.dataset.id = object.id;
    iznajmiButton.addEventListener("click", (event) => {
      this.HandleIznajmi(event);
    });

    if (object.trenutnoIznajmljen == true) {
      card.classList.add("card-iznajmljen");
      iznajmiButton.disabled = true;
    } else {
      card.classList.add("card-nijeiznajmljen");
    }

    card.appendChild(cardValues);
    card.appendChild(iznajmiButton);
    return card;
  }

  async HandleIznajmi(event) {
    console.log(event.target.dataset.id);
    const imePrezimeInput = this.container.querySelector(".imeprezime-input");
    if (
      imePrezimeInput.value.trim() == "" ||
      !imePrezimeInput.value.trim().includes(" ")
    ) {
      alert("Ime i prezime je obavezno polje.");
      return;
    }

    const jmbgInput = this.container.querySelector(".jmbg-input");
    if (jmbgInput.value == "") {
      alert("JMBG je obavezno polje.");
      return;
    }

    if (jmbgInput.value.includes("+")) {
      alert("JMBG ne sme da sadrzi znak '+'.");
      return;
    }

    if (jmbgInput.value.includes("-")) {
      alert("JMBG ne sme da sadrzi znak '-'.");
      return;
    }

    const brojVozackeInput = this.container.querySelector(".brojvozacke-input");
    if (brojVozackeInput.value == "") {
      alert("Broj vozacke dozvole je obavezno polje.");
      return;
    }

    if (brojVozackeInput.value.includes("+")) {
      alert("Broj vozacke dozvole ne sme da sadrzi znak '+'.");
      return;
    }

    if (brojVozackeInput.value.includes("-")) {
      alert("Broj vozacke dozvole ne sme da sadrzi znak '-'.");
      return;
    }

    const brojDanaInput = this.container.querySelector(".brojdana-input");
    if (brojDanaInput.value == "") {
      alert("Broj dana je obavezno polje.");
      return;
    }

    if (brojDanaInput.value.includes("+")) {
      alert("Broj dana ne sme da sadrzi znak '+'.");
      return;
    }

    if (brojDanaInput.value.includes("-")) {
      alert("Broj dana ne sme da sadrzi znak '-'.");
      return;
    }

    const parts = imePrezimeInput.value.split(" ");
    const ime = parts[0];
    const prezime = parts[1];

    let url = "http://localhost:5175/automobili/" + event.target.dataset.id;
    let response = await fetch(url);
    const automobilData = await response.json();
    if (response.status != 200) {
      alert(automobilData.detail);
      return;
    }

    if (automobilData.trenutnoIznajmljen == true) {
      alert("Automobil je vec iznajmljen.");
      return;
    }

    // url = "http://localhost:5175/korisnici/jmbg/" + jmbgInput.value;
    // response = await fetch(url);
    // const korisnikData = await response.json();
    // if (response.status != 200) {
    //   alert(korisnikData.detail);
    //   return;
    // }

    const jmbg = jmbgInput.value;
    const brojVozacke = brojVozackeInput.value;
    url = "http://localhost:5175/korisnici";
    response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        ime,
        prezime,
        jmbg,
        brojVozacke,
      }),
    });
    const data = await response.json();
    console.log(response);
    console.log(data);
  }
}
