class MojoCarrousel extends HTMLElement {
	Roll(panels) {
		this.currentIndex++;
		if (this.currentIndex > panels.length - 1) this.currentIndex = 0;
		panels.forEach((e, i) => {
			e.style.opacity = i == this.currentIndex ? "1" : "0";
		});
	}
	constructor() {
		super();
		let shadow = this.attachShadow({
			mode: "open"
		});
		let style = document.createElement("style");
		style.textContent = ":host { display: block; height: 50vh; overflow: hidden; position: relative; } slot { overflow: hidden; position: relative;} ::slotted(*) { position: absolute; transition: opacity 250ms linear; display:block; width: 100%; } ::slotted([center]) { top:-50%; } ::slotted([bottom]) {top: auto; bottom: 0%; }";
		shadow.appendChild(style);
		let slot = document.createElement("slot");
		slot.id = "imgs";
		shadow.appendChild(slot);
	}
	connectedCallback() {
		this.Panels = Array.from(this.children);
		let timer;
		if (this.hasAttribute("interval"))
			timer = this.getAttribute("interval");

		else this.timer = 1000;
		this.currentIndex = -1;
		window.setInterval(() => this.Roll(this.Panels), timer);
		this.Roll(this.Panels);
	}
	disconnectedCallback() {
		window.clearInterval(() => this.Roll(this.Panels));
	}

}
customElements.define("mojo-carrousel", MojoCarrousel);