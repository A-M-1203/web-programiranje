const bodyElement = document.querySelector("body");

const container = document.createElement("div");
container.classList.add("container");

// --------------------------- left --------------------------------
const containerLeft = document.createElement("div");
containerLeft.classList.add("left");

// rental
const rentalContainer = document.createElement("div");
rentalContainer.classList.add("rental");

const rentalLabels = document.createElement("div");
rentalLabels.classList.add("labels");
const rentalInputs = document.createElement("div");
rentalInputs.classList.add("inputs");

// Ime i prezime label
const imePrezimeLabel = document.createElement("label");
imePrezimeLabel.textContent = "Ime i prezime:";
rentalLabels.appendChild(imePrezimeLabel);

// Ime i prezime input
const imePrezimeInput = document.createElement("input");
imePrezimeInput.type = "text";
rentalInputs.appendChild(imePrezimeInput);

// JMBG label
const jmbgLabel = document.createElement("label");
jmbgLabel.textContent = "JMBG:";
rentalLabels.appendChild(jmbgLabel);

// JMBG input
const jmbgInput = document.createElement("input");
jmbgInput.type = "number";
rentalInputs.appendChild(jmbgInput);

// Broj vozacke label
const brojVozackeLabel = document.createElement("label");
brojVozackeLabel.textContent = "Broj vozacke dozvole:";
rentalLabels.appendChild(brojVozackeLabel);

// Broj vozacke input
const brojVozackeInput = document.createElement("input");
brojVozackeInput.type = "number";
rentalInputs.appendChild(brojVozackeInput);

// Broj dana label
const brojDanaLabel = document.createElement("label");
brojDanaLabel.textContent = "Broj dana:";
rentalLabels.appendChild(brojDanaLabel);

// Broj dana input
const brojDanaInput = document.createElement("input");
brojDanaInput.type = "number";
rentalInputs.appendChild(brojDanaInput);

rentalContainer.appendChild(rentalLabels);
rentalContainer.appendChild(rentalInputs);

// filter
const filterContainer = document.createElement("div");
filterContainer.classList.add("filter");

const filterLabels = document.createElement("div");
filterLabels.classList.add("labels");
const filterInputs = document.createElement("div");
filterInputs.classList.add("inputs");

// Predjena kilometraza label
const kilometrazaLabel = document.createElement("label");
kilometrazaLabel.textContent = "Predjena kilometraza:";
filterLabels.appendChild(kilometrazaLabel);

// Predjena kilometraza input
const kilometrazaInput = document.createElement("input");
kilometrazaInput.type = "number";
filterInputs.appendChild(kilometrazaInput);

// Broj sedista label
const brojSedistaLabel = document.createElement("label");
brojSedistaLabel.textContent = "Broj sedista:";
filterLabels.appendChild(brojSedistaLabel);

// Broj sedista input
const brojSedistaInput = document.createElement("input");
brojSedistaInput.type = "number";
filterInputs.appendChild(brojSedistaInput);

// Cena label
const cenaLabel = document.createElement("label");
cenaLabel.textContent = "Cena:";
filterLabels.appendChild(cenaLabel);

// Cena input
const cenaInput = document.createElement("input");
cenaInput.type = "number";
filterInputs.appendChild(cenaInput);

// Model label
const modelLabel = document.createElement("label");
modelLabel.textContent = "Model:";
filterLabels.appendChild(modelLabel);

// Model select
const modelSelect = document.createElement("select");
filterInputs.appendChild(modelSelect);

// filter button
const filterButton = document.createElement("button");
filterButton.classList.add("filter-btn");
filterButton.textContent = "Filtriraj prikaz";

// button container
const buttonContainer = document.createElement("div");
buttonContainer.classList.add("btn-container");
buttonContainer.appendChild(filterButton);

filterContainer.appendChild(filterLabels);
filterContainer.appendChild(filterInputs);
filterContainer.appendChild(buttonContainer);

// ------------------------------ right -------------------------------
const containerRight = document.createElement("div");
containerRight.classList.add("right");

const createCard = function (card, cardNames, cardValues) {
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

  card.appendChild(cardNames);

  // -----------------------------------

  const modelValue = document.createElement("label");
  modelValue.textContent = "Toyota";
  cardValues.appendChild(modelValue);

  const kilometrazaValue = document.createElement("label");
  kilometrazaValue.textContent = "1136";
  cardValues.appendChild(kilometrazaValue);

  const godisteValue = document.createElement("label");
  godisteValue.textContent = "2016";
  cardValues.appendChild(godisteValue);

  const brojSedistaValue = document.createElement("label");
  brojSedistaValue.textContent = "5";
  cardValues.appendChild(brojSedistaValue);

  const cenaPoDanuValue = document.createElement("label");
  cenaPoDanuValue.textContent = "30e";
  cardValues.appendChild(cenaPoDanuValue);

  const iznajmljenValue = document.createElement("label");
  iznajmljenValue.textContent = "false";
  cardValues.appendChild(iznajmljenValue);

  const iznajmiButton = document.createElement("button");
  iznajmiButton.textContent = "Iznajmi";
  iznajmiButton.classList.add("iznajmi-btn");
  // iznajmiButton.disabled = true;

  card.appendChild(cardValues);
  card.appendChild(iznajmiButton);
  return card;
};

// card names
const cardNames1 = document.createElement("div");
cardNames1.classList.add("labels");

// card values
const cardValues1 = document.createElement("div");
cardValues1.classList.add("inputs");

// card
let card1 = document.createElement("div");
card1.classList.add("card");
card1 = createCard(card1, cardNames1, cardValues1);
containerRight.appendChild(card1);

// card names
const cardNames2 = document.createElement("div");
cardNames2.classList.add("labels");

// card values
const cardValues2 = document.createElement("div");
cardValues2.classList.add("inputs");

// card
let card2 = document.createElement("div");
card2.classList.add("card");
card2 = createCard(card2, cardNames2, cardValues2);
containerRight.appendChild(card2);

// card names
const cardNames3 = document.createElement("div");
cardNames3.classList.add("labels");

// card values
const cardValues3 = document.createElement("div");
cardValues3.classList.add("inputs");

// card
let card3 = document.createElement("div");
card3.classList.add("card");
card3 = createCard(card3, cardNames3, cardValues3);
containerRight.appendChild(card3);

// card names
const cardNames4 = document.createElement("div");
cardNames4.classList.add("labels");

// card values
const cardValues4 = document.createElement("div");
cardValues4.classList.add("inputs");

// card
let card4 = document.createElement("div");
card4.classList.add("card");
card4 = createCard(card4, cardNames4, cardValues4);
containerRight.appendChild(card4);

// card names
const cardNames5 = document.createElement("div");
cardNames5.classList.add("labels");

// card values
const cardValues5 = document.createElement("div");
cardValues5.classList.add("inputs");

// card
let card5 = document.createElement("div");
card5.classList.add("card");
card5 = createCard(card5, cardNames5, cardValues5);
containerRight.appendChild(card5);

containerLeft.appendChild(rentalContainer);
containerLeft.appendChild(filterContainer);
container.appendChild(containerLeft);
container.appendChild(containerRight);

bodyElement.appendChild(container);
