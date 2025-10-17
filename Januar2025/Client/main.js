import App from "./App.js";

const bodyElement = document.querySelector("body");

const container = document.createElement("div");
container.classList.add("container");
bodyElement.appendChild(container);

const app = new App(container);
app.Init();
