import App from "./app.js";

const body = document.querySelector("body");
const mainContainer = document.createElement("div");
mainContainer.classList.add("main-container");
body.appendChild(mainContainer);
const app = new App(mainContainer);
app.Init();
